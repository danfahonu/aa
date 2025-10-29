using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormBaoCaoTonKho : Form
    {
        public FormBaoCaoTonKho()
        {
            InitializeComponent();
        }

        private void FormBaoCaoTonKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Câu truy vấn này tổng hợp số lượng tồn và giá trị tồn kho từ bảng chi tiết
                // Nó sẽ tính đúng giá trị tồn kho dựa trên giá nhập của từng lô hàng còn lại
                string sql = @"
                    SELECT
                        hh.MAHH AS [Mã Hàng],
                        hh.TENHH AS [Tên Hàng Hóa],
                        hh.DVT AS [ĐVT],
                        ISNULL(SUM(kct.SO_LUONG_TON), 0) AS [Số Lượng Tồn],
                        ISNULL(SUM(kct.SO_LUONG_TON * kct.DON_GIA_NHAP), 0) AS [Giá Trị Tồn Kho]
                    FROM
                        dbo.DM_HANGHOA hh
                    LEFT JOIN
                        dbo.KHO_CHITIET_TONKHO kct ON hh.MAHH = kct.MAHH
                    GROUP BY
                        hh.MAHH, hh.TENHH, hh.DVT
                    ORDER BY
                        hh.TENHH;
                ";

                DataTable dt = DbHelper.Query(sql);
                gridTonKho.DataSource = dt;

                // Tính tổng giá trị tồn kho và hiển thị lên StatusStrip
                decimal tongGiaTri = 0;
                foreach (DataRow row in dt.Rows)
                {
                    tongGiaTri += Convert.ToDecimal(row["Giá Trị Tồn Kho"]);
                }

                CultureInfo culture = new CultureInfo("vi-VN");
                staTongGiaTri.Text = $"Tổng giá trị tồn kho: {tongGiaTri.ToString("c", culture)}";

                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo tồn kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            // Định dạng cho các cột số và tiền tệ
            var currencyFormat = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };
            var numberFormat = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleCenter };

            if (gridTonKho.Columns.Contains("Số Lượng Tồn"))
            {
                gridTonKho.Columns["Số Lượng Tồn"].DefaultCellStyle = numberFormat;
            }
            if (gridTonKho.Columns.Contains("Giá Trị Tồn Kho"))
            {
                gridTonKho.Columns["Giá Trị Tồn Kho"].DefaultCellStyle = currencyFormat;
            }

            // Tự động điều chỉnh độ rộng cột
            gridTonKho.Columns["Tên Hàng Hóa"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridTonKho.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
        }
    }
}