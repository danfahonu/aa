using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;
using DoAnLapTrinhQuanLy.Controls;
using ThemeManager = DoAnLapTrinhQuanLy.Core.ThemeManager;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormDanhMucHangHoa : BaseForm
    {
        // 1. Fields & Constants
        private bool _isAdding;
        private string _currentImagePath;
        private DataTable _merchandiseTable;

        public FormDanhMucHangHoa()
        {
            InitializeComponent();
            UseCustomTitleBar = false; // Disable inner title bar
        }

        // 3. Data Loading & State
        private async void FormDanhMucHangHoa_Load(object sender, EventArgs e)
        {
            ThemeManager.Apply(this);
            await LoadInitialDataAsync();
        }

        private async Task LoadInitialDataAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Load Categories
                var dtNhom = await Task.Run(() => GetNhomHangData());
                cboCategory.DataSource = dtNhom;
                cboCategory.DisplayMember = "TENNHOM";
                cboCategory.ValueMember = "MANHOM";
                cboCategory.SelectedIndex = -1;

                // Load Merchandise
                _merchandiseTable = await Task.Run(() => GetData());
                dgvMerchandise.DataSource = _merchandiseTable;

                // Format Grid
                if (dgvMerchandise.Columns["MAHH"] != null) dgvMerchandise.Columns["MAHH"].HeaderText = "Mã hàng";
                if (dgvMerchandise.Columns["TENHH"] != null) dgvMerchandise.Columns["TENHH"].HeaderText = "Tên hàng";
                if (dgvMerchandise.Columns["MANHOM"] != null) dgvMerchandise.Columns["MANHOM"].HeaderText = "Nhóm";
                if (dgvMerchandise.Columns["DVT"] != null) dgvMerchandise.Columns["DVT"].HeaderText = "ĐVT";
                if (dgvMerchandise.Columns["GIAVON"] != null)
                {
                    dgvMerchandise.Columns["GIAVON"].HeaderText = "Giá vốn";
                    dgvMerchandise.Columns["GIAVON"].DefaultCellStyle.Format = "N0";
                }
                if (dgvMerchandise.Columns["GIABAN"] != null)
                {
                    dgvMerchandise.Columns["GIABAN"].HeaderText = "Giá bán";
                    dgvMerchandise.Columns["GIABAN"].DefaultCellStyle.Format = "N0";
                }
                if (dgvMerchandise.Columns["TONKHO"] != null) dgvMerchandise.Columns["TONKHO"].HeaderText = "Tồn kho";
                if (dgvMerchandise.Columns["ACTIVE"] != null) dgvMerchandise.Columns["ACTIVE"].HeaderText = "Đang kinh doanh";
                if (dgvMerchandise.Columns["ANH"] != null) dgvMerchandise.Columns["ANH"].Visible = false;

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
            return DbHelper.Query("SELECT MAHH, TENHH, MANHOM, DVT, GIAVON, GIABAN, TONKHO, ANH, ACTIVE FROM DM_HANGHOA");
        }

        private DataTable GetNhomHangData()
        {
            return DbHelper.Query("SELECT MANHOM, TENNHOM FROM DM_NHOMHANG");
        }

        private void SetInputMode(bool enable)
        {
            // Inputs
            txtMerchandiseCode.ReadOnly = !(_isAdding && enable); // Only editable when adding
            txtMerchandiseName.ReadOnly = !enable;
            cboCategory.Enabled = enable;
            txtUnit.ReadOnly = !enable;
            txtSellingPrice.ReadOnly = !enable;
            chkActive.Enabled = enable;
            btnBrowseImage.Enabled = enable;

            // Buttons
            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;

            btnThem.Enabled = !enable;
            bool hasSelection = dgvMerchandise.CurrentRow != null;
            btnSua.Enabled = !enable && hasSelection;
            btnXoa.Enabled = !enable && hasSelection;
        }

        private void ClearInputs()
        {
            txtMerchandiseCode.Texts = "";
            txtMerchandiseName.Texts = "";
            cboCategory.SelectedIndex = -1;
            txtUnit.Texts = "";
            txtCostPrice.Texts = "0";
            txtSellingPrice.Texts = "0";
            chkActive.Checked = true;

            if (picProductImage.Image != null) picProductImage.Image.Dispose();
            picProductImage.Image = null;
            _currentImagePath = null;
        }

        // 4. Search & Filter
        private void ApplyFilter(string keyword)
        {
            if (_merchandiseTable == null) return;
            keyword = keyword?.Trim().Replace("'", "''") ?? "";

            if (string.IsNullOrEmpty(keyword))
            {
                _merchandiseTable.DefaultView.RowFilter = "";
            }
            else
            {
                _merchandiseTable.DefaultView.RowFilter = $"MAHH LIKE '%{keyword}%' OR TENHH LIKE '%{keyword}%'";
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter(txtSearch.Texts);
        }

        // 5. Validation
        private bool ValidateInputs(out decimal giaBan)
        {
            giaBan = 0;
            if (string.IsNullOrWhiteSpace(txtMerchandiseCode.Texts))
            {
                MessageBox.Show("Mã hàng không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMerchandiseName.Texts))
            {
                MessageBox.Show("Tên hàng không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtSellingPrice.Texts, out giaBan))
            {
                giaBan = 0; // Accept 0 or invalid as 0
            }

            return true;
        }

        // 6. Event Handlers
        private void BtnThem_Click(object sender, EventArgs e)
        {
            _isAdding = true;
            ClearInputs();
            chkActive.Checked = true;
            SetInputMode(true);
            txtMerchandiseCode.Focus();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvMerchandise.CurrentRow == null) return;
            _isAdding = false;
            SetInputMode(true);
            txtMerchandiseName.Focus();
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            // Revert
            if (dgvMerchandise.CurrentRow != null)
            {
                DgvMerchandise_SelectionChanged(null, null);
            }
            else
            {
                ClearInputs();
            }
            SetInputMode(false);
            _isAdding = false;
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out decimal giaBan)) return;

            try
            {
                if (_isAdding)
                {
                    // Check duplicate
                    object count = DbHelper.Scalar("SELECT COUNT(*) FROM DM_HANGHOA WHERE MAHH = @MaHH", DbHelper.Param("@MaHH", txtMerchandiseCode.Texts));
                    if (Convert.ToInt32(count) > 0)
                    {
                        MessageBox.Show("Mã hàng đã tồn tại, vui lòng nhập mã khác.", "Trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string sql = @"INSERT INTO DM_HANGHOA (MAHH, TENHH, MANHOM, DVT, GIABAN, ANH, ACTIVE) 
                                   VALUES (@MaHH, @TenHH, @MaNhom, @DVT, @GiaBan, @Anh, @Active)";

                    DbHelper.Execute(sql,
                        DbHelper.Param("@MaHH", txtMerchandiseCode.Texts),
                        DbHelper.Param("@TenHH", txtMerchandiseName.Texts),
                        DbHelper.Param("@MaNhom", cboCategory.SelectedValue),
                        DbHelper.Param("@DVT", txtUnit.Texts),
                        DbHelper.Param("@GiaBan", giaBan),
                        DbHelper.Param("@Anh", _currentImagePath),
                        DbHelper.Param("@Active", chkActive.Checked)
                    );
                }
                else
                {
                    string sql = @"UPDATE DM_HANGHOA SET 
                                   TENHH = @TenHH, MANHOM = @MaNhom, DVT = @DVT, GIABAN = @GiaBan, 
                                   ANH = @Anh, ACTIVE = @Active 
                                   WHERE MAHH = @MaHH";

                    DbHelper.Execute(sql,
                        DbHelper.Param("@TenHH", txtMerchandiseName.Texts),
                        DbHelper.Param("@MaNhom", cboCategory.SelectedValue),
                        DbHelper.Param("@DVT", txtUnit.Texts),
                        DbHelper.Param("@GiaBan", giaBan),
                        DbHelper.Param("@Anh", _currentImagePath),
                        DbHelper.Param("@Active", chkActive.Checked),
                        DbHelper.Param("@MaHH", txtMerchandiseCode.Texts)
                    );
                }

                // Reload
                _merchandiseTable = GetData();
                dgvMerchandise.DataSource = _merchandiseTable;
                ApplyFilter(txtSearch.Texts); // Re-apply filter if any

                SetInputMode(false);
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvMerchandise.CurrentRow == null) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa hàng hóa này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string maHH = dgvMerchandise.CurrentRow.Cells["MAHH"].Value.ToString();
                    DbHelper.Execute("DELETE FROM DM_HANGHOA WHERE MAHH = @MaHH", DbHelper.Param("@MaHH", maHH));

                    _merchandiseTable = GetData();
                    dgvMerchandise.DataSource = _merchandiseTable;
                    ApplyFilter(txtSearch.Texts);

                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    if (ex.Number == 547)
                        MessageBox.Show("Hàng hóa đã phát sinh chứng từ, không thể xóa.", "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Lỗi khi xóa hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (picProductImage.Image != null) picProductImage.Image.Dispose();
                    _currentImagePath = dlg.FileName;
                    try
                    {
                        picProductImage.Image = Image.FromFile(_currentImagePath);
                    }
                    catch { /* Ignore invalid image */ }
                }
            }
        }

        private void DgvMerchandise_SelectionChanged(object sender, EventArgs e)
        {
            if (_isAdding) return;

            if (dgvMerchandise.CurrentRow != null)
            {
                var row = dgvMerchandise.CurrentRow;
                txtMerchandiseCode.Texts = row.Cells["MAHH"].Value?.ToString();
                txtMerchandiseName.Texts = row.Cells["TENHH"].Value?.ToString();
                cboCategory.SelectedValue = row.Cells["MANHOM"].Value;
                txtUnit.Texts = row.Cells["DVT"].Value?.ToString();
                txtCostPrice.Texts = row.Cells["GIAVON"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["GIAVON"].Value).ToString("N0") : "0";
                txtSellingPrice.Texts = row.Cells["GIABAN"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["GIABAN"].Value).ToString("N0") : "0";
                chkActive.Checked = row.Cells["ACTIVE"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["ACTIVE"].Value);

                // Image
                if (picProductImage.Image != null) picProductImage.Image.Dispose();
                _currentImagePath = row.Cells["ANH"].Value?.ToString();

                if (!string.IsNullOrEmpty(_currentImagePath) && File.Exists(_currentImagePath))
                {
                    try { picProductImage.Image = Image.FromFile(_currentImagePath); }
                    catch { picProductImage.Image = null; }
                }
                else
                {
                    picProductImage.Image = null;
                }

                SetInputMode(false); // Ensure buttons update state
            }
        }
    }
}