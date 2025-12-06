using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Services;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormNhaCungCap : Form
    {
        private readonly NhaCungCapService _service;
        private FormState _currentState;

        // Form State Enum
        public enum FormState
        {
            View,
            Adding,
            Editing
        }

        public FormNhaCungCap()
        {
            InitializeComponent();
            _service = new NhaCungCapService();

            // Wire Events
            this.Load += FormNhaCungCap_Load;
            this.dgvNhaCungCap.SelectionChanged += DgvNhaCungCap_SelectionChanged;

            this.btnThem.Click += BtnThem_Click;
            this.btnSua.Click += BtnSua_Click;
            this.btnLuu.Click += BtnLuu_Click;
            this.btnXoa.Click += BtnXoa_Click;
            this.btnHuy.Click += BtnHuy_Click;
        }

        private void FormNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
            SetState(FormState.View); // Default to View Mode
        }

        private void LoadData()
        {
            try
            {
                dgvNhaCungCap.DataSource = _service.GetAll();
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
                    // ReadOnly
                    SetInputsReadOnly(true);

                    // Buttons: Can Add/Edit/Delete (if row selected)
                    btnThem.Enabled = true;
                    btnSua.Enabled = (dgvNhaCungCap.CurrentRow != null);
                    btnXoa.Enabled = (dgvNhaCungCap.CurrentRow != null);
                    btnLuu.Enabled = false;
                    btnHuy.Enabled = false;

                    dgvNhaCungCap.Enabled = true;
                    if (dgvNhaCungCap.CurrentRow != null)
                        PopulateInputs(dgvNhaCungCap.CurrentRow);
                    else
                        ClearInputs();
                    break;

                case FormState.Adding:
                    // Editable (All)
                    ClearInputs();
                    SetInputsReadOnly(false);
                    txtMaNCC.Enabled = true; // Code unlocked

                    // Buttons: Only Save/Cancel
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = true;
                    btnHuy.Enabled = true;

                    dgvNhaCungCap.Enabled = false;
                    txtMaNCC.Focus();
                    break;

                case FormState.Editing:
                    // Editable (Except Code)
                    SetInputsReadOnly(false);
                    txtMaNCC.Enabled = false; // Code locked

                    // Buttons: Only Save/Cancel
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = true;
                    btnHuy.Enabled = true;

                    dgvNhaCungCap.Enabled = false;
                    txtTenNCC.Focus();
                    break;
            }
        }

        private void SetInputsReadOnly(bool isReadOnly)
        {
            bool isEnabled = !isReadOnly;
            txtMaNCC.Enabled = isEnabled;
            txtTenNCC.Enabled = isEnabled;
            txtDiaChi.Enabled = isEnabled;
            txtSDT.Enabled = isEnabled;
            txtEmail.Enabled = isEnabled;
            txtMSThue.Enabled = isEnabled;
            txtGhiChu.Enabled = isEnabled;
        }

        private void ClearInputs()
        {
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtMSThue.Clear();
            txtGhiChu.Clear();
        }

        private void PopulateInputs(DataGridViewRow row)
        {
            txtMaNCC.Texts = row.Cells["MA_NCC"].Value?.ToString();
            txtTenNCC.Texts = row.Cells["TEN_NCC"].Value?.ToString();
            txtDiaChi.Texts = row.Cells["DIACHI_NCC"].Value?.ToString();
            txtSDT.Texts = row.Cells["SDT"].Value?.ToString();
            txtEmail.Texts = row.Cells["EMAIL"].Value?.ToString();
            txtMSThue.Texts = row.Cells["MSTHUE"].Value?.ToString();
            txtGhiChu.Texts = row.Cells["GHICHU"].Value?.ToString();
        }

        // --- EVENTS ---

        private void DgvNhaCungCap_SelectionChanged(object sender, EventArgs e)
        {
            if (_currentState == FormState.View)
            {
                if (dgvNhaCungCap.CurrentRow != null)
                {
                    PopulateInputs(dgvNhaCungCap.CurrentRow);
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
            if (dgvNhaCungCap.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SetState(FormState.Editing);
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtMaNCC.Texts))
                {
                    MessageBox.Show("Vui lòng nhập Mã Nhà Cung Cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNCC.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTenNCC.Texts))
                {
                    MessageBox.Show("Vui lòng nhập Tên Nhà Cung Cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNCC.Focus();
                    return;
                }

                if (_currentState == FormState.Adding)
                {
                    if (_service.CheckTonTai(txtMaNCC.Texts))
                    {
                        MessageBox.Show("Mã Nhà Cung Cấp này đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _service.Insert(txtMaNCC.Texts, txtTenNCC.Texts, txtDiaChi.Texts, txtSDT.Texts, txtEmail.Texts, txtMSThue.Texts, txtGhiChu.Texts);
                    MessageBox.Show("Thêm nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_currentState == FormState.Editing)
                {
                    _service.Update(txtMaNCC.Texts, txtTenNCC.Texts, txtDiaChi.Texts, txtSDT.Texts, txtEmail.Texts, txtMSThue.Texts, txtGhiChu.Texts);
                    MessageBox.Show("Cập nhật nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadData();
                SetState(FormState.View);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Texts))
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _service.Delete(txtMaNCC.Texts);
                    MessageBox.Show("Xóa nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    SetState(FormState.View);
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