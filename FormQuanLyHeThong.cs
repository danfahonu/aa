using DoAnLapTrinhQuanLy.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormQuanLyHeThong : Form
    {
        // ===================================================================================
        // BIẾN TOÀN CỤC VÀ TRẠNG THÁI
        // ===================================================================================
        private enum FormMode { View, Edit, New }

        // Trạng thái cho tab Người dùng
        private FormMode _userMode = FormMode.View;
        private DataTable _dtNguoiDung;
        private BindingSource _bsNguoiDung = new BindingSource();
        private DataTable _dtQuyenHan_ForUserLookup; // Dùng cho combobox

        // Trạng thái cho tab Quyền hạn
        private FormMode _roleMode = FormMode.View;
        private DataTable _dtQuyenHan;
        private BindingSource _bsQuyenHan = new BindingSource();

        public FormQuanLyHeThong()
        {
            InitializeComponent();
        }

        private void FormQuanLyHeThong_Load(object sender, EventArgs e)
        {
            try
            {
                // Tải dữ liệu cho cả 2 tab
                LoadAllData();

                // Binding dữ liệu
                DataBindingControl_User();
                DataBindingControl_Role();

                // Cài đặt trạng thái ban đầu
                SetFormMode_User(FormMode.View);
                SetFormMode_Role(FormMode.View);

                // Gán sự kiện cho việc chọn dòng trên grid
                gridNguoiDung.SelectionChanged += gridNguoiDung_SelectionChanged;
                gridQuyenHan.SelectionChanged += gridQuyenHan_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllData()
        {
            LoadData_QuyenHan();
            LoadData_NguoiDung();
        }

        #region ======================== QUẢN LÝ NGƯỜI DÙNG ========================

        void LoadData_NguoiDung()
        {
            string sql = @"
                SELECT U.ID, U.TAIKHOAN, U.HOTEN, U.MAQUYEN, U.HOATDONG, QH.TENQUYEN
                FROM dbo.NGUOIDUNG U
                LEFT JOIN dbo.QUYENHAN QH ON U.MAQUYEN = QH.MAQUYEN
                ORDER BY U.TAIKHOAN";

            _dtNguoiDung = DbHelper.Query(sql);
            _bsNguoiDung.DataSource = _dtNguoiDung;
            gridNguoiDung.DataSource = _bsNguoiDung;

            // Cấu hình hiển thị Grid
            if (gridNguoiDung.Columns.Contains("ID")) gridNguoiDung.Columns["ID"].Visible = false;
            if (gridNguoiDung.Columns.Contains("TAIKHOAN")) gridNguoiDung.Columns["TAIKHOAN"].HeaderText = "Tài khoản";
            if (gridNguoiDung.Columns.Contains("HOTEN")) gridNguoiDung.Columns["HOTEN"].HeaderText = "Họ tên";
            if (gridNguoiDung.Columns.Contains("TENQUYEN")) gridNguoiDung.Columns["TENQUYEN"].HeaderText = "Quyền hạn";
            if (gridNguoiDung.Columns.Contains("HOATDONG")) gridNguoiDung.Columns["HOATDONG"].HeaderText = "Hoạt động";
            if (gridNguoiDung.Columns.Contains("MAQUYEN")) gridNguoiDung.Columns["MAQUYEN"].Visible = false;
            gridNguoiDung.AutoResizeColumns();
        }

        private void DataBindingControl_User()
        {
            txtTaiKhoan.DataBindings.Clear();
            txtHoTen.DataBindings.Clear();
            cboQuyenHan_User.DataBindings.Clear();
            chkHoatDong.DataBindings.Clear();

            txtTaiKhoan.DataBindings.Add("Text", _bsNguoiDung, "TAIKHOAN", true, DataSourceUpdateMode.Never);
            txtHoTen.DataBindings.Add("Text", _bsNguoiDung, "HOTEN", true, DataSourceUpdateMode.Never);
            cboQuyenHan_User.DataBindings.Add("SelectedValue", _bsNguoiDung, "MAQUYEN", true, DataSourceUpdateMode.Never);
            chkHoatDong.DataBindings.Add("Checked", _bsNguoiDung, "HOATDONG", true, DataSourceUpdateMode.Never);
        }

        private void SetFormMode_User(FormMode mode)
        {
            _userMode = mode;
            bool isEditing = (mode == FormMode.Edit || mode == FormMode.New);

            txtHoTen.ReadOnly = !isEditing;
            cboQuyenHan_User.Enabled = isEditing;
            chkHoatDong.Enabled = isEditing;
            txtTaiKhoan.ReadOnly = (mode != FormMode.New);

            btnMoiUser.Enabled = (mode == FormMode.View);
            btnSuaUser.Enabled = (mode == FormMode.View && _bsNguoiDung.Current != null);
            btnLuuUser.Enabled = isEditing;
            btnXoaUser.Enabled = (mode == FormMode.View && _bsNguoiDung.Current != null);
            gridNguoiDung.Enabled = (mode == FormMode.View);
        }

        private void gridNguoiDung_SelectionChanged(object sender, EventArgs e)
        {
            if (_userMode != FormMode.View) SetFormMode_User(FormMode.View);
        }

        private void btnMoiUser_Click(object sender, EventArgs e)
        {
            _bsNguoiDung.AddNew();
            txtTaiKhoan.Text = "";
            chkHoatDong.Checked = true;
            SetFormMode_User(FormMode.New);
            txtTaiKhoan.Focus();
        }

        private void btnSuaUser_Click(object sender, EventArgs e)
        {
            if (_bsNguoiDung.Current == null) return;
            SetFormMode_User(FormMode.Edit);
            txtHoTen.Focus();
        }

        private void btnLuuUser_Click(object sender, EventArgs e)
        {
            var taiKhoan = txtTaiKhoan.Text.Trim();
            if (string.IsNullOrEmpty(taiKhoan)) { MessageBox.Show("Vui lòng nhập Tài khoản.", "Cảnh báo"); return; }
            if (cboQuyenHan_User.SelectedValue == null) { MessageBox.Show("Vui lòng chọn Quyền hạn.", "Cảnh báo"); return; }

            try
            {
                var parameters = new List<SqlParameter>
                {
                    DbHelper.Param("@TAIKHOAN", taiKhoan),
                    DbHelper.Param("@HOTEN", string.IsNullOrWhiteSpace(txtHoTen.Text) ? (object)DBNull.Value : txtHoTen.Text.Trim()),
                    DbHelper.Param("@MAQUYEN", cboQuyenHan_User.SelectedValue),
                    DbHelper.Param("@HOATDONG", chkHoatDong.Checked)
                };

                if (_userMode == FormMode.New)
                {
                    if (DbHelper.Scalar("SELECT 1 FROM dbo.NGUOIDUNG WHERE TAIKHOAN=@TAIKHOAN", DbHelper.Param("@TAIKHOAN", taiKhoan)) != null)
                    {
                        MessageBox.Show("Tài khoản đã tồn tại.", "Lỗi");
                        return;
                    }
                    parameters.Add(DbHelper.Param("@MATKHAU", "123")); // Mật khẩu mặc định
                    string sql = "INSERT INTO dbo.NGUOIDUNG(TAIKHOAN, MATKHAU, HOTEN, MAQUYEN, HOATDONG) VALUES(@TAIKHOAN, @MATKHAU, @HOTEN, @MAQUYEN, @HOATDONG)";
                    DbHelper.Execute(sql, parameters.ToArray());
                }
                else // Edit mode
                {
                    var currentId = ((DataRowView)_bsNguoiDung.Current)["ID"];
                    parameters.Add(DbHelper.Param("@ID", currentId));
                    string sql = "UPDATE dbo.NGUOIDUNG SET HOTEN=@HOTEN, MAQUYEN=@MAQUYEN, HOATDONG=@HOATDONG WHERE ID=@ID";
                    DbHelper.Execute(sql, parameters.ToArray());
                }

                LoadData_NguoiDung();
                SetFormMode_User(FormMode.View);
                MessageBox.Show("Đã lưu thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi");
            }
        }

        private void btnXoaUser_Click(object sender, EventArgs e)
        {
            if (_bsNguoiDung.Current == null) return;
            var taiKhoan = ((DataRowView)_bsNguoiDung.Current)["TAIKHOAN"];
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa tài khoản '{taiKhoan}'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    var userId = ((DataRowView)_bsNguoiDung.Current)["ID"];
                    DbHelper.Execute("DELETE FROM dbo.NGUOIDUNG WHERE ID=@ID", DbHelper.Param("@ID", userId));
                    LoadData_NguoiDung();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi");
                }
            }
        }

        private void btnNapUser_Click(object sender, EventArgs e) => LoadData_NguoiDung();

        #endregion

        #region ========================= QUẢN LÝ QUYỀN HẠN =========================

        private void LoadData_QuyenHan()
        {
            // Tải dữ liệu cho lưới Quyền hạn
            _dtQuyenHan = DbHelper.Query("SELECT MAQUYEN, TENQUYEN, GHICHU FROM dbo.QUYENHAN ORDER BY TENQUYEN");
            _bsQuyenHan.DataSource = _dtQuyenHan;
            gridQuyenHan.DataSource = _bsQuyenHan;

            // Tải dữ liệu cho ComboBox trên tab Người dùng (chỉ cần làm 1 lần)
            if (_dtQuyenHan_ForUserLookup == null)
            {
                _dtQuyenHan_ForUserLookup = _dtQuyenHan.Copy();
                cboQuyenHan_User.DataSource = _dtQuyenHan_ForUserLookup;
                cboQuyenHan_User.ValueMember = "MAQUYEN";
                cboQuyenHan_User.DisplayMember = "TENQUYEN";
            }

            // Cấu hình hiển thị Grid
            gridQuyenHan.Columns["MAQUYEN"].HeaderText = "Mã Quyền";
            gridQuyenHan.Columns["TENQUYEN"].HeaderText = "Tên Quyền Hạn";
            gridQuyenHan.Columns["GHICHU"].HeaderText = "Ghi Chú";
            gridQuyenHan.AutoResizeColumns();
        }

        private void DataBindingControl_Role()
        {
            txtMaQuyen.DataBindings.Clear();
            txtTenQuyen.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();

            txtMaQuyen.DataBindings.Add("Text", _bsQuyenHan, "MAQUYEN", true, DataSourceUpdateMode.Never);
            txtTenQuyen.DataBindings.Add("Text", _bsQuyenHan, "TENQUYEN", true, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", _bsQuyenHan, "GHICHU", true, DataSourceUpdateMode.Never);
        }

        private void SetFormMode_Role(FormMode mode)
        {
            _roleMode = mode;
            bool isEditing = (mode == FormMode.Edit || mode == FormMode.New);

            txtTenQuyen.ReadOnly = !isEditing;
            txtGhiChu.ReadOnly = !isEditing;

            btnMoiRole.Enabled = (mode == FormMode.View);
            btnSuaRole.Enabled = (mode == FormMode.View && _bsQuyenHan.Current != null);
            btnLuuRole.Enabled = isEditing;
            btnXoaRole.Enabled = (mode == FormMode.View && _bsQuyenHan.Current != null);
            gridQuyenHan.Enabled = (mode == FormMode.View);

            if (mode == FormMode.New)
            {
                _bsQuyenHan.AddNew();
                txtMaQuyen.Text = "(Tự động)";
            }
        }

        private void gridQuyenHan_SelectionChanged(object sender, EventArgs e)
        {
            if (_roleMode != FormMode.View) SetFormMode_Role(FormMode.View);
        }

        private void btnMoiRole_Click(object sender, EventArgs e) => SetFormMode_Role(FormMode.New);

        private void btnSuaRole_Click(object sender, EventArgs e)
        {
            if (_bsQuyenHan.Current == null) return;
            SetFormMode_Role(FormMode.Edit);
        }

        private void btnLuuRole_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenQuyen.Text))
            {
                MessageBox.Show("Tên quyền hạn không được để trống.", "Cảnh báo");
                return;
            }
            try
            {
                if (_roleMode == FormMode.New)
                {
                    string sql = "INSERT INTO dbo.QUYENHAN(TENQUYEN, GHICHU) VALUES(@ten, @ghiChu)";
                    DbHelper.Execute(sql, DbHelper.Param("@ten", txtTenQuyen.Text.Trim()), DbHelper.Param("@ghiChu", txtGhiChu.Text.Trim()));
                }
                else // Edit mode
                {
                    string sql = "UPDATE dbo.QUYENHAN SET TENQUYEN=@ten, GHICHU=@ghiChu WHERE MAQUYEN=@ma";
                    DbHelper.Execute(sql, DbHelper.Param("@ten", txtTenQuyen.Text.Trim()), DbHelper.Param("@ghiChu", txtGhiChu.Text.Trim()), DbHelper.Param("@ma", Convert.ToInt32(txtMaQuyen.Text)));
                }

                LoadAllData(); // Tải lại dữ liệu cho cả 2 tab
                SetFormMode_Role(FormMode.View);
                MessageBox.Show("Đã lưu thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY"))
                    MessageBox.Show("Tên quyền hạn này đã tồn tại.", "Lỗi");
                else
                    MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi");
            }
        }

        private void btnXoaRole_Click(object sender, EventArgs e)
        {
            if (_bsQuyenHan.Current == null) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa quyền hạn này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int maQuyen = Convert.ToInt32(txtMaQuyen.Text);
                    var userCount = DbHelper.Scalar("SELECT COUNT(*) FROM dbo.NGUOIDUNG WHERE MAQUYEN=@ma", DbHelper.Param("@ma", maQuyen));
                    if (Convert.ToInt32(userCount) > 0)
                    {
                        MessageBox.Show("Không thể xóa vì đang có người dùng thuộc quyền hạn này.", "Thông báo");
                        return;
                    }

                    DbHelper.Execute("DELETE FROM dbo.QUYENHAN WHERE MAQUYEN=@ma", DbHelper.Param("@ma", maQuyen));
                    LoadAllData(); // Tải lại dữ liệu cho cả 2 tab
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi");
                }
            }
        }

        private void btnNapRole_Click(object sender, EventArgs e) => LoadData_QuyenHan();

        #endregion
    }
}