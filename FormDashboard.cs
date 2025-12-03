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
            try
            {
                ThemeManager.Apply(this);

                // Update Labels for new KPIs
                label1.Text = "Doanh Số Hôm Nay";
                label3.Text = "Tiền Mặt Hiện Có";
                label5.Text = "Giá Trị Tồn Kho";
                label7.Text = "Hàng Sắp Hết (<10)";

                LoadKpiData();
                LoadCategoryChart();
                LoadTopProductsGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải Dashboard: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadKpiData()
        {
            try
            {
                // 1. Doanh số hôm nay
                // Logic: Sum THANHTIEN from PHIEU_CT where PHIEU is Export (X) and Date is Today
                string salesQuery = @"
                    SELECT ISNULL(SUM(ct.THANHTIEN), 0)
                    FROM PHIEU p
                    JOIN PHIEU_CT ct ON p.SOPHIEU = ct.SOPHIEU
                    WHERE p.LOAI = 'X' AND p.TRANGTHAI = 1 AND p.NGAYLAP = CAST(GETDATE() AS DATE)";
                object salesObj = DbHelper.Scalar(salesQuery);
                lblGiaTriTon.Text = Convert.ToDecimal(salesObj).ToString("N0"); // Repurposed label

                // 2. Tiền mặt hiện có
                // Logic: Sum Thu - Sum Chi from PHIEUTHUCHI
                string cashQuery = "SELECT ISNULL(SUM(CASE WHEN LOAI = 'T' THEN SOTIEN ELSE -SOTIEN END), 0) FROM PHIEUTHUCHI";
                object cashObj = DbHelper.Scalar(cashQuery);
                lblSoMatHang.Text = Convert.ToDecimal(cashObj).ToString("N0"); // Repurposed label

                // 3. Giá trị tồn kho (Keep existing logic but optimized)
                // Logic: Sum TONKHO * GIAVON from DM_HANGHOA (assuming GIAVON is updated) 
                // OR Sum SO_LUONG_TON * DON_GIA_NHAP from KHO_CHITIET_TONKHO
                string stockValueQuery = "SELECT ISNULL(SUM(SO_LUONG_TON * DON_GIA_NHAP), 0) FROM KHO_CHITIET_TONKHO";
                object stockValueObj = DbHelper.Scalar(stockValueQuery);
                lblTongTon.Text = Convert.ToDecimal(stockValueObj).ToString("N0");

                // 4. Hàng sắp hết
                // Logic: Count items with TONKHO < 10
                string lowStockQuery = "SELECT COUNT(MAHH) FROM DM_HANGHOA WHERE ACTIVE = 1 AND TONKHO < 10";
                object lowStockObj = DbHelper.Scalar(lowStockQuery);
                lblHangSapHet.Text = Convert.ToInt32(lowStockObj).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải KPI: " + ex.Message);
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

                // Theme Chart
                chartNhomHang.BackColor = ThemeManager.SecondaryColor;
                chartNhomHang.ChartAreas[0].BackColor = ThemeManager.SecondaryColor;
                chartNhomHang.Legends[0].BackColor = ThemeManager.SecondaryColor;
                chartNhomHang.Legends[0].ForeColor = ThemeManager.TextColor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải biểu đồ: " + ex.Message);
            }
        }

        private void LoadTopProductsGrids()
        {
            try
            {
                // Top 5 theo số lượng
                string topQtyQuery = @"
                    SELECT TOP 5 
                        h.TENHH AS [Tên Hàng], 
                        SUM(t.SO_LUONG_TON) as [Tồn Kho]
                    FROM KHO_CHITIET_TONKHO t
                    JOIN DM_HANGHOA h ON t.MAHH = h.MAHH
                    WHERE t.SO_LUONG_TON > 0
                    GROUP BY h.TENHH
                    ORDER BY [Tồn Kho] DESC";
                DataTable dtQty = DbHelper.Query(topQtyQuery);
                dgvTopSoLuong.DataSource = dtQty;
                FormatGrid(dgvTopSoLuong);

                // Top 5 theo giá trị
                string topValueQuery = @"
                    SELECT TOP 5 
                        h.TENHH AS [Tên Hàng], 
                        SUM(t.SO_LUONG_TON * t.DON_GIA_NHAP) as [Giá Trị]
                    FROM KHO_CHITIET_TONKHO t
                    JOIN DM_HANGHOA h ON t.MAHH = h.MAHH
                    WHERE t.SO_LUONG_TON > 0
                    GROUP BY h.TENHH
                    ORDER BY [Giá Trị] DESC";
                DataTable dtVal = DbHelper.Query(topValueQuery);
                dgvTopGiaTri.DataSource = dtVal;
                FormatGrid(dgvTopGiaTri);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải Top sản phẩm: " + ex.Message);
            }
        }

        private void FormatGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.PrimaryColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgv.Columns.Count > 1)
            {
                dgv.Columns[1].DefaultCellStyle.Format = "N0";
                dgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
    }
}