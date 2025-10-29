using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormTamUngChamCong : Form
    {
        private enum FormMode { View, Edit, New }
        private FormMode _currentMode = FormMode.View;

        // Cờ để ngăn các sự kiện bị gọi lại chồng chéo
        private bool _isBusy = false;

        private BindingSource _bsNhanVien = new BindingSource();
        private BindingSource _bsTamUng = new BindingSource();
        private BindingSource _bsChamCong = new BindingSource();

        public FormTamUngChamCong()
        {
            InitializeComponent();
        }

        private void FormTamUngChamCong_Load(object sender, EventArgs e)
        {
            try
            {
                LoadNhanVienData();
                LoadLookups();

                // Gán sự kiện
                gridNhanVien.SelectionChanged += gridNhanVien_SelectionChanged;
                tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;

                // Tải dữ liệu ban đầu
                if (_bsNhanVien.Count > 0)
                {
                    _bsNhanVien.Position = 0;
                }
                else
                {
                    SetFormMode(FormMode.View);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khởi tạo: " + ex.Message, "Lỗi"); }
        }

        #region Tải Dữ Liệu

        void LoadNhanVienData()
        {
            _bsNhanVien.DataSource = DbHelper.Query("SELECT MANV, HOTEN FROM dbo.NHANVIEN ORDER BY HOTEN");
            gridNhanVien.DataSource = _bsNhanVien;
            gridNhanVien.Columns["MANV"].HeaderText = "Mã NV";
            gridNhanVien.Columns["HOTEN"].HeaderText = "Họ Tên";
            gridNhanVien.AutoResizeColumns();
        }

        void LoadLookups()
        {
            for (int i = 1; i <= 12; i++) cboThang.Items.Add(i);
        }

        void LoadTabData()
        {
            if (_bsNhanVien.Current == null) return;
            string selectedMaNV = ((DataRowView)_bsNhanVien.Current)["MANV"].ToString();

            if (tabControl.SelectedTab == tabTamUng)
            {
                var dt = DbHelper.Query("SELECT ID, NGAY, SOTIEN, GHICHU FROM dbo.TAMUNG WHERE MANV = @MANV ORDER BY NGAY DESC", DbHelper.Param("@MANV", selectedMaNV));
                dt.Columns["ID"].AllowDBNull = true;
                _bsTamUng.DataSource = dt;
                gridTamUng.DataSource = _bsTamUng;
            }
            else if (tabControl.SelectedTab == tabChamCong)
            {
                var dt = DbHelper.Query("SELECT ID, THANG, NAM, NGAYCONG, GHICHU FROM dbo.BANGCHAMCONG WHERE MANV = @MANV ORDER BY NAM DESC, THANG DESC", DbHelper.Param("@MANV", selectedMaNV));
                dt.Columns["ID"].AllowDBNull = true;
                _bsChamCong.DataSource = dt;
                gridChamCong.DataSource = _bsChamCong;
            }
        }

        #endregion

        #region Quản lý trạng thái Form

        private void SetFormMode(FormMode mode)
        {
            _currentMode = mode;
            bool isEditing = (mode != FormMode.View);

            // Bật/tắt các ô nhập liệu tùy theo tab đang active
            if (tabControl.SelectedTab == tabTamUng)
            {
                dtpNgayTU.Enabled = isEditing;
                numSoTienTU.Enabled = isEditing;
                txtGhiChuTU.ReadOnly = !isEditing;
            }
            else
            {
                cboThang.Enabled = isEditing;
                txtNam.ReadOnly = !isEditing;
                numNgayCong.Enabled = isEditing;
                txtGhiChuCC.ReadOnly = !isEditing;
            }

            // Bật/tắt các nút của cả 2 tab
            btnMoiTU.Enabled = !isEditing;
            btnSuaTU.Enabled = !isEditing && _bsTamUng.Current != null;
            btnLuuTU.Enabled = isEditing;
            btnXoaTU.Enabled = !isEditing && _bsTamUng.Current != null;
            btnMoiCC.Enabled = !isEditing;
            btnSuaCC.Enabled = !isEditing && _bsChamCong.Current != null;
            btnLuuCC.Enabled = isEditing;
            btnXoaCC.Enabled = !isEditing && _bsChamCong.Current != null;

            // Bật/tắt các thành phần điều hướng chính
            gridNhanVien.Enabled = !isEditing;
            tabControl.Enabled = !isEditing;
        }

        #endregion

        #region Sự kiện Controls

        // *** HÀM CHÍNH ĐỂ XỬ LÝ VIỆC "ĐƠ" ***
        private void UpdateDetailView()
        {
            if (_isBusy) return; // Nếu đang bận, không làm gì cả để tránh vòng lặp

            _isBusy = true; // Báo hiệu là tôi đang bận

            LoadTabData();
            SetFormMode(FormMode.View);

            _isBusy = false; // Xử lý xong, hết bận
        }

        private void gridNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            UpdateDetailView();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDetailView();
        }

        // --- Nút Tạm Ứng ---
        private void btnMoiTU_Click(object sender, EventArgs e)
        {
            if (_bsNhanVien.Current == null) return;
            _bsTamUng.AddNew();
            dtpNgayTU.Value = DateTime.Today;
            SetFormMode(FormMode.New);
            dtpNgayTU.Focus();
        }
        private void btnSuaTU_Click(object sender, EventArgs e) { if (_bsTamUng.Current != null) SetFormMode(FormMode.Edit); }

        private void btnLuuTU_Click(object sender, EventArgs e)
        {
            if (numSoTienTU.Value <= 0) { MessageBox.Show("Số tiền phải lớn hơn 0."); return; }
            try
            {
                var currentNV = ((DataRowView)_bsNhanVien.Current)["MANV"].ToString();
                if (_currentMode == FormMode.New)
                {
                    DbHelper.Execute("INSERT INTO dbo.TAMUNG(MANV, NGAY, SOTIEN, GHICHU) VALUES(@MANV, @NGAY, @SOTIEN, @GHICHU)",
                        DbHelper.Param("@MANV", currentNV), DbHelper.Param("@NGAY", dtpNgayTU.Value.Date),
                        DbHelper.Param("@SOTIEN", numSoTienTU.Value), DbHelper.Param("@GHICHU", txtGhiChuTU.Text.Trim()));
                }
                else
                {
                    var id = ((DataRowView)_bsTamUng.Current)["ID"];
                    DbHelper.Execute("UPDATE dbo.TAMUNG SET NGAY=@NGAY, SOTIEN=@SOTIEN, GHICHU=@GHICHU WHERE ID=@ID",
                        DbHelper.Param("@NGAY", dtpNgayTU.Value.Date), DbHelper.Param("@SOTIEN", numSoTienTU.Value),
                        DbHelper.Param("@GHICHU", txtGhiChuTU.Text.Trim()), DbHelper.Param("@ID", id));
                }
                UpdateDetailView();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi lưu: " + ex.Message); }
        }

        private void btnXoaTU_Click(object sender, EventArgs e)
        {
            if (_bsTamUng.Current != null && MessageBox.Show("Xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try { DbHelper.Execute("DELETE FROM dbo.TAMUNG WHERE ID=@ID", DbHelper.Param("@ID", ((DataRowView)_bsTamUng.Current)["ID"])); UpdateDetailView(); }
                catch (Exception ex) { MessageBox.Show("Lỗi khi xóa: " + ex.Message); }
            }
        }
        private void btnNapTU_Click(object sender, EventArgs e) => UpdateDetailView();

        // --- Nút Chấm Công ---
        private void btnMoiCC_Click(object sender, EventArgs e)
        {
            if (_bsNhanVien.Current == null) return;
            _bsChamCong.AddNew();
            cboThang.Text = DateTime.Today.Month.ToString();
            txtNam.Text = DateTime.Today.Year.ToString();
            SetFormMode(FormMode.New);
            numNgayCong.Focus();
        }
        private void btnSuaCC_Click(object sender, EventArgs e) { if (_bsChamCong.Current != null) SetFormMode(FormMode.Edit); }

        private void btnLuuCC_Click(object sender, EventArgs e)
        {
            try
            {
                var currentNV = ((DataRowView)_bsNhanVien.Current)["MANV"].ToString();
                if (_currentMode == FormMode.New)
                {
                    DbHelper.Execute("INSERT INTO dbo.BANGCHAMCONG(MANV, THANG, NAM, NGAYCONG, GHICHU) VALUES(@MANV, @THANG, @NAM, @NGAYCONG, @GHICHU)",
                        DbHelper.Param("@MANV", currentNV), DbHelper.Param("@THANG", cboThang.Text), DbHelper.Param("@NAM", txtNam.Text),
                        DbHelper.Param("@NGAYCONG", numNgayCong.Value), DbHelper.Param("@GHICHU", txtGhiChuCC.Text.Trim()));
                }
                else
                {
                    var id = ((DataRowView)_bsChamCong.Current)["ID"];
                    DbHelper.Execute("UPDATE dbo.BANGCHAMCONG SET THANG=@THANG, NAM=@NAM, NGAYCONG=@NGAYCONG, GHICHU=@GHICHU WHERE ID=@ID",
                        DbHelper.Param("@THANG", cboThang.Text), DbHelper.Param("@NAM", txtNam.Text),
                        DbHelper.Param("@NGAYCONG", numNgayCong.Value), DbHelper.Param("@GHICHU", txtGhiChuCC.Text.Trim()),
                        DbHelper.Param("@ID", id));
                }
                UpdateDetailView();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi lưu: " + ex.Message); }
        }

        private void btnXoaCC_Click(object sender, EventArgs e)
        {
            if (_bsChamCong.Current != null && MessageBox.Show("Xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try { DbHelper.Execute("DELETE FROM dbo.BANGCHAMCONG WHERE ID=@ID", DbHelper.Param("@ID", ((DataRowView)_bsChamCong.Current)["ID"])); UpdateDetailView(); }
                catch (Exception ex) { MessageBox.Show("Lỗi khi xóa: " + ex.Message); }
            }
        }
        private void btnNapCC_Click(object sender, EventArgs e) => UpdateDetailView();

        #endregion
    }
}