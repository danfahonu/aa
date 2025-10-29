namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormBangBaoGia
    {
        private System.ComponentModel.IContainer components = null;
        #region
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.split = new System.Windows.Forms.SplitContainer();
            this.groupList = new System.Windows.Forms.GroupBox();
            this.grid = new System.Windows.Forms.DataGridView();
            this.panelInput = new System.Windows.Forms.Panel();
            this.btnNap = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnMoi = new System.Windows.Forms.Button();
            this.numGia = new System.Windows.Forms.NumericUpDown();
            this.lblGia = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.lblNgay = new System.Windows.Forms.Label();
            this.cboHang = new System.Windows.Forms.ComboBox();
            this.lblHang = new System.Windows.Forms.Label();
            this.table.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.groupList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGia)).BeginInit();
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Controls.Add(this.split, 0, 0);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.RowCount = 1;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Size = new System.Drawing.Size(980, 600);
            this.table.TabIndex = 0;
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(3, 3);
            this.split.Name = "split";
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.groupList);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.panelInput);
            this.split.Size = new System.Drawing.Size(974, 594);
            this.split.SplitterDistance = 620;
            this.split.TabIndex = 0;
            // 
            // groupList
            // 
            this.groupList.Controls.Add(this.grid);
            this.groupList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupList.Location = new System.Drawing.Point(0, 0);
            this.groupList.Name = "groupList";
            this.groupList.Size = new System.Drawing.Size(620, 594);
            this.groupList.TabIndex = 0;
            this.groupList.TabStop = false;
            this.groupList.Text = "Bảng giá";
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(3, 19);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowTemplate.Height = 25;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(614, 572);
            this.grid.TabIndex = 0;
            this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            // 
            // panelInput
            // 
            this.panelInput.Controls.Add(this.btnNap);
            this.panelInput.Controls.Add(this.btnXoa);
            this.panelInput.Controls.Add(this.btnLuu);
            this.panelInput.Controls.Add(this.btnSua);
            this.panelInput.Controls.Add(this.btnMoi);
            this.panelInput.Controls.Add(this.numGia);
            this.panelInput.Controls.Add(this.lblGia);
            this.panelInput.Controls.Add(this.dtpNgay);
            this.panelInput.Controls.Add(this.lblNgay);
            this.panelInput.Controls.Add(this.cboHang);
            this.panelInput.Controls.Add(this.lblHang);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInput.Location = new System.Drawing.Point(0, 0);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(350, 594);
            this.panelInput.TabIndex = 0;
            // 
            // btnNap
            // 
            this.btnNap.Location = new System.Drawing.Point(265, 180);
            this.btnNap.Name = "btnNap";
            this.btnNap.Size = new System.Drawing.Size(78, 28);
            this.btnNap.TabIndex = 8;
            this.btnNap.Text = "Nạp lại";
            this.btnNap.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(180, 180);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(80, 28);
            this.btnXoa.TabIndex = 7;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(95, 180);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(80, 28);
            this.btnLuu.TabIndex = 6;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(180, 140);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(80, 28);
            this.btnSua.TabIndex = 5;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnMoi
            // 
            this.btnMoi.Location = new System.Drawing.Point(95, 140);
            this.btnMoi.Name = "btnMoi";
            this.btnMoi.Size = new System.Drawing.Size(80, 28);
            this.btnMoi.TabIndex = 4;
            this.btnMoi.Text = "Thêm mới";
            this.btnMoi.UseVisualStyleBackColor = true;
            this.btnMoi.Click += new System.EventHandler(this.btnMoi_Click);
            // 
            // numGia
            // 
            this.numGia.DecimalPlaces = 2;
            this.numGia.Location = new System.Drawing.Point(95, 96);
            this.numGia.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numGia.Name = "numGia";
            this.numGia.Size = new System.Drawing.Size(250, 23);
            this.numGia.TabIndex = 3;
            // 
            // lblGia
            // 
            this.lblGia.AutoSize = true;
            this.lblGia.Location = new System.Drawing.Point(16, 100);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(47, 15);
            this.lblGia.TabIndex = 7;
            this.lblGia.Text = "Đơn giá";
            // 
            // dtpNgay
            // 
            this.dtpNgay.Location = new System.Drawing.Point(95, 58);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(250, 23);
            this.dtpNgay.TabIndex = 2;
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location = new System.Drawing.Point(16, 62);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(76, 15);
            this.lblNgay.TabIndex = 5;
            this.lblNgay.Text = "Ngày áp dụng";
            // 
            // cboHang
            // 
            this.cboHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHang.FormattingEnabled = true;
            this.cboHang.Location = new System.Drawing.Point(95, 20);
            this.cboHang.Name = "cboHang";
            this.cboHang.Size = new System.Drawing.Size(250, 23);
            this.cboHang.TabIndex = 1;
            // 
            // lblHang
            // 
            this.lblHang.AutoSize = true;
            this.lblHang.Location = new System.Drawing.Point(16, 24);
            this.lblHang.Name = "lblHang";
            this.lblHang.Size = new System.Drawing.Size(61, 15);
            this.lblHang.TabIndex = 3;
            this.lblHang.Text = "Hàng hóa";
            // 
            // FormBangBaoGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 600);
            this.Controls.Add(this.table);
            this.Name = "FormBangBaoGia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bảng báo giá";
            this.Load += new System.EventHandler(this.FormBangBaoGia_Load);
            this.table.ResumeLayout(false);
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.groupList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGia)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.GroupBox groupList;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.Label lblHang;
        private System.Windows.Forms.ComboBox cboHang;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.NumericUpDown numGia;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnNap;
        private System.Windows.Forms.Button btnMoi; // Added
        private System.Windows.Forms.Button btnSua; // Added
    }
}