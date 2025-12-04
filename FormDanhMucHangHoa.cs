using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;
using DoAnLapTrinhQuanLy.Controls;
using ThemeManager = global::DoAnLapTrinhQuanLy.Core.ThemeManager;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormDanhMucHangHoa : BaseForm
    {
        private bool isAdding = false;
        private string currentImagePath = null;

        // UI Controls
        private Panel pnlFilter; // Top
        private ModernDataGrid dgvMerchandise; // Center
        private Panel pnlInput; // Right
        private Panel pnlActionButtons; // Bottom of Input

        // Filter Controls
        private MaterialTextBox txtSearch;
        private ModernButton btnAddNew;

        // Input Controls
        private MaterialTextBox txtMerchandiseCode;
        private MaterialTextBox txtMerchandiseName;
        private ComboBox cboCategory;
        private MaterialTextBox txtUnit;
        private MaterialTextBox txtCostPrice;
        private MaterialTextBox txtSellingPrice;
        private CheckBox chkIsActive;
        private PictureBox picProductImage;
        private ModernButton btnBrowseImage;

        // Action Buttons
        private ModernButton btnSave;
        private ModernButton btnCancel;
        private ModernButton btnEdit;
        private ModernButton btnDelete;

        public FormDanhMucHangHoa()
        {
            InitializeComponent();
            InitializeCustomUI();
        }

        private void InitializeCustomUI()
        {
            this.Size = new Size(1200, 700);
            this.Text = "DANH MỤC HÀNG HÓA";

            // 1. Filter Panel (Top)
            pnlFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = ThemeManager.SecondaryColor,
                Padding = new Padding(10)
            };
            InitializeFilterPanel();
            this.Controls.Add(pnlFilter);

            // 2. Input Panel (Right)
            pnlInput = new Panel
            {
                Dock = DockStyle.Right,
                Width = 350,
                BackColor = ThemeManager.SecondaryColor,
                Padding = new Padding(10)
            };
            InitializeInputPanel();
            this.Controls.Add(pnlInput);

            // 3. Grid (Fill)
            dgvMerchandise = new ModernDataGrid();
            dgvMerchandise.Dock = DockStyle.Fill;
            dgvMerchandise.SelectionChanged += DgvMerchandise_SelectionChanged;
            this.Controls.Add(dgvMerchandise);

            // Bring Filter to front to ensure it stays on top if needed, though Dock order handles it
            pnlFilter.BringToFront();
        }

        private void InitializeFilterPanel()
        {
            txtSearch = new MaterialTextBox
            {
                PlaceholderText = "🔍 Tìm kiếm hàng hóa...",
                Size = new Size(300, 40),
                Location = new Point(10, 10)
            };
            txtSearch.TextChanged += (s, e) => { /* Implement Search Logic */ };

            btnAddNew = new ModernButton
            {
                Text = "+ Thêm Mới",
                Size = new Size(120, 40),
                Location = new Point(330, 10),
                BackColor = ThemeManager.SuccessColor
            };
            btnAddNew.Click += BtnThem_Click;

            pnlFilter.Controls.Add(btnAddNew);
            pnlFilter.Controls.Add(txtSearch);
        }

        private void InitializeInputPanel()
        {
            int y = 10;
            int labelW = 100;
            int inputW = 220;
            int gap = 50;

            // Helper
            void AddField(string label, Control input)
            {
                Label lbl = new Label { Text = label, Location = new Point(10, y + 8), AutoSize = true, ForeColor = ThemeManager.TextColor };
                input.Location = new Point(labelW, y);
                input.Width = inputW;
                input.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                pnlInput.Controls.Add(lbl);
                pnlInput.Controls.Add(input);
                y += gap;
            }

            Label lblTitle = new Label { Text = "THÔNG TIN CHI TIẾT", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = ThemeManager.AccentColor, Location = new Point(10, y), AutoSize = true };
            pnlInput.Controls.Add(lblTitle);
            y += 40;

            txtMerchandiseCode = new MaterialTextBox { PlaceholderText = "Mã hàng" };
            AddField("Mã hàng:", txtMerchandiseCode);

            txtMerchandiseName = new MaterialTextBox { PlaceholderText = "Tên hàng hóa" };
            AddField("Tên hàng:", txtMerchandiseName);

            cboCategory = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, BackColor = ThemeManager.TextBoxBackground, ForeColor = ThemeManager.TextColor, FlatStyle = FlatStyle.Flat };
            AddField("Nhóm hàng:", cboCategory);

            txtUnit = new MaterialTextBox { PlaceholderText = "Đơn vị tính" };
            AddField("ĐVT:", txtUnit);

            txtCostPrice = new MaterialTextBox { PlaceholderText = "0", ReadOnly = true };
            AddField("Giá vốn:", txtCostPrice);

            txtSellingPrice = new MaterialTextBox { PlaceholderText = "0" };
            AddField("Giá bán:", txtSellingPrice);

            chkIsActive = new CheckBox { Text = "Đang kinh doanh", Checked = true, AutoSize = true, ForeColor = ThemeManager.TextColor };
            chkIsActive.Location = new Point(labelW, y);
            pnlInput.Controls.Add(chkIsActive);
            y += 40;

            // Image
            Label lblImg = new Label { Text = "Hình ảnh:", Location = new Point(10, y), AutoSize = true, ForeColor = ThemeManager.TextColor };
            pnlInput.Controls.Add(lblImg);

            picProductImage = new PictureBox { Location = new Point(labelW, y), Size = new Size(120, 120), BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.Black };
            pnlInput.Controls.Add(picProductImage);

            btnBrowseImage = new ModernButton { Text = "...", Location = new Point(labelW + 130, y + 85), Size = new Size(40, 35) };
            btnBrowseImage.Click += BtnBrowse_Click;
            pnlInput.Controls.Add(btnBrowseImage);
            y += 130;

            // Action Buttons Panel
            pnlActionButtons = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.Transparent };

            btnSave = new ModernButton { Text = "Lưu", Size = new Size(80, 40), BackColor = ThemeManager.AccentColor, Enabled = false };
            btnSave.Click += BtnLuu_Click;

            btnCancel = new ModernButton { Text = "Hủy", Size = new Size(80, 40), BackColor = Color.Gray, Enabled = false };
            btnCancel.Click += BtnHuy_Click;

            btnEdit = new ModernButton { Text = "Sửa", Size = new Size(80, 40), BackColor = ThemeManager.WarningColor };
            btnEdit.Click += BtnSua_Click;

            btnDelete = new ModernButton { Text = "Xóa", Size = new Size(80, 40), BackColor = ThemeManager.DangerColor };
            btnDelete.Click += BtnXoa_Click;

            // Layout buttons using FlowLayout
            FlowLayoutPanel flow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(5) };
            flow.Controls.Add(btnCancel);
            flow.Controls.Add(btnSave);
            flow.Controls.Add(btnDelete);
            flow.Controls.Add(btnEdit);

            pnlActionButtons.Controls.Add(flow);
            pnlInput.Controls.Add(pnlActionButtons);
        }

        private async void FormDanhMucHangHoa_Load(object sender, EventArgs e)
        {
            try
            {
                ThemeManager.Apply(this);
                this.Cursor = Cursors.WaitCursor;

                var dataTask = Task.Run(() => GetData());
                var nhomHangTask = Task.Run(() => GetNhomHangData());

                await Task.WhenAll(dataTask, nhomHangTask);

                dgvMerchandise.DataSource = dataTask.Result;

                cboCategory.DataSource = nhomHangTask.Result;
                cboCategory.DisplayMember = "TENNHOM";
                cboCategory.ValueMember = "MANHOM";

                SetInputMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private DataTable GetData()
        {
            string query = "SELECT MAHH, TENHH, MANHOM, DVT, GIAVON, GIABAN, TONKHO, ANH, ACTIVE FROM DM_HANGHOA";
            return DbHelper.Query(query);
        }

        private DataTable GetNhomHangData()
        {
            string query = "SELECT MANHOM, TENNHOM FROM DM_NHOMHANG";
            return DbHelper.Query(query);
        }

        private async void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dt = await Task.Run(() => GetData());
                dgvMerchandise.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SetInputMode(bool enable)
        {
            txtMerchandiseCode.ReadOnly = !isAdding;
            txtMerchandiseName.ReadOnly = !enable;
            txtUnit.ReadOnly = !enable;
            txtSellingPrice.ReadOnly = !enable;
            cboCategory.Enabled = enable;
            chkIsActive.Enabled = enable;
            btnBrowseImage.Enabled = enable;

            txtCostPrice.ReadOnly = true;
            txtCostPrice.BackColor = enable ? ThemeManager.TextBoxBackground : Color.FromArgb(50, 50, 50);

            btnSave.Enabled = enable;
            btnCancel.Enabled = enable;

            // Logic: Can only Edit/Delete when NOT adding/editing
            btnEdit.Enabled = !enable && dgvMerchandise.SelectedRows.Count > 0;
            btnDelete.Enabled = !enable && dgvMerchandise.SelectedRows.Count > 0;
            btnAddNew.Enabled = !enable;
        }

        private void ClearInputs()
        {
            txtMerchandiseCode.Texts = "";
            txtMerchandiseName.Texts = "";
            txtUnit.Texts = "";
            txtSellingPrice.Texts = "0";
            txtCostPrice.Texts = "0";
            cboCategory.SelectedIndex = -1;
            chkIsActive.Checked = true;
            picProductImage.Image = null;
            currentImagePath = null;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearInputs();
            SetInputMode(true);
            txtMerchandiseCode.Focus();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvMerchandise.SelectedRows.Count == 0) return;
            isAdding = false;
            SetInputMode(true);
            txtMerchandiseName.Focus();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvMerchandise.SelectedRows.Count == 0) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mặt hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string maHH = dgvMerchandise.SelectedRows[0].Cells["MAHH"].Value.ToString();
                    string query = "DELETE FROM DM_HANGHOA WHERE MAHH = @MaHH";
                    DbHelper.Execute(query, DbHelper.Param("@MaHH", maHH));

                    LoadData();
                    MessageBox.Show("Xóa mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                    {
                        MessageBox.Show("Dữ liệu này đang được sử dụng, không thể xóa!", "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi SQL: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMerchandiseCode.Texts) || string.IsNullOrWhiteSpace(txtMerchandiseName.Texts))
                {
                    MessageBox.Show("Mã và Tên hàng hóa không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isAdding)
                {
                    object count = DbHelper.Scalar("SELECT COUNT(*) FROM DM_HANGHOA WHERE MAHH = @MaHH", DbHelper.Param("@MaHH", txtMerchandiseCode.Texts));
                    if (Convert.ToInt32(count) > 0)
                    {
                        MessageBox.Show("Mã hàng hóa này đã tồn tại!", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMerchandiseCode.Focus();
                        return;
                    }

                    string query = @"
                        INSERT INTO DM_HANGHOA (MAHH, TENHH, MANHOM, DVT, GIABAN, ANH, ACTIVE)
                        VALUES (@MaHH, @TenHH, @MaNhom, @DVT, @GiaBan, @Anh, @Active)";
                    DbHelper.Execute(query,
                        DbHelper.Param("@MaHH", txtMerchandiseCode.Texts),
                        DbHelper.Param("@TenHH", txtMerchandiseName.Texts),
                        DbHelper.Param("@MaNhom", cboCategory.SelectedValue),
                        DbHelper.Param("@DVT", txtUnit.Texts),
                        DbHelper.Param("@GiaBan", decimal.Parse(txtSellingPrice.Texts)),
                        DbHelper.Param("@Anh", currentImagePath),
                        DbHelper.Param("@Active", chkIsActive.Checked)
                    );
                }
                else
                {
                    string query = @"
                        UPDATE DM_HANGHOA SET
                            TENHH = @TenHH, MANHOM = @MaNhom, DVT = @DVT, GIABAN = @GiaBan, 
                            ANH = @Anh, ACTIVE = @Active
                        WHERE MAHH = @MaHH";
                    DbHelper.Execute(query,
                        DbHelper.Param("@TenHH", txtMerchandiseName.Texts),
                        DbHelper.Param("@MaNhom", cboCategory.SelectedValue),
                        DbHelper.Param("@DVT", txtUnit.Texts),
                        DbHelper.Param("@GiaBan", decimal.Parse(txtSellingPrice.Texts)),
                        DbHelper.Param("@Anh", currentImagePath),
                        DbHelper.Param("@Active", chkIsActive.Checked),
                        DbHelper.Param("@MaHH", txtMerchandiseCode.Texts)
                    );
                }
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                SetInputMode(false);
                isAdding = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            if (isAdding)
            {
                ClearInputs();
            }
            else
            {
                DgvMerchandise_SelectionChanged(null, null);
            }
            SetInputMode(false);
            isAdding = false;
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                currentImagePath = openFile.FileName;
                picProductImage.Image = new Bitmap(currentImagePath);
            }
        }

        private void DgvMerchandise_SelectionChanged(object sender, EventArgs e)
        {
            if (!isAdding && dgvMerchandise.SelectedRows.Count > 0)
            {
                var row = dgvMerchandise.SelectedRows[0];
                txtMerchandiseCode.Texts = row.Cells["MAHH"].Value?.ToString();
                txtMerchandiseName.Texts = row.Cells["TENHH"].Value?.ToString();
                txtUnit.Texts = row.Cells["DVT"].Value?.ToString();
                txtCostPrice.Texts = row.Cells["GIAVON"].Value?.ToString();
                txtSellingPrice.Texts = row.Cells["GIABAN"].Value?.ToString();
                cboCategory.SelectedValue = row.Cells["MANHOM"].Value;

                chkIsActive.Checked = row.Cells["ACTIVE"].Value != null && (bool)row.Cells["ACTIVE"].Value;

                currentImagePath = row.Cells["ANH"].Value?.ToString();
                if (!string.IsNullOrEmpty(currentImagePath) && File.Exists(currentImagePath))
                {
                    picProductImage.Image = Image.FromFile(currentImagePath);
                }
                else
                {
                    picProductImage.Image = null;
                }

                // Enable Edit/Delete when row selected
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
    }
}