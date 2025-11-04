using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormBaoCaoQuy : Form
    {
        public FormBaoCaoQuy()
        {
            InitializeComponent();
        }

        private void FormBaoCaoQuy_Load(object sender, EventArgs e)
        {
            // Mặc định xem báo cáo từ đầu tháng đến ngày hiện tại
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            // Tải báo cáo ngay khi mở form
            btnXemBaoCao_Click(sender, e);
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date;

                // 1. Tính Tồn đầu kỳ
                string queryTonDau = @"
                    SELECT ISNULL(SUM(CASE WHEN LOAI = 'T' THEN SOTIEN ELSE -SOTIEN END), 0)
                    FROM PHIEUTHUCHI
                    WHERE NGAYLAP < @TuNgay";
                object tonDauObj = DbHelper.Scalar(queryTonDau, DbHelper.Param("@TuNgay", tuNgay));
                decimal tonDau = Convert.ToDecimal(tonDauObj);
                lblTonDau.Text = tonDau.ToString("N0");

                // 2. Lấy danh sách Thu/Chi trong kỳ
                string queryTrongKy = @"
                    SELECT 
                        NGAYLAP,
                        SOPTC,
                        LYDO,
                        CASE WHEN LOAI = 'T' THEN SOTIEN ELSE 0 END AS THU,
                        CASE WHEN LOAI = 'C' THEN SOTIEN ELSE 0 END AS CHI
                    FROM PHIEUTHUCHI
                    WHERE NGAYLAP BETWEEN @TuNgay AND @DenNgay
                    ORDER BY NGAYLAP, SOPTC";
                DataTable dt = DbHelper.Query(queryTrongKy,
                                    DbHelper.Param("@TuNgay", tuNgay),
                                    DbHelper.Param("@DenNgay", denNgay));

                // Thêm cột Tồn vào DataTable
                dt.Columns.Add("TON", typeof(decimal));

                decimal tonHienTai = tonDau;
                decimal tongThu = 0;
                decimal tongChi = 0;

                foreach (DataRow row in dt.Rows)
                {
                    decimal thu = Convert.ToDecimal(row["THU"]);
                    decimal chi = Convert.ToDecimal(row["CHI"]);
                    tongThu += thu;
                    tongChi += chi;
                    tonHienTai = tonHienTai + thu - chi;
                    row["TON"] = tonHienTai;
                }

                dgvBaoCao.DataSource = dt;

                // 3. Cập nhật các số liệu tổng hợp
                lblTongThu.Text = tongThu.ToString("N0");
                lblTongChi.Text = tongChi.ToString("N0");
                lblTonCuoi.Text = tonHienTai.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}