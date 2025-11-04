using DoAnLapTrinhQuanLy.GuiLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnLapTrinhQuanLy.Data
{
    // Đây là một lớp static, hoạt động như một bộ nhớ tạm toàn cục cho ứng dụng
    public static class Session
    {
        // Thuộc tính static để lưu thông tin người dùng đã đăng nhập
        // Có thể truy cập từ bất kỳ đâu bằng cách gọi: Session.LoggedInUser
        public static UserData LoggedInUser { get; set; }
    }
}
