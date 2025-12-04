using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;
using DoAnLapTrinhQuanLy.Core;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormBaoCaoQuy : BaseForm
    {
        private Button btnPrint; // Declare the button

        public FormBaoCaoQuy()
        {
            InitializeComponent();
            UseCustomTitleBar = false;
            InitializePrintButton(); // Initialize it programmatically
        }

        private void InitializePrintButton()
        {
            // Create the button
            btnPrint = new Button();
            btnPrint.Name = "btnPrint";
            btnPrint.Text = "In Báo Cáo";
            btnPrint.Size = new Size(120, 30);
            // Position it next to btnXemBaoCao
            btnPrint.Location = new Point(btnXemBaoCao.Location.X + btnXemBaoCao.Width + 10, btnXemBaoCao.Location.Y);

            // Style it
            btnPrint.BackColor = ThemeManager.PrimaryColor;
            btnPrint.ForeColor = ThemeManager.LightText;
            btnPrint.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Cursor = Cursors.Hand;

            // Add event handler
            btnPrint.Click += BtnPrint_Click;

            // Add to the filters panel
            this.pnlFilters.Controls.Add(btnPrint);
        }

        private void FormBaoCaoQuy_Load(object sender, EventArgs e)
        {
            try
            {
                ThemeManager.Apply(this);

                // Mặc định xem báo cáo từ đầu tháng đến ngày hiện tại
                dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpDenNgay.Value = DateTime.Now;

                // Tải báo cáo ngay khi mở form
                BtnXemBaoCao_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải form: " + ex.Message);
            }
        }

        private void BtnXemBaoCao_Click(object sender, EventArgs e)
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

                // 2. Lấy danh sách Thu/Chi trong kỳ theo NGÀY
                string queryTrongKy = @"
                    SELECT 
                        NGAYLAP,
                        SUM(CASE WHEN LOAI = 'T' THEN SOTIEN ELSE 0 END) AS THU,
                        SUM(CASE WHEN LOAI = 'C' THEN SOTIEN ELSE 0 END) AS CHI
                    FROM PHIEUTHUCHI
                    WHERE NGAYLAP BETWEEN @TuNgay AND @DenNgay
                    GROUP BY NGAYLAP
                    ORDER BY NGAYLAP";

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
                FormatGrid();

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

        private void FormatGrid()
        {
            if (dgvBaoCao.Columns.Count == 0) return;

            var currencyFormat = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };
            var boldFont = new Font("Segoe UI", 9F, FontStyle.Bold);

            dgvBaoCao.Columns["THU"].DefaultCellStyle = currencyFormat;
            dgvBaoCao.Columns["CHI"].DefaultCellStyle = currencyFormat;
            dgvBaoCao.Columns["TON"].DefaultCellStyle = currencyFormat;
            dgvBaoCao.Columns["TON"].DefaultCellStyle.Font = boldFont;

            dgvBaoCao.Columns["THU"].HeaderText = "Tổng Thu";
            dgvBaoCao.Columns["CHI"].HeaderText = "Tổng Chi";
            dgvBaoCao.Columns["TON"].HeaderText = "Tồn Quỹ";
            dgvBaoCao.Columns["NGAYLAP"].HeaderText = "Ngày";
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintPage;

            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            ppd.WindowState = FormWindowState.Maximized;
            ppd.ShowDialog();
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
            Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font contentFont = new Font("Segoe UI", 10);

            float y = 50;
            float margin = 50;

            // Title
            string title = "BÁO CÁO QUỸ TIỀN MẶT";
            SizeF titleSize = g.MeasureString(title, titleFont);
            g.DrawString(title, titleFont, Brushes.Black, (e.PageBounds.Width - titleSize.Width) / 2, y);
            y += 40;

            string dateRange = $"Từ ngày {dtpTuNgay.Value:dd/MM/yyyy} đến ngày {dtpDenNgay.Value:dd/MM/yyyy}";
            SizeF dateSize = g.MeasureString(dateRange, contentFont);
            g.DrawString(dateRange, contentFont, Brushes.Black, (e.PageBounds.Width - dateSize.Width) / 2, y);
            y += 40;

            // Summary Info
            g.DrawString($"Tồn Đầu Kỳ: {lblTonDau.Text}", headerFont, Brushes.Black, margin, y);
            y += 20;
            g.DrawString($"Tổng Thu: {lblTongThu.Text}", headerFont, Brushes.Black, margin, y);
            y += 20;
            g.DrawString($"Tổng Chi: {lblTongChi.Text}", headerFont, Brushes.Black, margin, y);
            y += 20;
            g.DrawString($"Tồn Cuối Kỳ: {lblTonCuoi.Text}", headerFont, Brushes.Black, margin, y);
            y += 40;

            // Table Header
            float[] colWidths = { 150, 150, 150, 150 };
            string[] headers = { "Ngày", "Tổng Thu", "Tổng Chi", "Tồn Quỹ" };
            float x = margin;
            float tableWidth = 600;
            // Center table
            x = (e.PageBounds.Width - tableWidth) / 2;

            for (int i = 0; i < headers.Length; i++)
            {
                g.FillRectangle(new SolidBrush(ThemeManager.PrimaryColor), x, y, colWidths[i], 30);
                g.DrawRectangle(Pens.Black, x, y, colWidths[i], 30);
                g.DrawString(headers[i], headerFont, Brushes.White, x + 5, y + 5);
                x += colWidths[i];
            }
            y += 30;

            // Table Content
            if (dgvBaoCao.DataSource != null)
            {
                DataTable dt = (DataTable)dgvBaoCao.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    x = (e.PageBounds.Width - tableWidth) / 2;
                    string[] values = {
                        Convert.ToDateTime(row["NGAYLAP"]).ToString("dd/MM/yyyy"),
                        Convert.ToDecimal(row["THU"]).ToString("N0"),
                        Convert.ToDecimal(row["CHI"]).ToString("N0"),
                        Convert.ToDecimal(row["TON"]).ToString("N0")
                    };

                    float rowHeight = 25;
                    for (int i = 0; i < values.Length; i++)
                    {
                        g.DrawRectangle(Pens.Black, x, y, colWidths[i], rowHeight);

                        StringFormat format = new StringFormat();
                        format.Alignment = (i >= 1) ? StringAlignment.Far : StringAlignment.Near;
                        format.LineAlignment = StringAlignment.Center;

                        RectangleF rect = new RectangleF(x + 5, y, colWidths[i] - 10, rowHeight);
                        g.DrawString(values[i], contentFont, Brushes.Black, rect, format);

                        x += colWidths[i];
                    }
                    y += rowHeight;
                }
            }
        }
    }
}
