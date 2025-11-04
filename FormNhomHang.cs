using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data; // Đảm bảo using này đúng

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormNhomHang : Form
    {
        private bool isAdding = false;

        public FormNhomHang()
        {
            InitializeComponent();
        }

        private void FormNhomHang_Load(object sender, EventArgs e)
        {
            LoadData();
            SetInputMode(false); // Khóa giao diện khi mới mở
        }

        #region Xử lý dữ liệu

        private void LoadData()
        {
            try
            {
                string query = "SELECT MANHOM, TENNHOM, GHICHU FROM DM_NHOMHANG";
                DataTable dt = DbHelper.Query(query);
                dgvNhomHang.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu nhóm hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaNhom.Text = "";
            txtTenNhom.Text = "";
            txtGhiChu.Text = "";
        }

        #endregion

        #region Quản lý trạng thái giao diện (UX)

        private void SetInputMode(bool enable)
        {
            txtMaNhom.ReadOnly = !isAdding;
            txtTenNhom.ReadOnly = !enable;
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
            txtMaNhom.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhomHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhóm hàng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAdding = false;
            SetInputMode(true);
            txtTenNhom.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhomHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhóm hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhóm hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string maNhom = dgvNhomHang.SelectedRows[0].Cells["MANHOM"].Value.ToString();
                    string query = "DELETE FROM DM_NHOMHANG WHERE MANHOM = @MaNhom";
                    DbHelper.Execute(query, DbHelper.Param("@MaNhom", maNhom));

                    LoadData();
                    MessageBox.Show("Xóa nhóm hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (string.IsNullOrWhiteSpace(txtMaNhom.Text) || string.IsNullOrWhiteSpace(txtTenNhom.Text))
                {
                    MessageBox.Show("Mã và Tên nhóm hàng không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isAdding)
                {
                    string query = @"
                        INSERT INTO DM_NHOMHANG (MANHOM, TENNHOM, GHICHU)
                        VALUES (@MaNhom, @TenNhom, @GhiChu)";
                    DbHelper.Execute(query,
                        DbHelper.Param("@MaNhom", txtMaNhom.Text),
                        DbHelper.Param("@TenNhom", txtTenNhom.Text),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text)
                    );
                }
                else
                {
                    string query = @"
                        UPDATE DM_NHOMHANG SET
                            TENNHOM = @TenNhom, GHICHU = @GhiChu
                        WHERE MANHOM = @MaNhom";
                    DbHelper.Execute(query,
                        DbHelper.Param("@TenNhom", txtTenNhom.Text),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text),
                        DbHelper.Param("@MaNhom", txtMaNhom.Text)
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
                dgvNhomHang_SelectionChanged(null, null);
            }
            else
            {
                ClearInputs();
            }
            SetInputMode(false);
            isAdding = false;
        }

        private void dgvNhomHang_SelectionChanged(object sender, EventArgs e)
        {
            if (!isAdding && dgvNhomHang.SelectedRows.Count > 0)
            {
                var row = dgvNhomHang.SelectedRows[0];
                txtMaNhom.Text = row.Cells["MANHOM"].Value?.ToString();
                txtTenNhom.Text = row.Cells["TENNHOM"].Value?.ToString();
                txtGhiChu.Text = row.Cells["GHICHU"].Value?.ToString();
            }
        }

        #endregion
    }
}