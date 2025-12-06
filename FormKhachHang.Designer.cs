namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormKhachHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnHuy = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.btnLuu = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.btnXoa = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.btnSua = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.btnThem = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.dgvKhachHang = new DoAnLapTrinhQuanLy.Controls.ModernDataGrid();
            this.txtMaKH = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.txtTenKH = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.txtSDT = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.txtDiaChi = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.txtEmail = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.txtGhiChu = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.lblMaKH = new System.Windows.Forms.Label();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 60);
            this.pnlHeader.TabIndex = 0;

            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblHeaderTitle.Location = new System.Drawing.Point(20, 15);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(298, 30);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "DANH MỤC KHÁCH HÀNG";

            // 
            // pnlInput
            // 
            this.pnlInput.BackColor = System.Drawing.Color.White;
            this.pnlInput.Controls.Add(this.lblGhiChu);
            this.pnlInput.Controls.Add(this.lblEmail);
            this.pnlInput.Controls.Add(this.lblDiaChi);
            this.pnlInput.Controls.Add(this.lblSDT);
            this.pnlInput.Controls.Add(this.lblTenKH);
            this.pnlInput.Controls.Add(this.lblMaKH);
            this.pnlInput.Controls.Add(this.txtGhiChu);
            this.pnlInput.Controls.Add(this.txtEmail);
            this.pnlInput.Controls.Add(this.txtDiaChi);
            this.pnlInput.Controls.Add(this.txtSDT);
            this.pnlInput.Controls.Add(this.txtTenKH);
            this.pnlInput.Controls.Add(this.txtMaKH);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.Location = new System.Drawing.Point(0, 60);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(1200, 160);
            this.pnlInput.TabIndex = 1;

            // Labels setup (DimGray, Segoe UI 10)
            System.Drawing.Font labelFont = new System.Drawing.Font("Segoe UI", 10F);
            System.Drawing.Color labelColor = System.Drawing.Color.DimGray;

            this.lblMaKH.Text = "Mã KH:";
            this.lblMaKH.Location = new System.Drawing.Point(30, 20);
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Font = labelFont;
            this.lblMaKH.ForeColor = labelColor;

            this.lblTenKH.Text = "Tên KH:";
            this.lblTenKH.Location = new System.Drawing.Point(30, 65);
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Font = labelFont;
            this.lblTenKH.ForeColor = labelColor;

            this.lblSDT.Text = "SĐT:";
            this.lblSDT.Location = new System.Drawing.Point(30, 110);
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = labelFont;
            this.lblSDT.ForeColor = labelColor;

            this.lblDiaChi.Text = "Địa Chỉ:";
            this.lblDiaChi.Location = new System.Drawing.Point(400, 20);
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = labelFont;
            this.lblDiaChi.ForeColor = labelColor;

            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new System.Drawing.Point(400, 65);
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = labelFont;
            this.lblEmail.ForeColor = labelColor;

            this.lblGhiChu.Text = "Ghi Chú:";
            this.lblGhiChu.Location = new System.Drawing.Point(400, 110);
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = labelFont;
            this.lblGhiChu.ForeColor = labelColor;

            // Inputs setup
            this.txtMaKH.Location = new System.Drawing.Point(100, 15);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(200, 30);
            this.txtMaKH.TabIndex = 0;

            this.txtTenKH.Location = new System.Drawing.Point(100, 60);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(200, 30);
            this.txtTenKH.TabIndex = 1;

            this.txtSDT.Location = new System.Drawing.Point(100, 105);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(200, 30);
            this.txtSDT.TabIndex = 2;

            this.txtDiaChi.Location = new System.Drawing.Point(480, 15);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(300, 30);
            this.txtDiaChi.TabIndex = 3;

            this.txtEmail.Location = new System.Drawing.Point(480, 60);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(300, 30);
            this.txtEmail.TabIndex = 4;

            this.txtGhiChu.Location = new System.Drawing.Point(480, 105);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(300, 30);
            this.txtGhiChu.TabIndex = 5;

            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnHuy);
            this.pnlFooter.Controls.Add(this.btnLuu);
            this.pnlFooter.Controls.Add(this.btnXoa);
            this.pnlFooter.Controls.Add(this.btnSua);
            this.pnlFooter.Controls.Add(this.btnThem);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 620);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1200, 80);
            this.pnlFooter.TabIndex = 3;

            // Buttons (Right Aligned, Styled)
            // Order: Them (580) -> Sua (700) -> Xoa (820) -> Luu (940) -> Huy (1060)
            // Width 110, Spacing 10. Total 5 buttons.
            // StartX = 1200 - (5*110 + 5*10) = 1200 - 600 = 600?
            // Let's use:
            // Huy: 1060 (Rightmost, keeping prev alignment)
            // Luu: 940
            // Xoa: 820
            // Sua: 700
            // Them: 580

            // btnThem (Blue)
            this.btnThem.Text = "Thêm";
            this.btnThem.Location = new System.Drawing.Point(580, 18);
            this.btnThem.Size = new System.Drawing.Size(110, 45);
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.BorderRadius = 20;
            this.btnThem.Name = "btnThem";
            this.btnThem.TabIndex = 0;

            // btnSua (Orange - New)
            this.btnSua.Text = "Sửa";
            this.btnSua.Location = new System.Drawing.Point(700, 18);
            this.btnSua.Size = new System.Drawing.Size(110, 45);
            this.btnSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.BorderRadius = 20;
            this.btnSua.Name = "btnSua";
            this.btnSua.TabIndex = 1;

            // btnXoa (Red)
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Location = new System.Drawing.Point(820, 18);
            this.btnXoa.Size = new System.Drawing.Size(110, 45);
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.BorderRadius = 20;
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.TabIndex = 2;

            // btnLuu (Dark)
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Location = new System.Drawing.Point(940, 18);
            this.btnLuu.Size = new System.Drawing.Size(110, 45);
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.BorderRadius = 20;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.TabIndex = 3;

            // btnHuy (White)
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Location = new System.Drawing.Point(1060, 18);
            this.btnHuy.Size = new System.Drawing.Size(110, 45);
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.BackColor = System.Drawing.Color.White;
            this.btnHuy.ForeColor = System.Drawing.Color.Black;
            this.btnHuy.BorderRadius = 20;
            this.btnHuy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(66)))));
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.TabIndex = 4;

            // 
            // dgvKhachHang
            // 
            this.dgvKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhachHang.Location = new System.Drawing.Point(0, 220); // Top = Header(60) + Input(160)
            this.dgvKhachHang.Name = "dgvKhachHang";
            this.dgvKhachHang.Padding = new System.Windows.Forms.Padding(15);
            this.dgvKhachHang.Size = new System.Drawing.Size(1200, 400);
            this.dgvKhachHang.TabIndex = 2;

            // 
            // FormKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.dgvKhachHang);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FormKhachHang";
            this.Text = "Danh Mục Khách Hàng";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.Panel pnlFooter;
        private DoAnLapTrinhQuanLy.Controls.ModernDataGrid dgvKhachHang;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnThem;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnSua;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnXoa;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnLuu;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnHuy;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtMaKH;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtTenKH;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtSDT;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtDiaChi;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtEmail;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtGhiChu;
        private System.Windows.Forms.Label lblMaKH;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblGhiChu;
    }
}
