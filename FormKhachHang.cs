using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Services;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormKhachHang : Form
    {
        private readonly KhachHangService _service;
        private FormState _currentState;

        // Define States
        public enum FormState
        {
            View,
            Adding,
            Editing
        }

        public FormKhachHang()
        {
            InitializeComponent();
            _service = new KhachHangService();

            // Bind Events
            this.Load += FormKhachHang_Load;
            this.dgvKhachHang.SelectionChanged += DgvKhachHang_SelectionChanged;

            this.btnThem.Click += BtnThem_Click;
            this.btnSua.Click += BtnSua_Click; // New Edit Button
            this.btnLuu.Click += BtnLuu_Click;
            this.btnXoa.Click += BtnXoa_Click;
            this.btnHuy.Click += BtnHuy_Click;
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
            SetState(FormState.View); // Default to View Mode
        }

        private void LoadData()
        {
            try
            {
                dgvKhachHang.DataSource = _service.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- STATE MANAGEMENT ---

        private void SetState(FormState state)
        {
            _currentState = state;

            switch (state)
            {
                case FormState.View:
                    // Inputs ReadOnly
                    SetInputsReadOnly(true);

                    // Button Logic: Can Add, Edit/Delete (if row selected), but NOT Save/Cancel
                    btnThem.Enabled = true;
                    btnSua.Enabled = (dgvKhachHang.CurrentRow != null);
                    btnXoa.Enabled = (dgvKhachHang.CurrentRow != null);
                    btnLuu.Enabled = false;
                    btnHuy.Enabled = false;

                    dgvKhachHang.Enabled = true;
                    if (dgvKhachHang.CurrentRow != null)
                        PopulateInputs(dgvKhachHang.CurrentRow);
                    else
                        ClearInputs();
                    break;

                case FormState.Adding:
                    // Inputs Writeable (All)
                    ClearInputs();
                    SetInputsReadOnly(false);
                    txtMaKH.Enabled = true; // Code unlocked for new

                    // Button Logic: Can Only Save/Cancel
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = true;
                    btnHuy.Enabled = true;

                    dgvKhachHang.Enabled = false; // Check requirement: usually disable grid to lock context
                    txtMaKH.Focus();
                    break;

                case FormState.Editing:
                    // Inputs Writeable (Except Code)
                    SetInputsReadOnly(false);
                    txtMaKH.Enabled = false; // Primary Key Locked

                    // Button Logic: Can Only Save/Cancel
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = true;
                    btnHuy.Enabled = true;

                    dgvKhachHang.Enabled = false; // Lock grid to prevent changing selection while editing
                    txtTenKH.Focus();
                    break;
            }
        }

        private void SetInputsReadOnly(bool isReadOnly)
        {
            // MaterialTextBox might not support ReadOnly property directly if not exposed, 
            // checking MaterialTextBox.cs -> it has ReadOnly property.
            txtMaKH.ReadOnly = isReadOnly;
            txtTenKH.ReadOnly = isReadOnly;
            txtDiaChi.ReadOnly = isReadOnly;
            txtSDT.ReadOnly = isReadOnly;
            txtEmail.ReadOnly = isReadOnly;
            txtGhiChu.ReadOnly = isReadOnly;

            // Also manage Enabled state to visualize "Disabled" look if ReadOnly looks like active
            // But requirement said "ReadOnly", let's trust the property first.
            // If ReadOnly doesn't grey out, we might want Enabled = !isReadOnly
            // User Complaint: "Accidental editing". ReadOnly is best? 
            // Let's use Enabled for strong visual indication if ReadOnly is ambiguous.
            // Actually, MaterialTextBox.ReadOnly usually just blocks input.
            // Let's use Enabled to be safe and consistent with previous "Grayed out" look.

            bool isEnabled = !isReadOnly;
            txtMaKH.Enabled = isEnabled;
            txtTenKH.Enabled = isEnabled;
            txtDiaChi.Enabled = isEnabled;
            txtSDT.Enabled = isEnabled;
            txtEmail.Enabled = isEnabled;
            txtGhiChu.Enabled = isEnabled;
        }

        private void ClearInputs()
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtGhiChu.Clear();
        }

        private void PopulateInputs(DataGridViewRow row)
        {
            txtMaKH.Texts = row.Cells["MAKH"].Value?.ToString();
            txtTenKH.Texts = row.Cells["TENKH"].Value?.ToString();
            txtDiaChi.Texts = row.Cells["DIACHI"].Value?.ToString();
            txtSDT.Texts = row.Cells["SDT"].Value?.ToString();
            txtEmail.Texts = row.Cells["EMAIL"].Value?.ToString();
            txtGhiChu.Texts = row.Cells["GHICHU"].Value?.ToString();
        }

        // --- EVENTS ---

        private void DgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            // Only react if we are in View Mode.
            // If Adding/Editing, the grid should be disabled, but if not, ignore changes.
            if (_currentState == FormState.View)
            {
                if (dgvKhachHang.CurrentRow != null)
                {
                    PopulateInputs(dgvKhachHang.CurrentRow);
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                }
                else
                {
                    ClearInputs();
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            SetState(FormState.Adding);
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SetState(FormState.Editing);
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtMaKH.Texts))
                {
                    MessageBox.Show("Vui lòng nhập Mã Khách Hàng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaKH.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTenKH.Texts))
                {
                    MessageBox.Show("Vui lòng nhập Tên Khách Hàng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenKH.Focus();
                    return;
                }

                if (_currentState == FormState.Adding)
                {
                    if (_service.CheckTonTai(txtMaKH.Texts))
                    {
                        MessageBox.Show("Mã Khách Hàng này đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _service.Insert(txtMaKH.Texts, txtTenKH.Texts, txtDiaChi.Texts, txtSDT.Texts, txtEmail.Texts, txtGhiChu.Texts);
                    MessageBox.Show("Thêm khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_currentState == FormState.Editing)
                {
                    _service.Update(txtMaKH.Texts, txtTenKH.Texts, txtDiaChi.Texts, txtSDT.Texts, txtEmail.Texts, txtGhiChu.Texts);
                    MessageBox.Show("Cập nhật khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadData(); // Reload grid
                SetState(FormState.View); // Return to View Mode
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            // Delete works in View Mode (based on selection)
            if (string.IsNullOrWhiteSpace(txtMaKH.Texts))
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _service.Delete(txtMaKH.Texts);
                    MessageBox.Show("Xóa khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    SetState(FormState.View); // Refresh state
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            SetState(FormState.View);
        }
    }
}