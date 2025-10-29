using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormSoNhatKyChung : Form
    {
        public FormSoNhatKyChung()
        {
            InitializeComponent();
        }

        private void FormSoNhatKyChung_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = dtpTuNgay.Value.AddMonths(1).AddDays(-1);
            btnXem.PerformClick();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            try
            {
                // *** SỬA LỖI: Truy vấn trực tiếp vào bảng BUTTOAN_KETOAN ***
                string sql = @"
                    SELECT 
                        NGAY_HT AS [Ngày Ghi Sổ],
                        SO_CT AS [Số CT],
                        DIEN_GIAI AS [Diễn Giải],
                        TK_NO AS [TK Nợ],
                        TK_CO AS [TK Có],
                        SOTIEN AS [Số Tiền Phát Sinh]
                    FROM dbo.BUTTOAN_KETOAN
                    WHERE NGAY_HT BETWEEN @TuNgay AND @DenNgay
                    ORDER BY NGAY_HT, ID";

                DataTable dt = DbHelper.Query(sql,
                    DbHelper.Param("@TuNgay", tuNgay),
                    DbHelper.Param("@DenNgay", denNgay));

                gridNhatKy.DataSource = dt;

                decimal tongPhatSinh = 0;
                foreach (DataRow row in dt.Rows)
                {
                    tongPhatSinh += Convert.ToDecimal(row["Số Tiền Phát Sinh"]);
                }
                staTongPhatSinh.Text = $"Tổng phát sinh Nợ / Có: {tongPhatSinh.ToString("N0", new CultureInfo("vi-VN"))}";

                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem sổ nhật ký chung: " + ex.Message, "Lỗi");
            }
        }

        private void FormatGrid()
        {
            if (gridNhatKy.Columns.Contains("Số Tiền Phát Sinh"))
            {
                gridNhatKy.Columns["Số Tiền Phát Sinh"].DefaultCellStyle.Format = "N0";
                gridNhatKy.Columns["Số Tiền Phát Sinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            gridNhatKy.Columns["Diễn Giải"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}