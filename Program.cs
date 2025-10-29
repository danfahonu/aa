using System;
using System.Windows.Forms;
// THÊM DÒNG NÀY ĐỂ TRỎ ĐÚNG VÀO CÁC FORM TRONG THƯ MỤC GuiLayer
using DoAnLapTrinhQuanLy.GuiLayer;

namespace DoAnLapTrinhQuanLy
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var loginForm = new FormDangNhap())
            {
                // Chỉ khi đăng nhập thành công (DialogResult.OK) thì mới chạy form chính
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Chắc chắn rằng chúng ta đang gọi đúng FrmMain trong GuiLayer
                    var mainForm = new FrmMain();
                    // Truyền thông tin người dùng đã đăng nhập vào form chính
                    mainForm.SetLoggedInUser(loginForm.AuthenticatedUser);
                    Application.Run(mainForm);
                }
            }
        }
    }
}