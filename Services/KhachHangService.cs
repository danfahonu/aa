using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoAnLapTrinhQuanLy.Data;

namespace DoAnLapTrinhQuanLy.Services
{
    public class KhachHangService
    {
        public DataTable GetAll()
        {
            return DbHelper.Query("SELECT MAKH, TENKH, DIACHI, SDT, EMAIL, GHICHU, NGAYTAO FROM DANHMUCKHACHHANG");
        }

        public bool CheckTonTai(string maKH)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM DANHMUCKHACHHANG WHERE MAKH = @MaKH";
                object result = DbHelper.Scalar(query, DbHelper.Param("@MaKH", maKH));
                return Convert.ToInt32(result) > 0;
            }
            catch
            {
                return false;
            }
        }

        public void Insert(string maKH, string tenKH, string diaChi, string sdt, string email, string ghiChu)
        {
            string query = @"INSERT INTO DANHMUCKHACHHANG (MAKH, TENKH, DIACHI, SDT, EMAIL, GHICHU, NGAYTAO) 
                             VALUES (@MaKH, @TenKH, @DiaChi, @SDT, @Email, @GhiChu, @NgayTao)";

            DbHelper.Execute(query,
                DbHelper.Param("@MaKH", maKH),
                DbHelper.Param("@TenKH", tenKH),
                DbHelper.Param("@DiaChi", diaChi),
                DbHelper.Param("@SDT", sdt),
                DbHelper.Param("@Email", email),
                DbHelper.Param("@GhiChu", ghiChu),
                DbHelper.Param("@NgayTao", DateTime.Now)
            );
        }

        public void Update(string maKH, string tenKH, string diaChi, string sdt, string email, string ghiChu)
        {
            string query = @"UPDATE DANHMUCKHACHHANG 
                             SET TENKH = @TenKH, DIACHI = @DiaChi, SDT = @SDT, EMAIL = @Email, GHICHU = @GhiChu 
                             WHERE MAKH = @MaKH";

            DbHelper.Execute(query,
                DbHelper.Param("@TenKH", tenKH),
                DbHelper.Param("@DiaChi", diaChi),
                DbHelper.Param("@SDT", sdt),
                DbHelper.Param("@Email", email),
                DbHelper.Param("@GhiChu", ghiChu),
                DbHelper.Param("@MaKH", maKH)
            );
        }

        public void Delete(string maKH)
        {
            string query = "DELETE FROM DANHMUCKHACHHANG WHERE MAKH = @MaKH";
            DbHelper.Execute(query, DbHelper.Param("@MaKH", maKH));
        }
    }
}
