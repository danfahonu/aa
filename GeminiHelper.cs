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
                    - Luôn sử dụng N'...' cho các chuỗi ký tự tiếng Việt (Unicode).

                    Dưới đây là cấu trúc CSDL SALEGEARVN:
                    -- Bảng nhân sự và lương
                    - NHANVIEN(MANV, HOTEN, CHUCVU, DIACHI, SDT, EMAIL, ANH, HOATDONG)
                    - HESOLUONG(CHUCVU, HESOLUONG)
                    - BANGCHAMCONG(ID, MANV, THANG, NAM, NGAYCONG, GHICHU)
                    - TAMUNG(ID, MANV, NGAY, SOTIEN, GHICHU)
                    - PHIEU_YEUCAU_NHAPKHO(ID, NGAY_YEUCAU, MANV_YEUCAU, LYDO, TRANGTHAI, MANV_DUYET, NGAY_DUYET)
                    
                    -- Bảng danh mục
                    - DANHMUCKHACHHANG(MAKH, TENKH, DIACHI, SDT, EMAIL, GHICHU, NGAYTAO)
                    - DM_NHACUNGCAP(MA_NCC, TEN_NCC, DIACHI_NCC, SDT, EMAIL, MSTHUE, GHICHU)
                    - DM_NHOMHANG(MANHOM, TENNHOM, GHICHU)
                    - DM_HANGHOA(MAHH, TENHH, MANHOM, DVT, GIAVON, GIABAN, TONKHO, ANH, ACTIVE)
                    - DM_NGANHANG(MANH, TENNH, CHINHANH)
                    - DM_TAIKHOAN_NGANHANG(ID, SO_TK, NGANHANG, CHU_TK, LOAI_TK, MA_DOITUONG)

                    -- Bảng nghiệp vụ kho
                    - PHIEU(SOPHIEU, NGAYLAP, LOAI, MAKH, MA_NCC, GHICHU, TRANGTHAI, SO_CT, MACT, ID_YEUCAU) 
                      (LOAI='N': Nhập, LOAI='X': Xuất)
                    - PHIEU_CT(ID, SOPHIEU, MAHH, SL, DONGIA, THANHTIEN, GIAVON)
                    - KHO_CHITIET_TONKHO(ID, ID_PHIEUNHAP_CT, MAHH, NGAY_NHAP, SO_LUONG_NHAP, DON_GIA_NHAP, SO_LUONG_TON)
                    - View: vw_TonKhoHienTai(MAHH, TON_HIEN_TAI)
                    - View: vw_BanHangNgay(NGAYLAP, DOANH_THU, GIA_VON, LAI_GOP)

                    -- Bảng kế toán, tài chính
                    - PHIEUTHUCHI(SOPTC, NGAYLAP, LOAI, MAKH, MA_NCC, SOTIEN, LYDO, MANV, SOTK_NO, SOTK_CO) 
                      (LOAI='T': Thu, LOAI='C': Chi)
                    - BUTTOAN_KETOAN(ID, NGAY_HT, SO_CT, MA_CT, DIEN_GIAI, TK_NO, TK_CO, SOTIEN)
                    - DM_TAIKHOANKETOAN(SOTK, TENTK, CAP, TK_ME)
                    - SODUDAU_KETOAN(NAM, SOTK, MATK2, DU_NO, DU_CO)

                    Ví dụ:
                    Câu hỏi: có bao nhiêu khách hàng?
                    SQL: SELECT COUNT(*) FROM DANHMUCKHACHHANG

                    Câu hỏi: liệt kê các nhân viên có chức vụ kế toán
                    SQL: SELECT * FROM NHANVIEN WHERE CHUCVU = N'Kế toán'

                    Câu hỏi: tổng tiền nhập kho trong tháng 8 năm 2025 là bao nhiêu?
                    SQL: SELECT SUM(ct.THANHTIEN) FROM PHIEU p JOIN PHIEU_CT ct ON p.SOPHIEU = ct.SOPHIEU WHERE p.LOAI = 'N' AND MONTH(p.NGAYLAP) = 8 AND YEAR(p.NGAYLAP) = 2025
                    
                    Câu hỏi: Doanh thu ngày hôm nay?
                    SQL: SELECT * FROM vw_BanHangNgay WHERE NGAYLAP = CAST(GETDATE() AS DATE)
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