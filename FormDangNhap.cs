using System;
using System.Data;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormDangNhap : Form
    {
        // Property để Program.cs có thể lấy thông tin user sau khi đăng nhập thành công
        public UserData AuthenticatedUser { get; private set; }

        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Nút thoát sẽ kết thúc toàn bộ ứng dụng
            Application.Exit();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text;
            string matKhau = txtMatKhau.Text;

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = @"
                    SELECT nd.TAIKHOAN, nd.MATKHAU, nd.HOTEN, qh.TENQUYEN 
                    FROM NGUOIDUNG nd 
                    JOIN QUYENHAN qh ON nd.MAQUYEN = qh.MAQUYEN 
                    WHERE nd.TAIKHOAN = @TaiKhoan AND nd.MATKHAU = @MatKhau AND nd.HOATDONG = 1";

                DataTable dt = DbHelper.Query(query,
                                    DbHelper.Param("@TaiKhoan", taiKhoan),
                                    DbHelper.Param("@MatKhau", matKhau));

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    AuthenticatedUser = new UserData
                    {
                        TaiKhoan = row["TAIKHOAN"].ToString(),
                        MatKhau = row["MATKHAU"].ToString(),
                        HoTen = row["HOTEN"].ToString(),
                        TenQuyen = row["TENQUYEN"].ToString()
                    };

                    // === LƯU VÀO SESSION ===
                    Session.LoggedInUser = this.AuthenticatedUser;

                    this.DialogResult = DialogResult.OK; // Báo cho Program.cs là đăng nhập thành công
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng, hoặc tài khoản đã bị khóa.", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu checkbox được check, bỏ ký tự che mật khẩu. Nếu không, đặt lại.
            if (chkShowPassword.Checked)
            {
                txtMatKhau.PasswordChar = '\0'; // Ký tự null để hiển thị text
            }
            else
            {
                txtMatKhau.PasswordChar = '●';
            }
        }
    }
}