using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public class UserData
    {
        public int ID { get; set; }
        public string TaiKhoan { get; set; }
        public string HoTen { get; set; }
        public int MaQuyen { get; set; }
        public string TenQuyen { get; set; }
    }

    public partial class FormDangNhap : Form
    {
        public UserData AuthenticatedUser { get; private set; }

        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            var taiKhoan = txtTaiKhoan.Text.Trim();
            var matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Logic đăng nhập đơn giản, so sánh mật khẩu thường
                string query = @"
                    SELECT U.ID, U.TAIKHOAN, U.HOTEN, U.MAQUYEN, Q.TENQUYEN
                    FROM NGUOIDUNG U
                    JOIN QUYENHAN Q ON U.MAQUYEN = Q.MAQUYEN
                    WHERE U.TAIKHOAN=@tk AND U.MATKHAU=@mk AND U.HOATDONG=1";

                DataTable dt = DbHelper.Query(query,
                    DbHelper.Param("@tk", taiKhoan),
                    DbHelper.Param("@mk", matKhau));

                if (dt.Rows.Count > 0)
                {
                    // Đăng nhập thành công
                    AuthenticatedUser = new UserData
                    {
                        ID = Convert.ToInt32(dt.Rows[0]["ID"]),
                        TaiKhoan = dt.Rows[0]["TAIKHOAN"].ToString(),
                        HoTen = dt.Rows[0]["HOTEN"].ToString(),
                        MaQuyen = Convert.ToInt32(dt.Rows[0]["MAQUYEN"]),
                        TenQuyen = dt.Rows[0]["TENQUYEN"].ToString()
                    };

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc truy vấn CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienMatKhau.Checked) { txtMatKhau.PasswordChar = '\0'; }
            else { txtMatKhau.PasswordChar = '●'; }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region Code để di chuyển cửa sổ không viền
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);
        private void Movable_MouseDown(object sender, MouseEventArgs e) { _dragging = true; _start_point = new Point(e.X, e.Y); }
        private void Movable_MouseUp(object sender, MouseEventArgs e) { _dragging = false; }
        private void Movable_MouseMove(object sender, MouseEventArgs e) { if (_dragging) { Point p = PointToScreen(e.Location); Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y); } }
        #endregion
    }
}