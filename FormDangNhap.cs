using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;
using DoAnLapTrinhQuanLy.Controls;
using DoAnLapTrinhQuanLy.Core;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormDangNhap : BaseForm
    {
        public UserData AuthenticatedUser { get; private set; }

        // UI Controls
        private Panel pnlCard;
        private Label lblHeader;
        private MaterialTextBox txtLoginUsername;
        private MaterialTextBox txtLoginPassword;
        private ModernButton btnLogin;
        private ModernButton btnExit;
        private CheckBox chkTogglePassword;

        public FormDangNhap()
        {
            InitializeComponent();
            InitializeCustomUI();
        }

        private void InitializeCustomUI()
        {
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Background Gradient (Handled in OnPaint if needed, or just dark color)
            this.BackColor = ThemeManager.PrimaryColor;

            // Floating Card
            pnlCard = new Panel
            {
                Size = new Size(400, 380),
                BackColor = ThemeManager.SecondaryColor,
                Anchor = AnchorStyles.None
            };
            // Center the card
            pnlCard.Location = new Point((this.Width - pnlCard.Width) / 2, (this.Height - pnlCard.Height) / 2);
            ThemeControlExtensions.SetDoubleBuffered(pnlCard, true);

            // Header
            lblHeader = new Label
            {
                Text = "ĐĂNG NHẬP",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 80
            };

            // Inputs
            txtLoginUsername = new MaterialTextBox
            {
                PlaceholderText = "Tài khoản",
                Location = new Point(50, 90),
                Size = new Size(300, 40),
                Texts = ""
            };

            txtLoginPassword = new MaterialTextBox
            {
                PlaceholderText = "Mật khẩu",
                PasswordChar = true,
                Location = new Point(50, 150),
                Size = new Size(300, 40),
                Texts = ""
            };

            chkTogglePassword = new CheckBox
            {
                Text = "Hiện mật khẩu",
                Location = new Point(50, 200),
                AutoSize = true,
                ForeColor = ThemeManager.TextColor
            };
            chkTogglePassword.CheckedChanged += ChkShowPassword_CheckedChanged;

            // Buttons
            btnLogin = new ModernButton
            {
                Text = "ĐĂNG NHẬP",
                Location = new Point(50, 240),
                Size = new Size(300, 45),
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White,
                BorderRadius = 20
            };
            btnLogin.Click += BtnDangNhap_Click;

            btnExit = new ModernButton
            {
                Text = "THOÁT",
                Location = new Point(50, 300),
                Size = new Size(300, 45),
                BackColor = ThemeManager.DangerColor,
                ForeColor = Color.White,
                BorderRadius = 20
            };
            btnExit.Click += BtnThoat_Click;

            // Add Controls to Card
            pnlCard.Controls.Add(btnExit);
            pnlCard.Controls.Add(btnLogin);
            pnlCard.Controls.Add(chkTogglePassword);
            pnlCard.Controls.Add(txtLoginPassword);
            pnlCard.Controls.Add(txtLoginUsername);
            pnlCard.Controls.Add(lblHeader);

            this.Controls.Add(pnlCard);

            // Allow Enter key to trigger login
            this.AcceptButton = btnLogin;

            // Handle Resize to keep centered
            this.Resize += (s, e) =>
            {
                pnlCard.Location = new Point((this.Width - pnlCard.Width) / 2, (this.Height - pnlCard.Height) / 2);
            };
        }

        // Override OnPaint to draw a subtle border for the card
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw Card Border
            if (pnlCard != null)
            {
                using (Pen p = new Pen(ThemeManager.BorderColor, 1))
                {
                    Rectangle rect = new Rectangle(pnlCard.Location.X - 1, pnlCard.Location.Y - 1, pnlCard.Width + 1, pnlCard.Height + 1);
                    e.Graphics.DrawRectangle(p, rect);
                }
            }
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            // BaseForm handles ThemeManager.Apply(this);
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtLoginUsername.Texts;
            string matKhau = txtLoginPassword.Texts;

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = @"
                    SELECT nd.TAIKHOAN, nd.MATKHAU, nd.HOTEN, nd.MANV, qh.TENQUYEN 
                    FROM NGUOIDUNG nd 
                    JOIN QUYENHAN qh ON nd.MAQUYEN = qh.MAQUYEN 
                    WHERE nd.TAIKHOAN = @TaiKhoan AND nd.MATKHAU = @MatKhau AND nd.HOATDONG = 1";

                DataTable dt = DbHelper.Query(query,
                                    DbHelper.Param("@TaiKhoan", taiKhoan),
                                    DbHelper.Param("@MatKhau", matKhau));

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    AuthenticatedUser = new UserData
                    {
                        TaiKhoan = row["TAIKHOAN"].ToString(),
                        MatKhau = row["MATKHAU"].ToString(),
                        HoTen = row["HOTEN"].ToString(),
                        TenQuyen = row["TENQUYEN"].ToString(),
                        MaNV = row["MANV"].ToString()
                    };

                    Session.LoggedInUser = this.AuthenticatedUser;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng, hoặc tài khoản đã bị khóa.", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtLoginPassword.PasswordChar = !chkTogglePassword.Checked;
        }
    }
}
