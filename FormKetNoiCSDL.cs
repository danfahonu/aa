using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data; // Sử dụng DbHelper

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    /// <summary>
    /// Form cấu hình và kiểm tra chuỗi kết nối CSDL SQL Server.
    /// </summary>
    public partial class FormKetNoiCSDL : Form
    {
        private const string _connectionName = "Db";

        // Cấu trúc đơn giản cho chế độ xác thực
        private class AuthMode
        {
            public string Name { get; set; }
            public bool RequiresLogin { get; set; }
        }

        public FormKetNoiCSDL()
        {
            InitializeComponent();
        }

        private void FormKetNoiCSDL_Load(object sender, EventArgs e)
        {
            // 1. Khởi tạo ComboBox Xác thực
            cboAuthMode.Items.Add(new AuthMode { Name = "Windows Authentication", RequiresLogin = false });
            cboAuthMode.Items.Add(new AuthMode { Name = "SQL Server Authentication", RequiresLogin = true });
            cboAuthMode.DisplayMember = "Name";
            cboAuthMode.ValueMember = "RequiresLogin";
            cboAuthMode.SelectedIndex = 0;

            // 2. Tải chuỗi kết nối hiện tại (nếu có)
            LoadExistingConnectionString();
        }

        /// <summary>
        /// Đọc và phân tích chuỗi kết nối hiện tại trong App.config.
        /// </summary>
        private void LoadExistingConnectionString()
        {
            try
            {
                var settings = ConfigurationManager.ConnectionStrings[_connectionName];
                if (settings != null && !string.IsNullOrWhiteSpace(settings.ConnectionString))
                {
                    var csb = new SqlConnectionStringBuilder(settings.ConnectionString);

                    txtServer.Text = csb.DataSource;
                    txtDBName.Text = csb.InitialCatalog;

                    if (csb.IntegratedSecurity)
                    {
                        cboAuthMode.SelectedIndex = 0; // Windows Auth
                    }
                    else
                    {
                        cboAuthMode.SelectedIndex = 1; // SQL Server Auth
                        txtUser.Text = csb.UserID;
                        txtPassword.Text = csb.Password;
                    }
                }
            }
            catch (Exception ex)
            {
                // Bỏ qua lỗi nếu chuỗi kết nối cũ bị lỗi
                // MessageBox.Show("Lỗi đọc cấu hình cũ: " + ex.Message);
            }
        }

        /// <summary>
        /// Xây dựng chuỗi kết nối từ dữ liệu nhập vào Form.
        /// </summary>
        private string BuildConnectionString()
        {
            if (string.IsNullOrWhiteSpace(txtServer.Text) || string.IsNullOrWhiteSpace(txtDBName.Text))
            {
                return null;
            }

            var csb = new SqlConnectionStringBuilder();
            csb.DataSource = txtServer.Text.Trim();
            csb.InitialCatalog = txtDBName.Text.Trim();
            csb.TrustServerCertificate = true; // Thường cần thiết cho các phiên bản SQL Express/LocalDB

            var selectedAuth = cboAuthMode.SelectedItem as AuthMode;

            if (selectedAuth.RequiresLogin) // SQL Server Authentication
            {
                if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tài khoản và Mật khẩu SQL Server.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
                csb.IntegratedSecurity = false;
                csb.UserID = txtUser.Text.Trim();
                csb.Password = txtPassword.Text;
            }
            else // Windows Authentication
            {
                csb.IntegratedSecurity = true;
            }

            return csb.ConnectionString;
        }

        // ===================================================================================
        // SỰ KIỆN CONTROLS
        // ===================================================================================

        private void cboAuthMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedAuth = cboAuthMode.SelectedItem as AuthMode;
            bool requiresLogin = selectedAuth.RequiresLogin;

            lblUser.Enabled = requiresLogin;
            txtUser.Enabled = requiresLogin;
            lblPassword.Enabled = requiresLogin;
            txtPassword.Enabled = requiresLogin;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string cs = BuildConnectionString();
            if (cs == null) return;

            try
            {
                // Thử mở và đóng kết nối
                using (var conn = new SqlConnection(cs))
                {
                    conn.Open();
                    conn.Close();
                }
                MessageBox.Show("Kiểm tra kết nối thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối thất bại. Chi tiết lỗi:\n\n" + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string cs = BuildConnectionString();
            if (cs == null) return;

            // 1. Kiểm tra kết nối trước khi lưu
            try
            {
                using (var conn = new SqlConnection(cs))
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Kiểm tra kết nối thất bại. Bạn vẫn muốn lưu cấu hình này không?\n\n" + ex.Message, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }

            // 2. Lưu chuỗi kết nối vào App.config
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConnectionStringsSection csSection = config.ConnectionStrings;

                // Xóa cấu hình cũ (nếu có)
                if (csSection.ConnectionStrings[_connectionName] != null)
                {
                    csSection.ConnectionStrings.Remove(_connectionName);
                }

                // Thêm cấu hình mới
                csSection.ConnectionStrings.Add(new ConnectionStringSettings(_connectionName, cs, "System.Data.SqlClient"));

                // Lưu file cấu hình
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");

                // Cập nhật lại DbHelper (nếu cần, mặc dù DbHelper dùng Lazy nên sẽ tự cập nhật)
                // DbHelper.ResetConnectionStringCache(); // Nếu bạn có hàm này trong DbHelper

                MessageBox.Show("Lưu cấu hình thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ghi vào App.config: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAttachDB_Click(object sender, EventArgs e)
        {
            // Mở form Đính kèm CSDL (FormAttach)
            // Bạn cần đảm bảo FormAttach.cs đã được thêm vào dự án
            try
            {
                using (var formAttach = new FormAttach())
                {
                    if (formAttach.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("Đính kèm thành công! Vui lòng kiểm tra lại kết nối.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Tải lại cấu hình hiện tại sau khi Attach (tùy chọn)
                        LoadExistingConnectionString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở form Đính kèm CSDL. Chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}