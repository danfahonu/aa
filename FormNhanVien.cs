using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data; // Đảm bảo using này đúng

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormNhanVien : Form
    {
        private bool isAdding = false;
        private string currentImagePath = null;

        public FormNhanVien()
        {
            InitializeComponent();
        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBoxChucVu();
            SetInputMode(false); // Khóa giao diện khi mới mở
        }

        #region Xử lý dữ liệu

        private void LoadData()
        {
            try
            {
                string query = "SELECT MANV, HOTEN, CHUCVU, DIACHI, SDT, EMAIL, ANH, HOATDONG FROM NHANVIEN";
                DataTable dt = DbHelper.Query(query);
                dgvNhanVien.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxChucVu()
        {
            try
            {
                string query = "SELECT CHUCVU FROM HESOLUONG";
                DataTable dt = DbHelper.Query(query);
                cboChucVu.DataSource = dt;
                cboChucVu.DisplayMember = "CHUCVU";
                cboChucVu.ValueMember = "CHUCVU";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách chức vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaNV.Text = "";
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            cboChucVu.SelectedIndex = -1;
            chkHoatDong.Checked = true;
            picHinhAnh.Image = null;
            currentImagePath = null;
        }

        #endregion

        #region Quản lý trạng thái giao diện (UX)

        private void SetInputMode(bool enable)
        {
            txtMaNV.ReadOnly = !isAdding;
            txtHoTen.ReadOnly = !enable;
            txtDiaChi.ReadOnly = !enable;
            txtSDT.ReadOnly = !enable;
            txtEmail.ReadOnly = !enable;
            cboChucVu.Enabled = enable;
            chkHoatDong.Enabled = enable;
            btnBrowse.Enabled = enable;

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
            txtMaNV.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAdding = false;
            SetInputMode(true);
            txtHoTen.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string maNV = dgvNhanVien.SelectedRows[0].Cells["MANV"].Value.ToString();
                    string query = "DELETE FROM NHANVIEN WHERE MANV = @MaNV";
                    DbHelper.Execute(query, DbHelper.Param("@MaNV", maNV));

                    LoadData();
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (string.IsNullOrWhiteSpace(txtMaNV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Mã và Tên nhân viên không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isAdding)
                {
                    string query = @"
                        INSERT INTO NHANVIEN (MANV, HOTEN, CHUCVU, DIACHI, SDT, EMAIL, ANH, HOATDONG)
                        VALUES (@MaNV, @HoTen, @ChucVu, @DiaChi, @SDT, @Email, @Anh, @HoatDong)";
                    DbHelper.Execute(query,
                        DbHelper.Param("@MaNV", txtMaNV.Text),
                        DbHelper.Param("@HoTen", txtHoTen.Text),
                        DbHelper.Param("@ChucVu", cboChucVu.SelectedValue),
                        DbHelper.Param("@DiaChi", txtDiaChi.Text),
                        DbHelper.Param("@SDT", txtSDT.Text),
                        DbHelper.Param("@Email", txtEmail.Text),
                        DbHelper.Param("@Anh", currentImagePath),
                        DbHelper.Param("@HoatDong", chkHoatDong.Checked)
                    );
                }
                else
                {
                    string query = @"
                        UPDATE NHANVIEN SET
                            HOTEN = @HoTen, CHUCVU = @ChucVu, DIACHI = @DiaChi, SDT = @SDT, 
                            EMAIL = @Email, ANH = @Anh, HOATDONG = @HoatDong
                        WHERE MANV = @MaNV";
                    DbHelper.Execute(query,
                        DbHelper.Param("@HoTen", txtHoTen.Text),
                        DbHelper.Param("@ChucVu", cboChucVu.SelectedValue),
                        DbHelper.Param("@DiaChi", txtDiaChi.Text),
                        DbHelper.Param("@SDT", txtSDT.Text),
                        DbHelper.Param("@Email", txtEmail.Text),
                        DbHelper.Param("@Anh", currentImagePath),
                        DbHelper.Param("@HoatDong", chkHoatDong.Checked),
                        DbHelper.Param("@MaNV", txtMaNV.Text)
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
                dgvNhanVien_SelectionChanged(null, null);
            }
            else
            {
                ClearInputs();
            }
            SetInputMode(false);
            isAdding = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                currentImagePath = openFile.FileName;
                picHinhAnh.Image = new Bitmap(currentImagePath);
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (!isAdding && dgvNhanVien.SelectedRows.Count > 0)
            {
                var row = dgvNhanVien.SelectedRows[0];
                txtMaNV.Text = row.Cells["MANV"].Value?.ToString();
                txtHoTen.Text = row.Cells["HOTEN"].Value?.ToString();
                cboChucVu.SelectedValue = row.Cells["CHUCVU"].Value;
                txtDiaChi.Text = row.Cells["DIACHI"].Value?.ToString();
                txtSDT.Text = row.Cells["SDT"].Value?.ToString();
                txtEmail.Text = row.Cells["EMAIL"].Value?.ToString();
                chkHoatDong.Checked = row.Cells["HOATDONG"].Value != null && (bool)row.Cells["HOATDONG"].Value;

                currentImagePath = row.Cells["ANH"].Value?.ToString();
                if (!string.IsNullOrEmpty(currentImagePath) && File.Exists(currentImagePath))
                {
                    picHinhAnh.Image = Image.FromFile(currentImagePath);
                }
                else
                {
                    picHinhAnh.Image = null;
                }
            }
        }

        #endregion
    }
}