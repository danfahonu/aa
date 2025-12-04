using DoAnLapTrinhQuanLy.Data;
using DoAnLapTrinhQuanLy.Core;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormReportCongNo : Form
    {
        private Button btnPrint;

        public FormReportCongNo()
        {
            InitializeComponent();
            InitializePrintButton();
        }

        private void InitializePrintButton()
        {
            btnPrint = new Button
            {
                Text = "In Báo Cáo",
                Size = new Size(120, 36),
                Location = new Point(btnXem.Location.X + btnXem.Width + 10, btnXem.Location.Y)
            };
            btnPrint.Click += BtnPrint_Click;
            // Add to panelTop
            this.panelTop.Controls.Add(btnPrint);
            // Apply theme to new button manually as it was added after ApplyTheme
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.BackColor = ThemeManager.PrimaryColor;
            btnPrint.ForeColor = ThemeManager.LightText;
            btnPrint.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnPrint.Cursor = Cursors.Hand;
        }

        private void FormReportCongNo_Load(object sender, EventArgs e)
        {
            try
            {
                ThemeManager.Apply(this);

                dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpDenNgay.Value = dtpTuNgay.Value.AddMonths(1).AddDays(-1);

                cboLoaiCongNo.Items.Clear();
                cboLoaiCongNo.Items.Add("Khách Hàng (Phải Thu)");
                cboLoaiCongNo.Items.Add("Nhà Cung Cấp (Phải Trả)");
                cboLoaiCongNo.SelectedIndex = 0;

                // Hide Detail selection for Summary Report as we are showing the full list
                lblDoiTuong.Visible = false;
                cboDoiTuong.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải form: " + ex.Message);
            }
        }

        private void CboLoaiCongNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear grid when changing type
            if (gridCongNo.DataSource != null)
            {
                ((DataTable)gridCongNo.DataSource).Clear();
            }
        }

        private void BtnXem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string loai = cboLoaiCongNo.SelectedIndex == 0 ? "KH" : "NCC";
                string query = $@"
                    WITH PhatSinh AS (
                        SELECT MA_NCC, NGAYLAP, 0 as PhatSinhNo, SUM(t.THANHTIEN) as PhatSinhCo
                        FROM PHIEU p JOIN PHIEU_CT t ON p.SOPHIEU = t.SOPHIEU
                        WHERE p.LOAI = 'N' AND p.TRANGTHAI = 1
                        GROUP BY MA_NCC, NGAYLAP
                        UNION ALL
                        SELECT MA_NCC, NGAYLAP, SOTIEN, 0
                        FROM PHIEUTHUCHI
                        WHERE LOAI = 'C'
                        GROUP BY MA_NCC, NGAYLAP
                    )
                    SELECT
                        ncc.MA_NCC as [Mã],
                        ncc.TEN_NCC as [Tên Đối Tượng],
                        ISNULL(SUM(CASE WHEN ps.NGAYLAP < '{dtpTuNgay.Value:yyyy-MM-dd}' THEN ps.PhatSinhCo - ps.PhatSinhNo ELSE 0 END), 0) AS [Nợ Đầu Kỳ],
                        ISNULL(SUM(CASE WHEN ps.NGAYLAP BETWEEN '{dtpTuNgay.Value:yyyy-MM-dd}' AND '{dtpDenNgay.Value:yyyy-MM-dd}' THEN ps.PhatSinhCo ELSE 0 END), 0) AS [Phát Sinh Tăng],
                        ISNULL(SUM(CASE WHEN ps.NGAYLAP BETWEEN '{dtpTuNgay.Value:yyyy-MM-dd}' AND '{dtpDenNgay.Value:yyyy-MM-dd}' THEN ps.PhatSinhNo ELSE 0 END), 0) AS [Phát Sinh Giảm],
                        ISNULL(SUM(CASE WHEN ps.NGAYLAP < '{dtpTuNgay.Value:yyyy-MM-dd}' THEN ps.PhatSinhCo - ps.PhatSinhNo ELSE 0 END), 0) +
                        ISNULL(SUM(CASE WHEN ps.NGAYLAP BETWEEN '{dtpTuNgay.Value:yyyy-MM-dd}' AND '{dtpDenNgay.Value:yyyy-MM-dd}' THEN ps.PhatSinhCo ELSE 0 END), 0) -
                        ISNULL(SUM(CASE WHEN ps.NGAYLAP BETWEEN '{dtpTuNgay.Value:yyyy-MM-dd}' AND '{dtpDenNgay.Value:yyyy-MM-dd}' THEN ps.PhatSinhNo ELSE 0 END), 0) AS [Nợ Cuối Kỳ]
                    FROM DM_NHACUNGCAP ncc
                    LEFT JOIN PhatSinh ps ON ncc.MA_NCC = ps.MA_NCC
                    GROUP BY ncc.MA_NCC, ncc.TEN_NCC
                    HAVING 
                        ISNULL(SUM(CASE WHEN ps.NGAYLAP < '{dtpTuNgay.Value:yyyy-MM-dd}' THEN ps.PhatSinhCo - ps.PhatSinhNo ELSE 0 END), 0) <> 0
                        OR ISNULL(SUM(CASE WHEN ps.NGAYLAP BETWEEN '{dtpTuNgay.Value:yyyy-MM-dd}' AND '{dtpDenNgay.Value:yyyy-MM-dd}' THEN ps.PhatSinhCo ELSE 0 END), 0) <> 0
                        OR ISNULL(SUM(CASE WHEN ps.NGAYLAP BETWEEN '{dtpTuNgay.Value:yyyy-MM-dd}' AND '{dtpDenNgay.Value:yyyy-MM-dd}' THEN ps.PhatSinhNo ELSE 0 END), 0) <> 0";

                DataTable dt = DbHelper.Query(query);
                gridCongNo.DataSource = dt;

                // Format columns
                var currencyFormat = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };
                var boldFont = new Font("Segoe UI", 9F, FontStyle.Bold);

                if (gridCongNo.Columns["Nợ Đầu Kỳ"] != null) gridCongNo.Columns["Nợ Đầu Kỳ"].DefaultCellStyle = currencyFormat;
                if (gridCongNo.Columns["Phát Sinh Tăng"] != null) gridCongNo.Columns["Phát Sinh Tăng"].DefaultCellStyle = currencyFormat;
                if (gridCongNo.Columns["Phát Sinh Giảm"] != null) gridCongNo.Columns["Phát Sinh Giảm"].DefaultCellStyle = currencyFormat;
                if (gridCongNo.Columns["Nợ Cuối Kỳ"] != null)
                {
                    gridCongNo.Columns["Nợ Cuối Kỳ"].DefaultCellStyle = currencyFormat;
                    gridCongNo.Columns["Nợ Cuối Kỳ"].DefaultCellStyle.Font = boldFont;
                }

                if (gridCongNo.Columns["Tên Đối Tượng"] != null) gridCongNo.Columns["Tên Đối Tượng"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                gridCongNo.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo: " + ex.Message);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = true; // Landscape for wide report
            pd.PrintPage += Pd_PrintPage;

            PrintPreviewDialog ppd = new PrintPreviewDialog
            {
                Document = pd,
                WindowState = FormWindowState.Maximized
            };
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
            string title = "BÁO CÁO CÔNG NỢ " + (cboLoaiCongNo.SelectedIndex == 0 ? "KHÁCH HÀNG" : "NHÀ CUNG CẤP");
            SizeF titleSize = g.MeasureString(title, titleFont);
            g.DrawString(title, titleFont, Brushes.Black, (e.PageBounds.Width - titleSize.Width) / 2, y);
            y += 40;

            string dateRange = $"Từ ngày {dtpTuNgay.Value:dd/MM/yyyy} đến ngày {dtpDenNgay.Value:dd/MM/yyyy}";
            SizeF dateSize = g.MeasureString(dateRange, contentFont);
            g.DrawString(dateRange, contentFont, Brushes.Black, (e.PageBounds.Width - dateSize.Width) / 2, y);
            y += 40;

            // Table Header
            float[] colWidths = { 100, 300, 120, 120, 120, 120 }; // Adjust based on page width
            string[] headers = { "Mã", "Tên Đối Tượng", "Đầu Kỳ", "Tăng", "Giảm", "Cuối Kỳ" };
            float x = margin;

            for (int i = 0; i < headers.Length; i++)
            {
                g.FillRectangle(new SolidBrush(ThemeManager.PrimaryColor), x, y, colWidths[i], 30);
                g.DrawRectangle(Pens.Black, x, y, colWidths[i], 30);
                g.DrawString(headers[i], headerFont, Brushes.White, x + 5, y + 5);
                x += colWidths[i];
            }
            y += 30;

            // Table Content
            if (gridCongNo.DataSource != null)
            {
                DataTable dt = (DataTable)gridCongNo.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    x = margin;
                    string[] values = {
                        row["Mã"].ToString(),
                        row["Tên Đối Tượng"].ToString(),
                        Convert.ToDecimal(row["Nợ Đầu Kỳ"]).ToString("N0"),
                        Convert.ToDecimal(row["Phát Sinh Tăng"]).ToString("N0"),
                        Convert.ToDecimal(row["Phát Sinh Giảm"]).ToString("N0"),
                        Convert.ToDecimal(row["Nợ Cuối Kỳ"]).ToString("N0")
                    };

                    float rowHeight = 25;
                    for (int i = 0; i < values.Length; i++)
                    {
                        g.DrawRectangle(Pens.Black, x, y, colWidths[i], rowHeight);

                        StringFormat format = new StringFormat
                        {
                            Alignment = (i >= 2) ? StringAlignment.Far : StringAlignment.Near,
                            LineAlignment = StringAlignment.Center
                        };

                        RectangleF rect = new RectangleF(x + 5, y, colWidths[i] - 10, rowHeight);
                        g.DrawString(values[i], contentFont, Brushes.Black, rect, format);

                        x += colWidths[i];
                    }
                    y += rowHeight;
                }
            }

            // Footer Summary
            y += 20;
            g.DrawString(lblDuDauKy.Text, headerFont, Brushes.Black, margin, y);
            g.DrawString(lblPhatSinhTrongKy.Text.Replace("\n", "  "), headerFont, Brushes.Black, margin + 300, y);
            g.DrawString(lblDuCuoiKy.Text, headerFont, Brushes.Black, margin + 700, y);
        }
    }
}