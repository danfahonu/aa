namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormCaiDatHeThong
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
            this.tabCongTy = new System.Windows.Forms.TabPage();
            this.btnLuuCongTy = new System.Windows.Forms.Button();
            this.txtGhiChuCty = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMST = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmailCty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSDTCty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDiaChiCty = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenCty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabThue = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpThueList = new System.Windows.Forms.GroupBox();
            this.gridThue = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpThueDetail = new System.Windows.Forms.GroupBox();
            this.txtGhiChuThue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTiLeThue = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMaThue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panelThueButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNapThue = new System.Windows.Forms.Button();
            this.btnXoaThue = new System.Windows.Forms.Button();
            this.btnLuuThue = new System.Windows.Forms.Button();
            this.btnSuaThue = new System.Windows.Forms.Button();
            this.btnMoiThue = new System.Windows.Forms.Button();
            this.tabTyGia = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grpTyGiaList = new System.Windows.Forms.GroupBox();
            this.gridTyGia = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpTyGiaDetail = new System.Windows.Forms.GroupBox();
            this.txtGiaTri = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpNgayTyGia = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.cboTienTe = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtIDTyGia = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panelTyGiaButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNapTyGia = new System.Windows.Forms.Button();
            this.btnXoaTyGia = new System.Windows.Forms.Button();
            this.btnLuuTyGia = new System.Windows.Forms.Button();
            this.btnSuaTyGia = new System.Windows.Forms.Button();
            this.btnMoiTyGia = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabCongTy.SuspendLayout();
            this.tabThue.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpThueList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridThue)).BeginInit();
            this.panel1.SuspendLayout();
            this.grpThueDetail.SuspendLayout();
            this.panelThueButtons.SuspendLayout();
            this.tabTyGia.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpTyGiaList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTyGia)).BeginInit();
            this.panel2.SuspendLayout();
            this.grpTyGiaDetail.SuspendLayout();
            this.panelTyGiaButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabCongTy);
            this.tabControlMain.Controls.Add(this.tabThue);
            this.tabControlMain.Controls.Add(this.tabTyGia);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(984, 561);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabCongTy
            // 
            this.tabCongTy.Controls.Add(this.btnLuuCongTy);
            this.tabCongTy.Controls.Add(this.txtGhiChuCty);
            this.tabCongTy.Controls.Add(this.label6);
            this.tabCongTy.Controls.Add(this.txtMST);
            this.tabCongTy.Controls.Add(this.label5);
            this.tabCongTy.Controls.Add(this.txtEmailCty);
            this.tabCongTy.Controls.Add(this.label4);
            this.tabCongTy.Controls.Add(this.txtSDTCty);
            this.tabCongTy.Controls.Add(this.label3);
            this.tabCongTy.Controls.Add(this.txtDiaChiCty);
            this.tabCongTy.Controls.Add(this.label2);
            this.tabCongTy.Controls.Add(this.txtTenCty);
            this.tabCongTy.Controls.Add(this.label1);
            this.tabCongTy.Location = new System.Drawing.Point(4, 24);
            this.tabCongTy.Name = "tabCongTy";
            this.tabCongTy.Padding = new System.Windows.Forms.Padding(3);
            this.tabCongTy.Size = new System.Drawing.Size(976, 533);
            this.tabCongTy.TabIndex = 0;
            this.tabCongTy.Text = "Thông tin công ty";
            this.tabCongTy.UseVisualStyleBackColor = true;
            // 
            // btnLuuCongTy
            // 
            this.btnLuuCongTy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuCongTy.Location = new System.Drawing.Point(123, 335);
            this.btnLuuCongTy.Name = "btnLuuCongTy";
            this.btnLuuCongTy.Size = new System.Drawing.Size(155, 43);
            this.btnLuuCongTy.TabIndex = 12;
            this.btnLuuCongTy.Text = "Lưu Thay Đổi";
            this.btnLuuCongTy.UseVisualStyleBackColor = true;
            this.btnLuuCongTy.Click += new System.EventHandler(this.btnLuuCongTy_Click);
            // 
            // txtGhiChuCty
            // 
            this.txtGhiChuCty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChuCty.Location = new System.Drawing.Point(123, 227);
            this.txtGhiChuCty.Multiline = true;
            this.txtGhiChuCty.Name = "txtGhiChuCty";
            this.txtGhiChuCty.Size = new System.Drawing.Size(785, 87);
            this.txtGhiChuCty.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Ghi chú:";
            // 
            // txtMST
            // 
            this.txtMST.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMST.Location = new System.Drawing.Point(123, 185);
            this.txtMST.Name = "txtMST";
            this.txtMST.Size = new System.Drawing.Size(325, 23);
            this.txtMST.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Mã số thuế:";
            // 
            // txtEmailCty
            // 
            this.txtEmailCty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailCty.Location = new System.Drawing.Point(123, 145);
            this.txtEmailCty.Name = "txtEmailCty";
            this.txtEmailCty.Size = new System.Drawing.Size(325, 23);
            this.txtEmailCty.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Email:";
            // 
            // txtSDTCty
            // 
            this.txtSDTCty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSDTCty.Location = new System.Drawing.Point(123, 106);
            this.txtSDTCty.Name = "txtSDTCty";
            this.txtSDTCty.Size = new System.Drawing.Size(325, 23);
            this.txtSDTCty.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Số điện thoại:";
            // 
            // txtDiaChiCty
            // 
            this.txtDiaChiCty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiaChiCty.Location = new System.Drawing.Point(123, 67);
            this.txtDiaChiCty.Name = "txtDiaChiCty";
            this.txtDiaChiCty.Size = new System.Drawing.Size(785, 23);
            this.txtDiaChiCty.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Địa chỉ:";
            // 
            // txtTenCty
            // 
            this.txtTenCty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenCty.Location = new System.Drawing.Point(123, 28);
            this.txtTenCty.Name = "txtTenCty";
            this.txtTenCty.Size = new System.Drawing.Size(785, 23);
            this.txtTenCty.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên công ty:";
            // 
            // tabThue
            // 
            this.tabThue.Controls.Add(this.tableLayoutPanel1);
            this.tabThue.Location = new System.Drawing.Point(4, 24);
            this.tabThue.Name = "tabThue";
            this.tabThue.Padding = new System.Windows.Forms.Padding(3);
            this.tabThue.Size = new System.Drawing.Size(976, 533);
            this.tabThue.TabIndex = 1;
            this.tabThue.Text = "Quản lý Thuế VAT";
            this.tabThue.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Controls.Add(this.grpThueList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(970, 527);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // grpThueList
            // 
            this.grpThueList.Controls.Add(this.gridThue);
            this.grpThueList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpThueList.Location = new System.Drawing.Point(8, 8);
            this.grpThueList.Name = "grpThueList";
            this.grpThueList.Size = new System.Drawing.Size(524, 511);
            this.grpThueList.TabIndex = 0;
            this.grpThueList.TabStop = false;
            this.grpThueList.Text = "Danh sách mức thuế";
            // 
            // gridThue
            // 
            this.gridThue.AllowUserToAddRows = false;
            this.gridThue.AllowUserToDeleteRows = false;
            this.gridThue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridThue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridThue.Location = new System.Drawing.Point(3, 19);
            this.gridThue.MultiSelect = false;
            this.gridThue.Name = "gridThue";
            this.gridThue.ReadOnly = true;
            this.gridThue.RowTemplate.Height = 25;
            this.gridThue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridThue.Size = new System.Drawing.Size(518, 489);
            this.gridThue.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grpThueDetail);
            this.panel1.Controls.Add(this.panelThueButtons);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(538, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 511);
            this.panel1.TabIndex = 1;
            // 
            // grpThueDetail
            // 
            this.grpThueDetail.Controls.Add(this.txtGhiChuThue);
            this.grpThueDetail.Controls.Add(this.label9);
            this.grpThueDetail.Controls.Add(this.txtTiLeThue);
            this.grpThueDetail.Controls.Add(this.label8);
            this.grpThueDetail.Controls.Add(this.txtMaThue);
            this.grpThueDetail.Controls.Add(this.label7);
            this.grpThueDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpThueDetail.Location = new System.Drawing.Point(0, 0);
            this.grpThueDetail.Name = "grpThueDetail";
            this.grpThueDetail.Size = new System.Drawing.Size(424, 461);
            this.grpThueDetail.TabIndex = 1;
            this.grpThueDetail.TabStop = false;
            this.grpThueDetail.Text = "Chi tiết";
            // 
            // txtGhiChuThue
            // 
            this.txtGhiChuThue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChuThue.Location = new System.Drawing.Point(105, 110);
            this.txtGhiChuThue.Multiline = true;
            this.txtGhiChuThue.Name = "txtGhiChuThue";
            this.txtGhiChuThue.Size = new System.Drawing.Size(302, 85);
            this.txtGhiChuThue.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 15);
            this.label9.TabIndex = 4;
            this.label9.Text = "Ghi chú:";
            // 
            // txtTiLeThue
            // 
            this.txtTiLeThue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTiLeThue.Location = new System.Drawing.Point(105, 71);
            this.txtTiLeThue.Name = "txtTiLeThue";
            this.txtTiLeThue.Size = new System.Drawing.Size(134, 23);
            this.txtTiLeThue.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "Tỉ lệ (%):";
            // 
            // txtMaThue
            // 
            this.txtMaThue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaThue.Location = new System.Drawing.Point(105, 33);
            this.txtMaThue.Name = "txtMaThue";
            this.txtMaThue.Size = new System.Drawing.Size(302, 23);
            this.txtMaThue.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Mã thuế:";
            // 
            // panelThueButtons
            // 
            this.panelThueButtons.Controls.Add(this.btnNapThue);
            this.panelThueButtons.Controls.Add(this.btnXoaThue);
            this.panelThueButtons.Controls.Add(this.btnLuuThue);
            this.panelThueButtons.Controls.Add(this.btnSuaThue);
            this.panelThueButtons.Controls.Add(this.btnMoiThue);
            this.panelThueButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelThueButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelThueButtons.Location = new System.Drawing.Point(0, 461);
            this.panelThueButtons.Name = "panelThueButtons";
            this.panelThueButtons.Size = new System.Drawing.Size(424, 50);
            this.panelThueButtons.TabIndex = 0;
            // 
            // btnNapThue
            // 
            this.btnNapThue.Location = new System.Drawing.Point(336, 8);
            this.btnNapThue.Margin = new System.Windows.Forms.Padding(3, 8, 8, 3);
            this.btnNapThue.Name = "btnNapThue";
            this.btnNapThue.Size = new System.Drawing.Size(80, 34);
            this.btnNapThue.TabIndex = 4;
            this.btnNapThue.Text = "Nạp lại";
            this.btnNapThue.UseVisualStyleBackColor = true;
            this.btnNapThue.Click += new System.EventHandler(this.btnNapThue_Click);
            // 
            // btnXoaThue
            // 
            this.btnXoaThue.Location = new System.Drawing.Point(250, 8);
            this.btnXoaThue.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnXoaThue.Name = "btnXoaThue";
            this.btnXoaThue.Size = new System.Drawing.Size(80, 34);
            this.btnXoaThue.TabIndex = 3;
            this.btnXoaThue.Text = "Xóa";
            this.btnXoaThue.UseVisualStyleBackColor = true;
            this.btnXoaThue.Click += new System.EventHandler(this.btnXoaThue_Click);
            // 
            // btnLuuThue
            // 
            this.btnLuuThue.Location = new System.Drawing.Point(164, 8);
            this.btnLuuThue.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnLuuThue.Name = "btnLuuThue";
            this.btnLuuThue.Size = new System.Drawing.Size(80, 34);
            this.btnLuuThue.TabIndex = 2;
            this.btnLuuThue.Text = "Lưu";
            this.btnLuuThue.UseVisualStyleBackColor = true;
            this.btnLuuThue.Click += new System.EventHandler(this.btnLuuThue_Click);
            // 
            // btnSuaThue
            // 
            this.btnSuaThue.Location = new System.Drawing.Point(78, 8);
            this.btnSuaThue.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnSuaThue.Name = "btnSuaThue";
            this.btnSuaThue.Size = new System.Drawing.Size(80, 34);
            this.btnSuaThue.TabIndex = 1;
            this.btnSuaThue.Text = "Sửa";
            this.btnSuaThue.UseVisualStyleBackColor = true;
            this.btnSuaThue.Click += new System.EventHandler(this.btnSuaThue_Click);
            // 
            // btnMoiThue
            // 
            this.btnMoiThue.Location = new System.Drawing.Point(-8, 8);
            this.btnMoiThue.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnMoiThue.Name = "btnMoiThue";
            this.btnMoiThue.Size = new System.Drawing.Size(80, 34);
            this.btnMoiThue.TabIndex = 0;
            this.btnMoiThue.Text = "Thêm mới";
            this.btnMoiThue.UseVisualStyleBackColor = true;
            this.btnMoiThue.Click += new System.EventHandler(this.btnMoiThue_Click);
            // 
            // tabTyGia
            // 
            this.tabTyGia.Controls.Add(this.tableLayoutPanel2);
            this.tabTyGia.Location = new System.Drawing.Point(4, 24);
            this.tabTyGia.Name = "tabTyGia";
            this.tabTyGia.Padding = new System.Windows.Forms.Padding(3);
            this.tabTyGia.Size = new System.Drawing.Size(976, 533);
            this.tabTyGia.TabIndex = 2;
            this.tabTyGia.Text = "Quản lý Tỷ giá";
            this.tabTyGia.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.Controls.Add(this.grpTyGiaList, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(970, 527);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // grpTyGiaList
            // 
            this.grpTyGiaList.Controls.Add(this.gridTyGia);
            this.grpTyGiaList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTyGiaList.Location = new System.Drawing.Point(8, 8);
            this.grpTyGiaList.Name = "grpTyGiaList";
            this.grpTyGiaList.Size = new System.Drawing.Size(524, 511);
            this.grpTyGiaList.TabIndex = 0;
            this.grpTyGiaList.TabStop = false;
            this.grpTyGiaList.Text = "Danh sách Tỷ giá";
            // 
            // gridTyGia
            // 
            this.gridTyGia.AllowUserToAddRows = false;
            this.gridTyGia.AllowUserToDeleteRows = false;
            this.gridTyGia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTyGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTyGia.Location = new System.Drawing.Point(3, 19);
            this.gridTyGia.MultiSelect = false;
            this.gridTyGia.Name = "gridTyGia";
            this.gridTyGia.ReadOnly = true;
            this.gridTyGia.RowTemplate.Height = 25;
            this.gridTyGia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTyGia.Size = new System.Drawing.Size(518, 489);
            this.gridTyGia.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grpTyGiaDetail);
            this.panel2.Controls.Add(this.panelTyGiaButtons);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(538, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(424, 511);
            this.panel2.TabIndex = 1;
            // 
            // grpTyGiaDetail
            // 
            this.grpTyGiaDetail.Controls.Add(this.txtGiaTri);
            this.grpTyGiaDetail.Controls.Add(this.label13);
            this.grpTyGiaDetail.Controls.Add(this.dtpNgayTyGia);
            this.grpTyGiaDetail.Controls.Add(this.label12);
            this.grpTyGiaDetail.Controls.Add(this.cboTienTe);
            this.grpTyGiaDetail.Controls.Add(this.label11);
            this.grpTyGiaDetail.Controls.Add(this.txtIDTyGia);
            this.grpTyGiaDetail.Controls.Add(this.label10);
            this.grpTyGiaDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTyGiaDetail.Location = new System.Drawing.Point(0, 0);
            this.grpTyGiaDetail.Name = "grpTyGiaDetail";
            this.grpTyGiaDetail.Size = new System.Drawing.Size(424, 461);
            this.grpTyGiaDetail.TabIndex = 1;
            this.grpTyGiaDetail.TabStop = false;
            this.grpTyGiaDetail.Text = "Chi tiết";
            // 
            // txtGiaTri
            // 
            this.txtGiaTri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGiaTri.Location = new System.Drawing.Point(105, 149);
            this.txtGiaTri.Name = "txtGiaTri";
            this.txtGiaTri.Size = new System.Drawing.Size(302, 23);
            this.txtGiaTri.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 152);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 15);
            this.label13.TabIndex = 6;
            this.label13.Text = "Giá trị:";
            // 
            // dtpNgayTyGia
            // 
            this.dtpNgayTyGia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpNgayTyGia.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayTyGia.Location = new System.Drawing.Point(105, 110);
            this.dtpNgayTyGia.Name = "dtpNgayTyGia";
            this.dtpNgayTyGia.Size = new System.Drawing.Size(163, 23);
            this.dtpNgayTyGia.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 114);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 15);
            this.label12.TabIndex = 4;
            this.label12.Text = "Ngày:";
            // 
            // cboTienTe
            // 
            this.cboTienTe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTienTe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTienTe.FormattingEnabled = true;
            this.cboTienTe.Location = new System.Drawing.Point(105, 71);
            this.cboTienTe.Name = "cboTienTe";
            this.cboTienTe.Size = new System.Drawing.Size(302, 23);
            this.cboTienTe.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 15);
            this.label11.TabIndex = 2;
            this.label11.Text = "Tiền tệ:";
            // 
            // txtIDTyGia
            // 
            this.txtIDTyGia.Location = new System.Drawing.Point(105, 33);
            this.txtIDTyGia.Name = "txtIDTyGia";
            this.txtIDTyGia.ReadOnly = true;
            this.txtIDTyGia.Size = new System.Drawing.Size(121, 23);
            this.txtIDTyGia.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "ID:";
            // 
            // panelTyGiaButtons
            // 
            this.panelTyGiaButtons.Controls.Add(this.btnNapTyGia);
            this.panelTyGiaButtons.Controls.Add(this.btnXoaTyGia);
            this.panelTyGiaButtons.Controls.Add(this.btnLuuTyGia);
            this.panelTyGiaButtons.Controls.Add(this.btnSuaTyGia);
            this.panelTyGiaButtons.Controls.Add(this.btnMoiTyGia);
            this.panelTyGiaButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTyGiaButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelTyGiaButtons.Location = new System.Drawing.Point(0, 461);
            this.panelTyGiaButtons.Name = "panelTyGiaButtons";
            this.panelTyGiaButtons.Size = new System.Drawing.Size(424, 50);
            this.panelTyGiaButtons.TabIndex = 0;
            // 
            // btnNapTyGia
            // 
            this.btnNapTyGia.Location = new System.Drawing.Point(336, 8);
            this.btnNapTyGia.Margin = new System.Windows.Forms.Padding(3, 8, 8, 3);
            this.btnNapTyGia.Name = "btnNapTyGia";
            this.btnNapTyGia.Size = new System.Drawing.Size(80, 34);
            this.btnNapTyGia.TabIndex = 4;
            this.btnNapTyGia.Text = "Nạp lại";
            this.btnNapTyGia.UseVisualStyleBackColor = true;
            this.btnNapTyGia.Click += new System.EventHandler(this.btnNapTyGia_Click);
            // 
            // btnXoaTyGia
            // 
            this.btnXoaTyGia.Location = new System.Drawing.Point(250, 8);
            this.btnXoaTyGia.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnXoaTyGia.Name = "btnXoaTyGia";
            this.btnXoaTyGia.Size = new System.Drawing.Size(80, 34);
            this.btnXoaTyGia.TabIndex = 3;
            this.btnXoaTyGia.Text = "Xóa";
            this.btnXoaTyGia.UseVisualStyleBackColor = true;
            this.btnXoaTyGia.Click += new System.EventHandler(this.btnXoaTyGia_Click);
            // 
            // btnLuuTyGia
            // 
            this.btnLuuTyGia.Location = new System.Drawing.Point(164, 8);
            this.btnLuuTyGia.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnLuuTyGia.Name = "btnLuuTyGia";
            this.btnLuuTyGia.Size = new System.Drawing.Size(80, 34);
            this.btnLuuTyGia.TabIndex = 2;
            this.btnLuuTyGia.Text = "Lưu";
            this.btnLuuTyGia.UseVisualStyleBackColor = true;
            this.btnLuuTyGia.Click += new System.EventHandler(this.btnLuuTyGia_Click);
            // 
            // btnSuaTyGia
            // 
            this.btnSuaTyGia.Location = new System.Drawing.Point(78, 8);
            this.btnSuaTyGia.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnSuaTyGia.Name = "btnSuaTyGia";
            this.btnSuaTyGia.Size = new System.Drawing.Size(80, 34);
            this.btnSuaTyGia.TabIndex = 1;
            this.btnSuaTyGia.Text = "Sửa";
            this.btnSuaTyGia.UseVisualStyleBackColor = true;
            this.btnSuaTyGia.Click += new System.EventHandler(this.btnSuaTyGia_Click);
            // 
            // btnMoiTyGia
            // 
            this.btnMoiTyGia.Location = new System.Drawing.Point(-8, 8);
            this.btnMoiTyGia.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnMoiTyGia.Name = "btnMoiTyGia";
            this.btnMoiTyGia.Size = new System.Drawing.Size(80, 34);
            this.btnMoiTyGia.TabIndex = 0;
            this.btnMoiTyGia.Text = "Thêm mới";
            this.btnMoiTyGia.UseVisualStyleBackColor = true;
            this.btnMoiTyGia.Click += new System.EventHandler(this.btnMoiTyGia_Click);
            // 
            // FormCaiDatHeThong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tabControlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormCaiDatHeThong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cài Đặt Hệ Thống";
            this.Load += new System.EventHandler(this.FormCaiDatHeThong_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabCongTy.ResumeLayout(false);
            this.tabCongTy.PerformLayout();
            this.tabThue.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpThueList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridThue)).EndInit();
            this.panel1.ResumeLayout(false);
            this.grpThueDetail.ResumeLayout(false);
            this.grpThueDetail.PerformLayout();
            this.panelThueButtons.ResumeLayout(false);
            this.tabTyGia.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.grpTyGiaList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTyGia)).EndInit();
            this.panel2.ResumeLayout(false);
            this.grpTyGiaDetail.ResumeLayout(false);
            this.grpTyGiaDetail.PerformLayout();
            this.panelTyGiaButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabCongTy;
        private System.Windows.Forms.TabPage tabThue;
        private System.Windows.Forms.TabPage tabTyGia;
        private System.Windows.Forms.Button btnLuuCongTy;
        private System.Windows.Forms.TextBox txtGhiChuCty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMST;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmailCty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSDTCty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDiaChiCty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenCty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpThueList;
        private System.Windows.Forms.DataGridView gridThue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpThueDetail;
        private System.Windows.Forms.FlowLayoutPanel panelThueButtons;
        private System.Windows.Forms.Button btnNapThue;
        private System.Windows.Forms.Button btnXoaThue;
        private System.Windows.Forms.Button btnLuuThue;
        private System.Windows.Forms.Button btnSuaThue;
        private System.Windows.Forms.Button btnMoiThue;
        private System.Windows.Forms.TextBox txtGhiChuThue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTiLeThue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMaThue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox grpTyGiaList;
        private System.Windows.Forms.DataGridView gridTyGia;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox grpTyGiaDetail;
        private System.Windows.Forms.TextBox txtGiaTri;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpNgayTyGia;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboTienTe;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtIDTyGia;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.FlowLayoutPanel panelTyGiaButtons;
        private System.Windows.Forms.Button btnNapTyGia;
        private System.Windows.Forms.Button btnXoaTyGia;
        private System.Windows.Forms.Button btnLuuTyGia;
        private System.Windows.Forms.Button btnSuaTyGia;
        private System.Windows.Forms.Button btnMoiTyGia;
    }
}