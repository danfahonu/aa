using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;
using DoAnLapTrinhQuanLy.Core;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormQuanLyHeThong : Form
    {
        private bool isAdding = false;
        private int selectedId = -1; // Dùng ID (khóa chính) để xác định người dùng đang chọn

        public FormQuanLyHeThong()
        {
            InitializeComponent();
        }

        private void FormQuanLyHeThong_Load(object sender, EventArgs e)
        {
            try
            {
                ThemeManager.Apply(this);
                LoadDataNguoiDung();
                LoadComboBoxQuyen();
                LoadComboBoxNhanVien();
                SetInputMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải form: " + ex.Message);
            }
        }

        #region Xử lý dữ liệu và ComboBox

        private void LoadDataNguoiDung()
        {
            try
            {
                string query = @"
                    SELECT nd.ID, nd.TAIKHOAN, nd.HOTEN, qh.TENQUYEN, nd.HOATDONG 
                    FROM NGUOIDUNG nd
                    JOIN QUYENHAN qh ON nd.MAQUYEN = qh.MAQUYEN
                    ORDER BY nd.TAIKHOAN";
                DataTable dt = DbHelper.Query(query);
                dgvNguoiDung.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxQuyen()
        {
            try
            {
                string query = "SELECT MAQUYEN, TENQUYEN FROM QUYENHAN";
                DataTable dt = DbHelper.Query(query);
                cboQuyen.DataSource = dt;
                cboQuyen.DisplayMember = "TENQUYEN";
                cboQuyen.ValueMember = "MAQUYEN";
                cboQuyen.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách quyền: " + ex.Message);
            }
        }

        private void LoadComboBoxNhanVien()
        {
            try
            {
                string query = "SELECT MANV, HOTEN FROM NHANVIEN";
                DataTable dt = DbHelper.Query(query);

                // Thêm dòng "Không chọn" (cho tài khoản nội bộ không liên kết nhân viên)
                if (dt.Columns.Contains("MANV"))
                {
                    dt.Columns["MANV"].AllowDBNull = true;
                }
                DataRow dr = dt.NewRow();
                dr["MANV"] = DBNull.Value;
                dr["HOTEN"] = "--- Không chọn / Tài khoản nội bộ ---";
                dt.Rows.InsertAt(dr, 0);

                cboNhanVien.DataSource = dt;
                cboNhanVien.DisplayMember = "HOTEN";
                cboNhanVien.ValueMember = "MANV";
                cboNhanVien.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhân viên: " + ex.Message);
            }
        }

        private void ClearInputs()
        {
            txtTaiKhoan.Text = "";
            txtHoTen.Text = "";
            txtMatKhau.Text = "";
            cboQuyen.SelectedIndex = -1;
            cboNhanVien.SelectedIndex = 0; // Mặc định về "Không chọn"
            chkHoatDong.Checked = true;
            selectedId = -1;
        }

        private void SetInputMode(bool enable)
        {
            txtTaiKhoan.ReadOnly = !isAdding;
            txtHoTen.ReadOnly = !enable;
            txtMatKhau.ReadOnly = !enable;
            cboQuyen.Enabled = enable;
            cboNhanVien.Enabled = enable;
            chkHoatDong.Enabled = enable;

            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;
            btnThem.Enabled = !enable;
            btnSua.Enabled = !enable;
            btnXoa.Enabled = !enable;
        }

        #endregion

        #region Sự kiện

        private void DgvNguoiDung_SelectionChanged(object sender, EventArgs e)
        {
            if (!isAdding && dgvNguoiDung.SelectedRows.Count > 0)
            {
                try
                {
                    selectedId = Convert.ToInt32(dgvNguoiDung.SelectedRows[0].Cells["ID"].Value);

                    var dt = DbHelper.Query("SELECT * FROM NGUOIDUNG WHERE ID = @ID", DbHelper.Param("@ID", selectedId));
                    if (dt.Rows.Count > 0)
                    {
                        var row = dt.Rows[0];
                        txtTaiKhoan.Text = row["TAIKHOAN"].ToString();
                        txtHoTen.Text = row["HOTEN"].ToString();
                        txtMatKhau.Text = ""; // Luôn xóa trắng ô mật khẩu khi chọn để bảo mật
                        cboQuyen.SelectedValue = row["MAQUYEN"];
                        cboNhanVien.SelectedValue = row["MANV"] == DBNull.Value ? DBNull.Value : row["MANV"];
                        chkHoatDong.Checked = (bool)row["HOATDONG"];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn người dùng: " + ex.Message);
                }
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearInputs();
            SetInputMode(true);
            txtTaiKhoan.Focus();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtTaiKhoan.Text.ToLower() == "admin")
            {
                MessageBox.Show("Không thể sửa tài khoản admin gốc.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAdding = false;
            SetInputMode(true);
            txtHoTen.Focus();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtTaiKhoan.Text.ToLower() == "admin")
            {
                MessageBox.Show("Không thể xóa tài khoản admin gốc.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM NGUOIDUNG WHERE ID = @ID";
                    DbHelper.Execute(query, DbHelper.Param("@ID", selectedId));

                    LoadDataNguoiDung();
                    ClearInputs();
                    MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text) || cboQuyen.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ Tên tài khoản, Họ tên và Quyền.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isAdding)
                {
                    if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
                    {
                        MessageBox.Show("Vui lòng nhập mật khẩu cho tài khoản mới.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string query = @"
                        INSERT INTO NGUOIDUNG (TAIKHOAN, MATKHAU, MAQUYEN, HOTEN, HOATDONG, MANV)
                        VALUES (@TaiKhoan, @MatKhau, @MaQuyen, @HoTen, @HoatDong, @MaNV)";
                    DbHelper.Execute(query,
                        DbHelper.Param("@TaiKhoan", txtTaiKhoan.Text),
                        DbHelper.Param("@MatKhau", txtMatKhau.Text),
                        DbHelper.Param("@MaQuyen", cboQuyen.SelectedValue),
                        DbHelper.Param("@HoTen", txtHoTen.Text),
                        DbHelper.Param("@HoatDong", chkHoatDong.Checked),
                        DbHelper.Param("@MaNV", cboNhanVien.SelectedValue)
                    );
                }
                else
                {
                    string query;
                    if (!string.IsNullOrWhiteSpace(txtMatKhau.Text))
                    {
                        query = @"
                            UPDATE NGUOIDUNG SET
                                HOTEN = @HoTen, MAQUYEN = @MaQuyen, HOATDONG = @HoatDong, 
                                MANV = @MaNV, MATKHAU = @MatKhau
                            WHERE ID = @ID";
                        DbHelper.Execute(query,
                           DbHelper.Param("@HoTen", txtHoTen.Text),
                           DbHelper.Param("@MaQuyen", cboQuyen.SelectedValue),
                           DbHelper.Param("@HoatDong", chkHoatDong.Checked),
                           DbHelper.Param("@MaNV", cboNhanVien.SelectedValue),
                           DbHelper.Param("@MatKhau", txtMatKhau.Text),
                           DbHelper.Param("@ID", selectedId)
                       );
                    }
                    else
                    {
                        query = @"
                            UPDATE NGUOIDUNG SET
                                HOTEN = @HoTen, MAQUYEN = @MaQuyen, HOATDONG = @HoatDong, MANV = @MaNV
                            WHERE ID = @ID";
                        DbHelper.Execute(query,
                           DbHelper.Param("@HoTen", txtHoTen.Text),
                           DbHelper.Param("@MaQuyen", cboQuyen.SelectedValue),
                           DbHelper.Param("@HoatDong", chkHoatDong.Checked),
                           DbHelper.Param("@MaNV", cboNhanVien.SelectedValue),
                           DbHelper.Param("@ID", selectedId)
                       );
                    }
                }
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataNguoiDung();
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
            if (!isAdding && dgvNguoiDung.SelectedRows.Count > 0)
            {
                DgvNguoiDung_SelectionChanged(null, null);
            }
            else
            {
                ClearInputs();
            }
            SetInputMode(false);
            isAdding = false;
        }

        #endregion
    }
}