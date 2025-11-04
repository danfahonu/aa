using DoAnLapTrinhQuanLy.Data; // Đảm bảo using này đúng
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormDoiMatKhau : Form
    {
        public FormDoiMatKhau()
        {
            InitializeComponent();
        }

        private void FormDoiMatKhau_Load(object sender, EventArgs e)
        {
            // Tự động điền tên đăng nhập của người dùng hiện tại
            if (Session.LoggedInUser != null)
            {
                txtTenDangNhap.Text = Session.LoggedInUser.TaiKhoan;
            }
            else
            {
                // Nếu chưa có ai đăng nhập, không cho phép đổi mật khẩu
                MessageBox.Show("Vui lòng đăng nhập để sử dụng chức năng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void ClearInputs()
        {
            txtMatKhauCu.Text = "";
            txtMatKhauMoi.Text = "";
            txtXacNhanMK.Text = "";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string matKhauCu = txtMatKhauCu.Text;
            string matKhauMoi = txtMatKhauMoi.Text;
            string xacNhanMK = txtXacNhanMK.Text;

            // 1. Kiểm tra các ô nhập liệu có trống không
            if (string.IsNullOrWhiteSpace(matKhauCu) || string.IsNullOrWhiteSpace(matKhauMoi) || string.IsNullOrWhiteSpace(xacNhanMK))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra mật khẩu cũ có đúng không
            if (matKhauCu != Session.LoggedInUser.MatKhau)
            {
                MessageBox.Show("Mật khẩu cũ không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhauCu.Focus();
                return;
            }

            // 3. Kiểm tra mật khẩu mới và xác nhận có khớp không
            if (matKhauMoi != xacNhanMK)
            {
                MessageBox.Show("Mật khẩu mới và mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhauMoi.Focus();
                return;
            }

            try
            {
                // 4. Cập nhật mật khẩu mới vào CSDL
                string query = "UPDATE NGUOIDUNG SET MATKHAU = @MatKhauMoi WHERE TAIKHOAN = @TaiKhoan";
                DbHelper.Execute(query,
                    DbHelper.Param("@MatKhauMoi", matKhauMoi),
                    DbHelper.Param("@TaiKhoan", Session.LoggedInUser.TaiKhoan)
                );

                // Cập nhật lại mật khẩu trong Session để các lần kiểm tra sau được đúng
                Session.LoggedInUser.MatKhau = matKhauMoi;

                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}