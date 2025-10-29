using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormCaiDatHeThong : Form
    {
        private enum FormMode { View, Edit, New }

        private FormMode _thueMode = FormMode.View;
        private DataTable _dtThue;
        private BindingSource _bsThue = new BindingSource();

        private FormMode _tyGiaMode = FormMode.View;
        private DataTable _dtTyGia;
        private BindingSource _bsTyGia = new BindingSource();
        private DataTable _dtTienTe;

        public FormCaiDatHeThong()
        {
            InitializeComponent();
        }

        private void FormCaiDatHeThong_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData_CongTy();
                LoadData_Thue();
                DataBindingControl_Thue();
                SetFormMode_Thue(FormMode.View);
                LoadLookups_TyGia();
                LoadData_TyGia();
                DataBindingControl_TyGia();
                SetFormMode_TyGia(FormMode.View);
                gridThue.SelectionChanged += (s, ev) => { if (_thueMode != FormMode.View) SetFormMode_Thue(FormMode.View); };
                gridTyGia.SelectionChanged += (s, ev) => { if (_tyGiaMode != FormMode.View) SetFormMode_TyGia(FormMode.View); };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu cài đặt: " + ex.Message, "Lỗi");
            }
        }

        // ... (Phần code của tab Công Ty và Thuế giữ nguyên) ...
        #region Giữ Nguyên
        private void LoadData_CongTy()
        {
            DataTable dt = DbHelper.Query("SELECT TOP 1 * FROM dbo.THONGTINCONGTY");
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                txtTenCty.Text = r["TENCTY"].ToString();
                txtDiaChiCty.Text = r["DIACHI"].ToString();
                txtSDTCty.Text = r["SDT"].ToString();
                txtEmailCty.Text = r["EMAIL"].ToString();
                txtMST.Text = r["MST"].ToString();
                txtGhiChuCty.Text = r["GHICHU"].ToString();
            }
        }
        private void btnLuuCongTy_Click(object sender, EventArgs e)
        {
            try
            {
                var p = new[] {
                    DbHelper.Param("@ten", txtTenCty.Text.Trim()), DbHelper.Param("@diaChi", txtDiaChiCty.Text.Trim()),
                    DbHelper.Param("@sdt", txtSDTCty.Text.Trim()), DbHelper.Param("@email", txtEmailCty.Text.Trim()),
                    DbHelper.Param("@mst", txtMST.Text.Trim()), DbHelper.Param("@ghiChu", txtGhiChuCty.Text.Trim())
                };
                if (Convert.ToInt32(DbHelper.Scalar("SELECT COUNT(*) FROM dbo.THONGTINCONGTY")) > 0)
                {
                    DbHelper.Execute("UPDATE dbo.THONGTINCONGTY SET TENCTY=@ten, DIACHI=@diaChi, SDT=@sdt, EMAIL=@email, MST=@mst, GHICHU=@ghiChu", p);
                }
                else
                {
                    DbHelper.Execute("INSERT INTO dbo.THONGTINCONGTY(TENCTY, DIACHI, SDT, EMAIL, MST, GHICHU) VALUES(@ten, @diaChi, @sdt, @email, @mst, @ghiChu)", p);
                }
                MessageBox.Show("Đã lưu thông tin công ty thành công!", "Thông báo");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi lưu thông tin công ty: " + ex.Message, "Lỗi"); }
        }
        private void LoadData_Thue()
        {
            _dtThue = DbHelper.Query("SELECT MAVAT, TILE, GHICHU FROM dbo.THUEVAT ORDER BY TILE");
            _bsThue.DataSource = _dtThue;
            gridThue.DataSource = _bsThue;
            gridThue.Columns["MAVAT"].HeaderText = "Mã Thuế";
            gridThue.Columns["TILE"].HeaderText = "Tỉ Lệ (%)";
            gridThue.Columns["GHICHU"].HeaderText = "Ghi Chú";
            gridThue.AutoResizeColumns();
        }
        private void DataBindingControl_Thue()
        {
            txtMaThue.DataBindings.Add("Text", _bsThue, "MAVAT", true, DataSourceUpdateMode.Never);
            txtTiLeThue.DataBindings.Add("Text", _bsThue, "TILE", true, DataSourceUpdateMode.Never, "N2");
            txtGhiChuThue.DataBindings.Add("Text", _bsThue, "GHICHU", true, DataSourceUpdateMode.Never);
        }
        private void SetFormMode_Thue(FormMode mode)
        {
            _thueMode = mode;
            bool isEditing = (mode != FormMode.View);
            txtTiLeThue.ReadOnly = !isEditing;
            txtGhiChuThue.ReadOnly = !isEditing;
            txtMaThue.ReadOnly = (mode == FormMode.Edit);
            btnMoiThue.Enabled = !isEditing;
            btnSuaThue.Enabled = !isEditing && _bsThue.Current != null;
            btnLuuThue.Enabled = isEditing;
            btnXoaThue.Enabled = !isEditing && _bsThue.Current != null;
            gridThue.Enabled = !isEditing;
            if (mode == FormMode.New) { _bsThue.AddNew(); txtMaThue.Focus(); }
        }
        private void btnMoiThue_Click(object sender, EventArgs e) => SetFormMode_Thue(FormMode.New);
        private void btnSuaThue_Click(object sender, EventArgs e) => SetFormMode_Thue(FormMode.Edit);
        private void btnNapThue_Click(object sender, EventArgs e) => LoadData_Thue();
        private void btnLuuThue_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaThue.Text) || !decimal.TryParse(txtTiLeThue.Text, out _))
            { MessageBox.Show("Mã thuế và tỉ lệ không hợp lệ.", "Cảnh báo"); return; }
            try
            {
                var p = new[] { DbHelper.Param("@ma", txtMaThue.Text.Trim()), DbHelper.Param("@tiLe", decimal.Parse(txtTiLeThue.Text)), DbHelper.Param("@ghiChu", txtGhiChuThue.Text.Trim()) };
                if (_thueMode == FormMode.New) { DbHelper.Execute("INSERT INTO dbo.THUEVAT(MAVAT, TILE, GHICHU) VALUES(@ma, @tiLe, @ghiChu)", p); }
                else { DbHelper.Execute("UPDATE dbo.THUEVAT SET TILE=@tiLe, GHICHU=@ghiChu WHERE MAVAT=@ma", p); }
                LoadData_Thue();
                SetFormMode_Thue(FormMode.View);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lưu thuế: " + ex.Message, "Lỗi"); }
        }
        private void btnXoaThue_Click(object sender, EventArgs e)
        {
            if (_bsThue.Current != null && MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try { DbHelper.Execute("DELETE FROM dbo.THUEVAT WHERE MAVAT=@ma", DbHelper.Param("@ma", txtMaThue.Text)); LoadData_Thue(); }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa thuế: " + ex.Message, "Lỗi"); }
            }
        }
        #endregion

        #region ========================= QUẢN LÝ TỶ GIÁ =========================

        private void LoadLookups_TyGia()
        {
            _dtTienTe = DbHelper.Query("SELECT MATIENTE, TENTIENTE FROM dbo.DM_TIENTE WHERE MATIENTE <> 'VND'");
            cboTienTe.DataSource = _dtTienTe;
            cboTienTe.ValueMember = "MATIENTE";
            cboTienTe.DisplayMember = "TENTIENTE";
        }

        private void LoadData_TyGia()
        {
            _dtTyGia = DbHelper.Query("SELECT ID, MATIENTE, NGAY, GIATRI FROM dbo.TIGIA ORDER BY NGAY DESC");

            // *** SỬA LỖI: Cho phép cột ID được null TẠM THỜI trong bộ nhớ ***
            _dtTyGia.Columns["ID"].AllowDBNull = true;

            _bsTyGia.DataSource = _dtTyGia;
            gridTyGia.DataSource = _bsTyGia;
            gridTyGia.Columns["ID"].HeaderText = "ID";
            gridTyGia.Columns["MATIENTE"].HeaderText = "Tiền Tệ";
            gridTyGia.Columns["NGAY"].HeaderText = "Ngày";
            gridTyGia.Columns["GIATRI"].HeaderText = "Giá Trị";
            gridTyGia.Columns["GIATRI"].DefaultCellStyle.Format = "N2";
            gridTyGia.AutoResizeColumns();
        }

        private void DataBindingControl_TyGia()
        {
            txtIDTyGia.DataBindings.Add("Text", _bsTyGia, "ID", true, DataSourceUpdateMode.Never);
            cboTienTe.DataBindings.Add("SelectedValue", _bsTyGia, "MATIENTE", true, DataSourceUpdateMode.Never);
            dtpNgayTyGia.DataBindings.Add("Value", _bsTyGia, "NGAY", true, DataSourceUpdateMode.Never);
            txtGiaTri.DataBindings.Add("Text", _bsTyGia, "GIATRI", true, DataSourceUpdateMode.Never, "N2");
        }

        private void SetFormMode_TyGia(FormMode mode)
        {
            _tyGiaMode = mode;
            bool isEditing = (mode != FormMode.View);
            cboTienTe.Enabled = isEditing;
            dtpNgayTyGia.Enabled = isEditing;
            txtGiaTri.ReadOnly = !isEditing;

            btnMoiTyGia.Enabled = !isEditing;
            btnSuaTyGia.Enabled = !isEditing && _bsTyGia.Current != null;
            btnLuuTyGia.Enabled = isEditing;
            btnXoaTyGia.Enabled = !isEditing && _bsTyGia.Current != null;
            gridTyGia.Enabled = !isEditing;

            if (mode == FormMode.New)
            {
                _bsTyGia.AddNew();
                txtIDTyGia.Text = "(Tự động)";
                dtpNgayTyGia.Value = DateTime.Today;
            }
        }

        private void btnMoiTyGia_Click(object sender, EventArgs e) => SetFormMode_TyGia(FormMode.New);
        private void btnSuaTyGia_Click(object sender, EventArgs e) => SetFormMode_TyGia(FormMode.Edit);
        private void btnNapTyGia_Click(object sender, EventArgs e) => LoadData_TyGia();

        private void btnLuuTyGia_Click(object sender, EventArgs e)
        {
            if (cboTienTe.SelectedValue == null || !decimal.TryParse(txtGiaTri.Text, out _))
            { MessageBox.Show("Vui lòng chọn tiền tệ và nhập giá trị hợp lệ.", "Cảnh báo"); return; }
            try
            {
                var p = new[] { DbHelper.Param("@maTienTe", cboTienTe.SelectedValue), DbHelper.Param("@ngay", dtpNgayTyGia.Value), DbHelper.Param("@giaTri", decimal.Parse(txtGiaTri.Text)) };
                if (_tyGiaMode == FormMode.New)
                {
                    DbHelper.Execute("INSERT INTO dbo.TIGIA(MATIENTE, NGAY, GIATRI) VALUES(@maTienTe, @ngay, @giaTri)", p);
                }
                else
                {
                    var pUpdate = new[] {
                        DbHelper.Param("@maTienTe", cboTienTe.SelectedValue), DbHelper.Param("@ngay", dtpNgayTyGia.Value),
                        DbHelper.Param("@giaTri", decimal.Parse(txtGiaTri.Text)), DbHelper.Param("@id", int.Parse(txtIDTyGia.Text)) };
                    DbHelper.Execute("UPDATE dbo.TIGIA SET MATIENTE=@maTienTe, NGAY=@ngay, GIATRI=@giaTri WHERE ID=@id", pUpdate);
                }
                LoadData_TyGia();
                SetFormMode_TyGia(FormMode.View);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lưu tỷ giá: " + ex.Message, "Lỗi"); }
        }

        private void btnXoaTyGia_Click(object sender, EventArgs e)
        {
            if (_bsTyGia.Current != null && MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DbHelper.Execute("DELETE FROM dbo.TIGIA WHERE ID=@id", DbHelper.Param("@id", int.Parse(txtIDTyGia.Text)));
                    LoadData_TyGia();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa tỷ giá: " + ex.Message, "Lỗi"); }
            }
        }
        #endregion
    }
}