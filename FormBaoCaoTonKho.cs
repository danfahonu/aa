using System;
using System.Data;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;

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
            // Khi form load, chạy báo cáo ngay với bộ lọc mặc định
            btnXemBaoCao_Click(sender, e);
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                // Câu query này sẽ không tự tính GIATRI_TON nữa để tránh lỗi
                string query = @"
                    SELECT 
                        h.MAHH,
                        h.TENHH,
                        nh.TENNHOM,
                        h.DVT,
                        ISNULL(v.TON_HIEN_TAI, 0) AS TONKHO,
                        h.GIAVON
                    FROM 
                        DM_HANGHOA h
                    LEFT JOIN 
                        DM_NHOMHANG nh ON h.MANHOM = nh.MANHOM
                    LEFT JOIN
                        vw_TonKhoHienTai v ON h.MAHH = v.MAHH
                    ORDER BY 
                        nh.TENNHOM, h.TENHH";

                DataTable dt = DbHelper.Query(query);

                // === PHẦN SỬA LỖI QUAN TRỌNG NHẤT ===
                // Thêm một cột mới 'GIATRI_TON' vào DataTable bằng code C#
                dt.Columns.Add("GIATRI_TON", typeof(decimal));

                decimal tongGiaTri = 0;
                foreach (DataRow row in dt.Rows)
                {
                    // Kiểm tra DBNull trước khi chuyển đổi kiểu dữ liệu
                    decimal tonKho = (row["TONKHO"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["TONKHO"]);
                    decimal giaVon = (row["GIAVON"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["GIAVON"]);

                    decimal giaTriTon = tonKho * giaVon;

                    // Gán giá trị vừa tính vào cột mới
                    row["GIATRI_TON"] = giaTriTon;

                    // Cộng dồn vào tổng giá trị
                    tongGiaTri += giaTriTon;
                }

                dgvBaoCao.DataSource = dt;
                lblTongGiaTri.Text = tongGiaTri.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}