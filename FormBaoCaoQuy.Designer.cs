namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormBaoCaoQuy
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
            this.cboLoaiPhieu = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.gridBaoCao = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.staSoDuDau = new System.Windows.Forms.ToolStripStatusLabel();
            this.staTongThu = new System.Windows.Forms.ToolStripStatusLabel();
            this.staTongChi = new System.Windows.Forms.ToolStripStatusLabel();
            this.staSoDuCuoi = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBaoCao)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnXem);
            this.panelTop.Controls.Add(this.cboLoaiPhieu);
            this.panelTop.Controls.Add(this.label3);
            this.panelTop.Controls.Add(this.dtpDenNgay);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.dtpTuNgay);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 80);
            this.panelTop.TabIndex = 0;
            // 
            // btnXem
            // 
            this.btnXem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXem.Location = new System.Drawing.Point(829, 22);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(121, 36);
            this.btnXem.TabIndex = 6;
            this.btnXem.Text = "Xem Báo Cáo";
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // cboLoaiPhieu
            // 
            this.cboLoaiPhieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiPhieu.FormattingEnabled = true;
            this.cboLoaiPhieu.Location = new System.Drawing.Point(604, 29);
            this.cboLoaiPhieu.Name = "cboLoaiPhieu";
            this.cboLoaiPhieu.Size = new System.Drawing.Size(185, 23);
            this.cboLoaiPhieu.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(532, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Loại phiếu:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(326, 29);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(148, 23);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(261, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(80, 29);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(148, 23);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày:";
            // 
            // gridBaoCao
            // 
            this.gridBaoCao.AllowUserToAddRows = false;
            this.gridBaoCao.AllowUserToDeleteRows = false;
            this.gridBaoCao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBaoCao.Location = new System.Drawing.Point(0, 80);
            this.gridBaoCao.Name = "gridBaoCao";
            this.gridBaoCao.ReadOnly = true;
            this.gridBaoCao.RowTemplate.Height = 25;
            this.gridBaoCao.Size = new System.Drawing.Size(984, 459);
            this.gridBaoCao.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staSoDuDau,
            this.staTongThu,
            this.staTongChi,
            this.staSoDuCuoi});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // staSoDuDau
            // 
            this.staSoDuDau.Name = "staSoDuDau";
            this.staSoDuDau.Size = new System.Drawing.Size(78, 17);
            this.staSoDuDau.Text = "Số dư đầu: 0";
            // 
            // staTongThu
            // 
            this.staTongThu.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.staTongThu.Name = "staTongThu";
            this.staTongThu.Size = new System.Drawing.Size(74, 17);
            this.staTongThu.Text = "Tổng thu: 0";
            // 
            // staTongChi
            // 
            this.staTongChi.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.staTongChi.Name = "staTongChi";
            this.staTongChi.Size = new System.Drawing.Size(71, 17);
            this.staTongChi.Text = "Tổng chi: 0";
            // 
            // staSoDuCuoi
            // 
            this.staSoDuCuoi.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.staSoDuCuoi.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staSoDuCuoi.Name = "staSoDuCuoi";
            this.staSoDuCuoi.Size = new System.Drawing.Size(86, 17);
            this.staSoDuCuoi.Text = "Số dư cuối: 0";
            // 
            // FormBaoCaoQuy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.gridBaoCao);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormBaoCaoQuy";
            this.Text = "Báo Cáo Sổ Quỹ";
            this.Load += new System.EventHandler(this.FormBaoCaoQuy_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBaoCao)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.DataGridView gridBaoCao;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboLoaiPhieu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.ToolStripStatusLabel staSoDuDau;
        private System.Windows.Forms.ToolStripStatusLabel staTongThu;
        private System.Windows.Forms.ToolStripStatusLabel staTongChi;
        private System.Windows.Forms.ToolStripStatusLabel staSoDuCuoi;
    }
}