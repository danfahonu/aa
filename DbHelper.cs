using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace DoAnLapTrinhQuanLy.Data
{
    /// <summary>
    /// ADO.NET helper cho SQL Server (Framework 4.8)
    /// - Hỗ trợ thao tác CSDL an toàn, thread-safe.
    /// - Sử dụng tham số (parameterized queries) để ngăn chặn SQL Injection.
    /// - Cung cấp các phương thức tiện ích cho truy vấn, thực thi và giao dịch.
    /// </summary>
    public static class DbHelper
    {
        /// <summary>
        /// Timeout mặc định cho command (giây). Có thể chỉnh sửa.
        /// </summary>
        public static int DefaultCommandTimeoutSeconds { get; set; } = 30;

        // Đọc và cache connection string một cách an toàn luồng (thread-safe)
        private static Lazy<string> _lazyCs = new Lazy<string>(() =>
        {
            var c = ConfigurationManager.ConnectionStrings["Db"];
            if (c == null || string.IsNullOrWhiteSpace(c.ConnectionString))
            {
                throw new InvalidOperationException("Missing connection string 'Db' in App.config. Please configure it using the connection form.");
            }
            return c.ConnectionString;
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Trả về chuỗi kết nối CSDL từ App.config.
        /// </summary>
        public static string ConnectionString => _lazyCs.Value;

        /// <summary>
        /// Mở một kết nối mới tới CSDL.
        /// </summary>
        public static SqlConnection OpenConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// Tải lại chuỗi kết nối từ App.config.
        /// Thường được gọi sau khi cấu hình kết nối CSDL được thay đổi.
        /// </summary>
        public static void ReloadConnectionString()
        {
            ConfigurationManager.RefreshSection("connectionStrings");
            _lazyCs = new Lazy<string>(() =>
            {
                var c = ConfigurationManager.ConnectionStrings["Db"];
                if (c == null || string.IsNullOrWhiteSpace(c.ConnectionString))
                {
                    throw new InvalidOperationException("Missing connection string 'Db' in App.config. Please configure it.");
                }
                return c.ConnectionString;
            }, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public static DataTable Proc(string procName, params SqlParameter[] parameters)
            => Query(procName, CommandType.StoredProcedure, parameters);

        public static object Scalar(string sql, params SqlParameter[] parameters)
            => Scalar(sql, CommandType.Text, parameters);

        public static int Execute(string sql, params SqlParameter[] parameters)
            => Execute(sql, CommandType.Text, parameters);

        public static DataTable Query(string sql, params SqlParameter[] parameters)
            => Query(sql, CommandType.Text, parameters);

        public static SqlParameter Param(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        /// <summary>
        /// Thực hiện một chuỗi các thao tác trong một giao dịch (transaction).
        /// </summary>
        public static DataSet QueryDs(string sql, params SqlParameter[] parameters)
        {
            using (var conn = OpenConnection())
            using (var cmd = CreateCommand(conn, sql, CommandType.Text, parameters))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
        }

        /// <summary>
        /// Thực hiện transaction an toàn.
        /// </summary>
        public static int ExecuteTran(Func<SqlConnection, SqlTransaction, int> work)
            => ExecuteTran(IsolationLevel.ReadCommitted, work);

        // ========== CÁC OVERLOAD (NÂNG CAO) ==========

        public static DataTable Query(string sql, CommandType type, params SqlParameter[] parameters)
            => WithConnection(conn => QueryCore(conn, sql, type, parameters));

        public static object Scalar(string sql, CommandType type, params SqlParameter[] parameters)
            => WithConnection(conn => ScalarCore(conn, sql, type, parameters));

        public static int Execute(string sql, CommandType type, params SqlParameter[] parameters)
            => WithConnection(conn => ExecuteCore(conn, sql, type, parameters));

        public static int ExecuteTran(IsolationLevel iso, Func<SqlConnection, SqlTransaction, int> work)
        {
            using (var conn = OpenConnection())
            using (var tran = conn.BeginTransaction(iso))
            {
                try
                {
                    var result = work(conn, tran);
                    tran.Commit();
                    return result;
                }
                catch
                {
                    try { tran.Rollback(); } catch { /* ignore */ }
                    throw;
                }
            }
        }

        // ========== CORE IMPLEMENTATION (HỖ TRỢ NỘI BỘ) ==========

        private static T WithConnection<T>(Func<SqlConnection, T> body)
        {
            using (var conn = OpenConnection())
            {
                return body(conn);
            }
        }

        private static DataTable QueryCore(SqlConnection conn, string sql, CommandType type, SqlParameter[] ps)
        {
            using (var cmd = CreateCommand(conn, sql, type, ps))
            using (var rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
            {
                var dt = new DataTable();
                dt.Load(rdr);
                return dt;
            }
        }

        private static object ScalarCore(SqlConnection conn, string sql, CommandType type, SqlParameter[] ps)
        {
            using (var cmd = CreateCommand(conn, sql, type, ps))
            {
                var val = cmd.ExecuteScalar();
                return val == DBNull.Value ? null : val;
            }
        }

        private static int ExecuteCore(SqlConnection conn, string sql, CommandType type, SqlParameter[] ps)
        {
            using (var cmd = CreateCommand(conn, sql, type, ps))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        private static SqlCommand CreateCommand(SqlConnection conn, string sql, CommandType type, SqlParameter[] ps)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = type;
            cmd.CommandTimeout = DefaultCommandTimeoutSeconds;
            if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
            return cmd;
        }

        // ========== TIỆN ÍCH TUỲ CHỌN (dùng khi cần) ==========

        public static int ExecuteInTran(SqlConnection conn, SqlTransaction tran, string sql, params SqlParameter[] ps)
        {
            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.CommandTimeout = DefaultCommandTimeoutSeconds;
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                return cmd.ExecuteNonQuery();
            }
        }

        public static object ScalarInTran(SqlConnection conn, SqlTransaction tran, string sql, params SqlParameter[] ps)
        {
            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.CommandTimeout = DefaultCommandTimeoutSeconds;
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                var val = cmd.ExecuteScalar();
                return val == DBNull.Value ? null : val;
            }
        }

        public static DataTable QueryInTran(SqlConnection conn, SqlTransaction tran, string sql, params SqlParameter[] parameters)
        {
            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.CommandTimeout = DefaultCommandTimeoutSeconds;
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        // === HÀM MỚI (TRẢ VỀ 1 GIÁ TRỊ DUY NHẤT) ===
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            return Scalar(sql, CommandType.Text, parameters);
        }

        public static void CheckAndInitDatabase()
        {
            try
            {
                string checkTable = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'THONGTINCONGTY'";
                int count = Convert.ToInt32(ExecuteScalar(checkTable));

                if (count == 0)
                {
                    string createTable = @"
                        CREATE TABLE THONGTINCONGTY (
                            ID INT PRIMARY KEY IDENTITY(1,1),
                            TENCONGTY NVARCHAR(100),
                            DIACHI NVARCHAR(200),
                            DIENTHOAI NVARCHAR(20),
                            EMAIL NVARCHAR(50),
                            WEBSITE NVARCHAR(50)
                        );
                        INSERT INTO THONGTINCONGTY (TENCONGTY, DIACHI, DIENTHOAI, EMAIL, WEBSITE)
                        VALUES (N'CÔNG TY TNHH ABC', N'123 Đường ABC, TP.HCM', '0123456789', 'contact@abc.com', 'www.abc.com');";
                    Execute(createTable);
                }
            }
            catch (Exception ex)
            {
                // Log error or ignore if db not ready
                Console.WriteLine("DB Init Error: " + ex.Message);
            }
        }
    }
}