namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormQuanLyHeThong
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabNguoiDung = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpNguoiDungList = new System.Windows.Forms.GroupBox();
            this.gridNguoiDung = new System.Windows.Forms.DataGridView();
            this.panelNguoiDungDetail = new System.Windows.Forms.Panel();
            this.grpNguoiDungDetail = new System.Windows.Forms.GroupBox();
            this.chkHoatDong = new System.Windows.Forms.CheckBox();
            this.cboQuyenHan_User = new System.Windows.Forms.ComboBox();
            this.lblQuyenHan_User = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtTaiKhoan = new System.Windows.Forms.TextBox();
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.panelNguoiDungButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNapUser = new System.Windows.Forms.Button();
            this.btnXoaUser = new System.Windows.Forms.Button();
            this.btnLuuUser = new System.Windows.Forms.Button();
            this.btnSuaUser = new System.Windows.Forms.Button();
            this.btnMoiUser = new System.Windows.Forms.Button();
            this.tabQuyenHan = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grpQuyenHanList = new System.Windows.Forms.GroupBox();
            this.gridQuyenHan = new System.Windows.Forms.DataGridView();
            this.panelQuyenHanDetail = new System.Windows.Forms.Panel();
            this.grpQuyenHanDetail = new System.Windows.Forms.GroupBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtTenQuyen = new System.Windows.Forms.TextBox();
            this.lblTenQuyen = new System.Windows.Forms.Label();
            this.txtMaQuyen = new System.Windows.Forms.TextBox();
            this.lblMaQuyen = new System.Windows.Forms.Label();
            this.panelQuyenHanButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNapRole = new System.Windows.Forms.Button();
            this.btnXoaRole = new System.Windows.Forms.Button();
            this.btnLuuRole = new System.Windows.Forms.Button();
            this.btnSuaRole = new System.Windows.Forms.Button();
            this.btnMoiRole = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabNguoiDung.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpNguoiDungList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNguoiDung)).BeginInit();
            this.panelNguoiDungDetail.SuspendLayout();
            this.grpNguoiDungDetail.SuspendLayout();
            this.panelNguoiDungButtons.SuspendLayout();
            this.tabQuyenHan.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpQuyenHanList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridQuyenHan)).BeginInit();
            this.panelQuyenHanDetail.SuspendLayout();
            this.grpQuyenHanDetail.SuspendLayout();
            this.panelQuyenHanButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabNguoiDung);
            this.tabControlMain.Controls.Add(this.tabQuyenHan);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1008, 601);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabNguoiDung
            // 
            this.tabNguoiDung.Controls.Add(this.tableLayoutPanel1);
            this.tabNguoiDung.Location = new System.Drawing.Point(4, 24);
            this.tabNguoiDung.Name = "tabNguoiDung";
            this.tabNguoiDung.Padding = new System.Windows.Forms.Padding(3);
            this.tabNguoiDung.Size = new System.Drawing.Size(1000, 573);
            this.tabNguoiDung.TabIndex = 0;
            this.tabNguoiDung.Text = "Quản lý Người dùng";
            this.tabNguoiDung.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Controls.Add(this.grpNguoiDungList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelNguoiDungDetail, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(994, 567);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // grpNguoiDungList
            // 
            this.grpNguoiDungList.Controls.Add(this.gridNguoiDung);
            this.grpNguoiDungList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpNguoiDungList.Location = new System.Drawing.Point(8, 8);
            this.grpNguoiDungList.Name = "grpNguoiDungList";
            this.grpNguoiDungList.Size = new System.Drawing.Size(532, 551);
            this.grpNguoiDungList.TabIndex = 0;
            this.grpNguoiDungList.TabStop = false;
            this.grpNguoiDungList.Text = "Danh sách Người dùng";
            // 
            // gridNguoiDung
            // 
            this.gridNguoiDung.AllowUserToAddRows = false;
            this.gridNguoiDung.AllowUserToDeleteRows = false;
            this.gridNguoiDung.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridNguoiDung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridNguoiDung.Location = new System.Drawing.Point(3, 19);
            this.gridNguoiDung.MultiSelect = false;
            this.gridNguoiDung.Name = "gridNguoiDung";
            this.gridNguoiDung.ReadOnly = true;
            this.gridNguoiDung.RowTemplate.Height = 25;
            this.gridNguoiDung.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridNguoiDung.Size = new System.Drawing.Size(526, 529);
            this.gridNguoiDung.TabIndex = 0;
            // 
            // panelNguoiDungDetail
            // 
            this.panelNguoiDungDetail.Controls.Add(this.grpNguoiDungDetail);
            this.panelNguoiDungDetail.Controls.Add(this.panelNguoiDungButtons);
            this.panelNguoiDungDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNguoiDungDetail.Location = new System.Drawing.Point(546, 8);
            this.panelNguoiDungDetail.Name = "panelNguoiDungDetail";
            this.panelNguoiDungDetail.Size = new System.Drawing.Size(440, 551);
            this.panelNguoiDungDetail.TabIndex = 1;
            // 
            // grpNguoiDungDetail
            // 
            this.grpNguoiDungDetail.Controls.Add(this.chkHoatDong);
            this.grpNguoiDungDetail.Controls.Add(this.cboQuyenHan_User);
            this.grpNguoiDungDetail.Controls.Add(this.lblQuyenHan_User);
            this.grpNguoiDungDetail.Controls.Add(this.txtHoTen);
            this.grpNguoiDungDetail.Controls.Add(this.lblHoTen);
            this.grpNguoiDungDetail.Controls.Add(this.txtTaiKhoan);
            this.grpNguoiDungDetail.Controls.Add(this.lblTaiKhoan);
            this.grpNguoiDungDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpNguoiDungDetail.Location = new System.Drawing.Point(0, 0);
            this.grpNguoiDungDetail.Name = "grpNguoiDungDetail";
            this.grpNguoiDungDetail.Size = new System.Drawing.Size(440, 501);
            this.grpNguoiDungDetail.TabIndex = 1;
            this.grpNguoiDungDetail.TabStop = false;
            this.grpNguoiDungDetail.Text = "Chi tiết";
            // 
            // chkHoatDong
            // 
            this.chkHoatDong.AutoSize = true;
            this.chkHoatDong.Location = new System.Drawing.Point(105, 151);
            this.chkHoatDong.Name = "chkHoatDong";
            this.chkHoatDong.Size = new System.Drawing.Size(83, 19);
            this.chkHoatDong.TabIndex = 6;
            this.chkHoatDong.Text = "Hoạt động";
            this.chkHoatDong.UseVisualStyleBackColor = true;
            // 
            // cboQuyenHan_User
            // 
            this.cboQuyenHan_User.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboQuyenHan_User.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQuyenHan_User.FormattingEnabled = true;
            this.cboQuyenHan_User.Location = new System.Drawing.Point(105, 110);
            this.cboQuyenHan_User.Name = "cboQuyenHan_User";
            this.cboQuyenHan_User.Size = new System.Drawing.Size(318, 23);
            this.cboQuyenHan_User.TabIndex = 5;
            // 
            // lblQuyenHan_User
            // 
            this.lblQuyenHan_User.AutoSize = true;
            this.lblQuyenHan_User.Location = new System.Drawing.Point(17, 113);
            this.lblQuyenHan_User.Name = "lblQuyenHan_User";
            this.lblQuyenHan_User.Size = new System.Drawing.Size(68, 15);
            this.lblQuyenHan_User.TabIndex = 4;
            this.lblQuyenHan_User.Text = "Quyền hạn:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHoTen.Location = new System.Drawing.Point(105, 71);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(318, 23);
            this.txtHoTen.TabIndex = 3;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(17, 74);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(46, 15);
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "Họ tên:";
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTaiKhoan.Location = new System.Drawing.Point(105, 33);
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.Size = new System.Drawing.Size(318, 23);
            this.txtTaiKhoan.TabIndex = 1;
            // 
            // lblTaiKhoan
            // 
            this.lblTaiKhoan.AutoSize = true;
            this.lblTaiKhoan.Location = new System.Drawing.Point(17, 36);
            this.lblTaiKhoan.Name = "lblTaiKhoan";
            this.lblTaiKhoan.Size = new System.Drawing.Size(60, 15);
            this.lblTaiKhoan.TabIndex = 0;
            this.lblTaiKhoan.Text = "Tài khoản:";
            // 
            // panelNguoiDungButtons
            // 
            this.panelNguoiDungButtons.Controls.Add(this.btnNapUser);
            this.panelNguoiDungButtons.Controls.Add(this.btnXoaUser);
            this.panelNguoiDungButtons.Controls.Add(this.btnLuuUser);
            this.panelNguoiDungButtons.Controls.Add(this.btnSuaUser);
            this.panelNguoiDungButtons.Controls.Add(this.btnMoiUser);
            this.panelNguoiDungButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelNguoiDungButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelNguoiDungButtons.Location = new System.Drawing.Point(0, 501);
            this.panelNguoiDungButtons.Name = "panelNguoiDungButtons";
            this.panelNguoiDungButtons.Size = new System.Drawing.Size(440, 50);
            this.panelNguoiDungButtons.TabIndex = 0;
            // 
            // btnNapUser
            // 
            this.btnNapUser.Location = new System.Drawing.Point(352, 8);
            this.btnNapUser.Margin = new System.Windows.Forms.Padding(3, 8, 8, 3);
            this.btnNapUser.Name = "btnNapUser";
            this.btnNapUser.Size = new System.Drawing.Size(80, 34);
            this.btnNapUser.TabIndex = 4;
            this.btnNapUser.Text = "Nạp lại";
            this.btnNapUser.UseVisualStyleBackColor = true;
            this.btnNapUser.Click += new System.EventHandler(this.btnNapUser_Click);
            // 
            // btnXoaUser
            // 
            this.btnXoaUser.Location = new System.Drawing.Point(266, 8);
            this.btnXoaUser.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnXoaUser.Name = "btnXoaUser";
            this.btnXoaUser.Size = new System.Drawing.Size(80, 34);
            this.btnXoaUser.TabIndex = 3;
            this.btnXoaUser.Text = "Xóa";
            this.btnXoaUser.UseVisualStyleBackColor = true;
            this.btnXoaUser.Click += new System.EventHandler(this.btnXoaUser_Click);
            // 
            // btnLuuUser
            // 
            this.btnLuuUser.Location = new System.Drawing.Point(180, 8);
            this.btnLuuUser.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnLuuUser.Name = "btnLuuUser";
            this.btnLuuUser.Size = new System.Drawing.Size(80, 34);
            this.btnLuuUser.TabIndex = 2;
            this.btnLuuUser.Text = "Lưu";
            this.btnLuuUser.UseVisualStyleBackColor = true;
            this.btnLuuUser.Click += new System.EventHandler(this.btnLuuUser_Click);
            // 
            // btnSuaUser
            // 
            this.btnSuaUser.Location = new System.Drawing.Point(94, 8);
            this.btnSuaUser.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnSuaUser.Name = "btnSuaUser";
            this.btnSuaUser.Size = new System.Drawing.Size(80, 34);
            this.btnSuaUser.TabIndex = 1;
            this.btnSuaUser.Text = "Sửa";
            this.btnSuaUser.UseVisualStyleBackColor = true;
            this.btnSuaUser.Click += new System.EventHandler(this.btnSuaUser_Click);
            // 
            // btnMoiUser
            // 
            this.btnMoiUser.Location = new System.Drawing.Point(8, 8);
            this.btnMoiUser.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnMoiUser.Name = "btnMoiUser";
            this.btnMoiUser.Size = new System.Drawing.Size(80, 34);
            this.btnMoiUser.TabIndex = 0;
            this.btnMoiUser.Text = "Thêm mới";
            this.btnMoiUser.UseVisualStyleBackColor = true;
            this.btnMoiUser.Click += new System.EventHandler(this.btnMoiUser_Click);
            // 
            // tabQuyenHan
            // 
            this.tabQuyenHan.Controls.Add(this.tableLayoutPanel2);
            this.tabQuyenHan.Location = new System.Drawing.Point(4, 24);
            this.tabQuyenHan.Name = "tabQuyenHan";
            this.tabQuyenHan.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuyenHan.Size = new System.Drawing.Size(1000, 573);
            this.tabQuyenHan.TabIndex = 1;
            this.tabQuyenHan.Text = "Quản lý Quyền hạn";
            this.tabQuyenHan.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.Controls.Add(this.grpQuyenHanList, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelQuyenHanDetail, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(994, 567);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // grpQuyenHanList
            // 
            this.grpQuyenHanList.Controls.Add(this.gridQuyenHan);
            this.grpQuyenHanList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpQuyenHanList.Location = new System.Drawing.Point(8, 8);
            this.grpQuyenHanList.Name = "grpQuyenHanList";
            this.grpQuyenHanList.Size = new System.Drawing.Size(532, 551);
            this.grpQuyenHanList.TabIndex = 0;
            this.grpQuyenHanList.TabStop = false;
            this.grpQuyenHanList.Text = "Danh sách Quyền hạn";
            // 
            // gridQuyenHan
            // 
            this.gridQuyenHan.AllowUserToAddRows = false;
            this.gridQuyenHan.AllowUserToDeleteRows = false;
            this.gridQuyenHan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridQuyenHan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridQuyenHan.Location = new System.Drawing.Point(3, 19);
            this.gridQuyenHan.MultiSelect = false;
            this.gridQuyenHan.Name = "gridQuyenHan";
            this.gridQuyenHan.ReadOnly = true;
            this.gridQuyenHan.RowTemplate.Height = 25;
            this.gridQuyenHan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridQuyenHan.Size = new System.Drawing.Size(526, 529);
            this.gridQuyenHan.TabIndex = 0;
            // 
            // panelQuyenHanDetail
            // 
            this.panelQuyenHanDetail.Controls.Add(this.grpQuyenHanDetail);
            this.panelQuyenHanDetail.Controls.Add(this.panelQuyenHanButtons);
            this.panelQuyenHanDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelQuyenHanDetail.Location = new System.Drawing.Point(546, 8);
            this.panelQuyenHanDetail.Name = "panelQuyenHanDetail";
            this.panelQuyenHanDetail.Size = new System.Drawing.Size(440, 551);
            this.panelQuyenHanDetail.TabIndex = 1;
            // 
            // grpQuyenHanDetail
            // 
            this.grpQuyenHanDetail.Controls.Add(this.txtGhiChu);
            this.grpQuyenHanDetail.Controls.Add(this.lblGhiChu);
            this.grpQuyenHanDetail.Controls.Add(this.txtTenQuyen);
            this.grpQuyenHanDetail.Controls.Add(this.lblTenQuyen);
            this.grpQuyenHanDetail.Controls.Add(this.txtMaQuyen);
            this.grpQuyenHanDetail.Controls.Add(this.lblMaQuyen);
            this.grpQuyenHanDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpQuyenHanDetail.Location = new System.Drawing.Point(0, 0);
            this.grpQuyenHanDetail.Name = "grpQuyenHanDetail";
            this.grpQuyenHanDetail.Size = new System.Drawing.Size(440, 501);
            this.grpQuyenHanDetail.TabIndex = 1;
            this.grpQuyenHanDetail.TabStop = false;
            this.grpQuyenHanDetail.Text = "Chi tiết";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Location = new System.Drawing.Point(105, 110);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(318, 85);
            this.txtGhiChu.TabIndex = 5;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(17, 113);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(50, 15);
            this.lblGhiChu.TabIndex = 4;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // txtTenQuyen
            // 
            this.txtTenQuyen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenQuyen.Location = new System.Drawing.Point(105, 71);
            this.txtTenQuyen.Name = "txtTenQuyen";
            this.txtTenQuyen.Size = new System.Drawing.Size(318, 23);
            this.txtTenQuyen.TabIndex = 3;
            // 
            // lblTenQuyen
            // 
            this.lblTenQuyen.AutoSize = true;
            this.lblTenQuyen.Location = new System.Drawing.Point(17, 74);
            this.lblTenQuyen.Name = "lblTenQuyen";
            this.lblTenQuyen.Size = new System.Drawing.Size(65, 15);
            this.lblTenQuyen.TabIndex = 2;
            this.lblTenQuyen.Text = "Tên quyền:";
            // 
            // txtMaQuyen
            // 
            this.txtMaQuyen.Location = new System.Drawing.Point(105, 33);
            this.txtMaQuyen.Name = "txtMaQuyen";
            this.txtMaQuyen.ReadOnly = true;
            this.txtMaQuyen.Size = new System.Drawing.Size(121, 23);
            this.txtMaQuyen.TabIndex = 1;
            // 
            // lblMaQuyen
            // 
            this.lblMaQuyen.AutoSize = true;
            this.lblMaQuyen.Location = new System.Drawing.Point(17, 36);
            this.lblMaQuyen.Name = "lblMaQuyen";
            this.lblMaQuyen.Size = new System.Drawing.Size(64, 15);
            this.lblMaQuyen.TabIndex = 0;
            this.lblMaQuyen.Text = "Mã quyền:";
            // 
            // panelQuyenHanButtons
            // 
            this.panelQuyenHanButtons.Controls.Add(this.btnNapRole);
            this.panelQuyenHanButtons.Controls.Add(this.btnXoaRole);
            this.panelQuyenHanButtons.Controls.Add(this.btnLuuRole);
            this.panelQuyenHanButtons.Controls.Add(this.btnSuaRole);
            this.panelQuyenHanButtons.Controls.Add(this.btnMoiRole);
            this.panelQuyenHanButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelQuyenHanButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelQuyenHanButtons.Location = new System.Drawing.Point(0, 501);
            this.panelQuyenHanButtons.Name = "panelQuyenHanButtons";
            this.panelQuyenHanButtons.Size = new System.Drawing.Size(440, 50);
            this.panelQuyenHanButtons.TabIndex = 0;
            // 
            // btnNapRole
            // 
            this.btnNapRole.Location = new System.Drawing.Point(352, 8);
            this.btnNapRole.Margin = new System.Windows.Forms.Padding(3, 8, 8, 3);
            this.btnNapRole.Name = "btnNapRole";
            this.btnNapRole.Size = new System.Drawing.Size(80, 34);
            this.btnNapRole.TabIndex = 4;
            this.btnNapRole.Text = "Nạp lại";
            this.btnNapRole.UseVisualStyleBackColor = true;
            this.btnNapRole.Click += new System.EventHandler(this.btnNapRole_Click);
            // 
            // btnXoaRole
            // 
            this.btnXoaRole.Location = new System.Drawing.Point(266, 8);
            this.btnXoaRole.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnXoaRole.Name = "btnXoaRole";
            this.btnXoaRole.Size = new System.Drawing.Size(80, 34);
            this.btnXoaRole.TabIndex = 3;
            this.btnXoaRole.Text = "Xóa";
            this.btnXoaRole.UseVisualStyleBackColor = true;
            this.btnXoaRole.Click += new System.EventHandler(this.btnXoaRole_Click);
            // 
            // btnLuuRole
            // 
            this.btnLuuRole.Location = new System.Drawing.Point(180, 8);
            this.btnLuuRole.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnLuuRole.Name = "btnLuuRole";
            this.btnLuuRole.Size = new System.Drawing.Size(80, 34);
            this.btnLuuRole.TabIndex = 2;
            this.btnLuuRole.Text = "Lưu";
            this.btnLuuRole.UseVisualStyleBackColor = true;
            this.btnLuuRole.Click += new System.EventHandler(this.btnLuuRole_Click);
            // 
            // btnSuaRole
            // 
            this.btnSuaRole.Location = new System.Drawing.Point(94, 8);
            this.btnSuaRole.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnSuaRole.Name = "btnSuaRole";
            this.btnSuaRole.Size = new System.Drawing.Size(80, 34);
            this.btnSuaRole.TabIndex = 1;
            this.btnSuaRole.Text = "Sửa";
            this.btnSuaRole.UseVisualStyleBackColor = true;
            this.btnSuaRole.Click += new System.EventHandler(this.btnSuaRole_Click);
            // 
            // btnMoiRole
            // 
            this.btnMoiRole.Location = new System.Drawing.Point(8, 8);
            this.btnMoiRole.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnMoiRole.Name = "btnMoiRole";
            this.btnMoiRole.Size = new System.Drawing.Size(80, 34);
            this.btnMoiRole.TabIndex = 0;
            this.btnMoiRole.Text = "Thêm mới";
            this.btnMoiRole.UseVisualStyleBackColor = true;
            this.btnMoiRole.Click += new System.EventHandler(this.btnMoiRole_Click);
            // 
            // FormQuanLyHeThong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 601);
            this.Controls.Add(this.tabControlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormQuanLyHeThong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản Lý Hệ Thống";
            this.Load += new System.EventHandler(this.FormQuanLyHeThong_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabNguoiDung.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpNguoiDungList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridNguoiDung)).EndInit();
            this.panelNguoiDungDetail.ResumeLayout(false);
            this.grpNguoiDungDetail.ResumeLayout(false);
            this.grpNguoiDungDetail.PerformLayout();
            this.panelNguoiDungButtons.ResumeLayout(false);
            this.tabQuyenHan.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.grpQuyenHanList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridQuyenHan)).EndInit();
            this.panelQuyenHanDetail.ResumeLayout(false);
            this.grpQuyenHanDetail.ResumeLayout(false);
            this.grpQuyenHanDetail.PerformLayout();
            this.panelQuyenHanButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabNguoiDung;
        private System.Windows.Forms.TabPage tabQuyenHan;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpNguoiDungList;
        private System.Windows.Forms.DataGridView gridNguoiDung;
        private System.Windows.Forms.Panel panelNguoiDungDetail;
        private System.Windows.Forms.GroupBox grpNguoiDungDetail;
        private System.Windows.Forms.FlowLayoutPanel panelNguoiDungButtons;
        private System.Windows.Forms.Button btnNapUser;
        private System.Windows.Forms.Button btnXoaUser;
        private System.Windows.Forms.Button btnLuuUser;
        private System.Windows.Forms.Button btnSuaUser;
        private System.Windows.Forms.Button btnMoiUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox grpQuyenHanList;
        private System.Windows.Forms.DataGridView gridQuyenHan;
        private System.Windows.Forms.Panel panelQuyenHanDetail;
        private System.Windows.Forms.GroupBox grpQuyenHanDetail;
        private System.Windows.Forms.FlowLayoutPanel panelQuyenHanButtons;
        private System.Windows.Forms.Button btnNapRole;
        private System.Windows.Forms.Button btnXoaRole;
        private System.Windows.Forms.Button btnLuuRole;
        private System.Windows.Forms.Button btnSuaRole;
        private System.Windows.Forms.Button btnMoiRole;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtTaiKhoan;
        private System.Windows.Forms.Label lblTaiKhoan;
        private System.Windows.Forms.ComboBox cboQuyenHan_User;
        private System.Windows.Forms.Label lblQuyenHan_User;
        private System.Windows.Forms.CheckBox chkHoatDong;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtTenQuyen;
        private System.Windows.Forms.Label lblTenQuyen;
        private System.Windows.Forms.TextBox txtMaQuyen;
        private System.Windows.Forms.Label lblMaQuyen;
    }
}