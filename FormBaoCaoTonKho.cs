using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;
using DoAnLapTrinhQuanLy.Core;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormBaoCaoTonKho : BaseForm
    {
        public FormBaoCaoTonKho()
        {
            InitializeComponent();
            UseCustomTitleBar = false;
        }

        private void FormBaoCaoTonKho_Load(object sender, EventArgs e)
        {
            try
            {
                // Khi form load, chạy báo cáo ngay
                btnXemBaoCao_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                // Tối ưu hóa: Tính toán GIATRI_TON ngay trong SQL
                string query = @"
                    SELECT 
                        h.MAHH,
                        h.TENHH,
                        nh.TENNHOM,
                        h.DVT,
                        ISNULL(v.TON_HIEN_TAI, 0) AS TONKHO,
                        h.GIAVON,
                        (ISNULL(v.TON_HIEN_TAI, 0) * ISNULL(h.GIAVON, 0)) AS GIATRI_TON
                    FROM 
                        DM_HANGHOA h
                    LEFT JOIN 
                        DM_NHOMHANG nh ON h.MANHOM = nh.MANHOM
                    LEFT JOIN
                        vw_TonKhoHienTai v ON h.MAHH = v.MAHH
                    ORDER BY 
                        nh.TENNHOM, h.TENHH";

                DataTable dt = DbHelper.Query(query);
                dgvBaoCao.DataSource = dt;

                // Định dạng cột
                if (dgvBaoCao.Columns["GIAVON"] != null) dgvBaoCao.Columns["GIAVON"].DefaultCellStyle.Format = "N0";
                if (dgvBaoCao.Columns["GIATRI_TON"] != null) dgvBaoCao.Columns["GIATRI_TON"].DefaultCellStyle.Format = "N0";

                // Tính tổng giá trị từ DataTable (nhanh hơn loop UI)
                object sumObj = dt.Compute("SUM(GIATRI_TON)", "");
                decimal tongGiaTri = (sumObj == DBNull.Value) ? 0 : Convert.ToDecimal(sumObj);

                lblTongGiaTri.Text = tongGiaTri.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintPage);

            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = pd;
            dlg.ShowDialog();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            // Vẽ tiêu đề
            Font titleFont = new Font("Arial", 20, FontStyle.Bold);
            e.Graphics.DrawString("BÁO CÁO TỒN KHO", titleFont, Brushes.Black, new Point(250, 50));

            // Vẽ ngày tháng
            e.Graphics.DrawString("Ngày in: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), new Font("Arial", 10), Brushes.Black, new Point(250, 90));

            // Vẽ bảng (đơn giản hóa)
            int y = 150;
            int x = 50;

            // Header
            e.Graphics.DrawString("Mã HH", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, x, y);
            e.Graphics.DrawString("Tên Hàng Hóa", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, x + 100, y);
            e.Graphics.DrawString("Tồn Kho", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, x + 400, y);
            e.Graphics.DrawString("Giá Trị", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, x + 500, y);

            y += 30;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 600, y);
            y += 10;

            // Data
            if (dgvBaoCao.DataSource is DataTable dt)
            {
                foreach (DataRow row in dt.Rows)
                {
                    e.Graphics.DrawString(row["MAHH"].ToString(), new Font("Arial", 10), Brushes.Black, x, y);

                    // Cắt tên nếu quá dài
                    string tenHH = row["TENHH"].ToString();
                    if (tenHH.Length > 30) tenHH = tenHH.Substring(0, 27) + "...";
                    e.Graphics.DrawString(tenHH, new Font("Arial", 10), Brushes.Black, x + 100, y);

                    e.Graphics.DrawString(row["TONKHO"].ToString(), new Font("Arial", 10), Brushes.Black, x + 400, y);

                    decimal giaTri = row["GIATRI_TON"] != DBNull.Value ? Convert.ToDecimal(row["GIATRI_TON"]) : 0;
                    e.Graphics.DrawString(giaTri.ToString("N0"), new Font("Arial", 10), Brushes.Black, x + 500, y);

                    y += 25;
                }
            }

            // Tổng cộng
            y += 20;
            e.Graphics.DrawLine(Pens.Black, x, y, x + 600, y);
            y += 10;
            e.Graphics.DrawString("TỔNG CỘNG: " + lblTongGiaTri.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, x + 300, y);
        }
    }
}