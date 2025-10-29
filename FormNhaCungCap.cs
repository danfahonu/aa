using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormNhaCungCap : Form
    {
        private enum FormMode { View, Edit, New }
        private FormMode _currentMode = FormMode.View;
        private DataTable _dt;
        private BindingSource _bs = new BindingSource();

        public FormNhaCungCap()
        {
            InitializeComponent();
        }

        private void FormNhaCungCap_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                DataBindingControl();
                SetFormMode(FormMode.View);
                gridMain.SelectionChanged += gridMain_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadData()
        {
            _dt = DbHelper.Query("SELECT MA_NCC, TEN_NCC, DIACHI_NCC, SDT, EMAIL, MSTHUE, GHICHU FROM dbo.DM_NHACUNGCAP ORDER BY TEN_NCC");
            _bs.DataSource = _dt;
            gridMain.DataSource = _bs;
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            gridMain.Columns["MA_NCC"].HeaderText = "Mã NCC";
            gridMain.Columns["TEN_NCC"].HeaderText = "Tên Nhà Cung Cấp";
            gridMain.Columns["DIACHI_NCC"].HeaderText = "Địa Chỉ";
            gridMain.Columns["SDT"].HeaderText = "Số Điện Thoại";
            gridMain.Columns["EMAIL"].HeaderText = "Email";
            gridMain.Columns["MSTHUE"].HeaderText = "Mã Số Thuế";
            gridMain.Columns["GHICHU"].Visible = false;
            gridMain.AutoResizeColumns();
        }

        private void DataBindingControl()
        {
            ClearDataBindings();
            txtMaNCC.DataBindings.Add("Text", _bs, "MA_NCC", true, DataSourceUpdateMode.Never);
            txtTenNCC.DataBindings.Add("Text", _bs, "TEN_NCC", true, DataSourceUpdateMode.Never);
            txtDiaChi.DataBindings.Add("Text", _bs, "DIACHI_NCC", true, DataSourceUpdateMode.Never);
            txtSDT.DataBindings.Add("Text", _bs, "SDT", true, DataSourceUpdateMode.Never);
            txtEmail.DataBindings.Add("Text", _bs, "EMAIL", true, DataSourceUpdateMode.Never);
            txtMST.DataBindings.Add("Text", _bs, "MSTHUE", true, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", _bs, "GHICHU", true, DataSourceUpdateMode.Never);
        }

        private void SetFormMode(FormMode mode)
        {
            _currentMode = mode;
            bool isEditing = (mode != FormMode.View);

            txtTenNCC.ReadOnly = !isEditing;
            txtDiaChi.ReadOnly = !isEditing;
            txtSDT.ReadOnly = !isEditing;
            txtEmail.ReadOnly = !isEditing;
            txtMST.ReadOnly = !isEditing;
            txtGhiChu.ReadOnly = !isEditing;
            txtMaNCC.ReadOnly = (mode != FormMode.New);

            btnMoi.Enabled = !isEditing;
            btnSua.Enabled = !isEditing && _bs.Current != null;
            btnLuu.Enabled = isEditing;
            btnXoa.Enabled = !isEditing && _bs.Current != null;
            gridMain.Enabled = !isEditing;

            if (mode == FormMode.New)
            {
                ClearDataBindings();
                ClearInputControls();
                txtMaNCC.Focus();
            }
            else
            {
                if (txtMaNCC.DataBindings.Count == 0)
                {
                    DataBindingControl();
                }
            }
        }

        private void ClearDataBindings()
        {
            foreach (Control c in grpChiTiet.Controls)
            {
                c.DataBindings.Clear();
            }
        }

        private void ClearInputControls()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtMST.Text = "";
            txtGhiChu.Text = "";
        }

        private void gridMain_SelectionChanged(object sender, EventArgs e)
        {
            if (_currentMode != FormMode.View) SetFormMode(FormMode.View);
        }

        private void btnMoi_Click(object sender, EventArgs e) => SetFormMode(FormMode.New);
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_bs.Current != null) SetFormMode(FormMode.Edit);
        }
        private void btnNap_Click(object sender, EventArgs e) => LoadData();

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text) || string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Mã và Tên Nhà cung cấp không được để trống.", "Cảnh báo");
                return;
            }

            try
            {
                var p = new[] {
                    DbHelper.Param("@ma", txtMaNCC.Text.Trim()),
                    DbHelper.Param("@ten", txtTenNCC.Text.Trim()),
                    DbHelper.Param("@diaChi", txtDiaChi.Text.Trim()),
                    DbHelper.Param("@sdt", txtSDT.Text.Trim()),
                    DbHelper.Param("@email", txtEmail.Text.Trim()),
                    DbHelper.Param("@mst", txtMST.Text.Trim()),
                    DbHelper.Param("@ghiChu", txtGhiChu.Text.Trim())
                };

                if (_currentMode == FormMode.New)
                {
                    string sql = "INSERT INTO dbo.DM_NHACUNGCAP (MA_NCC, TEN_NCC, DIACHI_NCC, SDT, EMAIL, MSTHUE, GHICHU) VALUES (@ma, @ten, @diaChi, @sdt, @email, @mst, @ghiChu)";
                    DbHelper.Execute(sql, p);
                }
                else // Edit
                {
                    string sql = "UPDATE dbo.DM_NHACUNGCAP SET TEN_NCC=@ten, DIACHI_NCC=@diaChi, SDT=@sdt, EMAIL=@email, MSTHUE=@mst, GHICHU=@ghiChu WHERE MA_NCC=@ma";
                    DbHelper.Execute(sql, p);
                }

                LoadData();
                _bs.Position = _bs.Find("MA_NCC", txtMaNCC.Text.Trim());
                SetFormMode(FormMode.View);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                    MessageBox.Show("Mã nhà cung cấp này đã tồn tại. Vui lòng chọn mã khác.", "Lỗi");
                else
                    MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_bs.Current != null && MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DbHelper.Execute("DELETE FROM dbo.DM_NHACUNGCAP WHERE MA_NCC=@ma", DbHelper.Param("@ma", txtMaNCC.Text));
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa nhà cung cấp này. Có thể do đã có phát sinh giao dịch liên quan.\n\nChi tiết: " + ex.Message, "Lỗi ràng buộc dữ liệu");
                }
            }
        }
    }
}