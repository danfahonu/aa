using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.GuiLayer;

namespace DoAnLapTrinhQuanLy
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Hiển thị user hiện tại + DB từ App.config
            stUser.Text = "User: " + (AppSession.CurrentUser ?? "(anonymous)");
            try
            {
                var c = ConfigurationManager.ConnectionStrings["Db"];
                if (c != null)
                {
                    var csb = new SqlConnectionStringBuilder(c.ConnectionString);
                    stDb.Text = $"DB: {csb.InitialCatalog} @ {csb.DataSource}";
                }
            }
            catch { stDb.Text = "DB: (unknown)"; }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Thoát ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        // ====== Utils: mở 1 instance duy nhất cho mỗi form ======
        private T OpenChild<T>() where T : Form, new()
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f is T) { f.Activate(); return (T)f; }
            }
            var child = new T();
            child.MdiParent = this;
            child.WindowState = FormWindowState.Maximized;
            child.Show();
            return child;
        }

        // ====== Hệ thống ======
        private void mDangNhapLai_Click(object sender, EventArgs e)
        {
            using (var login = new FormDangNhap())
            {
                if (login.ShowDialog(this) == DialogResult.OK)
                {
                    AppSession.CurrentUser = login.TenDangNhap;
                    stUser.Text = "User: " + AppSession.CurrentUser;
                }
            }
        }

        private void mDoiMatKhau_Click(object sender, EventArgs e)
        {
            using (var f = new FormDoiMatKhau(AppSession.CurrentUser))
                f.ShowDialog(this);
        }

        private void mAttachDb_Click(object sender, EventArgs e)
        {
            using (var f = new FormAttach())
                f.ShowDialog(this);
        }

        private void mThoat_Click(object sender, EventArgs e) => this.Close();

        // ====== Danh mục ======
        private void mKhachHang_Click(object sender, EventArgs e) => OpenChild<FormKhachHang>();
        private void mNhaCungCap_Click(object sender, EventArgs e) => OpenChild<FormNhaCungCap>();
        private void mNhomHang_Click(object sender, EventArgs e) => OpenChild<FormNhomHang>();
        private void mHangHoa_Click(object sender, EventArgs e) => OpenChild<FormDanhMucHangHoa>();
        //private void mDonViTinh_Click(object sender, EventArgs e) => OpenChild<FormDonViTinh>();
        private void mNganHang_Click(object sender, EventArgs e) => OpenChild<FormNganHang>();
        private void mTaiKhoanNH_NCC_Click(object sender, EventArgs e) => OpenChild<FormTaiKhoanNganHangDoiTac>();

        // ====== Nghiệp vụ ======
        private void mPhieuNhap_Click(object sender, EventArgs e) => OpenChild<FormPhieuNhap>();
        private void mPhieuXuat_Click(object sender, EventArgs e) => OpenChild<FormPhieuXuat>();
        private void mPhieuThu_Click(object sender, EventArgs e) => OpenChild<FormPhieuThu>();
        private void mPhieuChi_Click(object sender, EventArgs e) => OpenChild<FormPhieuChi>();

        // ====== Bảng giá ======
        private void mBangBaoGia_Click(object sender, EventArgs e) => OpenChild<FormBangBaoGia>();
    }
}
