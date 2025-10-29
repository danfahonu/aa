using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormNhomHang : Form
    {
        private enum FormMode { View, Edit, New }
        private FormMode _currentMode = FormMode.View;
        private DataTable _dt;
        private BindingSource _bs = new BindingSource();

        public FormNhomHang()
        {
            InitializeComponent();
        }

        private void FormNhomHang_Load(object sender, EventArgs e)
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
            _dt = DbHelper.Query("SELECT MANHOM, TENNHOM, GHICHU FROM dbo.DM_NHOMHANG ORDER BY TENNHOM");
            _bs.DataSource = _dt;
            gridMain.DataSource = _bs;
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            gridMain.Columns["MANHOM"].HeaderText = "Mã Nhóm";
            gridMain.Columns["TENNHOM"].HeaderText = "Tên Nhóm Hàng";
            gridMain.Columns["GHICHU"].HeaderText = "Ghi Chú";
            gridMain.AutoResizeColumns();
        }

        private void DataBindingControl()
        {
            txtMaNhom.DataBindings.Clear();
            txtTenNhom.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();

            txtMaNhom.DataBindings.Add("Text", _bs, "MANHOM", true, DataSourceUpdateMode.Never);
            txtTenNhom.DataBindings.Add("Text", _bs, "TENNHOM", true, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", _bs, "GHICHU", true, DataSourceUpdateMode.Never);
        }

        private void SetFormMode(FormMode mode)
        {
            _currentMode = mode;
            bool isEditing = (mode != FormMode.View);

            txtTenNhom.ReadOnly = !isEditing;
            txtGhiChu.ReadOnly = !isEditing;
            txtMaNhom.ReadOnly = (mode != FormMode.New);

            btnMoi.Enabled = !isEditing;
            btnSua.Enabled = !isEditing && _bs.Current != null;
            btnLuu.Enabled = isEditing;
            btnXoa.Enabled = !isEditing && _bs.Current != null;
            gridMain.Enabled = !isEditing;

            if (mode == FormMode.New)
            {
                ClearDataBindings();
                txtMaNhom.Text = "";
                txtTenNhom.Text = "";
                txtGhiChu.Text = "";
                txtMaNhom.Focus();
            }
            else
            {
                if (txtMaNhom.DataBindings.Count == 0)
                {
                    DataBindingControl();
                }
            }
        }

        private void ClearDataBindings()
        {
            txtMaNhom.DataBindings.Clear();
            txtTenNhom.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();
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
            if (string.IsNullOrWhiteSpace(txtMaNhom.Text) || string.IsNullOrWhiteSpace(txtTenNhom.Text))
            {
                MessageBox.Show("Mã và Tên nhóm không được để trống.", "Cảnh báo");
                return;
            }

            try
            {
                var p = new[] {
                    DbHelper.Param("@ma", txtMaNhom.Text.Trim()),
                    DbHelper.Param("@ten", txtTenNhom.Text.Trim()),
                    DbHelper.Param("@ghiChu", txtGhiChu.Text.Trim())
                };

                if (_currentMode == FormMode.New)
                {
                    string sql = "INSERT INTO dbo.DM_NHOMHANG (MANHOM, TENNHOM, GHICHU) VALUES (@ma, @ten, @ghiChu)";
                    DbHelper.Execute(sql, p);
                }
                else // Edit
                {
                    string sql = "UPDATE dbo.DM_NHOMHANG SET TENNHOM=@ten, GHICHU=@ghiChu WHERE MANHOM=@ma";
                    DbHelper.Execute(sql, p);
                }

                LoadData();
                _bs.Position = _bs.Find("MANHOM", txtMaNhom.Text.Trim());
                SetFormMode(FormMode.View);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                    MessageBox.Show("Mã nhóm này đã tồn tại. Vui lòng chọn mã khác.", "Lỗi");
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
                    DbHelper.Execute("DELETE FROM dbo.DM_NHOMHANG WHERE MANHOM=@ma", DbHelper.Param("@ma", txtMaNhom.Text));
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa nhóm hàng này. Có thể do đã có hàng hóa thuộc nhóm này.\n\nChi tiết: " + ex.Message, "Lỗi ràng buộc dữ liệu");
                }
            }
        }
    }
}