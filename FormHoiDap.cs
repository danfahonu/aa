using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data; // Đảm bảo using DbHelper và GeminiHelper

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormHoiDap : Form
    {
        public FormHoiDap()
        {
            InitializeComponent();
        }

        private async void btnHoi_Click(object sender, EventArgs e)
        {
            string cauHoi = txtCauHoi.Text.Trim();
            if (string.IsNullOrEmpty(cauHoi))
            {
                MessageBox.Show("Vui lòng nhập câu hỏi của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Vô hiệu hóa UI để người dùng không thao tác khi đang xử lý ---
            SetLoadingState(true);
            lblTrangThai.Text = "Đang gửi câu hỏi đến AI...";

            try
            {
                // Bước 1: Chuyển câu hỏi thành SQL
                string sqlQuery = await GeminiHelper.ChuyenCauHoiThanhSQL(cauHoi);
                lblTrangThai.Text = "Đã nhận được câu lệnh SQL, đang truy vấn CSDL...";

                // --- Kiểm tra và bảo mật câu lệnh SQL ---
                if (!IsSafeQuery(sqlQuery))
                {
                    MessageBox.Show("Câu hỏi của bạn có chứa từ khóa không được phép hoặc không phải là lệnh truy vấn dữ liệu.", "Lỗi bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgvKetQua.DataSource = null;
                    txtSQL.Text = sqlQuery;
                    lblTrangThai.Text = "Đã dừng lại do lỗi bảo mật.";
                    return;
                }

                txtSQL.Text = sqlQuery; // Hiển thị câu SQL để kiểm tra

                // Bước 2: Thực thi câu lệnh SQL
                DataTable result = DbHelper.Query(sqlQuery);
                dgvKetQua.DataSource = result;
                lblTrangThai.Text = $"Hoàn tất! Tìm thấy {result.Rows.Count} kết quả.";
            }
            catch (Exception ex)
            {
                // Bắt lỗi (từ Gemini hoặc CSDL) và hiển thị thông báo
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblTrangThai.Text = "Đã xảy ra lỗi trong quá trình xử lý.";
                dgvKetQua.DataSource = null;
            }
            finally
            {
                // --- Kích hoạt lại UI sau khi xử lý xong ---
                SetLoadingState(false);
            }
        }

        /// <summary>
        /// Hàm kiểm tra bảo mật cơ bản cho câu lệnh SQL từ AI.
        /// </summary>
        private bool IsSafeQuery(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql) || sql.ToUpper().Contains("KHONG_THE_TRA_LOI"))
            {
                return false;
            }

            // Loại bỏ các comment SQL để kiểm tra chính xác hơn
            string cleanSql = System.Text.RegularExpressions.Regex.Replace(sql, @"--.*$", "", System.Text.RegularExpressions.RegexOptions.Multiline);

            string sqlUpper = cleanSql.ToUpper().Trim();

            // Chỉ cho phép câu lệnh bắt đầu bằng SELECT
            if (!sqlUpper.StartsWith("SELECT"))
            {
                return false;
            }

            // Danh sách đen các từ khóa nguy hiểm
            string[] forbiddenKeywords = { "DELETE", "UPDATE", "INSERT", "DROP", "TRUNCATE", "EXEC", "CREATE", "ALTER" };
            if (forbiddenKeywords.Any(keyword => sqlUpper.Contains(keyword)))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Thiết lập trạng thái đang tải cho các control trên form.
        /// </summary>
        private void SetLoadingState(bool isLoading)
        {
            txtCauHoi.Enabled = !isLoading;
            btnHoi.Enabled = !isLoading;
            dgvKetQua.Enabled = !isLoading;
            if (isLoading)
            {
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}