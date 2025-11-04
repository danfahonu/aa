namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlNavigationRail = new System.Windows.Forms.Panel();
            this.pnlNavIndicator = new System.Windows.Forms.Panel();
            this.btnBaoCao = new System.Windows.Forms.Button();
            this.btnNghiepVu = new System.Windows.Forms.Button();
            this.btnDanhMuc = new System.Windows.Forms.Button();
            this.btnHeThong = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.pnlUserInfo = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.pnlSubMenu = new System.Windows.Forms.Panel();
            this.pnlBaoCaoSubMenu = new System.Windows.Forms.Panel();
            this.btnBaoCaoLuong = new System.Windows.Forms.Button();
            this.btnBaoCaoCongNo = new System.Windows.Forms.Button();
            this.btnBaoCaoSoChiTietTK = new System.Windows.Forms.Button();
            this.btnBaoCaoNhatKyChung = new System.Windows.Forms.Button();
            this.btnBaoCaoQuy = new System.Windows.Forms.Button();
            this.btnBaoCaoTonKho = new System.Windows.Forms.Button();
            this.btnBaoCaoXuatKho = new System.Windows.Forms.Button();
            this.btnBaoCaoNhapKho = new System.Windows.Forms.Button();
            this.pnlNghiepVuSubMenu = new System.Windows.Forms.Panel();
            this.btnChamCong = new System.Windows.Forms.Button();
            this.btnBaoGia = new System.Windows.Forms.Button();
            this.btnPhieuChi = new System.Windows.Forms.Button();
            this.btnPhieuThu = new System.Windows.Forms.Button();
            this.btnXuatKho = new System.Windows.Forms.Button();
            this.btnNhapKho = new System.Windows.Forms.Button();
            this.pnlDanhMucSubMenu = new System.Windows.Forms.Panel();
            this.btnHeThongTKKeToan = new System.Windows.Forms.Button();
            this.btnTaiKhoanNganHang = new System.Windows.Forms.Button();
            this.btnNhanVien = new System.Windows.Forms.Button();
            this.btnNhaCungCap = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.btnNhomHang = new System.Windows.Forms.Button();
            this.btnHangHoa = new System.Windows.Forms.Button();
            this.pnlHeThongSubMenu = new System.Windows.Forms.Panel();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnKetNoiCSDL = new System.Windows.Forms.Button();
            this.btnCaiDat = new System.Windows.Forms.Button();
            this.btnQuanLyHeThong = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.staUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.staDb = new System.Windows.Forms.ToolStripStatusLabel();
            this.staTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.pnlNavigationRail.SuspendLayout();
            this.pnlSubMenu.SuspendLayout();
            this.pnlBaoCaoSubMenu.SuspendLayout();
            this.pnlNghiepVuSubMenu.SuspendLayout();
            this.pnlDanhMucSubMenu.SuspendLayout();
            this.pnlHeThongSubMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNavigationRail
            // 
            this.pnlNavigationRail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(34)))), ((int)(((byte)(56)))));
            this.pnlNavigationRail.Controls.Add(this.pnlNavIndicator);
            this.pnlNavigationRail.Controls.Add(this.btnBaoCao);
            this.pnlNavigationRail.Controls.Add(this.btnNghiepVu);
            this.pnlNavigationRail.Controls.Add(this.btnDanhMuc);
            this.pnlNavigationRail.Controls.Add(this.btnHeThong);
            this.pnlNavigationRail.Controls.Add(this.btnDashboard);
            this.pnlNavigationRail.Controls.Add(this.pnlUserInfo);
            this.pnlNavigationRail.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNavigationRail.Location = new System.Drawing.Point(0, 0);
            this.pnlNavigationRail.Name = "pnlNavigationRail";
            this.pnlNavigationRail.Size = new System.Drawing.Size(75, 577);
            this.pnlNavigationRail.TabIndex = 0;
            // 
            // pnlNavIndicator
            // 
            this.pnlNavIndicator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(170)))), ((int)(((byte)(242)))));
            this.pnlNavIndicator.Location = new System.Drawing.Point(0, 100);
            this.pnlNavIndicator.Name = "pnlNavIndicator";
            this.pnlNavIndicator.Size = new System.Drawing.Size(3, 60);
            this.pnlNavIndicator.TabIndex = 0;
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCao.FlatAppearance.BorderSize = 0;
            this.btnBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCao.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnBaoCao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(170)))), ((int)(((byte)(242)))));
            this.btnBaoCao.Location = new System.Drawing.Point(0, 340);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(75, 60);
            this.btnBaoCao.TabIndex = 5;
            this.btnBaoCao.Text = "BC";
            this.btnBaoCao.UseVisualStyleBackColor = true;
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            this.btnBaoCao.Leave += new System.EventHandler(this.navButton_Leave);
            // 
            // btnNghiepVu
            // 
            this.btnNghiepVu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNghiepVu.FlatAppearance.BorderSize = 0;
            this.btnNghiepVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNghiepVu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnNghiepVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(170)))), ((int)(((byte)(242)))));
            this.btnNghiepVu.Location = new System.Drawing.Point(0, 280);
            this.btnNghiepVu.Name = "btnNghiepVu";
            this.btnNghiepVu.Size = new System.Drawing.Size(75, 60);
            this.btnNghiepVu.TabIndex = 4;
            this.btnNghiepVu.Text = "NV";
            this.btnNghiepVu.UseVisualStyleBackColor = true;
            this.btnNghiepVu.Click += new System.EventHandler(this.btnNghiepVu_Click);
            this.btnNghiepVu.Leave += new System.EventHandler(this.navButton_Leave);
            // 
            // btnDanhMuc
            // 
            this.btnDanhMuc.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDanhMuc.FlatAppearance.BorderSize = 0;
            this.btnDanhMuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDanhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(170)))), ((int)(((byte)(242)))));
            this.btnDanhMuc.Location = new System.Drawing.Point(0, 220);
            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnDanhMuc.Size = new System.Drawing.Size(75, 60);
            this.btnDanhMuc.TabIndex = 3;
            this.btnDanhMuc.Text = "DM";
            this.btnDanhMuc.UseVisualStyleBackColor = true;
            this.btnDanhMuc.Click += new System.EventHandler(this.btnDanhMuc_Click);
            this.btnDanhMuc.Leave += new System.EventHandler(this.navButton_Leave);
            // 
            // btnHeThong
            // 
            this.btnHeThong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHeThong.FlatAppearance.BorderSize = 0;
            this.btnHeThong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeThong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnHeThong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(170)))), ((int)(((byte)(242)))));
            this.btnHeThong.Location = new System.Drawing.Point(0, 160);
            this.btnHeThong.Name = "btnHeThong";
            this.btnHeThong.Size = new System.Drawing.Size(75, 60);
            this.btnHeThong.TabIndex = 2;
            this.btnHeThong.Text = "HT";
            this.btnHeThong.UseVisualStyleBackColor = true;
            this.btnHeThong.Click += new System.EventHandler(this.btnHeThong_Click);
            this.btnHeThong.Leave += new System.EventHandler(this.navButton_Leave);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(170)))), ((int)(((byte)(242)))));
            this.btnDashboard.Location = new System.Drawing.Point(0, 100);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(75, 60);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "DB";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            this.btnDashboard.Leave += new System.EventHandler(this.navButton_Leave);
            // 
            // pnlUserInfo
            // 
            this.pnlUserInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUserInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlUserInfo.Name = "pnlUserInfo";
            this.pnlUserInfo.Size = new System.Drawing.Size(75, 100);
            this.pnlUserInfo.TabIndex = 0;
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(255, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(703, 555);
            this.panelMain.TabIndex = 1;
            // 
            // pnlSubMenu
            // 
            this.pnlSubMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(34)))), ((int)(((byte)(56)))));
            this.pnlSubMenu.Controls.Add(this.pnlBaoCaoSubMenu);
            this.pnlSubMenu.Controls.Add(this.pnlNghiepVuSubMenu);
            this.pnlSubMenu.Controls.Add(this.pnlDanhMucSubMenu);
            this.pnlSubMenu.Controls.Add(this.pnlHeThongSubMenu);
            this.pnlSubMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSubMenu.Location = new System.Drawing.Point(75, 0);
            this.pnlSubMenu.Name = "pnlSubMenu";
            this.pnlSubMenu.Size = new System.Drawing.Size(180, 577);
            this.pnlSubMenu.TabIndex = 2;
            // 
            // pnlBaoCaoSubMenu
            // 
            this.pnlBaoCaoSubMenu.Controls.Add(this.btnBaoCaoLuong);
            this.pnlBaoCaoSubMenu.Controls.Add(this.btnBaoCaoCongNo);
            this.pnlBaoCaoSubMenu.Controls.Add(this.btnBaoCaoSoChiTietTK);
            this.pnlBaoCaoSubMenu.Controls.Add(this.btnBaoCaoNhatKyChung);
            this.pnlBaoCaoSubMenu.Controls.Add(this.btnBaoCaoQuy);
            this.pnlBaoCaoSubMenu.Controls.Add(this.btnBaoCaoTonKho);
            this.pnlBaoCaoSubMenu.Controls.Add(this.btnBaoCaoXuatKho);
            this.pnlBaoCaoSubMenu.Controls.Add(this.btnBaoCaoNhapKho);
            this.pnlBaoCaoSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBaoCaoSubMenu.Location = new System.Drawing.Point(0, 680);
            this.pnlBaoCaoSubMenu.Name = "pnlBaoCaoSubMenu";
            this.pnlBaoCaoSubMenu.Size = new System.Drawing.Size(180, 320);
            this.pnlBaoCaoSubMenu.TabIndex = 3;
            // 
            // btnBaoCaoLuong
            // 
            this.btnBaoCaoLuong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCaoLuong.FlatAppearance.BorderSize = 0;
            this.btnBaoCaoLuong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCaoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBaoCaoLuong.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBaoCaoLuong.Location = new System.Drawing.Point(0, 280);
            this.btnBaoCaoLuong.Name = "btnBaoCaoLuong";
            this.btnBaoCaoLuong.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBaoCaoLuong.Size = new System.Drawing.Size(180, 40);
            this.btnBaoCaoLuong.TabIndex = 7;
            this.btnBaoCaoLuong.Text = "Báo cáo Lương";
            this.btnBaoCaoLuong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCaoLuong.UseVisualStyleBackColor = true;
            this.btnBaoCaoLuong.Click += new System.EventHandler(this.btnBaoCaoLuong_Click);
            // 
            // btnBaoCaoCongNo
            // 
            this.btnBaoCaoCongNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCaoCongNo.FlatAppearance.BorderSize = 0;
            this.btnBaoCaoCongNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCaoCongNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBaoCaoCongNo.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBaoCaoCongNo.Location = new System.Drawing.Point(0, 240);
            this.btnBaoCaoCongNo.Name = "btnBaoCaoCongNo";
            this.btnBaoCaoCongNo.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBaoCaoCongNo.Size = new System.Drawing.Size(180, 40);
            this.btnBaoCaoCongNo.TabIndex = 6;
            this.btnBaoCaoCongNo.Text = "Báo cáo Công nợ";
            this.btnBaoCaoCongNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCaoCongNo.UseVisualStyleBackColor = true;
            this.btnBaoCaoCongNo.Click += new System.EventHandler(this.btnBaoCaoCongNo_Click);
            // 
            // btnBaoCaoSoChiTietTK
            // 
            this.btnBaoCaoSoChiTietTK.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCaoSoChiTietTK.FlatAppearance.BorderSize = 0;
            this.btnBaoCaoSoChiTietTK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCaoSoChiTietTK.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBaoCaoSoChiTietTK.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBaoCaoSoChiTietTK.Location = new System.Drawing.Point(0, 200);
            this.btnBaoCaoSoChiTietTK.Name = "btnBaoCaoSoChiTietTK";
            this.btnBaoCaoSoChiTietTK.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBaoCaoSoChiTietTK.Size = new System.Drawing.Size(180, 40);
            this.btnBaoCaoSoChiTietTK.TabIndex = 5;
            this.btnBaoCaoSoChiTietTK.Text = "Sổ chi tiết tài khoản";
            this.btnBaoCaoSoChiTietTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCaoSoChiTietTK.UseVisualStyleBackColor = true;
            this.btnBaoCaoSoChiTietTK.Click += new System.EventHandler(this.btnBaoCaoSoChiTietTK_Click);
            // 
            // btnBaoCaoNhatKyChung
            // 
            this.btnBaoCaoNhatKyChung.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCaoNhatKyChung.FlatAppearance.BorderSize = 0;
            this.btnBaoCaoNhatKyChung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCaoNhatKyChung.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBaoCaoNhatKyChung.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBaoCaoNhatKyChung.Location = new System.Drawing.Point(0, 160);
            this.btnBaoCaoNhatKyChung.Name = "btnBaoCaoNhatKyChung";
            this.btnBaoCaoNhatKyChung.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBaoCaoNhatKyChung.Size = new System.Drawing.Size(180, 40);
            this.btnBaoCaoNhatKyChung.TabIndex = 4;
            this.btnBaoCaoNhatKyChung.Text = "Sổ nhật ký chung";
            this.btnBaoCaoNhatKyChung.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCaoNhatKyChung.UseVisualStyleBackColor = true;
            this.btnBaoCaoNhatKyChung.Click += new System.EventHandler(this.btnBaoCaoNhatKyChung_Click);
            // 
            // btnBaoCaoQuy
            // 
            this.btnBaoCaoQuy.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCaoQuy.FlatAppearance.BorderSize = 0;
            this.btnBaoCaoQuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCaoQuy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBaoCaoQuy.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBaoCaoQuy.Location = new System.Drawing.Point(0, 120);
            this.btnBaoCaoQuy.Name = "btnBaoCaoQuy";
            this.btnBaoCaoQuy.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBaoCaoQuy.Size = new System.Drawing.Size(180, 40);
            this.btnBaoCaoQuy.TabIndex = 3;
            this.btnBaoCaoQuy.Text = "Báo cáo Quỹ";
            this.btnBaoCaoQuy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCaoQuy.UseVisualStyleBackColor = true;
            this.btnBaoCaoQuy.Click += new System.EventHandler(this.btnBaoCaoQuy_Click);
            // 
            // btnBaoCaoTonKho
            // 
            this.btnBaoCaoTonKho.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCaoTonKho.FlatAppearance.BorderSize = 0;
            this.btnBaoCaoTonKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCaoTonKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBaoCaoTonKho.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBaoCaoTonKho.Location = new System.Drawing.Point(0, 80);
            this.btnBaoCaoTonKho.Name = "btnBaoCaoTonKho";
            this.btnBaoCaoTonKho.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBaoCaoTonKho.Size = new System.Drawing.Size(180, 40);
            this.btnBaoCaoTonKho.TabIndex = 2;
            this.btnBaoCaoTonKho.Text = "Tổng hợp tồn kho";
            this.btnBaoCaoTonKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCaoTonKho.UseVisualStyleBackColor = true;
            this.btnBaoCaoTonKho.Click += new System.EventHandler(this.btnBaoCaoTonKho_Click);
            // 
            // btnBaoCaoXuatKho
            // 
            this.btnBaoCaoXuatKho.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCaoXuatKho.FlatAppearance.BorderSize = 0;
            this.btnBaoCaoXuatKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCaoXuatKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBaoCaoXuatKho.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBaoCaoXuatKho.Location = new System.Drawing.Point(0, 40);
            this.btnBaoCaoXuatKho.Name = "btnBaoCaoXuatKho";
            this.btnBaoCaoXuatKho.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBaoCaoXuatKho.Size = new System.Drawing.Size(180, 40);
            this.btnBaoCaoXuatKho.TabIndex = 1;
            this.btnBaoCaoXuatKho.Text = "Báo cáo Xuất kho";
            this.btnBaoCaoXuatKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCaoXuatKho.UseVisualStyleBackColor = true;
            this.btnBaoCaoXuatKho.Click += new System.EventHandler(this.btnBaoCaoXuatKho_Click);
            // 
            // btnBaoCaoNhapKho
            // 
            this.btnBaoCaoNhapKho.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCaoNhapKho.FlatAppearance.BorderSize = 0;
            this.btnBaoCaoNhapKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCaoNhapKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBaoCaoNhapKho.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBaoCaoNhapKho.Location = new System.Drawing.Point(0, 0);
            this.btnBaoCaoNhapKho.Name = "btnBaoCaoNhapKho";
            this.btnBaoCaoNhapKho.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBaoCaoNhapKho.Size = new System.Drawing.Size(180, 40);
            this.btnBaoCaoNhapKho.TabIndex = 0;
            this.btnBaoCaoNhapKho.Text = "Báo cáo Nhập kho";
            this.btnBaoCaoNhapKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCaoNhapKho.UseVisualStyleBackColor = true;
            this.btnBaoCaoNhapKho.Click += new System.EventHandler(this.btnBaoCaoNhapKho_Click);
            // 
            // pnlNghiepVuSubMenu
            // 
            this.pnlNghiepVuSubMenu.Controls.Add(this.btnChamCong);
            this.pnlNghiepVuSubMenu.Controls.Add(this.btnBaoGia);
            this.pnlNghiepVuSubMenu.Controls.Add(this.btnPhieuChi);
            this.pnlNghiepVuSubMenu.Controls.Add(this.btnPhieuThu);
            this.pnlNghiepVuSubMenu.Controls.Add(this.btnXuatKho);
            this.pnlNghiepVuSubMenu.Controls.Add(this.btnNhapKho);
            this.pnlNghiepVuSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNghiepVuSubMenu.Location = new System.Drawing.Point(0, 440);
            this.pnlNghiepVuSubMenu.Name = "pnlNghiepVuSubMenu";
            this.pnlNghiepVuSubMenu.Size = new System.Drawing.Size(180, 240);
            this.pnlNghiepVuSubMenu.TabIndex = 2;
            // 
            // btnChamCong
            // 
            this.btnChamCong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnChamCong.FlatAppearance.BorderSize = 0;
            this.btnChamCong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChamCong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnChamCong.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnChamCong.Location = new System.Drawing.Point(0, 200);
            this.btnChamCong.Name = "btnChamCong";
            this.btnChamCong.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnChamCong.Size = new System.Drawing.Size(180, 40);
            this.btnChamCong.TabIndex = 5;
            this.btnChamCong.Text = "Chấm công";
            this.btnChamCong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChamCong.UseVisualStyleBackColor = true;
            this.btnChamCong.Click += new System.EventHandler(this.btnChamCong_Click);
            // 
            // btnBaoGia
            // 
            this.btnBaoGia.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoGia.FlatAppearance.BorderSize = 0;
            this.btnBaoGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBaoGia.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBaoGia.Location = new System.Drawing.Point(0, 160);
            this.btnBaoGia.Name = "btnBaoGia";
            this.btnBaoGia.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBaoGia.Size = new System.Drawing.Size(180, 40);
            this.btnBaoGia.TabIndex = 4;
            this.btnBaoGia.Text = "Báo giá";
            this.btnBaoGia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoGia.UseVisualStyleBackColor = true;
            this.btnBaoGia.Click += new System.EventHandler(this.btnBaoGia_Click);
            // 
            // btnPhieuChi
            // 
            this.btnPhieuChi.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPhieuChi.FlatAppearance.BorderSize = 0;
            this.btnPhieuChi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhieuChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPhieuChi.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnPhieuChi.Location = new System.Drawing.Point(0, 120);
            this.btnPhieuChi.Name = "btnPhieuChi";
            this.btnPhieuChi.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnPhieuChi.Size = new System.Drawing.Size(180, 40);
            this.btnPhieuChi.TabIndex = 3;
            this.btnPhieuChi.Text = "Phiếu chi";
            this.btnPhieuChi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPhieuChi.UseVisualStyleBackColor = true;
            this.btnPhieuChi.Click += new System.EventHandler(this.btnPhieuChi_Click);
            // 
            // btnPhieuThu
            // 
            this.btnPhieuThu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPhieuThu.FlatAppearance.BorderSize = 0;
            this.btnPhieuThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhieuThu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPhieuThu.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnPhieuThu.Location = new System.Drawing.Point(0, 80);
            this.btnPhieuThu.Name = "btnPhieuThu";
            this.btnPhieuThu.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnPhieuThu.Size = new System.Drawing.Size(180, 40);
            this.btnPhieuThu.TabIndex = 2;
            this.btnPhieuThu.Text = "Phiếu thu";
            this.btnPhieuThu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPhieuThu.UseVisualStyleBackColor = true;
            this.btnPhieuThu.Click += new System.EventHandler(this.btnPhieuThu_Click);
            // 
            // btnXuatKho
            // 
            this.btnXuatKho.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnXuatKho.FlatAppearance.BorderSize = 0;
            this.btnXuatKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXuatKho.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnXuatKho.Location = new System.Drawing.Point(0, 40);
            this.btnXuatKho.Name = "btnXuatKho";
            this.btnXuatKho.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnXuatKho.Size = new System.Drawing.Size(180, 40);
            this.btnXuatKho.TabIndex = 1;
            this.btnXuatKho.Text = "Phiếu Xuất kho";
            this.btnXuatKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXuatKho.UseVisualStyleBackColor = true;
            this.btnXuatKho.Click += new System.EventHandler(this.btnXuatKho_Click);
            // 
            // btnNhapKho
            // 
            this.btnNhapKho.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNhapKho.FlatAppearance.BorderSize = 0;
            this.btnNhapKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhapKho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNhapKho.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnNhapKho.Location = new System.Drawing.Point(0, 0);
            this.btnNhapKho.Name = "btnNhapKho";
            this.btnNhapKho.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNhapKho.Size = new System.Drawing.Size(180, 40);
            this.btnNhapKho.TabIndex = 0;
            this.btnNhapKho.Text = "Phiếu Nhập kho";
            this.btnNhapKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhapKho.UseVisualStyleBackColor = true;
            this.btnNhapKho.Click += new System.EventHandler(this.btnNhapKho_Click);
            // 
            // pnlDanhMucSubMenu
            // 
            this.pnlDanhMucSubMenu.Controls.Add(this.btnHeThongTKKeToan);
            this.pnlDanhMucSubMenu.Controls.Add(this.btnTaiKhoanNganHang);
            this.pnlDanhMucSubMenu.Controls.Add(this.btnNhanVien);
            this.pnlDanhMucSubMenu.Controls.Add(this.btnNhaCungCap);
            this.pnlDanhMucSubMenu.Controls.Add(this.btnKhachHang);
            this.pnlDanhMucSubMenu.Controls.Add(this.btnNhomHang);
            this.pnlDanhMucSubMenu.Controls.Add(this.btnHangHoa);
            this.pnlDanhMucSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDanhMucSubMenu.Location = new System.Drawing.Point(0, 160);
            this.pnlDanhMucSubMenu.Name = "pnlDanhMucSubMenu";
            this.pnlDanhMucSubMenu.Size = new System.Drawing.Size(180, 280);
            this.pnlDanhMucSubMenu.TabIndex = 1;
            // 
            // btnHeThongTKKeToan
            // 
            this.btnHeThongTKKeToan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHeThongTKKeToan.FlatAppearance.BorderSize = 0;
            this.btnHeThongTKKeToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeThongTKKeToan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHeThongTKKeToan.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnHeThongTKKeToan.Location = new System.Drawing.Point(0, 240);
            this.btnHeThongTKKeToan.Name = "btnHeThongTKKeToan";
            this.btnHeThongTKKeToan.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnHeThongTKKeToan.Size = new System.Drawing.Size(180, 40);
            this.btnHeThongTKKeToan.TabIndex = 6;
            this.btnHeThongTKKeToan.Text = "Hệ thống TK Kế toán";
            this.btnHeThongTKKeToan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHeThongTKKeToan.UseVisualStyleBackColor = true;
            this.btnHeThongTKKeToan.Click += new System.EventHandler(this.btnHeThongTKKeToan_Click);
            // 
            // btnTaiKhoanNganHang
            // 
            this.btnTaiKhoanNganHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTaiKhoanNganHang.FlatAppearance.BorderSize = 0;
            this.btnTaiKhoanNganHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiKhoanNganHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTaiKhoanNganHang.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnTaiKhoanNganHang.Location = new System.Drawing.Point(0, 200);
            this.btnTaiKhoanNganHang.Name = "btnTaiKhoanNganHang";
            this.btnTaiKhoanNganHang.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnTaiKhoanNganHang.Size = new System.Drawing.Size(180, 40);
            this.btnTaiKhoanNganHang.TabIndex = 5;
            this.btnTaiKhoanNganHang.Text = "Tài khoản Ngân hàng";
            this.btnTaiKhoanNganHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaiKhoanNganHang.UseVisualStyleBackColor = true;
            this.btnTaiKhoanNganHang.Click += new System.EventHandler(this.btnTaiKhoanNganHang_Click);
            // 
            // btnNhanVien
            // 
            this.btnNhanVien.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNhanVien.FlatAppearance.BorderSize = 0;
            this.btnNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNhanVien.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnNhanVien.Location = new System.Drawing.Point(0, 160);
            this.btnNhanVien.Name = "btnNhanVien";
            this.btnNhanVien.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNhanVien.Size = new System.Drawing.Size(180, 40);
            this.btnNhanVien.TabIndex = 4;
            this.btnNhanVien.Text = "Nhân viên";
            this.btnNhanVien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhanVien.UseVisualStyleBackColor = true;
            this.btnNhanVien.Click += new System.EventHandler(this.btnNhanVien_Click);
            // 
            // btnNhaCungCap
            // 
            this.btnNhaCungCap.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNhaCungCap.FlatAppearance.BorderSize = 0;
            this.btnNhaCungCap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNhaCungCap.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnNhaCungCap.Location = new System.Drawing.Point(0, 120);
            this.btnNhaCungCap.Name = "btnNhaCungCap";
            this.btnNhaCungCap.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNhaCungCap.Size = new System.Drawing.Size(180, 40);
            this.btnNhaCungCap.TabIndex = 3;
            this.btnNhaCungCap.Text = "Nhà cung cấp";
            this.btnNhaCungCap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhaCungCap.UseVisualStyleBackColor = true;
            this.btnNhaCungCap.Click += new System.EventHandler(this.btnNhaCungCap_Click);
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnKhachHang.FlatAppearance.BorderSize = 0;
            this.btnKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKhachHang.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnKhachHang.Location = new System.Drawing.Point(0, 80);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnKhachHang.Size = new System.Drawing.Size(180, 40);
            this.btnKhachHang.TabIndex = 2;
            this.btnKhachHang.Text = "Khách hàng";
            this.btnKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKhachHang.UseVisualStyleBackColor = true;
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            // 
            // btnNhomHang
            // 
            this.btnNhomHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNhomHang.FlatAppearance.BorderSize = 0;
            this.btnNhomHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhomHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNhomHang.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnNhomHang.Location = new System.Drawing.Point(0, 40);
            this.btnNhomHang.Name = "btnNhomHang";
            this.btnNhomHang.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNhomHang.Size = new System.Drawing.Size(180, 40);
            this.btnNhomHang.TabIndex = 1;
            this.btnNhomHang.Text = "Nhóm hàng";
            this.btnNhomHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhomHang.UseVisualStyleBackColor = true;
            this.btnNhomHang.Click += new System.EventHandler(this.btnNhomHang_Click);
            // 
            // btnHangHoa
            // 
            this.btnHangHoa.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHangHoa.FlatAppearance.BorderSize = 0;
            this.btnHangHoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHangHoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHangHoa.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnHangHoa.Location = new System.Drawing.Point(0, 0);
            this.btnHangHoa.Name = "btnHangHoa";
            this.btnHangHoa.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnHangHoa.Size = new System.Drawing.Size(180, 40);
            this.btnHangHoa.TabIndex = 0;
            this.btnHangHoa.Text = "Hàng hóa - Vật tư";
            this.btnHangHoa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHangHoa.UseVisualStyleBackColor = true;
            this.btnHangHoa.Click += new System.EventHandler(this.btnHangHoa_Click);
            // 
            // pnlHeThongSubMenu
            // 
            this.pnlHeThongSubMenu.Controls.Add(this.btnAbout);
            this.pnlHeThongSubMenu.Controls.Add(this.btnKetNoiCSDL);
            this.pnlHeThongSubMenu.Controls.Add(this.btnCaiDat);
            this.pnlHeThongSubMenu.Controls.Add(this.btnQuanLyHeThong);
            this.pnlHeThongSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeThongSubMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlHeThongSubMenu.Name = "pnlHeThongSubMenu";
            this.pnlHeThongSubMenu.Size = new System.Drawing.Size(180, 160);
            this.pnlHeThongSubMenu.TabIndex = 0;
            // 
            // btnAbout
            // 
            this.btnAbout.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAbout.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnAbout.Location = new System.Drawing.Point(0, 120);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnAbout.Size = new System.Drawing.Size(180, 40);
            this.btnAbout.TabIndex = 3;
            this.btnAbout.Text = "Thông tin phần mềm";
            this.btnAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnKetNoiCSDL
            // 
            this.btnKetNoiCSDL.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnKetNoiCSDL.FlatAppearance.BorderSize = 0;
            this.btnKetNoiCSDL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKetNoiCSDL.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKetNoiCSDL.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnKetNoiCSDL.Location = new System.Drawing.Point(0, 80);
            this.btnKetNoiCSDL.Name = "btnKetNoiCSDL";
            this.btnKetNoiCSDL.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnKetNoiCSDL.Size = new System.Drawing.Size(180, 40);
            this.btnKetNoiCSDL.TabIndex = 2;
            this.btnKetNoiCSDL.Text = "Kết nối CSDL";
            this.btnKetNoiCSDL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKetNoiCSDL.UseVisualStyleBackColor = true;
            this.btnKetNoiCSDL.Click += new System.EventHandler(this.btnKetNoiCSDL_Click);
            // 
            // btnCaiDat
            // 
            this.btnCaiDat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCaiDat.FlatAppearance.BorderSize = 0;
            this.btnCaiDat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaiDat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCaiDat.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnCaiDat.Location = new System.Drawing.Point(0, 40);
            this.btnCaiDat.Name = "btnCaiDat";
            this.btnCaiDat.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnCaiDat.Size = new System.Drawing.Size(180, 40);
            this.btnCaiDat.TabIndex = 1;
            this.btnCaiDat.Text = "Cài đặt chung";
            this.btnCaiDat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCaiDat.UseVisualStyleBackColor = true;
            this.btnCaiDat.Click += new System.EventHandler(this.btnCaiDat_Click);
            // 
            // btnQuanLyHeThong
            // 
            this.btnQuanLyHeThong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnQuanLyHeThong.FlatAppearance.BorderSize = 0;
            this.btnQuanLyHeThong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuanLyHeThong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnQuanLyHeThong.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnQuanLyHeThong.Location = new System.Drawing.Point(0, 0);
            this.btnQuanLyHeThong.Name = "btnQuanLyHeThong";
            this.btnQuanLyHeThong.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnQuanLyHeThong.Size = new System.Drawing.Size(180, 40);
            this.btnQuanLyHeThong.TabIndex = 0;
            this.btnQuanLyHeThong.Text = "Quản lý Hệ thống";
            this.btnQuanLyHeThong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuanLyHeThong.UseVisualStyleBackColor = true;
            this.btnQuanLyHeThong.Click += new System.EventHandler(this.btnQuanLyHeThong_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(34)))), ((int)(((byte)(56)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staUser,
            this.staDb,
            this.staTime});
            this.statusStrip1.Location = new System.Drawing.Point(75, 555);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(883, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // staUser
            // 
            this.staUser.ForeColor = System.Drawing.Color.Gainsboro;
            this.staUser.Name = "staUser";
            this.staUser.Size = new System.Drawing.Size(38, 17);
            this.staUser.Text = "User: ";
            // 
            // staDb
            // 
            this.staDb.ForeColor = System.Drawing.Color.Gainsboro;
            this.staDb.Name = "staDb";
            this.staDb.Size = new System.Drawing.Size(31, 17);
            this.staDb.Text = "DB: ";
            // 
            // staTime
            // 
            this.staTime.ForeColor = System.Drawing.Color.Gainsboro;
            this.staTime.Name = "staTime";
            this.staTime.Size = new System.Drawing.Size(34, 17);
            this.staTime.Text = "Time";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(22)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(958, 577);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.pnlSubMenu);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlNavigationRail);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phần Mềm Quản Lý Bán Hàng";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.pnlNavigationRail.ResumeLayout(false);
            this.pnlSubMenu.ResumeLayout(false);
            this.pnlBaoCaoSubMenu.ResumeLayout(false);
            this.pnlNghiepVuSubMenu.ResumeLayout(false);
            this.pnlDanhMucSubMenu.ResumeLayout(false);
            this.pnlHeThongSubMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlNavigationRail;
        private System.Windows.Forms.Panel pnlUserInfo;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnHeThong;
        private System.Windows.Forms.Button btnBaoCao;
        private System.Windows.Forms.Button btnNghiepVu;
        private System.Windows.Forms.Button btnDanhMuc;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel pnlSubMenu;
        private System.Windows.Forms.Panel pnlHeThongSubMenu;
        private System.Windows.Forms.Button btnQuanLyHeThong;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnKetNoiCSDL;
        private System.Windows.Forms.Button btnCaiDat;
        private System.Windows.Forms.Panel pnlDanhMucSubMenu;
        private System.Windows.Forms.Button btnHeThongTKKeToan;
        private System.Windows.Forms.Button btnTaiKhoanNganHang;
        private System.Windows.Forms.Button btnNhanVien;
        private System.Windows.Forms.Button btnNhaCungCap;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnNhomHang;
        private System.Windows.Forms.Button btnHangHoa;
        private System.Windows.Forms.Panel pnlNghiepVuSubMenu;
        private System.Windows.Forms.Button btnChamCong;
        private System.Windows.Forms.Button btnBaoGia;
        private System.Windows.Forms.Button btnPhieuChi;
        private System.Windows.Forms.Button btnPhieuThu;
        private System.Windows.Forms.Button btnXuatKho;
        private System.Windows.Forms.Button btnNhapKho;
        private System.Windows.Forms.Panel pnlBaoCaoSubMenu;
        private System.Windows.Forms.Button btnBaoCaoLuong;
        private System.Windows.Forms.Button btnBaoCaoCongNo;
        private System.Windows.Forms.Button btnBaoCaoSoChiTietTK;
        private System.Windows.Forms.Button btnBaoCaoNhatKyChung;
        private System.Windows.Forms.Button btnBaoCaoQuy;
        private System.Windows.Forms.Button btnBaoCaoTonKho;
        private System.Windows.Forms.Button btnBaoCaoXuatKho;
        private System.Windows.Forms.Button btnBaoCaoNhapKho;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel staUser;
        private System.Windows.Forms.ToolStripStatusLabel staDb;
        private System.Windows.Forms.ToolStripStatusLabel staTime;
        private System.Windows.Forms.Panel pnlNavIndicator;
        private System.Windows.Forms.ToolTip toolTipInfo;
    }
}