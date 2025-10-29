using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private void LoadAllData()
        {
            try
            {
                LoadSummaryCards();
                LoadGiaTriTonKhoChart();
                LoadTop5HangHoaTonKho();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải dữ liệu Dashboard. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSummaryCards()
        {
            string sql = @"
                -- 1. Tổng giá trị tồn kho (tính từ giá vốn nhập)
                SELECT ISNULL(SUM(SO_LUONG_TON * DON_GIA_NHAP), 0) FROM dbo.KHO_CHITIET_TONKHO;

                -- 2. Giá vốn hàng bán trong tháng
                SELECT ISNULL(SUM(SOTIEN), 0) FROM dbo.BUTTOAN_KETOAN
                WHERE TK_NO = '632' AND MONTH(NGAY_HT) = MONTH(GETDATE()) AND YEAR(NGAY_HT) = YEAR(GETDATE());

                -- 3. Số lượng mã hàng tồn kho lâu ngày (> 30 ngày)
                SELECT COUNT(DISTINCT MAHH) FROM dbo.KHO_CHITIET_TONKHO
                WHERE SO_LUONG_TON > 0 AND DATEDIFF(day, NGAY_NHAP, GETDATE()) > 30;
                
                -- 4. Số lượng sản phẩm sắp hết hàng (tồn kho tổng < 5)
                SELECT COUNT(MAHH) FROM dbo.DM_HANGHOA WHERE TONKHO > 0 AND TONKHO < 5;";

            DataSet ds = DbHelper.QueryDs(sql);

            if (ds.Tables.Count >= 4)
            {
                CultureInfo culture = new CultureInfo("vi-VN");

                // 1. Tổng giá trị tồn kho
                decimal tongGiaTriTon = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);
                lblTongGiaTriTon.Text = tongGiaTriTon.ToString("c0", culture);

                // 2. Vòng quay tồn kho
                decimal giaVonThang = Convert.ToDecimal(ds.Tables[1].Rows[0][0]);
                if (tongGiaTriTon > 0)
                {
                    // Công thức: Vòng quay = Giá vốn hàng bán kỳ / Bình quân giá trị tồn kho
                    // (Tạm tính bình quân bằng giá trị tồn cuối kỳ)
                    decimal vongQuay = giaVonThang / tongGiaTriTon;
                    lblVongQuayTonKho.Text = $"{vongQuay:N2} vòng";
                }
                else
                {
                    lblVongQuayTonKho.Text = "0 vòng";
                }

                // 3. Hàng tồn kho lâu ngày
                lblHangTonLauNgay.Text = Convert.ToInt32(ds.Tables[2].Rows[0][0]).ToString();

                // 4. Cảnh báo tồn kho thấp
                lblSapHetHang.Text = Convert.ToInt32(ds.Tables[3].Rows[0][0]).ToString();
            }
        }

        private void LoadGiaTriTonKhoChart()
        {
            string sql = @"
                SELECT TOP 5
                    nh.TENNHOM,
                    SUM(kct.SO_LUONG_TON * kct.DON_GIA_NHAP) AS GiaTriTon
                FROM 
                    dbo.KHO_CHITIET_TONKHO kct
                JOIN 
                    dbo.DM_HANGHOA hh ON kct.MAHH = hh.MAHH
                JOIN 
                    dbo.DM_NHOMHANG nh ON hh.MANHOM = nh.MANHOM
                WHERE 
                    kct.SO_LUONG_TON > 0
                GROUP BY 
                    nh.TENNHOM
                ORDER BY 
                    GiaTriTon DESC;";

            DataTable dt = DbHelper.Query(sql);

            chartGiaTriTonKho.Series.Clear();
            var series = new Series("GiaTriTon")
            {
                ChartType = SeriesChartType.Doughnut,
                XValueMember = "TENNHOM",
                YValueMembers = "GiaTriTon",
                IsValueShownAsLabel = true,
                Label = "#PERCENT{P0}",
                LegendText = "#VALX"
            };
            chartGiaTriTonKho.Legends[0].Enabled = true;
            chartGiaTriTonKho.DataSource = dt;
            chartGiaTriTonKho.Series.Add(series);
            chartGiaTriTonKho.DataBind();
        }

        private void LoadTop5HangHoaTonKho()
        {
            string sql = @"
                SELECT TOP 5
                    hh.TENHH,
                    SUM(kct.SO_LUONG_TON * kct.DON_GIA_NHAP) AS GiaTriTon
                FROM 
                    dbo.KHO_CHITIET_TONKHO kct
                JOIN 
                    dbo.DM_HANGHOA hh ON kct.MAHH = hh.MAHH
                WHERE 
                    kct.SO_LUONG_TON > 0
                GROUP BY 
                    hh.TENHH
                ORDER BY 
                    GiaTriTon DESC;";

            DataTable dt = DbHelper.Query(sql);

            lstTopSanPham.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row["TENHH"].ToString());
                decimal giaTri = Convert.ToDecimal(row["GiaTriTon"]);
                item.SubItems.Add(giaTri.ToString("N0"));
                lstTopSanPham.Items.Add(item);
            }
        }
    }
}