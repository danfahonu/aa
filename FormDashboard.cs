using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DoAnLapTrinhQuanLy.Data;

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
            LoadKpiData();
            LoadCategoryChart();
            LoadTopProductsGrids();
        }

        private void LoadKpiData()
        {
            try
            {
                // 1. Tổng giá trị tồn kho (tính chính xác từ các lô FIFO)
                string valueQuery = "SELECT ISNULL(SUM(SO_LUONG_TON * DON_GIA_NHAP), 0) FROM KHO_CHITIET_TONKHO";
                object totalValue = DbHelper.Scalar(valueQuery);
                lblGiaTriTon.Text = Convert.ToDecimal(totalValue).ToString("N0");

                // 2. Tổng số mặt hàng (SKU) đang quản lý
                string skuQuery = "SELECT COUNT(MAHH) FROM DM_HANGHOA WHERE ACTIVE = 1";
                object totalSku = DbHelper.Scalar(skuQuery);
                lblSoMatHang.Text = Convert.ToInt32(totalSku).ToString();

                // 3. Tổng số lượng đơn vị sản phẩm trong kho
                string unitQuery = "SELECT ISNULL(SUM(TONKHO), 0) FROM DM_HANGHOA WHERE ACTIVE = 1";
                object totalUnits = DbHelper.Scalar(unitQuery);
                lblTongTon.Text = Convert.ToInt32(totalUnits).ToString("N0");

                // 4. Số mặt hàng dưới mức tồn tối thiểu (cần có cột TONKHO_TOITHIEU trong DM_HANGHOA)
                // Nếu chưa có, bà có thể bỏ qua phần này hoặc mặc định nó bằng 0
                try
                {
                    string lowStockQuery = "SELECT COUNT(MAHH) FROM DM_HANGHOA WHERE ACTIVE = 1 AND TONKHO < TONKHO_TOITHIEU";
                    object lowStockCount = DbHelper.Scalar(lowStockQuery);
                    lblHangSapHet.Text = Convert.ToInt32(lowStockCount).ToString();
                }
                catch
                {
                    // Nếu bảng DM_HANGHOA chưa có cột TONKHO_TOITHIEU thì sẽ báo lỗi, ta bắt lỗi và cho nó bằng 0
                    lblHangSapHet.Text = "N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu KPI Kho: " + ex.Message);
            }
        }

        private void LoadCategoryChart()
        {
            try
            {
                string query = @"
                    SELECT 
                        nh.TENNHOM,
                        ISNULL(SUM(t.SO_LUONG_TON * t.DON_GIA_NHAP), 0) as TongGiaTri
                    FROM KHO_CHITIET_TONKHO t
                    JOIN DM_HANGHOA h ON t.MAHH = h.MAHH
                    JOIN DM_NHOMHANG nh ON h.MANHOM = nh.MANHOM
                    WHERE t.SO_LUONG_TON > 0
                    GROUP BY nh.TENNHOM
                    HAVING ISNULL(SUM(t.SO_LUONG_TON * t.DON_GIA_NHAP), 0) > 0";

                DataTable dt = DbHelper.Query(query);

                chartNhomHang.DataSource = dt;
                chartNhomHang.Series["Series1"].XValueMember = "TENNHOM";
                chartNhomHang.Series["Series1"].YValueMembers = "TongGiaTri";
                chartNhomHang.Series["Series1"].Label = "#PERCENT{P0}";
                chartNhomHang.Series["Series1"].LegendText = "#VALX";
                chartNhomHang.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải biểu đồ Nhóm hàng: " + ex.Message);
            }
        }

        private void LoadTopProductsGrids()
        {
            try
            {
                // Top 5 theo số lượng
                string topQtyQuery = @"
                    SELECT TOP 5 
                        h.TENHH, 
                        SUM(t.SO_LUONG_TON) as TongTon
                    FROM KHO_CHITIET_TONKHO t
                    JOIN DM_HANGHOA h ON t.MAHH = h.MAHH
                    WHERE t.SO_LUONG_TON > 0
                    GROUP BY h.TENHH
                    ORDER BY TongTon DESC";
                dgvTopSoLuong.DataSource = DbHelper.Query(topQtyQuery);

                // Top 5 theo giá trị
                string topValueQuery = @"
                    SELECT TOP 5 
                        h.TENHH, 
                        SUM(t.SO_LUONG_TON * t.DON_GIA_NHAP) as GiaTriTon
                    FROM KHO_CHITIET_TONKHO t
                    JOIN DM_HANGHOA h ON t.MAHH = h.MAHH
                    WHERE t.SO_LUONG_TON > 0
                    GROUP BY h.TENHH
                    ORDER BY GiaTriTon DESC";
                dgvTopGiaTri.DataSource = DbHelper.Query(topValueQuery);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải top sản phẩm tồn kho: " + ex.Message);
            }
        }
    }
}