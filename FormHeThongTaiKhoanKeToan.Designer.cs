namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormHeThongTaiKhoanKeToan
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
            this.grpDanhSach = new System.Windows.Forms.GroupBox();
            this.treeViewTK = new System.Windows.Forms.TreeView();
            this.panelChiTiet = new System.Windows.Forms.Panel();
            this.grpChiTiet = new System.Windows.Forms.GroupBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboTKMe = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numCap = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenTK = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSoTK = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNap = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnMoi = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            this.panelChiTiet.SuspendLayout();
            this.grpChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCap)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.grpDanhSach, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelChiTiet, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 561);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grpDanhSach
            // 
            this.grpDanhSach.Controls.Add(this.treeViewTK);
            this.grpDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDanhSach.Location = new System.Drawing.Point(13, 13);
            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Size = new System.Drawing.Size(476, 535);
            this.grpDanhSach.TabIndex = 0;
            this.grpDanhSach.TabStop = false;
            this.grpDanhSach.Text = "Cây Hệ thống tài khoản";
            // 
            // treeViewTK
            // 
            this.treeViewTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTK.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewTK.Location = new System.Drawing.Point(3, 19);
            this.treeViewTK.Name = "treeViewTK";
            this.treeViewTK.Size = new System.Drawing.Size(470, 513);
            this.treeViewTK.TabIndex = 0;
            this.treeViewTK.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTK_AfterSelect);
            // 
            // panelChiTiet
            // 
            this.panelChiTiet.Controls.Add(this.grpChiTiet);
            this.panelChiTiet.Controls.Add(this.panelButtons);
            this.panelChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChiTiet.Location = new System.Drawing.Point(495, 13);
            this.panelChiTiet.Name = "panelChiTiet";
            this.panelChiTiet.Size = new System.Drawing.Size(476, 535);
            this.panelChiTiet.TabIndex = 1;
            // 
            // grpChiTiet
            // 
            this.grpChiTiet.Controls.Add(this.txtGhiChu);
            this.grpChiTiet.Controls.Add(this.label5);
            this.grpChiTiet.Controls.Add(this.cboTKMe);
            this.grpChiTiet.Controls.Add(this.label4);
            this.grpChiTiet.Controls.Add(this.numCap);
            this.grpChiTiet.Controls.Add(this.label3);
            this.grpChiTiet.Controls.Add(this.txtTenTK);
            this.grpChiTiet.Controls.Add(this.label2);
            this.grpChiTiet.Controls.Add(this.txtSoTK);
            this.grpChiTiet.Controls.Add(this.label1);
            this.grpChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpChiTiet.Location = new System.Drawing.Point(0, 0);
            this.grpChiTiet.Name = "grpChiTiet";
            this.grpChiTiet.Size = new System.Drawing.Size(476, 485);
            this.grpChiTiet.TabIndex = 1;
            this.grpChiTiet.TabStop = false;
            this.grpChiTiet.Text = "Chi tiết Tài khoản";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Location = new System.Drawing.Point(113, 196);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(344, 96);
            this.txtGhiChu.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ghi chú:";
            // 
            // cboTKMe
            // 
            this.cboTKMe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTKMe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTKMe.FormattingEnabled = true;
            this.cboTKMe.Location = new System.Drawing.Point(113, 156);
            this.cboTKMe.Name = "cboTKMe";
            this.cboTKMe.Size = new System.Drawing.Size(344, 23);
            this.cboTKMe.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tài khoản mẹ:";
            // 
            // numCap
            // 
            this.numCap.Location = new System.Drawing.Point(113, 117);
            this.numCap.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCap.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCap.Name = "numCap";
            this.numCap.Size = new System.Drawing.Size(120, 23);
            this.numCap.TabIndex = 5;
            this.numCap.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cấp:";
            // 
            // txtTenTK
            // 
            this.txtTenTK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenTK.Location = new System.Drawing.Point(113, 78);
            this.txtTenTK.Name = "txtTenTK";
            this.txtTenTK.Size = new System.Drawing.Size(344, 23);
            this.txtTenTK.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên tài khoản:";
            // 
            // txtSoTK
            // 
            this.txtSoTK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSoTK.Location = new System.Drawing.Point(113, 39);
            this.txtSoTK.Name = "txtSoTK";
            this.txtSoTK.Size = new System.Drawing.Size(344, 23);
            this.txtSoTK.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số tài khoản:";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnNap);
            this.panelButtons.Controls.Add(this.btnXoa);
            this.panelButtons.Controls.Add(this.btnLuu);
            this.panelButtons.Controls.Add(this.btnSua);
            this.panelButtons.Controls.Add(this.btnMoi);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelButtons.Location = new System.Drawing.Point(0, 485);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(476, 50);
            this.panelButtons.TabIndex = 0;
            // 
            // btnNap
            // 
            this.btnNap.Location = new System.Drawing.Point(388, 8);
            this.btnNap.Margin = new System.Windows.Forms.Padding(3, 8, 8, 3);
            this.btnNap.Name = "btnNap";
            this.btnNap.Size = new System.Drawing.Size(80, 34);
            this.btnNap.TabIndex = 4;
            this.btnNap.Text = "Nạp lại";
            this.btnNap.UseVisualStyleBackColor = true;
            this.btnNap.Click += new System.EventHandler(this.btnNap_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(302, 8);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(80, 34);
            this.btnXoa.TabIndex = 3;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(216, 8);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(80, 34);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(130, 8);
            this.btnSua.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(80, 34);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnMoi
            // 
            this.btnMoi.Location = new System.Drawing.Point(44, 8);
            this.btnMoi.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnMoi.Name = "btnMoi";
            this.btnMoi.Size = new System.Drawing.Size(80, 34);
            this.btnMoi.TabIndex = 0;
            this.btnMoi.Text = "Thêm mới";
            this.btnMoi.UseVisualStyleBackColor = true;
            this.btnMoi.Click += new System.EventHandler(this.btnMoi_Click);
            // 
            // FormHeThongTaiKhoanKeToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormHeThongTaiKhoanKeToan";
            this.Text = "Hệ Thống Tài Khoản Kế Toán";
            this.Load += new System.EventHandler(this.FormHeThongTaiKhoanKeToan_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpDanhSach.ResumeLayout(false);
            this.panelChiTiet.ResumeLayout(false);
            this.grpChiTiet.ResumeLayout(false);
            this.grpChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCap)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.TreeView treeViewTK;
        private System.Windows.Forms.Panel panelChiTiet;
        private System.Windows.Forms.GroupBox grpChiTiet;
        private System.Windows.Forms.FlowLayoutPanel panelButtons;
        private System.Windows.Forms.TextBox txtTenTK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSoTK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numCap;
        private System.Windows.Forms.ComboBox cboTKMe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnNap;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnMoi;
    }
}