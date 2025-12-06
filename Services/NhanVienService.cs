using System;
using System.Data;
using System.Data.SqlClient;
using DoAnLapTrinhQuanLy.Data;

namespace DoAnLapTrinhQuanLy.Services
{
    public class NhanVienService
    {
        public DataTable GetAll()
        {
            return DbHelper.Query("SELECT * FROM NHANVIEN");
        }

        public bool CheckTonTai(string maNV)
        {
            string query = "SELECT COUNT(*) FROM NHANVIEN WHERE MANV = @MaNV";
            SqlParameter[] parameters = { new SqlParameter("@MaNV", maNV) };
            object result = DbHelper.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        public void Insert(string maNV, string hoTen, string chucVu, string diaChi, string sdt, string email, string anh, bool hoatDong)
        {
            string query = "INSERT INTO NHANVIEN (MANV, HOTEN, CHUCVU, DIACHI, SDT, EMAIL, ANH, HOATDONG) " +
                           "VALUES (@MaNV, @HoTen, @ChucVu, @DiaChi, @SDT, @Email, @Anh, @HoatDong)";
            SqlParameter[] parameters = {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@ChucVu", chucVu),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@SDT", sdt),
                new SqlParameter("@Email", email),
                new SqlParameter("@Anh", anh ?? (object)DBNull.Value),
                new SqlParameter("@HoatDong", hoatDong)
            };
            DbHelper.Execute(query, parameters);
        }

        public void Update(string maNV, string hoTen, string chucVu, string diaChi, string sdt, string email, string anh, bool hoatDong)
        {
            string query = "UPDATE NHANVIEN SET HOTEN = @HoTen, CHUCVU = @ChucVu, DIACHI = @DiaChi, SDT = @SDT, " +
                           "EMAIL = @Email, ANH = @Anh, HOATDONG = @HoatDong WHERE MANV = @MaNV";
            SqlParameter[] parameters = {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@ChucVu", chucVu),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@SDT", sdt),
                new SqlParameter("@Email", email),
                new SqlParameter("@Anh", anh ?? (object)DBNull.Value),
                new SqlParameter("@HoatDong", hoatDong)
            };
            DbHelper.Execute(query, parameters);
        }

        public void Delete(string maNV)
        {
            string query = "DELETE FROM NHANVIEN WHERE MANV = @MaNV";
            SqlParameter[] parameters = { new SqlParameter("@MaNV", maNV) };
            DbHelper.Execute(query, parameters);
        }
    }
}
