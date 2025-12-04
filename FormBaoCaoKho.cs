using DoAnLapTrinhQuanLy.Core;
using DoAnLapTrinhQuanLy.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormBaoCaoKho : BaseForm
    {
        private Button btnPrint;

        public FormBaoCaoKho()
        {
            InitializeComponent();
            UseCustomTitleBar = false;
            // ThemeManager.Apply(this); // Handled by BaseForm
            InitializePrintButton();

            // Adjust UI for Stock Card
            this.Text = "Thẻ Kho (Sổ Chi Tiết Vật Tư)";
            lblDoiTac.Text = "Chọn Hàng Hóa:";
            chkXemChiTiet.Visible = false; // Stock Card is always detailed
        }

        private void InitializePrintButton()
        {
            btnPrint = new Button();
            btnPrint.Text = "In Thẻ Kho";
            btnPrint.Size = new Size(120, 34);
            btnPrint.Location = new Point(btnXemBaoCao.Location.X + btnXemBaoCao.Width + 10, btnXemBaoCao.Location.Y);
            btnPrint.Click += BtnPrint_Click;
            this.panelTop.Controls.Add(btnPrint);

            // Apply theme
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.BackColor = ThemeManager.PrimaryColor;
            btnPrint.ForeColor = ThemeManager.LightText;
            btnPrint.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnPrint.Cursor = Cursors.Hand;
        }

        private void FormBaoCaoKho_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;
            LoadComboBoxHangHoa();
        }

        private void LoadComboBoxHangHoa()
        {
            // Load list of active products
            DataTable dt = DbHelper.Query("SELECT MAHH, TENHH FROM DM_HANGHOA WHERE ACTIVE = 1 ORDER BY TENHH");

            // Add custom display column
            dt.Columns.Add("Display", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                row["Display"] = $"{row["MAHH"]} - {row["TENHH"]}";
            }

            cboDoiTac.DataSource = dt;
            cboDoiTac.ValueMember = "MAHH";
            cboDoiTac.DisplayMember = "Display";

            // Enable autocomplete
            cboDoiTac.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboDoiTac.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void BtnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (cboDoiTac.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn hàng hóa để xem thẻ kho.", "Thông báo");
                return;
            }

            string maHH = cboDoiTac.SelectedValue.ToString();
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            try
            {
                // 1. Calculate Opening Stock (Tồn Đầu)
                string sqlTonDau = @"
                    SELECT 
                        (SELECT ISNULL(SUM(ct.SL), 0) 
                         FROM PHIEU p JOIN PHIEU_CT ct ON p.SOPHIEU = ct.SOPHIEU 
                         WHERE p.LOAI = 'N' AND p.TRANGTHAI = 1 AND ct.MAHH = @MaHH AND p.NGAYLAP < @TuNgay)
                        -
                        (SELECT ISNULL(SUM(ct.SL), 0) 
                         FROM PHIEU p JOIN PHIEU_CT ct ON p.SOPHIEU = ct.SOPHIEU 
                         WHERE p.LOAI = 'X' AND p.TRANGTHAI = 1 AND ct.MAHH = @MaHH AND p.NGAYLAP < @TuNgay)
                ";

                object objTonDau = DbHelper.Scalar(sqlTonDau,
                    DbHelper.Param("@MaHH", maHH),
                    DbHelper.Param("@TuNgay", tuNgay));

                int tonDau = Convert.ToInt32(objTonDau);

                // 2. Get Transactions in Range
                string sqlTrongKy = @"
                    SELECT 
                        p.NGAYLAP,
                        p.SOPHIEU,
                        p.LOAI,
                        CASE WHEN p.LOAI = 'N' THEN N'Nhập' ELSE N'Xuất' END AS DIENGIAI,
                        ct.SL,
                        ct.DONGIA
                    FROM PHIEU p 
                    JOIN PHIEU_CT ct ON p.SOPHIEU = ct.SOPHIEU
                    WHERE ct.MAHH = @MaHH 
                      AND p.TRANGTHAI = 1
                      AND p.NGAYLAP BETWEEN @TuNgay AND @DenNgay
                    ORDER BY p.NGAYLAP, p.SOPHIEU
                ";

                DataTable dtTrongKy = DbHelper.Query(sqlTrongKy,
                    DbHelper.Param("@MaHH", maHH),
                    DbHelper.Param("@TuNgay", tuNgay),
                    DbHelper.Param("@DenNgay", denNgay));

                // 3. Process Data
                DataTable dtResult = new DataTable();
                dtResult.Columns.Add("Ngày", typeof(DateTime));
                dtResult.Columns.Add("Số Phiếu");
                dtResult.Columns.Add("Diễn Giải");
                dtResult.Columns.Add("Nhập", typeof(int));
                dtResult.Columns.Add("Xuất", typeof(int));
                dtResult.Columns.Add("Tồn", typeof(int));

                // Add Opening Row
                dtResult.Rows.Add(tuNgay, "", "Số dư đầu kỳ", 0, 0, tonDau);

                int tonHienTai = tonDau;
                int tongNhap = 0;
                int tongXuat = 0;

                foreach (DataRow row in dtTrongKy.Rows)
                {
                    string loai = row["LOAI"].ToString();
                    int sl = Convert.ToInt32(row["SL"]);

                    int nhap = (loai == "N") ? sl : 0;
                    int xuat = (loai == "X") ? sl : 0;

                    tonHienTai = tonHienTai + nhap - xuat;
                    tongNhap += nhap;
                    tongXuat += xuat;

                    dtResult.Rows.Add(
                        row["NGAYLAP"],
                        row["SOPHIEU"],
                        row["DIENGIAI"],
                        nhap,
                        xuat,
                        tonHienTai
                    );
                }

                gridBaoCao.DataSource = dtResult;
                FormatGrid();

                // Update Status
                staTongTien.Text = $"Tồn đầu: {tonDau:N0} | Nhập: {tongNhap:N0} | Xuất: {tongXuat:N0} | Tồn cuối: {tonHienTai:N0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thẻ kho: " + ex.Message, "Lỗi");
            }
        }

        private void FormatGrid()
        {
            if (gridBaoCao.Columns.Count == 0) return;

            var numberFormat = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };
            var boldFont = new Font("Segoe UI", 9F, FontStyle.Bold);

            gridBaoCao.Columns["Nhập"].DefaultCellStyle = numberFormat;
            gridBaoCao.Columns["Xuất"].DefaultCellStyle = numberFormat;
            gridBaoCao.Columns["Tồn"].DefaultCellStyle = numberFormat;
            gridBaoCao.Columns["Tồn"].DefaultCellStyle.Font = boldFont;

            gridBaoCao.Columns["Diễn Giải"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridBaoCao.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (gridBaoCao.Rows.Count == 0) return;

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
            Font titleFont = new Font("Segoe UI", 16, FontStyle.Bold);
            Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font contentFont = new Font("Segoe UI", 10);

            float y = 50;
            // float margin = 50; // Removed unused variable

            // Title
            string title = "THẺ KHO";
            SizeF titleSize = g.MeasureString(title, titleFont);
            g.DrawString(title, titleFont, Brushes.Black, (e.PageBounds.Width - titleSize.Width) / 2, y);
            y += 30;

            // Subtitle
            string subTitle = cboDoiTac.Text;
            SizeF subSize = g.MeasureString(subTitle, headerFont);
            g.DrawString(subTitle, headerFont, Brushes.Black, (e.PageBounds.Width - subSize.Width) / 2, y);
            y += 30;

            string dateRange = $"Từ ngày {dtpTuNgay.Value:dd/MM/yyyy} đến ngày {dtpDenNgay.Value:dd/MM/yyyy}";
            SizeF dateSize = g.MeasureString(dateRange, contentFont);
            g.DrawString(dateRange, contentFont, Brushes.Black, (e.PageBounds.Width - dateSize.Width) / 2, y);
            y += 40;

            // Table Header
            float[] colWidths = { 100, 100, 150, 80, 80, 80 };
            string[] headers = { "Ngày", "Số Phiếu", "Diễn Giải", "Nhập", "Xuất", "Tồn" };
            float x = (e.PageBounds.Width - 600) / 2; // Center table (approx width 600)

            for (int i = 0; i < headers.Length; i++)
            {
                g.FillRectangle(new SolidBrush(ThemeManager.PrimaryColor), x, y, colWidths[i], 30);
                g.DrawRectangle(Pens.Black, x, y, colWidths[i], 30);
                g.DrawString(headers[i], headerFont, Brushes.White, x + 5, y + 5);
                x += colWidths[i];
            }
            y += 30;

            // Table Content
            if (gridBaoCao.DataSource != null)
            {
                DataTable dt = (DataTable)gridBaoCao.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    x = (e.PageBounds.Width - 600) / 2;

                    string ngay = row["Ngày"] != DBNull.Value ? Convert.ToDateTime(row["Ngày"]).ToString("dd/MM/yyyy") : "";

                    string[] values = {
                        ngay,
                        row["Số Phiếu"].ToString(),
                        row["Diễn Giải"].ToString(),
                        Convert.ToInt32(row["Nhập"]).ToString("N0"),
                        Convert.ToInt32(row["Xuất"]).ToString("N0"),
                        Convert.ToInt32(row["Tồn"]).ToString("N0")
                    };

                    float rowHeight = 25;
                    for (int i = 0; i < values.Length; i++)
                    {
                        g.DrawRectangle(Pens.Black, x, y, colWidths[i], rowHeight);

                        StringFormat format = new StringFormat();
                        format.Alignment = (i >= 3) ? StringAlignment.Far : StringAlignment.Near;
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
