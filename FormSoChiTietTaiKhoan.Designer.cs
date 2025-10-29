namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormSoChiTietTaiKhoan
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnXem = new System.Windows.Forms.Button();
            this.cboTaiKhoan = new System.Windows.Forms.ComboBox();
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.gridSoChiTiet = new System.Windows.Forms.DataGridView();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.lblDuCuoiKy = new System.Windows.Forms.Label();
            this.lblPhatSinhTrongKy = new System.Windows.Forms.Label();
            this.lblDuDauKy = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSoChiTiet)).BeginInit();
            this.panelSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnXem);
            this.panelTop.Controls.Add(this.cboTaiKhoan);
            this.panelTop.Controls.Add(this.lblTaiKhoan);
            this.panelTop.Controls.Add(this.dtpDenNgay);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.dtpTuNgay);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 80);
            this.panelTop.TabIndex = 1;
            // 
            // btnXem
            // 
            this.btnXem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXem.Location = new System.Drawing.Point(822, 11);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(121, 36);
            this.btnXem.TabIndex = 8;
            this.btnXem.Text = "Xem Sổ";
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // cboTaiKhoan
            // 
            this.cboTaiKhoan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTaiKhoan.FormattingEnabled = true;
            this.cboTaiKhoan.Location = new System.Drawing.Point(89, 19);
            this.cboTaiKhoan.Name = "cboTaiKhoan";
            this.cboTaiKhoan.Size = new System.Drawing.Size(262, 23);
            this.cboTaiKhoan.TabIndex = 7;
            // 
            // lblTaiKhoan
            // 
            this.lblTaiKhoan.AutoSize = true;
            this.lblTaiKhoan.Location = new System.Drawing.Point(19, 22);
            this.lblTaiKhoan.Name = "lblTaiKhoan";
            this.lblTaiKhoan.Size = new System.Drawing.Size(64, 15);
            this.lblTaiKhoan.TabIndex = 6;
            this.lblTaiKhoan.Text = "Tài khoản:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(647, 19);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(139, 23);
            this.dtpDenNgay.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(582, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Đến ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(427, 19);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(139, 23);
            this.dtpTuNgay.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(369, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Từ ngày:";
            // 
            // gridSoChiTiet
            // 
            this.gridSoChiTiet.AllowUserToAddRows = false;
            this.gridSoChiTiet.AllowUserToDeleteRows = false;
            this.gridSoChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSoChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSoChiTiet.Location = new System.Drawing.Point(0, 80);
            this.gridSoChiTiet.Name = "gridSoChiTiet";
            this.gridSoChiTiet.ReadOnly = true;
            this.gridSoChiTiet.RowTemplate.Height = 25;
            this.gridSoChiTiet.Size = new System.Drawing.Size(984, 401);
            this.gridSoChiTiet.TabIndex = 2;
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelSummary.Controls.Add(this.lblDuCuoiKy);
            this.panelSummary.Controls.Add(this.lblPhatSinhTrongKy);
            this.panelSummary.Controls.Add(this.lblDuDauKy);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSummary.Location = new System.Drawing.Point(0, 481);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(984, 80);
            this.panelSummary.TabIndex = 3;
            // 
            // lblDuCuoiKy
            // 
            this.lblDuCuoiKy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDuCuoiKy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuCuoiKy.Location = new System.Drawing.Point(674, 14);
            this.lblDuCuoiKy.Name = "lblDuCuoiKy";
            this.lblDuCuoiKy.Size = new System.Drawing.Size(298, 52);
            this.lblDuCuoiKy.TabIndex = 2;
            this.lblDuCuoiKy.Text = "Dư Cuối Kỳ: 0";
            this.lblDuCuoiKy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPhatSinhTrongKy
            // 
            this.lblPhatSinhTrongKy.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPhatSinhTrongKy.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhatSinhTrongKy.Location = new System.Drawing.Point(336, 14);
            this.lblPhatSinhTrongKy.Name = "lblPhatSinhTrongKy";
            this.lblPhatSinhTrongKy.Size = new System.Drawing.Size(315, 52);
            this.lblPhatSinhTrongKy.TabIndex = 1;
            this.lblPhatSinhTrongKy.Text = "Nợ trong kỳ: 0\r\nCó trong kỳ: 0";
            this.lblPhatSinhTrongKy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDuDauKy
            // 
            this.lblDuDauKy.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuDauKy.Location = new System.Drawing.Point(12, 14);
            this.lblDuDauKy.Name = "lblDuDauKy";
            this.lblDuDauKy.Size = new System.Drawing.Size(298, 52);
            this.lblDuDauKy.TabIndex = 0;
            this.lblDuDauKy.Text = "Dư Đầu Kỳ: 0";
            this.lblDuDauKy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormSoChiTietTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.gridSoChiTiet);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormSoChiTietTaiKhoan";
            this.Text = "Sổ Chi Tiết Tài Khoản (Sổ Cái)";
            this.Load += new System.EventHandler(this.FormSoChiTietTaiKhoan_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSoChiTiet)).EndInit();
            this.panelSummary.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.DataGridView gridSoChiTiet;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.ComboBox cboTaiKhoan;
        private System.Windows.Forms.Label lblTaiKhoan;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDuCuoiKy;
        private System.Windows.Forms.Label lblPhatSinhTrongKy;
        private System.Windows.Forms.Label lblDuDauKy;
    }
}