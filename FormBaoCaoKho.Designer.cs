namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormBaoCaoKho
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
            this.chkXemChiTiet = new System.Windows.Forms.CheckBox();
            this.cboDoiTac = new System.Windows.Forms.ComboBox();
            this.lblDoiTac = new System.Windows.Forms.Label();
            this.btnXemBaoCao = new System.Windows.Forms.Button();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.gridBaoCao = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.staTongTien = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBaoCao)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.chkXemChiTiet);
            this.panelTop.Controls.Add(this.cboDoiTac);
            this.panelTop.Controls.Add(this.lblDoiTac);
            this.panelTop.Controls.Add(this.btnXemBaoCao);
            this.panelTop.Controls.Add(this.dtpDenNgay);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.dtpTuNgay);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1134, 110);
            this.panelTop.TabIndex = 0;
            // 
            // chkXemChiTiet
            // 
            this.chkXemChiTiet.AutoSize = true;
            this.chkXemChiTiet.Location = new System.Drawing.Point(19, 68);
            this.chkXemChiTiet.Name = "chkXemChiTiet";
            this.chkXemChiTiet.Size = new System.Drawing.Size(157, 19);
            this.chkXemChiTiet.TabIndex = 7;
            this.chkXemChiTiet.Text = "Xem chi ti?t theo v?t tu";
            this.chkXemChiTiet.UseVisualStyleBackColor = true;
            // 
            // cboDoiTac
            // 
            this.cboDoiTac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoiTac.FormattingEnabled = true;
            this.cboDoiTac.Location = new System.Drawing.Point(513, 24);
            this.cboDoiTac.Name = "cboDoiTac";
            this.cboDoiTac.Size = new System.Drawing.Size(262, 23);
            this.cboDoiTac.TabIndex = 6;
            // 
            // lblDoiTac
            // 
            this.lblDoiTac.AutoSize = true;
            this.lblDoiTac.Location = new System.Drawing.Point(459, 27);
            this.lblDoiTac.Name = "lblDoiTac";
            this.lblDoiTac.Size = new System.Drawing.Size(48, 15);
            this.lblDoiTac.TabIndex = 5;
            this.lblDoiTac.Text = "Ð?i tác:";
            // 
            // btnXemBaoCao
            // 
            this.btnXemBaoCao.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemBaoCao.Location = new System.Drawing.Point(806, 18);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.Size = new System.Drawing.Size(147, 34);
            this.btnXemBaoCao.TabIndex = 4;
            this.btnXemBaoCao.Text = "Xem Báo Cáo";
            this.btnXemBaoCao.UseVisualStyleBackColor = true;
            this.btnXemBaoCao.Click += new System.EventHandler(this.BtnXemBaoCao_Click);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(286, 24);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(131, 23);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ð?n ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(74, 24);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(131, 23);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "T? ngày:";
            // 
            // gridBaoCao
            // 
            this.gridBaoCao.AllowUserToAddRows = false;
            this.gridBaoCao.AllowUserToDeleteRows = false;
            this.gridBaoCao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBaoCao.Location = new System.Drawing.Point(0, 110);
            this.gridBaoCao.Name = "gridBaoCao";
            this.gridBaoCao.ReadOnly = true;
            this.gridBaoCao.RowTemplate.Height = 25;
            this.gridBaoCao.Size = new System.Drawing.Size(1134, 529);
            this.gridBaoCao.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staTongTien});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1134, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // staTongTien
            // 
            this.staTongTien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.staTongTien.Name = "staTongTien";
            this.staTongTien.Size = new System.Drawing.Size(70, 17);
            this.staTongTien.Text = "T?ng ti?n: 0";
            // 
            // FormBaoCaoKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 661);
            this.Controls.Add(this.gridBaoCao);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormBaoCaoKho";
            this.Text = "Báo Cáo Nh?p - Xu?t Kho";
            this.Load += new System.EventHandler(this.FormBaoCaoKho_Load);
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
        private System.Windows.Forms.Button btnXemBaoCao;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridBaoCao;
        private System.Windows.Forms.ComboBox cboDoiTac;
        private System.Windows.Forms.Label lblDoiTac;
        private System.Windows.Forms.CheckBox chkXemChiTiet;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel staTongTien;
    }
}
