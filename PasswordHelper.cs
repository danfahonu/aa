using System;
using System.Linq;
using System.Security.Cryptography;

namespace DoAnLapTrinhQuanLy.Data // Đảm bảo namespace này đúng với dự án của bạn
{
    /// <summary>
    /// Lớp trợ giúp xử lý mã hóa và xác thực mật khẩu bằng thuật toán PBKDF2.
    /// </summary>
    public static class PasswordHelper
    {
        private const int SaltSize = 16; // 128 bit
        private const int HashSize = 64; // 512 bit
        private const int Iterations = 10000; // Số lần lặp để tăng độ khó giải mã

        /// <summary>
        /// Tạo ra một chuỗi Hash và Salt từ mật khẩu văn bản thuần.
        /// </summary>
        /// <param name="password">Mật khẩu cần mã hóa.</param>
        /// <returns>Một Tuple chứa Hash (byte[]) và Salt (byte[]).</returns>
        public static (byte[] Hash, byte[] Salt) HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA512))
                {
                    byte[] hash = pbkdf2.GetBytes(HashSize);
                    return (hash, salt);
                }
            }
        }

        /// <summary>
        /// Xác thực một mật khẩu văn bản thuần với một chuỗi Hash và Salt đã lưu.
        /// </summary>
        /// <param name="password">Mật khẩu người dùng nhập vào.</param>
        /// <param name="storedHash">Hash đã lưu trong CSDL.</param>
        /// <param name="storedSalt">Salt đã lưu trong CSDL.</param>
        /// <returns>True nếu mật khẩu khớp, ngược lại là False.</returns>
        public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != HashSize || storedSalt.Length != SaltSize)
            {
                // Dữ liệu trong CSDL không hợp lệ
                return false;
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, Iterations, HashAlgorithmName.SHA512))
            {
                byte[] testHash = pbkdf2.GetBytes(HashSize);
                // So sánh từng byte của hai chuỗi hash
                return testHash.SequenceEqual(storedHash);
            }
        }
    }
}