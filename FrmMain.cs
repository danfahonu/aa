using System;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using DoAnLapTrinhQuanLy.GuiLayer;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FrmMain : Form
    {
        // === BIẾN KẾ THỪA TỪ CODE CŨ ===
        private UserData _loggedInUser;
        private Form _currentForm;

        // === BIẾN MỚI CHO UI HIỆN ĐẠI ===
        private Button _currentNavButton;
        private Panel _activeSubMenuPanel;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Thiết lập vị trí ban đầu cho thanh chỉ báo (indicator)
            pnlNavIndicator.Height = btnDashboard.Height;
            pnlNavIndicator.Top = btnDashboard.Top;
            pnlNavIndicator.Left = 0;

            // === HIỂN THỊ THÔNG TIN STATUS STRIP (Kế thừa) ===
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

            // Ẩn các menu con khi mới load
            pnlSubMenu.Visible = false;
            pnlHeThongSubMenu.Visible = false;
            pnlDanhMucSubMenu.Visible = false;
            pnlNghiepVuSubMenu.Visible = false;
            pnlBaoCaoSubMenu.Visible = false;

            // Thiết lập mô tả cho các nút (Tooltip)
            SetupTooltips();

            // Mở Dashboard làm form mặc định
            ActivateButton(btnDashboard);
            ShowForm<FormDashboard>();
        }

        // === PHƯƠI THỨC MỚI ĐỂ QUẢN LÝ TOOLTIP ===
        private void SetupTooltips()
        {
            toolTipInfo.SetToolTip(btnDashboard, "Trang chủ - Hiển thị tổng quan");
            toolTipInfo.SetToolTip(btnHeThong, "Hệ thống - Quản lý và cài đặt");
            toolTipInfo.SetToolTip(btnDanhMuc, "Danh mục - Quản lý các đối tượng");
            toolTipInfo.SetToolTip(btnNghiepVu, "Nghiệp vụ - Các hoạt động chính");
            toolTipInfo.SetToolTip(btnBaoCao, "Báo cáo - Xem và in ấn số liệu");
        }

        // === KẾ THỪA HOÀN TOÀN TỪ CODE CŨ ===
        public void SetLoggedInUser(UserData user)
        {
            _loggedInUser = user;
            staUser.Text = "User: " + (_loggedInUser != null ? $"{_loggedInUser.HoTen} ({_loggedInUser.TenQuyen})" : "(chưa đăng nhập)");

            // ÁP DỤNG PHÂN QUYỀN CHO GIAO DIỆN MỚI
            ApplyNavigationPermissions();
        }

        #region Kế thừa các phương thức mở Form
        private void ShowForm<T>() where T : Form, new()
        {
            if (_currentForm != null && _currentForm.GetType() == typeof(T)) { _currentForm.BringToFront(); return; }
            _currentForm?.Close();
            var newForm = new T { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelMain.Controls.Add(newForm);
            _currentForm = newForm;
            newForm.Show();
        }

        private void ShowBaoCaoKho(string reportType)
        {
            _currentForm?.Close();
            var newForm = new FormBaoCaoKho(reportType) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, Dock = DockStyle.Fill };
            panelMain.Controls.Add(newForm);
            _currentForm = newForm;
            newForm.Show();
        }
        #endregion

        // === NÂNG CẤP TỪ ApplyPermissions() VÀ BuildTreeView() ===
        private void ApplyNavigationPermissions()
        {
            if (_loggedInUser == null) return;
            string userRole = _loggedInUser.TenQuyen;

            switch (userRole)
            {
                case "Administrator":
                    btnHeThong.Visible = true;
                    btnDanhMuc.Visible = true;
                    btnNghiepVu.Visible = true;
                    btnBaoCao.Visible = true;
                    break;
                case "Kế toán":
                    btnHeThong.Visible = false;
                    btnDanhMuc.Visible = true;
                    btnNghiepVu.Visible = true;
                    btnBaoCao.Visible = true;
                    break;
                case "Nhân viên Kinh doanh":
                    btnHeThong.Visible = false;
                    btnDanhMuc.Visible = true;
                    btnNghiepVu.Visible = true;
                    btnBaoCao.Visible = true;
                    break;
                case "Nhân viên Kho":
                    btnHeThong.Visible = false;
                    btnDanhMuc.Visible = true;
                    btnNghiepVu.Visible = true;
                    btnBaoCao.Visible = true;
                    break;
            }

            // Phân quyền cho các nút trong menu con
            btnQuanLyHeThong.Visible = (userRole == "Administrator");
            btnCaiDat.Visible = (userRole == "Administrator");
            btnKetNoiCSDL.Visible = (userRole == "Administrator");

            btnHangHoa.Visible = (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kinh doanh" || userRole == "Nhân viên Kho");
            btnNhomHang.Visible = (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kinh doanh" || userRole == "Nhân viên Kho");
            btnKhachHang.Visible = (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kinh doanh");
            btnNhaCungCap.Visible = (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kho");
            btnNhanVien.Visible = (userRole == "Administrator");
            btnTaiKhoanNganHang.Visible = (userRole == "Administrator" || userRole == "Kế toán");
            btnHeThongTKKeToan.Visible = (userRole == "Administrator" || userRole == "Kế toán");

            btnNhapKho.Visible = (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kho");
            btnXuatKho.Visible = (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kinh doanh");
            btnBaoGia.Visible = (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kinh doanh");
            btnPhieuThu.Visible = (userRole == "Administrator" || userRole == "Kế toán");
            btnPhieuChi.Visible = (userRole == "Administrator" || userRole == "Kế toán");
            btnChamCong.Visible = (userRole == "Administrator" || userRole == "Kế toán");

            btnBaoCaoNhapKho.Visible = (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kho");
            btnBaoCaoXuatKho.Visible = (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kho");
            btnBaoCaoTonKho.Visible = (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kho");
            btnBaoCaoQuy.Visible = (userRole == "Administrator" || userRole == "Kế toán");
            btnBaoCaoNhatKyChung.Visible = (userRole == "Administrator" || userRole == "Kế toán");
            btnBaoCaoSoChiTietTK.Visible = (userRole == "Administrator" || userRole == "Kế toán");
            btnBaoCaoCongNo.Visible = (userRole == "Administrator" || userRole == "Kế toán");
            btnBaoCaoLuong.Visible = (userRole == "Administrator" || userRole == "Kế toán");
        }

        #region Các phương thức tiện ích cho UI
        private void ActivateButton(Button btnSender)
        {
            if (btnSender == null) return;
            if (_currentNavButton != btnSender)
            {
                if (_currentNavButton != null)
                {
                    _currentNavButton.BackColor = Color.FromArgb(26, 34, 56);
                }
                _currentNavButton = btnSender;
                pnlNavIndicator.Height = btnSender.Height;
                pnlNavIndicator.Top = btnSender.Top;
                btnSender.BackColor = Color.FromArgb(70, 81, 115);
                pnlNavIndicator.Visible = true;
            }
        }

        private void navButton_Leave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != _currentNavButton)
            {
                btn.BackColor = Color.FromArgb(26, 34, 56);
            }
        }

        private void ShowSubMenu(Panel subMenu)
        {
            foreach (Control control in pnlSubMenu.Controls)
            {
                if (control is Panel && control != subMenu)
                    control.Visible = false;
            }

            if (subMenu != null)
            {
                pnlSubMenu.Visible = !subMenu.Visible || _activeSubMenuPanel != subMenu;
                subMenu.Visible = pnlSubMenu.Visible;
            }
            else
            {
                pnlSubMenu.Visible = false;
            }
            _activeSubMenuPanel = subMenu;
        }
        #endregion

        #region Các sự kiện Click (Kế thừa logic từ treeChucNang_NodeMouseDoubleClick)

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as Button);
            ShowSubMenu(null);
            ShowForm<FormDashboard>();
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as Button);
            ShowSubMenu(pnlHeThongSubMenu);
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as Button);
            ShowSubMenu(pnlDanhMucSubMenu);
        }

        private void btnNghiepVu_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as Button);
            ShowSubMenu(pnlNghiepVuSubMenu);
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            ActivateButton(sender as Button);
            ShowSubMenu(pnlBaoCaoSubMenu);
        }

        private void btnQuanLyHeThong_Click(object sender, EventArgs e) => ShowForm<FormQuanLyHeThong>();
        private void btnCaiDat_Click(object sender, EventArgs e) => ShowForm<FormCaiDatHeThong>();
        private void btnKetNoiCSDL_Click(object sender, EventArgs e) => new FormKetNoiCSDL().ShowDialog(this);
        private void btnAbout_Click(object sender, EventArgs e) => new FormThongTinPhanMem().ShowDialog(this);
        private void btnHangHoa_Click(object sender, EventArgs e) => ShowForm<FormDanhMucHangHoa>();
        private void btnNhomHang_Click(object sender, EventArgs e) => ShowForm<FormNhomHang>();
        private void btnKhachHang_Click(object sender, EventArgs e) => ShowForm<FormKhachHang>();
        private void btnNhaCungCap_Click(object sender, EventArgs e) => ShowForm<FormNhaCungCap>();
        private void btnNhanVien_Click(object sender, EventArgs e) => ShowForm<FormNhanVien>();
        private void btnTaiKhoanNganHang_Click(object sender, EventArgs e) => ShowForm<FormQuanLyTaiKhoanNganHang>();
        private void btnHeThongTKKeToan_Click(object sender, EventArgs e) => ShowForm<FormHeThongTaiKhoanKeToan>();
        private void btnNhapKho_Click(object sender, EventArgs e) => ShowForm<FormPhieuNhap>();
        private void btnXuatKho_Click(object sender, EventArgs e) => ShowForm<FormPhieuXuat>();
        private void btnPhieuThu_Click(object sender, EventArgs e) => ShowForm<FormPhieuThu>();
        private void btnPhieuChi_Click(object sender, EventArgs e) => ShowForm<FormPhieuChi>();
        private void btnBaoGia_Click(object sender, EventArgs e) => ShowForm<FormBangBaoGia>();
        private void btnChamCong_Click(object sender, EventArgs e) => ShowForm<FormTamUngChamCong>();
        private void btnBaoCaoNhapKho_Click(object sender, EventArgs e) => ShowBaoCaoKho("NHAP");
        private void btnBaoCaoXuatKho_Click(object sender, EventArgs e) => ShowBaoCaoKho("XUAT");
        private void btnBaoCaoTonKho_Click(object sender, EventArgs e) => ShowForm<FormBaoCaoTonKho>();
        private void btnBaoCaoQuy_Click(object sender, EventArgs e) => ShowForm<FormBaoCaoQuy>();
        private void btnBaoCaoNhatKyChung_Click(object sender, EventArgs e) => ShowForm<FormSoNhatKyChung>();
        private void btnBaoCaoSoChiTietTK_Click(object sender, EventArgs e) => ShowForm<FormSoChiTietTaiKhoan>();
        private void btnBaoCaoCongNo_Click(object sender, EventArgs e) => ShowForm<FormReportCongNo>();
        private void btnBaoCaoLuong_Click(object sender, EventArgs e) => ShowForm<FormTinhLuong>();

        #endregion
    }
}