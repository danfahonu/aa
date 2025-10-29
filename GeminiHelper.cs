using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
// SỬ DỤNG ĐÚNG CÁC NAMESPACE CỦA THƯ VIỆN ĐÃ CÀI
using Mscc.GenerativeAI;
using Mscc.GenerativeAI.Google;

// Đảm bảo namespace này đúng với dự án của bạn
namespace DoAnLapTrinhQuanLy.Data
{
    public static class GeminiHelper
    {
        // Đọc API Key an toàn từ App.config
        private static readonly string ApiKey = ConfigurationManager.AppSettings["GeminiApiKey"];
        private static GenerativeModel _model;

        private static GenerativeModel GetModel()
        {
            if (_model == null)
            {
                if (string.IsNullOrEmpty(ApiKey))
                {
                    throw new InvalidOperationException("API key 'GeminiApiKey' chưa được cấu hình trong file App.config.");
                }

                // ===================================================================
                // ===== PROMPT ĐÃ ĐƯỢC CẬP NHẬT ĐẦY ĐỦ HƠN RẤT NHIỀU =====
                // ===================================================================
                string systemPrompt = @"
                    Bạn là một trợ lý AI chuyên nghiệp, chuyển đổi câu hỏi tiếng Việt thành câu lệnh SQL Server chính xác.
                    - Chỉ được trả lời bằng câu lệnh SQL, không giải thích gì thêm.
                    - Chỉ được tạo câu lệnh SELECT. Tuyệt đối không tạo câu lệnh UPDATE, DELETE, INSERT, DROP.
                    - Nếu không thể trả lời, hãy trả về chữ 'KHONG_THE_TRA_LOI'.

                    Dưới đây là cấu trúc CSDL SALEGEARVN:
                    -- Bảng nhân sự và lương
                    - NHANVIEN(MANV, HOTEN, CHUCVU, DIACHI, SDT, EMAIL)
                    - HESOLUONG(CHUCVU, HESOLUONG)
                    - BANGCHAMCONG(MANV, THANG, NAM, NGAYCONG)
                    - TAMUNG(MANV, NGAY, SOTIEN)
                    -- Bảng danh mục
                    - DANHMUCKHACHHANG(MAKH, TENKH, DIACHI, SDT)
                    - DM_NHACUNGCAP(MA_NCC, TEN_NCC, DIACHI_NCC, SDT)
                    - DM_NHOMHANG(MANHOM, TENNHOM)
                    - DM_HANGHOA(MAHH, TENHH, MANHOM, DVT, GIAVON, GIABAN, TONKHO)
                    -- Bảng nghiệp vụ kho
                    - PHIEU(SOPHIEU, NGAYLAP, LOAI, MAKH, MA_NCC) với LOAI='N' là phiếu nhập, LOAI='X' là phiếu xuất.
                    - PHIEU_CT(SOPHIEU, MAHH, SL, DONGIA, THANHTIEN)
                    -- Bảng kế toán, tài chính
                    - PHIEUTHUCHI(SOPTC, NGAYLAP, LOAI, MAKH, MA_NCC, SOTIEN, LYDO, MANV) với LOAI='T' là phiếu thu, LOAI='C' là phiếu chi.
                    - BUTTOAN_KETOAN(NGAY_HT, DIEN_GIAI, TK_NO, TK_CO, SOTIEN)
                    - DM_TAIKHOANKETOAN(SOTK, TENTK)

                    Ví dụ:
                    Câu hỏi: có bao nhiêu khách hàng?
                    SQL: SELECT COUNT(*) FROM DANHMUCKHACHHANG

                    Câu hỏi: liệt kê các nhân viên có chức vụ kế toán
                    SQL: SELECT * FROM NHANVIEN WHERE CHUCVU = N'Kế toán'

                    Câu hỏi: tổng tiền nhập kho trong tháng 8 năm 2025 là bao nhiêu?
                    SQL: SELECT SUM(ct.THANHTIEN) FROM PHIEU p JOIN PHIEU_CT ct ON p.SOPHIEU = ct.SOPHIEU WHERE p.LOAI = 'N' AND MONTH(p.NGAYLAP) = 8 AND YEAR(p.NGAYLAP) = 2025
                ";

                var googleAI = new GoogleAI(apiKey: ApiKey);

                var systemInstruction = new Content
                {
                    Parts = new List<IPart> { (IPart)new Part { Text = systemPrompt } },
                    Role = "model"
                };

                _model = googleAI.GenerativeModel(
                    model: "gemini-1.5-flash-latest",
                    systemInstruction: systemInstruction
                );
            }
            return _model;
        }

        public static async Task<string> ChuyenCauHoiThanhSQL(string cauHoi)
        {
            try
            {
                var model = GetModel();
                var response = await model.GenerateContent(cauHoi);

                var result = response?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

                if (string.IsNullOrWhiteSpace(result))
                {
                    return "KHONG_THE_TRA_LOI";
                }

                return result.Replace("```sql", "").Replace("```", "").Trim();
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi để dễ dàng gỡ rối (debug)
                // Bạn có thể xem lỗi này trong cửa sổ Output của Visual Studio khi chạy ở chế độ Debug
                System.Diagnostics.Debug.WriteLine($"Lỗi khi gọi Gemini API: {ex.Message}");
                return "KHONG_THE_TRA_LOI";
            }
        }
    }
}