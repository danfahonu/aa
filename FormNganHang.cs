using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormNganHang : Form
    {
        private enum FormMode { View, Edit, New }
        private FormMode _currentMode = FormMode.View;
        private DataTable _dt;
        private BindingSource _bs = new BindingSource();

        public FormNganHang()
        {
            InitializeComponent();
        }

        private void FormNganHang_Load(object sender, EventArgs e)
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
            _dt = DbHelper.Query("SELECT MANH, TENNH, CHINHANH FROM dbo.DM_NGANHANG ORDER BY TENNH");
            _bs.DataSource = _dt;
            gridMain.DataSource = _bs;
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            gridMain.Columns["MANH"].HeaderText = "Mã NH";
            gridMain.Columns["TENNH"].HeaderText = "Tên Ngân Hàng";
            gridMain.Columns["CHINHANH"].HeaderText = "Chi Nhánh";
            gridMain.AutoResizeColumns();
        }

        private void DataBindingControl()
        {
            ClearDataBindings();
            txtMaNH.DataBindings.Add("Text", _bs, "MANH", true, DataSourceUpdateMode.Never);
            txtTenNH.DataBindings.Add("Text", _bs, "TENNH", true, DataSourceUpdateMode.Never);
            txtChiNhanh.DataBindings.Add("Text", _bs, "CHINHANH", true, DataSourceUpdateMode.Never);
        }

        private void SetFormMode(FormMode mode)
        {
            _currentMode = mode;
            bool isEditing = (mode != FormMode.View);

            txtTenNH.ReadOnly = !isEditing;
            txtChiNhanh.ReadOnly = !isEditing;
            txtMaNH.ReadOnly = (mode != FormMode.New);

            btnMoi.Enabled = !isEditing;
            btnSua.Enabled = !isEditing && _bs.Current != null;
            btnLuu.Enabled = isEditing;
            btnXoa.Enabled = !isEditing && _bs.Current != null;
            gridMain.Enabled = !isEditing;

            if (mode == FormMode.New)
            {
                ClearDataBindings();
                txtMaNH.Text = "";
                txtTenNH.Text = "";
                txtChiNhanh.Text = "";
                txtMaNH.Focus();
            }
            else
            {
                if (txtMaNH.DataBindings.Count == 0)
                {
                    DataBindingControl();
                }
            }
        }

        private void ClearDataBindings()
        {
            txtMaNH.DataBindings.Clear();
            txtTenNH.DataBindings.Clear();
            txtChiNhanh.DataBindings.Clear();
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
            if (string.IsNullOrWhiteSpace(txtMaNH.Text) || string.IsNullOrWhiteSpace(txtTenNH.Text))
            {
                MessageBox.Show("Mã và Tên ngân hàng không được để trống.", "Cảnh báo");
                return;
            }

            try
            {
                var p = new[] {
                    DbHelper.Param("@ma", txtMaNH.Text.Trim()),
                    DbHelper.Param("@ten", txtTenNH.Text.Trim()),
                    DbHelper.Param("@chiNhanh", txtChiNhanh.Text.Trim())
                };

                if (_currentMode == FormMode.New)
                {
                    string sql = "INSERT INTO dbo.DM_NGANHANG (MANH, TENNH, CHINHANH) VALUES (@ma, @ten, @chiNhanh)";
                    DbHelper.Execute(sql, p);
                }
                else // Edit
                {
                    string sql = "UPDATE dbo.DM_NGANHANG SET TENNH=@ten, CHINHANH=@chiNhanh WHERE MANH=@ma";
                    DbHelper.Execute(sql, p);
                }

                LoadData();
                _bs.Position = _bs.Find("MANH", txtMaNH.Text.Trim());
                SetFormMode(FormMode.View);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                    MessageBox.Show("Mã ngân hàng này đã tồn tại. Vui lòng chọn mã khác.", "Lỗi");
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
                    DbHelper.Execute("DELETE FROM dbo.DM_NGANHANG WHERE MANH=@ma", DbHelper.Param("@ma", txtMaNH.Text));
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa ngân hàng này. Có thể do đã có tài khoản thuộc ngân hàng này.\n\nChi tiết: " + ex.Message, "Lỗi ràng buộc dữ liệu");
                }
            }
        }
    }
}