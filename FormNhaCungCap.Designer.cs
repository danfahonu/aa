namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormNhaCungCap
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.dgvNhaCungCap = new DoAnLapTrinhQuanLy.Controls.ModernDataGrid();

            // Buttons
            this.btnHuy = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.btnLuu = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.btnXoa = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.btnSua = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.btnThem = new DoAnLapTrinhQuanLy.Controls.ModernButton();

            // Inputs
            this.txtMaNCC = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.txtTenNCC = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.txtDiaChi = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.txtSDT = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.txtEmail = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.txtMSThue = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.txtGhiChu = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();

            // Labels
            this.lblMaNCC = new System.Windows.Forms.Label();
            this.lblTenNCC = new System.Windows.Forms.Label();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblMSThue = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhaCungCap)).BeginInit();
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
            this.lblHeaderTitle.Size = new System.Drawing.Size(320, 30);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "DANH MỤC NHÀ CUNG CẤP";

            // 
            // pnlInput
            // 
            this.pnlInput.BackColor = System.Drawing.Color.White;
            // Add Controls
            this.pnlInput.Controls.Add(this.lblGhiChu);
            this.pnlInput.Controls.Add(this.txtGhiChu);
            this.pnlInput.Controls.Add(this.lblMSThue);
            this.pnlInput.Controls.Add(this.txtMSThue);
            this.pnlInput.Controls.Add(this.lblEmail);
            this.pnlInput.Controls.Add(this.txtEmail);
            this.pnlInput.Controls.Add(this.lblSDT);
            this.pnlInput.Controls.Add(this.txtSDT);
            this.pnlInput.Controls.Add(this.lblDiaChi);
            this.pnlInput.Controls.Add(this.txtDiaChi);
            this.pnlInput.Controls.Add(this.lblTenNCC);
            this.pnlInput.Controls.Add(this.txtTenNCC);
            this.pnlInput.Controls.Add(this.lblMaNCC);
            this.pnlInput.Controls.Add(this.txtMaNCC);

            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.Location = new System.Drawing.Point(0, 60);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(1200, 200); // 4 rows? 
            this.pnlInput.TabIndex = 1;

            // Common Font/Color
            System.Drawing.Font lblFont = new System.Drawing.Font("Segoe UI", 10F);
            System.Drawing.Color lblColor = System.Drawing.Color.DimGray;

            // Row 1: MaNCC, TenNCC
            this.lblMaNCC.Text = "Mã NCC:";
            this.lblMaNCC.Location = new System.Drawing.Point(30, 20);
            this.lblMaNCC.AutoSize = true;
            this.lblMaNCC.Font = lblFont;
            this.lblMaNCC.ForeColor = lblColor;

            this.txtMaNCC.Location = new System.Drawing.Point(120, 15);
            this.txtMaNCC.Name = "txtMaNCC";
            this.txtMaNCC.Size = new System.Drawing.Size(180, 30);

            this.lblTenNCC.Text = "Tên NCC:";
            this.lblTenNCC.Location = new System.Drawing.Point(350, 20);
            this.lblTenNCC.AutoSize = true;
            this.lblTenNCC.Font = lblFont;
            this.lblTenNCC.ForeColor = lblColor;

            this.txtTenNCC.Location = new System.Drawing.Point(430, 15);
            this.txtTenNCC.Name = "txtTenNCC";
            this.txtTenNCC.Size = new System.Drawing.Size(300, 30);

            // Row 2: DiaChi, SDT
            this.lblDiaChi.Text = "Địa Chỉ:";
            this.lblDiaChi.Location = new System.Drawing.Point(30, 65);
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = lblFont;
            this.lblDiaChi.ForeColor = lblColor;

            this.txtDiaChi.Location = new System.Drawing.Point(120, 60);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(610, 30); // Span across

            this.lblSDT.Text = "SĐT:";
            this.lblSDT.Location = new System.Drawing.Point(750, 65);
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = lblFont;
            this.lblSDT.ForeColor = lblColor;

            this.txtSDT.Location = new System.Drawing.Point(800, 60);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(150, 30);

            // Row 3: Email, MSThue
            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new System.Drawing.Point(30, 110);
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = lblFont;
            this.lblEmail.ForeColor = lblColor;

            this.txtEmail.Location = new System.Drawing.Point(120, 105);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 30);

            this.lblMSThue.Text = "MS Thuế:";
            this.lblMSThue.Location = new System.Drawing.Point(400, 110);
            this.lblMSThue.AutoSize = true;
            this.lblMSThue.Font = lblFont;
            this.lblMSThue.ForeColor = lblColor;

            this.txtMSThue.Location = new System.Drawing.Point(480, 105);
            this.txtMSThue.Name = "txtMSThue";
            this.txtMSThue.Size = new System.Drawing.Size(200, 30);

            // Row 4: Ghi Chu
            this.lblGhiChu.Text = "Ghi Chú:";
            this.lblGhiChu.Location = new System.Drawing.Point(30, 155);
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = lblFont;
            this.lblGhiChu.ForeColor = lblColor;

            this.txtGhiChu.Location = new System.Drawing.Point(120, 150);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(610, 30);

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

            // Buttons - Align Right
            // Huy(1060), Luu(940), Xoa(820), Sua(700), Them(580)

            // btnThem
            this.btnThem.Text = "Thêm";
            this.btnThem.Location = new System.Drawing.Point(580, 18);
            this.btnThem.Size = new System.Drawing.Size(110, 45);
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.BorderRadius = 20;

            // btnSua
            this.btnSua.Text = "Sửa";
            this.btnSua.Location = new System.Drawing.Point(700, 18);
            this.btnSua.Size = new System.Drawing.Size(110, 45);
            this.btnSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.BorderRadius = 20;

            // btnXoa
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Location = new System.Drawing.Point(820, 18);
            this.btnXoa.Size = new System.Drawing.Size(110, 45);
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.BorderRadius = 20;

            // btnLuu
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Location = new System.Drawing.Point(940, 18);
            this.btnLuu.Size = new System.Drawing.Size(110, 45);
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.BorderRadius = 20;

            // btnHuy
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Location = new System.Drawing.Point(1060, 18);
            this.btnHuy.Size = new System.Drawing.Size(110, 45);
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.BackColor = System.Drawing.Color.White;
            this.btnHuy.ForeColor = System.Drawing.Color.Black;
            this.btnHuy.BorderRadius = 20;
            this.btnHuy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(66)))));

            // 
            // dgvNhaCungCap
            // 
            this.dgvNhaCungCap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhaCungCap.Location = new System.Drawing.Point(0, 260); // Header(60) + Inp(200)
            this.dgvNhaCungCap.Name = "dgvNhaCungCap";
            this.dgvNhaCungCap.Padding = new System.Windows.Forms.Padding(15);
            this.dgvNhaCungCap.Size = new System.Drawing.Size(1200, 360);
            this.dgvNhaCungCap.TabIndex = 2;

            // 
            // FormNhaCungCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.dgvNhaCungCap);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FormNhaCungCap";
            this.Text = "Danh Mục Nhà Cung Cấp";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhaCungCap)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.Panel pnlFooter;
        private DoAnLapTrinhQuanLy.Controls.ModernDataGrid dgvNhaCungCap;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnThem;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnSua;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnXoa;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnLuu;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnHuy;

        // Inputs
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtMaNCC;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtTenNCC;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtDiaChi;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtSDT;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtEmail;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtMSThue;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtGhiChu;

        // Labels
        private System.Windows.Forms.Label lblMaNCC;
        private System.Windows.Forms.Label lblTenNCC;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblMSThue;
        private System.Windows.Forms.Label lblGhiChu;
    }
}
