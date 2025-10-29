using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormBaoCaoQuy : Form
    {
        private const string TAI_KHOAN_QUY = "111"; // << Tài khoản tiền mặt

        public FormBaoCaoQuy()
        {
            InitializeComponent();
        }

        private void FormBaoCaoQuy_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = dtpTuNgay.Value.AddMonths(1).AddDays(-1);
            cboLoaiPhieu.Items.AddRange(new object[] { "-- Tất cả --", "Chỉ xem Phiếu Thu", "Chỉ xem Phiếu Chi" });
            cboLoaiPhieu.SelectedIndex = 0;
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            try
            {
                // *** SỬA LỖI: Tính toán lại từ bảng BUTTOAN_KETOAN ***
                string sqlDauKy = $@"
                    SELECT ISNULL(SUM(CASE WHEN TK_NO = @TaiKhoanQuy THEN SOTIEN ELSE -SOTIEN END), 0)
                    FROM dbo.BUTTOAN_KETOAN
                    WHERE (TK_NO = @TaiKhoanQuy OR TK_CO = @TaiKhoanQuy) AND NGAY_HT < @TuNgay";
                decimal soDuDauKy = Convert.ToDecimal(DbHelper.Scalar(sqlDauKy, DbHelper.Param("@TaiKhoanQuy", TAI_KHOAN_QUY), DbHelper.Param("@TuNgay", tuNgay)));

                string sqlTrongKy = $@"
                    SELECT NGAY_HT, SO_CT, DIEN_GIAI, TK_NO, TK_CO, SOTIEN
                    FROM dbo.BUTTOAN_KETOAN
                    WHERE (TK_NO = @TaiKhoanQuy OR TK_CO = @TaiKhoanQuy) AND NGAY_HT BETWEEN @TuNgay AND @DenNgay";

                // Lọc theo loại phiếu nếu người dùng chọn
                if (cboLoaiPhieu.SelectedIndex == 1) sqlTrongKy += " AND MA_CT = 'PT'";
                if (cboLoaiPhieu.SelectedIndex == 2) sqlTrongKy += " AND MA_CT = 'PC'";
                sqlTrongKy += " ORDER BY NGAY_HT, ID";

                DataTable dtTrongKy = DbHelper.Query(sqlTrongKy, DbHelper.Param("@TaiKhoanQuy", TAI_KHOAN_QUY), DbHelper.Param("@TuNgay", tuNgay), DbHelper.Param("@DenNgay", denNgay));

                DataTable dtResult = new DataTable();
                dtResult.Columns.Add("Ngày", typeof(DateTime));
                dtResult.Columns.Add("Số CT");
                dtResult.Columns.Add("Diễn giải");
                dtResult.Columns.Add("Thu", typeof(decimal));
                dtResult.Columns.Add("Chi", typeof(decimal));
                dtResult.Columns.Add("Tồn", typeof(decimal));

                decimal tonHienTai = soDuDauKy;
                decimal tongThuTrongKy = 0;
                decimal tongChiTrongKy = 0;

                dtResult.Rows.Add(null, null, "Tồn đầu kỳ", null, null, tonHienTai);

                foreach (DataRow row in dtTrongKy.Rows)
                {
                    decimal thu = (row["TK_NO"].ToString() == TAI_KHOAN_QUY) ? (decimal)row["SOTIEN"] : 0;
                    decimal chi = (row["TK_CO"].ToString() == TAI_KHOAN_QUY) ? (decimal)row["SOTIEN"] : 0;
                    tonHienTai = tonHienTai + thu - chi;
                    tongThuTrongKy += thu;
                    tongChiTrongKy += chi;
                    dtResult.Rows.Add(row["NGAY_HT"], row["SO_CT"], row["DIEN_GIAI"], thu, chi, tonHienTai);
                }

                gridBaoCao.DataSource = dtResult;

                CultureInfo culture = new CultureInfo("vi-VN");
                staSoDuDau.Text = $"Số dư đầu: {soDuDauKy.ToString("N0", culture)}";
                staTongThu.Text = $"Tổng thu: {tongThuTrongKy.ToString("N0", culture)}";
                staTongChi.Text = $"Tổng chi: {tongChiTrongKy.ToString("N0", culture)}";
                staSoDuCuoi.Text = $"Số dư cuối: {tonHienTai.ToString("N0", culture)}";

                FormatGrid();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi xem báo cáo quỹ: " + ex.Message, "Lỗi"); }
        }

        private void FormatGrid()
        {
            var currencyFormat = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };
            if (gridBaoCao.Columns.Contains("Thu")) gridBaoCao.Columns["Thu"].DefaultCellStyle = currencyFormat;
            if (gridBaoCao.Columns.Contains("Chi")) gridBaoCao.Columns["Chi"].DefaultCellStyle = currencyFormat;
            if (gridBaoCao.Columns.Contains("Tồn")) gridBaoCao.Columns["Tồn"].DefaultCellStyle = currencyFormat;
            gridBaoCao.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
    }
}