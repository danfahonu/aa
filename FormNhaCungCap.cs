using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data; // Đảm bảo using này đúng

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormNhaCungCap : Form
    {
        private bool isAdding = false;

        public FormNhaCungCap()
        {
            InitializeComponent();
        }

        private void FormNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
            SetInputMode(false); // Khóa giao diện khi mới mở
        }

        #region Xử lý dữ liệu

        private void LoadData()
        {
            try
            {
                string query = "SELECT MA_NCC, TEN_NCC, DIACHI_NCC, SDT, EMAIL, MSTHUE, GHICHU FROM DM_NHACUNGCAP";
                DataTable dt = DbHelper.Query(query);
                dgvNhaCungCap.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtMST.Text = "";
            txtGhiChu.Text = "";
        }

        #endregion

        #region Quản lý trạng thái giao diện (UX)

        private void SetInputMode(bool enable)
        {
            txtMaNCC.ReadOnly = !isAdding;
            txtTenNCC.ReadOnly = !enable;
            txtDiaChi.ReadOnly = !enable;
            txtSDT.ReadOnly = !enable;
            txtEmail.ReadOnly = !enable;
            txtMST.ReadOnly = !enable;
            txtGhiChu.ReadOnly = !enable;

            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;
            btnThem.Enabled = !enable;
            btnSua.Enabled = !enable;
            btnXoa.Enabled = !enable;
        }

        #endregion

        #region Sự kiện

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearInputs();
            SetInputMode(true);
            txtMaNCC.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAdding = false;
            SetInputMode(true);
            txtTenNCC.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string maNCC = dgvNhaCungCap.SelectedRows[0].Cells["MA_NCC"].Value.ToString();
                    string query = "DELETE FROM DM_NHACUNGCAP WHERE MA_NCC = @MaNCC";
                    DbHelper.Execute(query, DbHelper.Param("@MaNCC", maNCC));

                    LoadData();
                    MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaNCC.Text) || string.IsNullOrWhiteSpace(txtTenNCC.Text))
                {
                    MessageBox.Show("Mã và Tên nhà cung cấp không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isAdding)
                {
                    string query = @"
                        INSERT INTO DM_NHACUNGCAP (MA_NCC, TEN_NCC, DIACHI_NCC, SDT, EMAIL, MSTHUE, GHICHU)
                        VALUES (@MaNCC, @TenNCC, @DiaChi, @SDT, @Email, @MST, @GhiChu)";
                    DbHelper.Execute(query,
                        DbHelper.Param("@MaNCC", txtMaNCC.Text),
                        DbHelper.Param("@TenNCC", txtTenNCC.Text),
                        DbHelper.Param("@DiaChi", txtDiaChi.Text),
                        DbHelper.Param("@SDT", txtSDT.Text),
                        DbHelper.Param("@Email", txtEmail.Text),
                        DbHelper.Param("@MST", txtMST.Text),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text)
                    );
                }
                else
                {
                    string query = @"
                        UPDATE DM_NHACUNGCAP SET
                            TEN_NCC = @TenNCC, DIACHI_NCC = @DiaChi, SDT = @SDT, 
                            EMAIL = @Email, MSTHUE = @MST, GHICHU = @GhiChu
                        WHERE MA_NCC = @MaNCC";
                    DbHelper.Execute(query,
                        DbHelper.Param("@TenNCC", txtTenNCC.Text),
                        DbHelper.Param("@DiaChi", txtDiaChi.Text),
                        DbHelper.Param("@SDT", txtSDT.Text),
                        DbHelper.Param("@Email", txtEmail.Text),
                        DbHelper.Param("@MST", txtMST.Text),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text),
                        DbHelper.Param("@MaNCC", txtMaNCC.Text)
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (!isAdding)
            {
                dgvNhaCungCap_SelectionChanged(null, null);
            }
            else
            {
                ClearInputs();
            }
            SetInputMode(false);
            isAdding = false;
        }

        private void dgvNhaCungCap_SelectionChanged(object sender, EventArgs e)
        {
            if (!isAdding && dgvNhaCungCap.SelectedRows.Count > 0)
            {
                var row = dgvNhaCungCap.SelectedRows[0];
                txtMaNCC.Text = row.Cells["MA_NCC"].Value?.ToString();
                txtTenNCC.Text = row.Cells["TEN_NCC"].Value?.ToString();
                txtDiaChi.Text = row.Cells["DIACHI_NCC"].Value?.ToString();
                txtSDT.Text = row.Cells["SDT"].Value?.ToString();
                txtEmail.Text = row.Cells["EMAIL"].Value?.ToString();
                txtMST.Text = row.Cells["MSTHUE"].Value?.ToString();
                txtGhiChu.Text = row.Cells["GHICHU"].Value?.ToString();
            }
        }

        #endregion
    }
}