using System;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FrmMain : Form
    {
        private UserData _loggedInUser;
        private Form _currentForm;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
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

            // Ẩn cây chức năng ban đầu và hiển thị Dashboard
            treeChucNang.Nodes.Clear();
            ShowForm<FormDashboard>();
        }

        public void SetLoggedInUser(UserData user)
        {
            _loggedInUser = user;
            staUser.Text = "User: " + (_loggedInUser != null ? $"{_loggedInUser.HoTen} ({_loggedInUser.TenQuyen})" : "(chưa đăng nhập)");
            ApplyPermissions();
        }

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

        private void ApplyPermissions()
        {
            if (_loggedInUser == null) return;

            btnHeThong.Visible = false;
            btnDanhMuc.Visible = false;
            btnNghiepVu.Visible = false;
            btnBaoCao.Visible = false;

            switch (_loggedInUser.TenQuyen)
            {
                case "Administrator":
                    btnHeThong.Visible = true;
                    btnDanhMuc.Visible = true;
                    btnNghiepVu.Visible = true;
                    btnBaoCao.Visible = true;
                    break;
                case "Kế toán":
                    btnDanhMuc.Visible = true;
                    btnNghiepVu.Visible = true;
                    btnBaoCao.Visible = true;
                    break;
                case "Nhân viên Kinh doanh":
                    btnDanhMuc.Visible = true;
                    btnNghiepVu.Visible = true;
                    btnBaoCao.Visible = true;
                    break;
                case "Nhân viên Kho":
                    btnDanhMuc.Visible = true;
                    btnNghiepVu.Visible = true;
                    btnBaoCao.Visible = true;
                    break;
            }
            btnHeThong_Click(this, EventArgs.Empty);
        }

        private void BuildTreeView(string rootName)
        {
            treeChucNang.Nodes.Clear();
            TreeNode root = null;
            string userRole = _loggedInUser?.TenQuyen ?? "";

            switch (rootName)
            {
                case "Hệ Thống":
                    var heThongNodes = new List<TreeNode>();
                    heThongNodes.Add(new TreeNode("Dashboard (Trang chủ)") { Name = "sys.dashboard" });
                    if (userRole == "Administrator")
                    {
                        heThongNodes.Add(new TreeNode("Quản lý Hệ thống") { Name = "sys.quanlyhethong" });
                        heThongNodes.Add(new TreeNode("Cài đặt chung") { Name = "sys.caidat" });
                        heThongNodes.Add(new TreeNode("Kết nối CSDL") { Name = "sys.ketnoi" });
                    }
                    heThongNodes.Add(new TreeNode("Thông tin phần mềm") { Name = "sys.about" });
                    root = new TreeNode("Hệ Thống", heThongNodes.ToArray());
                    break;

                case "Danh Mục":
                    var danhMucNodes = new List<TreeNode>();
                    if (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kinh doanh" || userRole == "Nhân viên Kho")
                    {
                        danhMucNodes.Add(new TreeNode("Hàng hóa - Vật tư") { Name = "dm.hanghoa" });
                        danhMucNodes.Add(new TreeNode("Nhóm hàng") { Name = "dm.nhomhang" });
                    }
                    if (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kinh doanh")
                    {
                        danhMucNodes.Add(new TreeNode("Khách hàng") { Name = "dm.khachhang" });
                    }
                    if (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kho")
                    {
                        danhMucNodes.Add(new TreeNode("Nhà cung cấp") { Name = "dm.ncc" });
                    }
                    if (userRole == "Administrator")
                    {
                        danhMucNodes.Add(new TreeNode("Nhân viên") { Name = "dm.nhanvien" });
                    }
                    if (userRole == "Administrator" || userRole == "Kế toán")
                    {
                        danhMucNodes.Add(new TreeNode("Tài khoản Ngân hàng") { Name = "dm.tknh" });
                        danhMucNodes.Add(new TreeNode("Hệ thống TK Kế toán") { Name = "dm.kt" });
                    }
                    root = new TreeNode("Danh Mục", danhMucNodes.ToArray());
                    break;

                case "Nghiệp Vụ":
                    var nghiepVuNodes = new List<TreeNode>();
                    if (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kho")
                    {
                        nghiepVuNodes.Add(new TreeNode("Phiếu Nhập kho") { Name = "nv.nhapkho" });
                    }
                    if (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kinh doanh")
                    {
                        nghiepVuNodes.Add(new TreeNode("Phiếu Xuất kho") { Name = "nv.xuatkho" });
                        nghiepVuNodes.Add(new TreeNode("Báo giá") { Name = "nv.baogia" });
                    }
                    if (userRole == "Administrator" || userRole == "Kế toán")
                    {
                        nghiepVuNodes.Add(new TreeNode("Phiếu Thu tiền") { Name = "nv.phieuthu" });
                        nghiepVuNodes.Add(new TreeNode("Phiếu Chi tiền") { Name = "nv.phieuchi" });
                        nghiepVuNodes.Add(new TreeNode("Chấm công & Tạm ứng") { Name = "nv.chamcong" });
                    }
                    root = new TreeNode("Nghiệp Vụ", nghiepVuNodes.ToArray());
                    break;
                case "Báo Cáo":
                    var baoCaoNodes = new List<TreeNode>();
                    if (userRole == "Administrator" || userRole == "Kế toán" || userRole == "Nhân viên Kho")
                    {
                        baoCaoNodes.Add(new TreeNode("Báo cáo Kho", new[]{
                            new TreeNode("Báo cáo Nhập kho") { Name = "bc.nhapkho" },
                            new TreeNode("Báo cáo Xuất kho") { Name = "bc.xuatkho" },
                            new TreeNode("Tổng hợp Tồn kho") { Name = "bc.tonghopton" }
                        }));
                    }
                    if (userRole == "Administrator" || userRole == "Kế toán")
                    {
                        baoCaoNodes.Add(new TreeNode("Báo cáo Quỹ", new[] { new TreeNode("Sổ quỹ") { Name = "bc.quy" } }));
                        baoCaoNodes.Add(new TreeNode("Báo cáo Kế toán", new[] { new TreeNode("Sổ Nhật ký chung") { Name = "bc.nkc" }, new TreeNode("Sổ Chi tiết tài khoản") { Name = "bc.scttk" } }));
                        baoCaoNodes.Add(new TreeNode("Báo cáo Công nợ", new[] { new TreeNode("Sổ chi tiết công nợ") { Name = "bc.cn" } }));
                        baoCaoNodes.Add(new TreeNode("Báo cáo Nhân sự", new[] { new TreeNode("Bảng lương nhân viên") { Name = "bc.luong" } }));
                    }
                    root = new TreeNode("Báo Cáo", baoCaoNodes.ToArray());
                    break;
            }

            if (root != null && root.Nodes.Count > 0)
            {
                treeChucNang.Nodes.Add(root);
                root.ExpandAll();
            }
        }

        private void btnHeThong_Click(object sender, EventArgs e) => BuildTreeView("Hệ Thống");
        private void btnDanhMuc_Click(object sender, EventArgs e) => BuildTreeView("Danh Mục");
        private void btnNghiepVu_Click(object sender, EventArgs e) => BuildTreeView("Nghiệp Vụ");
        private void btnBaoCao_Click(object sender, EventArgs e) => BuildTreeView("Báo Cáo");

        private void mHoiDapAI_Click(object sender, EventArgs e) => ShowForm<FormHoiDap>(); private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (_loggedInUser == null) { MessageBox.Show("Vui lòng đăng nhập."); return; }
            var f = new FormDoiMatKhau(_loggedInUser.ID, _loggedInUser.HoTen);
            f.ShowDialog(this);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _currentForm?.Close();
                this.Hide();
                var loginForm = new FormDangNhap();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    this.SetLoggedInUser(loginForm.AuthenticatedUser);
                    this.Show();
                    ShowForm<FormDashboard>();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void treeChucNang_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                switch (e.Node.Name)
                {
                    case "sys.dashboard": ShowForm<FormDashboard>(); break;
                    case "sys.quanlyhethong": ShowForm<FormQuanLyHeThong>(); break;
                    case "sys.caidat": ShowForm<FormCaiDatHeThong>(); break;
                    case "sys.ketnoi": new FormKetNoiCSDL().ShowDialog(this); break;
                    case "sys.about": new FormThongTinPhanMem().ShowDialog(this); break;
                    case "dm.hanghoa": ShowForm<FormDanhMucHangHoa>(); break;
                    case "dm.nhomhang": ShowForm<FormNhomHang>(); break;
                    case "dm.khachhang": ShowForm<FormKhachHang>(); break;
                    case "dm.ncc": ShowForm<FormNhaCungCap>(); break;
                    case "dm.nhanvien": ShowForm<FormNhanVien>(); break;
                    case "dm.tknh": ShowForm<FormQuanLyTaiKhoanNganHang>(); break;
                    case "dm.kt": ShowForm<FormHeThongTaiKhoanKeToan>(); break;
                    case "nv.nhapkho": ShowForm<FormPhieuNhap>(); break;
                    case "nv.xuatkho": ShowForm<FormPhieuXuat>(); break;
                    case "nv.phieuthu": ShowForm<FormPhieuThu>(); break;
                    case "nv.phieuchi": ShowForm<FormPhieuChi>(); break;
                    case "nv.baogia": ShowForm<FormBangBaoGia>(); break;
                    case "nv.chamcong": ShowForm<FormTamUngChamCong>(); break;
                    case "bc.nhapkho": ShowBaoCaoKho("NHAP"); break;
                    case "bc.xuatkho": ShowBaoCaoKho("XUAT"); break;
                    case "bc.tonghopton": ShowForm<FormBaoCaoTonKho>(); break;
                    case "bc.quy": ShowForm<FormBaoCaoQuy>(); break;
                    case "bc.nkc": ShowForm<FormSoNhatKyChung>(); break;
                    case "bc.scttk": ShowForm<FormSoChiTietTaiKhoan>(); break;
                    case "bc.cn": ShowForm<FormReportCongNo>(); break;
                    case "bc.luong": ShowForm<FormTinhLuong>(); break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở chức năng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}