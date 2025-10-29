using DoAnLapTrinhQuanLy.Data; // Sử dụng DbHelper
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormDoiMatKhau : Form
    {
        // ===================================================================================
        // KHAI BÁO BIẾN & CONSTRUCTOR
        // ===================================================================================

        private int _currentUserId;
        private string _currentUserName;

        /// <summary>
        /// Constructor yêu cầu ID người dùng đang đăng nhập để biết tài khoản nào cần đổi mật khẩu.
        /// </summary>
        /// <param name="userId">ID (khóa chính) của người dùng trong bảng NGUOIDUNG.</param>
        /// <param name="userName">Tên hiển thị của người dùng (tùy chọn).</param>
        public FormDoiMatKhau(int userId, string userName = "Người dùng")
        {
            InitializeComponent();
            _currentUserId = userId;
            _currentUserName = userName;
            this.Text = $"Đổi mật khẩu - {_currentUserName}";
        }

        private void FormDoiMatKhau_Load(object sender, EventArgs e)
        {
            txtMatKhauCu.Focus();
        }

        // ===================================================================================
        // LOGIC ĐỔI MẬT KHẨU
        // ===================================================================================

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string matKhauCu = txtMatKhauCu.Text;
            string matKhauMoi = txtMatKhauMoi.Text;
            string xacNhan = txtXacNhan.Text;

            // 1. Kiểm tra ràng buộc đầu vào
            if (string.IsNullOrEmpty(matKhauCu) || string.IsNullOrEmpty(matKhauMoi) || string.IsNullOrEmpty(xacNhan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các trường.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (matKhauMoi != xacNhan)
            {
                MessageBox.Show("Mật khẩu mới và Xác nhận mật khẩu không khớp.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (matKhauMoi.Length < 3) // Ví dụ: yêu cầu độ dài tối thiểu
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 3 ký tự.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Xác thực mật khẩu cũ
                // Lấy mật khẩu (đã băm hoặc trần) hiện tại từ CSDL
                string sqlCheck = "SELECT MATKHAU FROM dbo.NGUOIDUNG WHERE ID = @ID";
                object storedPasswordObj = DbHelper.Scalar(sqlCheck, DbHelper.Param("@ID", _currentUserId));

                if (storedPasswordObj == null)
                {
                    MessageBox.Show("Tài khoản người dùng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string storedPassword = Convert.ToString(storedPasswordObj);
                bool isPasswordValid = false;

                // *******************************************************************
                // ** LOGIC QUAN TRỌNG: XÁC THỰC MẬT KHẨU **
                // *******************************************************************

                // Nếu đang dùng HASH (nên là vậy):
                // if (VerifyHash(matKhauCu, storedPassword)) isPasswordValid = true; 

                // Tạm thời, dùng mật khẩu trần để khớp với dữ liệu mẫu:
                if (matKhauCu == storedPassword)
                {
                    isPasswordValid = true;
                }
                // *******************************************************************

                if (!isPasswordValid)
                {
                    MessageBox.Show("Mật khẩu cũ không chính xác.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhauCu.Focus();
                    return;
                }

                // 3. Chuẩn bị cập nhật mật khẩu mới
                // *******************************************************************
                // ** LOGIC QUAN TRỌNG: BĂM MẬT KHẨU MỚI **
                // *******************************************************************
                // Nếu đang dùng HASH:
                // string hashedPassword = HashPassword(matKhauMoi);

                // Tạm thời, dùng mật khẩu trần để khớp với dữ liệu mẫu:
                string hashedPassword = matKhauMoi;
                // *******************************************************************


                // 4. Thực hiện cập nhật vào CSDL
                string sqlUpdate = "UPDATE dbo.NGUOIDUNG SET MATKHAU = @MATKHAUMOI WHERE ID = @ID";

                int rowsAffected = DbHelper.Execute(sqlUpdate,
                    DbHelper.Param("@MATKHAUMOI", hashedPassword),
                    DbHelper.Param("@ID", _currentUserId)
                );

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Đổi mật khẩu thành công! Vui lòng đăng nhập lại vào lần sau.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật mật khẩu. Vui lòng thử lại.", "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi đổi mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}