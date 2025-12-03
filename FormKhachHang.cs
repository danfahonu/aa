using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data; // Đảm bảo using này đúng

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormKhachHang : Form
    {
        private bool isAdding = false;

        public FormKhachHang()
        {
            InitializeComponent();
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            ThemeManager.Apply(this);
            LoadData();
            SetInputMode(false); // Khóa giao diện khi mới mở
        }

        #region Xử lý dữ liệu

        private void LoadData()
        {
            try
            {
                string query = "SELECT MAKH, TENKH, DIACHI, SDT, EMAIL, GHICHU FROM DANHMUCKHACHHANG";
                DataTable dt = DbHelper.Query(query);
                dgvKhachHang.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtGhiChu.Text = "";
        }

        #endregion

        #region Quản lý trạng thái giao diện (UX)

        private void SetInputMode(bool enable)
        {
            txtMaKH.ReadOnly = !isAdding;
            txtTenKH.ReadOnly = !enable;
            txtDiaChi.ReadOnly = !enable;
            txtSDT.ReadOnly = !enable;
            txtEmail.ReadOnly = !enable;
            txtGhiChu.ReadOnly = !enable;

            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;
            btnThem.Enabled = !enable;
            btnSua.Enabled = !enable;
            btnXoa.Enabled = !enable;
        }

        #endregion

        #region Sự kiện

        private void BtnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearInputs();
            SetInputMode(true);
            txtMaKH.Focus();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAdding = false;
            SetInputMode(true);
            txtTenKH.Focus();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string maKH = dgvKhachHang.SelectedRows[0].Cells["MAKH"].Value.ToString();
                    string query = "DELETE FROM DANHMUCKHACHHANG WHERE MAKH = @MaKH";
                    DbHelper.Execute(query, DbHelper.Param("@MaKH", maKH));

                    LoadData();
                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaKH.Text) || string.IsNullOrWhiteSpace(txtTenKH.Text))
                {
                    MessageBox.Show("Mã và Tên khách hàng không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isAdding)
                {
                    string query = @"
                        INSERT INTO DANHMUCKHACHHANG (MAKH, TENKH, DIACHI, SDT, EMAIL, GHICHU, NGAYTAO)
                        VALUES (@MaKH, @TenKH, @DiaChi, @SDT, @Email, @GhiChu, @NgayTao)";
                    DbHelper.Execute(query,
                        DbHelper.Param("@MaKH", txtMaKH.Text),
                        DbHelper.Param("@TenKH", txtTenKH.Text),
                        DbHelper.Param("@DiaChi", txtDiaChi.Text),
                        DbHelper.Param("@SDT", txtSDT.Text),
                        DbHelper.Param("@Email", txtEmail.Text),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text),
                        DbHelper.Param("@NgayTao", DateTime.Now)
                    );
                }
                else
                {
                    string query = @"
                        UPDATE DANHMUCKHACHHANG SET
                            TENKH = @TenKH, DIACHI = @DiaChi, SDT = @SDT, 
                            EMAIL = @Email, GHICHU = @GhiChu
                        WHERE MAKH = @MaKH";
                    DbHelper.Execute(query,
                        DbHelper.Param("@TenKH", txtTenKH.Text),
                        DbHelper.Param("@DiaChi", txtDiaChi.Text),
                        DbHelper.Param("@SDT", txtSDT.Text),
                        DbHelper.Param("@Email", txtEmail.Text),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text),
                        DbHelper.Param("@MaKH", txtMaKH.Text)
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
            if (!isAdding)
            {
                DgvKhachHang_SelectionChanged(null, null);
            }
            else
            {
                ClearInputs();
            }
            SetInputMode(false);
            isAdding = false;
        }

        private void DgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if (!isAdding && dgvKhachHang.SelectedRows.Count > 0)
            {
                var row = dgvKhachHang.SelectedRows[0];
                txtMaKH.Text = row.Cells["MAKH"].Value?.ToString();
                txtTenKH.Text = row.Cells["TENKH"].Value?.ToString();
                txtDiaChi.Text = row.Cells["DIACHI"].Value?.ToString();
                txtSDT.Text = row.Cells["SDT"].Value?.ToString();
                txtEmail.Text = row.Cells["EMAIL"].Value?.ToString();
                txtGhiChu.Text = row.Cells["GHICHU"].Value?.ToString();
            }
        }

        #endregion
    }
}