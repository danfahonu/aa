namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormDanhMucHangHoa
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
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlList = new System.Windows.Forms.Panel();
            this.dgvMerchandise = new DoAnLapTrinhQuanLy.Controls.ModernDataGrid();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnThem = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.txtSearch = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.pnlActionButtons = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHuy = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.btnLuu = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.btnXoa = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.btnSua = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.btnBrowseImage = new DoAnLapTrinhQuanLy.Controls.ModernButton();
            this.picProductImage = new System.Windows.Forms.PictureBox();
            this.lblImg = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.txtSellingPrice = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCostPrice = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUnit = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMerchandiseName = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMerchandiseCode = new DoAnLapTrinhQuanLy.Controls.MaterialTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.mainLayout.SuspendLayout();
            this.pnlList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMerchandise)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlActionButtons.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).BeginInit();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 2;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.mainLayout.Controls.Add(this.pnlList, 0, 0);
            this.mainLayout.Controls.Add(this.pnlInput, 1, 0);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 1;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Size = new System.Drawing.Size(1200, 700);
            this.mainLayout.TabIndex = 0;
            // 
            // pnlList
            // 
            this.pnlList.Controls.Add(this.dgvMerchandise);
            this.pnlList.Controls.Add(this.pnlFilter);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList.Location = new System.Drawing.Point(0, 0);
            this.pnlList.Margin = new System.Windows.Forms.Padding(0);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(780, 700);
            this.pnlList.TabIndex = 0;
            // 
            // dgvMerchandise
            // 
            this.dgvMerchandise.BackgroundColor = System.Drawing.Color.White;
            this.dgvMerchandise.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvMerchandise.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMerchandise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMerchandise.Location = new System.Drawing.Point(0, 60);
            this.dgvMerchandise.Name = "dgvMerchandise";
            this.dgvMerchandise.RowHeadersWidth = 51;
            this.dgvMerchandise.RowTemplate.Height = 24;
            this.dgvMerchandise.Size = new System.Drawing.Size(780, 640);
            this.dgvMerchandise.TabIndex = 1;
            this.dgvMerchandise.SelectionChanged += new System.EventHandler(this.DgvMerchandise_SelectionChanged);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.btnThem);
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.lblSearch);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(10);
            this.pnlFilter.Size = new System.Drawing.Size(780, 60);
            this.pnlFilter.TabIndex = 0;
            // 
            // btnThem
            // 
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(650, 15);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(120, 35);
            this.btnThem.TabIndex = 2;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.BtnThem_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.SystemColors.Window;
            this.txtSearch.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtSearch.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtSearch.BorderRadius = 0;
            this.txtSearch.BorderSize = 2;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSearch.Location = new System.Drawing.Point(90, 15);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Multiline = false;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtSearch.PasswordChar = false;
            this.txtSearch.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtSearch.PlaceholderText = "Mã hàng / Tên hàng...";
            this.txtSearch.Size = new System.Drawing.Size(350, 35);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Texts = "";
            this.txtSearch.UnderlinedStyle = false;
            this.txtSearch._TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSearch.Location = new System.Drawing.Point(10, 22);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(86, 23);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // pnlInput
            // 
            this.pnlInput.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlInput.Controls.Add(this.pnlActionButtons);
            this.pnlInput.Controls.Add(this.btnBrowseImage);
            this.pnlInput.Controls.Add(this.picProductImage);
            this.pnlInput.Controls.Add(this.lblImg);
            this.pnlInput.Controls.Add(this.chkActive);
            this.pnlInput.Controls.Add(this.txtSellingPrice);
            this.pnlInput.Controls.Add(this.label6);
            this.pnlInput.Controls.Add(this.txtCostPrice);
            this.pnlInput.Controls.Add(this.label5);
            this.pnlInput.Controls.Add(this.txtUnit);
            this.pnlInput.Controls.Add(this.label4);
            this.pnlInput.Controls.Add(this.cboCategory);
            this.pnlInput.Controls.Add(this.label3);
            this.pnlInput.Controls.Add(this.txtMerchandiseName);
            this.pnlInput.Controls.Add(this.label2);
            this.pnlInput.Controls.Add(this.txtMerchandiseCode);
            this.pnlInput.Controls.Add(this.label1);
            this.pnlInput.Controls.Add(this.lblHeader);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(783, 3);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Padding = new System.Windows.Forms.Padding(10);
            this.pnlInput.Size = new System.Drawing.Size(414, 694);
            this.pnlInput.TabIndex = 1;
            // 
            // pnlActionButtons
            // 
            this.pnlActionButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlActionButtons.Controls.Add(this.flowLayoutPanel1);
            this.pnlActionButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActionButtons.Location = new System.Drawing.Point(10, 634);
            this.pnlActionButtons.Name = "pnlActionButtons";
            this.pnlActionButtons.Size = new System.Drawing.Size(394, 50);
            this.pnlActionButtons.TabIndex = 17;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnHuy);
            this.flowLayoutPanel1.Controls.Add(this.btnLuu);
            this.flowLayoutPanel1.Controls.Add(this.btnXoa);
            this.flowLayoutPanel1.Controls.Add(this.btnSua);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(394, 50);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(321, 8);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(70, 35);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(245, 8);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(70, 35);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.BtnLuu_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(169, 8);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(70, 35);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.BtnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(93, 8);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(70, 35);
            this.btnSua.TabIndex = 3;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.BtnSua_Click);
            // 
            // btnBrowseImage
            // 
            this.btnBrowseImage.Location = new System.Drawing.Point(140, 580);
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Size = new System.Drawing.Size(100, 30);
            this.btnBrowseImage.TabIndex = 16;
            this.btnBrowseImage.Text = "Chọn hình...";
            this.btnBrowseImage.UseVisualStyleBackColor = true;
            this.btnBrowseImage.Click += new System.EventHandler(this.BtnBrowseImage_Click);
            // 
            // picProductImage
            // 
            this.picProductImage.BackColor = System.Drawing.Color.Black;
            this.picProductImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picProductImage.Location = new System.Drawing.Point(5, 490);
            this.picProductImage.Name = "picProductImage";
            this.picProductImage.Size = new System.Drawing.Size(120, 120);
            this.picProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProductImage.TabIndex = 15;
            this.picProductImage.TabStop = false;
            // 
            // lblImg
            // 
            this.lblImg.AutoSize = true;
            this.lblImg.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblImg.Location = new System.Drawing.Point(5, 465);
            this.lblImg.Name = "lblImg";
            this.lblImg.Size = new System.Drawing.Size(96, 23);
            this.lblImg.TabIndex = 14;
            this.lblImg.Text = "HÌNH ẢNH";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(5, 430);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(129, 20);
            this.chkActive.TabIndex = 13;
            this.chkActive.Text = "Đang kinh doanh";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSellingPrice.BackColor = System.Drawing.SystemColors.Window;
            this.txtSellingPrice.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtSellingPrice.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtSellingPrice.BorderRadius = 0;
            this.txtSellingPrice.BorderSize = 2;
            this.txtSellingPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSellingPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSellingPrice.Location = new System.Drawing.Point(5, 380);
            this.txtSellingPrice.Margin = new System.Windows.Forms.Padding(4);
            this.txtSellingPrice.Multiline = false;
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtSellingPrice.PasswordChar = false;
            this.txtSellingPrice.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtSellingPrice.PlaceholderText = "";
            this.txtSellingPrice.Size = new System.Drawing.Size(394, 35);
            this.txtSellingPrice.TabIndex = 12;
            this.txtSellingPrice.Texts = "0";
            this.txtSellingPrice.UnderlinedStyle = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 355);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Giá bán";
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCostPrice.BackColor = System.Drawing.SystemColors.Window;
            this.txtCostPrice.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtCostPrice.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtCostPrice.BorderRadius = 0;
            this.txtCostPrice.BorderSize = 2;
            this.txtCostPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCostPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCostPrice.Location = new System.Drawing.Point(5, 315);
            this.txtCostPrice.Margin = new System.Windows.Forms.Padding(4);
            this.txtCostPrice.Multiline = false;
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtCostPrice.PasswordChar = false;
            this.txtCostPrice.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtCostPrice.PlaceholderText = "";
            this.txtCostPrice.ReadOnly = true;
            this.txtCostPrice.Size = new System.Drawing.Size(394, 35);
            this.txtCostPrice.TabIndex = 10;
            this.txtCostPrice.Texts = "0";
            this.txtCostPrice.UnderlinedStyle = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Giá vốn";
            // 
            // txtUnit
            // 
            this.txtUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUnit.BackColor = System.Drawing.SystemColors.Window;
            this.txtUnit.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtUnit.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtUnit.BorderRadius = 0;
            this.txtUnit.BorderSize = 2;
            this.txtUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtUnit.Location = new System.Drawing.Point(5, 250);
            this.txtUnit.Margin = new System.Windows.Forms.Padding(4);
            this.txtUnit.Multiline = false;
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtUnit.PasswordChar = false;
            this.txtUnit.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtUnit.PlaceholderText = "";
            this.txtUnit.Size = new System.Drawing.Size(394, 35);
            this.txtUnit.TabIndex = 8;
            this.txtUnit.Texts = "";
            this.txtUnit.UnderlinedStyle = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Đơn vị tính";
            // 
            // cboCategory
            // 
            this.cboCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(5, 185);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(394, 24);
            this.cboCategory.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nhóm hàng (*)";
            // 
            // txtMerchandiseName
            // 
            this.txtMerchandiseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMerchandiseName.BackColor = System.Drawing.SystemColors.Window;
            this.txtMerchandiseName.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtMerchandiseName.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtMerchandiseName.BorderRadius = 0;
            this.txtMerchandiseName.BorderSize = 2;
            this.txtMerchandiseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMerchandiseName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtMerchandiseName.Location = new System.Drawing.Point(5, 120);
            this.txtMerchandiseName.Margin = new System.Windows.Forms.Padding(4);
            this.txtMerchandiseName.Multiline = false;
            this.txtMerchandiseName.Name = "txtMerchandiseName";
            this.txtMerchandiseName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtMerchandiseName.PasswordChar = false;
            this.txtMerchandiseName.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtMerchandiseName.PlaceholderText = "";
            this.txtMerchandiseName.Size = new System.Drawing.Size(394, 35);
            this.txtMerchandiseName.TabIndex = 4;
            this.txtMerchandiseName.Texts = "";
            this.txtMerchandiseName.UnderlinedStyle = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tên hàng (*)";
            // 
            // txtMerchandiseCode
            // 
            this.txtMerchandiseCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMerchandiseCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtMerchandiseCode.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtMerchandiseCode.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtMerchandiseCode.BorderRadius = 0;
            this.txtMerchandiseCode.BorderSize = 2;
            this.txtMerchandiseCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMerchandiseCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtMerchandiseCode.Location = new System.Drawing.Point(5, 55);
            this.txtMerchandiseCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtMerchandiseCode.Multiline = false;
            this.txtMerchandiseCode.Name = "txtMerchandiseCode";
            this.txtMerchandiseCode.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtMerchandiseCode.PasswordChar = false;
            this.txtMerchandiseCode.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtMerchandiseCode.PlaceholderText = "";
            this.txtMerchandiseCode.Size = new System.Drawing.Size(394, 35);
            this.txtMerchandiseCode.TabIndex = 2;
            this.txtMerchandiseCode.Texts = "";
            this.txtMerchandiseCode.UnderlinedStyle = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã hàng (*)";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblHeader.Location = new System.Drawing.Point(5, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(201, 28);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "CHI TIẾT HÀNG HÓA";
            // 
            // FormDanhMucHangHoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.mainLayout);
            this.Name = "FormDanhMucHangHoa";
            this.Text = "DANH MỤC HÀNG HÓA";
            this.Load += new System.EventHandler(this.FormDanhMucHangHoa_Load);
            this.mainLayout.ResumeLayout(false);
            this.pnlList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMerchandise)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            this.pnlActionButtons.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Panel pnlList;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.Panel pnlFilter;
        private DoAnLapTrinhQuanLy.Controls.ModernDataGrid dgvMerchandise;
        private System.Windows.Forms.Label lblSearch;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtSearch;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnThem;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label1;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtMerchandiseCode;
        private System.Windows.Forms.Label label2;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtMerchandiseName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label label4;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtUnit;
        private System.Windows.Forms.Label label5;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtCostPrice;
        private System.Windows.Forms.Label label6;
        private DoAnLapTrinhQuanLy.Controls.MaterialTextBox txtSellingPrice;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label lblImg;
        private System.Windows.Forms.PictureBox picProductImage;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnBrowseImage;
        private System.Windows.Forms.Panel pnlActionButtons;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnHuy;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnLuu;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnXoa;
        private DoAnLapTrinhQuanLy.Controls.ModernButton btnSua;
    }
}