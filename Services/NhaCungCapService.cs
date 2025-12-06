using System;
using System.Data;
using System.Data.SqlClient;
using DoAnLapTrinhQuanLy.Data;

namespace DoAnLapTrinhQuanLy.Services
{
    public class NhaCungCapService
    {
        // DbHelper is static, no instance needed

        public DataTable GetAll()
        {
            return DbHelper.Query("SELECT * FROM DM_NHACUNGCAP");
        }

        public bool CheckTonTai(string maNCC)
        {
            string query = "SELECT COUNT(*) FROM DM_NHACUNGCAP WHERE MA_NCC = @MaNCC";
            SqlParameter[] parameters = { new SqlParameter("@MaNCC", maNCC) };
            object result = DbHelper.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        public void Insert(string maNCC, string tenNCC, string diaChi, string sdt, string email, string msThue, string ghiChu)
        {
            string query = "INSERT INTO DM_NHACUNGCAP (MA_NCC, TEN_NCC, DIACHI_NCC, SDT, EMAIL, MSTHUE, GHICHU) " +
                           "VALUES (@MaNCC, @TenNCC, @DiaChi, @SDT, @Email, @MSThue, @GhiChu)";
            SqlParameter[] parameters = {
                new SqlParameter("@MaNCC", maNCC),
                new SqlParameter("@TenNCC", tenNCC),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@SDT", sdt),
                new SqlParameter("@Email", email),
                new SqlParameter("@MSThue", msThue),
                new SqlParameter("@GhiChu", ghiChu)
            };
            DbHelper.Execute(query, parameters);
        }

        public void Update(string maNCC, string tenNCC, string diaChi, string sdt, string email, string msThue, string ghiChu)
        {
            string query = "UPDATE DM_NHACUNGCAP SET TEN_NCC = @TenNCC, DIACHI_NCC = @DiaChi, SDT = @SDT, " +
                           "EMAIL = @Email, MSTHUE = @MSThue, GHICHU = @GhiChu WHERE MA_NCC = @MaNCC";
            SqlParameter[] parameters = {
                new SqlParameter("@MaNCC", maNCC),
                new SqlParameter("@TenNCC", tenNCC),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@SDT", sdt),
                new SqlParameter("@Email", email),
                new SqlParameter("@MSThue", msThue),
                new SqlParameter("@GhiChu", ghiChu)
            };
            DbHelper.Execute(query, parameters);
        }

        public void Delete(string maNCC)
        {
            string query = "DELETE FROM DM_NHACUNGCAP WHERE MA_NCC = @MaNCC";
            SqlParameter[] parameters = { new SqlParameter("@MaNCC", maNCC) };
            DbHelper.Execute(query, parameters);
        }
    }
}
