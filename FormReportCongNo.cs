using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormReportCongNo : Form
    {
        public FormReportCongNo()
        {
            InitializeComponent();
        }

        private void FormReportCongNo_Load(object sender, EventArgs e)
        {
            // Cài đặt ngày mặc định là tháng hiện tại
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = dtpTuNgay.Value.AddMonths(1).AddDays(-1);

            // Cài đặt ComboBox chọn loại công nợ
            var items = new[] {
                new { Value = "PHAITHU", Text = "Sổ Công Nợ Phải Thu (TK 131)" },
                new { Value = "PHAITRA", Text = "Sổ Công Nợ Phải Trả (TK 331)" }
            };
            cboLoaiCongNo.DataSource = items;
            cboLoaiCongNo.ValueMember = "Value";
            cboLoaiCongNo.DisplayMember = "Text";
            cboLoaiCongNo.SelectedIndex = 0; // Tự động kích hoạt sự kiện SelectedIndexChanged
        }

        private void cboLoaiCongNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tải lại danh sách đối tượng (Khách hàng hoặc NCC) khi thay đổi loại sổ
            string loai = cboLoaiCongNo.SelectedValue.ToString();
            DataTable dt;

            if (loai == "PHAITHU")
            {
                lblDoiTuong.Text = "Khách hàng:";
                dt = DbHelper.Query("SELECT MAKH AS ID, TENKH AS Name FROM dbo.DANHMUCKHACHHANG ORDER BY TENKH");
            }
            else // PHAITRA
            {
                lblDoiTuong.Text = "Nhà cung cấp:";
                dt = DbHelper.Query("SELECT MA_NCC AS ID, TEN_NCC AS Name FROM dbo.DM_NHACUNGCAP ORDER BY TEN_NCC");
            }
            cboDoiTuong.DataSource = dt;
            cboDoiTuong.ValueMember = "ID";
            cboDoiTuong.DisplayMember = "Name";
            cboDoiTuong.SelectedIndex = -1;
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (cboDoiTuong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn một đối tượng để xem công nợ.", "Thông báo");
                return;
            }

            string loaiCongNo = cboLoaiCongNo.SelectedValue.ToString();
            string maDoiTuong = cboDoiTuong.SelectedValue.ToString();
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            // Xác định tài khoản kế toán công nợ tương ứng
            string taiKhoanCongNo = (loaiCongNo == "PHAITHU") ? "131" : "331";

            try
            {
                // ---- BƯỚC 1: TÍNH SỐ DƯ ĐẦU KỲ TỪ BẢNG BUTTOAN_KETOAN ----
                string sqlDauKy = $@"
                    SELECT 
                        ISNULL(SUM(CASE WHEN TK_NO = @TaiKhoan THEN SOTIEN ELSE 0 END), 0) - 
                        ISNULL(SUM(CASE WHEN TK_CO = @TaiKhoan THEN SOTIEN ELSE 0 END), 0)
                    FROM dbo.BUTTOAN_KETOAN
                    WHERE (TK_NO = @TaiKhoan OR TK_CO = @TaiKhoan) 
                      AND DIEN_GIAI LIKE '%' + @MaDoiTuong + '%'
                      AND NGAY_HT < @TuNgay";

                object duDauKyObj = DbHelper.Scalar(sqlDauKy,
                    DbHelper.Param("@TaiKhoan", taiKhoanCongNo),
                    DbHelper.Param("@MaDoiTuong", maDoiTuong),
                    DbHelper.Param("@TuNgay", tuNgay));

                decimal duDauKy = (duDauKyObj == DBNull.Value) ? 0 : Convert.ToDecimal(duDauKyObj);

                // ---- BƯỚC 2: LIỆT KÊ PHÁT SINH TRONG KỲ TỪ BẢNG BUTTOAN_KETOAN ----
                string sqlTrongKy = $@"
                    SELECT 
                        NGAY_HT AS [Ngày],
                        SO_CT AS [Số Phiếu],
                        DIEN_GIAI AS [Diễn Giải],
                        CASE WHEN TK_NO = @TaiKhoan THEN SOTIEN ELSE 0 END AS [Nợ],
                        CASE WHEN TK_CO = @TaiKhoan THEN SOTIEN ELSE 0 END AS [Có]
                    FROM dbo.BUTTOAN_KETOAN
                    WHERE (TK_NO = @TaiKhoan OR TK_CO = @TaiKhoan) 
                      AND DIEN_GIAI LIKE '%' + @MaDoiTuong + '%'
                      AND NGAY_HT BETWEEN @TuNgay AND @DenNgay
                    ORDER BY NGAY_HT, ID";

                DataTable dtTrongKy = DbHelper.Query(sqlTrongKy,
                    DbHelper.Param("@TaiKhoan", taiKhoanCongNo),
                    DbHelper.Param("@MaDoiTuong", maDoiTuong),
                    DbHelper.Param("@TuNgay", tuNgay),
                    DbHelper.Param("@DenNgay", denNgay));

                // ---- BƯỚC 3: XỬ LÝ VÀ HIỂN THỊ ----
                DataTable dtResult = new DataTable();
                dtResult.Columns.Add("Ngày", typeof(DateTime));
                dtResult.Columns.Add("Số Phiếu");
                dtResult.Columns.Add("Diễn Giải");
                dtResult.Columns.Add("Nợ", typeof(decimal));
                dtResult.Columns.Add("Có", typeof(decimal));
                dtResult.Columns.Add("Số Dư", typeof(decimal));

                decimal soDuHienTai = duDauKy;
                decimal tongNoTrongKy = 0;
                decimal tongCoTrongKy = 0;

                // Thêm dòng số dư đầu kỳ
                dtResult.Rows.Add(null, null, "Số dư đầu kỳ", null, null, soDuHienTai);

                // Thêm các dòng phát sinh và tính lũy kế số dư
                foreach (DataRow row in dtTrongKy.Rows)
                {
                    decimal no = (decimal)row["Nợ"];
                    decimal co = (decimal)row["Có"];
                    soDuHienTai = soDuHienTai + no - co;
                    tongNoTrongKy += no;
                    tongCoTrongKy += co;
                    dtResult.Rows.Add(row["Ngày"], row["Số Phiếu"], row["Diễn Giải"], no, co, soDuHienTai);
                }

                gridCongNo.DataSource = dtResult;

                // Cập nhật các label tổng kết
                CultureInfo culture = new CultureInfo("vi-VN");
                lblDuDauKy.Text = $"Dư Đầu Kỳ: {duDauKy.ToString("N0", culture)}";
                lblPhatSinhTrongKy.Text = $"Phát sinh Nợ: {tongNoTrongKy.ToString("N0", culture)}\nPhát sinh Có: {tongCoTrongKy.ToString("N0", culture)}";
                lblDuCuoiKy.Text = $"Dư Cuối Kỳ: {soDuHienTai.ToString("N0", culture)}";

                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem sổ công nợ: " + ex.Message, "Lỗi");
            }
        }

        private void FormatGrid()
        {
            var currencyFormat = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };
            var boldFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            gridCongNo.Columns["Nợ"].DefaultCellStyle = currencyFormat;
            gridCongNo.Columns["Có"].DefaultCellStyle = currencyFormat;
            gridCongNo.Columns["Số Dư"].DefaultCellStyle = currencyFormat;
            gridCongNo.Columns["Số Dư"].DefaultCellStyle.Font = boldFont;

            // In đậm dòng số dư đầu kỳ
            if (gridCongNo.Rows.Count > 0)
            {
                gridCongNo.Rows[0].DefaultCellStyle.Font = boldFont;
                gridCongNo.Rows[0].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            }

            gridCongNo.Columns["Diễn Giải"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridCongNo.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
    }
}