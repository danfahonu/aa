namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormPhieuChi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.infoPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblNgayLap = new System.Windows.Forms.Label();
            this.dtpNgayLap = new System.Windows.Forms.DateTimePicker();
            this.lblDoiTuong = new System.Windows.Forms.Label();
            this.cboDoiTuong = new System.Windows.Forms.ComboBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblSoTien = new System.Windows.Forms.Label();
            this.numSoTien = new System.Windows.Forms.NumericUpDown();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            this.lblSoTkNo = new System.Windows.Forms.Label();
            this.cboSoTkNo = new System.Windows.Forms.ComboBox();
            this.lblSoTkCo = new System.Windows.Forms.Label();
            this.cboSoTkCo = new System.Windows.Forms.ComboBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.infoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoTien)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grpThongTin, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelButtons, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(900, 400);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.infoPanel);
            this.grpThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpThongTin.Location = new System.Drawing.Point(13, 13);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(874, 314);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin Phiếu Chi";
            // 
            // infoPanel
            // 
            this.infoPanel.ColumnCount = 4;
            this.infoPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.infoPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.infoPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.infoPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.infoPanel.Controls.Add(this.lblNgayLap, 0, 0);
            this.infoPanel.Controls.Add(this.dtpNgayLap, 1, 0);
            this.infoPanel.Controls.Add(this.lblDoiTuong, 0, 1);
            this.infoPanel.Controls.Add(this.cboDoiTuong, 1, 1);
            this.infoPanel.Controls.Add(this.lblNhanVien, 2, 1);
            this.infoPanel.Controls.Add(this.cboNhanVien, 3, 1);
            this.infoPanel.Controls.Add(this.lblSoTien, 0, 2);
            this.infoPanel.Controls.Add(this.numSoTien, 1, 2);
            this.infoPanel.Controls.Add(this.lblLyDo, 0, 4);
            this.infoPanel.Controls.Add(this.txtLyDo, 1, 4);
            this.infoPanel.Controls.Add(this.lblSoTkNo, 0, 3);
            this.infoPanel.Controls.Add(this.cboSoTkNo, 1, 3);
            this.infoPanel.Controls.Add(this.lblSoTkCo, 2, 3);
            this.infoPanel.Controls.Add(this.cboSoTkCo, 3, 3);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(3, 19);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Padding = new System.Windows.Forms.Padding(5);
            this.infoPanel.RowCount = 5;
            this.infoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.infoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.infoPanel.Size = new System.Drawing.Size(868, 292);
            this.infoPanel.TabIndex = 0;
            // 
            // lblNgayLap
            // 
            this.lblNgayLap.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNgayLap.AutoSize = true;
            this.lblNgayLap.Location = new System.Drawing.Point(8, 15);
            this.lblNgayLap.Name = "lblNgayLap";
            this.lblNgayLap.Size = new System.Drawing.Size(56, 15);
            this.lblNgayLap.TabIndex = 0;
            this.lblNgayLap.Text = "Ngày lập";
            // 
            // dtpNgayLap
            // 
            this.dtpNgayLap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpNgayLap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayLap.Location = new System.Drawing.Point(108, 8);
            this.dtpNgayLap.Name = "dtpNgayLap";
            this.dtpNgayLap.Size = new System.Drawing.Size(325, 23);
            this.dtpNgayLap.TabIndex = 1;
            // 
            // lblDoiTuong
            // 
            this.lblDoiTuong.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDoiTuong.AutoSize = true;
            this.lblDoiTuong.Location = new System.Drawing.Point(8, 50);
            this.lblDoiTuong.Name = "lblDoiTuong";
            this.lblDoiTuong.Size = new System.Drawing.Size(62, 15);
            this.lblDoiTuong.TabIndex = 2;
            this.lblDoiTuong.Text = "Đối tượng";
            // 
            // cboDoiTuong
            // 
            this.cboDoiTuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboDoiTuong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoiTuong.FormattingEnabled = true;
            this.cboDoiTuong.Location = new System.Drawing.Point(108, 43);
            this.cboDoiTuong.Name = "cboDoiTuong";
            this.cboDoiTuong.Size = new System.Drawing.Size(325, 23);
            this.cboDoiTuong.TabIndex = 3;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Location = new System.Drawing.Point(446, 50);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(63, 15);
            this.lblNhanVien.TabIndex = 4;
            this.lblNhanVien.Text = "Nhân viên";
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new System.Drawing.Point(546, 43);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(314, 23);
            this.cboNhanVien.TabIndex = 5;
            // 
            // lblSoTien
            // 
            this.lblSoTien.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSoTien.AutoSize = true;
            this.lblSoTien.Location = new System.Drawing.Point(8, 85);
            this.lblSoTien.Name = "lblSoTien";
            this.lblSoTien.Size = new System.Drawing.Size(43, 15);
            this.lblSoTien.TabIndex = 6;
            this.lblSoTien.Text = "Số tiền";
            // 
            // numSoTien
            // 
            this.infoPanel.SetColumnSpan(this.numSoTien, 3);
            this.numSoTien.DecimalPlaces = 2;
            this.numSoTien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numSoTien.Location = new System.Drawing.Point(108, 78);
            this.numSoTien.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numSoTien.Name = "numSoTien";
            this.numSoTien.Size = new System.Drawing.Size(752, 23);
            this.numSoTien.TabIndex = 7;
            // 
            // lblLyDo
            // 
            this.lblLyDo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Location = new System.Drawing.Point(8, 203);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(36, 15);
            this.lblLyDo.TabIndex = 10;
            this.lblLyDo.Text = "Lý do";
            // 
            // txtLyDo
            // 
            this.infoPanel.SetColumnSpan(this.txtLyDo, 3);
            this.txtLyDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLyDo.Location = new System.Drawing.Point(108, 148);
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(752, 136);
            this.txtLyDo.TabIndex = 11;
            // 
            // lblSoTkNo
            // 
            this.lblSoTkNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSoTkNo.AutoSize = true;
            this.lblSoTkNo.Location = new System.Drawing.Point(8, 120);
            this.lblSoTkNo.Name = "lblSoTkNo";
            this.lblSoTkNo.Size = new System.Drawing.Size(60, 15);
            this.lblSoTkNo.TabIndex = 12;
            this.lblSoTkNo.Text = "TK Ghi Nợ";
            // 
            // cboSoTkNo
            // 
            this.cboSoTkNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSoTkNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSoTkNo.FormattingEnabled = true;
            this.cboSoTkNo.Location = new System.Drawing.Point(108, 113);
            this.cboSoTkNo.Name = "cboSoTkNo";
            this.cboSoTkNo.Size = new System.Drawing.Size(325, 23);
            this.cboSoTkNo.TabIndex = 13;
            // 
            // lblSoTkCo
            // 
            this.lblSoTkCo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSoTkCo.AutoSize = true;
            this.lblSoTkCo.Location = new System.Drawing.Point(446, 120);
            this.lblSoTkCo.Name = "lblSoTkCo";
            this.lblSoTkCo.Size = new System.Drawing.Size(61, 15);
            this.lblSoTkCo.TabIndex = 14;
            this.lblSoTkCo.Text = "TK Ghi Có";
            // 
            // cboSoTkCo
            // 
            this.cboSoTkCo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSoTkCo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSoTkCo.FormattingEnabled = true;
            this.cboSoTkCo.Location = new System.Drawing.Point(546, 113);
            this.cboSoTkCo.Name = "cboSoTkCo";
            this.cboSoTkCo.Size = new System.Drawing.Size(314, 23);
            this.cboSoTkCo.TabIndex = 15;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnThoat);
            this.panelButtons.Controls.Add(this.btnLuu);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(13, 343);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(874, 44);
            this.panelButtons.TabIndex = 1;
            // 
            // btnThoat
            // 
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.Location = new System.Drawing.Point(777, 6);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(94, 30);
            this.btnThoat.TabIndex = 1;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Location = new System.Drawing.Point(677, 6);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(94, 30);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // FormPhieuChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 400);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FormPhieuChi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo Phiếu Chi";
            this.Load += new System.EventHandler(this.FormPhieuChi_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpThongTin.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoTien)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.TableLayoutPanel infoPanel;
        private System.Windows.Forms.Label lblNgayLap;
        private System.Windows.Forms.DateTimePicker dtpNgayLap;
        private System.Windows.Forms.Label lblDoiTuong;
        private System.Windows.Forms.ComboBox cboDoiTuong;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblSoTien;
        private System.Windows.Forms.NumericUpDown numSoTien;
        private System.Windows.Forms.Label lblLyDo;
        private System.Windows.Forms.TextBox txtLyDo;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Label lblSoTkNo;
        private System.Windows.Forms.ComboBox cboSoTkNo;
        private System.Windows.Forms.Label lblSoTkCo;
        private System.Windows.Forms.ComboBox cboSoTkCo;
    }
}