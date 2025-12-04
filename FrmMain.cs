using System;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using DoAnLapTrinhQuanLy.Controls;
using DoAnLapTrinhQuanLy.Core;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FrmMain : BaseForm
    {
        private UserData _loggedInUser;
        private Form _currentForm;

        // UI Layout Controls
        private Panel pnlNavigationRail;
        private Panel pnlSubMenuContainer;
        private Panel pnlContent;
        private Panel pnlHeader; // New Header
        private StatusStrip statusStrip;
        private ToolStripStatusLabel staUser;
        private ToolStripStatusLabel staDb;
        private ToolStripStatusLabel staTime;

        // Header Controls
        private Label lblAppTitle;
        private MaterialTextBox txtGlobalSearch;

        // Rail Buttons
        private ModernButton btnDashboard;
        private ModernButton btnHeThong;
        private ModernButton btnDanhMuc;
        private ModernButton btnNghiepVu;
        private ModernButton btnBaoCao;
        private ModernButton btnLogout;

        // SubMenu Panels
        private Panel pnlHeThongSub;
        private Panel pnlDanhMucSub;
        private Panel pnlNghiepVuSub;
        private Panel pnlBaoCaoSub;

        // SubMenu Buttons - HeThong
        private ModernButton btnQuanLyHeThong;
        private ModernButton btnCaiDat;
        private ModernButton btnKetNoiCSDL;
        private ModernButton btnAbout;

        // SubMenu Buttons - DanhMuc
        private ModernButton btnHangHoa;
        private ModernButton btnNhomHang;
        private ModernButton btnKhachHang;
        private ModernButton btnNhaCungCap;
        private ModernButton btnNhanVien;
        private ModernButton btnTaiKhoanNganHang;
        private ModernButton btnHeThongTKKeToan;

        // SubMenu Buttons - NghiepVu
        private ModernButton btnNhapKho;
        private ModernButton btnXuatKho;
        private ModernButton btnPhieuThu;
        private ModernButton btnPhieuChi;
        private ModernButton btnBaoGia;
        private ModernButton btnChamCong;
        private ModernButton btnYeuCauNhapKho;

        // SubMenu Buttons - BaoCao
        private ModernButton btnBaoCaoNhapKho;
        private ModernButton btnBaoCaoXuatKho;
        private ModernButton btnBaoCaoTonKho;
        private ModernButton btnBaoCaoQuy;
        private ModernButton btnBaoCaoNhatKyChung;
        private ModernButton btnBaoCaoSoChiTietTK;
        private ModernButton btnBaoCaoCongNo;
        private ModernButton btnBaoCaoLuong;

        public FrmMain()
        {
            InitializeComponent();
            InitializeCustomUI();
        }

        private void InitializeCustomUI()
        {
            this.Size = new Size(1280, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "SALEGEARVN - QUẢN LÝ BÁN HÀNG";

            // 1. Navigation Rail (Leftmost)
            pnlNavigationRail = new Panel
            {
                Dock = DockStyle.Left,
                Width = 80,
                BackColor = ThemeManager.SecondaryColor
            };

            // 2. SubMenu Container (Next to Rail)
            pnlSubMenuContainer = new Panel
            {
                Dock = DockStyle.Left,
                Width = 220,
                BackColor = Color.FromArgb(30, 30, 30),
                Visible = false
            };

            // 3. Header (Top)
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = ThemeManager.SecondaryColor
            };
            InitializeHeader();

            // 4. Status Strip (Bottom)
            statusStrip = new StatusStrip
            {
                BackColor = ThemeManager.AccentColor,
                ForeColor = Color.White
            };
            staUser = new ToolStripStatusLabel { Text = "User: ..." };
            staDb = new ToolStripStatusLabel { Text = "DB: ..." };
            staTime = new ToolStripStatusLabel { Text = "Time: ..." };
            statusStrip.Items.AddRange(new ToolStripItem[] { staUser, staDb, staTime });

            // 5. Content Panel (Fill)
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeManager.PrimaryColor
            };

            // Add Layout Containers
            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlHeader); // Add Header
            this.Controls.Add(pnlSubMenuContainer);
            this.Controls.Add(pnlNavigationRail);
            this.Controls.Add(statusStrip);

            // Initialize Buttons and SubMenus
            InitializeRailButtons();
            InitializeSubMenus();
        }

        private void InitializeHeader()
        {
            lblAppTitle = new Label
            {
                Text = "SALEGEARVN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ThemeManager.AccentColor,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            pnlHeader.Controls.Add(lblAppTitle);

            // Global Search Bar
            txtGlobalSearch = new MaterialTextBox
            {
                PlaceholderText = "🔍 Search (Ctrl+P)",
                Size = new Size(400, 35),
                Location = new Point((this.Width - 400) / 2, 7),
                Anchor = AnchorStyles.Top // Centered roughly
            };
            pnlHeader.Controls.Add(txtGlobalSearch);
        }

        private void InitializeRailButtons()
        {
            // Helper to create rail buttons
            static ModernButton CreateRailBtn(string text, string iconText)
            {
                var btn = new ModernButton
                {
                    Text = text,
                    Dock = DockStyle.Top,
                    Height = 70,
                    BackColor = Color.Transparent,
                    ForeColor = ThemeManager.TextColor,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderRadius = 10
                };
                return btn;
            }

            // Add in reverse order of Dock=Top
            btnLogout = CreateRailBtn("Thoát", "EXIT");
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.BackColor = ThemeManager.DangerColor;
            btnLogout.Click += (s, e) => Application.Exit();
            pnlNavigationRail.Controls.Add(btnLogout);

            btnBaoCao = CreateRailBtn("Báo Cáo", "BC");
            btnBaoCao.Click += (s, e) => ToggleSubMenu(pnlBaoCaoSub);
            pnlNavigationRail.Controls.Add(btnBaoCao);

            btnNghiepVu = CreateRailBtn("Nghiệp Vụ", "NV");
            btnNghiepVu.Click += (s, e) => ToggleSubMenu(pnlNghiepVuSub);
            pnlNavigationRail.Controls.Add(btnNghiepVu);

            btnDanhMuc = CreateRailBtn("Danh Mục", "DM");
            btnDanhMuc.Click += (s, e) => ToggleSubMenu(pnlDanhMucSub);
            pnlNavigationRail.Controls.Add(btnDanhMuc);

            btnHeThong = CreateRailBtn("Hệ Thống", "HT");
            btnHeThong.Click += (s, e) => ToggleSubMenu(pnlHeThongSub);
            pnlNavigationRail.Controls.Add(btnHeThong);

            btnDashboard = CreateRailBtn("Dashboard", "DB");
            btnDashboard.Click += (s, e) => { ShowForm<FormDashboard>(); HideSubMenu(); };
            pnlNavigationRail.Controls.Add(btnDashboard);
        }

        private void InitializeSubMenus()
        {
            // Helper to create sub-menu buttons
            static ModernButton CreateSubBtn(string text, EventHandler onClick)
            {
                var btn = new ModernButton
                {
                    Text = text,
                    Dock = DockStyle.Top,
                    Height = 45,
                    BackColor = Color.Transparent,
                    ForeColor = ThemeManager.TextColor,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(15, 0, 0, 0),
                    BorderRadius = 0
                };
                btn.Click += onClick;
                return btn;
            }

            // 1. HeThong SubMenu
            pnlHeThongSub = new Panel { Dock = DockStyle.Fill, Visible = false };
            btnAbout = CreateSubBtn("Thông tin phần mềm", (s, e) => new FormThongTinPhanMem().ShowDialog(this));
            btnKetNoiCSDL = CreateSubBtn("Kết nối CSDL", (s, e) => new FormKetNoiCSDL().ShowDialog(this));
            btnCaiDat = CreateSubBtn("Cài đặt chung", (s, e) => ShowForm<FormCaiDatHeThong>());
            btnQuanLyHeThong = CreateSubBtn("Quản lý Hệ thống", (s, e) => ShowForm<FormQuanLyHeThong>());

            pnlHeThongSub.Controls.Add(btnAbout);
            pnlHeThongSub.Controls.Add(btnKetNoiCSDL);
            pnlHeThongSub.Controls.Add(btnCaiDat);
            pnlHeThongSub.Controls.Add(btnQuanLyHeThong);
            pnlSubMenuContainer.Controls.Add(pnlHeThongSub);

            // 2. DanhMuc SubMenu
            pnlDanhMucSub = new Panel { Dock = DockStyle.Fill, Visible = false };
            btnHeThongTKKeToan = CreateSubBtn("Hệ thống TK Kế toán", (s, e) => ShowForm<FormHeThongTaiKhoanKeToan>());
            btnTaiKhoanNganHang = CreateSubBtn("Tài khoản Ngân hàng", (s, e) => ShowForm<FormQuanLyTaiKhoanNganHang>());
            btnNhanVien = CreateSubBtn("Nhân viên", (s, e) => ShowForm<FormNhanVien>());
            btnNhaCungCap = CreateSubBtn("Nhà cung cấp", (s, e) => ShowForm<FormNhaCungCap>());
            btnKhachHang = CreateSubBtn("Khách hàng", (s, e) => ShowForm<FormKhachHang>());
            btnNhomHang = CreateSubBtn("Nhóm hàng", (s, e) => ShowForm<FormNhomHang>());
            btnHangHoa = CreateSubBtn("Hàng hóa - Vật tư", (s, e) => ShowForm<FormDanhMucHangHoa>());

            pnlDanhMucSub.Controls.Add(btnHeThongTKKeToan);
            pnlDanhMucSub.Controls.Add(btnTaiKhoanNganHang);
            pnlDanhMucSub.Controls.Add(btnNhanVien);
            pnlDanhMucSub.Controls.Add(btnNhaCungCap);
            pnlDanhMucSub.Controls.Add(btnKhachHang);
            pnlDanhMucSub.Controls.Add(btnNhomHang);
            pnlDanhMucSub.Controls.Add(btnHangHoa);
            pnlSubMenuContainer.Controls.Add(pnlDanhMucSub);

            // 3. NghiepVu SubMenu
            pnlNghiepVuSub = new Panel { Dock = DockStyle.Fill, Visible = false };
            btnYeuCauNhapKho = CreateSubBtn("Yêu cầu Nhập kho", (s, e) => ShowForm<FormYeuCauNhapKho>());
            btnChamCong = CreateSubBtn("Chấm công", (s, e) => ShowForm<FormTamUngChamCong>());
            btnBaoGia = CreateSubBtn("Báo giá", (s, e) => ShowForm<FormBangBaoGia>());
            btnPhieuChi = CreateSubBtn("Phiếu chi", (s, e) => ShowForm<FormPhieuChi>());
            btnPhieuThu = CreateSubBtn("Phiếu thu", (s, e) => ShowForm<FormPhieuThu>());
            btnXuatKho = CreateSubBtn("Phiếu Xuất kho", (s, e) => ShowForm<FormPhieuXuat>());
            btnNhapKho = CreateSubBtn("Phiếu Nhập kho", (s, e) => ShowForm<FormPhieuNhap>());

            pnlNghiepVuSub.Controls.Add(btnYeuCauNhapKho);
            pnlNghiepVuSub.Controls.Add(btnChamCong);
            pnlNghiepVuSub.Controls.Add(btnBaoGia);
            pnlNghiepVuSub.Controls.Add(btnPhieuChi);
            pnlNghiepVuSub.Controls.Add(btnPhieuThu);
            pnlNghiepVuSub.Controls.Add(btnXuatKho);
            pnlNghiepVuSub.Controls.Add(btnNhapKho);
            pnlSubMenuContainer.Controls.Add(pnlNghiepVuSub);

            // 4. BaoCao SubMenu
            pnlBaoCaoSub = new Panel { Dock = DockStyle.Fill, Visible = false };
            btnBaoCaoLuong = CreateSubBtn("Báo cáo Lương", (s, e) => ShowForm<FormTinhLuong>());
            btnBaoCaoCongNo = CreateSubBtn("Báo cáo Công nợ", (s, e) => ShowForm<FormReportCongNo>());
            btnBaoCaoSoChiTietTK = CreateSubBtn("Sổ chi tiết tài khoản", (s, e) => ShowForm<FormSoChiTietTaiKhoan>());
            btnBaoCaoNhatKyChung = CreateSubBtn("Sổ nhật ký chung", (s, e) => ShowForm<FormSoNhatKyChung>());
            btnBaoCaoQuy = CreateSubBtn("Báo cáo Quỹ", (s, e) => ShowForm<FormBaoCaoQuy>());
            btnBaoCaoTonKho = CreateSubBtn("Tổng hợp tồn kho", (s, e) => ShowForm<FormBaoCaoTonKho>());
            btnBaoCaoXuatKho = CreateSubBtn("Báo cáo Xuất kho", (s, e) => ShowBaoCaoKho());
            btnBaoCaoNhapKho = CreateSubBtn("Báo cáo Nhập kho", (s, e) => ShowBaoCaoKho());

            pnlBaoCaoSub.Controls.Add(btnBaoCaoLuong);
            pnlBaoCaoSub.Controls.Add(btnBaoCaoCongNo);
            pnlBaoCaoSub.Controls.Add(btnBaoCaoSoChiTietTK);
            pnlBaoCaoSub.Controls.Add(btnBaoCaoNhatKyChung);
            pnlBaoCaoSub.Controls.Add(btnBaoCaoQuy);
            pnlBaoCaoSub.Controls.Add(btnBaoCaoTonKho);
            pnlBaoCaoSub.Controls.Add(btnBaoCaoXuatKho);
            pnlBaoCaoSub.Controls.Add(btnBaoCaoNhapKho);
            pnlSubMenuContainer.Controls.Add(pnlBaoCaoSub);
        }

        private void ToggleSubMenu(Panel subMenu)
        {
            if (pnlSubMenuContainer.Visible && subMenu.Visible)
            {
                // Toggle off
                pnlSubMenuContainer.Visible = false;
                subMenu.Visible = false;
            }
            else
            {
                // Show container and switch sub-menu
                pnlSubMenuContainer.Visible = true;
                pnlHeThongSub.Visible = false;
                pnlDanhMucSub.Visible = false;
                pnlNghiepVuSub.Visible = false;
                pnlBaoCaoSub.Visible = false;

                subMenu.Visible = true;
                subMenu.BringToFront();
            }
        }

        private void HideSubMenu()
        {
            pnlSubMenuContainer.Visible = false;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Status Strip Logic
            try
            {
                var cs = ConfigurationManager.ConnectionStrings["Db"]?.ConnectionString ?? "(chưa cấu hình)";
                staDb.Text = " | DB: " + new System.Data.SqlClient.SqlConnectionStringBuilder(cs).InitialCatalog;
            }
            catch { staDb.Text = " | DB: (lỗi cấu hình)"; }

            staTime.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            var timer = new Timer { Interval = 1000 };
            timer.Tick += (s, a) => staTime.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            timer.Start();

            // Open Dashboard
            ShowForm<FormDashboard>();
        }

        public void SetLoggedInUser(UserData user)
        {
            _loggedInUser = user;
            staUser.Text = "User: " + (_loggedInUser != null ? $"{_loggedInUser.HoTen} ({_loggedInUser.TenQuyen})" : "(chưa đăng nhập)");
            ApplyNavigationPermissions();
        }

        private void ApplyNavigationPermissions()
        {
            if (_loggedInUser == null) return;
            string userRole = _loggedInUser.TenQuyen;

            // Main Categories
            bool isAdmin = userRole == "Administrator";
            bool isKeToan = userRole == "Kế toán";
            bool isSale = userRole == "Nhân viên Kinh doanh";
            bool isKho = userRole == "Nhân viên Kho";

            btnHeThong.Visible = isAdmin;
            btnDanhMuc.Visible = true; // Most roles have some access
            btnNghiepVu.Visible = true;
            btnBaoCao.Visible = true;

            // Sub Items - HeThong
            btnQuanLyHeThong.Visible = isAdmin;
            btnCaiDat.Visible = isAdmin;
            btnKetNoiCSDL.Visible = isAdmin;

            // Sub Items - DanhMuc
            btnHangHoa.Visible = isAdmin || isKeToan || isSale || isKho;
            btnNhomHang.Visible = isAdmin || isKeToan || isSale || isKho;
            btnKhachHang.Visible = isAdmin || isKeToan || isSale;
            btnNhaCungCap.Visible = isAdmin || isKeToan || isKho;
            btnNhanVien.Visible = isAdmin;
            btnTaiKhoanNganHang.Visible = isAdmin || isKeToan;
            btnHeThongTKKeToan.Visible = isAdmin || isKeToan;

            // Sub Items - NghiepVu
            btnNhapKho.Visible = isAdmin || isKeToan || isKho;
            btnXuatKho.Visible = isAdmin || isKeToan || isSale;
            btnYeuCauNhapKho.Visible = isAdmin || isKeToan || isKho;
            btnBaoGia.Visible = isAdmin || isKeToan || isSale;
            btnPhieuThu.Visible = isAdmin || isKeToan;
            btnPhieuChi.Visible = isAdmin || isKeToan;
            btnChamCong.Visible = isAdmin || isKeToan;

            // Sub Items - BaoCao
            btnBaoCaoNhapKho.Visible = isAdmin || isKeToan || isKho;
            btnBaoCaoXuatKho.Visible = isAdmin || isKeToan || isKho;
            btnBaoCaoTonKho.Visible = isAdmin || isKeToan || isKho;
            btnBaoCaoQuy.Visible = isAdmin || isKeToan;
            btnBaoCaoNhatKyChung.Visible = isAdmin || isKeToan;
            btnBaoCaoSoChiTietTK.Visible = isAdmin || isKeToan;
            btnBaoCaoCongNo.Visible = isAdmin || isKeToan;
            btnBaoCaoLuong.Visible = isAdmin || isKeToan;
        }

        private void ShowForm<T>() where T : Form, new()
        {
            if (_currentForm != null && _currentForm.GetType() == typeof(T)) { _currentForm.BringToFront(); return; }
            _currentForm?.Close();
            var newForm = new T { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            pnlContent.Controls.Add(newForm);
            _currentForm = newForm;
            newForm.Show();
        }

        private void ShowBaoCaoKho()
        {
            _currentForm?.Close();
            // Placeholder for Report Logic
            // var newForm = new FormBaoCaoKho(reportType) { ... };
            // pnlContent.Controls.Add(newForm);
            // ...
        }
    }
}