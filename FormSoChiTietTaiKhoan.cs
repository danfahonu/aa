using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormSoChiTietTaiKhoan : Form
    {
        public FormSoChiTietTaiKhoan()
        {
            InitializeComponent();
        }

        private void FormSoChiTietTaiKhoan_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = dtpTuNgay.Value.AddMonths(1).AddDays(-1);
            LoadTaiKhoanComboBox();
        }

        private void LoadTaiKhoanComboBox()
        {
            try
            {
                DataTable dt = DbHelper.Query("SELECT SOTK, TENTK FROM dbo.DM_TAIKHOANKETOAN ORDER BY SOTK");
                dt.Columns.Add("Display", typeof(string), "SOTK + ' - ' + TENTK");
                cboTaiKhoan.DataSource = dt;
                cboTaiKhoan.ValueMember = "SOTK";
                cboTaiKhoan.DisplayMember = "Display";
            }
            catch (Exception ex) { MessageBox.Show("Không thể tải danh mục tài khoản: " + ex.Message); }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (cboTaiKhoan.SelectedValue == null) { MessageBox.Show("Vui lòng chọn một tài khoản để xem.", "Thông báo"); return; }

            string taiKhoan = cboTaiKhoan.SelectedValue.ToString();
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            try
            {
                // *** SỬA LỖI: Tính toán lại từ bảng BUTTOAN_KETOAN ***
                string sqlDauKy = @"
                    SELECT ISNULL(SUM(CASE WHEN TK_NO = @TaiKhoan THEN SOTIEN ELSE -SOTIEN END), 0)
                    FROM dbo.BUTTOAN_KETOAN
                    WHERE (TK_NO = @TaiKhoan OR TK_CO = @TaiKhoan) AND NGAY_HT < @TuNgay";
                decimal duDauKy = Convert.ToDecimal(DbHelper.Scalar(sqlDauKy, DbHelper.Param("@TaiKhoan", taiKhoan), DbHelper.Param("@TuNgay", tuNgay)));

                string sqlTrongKy = @"
                    SELECT NGAY_HT AS [Ngày], SO_CT AS [Số CT], DIEN_GIAI AS [Diễn Giải], TK_NO, TK_CO, SOTIEN
                    FROM dbo.BUTTOAN_KETOAN
                    WHERE (TK_NO = @TaiKhoan OR TK_CO = @TaiKhoan) AND NGAY_HT BETWEEN @TuNgay AND @DenNgay
                    ORDER BY NGAY_HT, ID";
                DataTable dtTrongKy = DbHelper.Query(sqlTrongKy, DbHelper.Param("@TaiKhoan", taiKhoan), DbHelper.Param("@TuNgay", tuNgay), DbHelper.Param("@DenNgay", denNgay));

                DataTable dtResult = new DataTable();
                dtResult.Columns.Add("Ngày", typeof(DateTime));
                dtResult.Columns.Add("Số CT");
                dtResult.Columns.Add("Diễn Giải");
                dtResult.Columns.Add("TK Đối Ứng");
                dtResult.Columns.Add("Nợ", typeof(decimal));
                dtResult.Columns.Add("Có", typeof(decimal));
                dtResult.Columns.Add("Số Dư", typeof(decimal));

                decimal soDuHienTai = duDauKy;
                decimal tongNoTrongKy = 0;
                decimal tongCoTrongKy = 0;

                dtResult.Rows.Add(null, null, "Số dư đầu kỳ", null, null, null, soDuHienTai);

                foreach (DataRow row in dtTrongKy.Rows)
                {
                    decimal no = (row["TK_NO"].ToString() == taiKhoan) ? (decimal)row["SOTIEN"] : 0;
                    decimal co = (row["TK_CO"].ToString() == taiKhoan) ? (decimal)row["SOTIEN"] : 0;
                    string tkDoiUng = (no > 0) ? row["TK_CO"].ToString() : row["TK_NO"].ToString();

                    soDuHienTai = soDuHienTai + no - co;
                    tongNoTrongKy += no;
                    tongCoTrongKy += co;
                    dtResult.Rows.Add(row["Ngày"], row["Số CT"], row["Diễn Giải"], tkDoiUng, no, co, soDuHienTai);
                }

                gridSoChiTiet.DataSource = dtResult;

                CultureInfo culture = new CultureInfo("vi-VN");
                lblDuDauKy.Text = $"Dư Đầu Kỳ: {duDauKy.ToString("N0", culture)}";
                lblPhatSinhTrongKy.Text = $"Phát sinh Nợ: {tongNoTrongKy.ToString("N0", culture)}\nPhát sinh Có: {tongCoTrongKy.ToString("N0", culture)}";
                lblDuCuoiKy.Text = $"Dư Cuối Kỳ: {soDuHienTai.ToString("N0", culture)}";

                FormatGrid();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi xem sổ chi tiết: " + ex.Message, "Lỗi"); }
        }

        private void FormatGrid()
        {
            var currencyFormat = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };
            if (gridSoChiTiet.Columns.Contains("Nợ")) gridSoChiTiet.Columns["Nợ"].DefaultCellStyle = currencyFormat;
            if (gridSoChiTiet.Columns.Contains("Có")) gridSoChiTiet.Columns["Có"].DefaultCellStyle = currencyFormat;
            if (gridSoChiTiet.Columns.Contains("Số Dư")) gridSoChiTiet.Columns["Số Dư"].DefaultCellStyle = currencyFormat;
            gridSoChiTiet.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
    }
}