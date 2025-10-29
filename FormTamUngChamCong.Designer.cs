namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormTamUngChamCong
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.grpNhanVien = new System.Windows.Forms.GroupBox();
            this.gridNhanVien = new System.Windows.Forms.DataGridView();
            this.panelRight = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabTamUng = new System.Windows.Forms.TabPage();
            this.infoPanelTamUng = new System.Windows.Forms.TableLayoutPanel();
            this.lblNgayTU = new System.Windows.Forms.Label();
            this.dtpNgayTU = new System.Windows.Forms.DateTimePicker();
            this.lblSoTienTU = new System.Windows.Forms.Label();
            this.numSoTienTU = new System.Windows.Forms.NumericUpDown();
            this.lblGhiChuTU = new System.Windows.Forms.Label();
            this.txtGhiChuTU = new System.Windows.Forms.TextBox();
            this.gridTamUng = new System.Windows.Forms.DataGridView();
            this.panelButtonsTU = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNapTU = new System.Windows.Forms.Button();
            this.btnXoaTU = new System.Windows.Forms.Button();
            this.btnLuuTU = new System.Windows.Forms.Button();
            this.btnSuaTU = new System.Windows.Forms.Button();
            this.btnMoiTU = new System.Windows.Forms.Button();
            this.tabChamCong = new System.Windows.Forms.TabPage();
            this.infoPanelCC = new System.Windows.Forms.TableLayoutPanel();
            this.lblThangNam = new System.Windows.Forms.Label();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.txtNam = new System.Windows.Forms.TextBox();
            this.lblNgayCong = new System.Windows.Forms.Label();
            this.numNgayCong = new System.Windows.Forms.NumericUpDown();
            this.lblGhiChuCC = new System.Windows.Forms.Label();
            this.txtGhiChuCC = new System.Windows.Forms.TextBox();
            this.gridChamCong = new System.Windows.Forms.DataGridView();
            this.panelButtonsCC = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNapCC = new System.Windows.Forms.Button();
            this.btnXoaCC = new System.Windows.Forms.Button();
            this.btnLuuCC = new System.Windows.Forms.Button();
            this.btnSuaCC = new System.Windows.Forms.Button();
            this.btnMoiCC = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.grpNhanVien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNhanVien)).BeginInit();
            this.panelRight.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabTamUng.SuspendLayout();
            this.infoPanelTamUng.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoTienTU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTamUng)).BeginInit();
            this.panelButtonsTU.SuspendLayout();
            this.tabChamCong.SuspendLayout();
            this.infoPanelCC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNgayCong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridChamCong)).BeginInit();
            this.panelButtonsCC.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitMain, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1000, 700);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(3, 3);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.grpNhanVien);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.panelRight);
            this.splitMain.Size = new System.Drawing.Size(994, 694);
            this.splitMain.SplitterDistance = 350;
            this.splitMain.TabIndex = 0;
            // 
            // grpNhanVien
            // 
            this.grpNhanVien.Controls.Add(this.gridNhanVien);
            this.grpNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpNhanVien.Location = new System.Drawing.Point(0, 0);
            this.grpNhanVien.Name = "grpNhanVien";
            this.grpNhanVien.Size = new System.Drawing.Size(350, 694);
            this.grpNhanVien.TabIndex = 0;
            this.grpNhanVien.TabStop = false;
            this.grpNhanVien.Text = "Danh sách Nhân viên";
            // 
            // gridNhanVien
            // 
            this.gridNhanVien.AllowUserToAddRows = false;
            this.gridNhanVien.AllowUserToDeleteRows = false;
            this.gridNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridNhanVien.Location = new System.Drawing.Point(3, 19);
            this.gridNhanVien.Name = "gridNhanVien";
            this.gridNhanVien.ReadOnly = true;
            this.gridNhanVien.RowTemplate.Height = 25;
            this.gridNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridNhanVien.Size = new System.Drawing.Size(344, 672);
            this.gridNhanVien.TabIndex = 0;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.tabControl);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(640, 694);
            this.panelRight.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabTamUng);
            this.tabControl.Controls.Add(this.tabChamCong);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(640, 694);
            this.tabControl.TabIndex = 0;
            // 
            // tabTamUng
            // 
            this.tabTamUng.Controls.Add(this.infoPanelTamUng);
            this.tabTamUng.Controls.Add(this.gridTamUng);
            this.tabTamUng.Controls.Add(this.panelButtonsTU);
            this.tabTamUng.Location = new System.Drawing.Point(4, 24);
            this.tabTamUng.Name = "tabTamUng";
            this.tabTamUng.Padding = new System.Windows.Forms.Padding(3);
            this.tabTamUng.Size = new System.Drawing.Size(632, 666);
            this.tabTamUng.TabIndex = 0;
            this.tabTamUng.Text = "Tạm ứng";
            this.tabTamUng.UseVisualStyleBackColor = true;
            // 
            // infoPanelTamUng
            // 
            this.infoPanelTamUng.ColumnCount = 2;
            this.infoPanelTamUng.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.infoPanelTamUng.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.infoPanelTamUng.Controls.Add(this.lblNgayTU, 0, 0);
            this.infoPanelTamUng.Controls.Add(this.dtpNgayTU, 1, 0);
            this.infoPanelTamUng.Controls.Add(this.lblSoTienTU, 0, 1);
            this.infoPanelTamUng.Controls.Add(this.numSoTienTU, 1, 1);
            this.infoPanelTamUng.Controls.Add(this.lblGhiChuTU, 0, 2);
            this.infoPanelTamUng.Controls.Add(this.txtGhiChuTU, 1, 2);
            this.infoPanelTamUng.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.infoPanelTamUng.Location = new System.Drawing.Point(3, 497);
            this.infoPanelTamUng.Name = "infoPanelTamUng";
            this.infoPanelTamUng.Padding = new System.Windows.Forms.Padding(5);
            this.infoPanelTamUng.RowCount = 3;
            this.infoPanelTamUng.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanelTamUng.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanelTamUng.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.infoPanelTamUng.Size = new System.Drawing.Size(626, 114);
            this.infoPanelTamUng.TabIndex = 2;
            // 
            // lblNgayTU
            // 
            this.lblNgayTU.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNgayTU.AutoSize = true;
            this.lblNgayTU.Location = new System.Drawing.Point(8, 15);
            this.lblNgayTU.Name = "lblNgayTU";
            this.lblNgayTU.Size = new System.Drawing.Size(35, 15);
            this.lblNgayTU.TabIndex = 0;
            this.lblNgayTU.Text = "Ngày";
            // 
            // dtpNgayTU
            // 
            this.dtpNgayTU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpNgayTU.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayTU.Location = new System.Drawing.Point(108, 8);
            this.dtpNgayTU.Name = "dtpNgayTU";
            this.dtpNgayTU.Size = new System.Drawing.Size(510, 23);
            this.dtpNgayTU.TabIndex = 1;
            // 
            // lblSoTienTU
            // 
            this.lblSoTienTU.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSoTienTU.AutoSize = true;
            this.lblSoTienTU.Location = new System.Drawing.Point(8, 50);
            this.lblSoTienTU.Name = "lblSoTienTU";
            this.lblSoTienTU.Size = new System.Drawing.Size(43, 15);
            this.lblSoTienTU.TabIndex = 2;
            this.lblSoTienTU.Text = "Số tiền";
            // 
            // numSoTienTU
            // 
            this.numSoTienTU.DecimalPlaces = 2;
            this.numSoTienTU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numSoTienTU.Location = new System.Drawing.Point(108, 43);
            this.numSoTienTU.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numSoTienTU.Name = "numSoTienTU";
            this.numSoTienTU.Size = new System.Drawing.Size(510, 23);
            this.numSoTienTU.TabIndex = 3;
            // 
            // lblGhiChuTU
            // 
            this.lblGhiChuTU.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGhiChuTU.AutoSize = true;
            this.lblGhiChuTU.Location = new System.Drawing.Point(8, 84);
            this.lblGhiChuTU.Name = "lblGhiChuTU";
            this.lblGhiChuTU.Size = new System.Drawing.Size(48, 15);
            this.lblGhiChuTU.TabIndex = 4;
            this.lblGhiChuTU.Text = "Ghi chú";
            // 
            // txtGhiChuTU
            // 
            this.txtGhiChuTU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGhiChuTU.Location = new System.Drawing.Point(108, 78);
            this.txtGhiChuTU.Multiline = true;
            this.txtGhiChuTU.Name = "txtGhiChuTU";
            this.txtGhiChuTU.Size = new System.Drawing.Size(510, 28);
            this.txtGhiChuTU.TabIndex = 5;
            // 
            // gridTamUng
            // 
            this.gridTamUng.AllowUserToAddRows = false;
            this.gridTamUng.AllowUserToDeleteRows = false;
            this.gridTamUng.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTamUng.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTamUng.Location = new System.Drawing.Point(3, 3);
            this.gridTamUng.Name = "gridTamUng";
            this.gridTamUng.ReadOnly = true;
            this.gridTamUng.RowTemplate.Height = 25;
            this.gridTamUng.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTamUng.Size = new System.Drawing.Size(626, 608);
            this.gridTamUng.TabIndex = 1;
            // 
            // panelButtonsTU
            // 
            this.panelButtonsTU.Controls.Add(this.btnNapTU);
            this.panelButtonsTU.Controls.Add(this.btnXoaTU);
            this.panelButtonsTU.Controls.Add(this.btnLuuTU);
            this.panelButtonsTU.Controls.Add(this.btnSuaTU);
            this.panelButtonsTU.Controls.Add(this.btnMoiTU);
            this.panelButtonsTU.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtonsTU.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelButtonsTU.Location = new System.Drawing.Point(3, 611);
            this.panelButtonsTU.Name = "panelButtonsTU";
            this.panelButtonsTU.Size = new System.Drawing.Size(626, 52);
            this.panelButtonsTU.TabIndex = 3;
            // 
            // btnNapTU
            // 
            this.btnNapTU.Location = new System.Drawing.Point(543, 3);
            this.btnNapTU.Name = "btnNapTU";
            this.btnNapTU.Size = new System.Drawing.Size(80, 29);
            this.btnNapTU.TabIndex = 4;
            this.btnNapTU.Text = "Nạp lại";
            this.btnNapTU.UseVisualStyleBackColor = true;
            // 
            // btnXoaTU
            // 
            this.btnXoaTU.Location = new System.Drawing.Point(457, 3);
            this.btnXoaTU.Name = "btnXoaTU";
            this.btnXoaTU.Size = new System.Drawing.Size(80, 29);
            this.btnXoaTU.TabIndex = 3;
            this.btnXoaTU.Text = "Xóa";
            this.btnXoaTU.UseVisualStyleBackColor = true;
            // 
            // btnLuuTU
            // 
            this.btnLuuTU.Location = new System.Drawing.Point(371, 3);
            this.btnLuuTU.Name = "btnLuuTU";
            this.btnLuuTU.Size = new System.Drawing.Size(80, 29);
            this.btnLuuTU.TabIndex = 2;
            this.btnLuuTU.Text = "Lưu";
            this.btnLuuTU.UseVisualStyleBackColor = true;
            // 
            // btnSuaTU
            // 
            this.btnSuaTU.Location = new System.Drawing.Point(285, 3);
            this.btnSuaTU.Name = "btnSuaTU";
            this.btnSuaTU.Size = new System.Drawing.Size(80, 29);
            this.btnSuaTU.TabIndex = 1;
            this.btnSuaTU.Text = "Sửa";
            this.btnSuaTU.UseVisualStyleBackColor = true;
            // 
            // btnMoiTU
            // 
            this.btnMoiTU.Location = new System.Drawing.Point(199, 3);
            this.btnMoiTU.Name = "btnMoiTU";
            this.btnMoiTU.Size = new System.Drawing.Size(80, 29);
            this.btnMoiTU.TabIndex = 0;
            this.btnMoiTU.Text = "Thêm mới";
            this.btnMoiTU.UseVisualStyleBackColor = true;
            // 
            // tabChamCong
            // 
            this.tabChamCong.Controls.Add(this.infoPanelCC);
            this.tabChamCong.Controls.Add(this.gridChamCong);
            this.tabChamCong.Controls.Add(this.panelButtonsCC);
            this.tabChamCong.Location = new System.Drawing.Point(4, 22);
            this.tabChamCong.Name = "tabChamCong";
            this.tabChamCong.Padding = new System.Windows.Forms.Padding(3);
            this.tabChamCong.Size = new System.Drawing.Size(632, 668);
            this.tabChamCong.TabIndex = 1;
            this.tabChamCong.Text = "Chấm công";
            this.tabChamCong.UseVisualStyleBackColor = true;
            // 
            // infoPanelCC
            // 
            this.infoPanelCC.ColumnCount = 3;
            this.infoPanelCC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.infoPanelCC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.infoPanelCC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.infoPanelCC.Controls.Add(this.lblThangNam, 0, 0);
            this.infoPanelCC.Controls.Add(this.cboThang, 1, 0);
            this.infoPanelCC.Controls.Add(this.txtNam, 2, 0);
            this.infoPanelCC.Controls.Add(this.lblNgayCong, 0, 1);
            this.infoPanelCC.Controls.Add(this.numNgayCong, 1, 1);
            this.infoPanelCC.Controls.Add(this.lblGhiChuCC, 0, 2);
            this.infoPanelCC.Controls.Add(this.txtGhiChuCC, 1, 2);
            this.infoPanelCC.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.infoPanelCC.Location = new System.Drawing.Point(3, 499);
            this.infoPanelCC.Name = "infoPanelCC";
            this.infoPanelCC.Padding = new System.Windows.Forms.Padding(5);
            this.infoPanelCC.RowCount = 3;
            this.infoPanelCC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanelCC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanelCC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.infoPanelCC.Size = new System.Drawing.Size(626, 114);
            this.infoPanelCC.TabIndex = 2;
            // 
            // lblThangNam
            // 
            this.lblThangNam.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblThangNam.AutoSize = true;
            this.lblThangNam.Location = new System.Drawing.Point(8, 15);
            this.lblThangNam.Name = "lblThangNam";
            this.lblThangNam.Size = new System.Drawing.Size(72, 15);
            this.lblThangNam.TabIndex = 0;
            this.lblThangNam.Text = "Tháng/Năm";
            // 
            // cboThang
            // 
            this.cboThang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThang.FormattingEnabled = true;
            this.cboThang.Location = new System.Drawing.Point(108, 8);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new System.Drawing.Size(252, 23);
            this.cboThang.TabIndex = 1;
            // 
            // txtNam
            // 
            this.txtNam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNam.Location = new System.Drawing.Point(366, 8);
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(252, 23);
            this.txtNam.TabIndex = 2;
            // 
            // lblNgayCong
            // 
            this.lblNgayCong.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNgayCong.AutoSize = true;
            this.lblNgayCong.Location = new System.Drawing.Point(8, 50);
            this.lblNgayCong.Name = "lblNgayCong";
            this.lblNgayCong.Size = new System.Drawing.Size(65, 15);
            this.lblNgayCong.TabIndex = 3;
            this.lblNgayCong.Text = "Ngày công";
            // 
            // numNgayCong
            // 
            this.infoPanelCC.SetColumnSpan(this.numNgayCong, 2);
            this.numNgayCong.DecimalPlaces = 2;
            this.numNgayCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numNgayCong.Location = new System.Drawing.Point(108, 43);
            this.numNgayCong.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numNgayCong.Name = "numNgayCong";
            this.numNgayCong.Size = new System.Drawing.Size(510, 23);
            this.numNgayCong.TabIndex = 4;
            // 
            // lblGhiChuCC
            // 
            this.lblGhiChuCC.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGhiChuCC.AutoSize = true;
            this.lblGhiChuCC.Location = new System.Drawing.Point(8, 84);
            this.lblGhiChuCC.Name = "lblGhiChuCC";
            this.lblGhiChuCC.Size = new System.Drawing.Size(48, 15);
            this.lblGhiChuCC.TabIndex = 5;
            this.lblGhiChuCC.Text = "Ghi chú";
            // 
            // txtGhiChuCC
            // 
            this.infoPanelCC.SetColumnSpan(this.txtGhiChuCC, 2);
            this.txtGhiChuCC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGhiChuCC.Location = new System.Drawing.Point(108, 78);
            this.txtGhiChuCC.Multiline = true;
            this.txtGhiChuCC.Name = "txtGhiChuCC";
            this.txtGhiChuCC.Size = new System.Drawing.Size(510, 28);
            this.txtGhiChuCC.TabIndex = 6;
            // 
            // gridChamCong
            // 
            this.gridChamCong.AllowUserToAddRows = false;
            this.gridChamCong.AllowUserToDeleteRows = false;
            this.gridChamCong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridChamCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridChamCong.Location = new System.Drawing.Point(3, 3);
            this.gridChamCong.Name = "gridChamCong";
            this.gridChamCong.ReadOnly = true;
            this.gridChamCong.RowTemplate.Height = 25;
            this.gridChamCong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridChamCong.Size = new System.Drawing.Size(626, 610);
            this.gridChamCong.TabIndex = 1;
            // 
            // panelButtonsCC
            // 
            this.panelButtonsCC.Controls.Add(this.btnNapCC);
            this.panelButtonsCC.Controls.Add(this.btnXoaCC);
            this.panelButtonsCC.Controls.Add(this.btnLuuCC);
            this.panelButtonsCC.Controls.Add(this.btnSuaCC);
            this.panelButtonsCC.Controls.Add(this.btnMoiCC);
            this.panelButtonsCC.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtonsCC.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelButtonsCC.Location = new System.Drawing.Point(3, 613);
            this.panelButtonsCC.Name = "panelButtonsCC";
            this.panelButtonsCC.Size = new System.Drawing.Size(626, 52);
            this.panelButtonsCC.TabIndex = 3;
            // 
            // btnNapCC
            // 
            this.btnNapCC.Location = new System.Drawing.Point(543, 3);
            this.btnNapCC.Name = "btnNapCC";
            this.btnNapCC.Size = new System.Drawing.Size(80, 29);
            this.btnNapCC.TabIndex = 4;
            this.btnNapCC.Text = "Nạp lại";
            this.btnNapCC.UseVisualStyleBackColor = true;
            // 
            // btnXoaCC
            // 
            this.btnXoaCC.Location = new System.Drawing.Point(457, 3);
            this.btnXoaCC.Name = "btnXoaCC";
            this.btnXoaCC.Size = new System.Drawing.Size(80, 29);
            this.btnXoaCC.TabIndex = 3;
            this.btnXoaCC.Text = "Xóa";
            this.btnXoaCC.UseVisualStyleBackColor = true;
            // 
            // btnLuuCC
            // 
            this.btnLuuCC.Location = new System.Drawing.Point(371, 3);
            this.btnLuuCC.Name = "btnLuuCC";
            this.btnLuuCC.Size = new System.Drawing.Size(80, 29);
            this.btnLuuCC.TabIndex = 2;
            this.btnLuuCC.Text = "Lưu";
            this.btnLuuCC.UseVisualStyleBackColor = true;
            // 
            // btnSuaCC
            // 
            this.btnSuaCC.Location = new System.Drawing.Point(285, 3);
            this.btnSuaCC.Name = "btnSuaCC";
            this.btnSuaCC.Size = new System.Drawing.Size(80, 29);
            this.btnSuaCC.TabIndex = 1;
            this.btnSuaCC.Text = "Sửa";
            this.btnSuaCC.UseVisualStyleBackColor = true;
            // 
            // btnMoiCC
            // 
            this.btnMoiCC.Location = new System.Drawing.Point(199, 3);
            this.btnMoiCC.Name = "btnMoiCC";
            this.btnMoiCC.Size = new System.Drawing.Size(80, 29);
            this.btnMoiCC.TabIndex = 0;
            this.btnMoiCC.Text = "Thêm mới";
            this.btnMoiCC.UseVisualStyleBackColor = true;
            // 
            // FormTamUngChamCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FormTamUngChamCong";
            this.Text = "Quản lý Tạm ứng và Chấm công";
            this.Load += new System.EventHandler(this.FormTamUngChamCong_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.grpNhanVien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridNhanVien)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabTamUng.ResumeLayout(false);
            this.infoPanelTamUng.ResumeLayout(false);
            this.infoPanelTamUng.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoTienTU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTamUng)).EndInit();
            this.panelButtonsTU.ResumeLayout(false);
            this.tabChamCong.ResumeLayout(false);
            this.infoPanelCC.ResumeLayout(false);
            this.infoPanelCC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNgayCong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridChamCong)).EndInit();
            this.panelButtonsCC.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.GroupBox grpNhanVien;
        private System.Windows.Forms.DataGridView gridNhanVien;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabTamUng;
        private System.Windows.Forms.TabPage tabChamCong;
        private System.Windows.Forms.DataGridView gridTamUng;
        private System.Windows.Forms.FlowLayoutPanel panelButtonsTU;
        private System.Windows.Forms.Button btnNapTU;
        private System.Windows.Forms.Button btnXoaTU;
        private System.Windows.Forms.Button btnLuuTU;
        private System.Windows.Forms.Button btnSuaTU;
        private System.Windows.Forms.Button btnMoiTU;
        private System.Windows.Forms.TableLayoutPanel infoPanelTamUng;
        private System.Windows.Forms.Label lblNgayTU;
        private System.Windows.Forms.DateTimePicker dtpNgayTU;
        private System.Windows.Forms.Label lblSoTienTU;
        private System.Windows.Forms.NumericUpDown numSoTienTU;
        private System.Windows.Forms.Label lblGhiChuTU;
        private System.Windows.Forms.TextBox txtGhiChuTU;
        private System.Windows.Forms.DataGridView gridChamCong;
        private System.Windows.Forms.FlowLayoutPanel panelButtonsCC;
        private System.Windows.Forms.Button btnNapCC;
        private System.Windows.Forms.Button btnXoaCC;
        private System.Windows.Forms.Button btnLuuCC;
        private System.Windows.Forms.Button btnSuaCC;
        private System.Windows.Forms.Button btnMoiCC;
        private System.Windows.Forms.TableLayoutPanel infoPanelCC;
        private System.Windows.Forms.Label lblThangNam;
        private System.Windows.Forms.ComboBox cboThang;
        private System.Windows.Forms.TextBox txtNam;
        private System.Windows.Forms.Label lblNgayCong;
        private System.Windows.Forms.NumericUpDown numNgayCong;
        private System.Windows.Forms.Label lblGhiChuCC;
        private System.Windows.Forms.TextBox txtGhiChuCC;
    }
}
