using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormQuanLyTaiKhoanNganHang : Form
    {
        private enum FormMode { View, Edit, New }
        private FormMode _currentMode = FormMode.View;

        private DataTable _dt;
        private BindingSource _bs = new BindingSource();

        private DataTable _dtNganHang;
        private DataTable _dtNhaCungCap;

        public FormQuanLyTaiKhoanNganHang()
        {
            InitializeComponent();
        }

        private void FormQuanLyTaiKhoanNganHang_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLookups();
                SetupLoaiTaiKhoanComboBox();
                SetFormMode(FormMode.View);
                gridMain.SelectionChanged += (s, ev) => { if (_currentMode != FormMode.View) SetFormMode(FormMode.View); };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message, "Lỗi");
            }
        }

        private void LoadLookups()
        {
            _dtNganHang = DbHelper.Query("SELECT MANH, TENNH FROM dbo.DM_NGANHANG ORDER BY TENNH");
            cboNganHang.DataSource = _dtNganHang;
            cboNganHang.ValueMember = "MANH";
            cboNganHang.DisplayMember = "TENNH";

            _dtNhaCungCap = DbHelper.Query("SELECT MA_NCC, TEN_NCC FROM dbo.DM_NHACUNGCAP ORDER BY TEN_NCC");
            cboDoiTuong.DataSource = _dtNhaCungCap;
            cboDoiTuong.ValueMember = "MA_NCC";
            cboDoiTuong.DisplayMember = "TEN_NCC";
        }

        private void SetupLoaiTaiKhoanComboBox()
        {
            var items = new[] {
                new { Value = "CTY", Text = "Tài khoản của Công ty" },
                new { Value = "NCC", Text = "Tài khoản của Nhà Cung Cấp" }
            };
            cboLoaiTaiKhoan.DataSource = items;
            cboLoaiTaiKhoan.ValueMember = "Value";
            cboLoaiTaiKhoan.DisplayMember = "Text";
            cboLoaiTaiKhoan.SelectedIndex = 0;
        }

        private void cboLoaiTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string loaiTK = cboLoaiTaiKhoan.SelectedValue.ToString();

            string sql = @"
                SELECT tk.ID, tk.LOAI_TK, tk.MA_DOITUONG, tk.NGANHANG, tk.SO_TK, tk.CHU_TK, tk.GHICHU,
                       nh.TENNH, ISNULL(ncc.TEN_NCC, N'-- Tài khoản công ty --') AS TEN_DOITUONG
                FROM dbo.DM_TAIKHOAN_NGANHANG tk
                JOIN dbo.DM_NGANHANG nh ON tk.NGANHANG = nh.MANH
                LEFT JOIN dbo.DM_NHACUNGCAP ncc ON tk.MA_DOITUONG = ncc.MA_NCC AND tk.LOAI_TK = 'NCC'
                WHERE tk.LOAI_TK = @LoaiTK ORDER BY TEN_DOITUONG, nh.TENNH";

            _dt = DbHelper.Query(sql, DbHelper.Param("@LoaiTK", loaiTK));

            // *** SỬA LỖI: Cho phép cột ID được null TẠM THỜI trong bộ nhớ ***
            _dt.Columns["ID"].AllowDBNull = true;

            _bs.DataSource = _dt;
            gridMain.DataSource = _bs;

            ConfigureGrid();
            DataBindingControl();

            bool isNCC = (loaiTK == "NCC");
            lblDoiTuong.Visible = isNCC;
            cboDoiTuong.Visible = isNCC;
        }

        private void ConfigureGrid()
        {
            if (gridMain.DataSource == null) return;
            gridMain.Columns["ID"].HeaderText = "ID";
            gridMain.Columns["TEN_DOITUONG"].HeaderText = "Chủ Sở Hữu";
            gridMain.Columns["TENNH"].HeaderText = "Ngân Hàng";
            gridMain.Columns["SO_TK"].HeaderText = "Số Tài Khoản";
            gridMain.Columns["CHU_TK"].HeaderText = "Tên Chủ TK";

            gridMain.Columns["LOAI_TK"].Visible = false;
            gridMain.Columns["MA_DOITUONG"].Visible = false;
            gridMain.Columns["NGANHANG"].Visible = false;
            gridMain.Columns["GHICHU"].Visible = false;
            gridMain.AutoResizeColumns();
        }

        private void DataBindingControl()
        {
            txtID.DataBindings.Clear();
            cboDoiTuong.DataBindings.Clear();
            cboNganHang.DataBindings.Clear();
            txtSoTaiKhoan.DataBindings.Clear();
            txtChuTaiKhoan.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();

            txtID.DataBindings.Add("Text", _bs, "ID", true, DataSourceUpdateMode.Never);
            cboDoiTuong.DataBindings.Add("SelectedValue", _bs, "MA_DOITUONG", true, DataSourceUpdateMode.Never);
            cboNganHang.DataBindings.Add("SelectedValue", _bs, "NGANHANG", true, DataSourceUpdateMode.Never);
            txtSoTaiKhoan.DataBindings.Add("Text", _bs, "SO_TK", true, DataSourceUpdateMode.Never);
            txtChuTaiKhoan.DataBindings.Add("Text", _bs, "CHU_TK", true, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", _bs, "GHICHU", true, DataSourceUpdateMode.Never);
        }

        private void SetFormMode(FormMode mode)
        {
            _currentMode = mode;
            bool isEditing = (mode != FormMode.View);

            cboDoiTuong.Enabled = isEditing;
            cboNganHang.Enabled = isEditing;
            txtSoTaiKhoan.ReadOnly = !isEditing;
            txtChuTaiKhoan.ReadOnly = !isEditing;
            txtGhiChu.ReadOnly = !isEditing;

            btnMoi.Enabled = !isEditing;
            btnSua.Enabled = !isEditing && _bs.Current != null;
            btnLuu.Enabled = isEditing;
            btnXoa.Enabled = !isEditing && _bs.Current != null;
            gridMain.Enabled = !isEditing;
            cboLoaiTaiKhoan.Enabled = !isEditing;

            if (mode == FormMode.New) _bs.AddNew();
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            SetFormMode(FormMode.New);
            txtID.Text = "(Tự động)";
            if (cboDoiTuong.Visible) cboDoiTuong.Focus();
            else cboNganHang.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e) => SetFormMode(FormMode.Edit);
        private void btnNap_Click(object sender, EventArgs e) => LoadData();

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string loaiTK = cboLoaiTaiKhoan.SelectedValue.ToString();
            if (cboNganHang.SelectedValue == null || string.IsNullOrWhiteSpace(txtSoTaiKhoan.Text))
            { MessageBox.Show("Vui lòng chọn ngân hàng và nhập số tài khoản.", "Cảnh báo"); return; }
            if (loaiTK == "NCC" && cboDoiTuong.SelectedValue == null)
            { MessageBox.Show("Vui lòng chọn nhà cung cấp.", "Cảnh báo"); return; }

            try
            {
                string sql;
                string maDoiTuong = (loaiTK == "NCC") ? cboDoiTuong.SelectedValue.ToString() : "1";

                if (_currentMode == FormMode.New)
                {
                    sql = "INSERT INTO dbo.DM_TAIKHOAN_NGANHANG (LOAI_TK, MA_DOITUONG, NGANHANG, SO_TK, CHU_TK, GHICHU) VALUES (@LoaiTK, @MaDoiTuong, @NganHang, @SoTK, @ChuTK, @GhiChu)";
                    DbHelper.Execute(sql,
                        DbHelper.Param("@LoaiTK", loaiTK), DbHelper.Param("@MaDoiTuong", maDoiTuong),
                        DbHelper.Param("@NganHang", cboNganHang.SelectedValue), DbHelper.Param("@SoTK", txtSoTaiKhoan.Text.Trim()),
                        DbHelper.Param("@ChuTK", txtChuTaiKhoan.Text.Trim()), DbHelper.Param("@GhiChu", txtGhiChu.Text.Trim()));
                }
                else
                {
                    sql = "UPDATE dbo.DM_TAIKHOAN_NGANHANG SET MA_DOITUONG=@MaDoiTuong, NGANHANG=@NganHang, SO_TK=@SoTK, CHU_TK=@ChuTK, GHICHU=@GhiChu WHERE ID=@ID";
                    DbHelper.Execute(sql,
                        DbHelper.Param("@MaDoiTuong", maDoiTuong), DbHelper.Param("@NganHang", cboNganHang.SelectedValue),
                        DbHelper.Param("@SoTK", txtSoTaiKhoan.Text.Trim()), DbHelper.Param("@ChuTK", txtChuTaiKhoan.Text.Trim()),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text.Trim()), DbHelper.Param("@ID", int.Parse(txtID.Text)));
                }
                LoadData();
                SetFormMode(FormMode.View);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi"); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_bs.Current != null && MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DbHelper.Execute("DELETE FROM dbo.DM_TAIKHOAN_NGANHANG WHERE ID=@ID", DbHelper.Param("@ID", int.Parse(txtID.Text)));
                    LoadData();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi"); }
            }
        }
    }
}