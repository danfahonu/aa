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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.staUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.staDb = new System.Windows.Forms.ToolStripStatusLabel();
            this.staTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.treeChucNang = new System.Windows.Forms.TreeView();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelMain = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnHeThong = new System.Windows.Forms.ToolStripButton();
            this.btnDanhMuc = new System.Windows.Forms.ToolStripButton();
            this.btnNghiepVu = new System.Windows.Forms.ToolStripButton();
            this.btnBaoCao = new System.Windows.Forms.ToolStripButton();
            this.btnHoiDapAI = new System.Windows.Forms.ToolStripButton(); // KHAI BÁO NÚT HỎI ĐÁP AI
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDoiMatKhau = new System.Windows.Forms.ToolStripButton();
            this.btnDangXuat = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staUser,
            this.staDb,
            this.staTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1264, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // staUser
            // 
            this.staUser.Name = "staUser";
            this.staUser.Size = new System.Drawing.Size(113, 17);
            this.staUser.Text = "User: Chưa đăng nhập";
            // 
            // staDb
            // 
            this.staDb.Name = "staDb";
            this.staDb.Size = new System.Drawing.Size(1114, 17);
            this.staDb.Spring = true;
            this.staDb.Text = "DB: (Not connected)";
            this.staDb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // staTime
            // 
            this.staTime.Name = "staTime";
            this.staTime.Size = new System.Drawing.Size(22, 17);
            this.staTime.Text = "Time";
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.treeChucNang);
            this.panelLeft.Controls.Add(this.panelLogo);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 52);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(250, 587);
            this.panelLeft.TabIndex = 1;
            // 
            // treeChucNang
            // 
            this.treeChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeChucNang.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeChucNang.Location = new System.Drawing.Point(0, 55);
            this.treeChucNang.Name = "treeChucNang";
            this.treeChucNang.Size = new System.Drawing.Size(250, 532);
            this.treeChucNang.TabIndex = 1;
            this.treeChucNang.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeChucNang_NodeMouseDoubleClick);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panelLogo.Controls.Add(this.label1);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(250, 55);
            this.panelLogo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "SALEGEAR";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(250, 52);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 587);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(253, 52);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1011, 587);
            this.panelMain.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnHeThong,
            this.btnDanhMuc,
            this.btnNghiepVu,
            this.btnBaoCao,
            this.btnHoiDapAI, // THÊM NÚT HỎI ĐÁP VÀO THANH CÔNG CỤ
            this.toolStripSeparator1,
            this.btnDoiMatKhau,
            this.btnDangXuat});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1264, 52);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnHeThong
            // 
            this.btnHeThong.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHeThong.Name = "btnHeThong";
            this.btnHeThong.Padding = new System.Windows.Forms.Padding(10);
            this.btnHeThong.Size = new System.Drawing.Size(95, 49);
            this.btnHeThong.Text = "Hệ Thống";
            this.btnHeThong.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHeThong.Click += new System.EventHandler(this.btnHeThong_Click);
            // 
            // btnDanhMuc
            // 
            this.btnDanhMuc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnDanhMuc.Padding = new System.Windows.Forms.Padding(10);
            this.btnDanhMuc.Size = new System.Drawing.Size(104, 49);
            this.btnDanhMuc.Text = "Danh Mục";
            this.btnDanhMuc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDanhMuc.Click += new System.EventHandler(this.btnDanhMuc_Click);
            // 
            // btnNghiepVu
            // 
            this.btnNghiepVu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNghiepVu.Name = "btnNghiepVu";
            this.btnNghiepVu.Padding = new System.Windows.Forms.Padding(10);
            this.btnNghiepVu.Size = new System.Drawing.Size(104, 49);
            this.btnNghiepVu.Text = "Nghiệp Vụ";
            this.btnNghiepVu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNghiepVu.Click += new System.EventHandler(this.btnNghiepVu_Click);
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Padding = new System.Windows.Forms.Padding(10);
            this.btnBaoCao.Size = new System.Drawing.Size(86, 49);
            this.btnBaoCao.Text = "Báo Cáo";
            this.btnBaoCao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            //
            // btnHoiDapAI
            // 
            this.btnHoiDapAI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHoiDapAI.Name = "btnHoiDapAI";
            this.btnHoiDapAI.Padding = new System.Windows.Forms.Padding(10);
            this.btnHoiDapAI.Size = new System.Drawing.Size(90, 49);
            this.btnHoiDapAI.Text = "Hỏi Đáp AI";
            this.btnHoiDapAI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHoiDapAI.Click += new System.EventHandler(this.mHoiDapAI_Click); // SỬ DỤNG LẠI EVENT HANDLER ĐÃ CÓ
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnDoiMatKhau.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Padding = new System.Windows.Forms.Padding(10);
            this.btnDoiMatKhau.Size = new System.Drawing.Size(119, 49);
            this.btnDoiMatKhau.Text = "Đổi Mật Khẩu";
            this.btnDoiMatKhau.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnDangXuat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Padding = new System.Windows.Forms.Padding(10);
            this.btnDangXuat.Size = new System.Drawing.Size(101, 49);
            this.btnDangXuat.Text = "Đăng Xuất";
            this.btnDangXuat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 661);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phần mềm kế toán kho StoreGearVN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripStatusLabel staUser;
        private System.Windows.Forms.ToolStripStatusLabel staDb;
        private System.Windows.Forms.ToolStripStatusLabel staTime;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeChucNang;
        private System.Windows.Forms.ToolStripButton btnHeThong;
        private System.Windows.Forms.ToolStripButton btnDanhMuc;
        private System.Windows.Forms.ToolStripButton btnNghiepVu;
        private System.Windows.Forms.ToolStripButton btnBaoCao;
        private System.Windows.Forms.ToolStripButton btnHoiDapAI; // THÊM KHAI BÁO BIẾN
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDoiMatKhau;
        private System.Windows.Forms.ToolStripButton btnDangXuat;
    }
}