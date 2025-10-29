using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormKhachHang : Form
    {
        private enum FormMode { View, Edit, New }
        private FormMode _currentMode = FormMode.View;
        private DataTable _dt;
        private BindingSource _bs = new BindingSource();

        public FormKhachHang()
        {
            InitializeComponent();
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
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
            _dt = DbHelper.Query("SELECT MAKH, TENKH, DIACHI, SDT, EMAIL, GHICHU FROM dbo.DANHMUCKHACHHANG ORDER BY TENKH");
            _bs.DataSource = _dt;
            gridMain.DataSource = _bs;
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            gridMain.Columns["MAKH"].HeaderText = "Mã KH";
            gridMain.Columns["TENKH"].HeaderText = "Tên Khách Hàng";
            gridMain.Columns["DIACHI"].HeaderText = "Địa Chỉ";
            gridMain.Columns["SDT"].HeaderText = "Số Điện Thoại";
            gridMain.Columns["EMAIL"].HeaderText = "Email";
            gridMain.Columns["GHICHU"].Visible = false; // Ẩn cột ghi chú cho gọn
            gridMain.AutoResizeColumns();
        }

        private void DataBindingControl()
        {
            ClearDataBindings();
            txtMaKH.DataBindings.Add("Text", _bs, "MAKH", true, DataSourceUpdateMode.Never);
            txtTenKH.DataBindings.Add("Text", _bs, "TENKH", true, DataSourceUpdateMode.Never);
            txtDiaChi.DataBindings.Add("Text", _bs, "DIACHI", true, DataSourceUpdateMode.Never);
            txtSDT.DataBindings.Add("Text", _bs, "SDT", true, DataSourceUpdateMode.Never);
            txtEmail.DataBindings.Add("Text", _bs, "EMAIL", true, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", _bs, "GHICHU", true, DataSourceUpdateMode.Never);
        }

        private void SetFormMode(FormMode mode)
        {
            _currentMode = mode;
            bool isEditing = (mode != FormMode.View);

            txtTenKH.ReadOnly = !isEditing;
            txtDiaChi.ReadOnly = !isEditing;
            txtSDT.ReadOnly = !isEditing;
            txtEmail.ReadOnly = !isEditing;
            txtGhiChu.ReadOnly = !isEditing;
            txtMaKH.ReadOnly = (mode != FormMode.New);

            btnMoi.Enabled = !isEditing;
            btnSua.Enabled = !isEditing && _bs.Current != null;
            btnLuu.Enabled = isEditing;
            btnXoa.Enabled = !isEditing && _bs.Current != null;
            gridMain.Enabled = !isEditing;

            if (mode == FormMode.New)
            {
                ClearDataBindings();
                ClearInputControls();
                txtMaKH.Focus();
            }
            else
            {
                if (txtMaKH.DataBindings.Count == 0)
                {
                    DataBindingControl();
                }
            }
        }

        private void ClearDataBindings()
        {
            txtMaKH.DataBindings.Clear();
            txtTenKH.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtEmail.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();
        }

        private void ClearInputControls()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
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
            if (string.IsNullOrWhiteSpace(txtMaKH.Text) || string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                MessageBox.Show("Mã và Tên khách hàng không được để trống.", "Cảnh báo");
                return;
            }

            try
            {
                var p = new[] {
                    DbHelper.Param("@ma", txtMaKH.Text.Trim()),
                    DbHelper.Param("@ten", txtTenKH.Text.Trim()),
                    DbHelper.Param("@diaChi", txtDiaChi.Text.Trim()),
                    DbHelper.Param("@sdt", txtSDT.Text.Trim()),
                    DbHelper.Param("@email", txtEmail.Text.Trim()),
                    DbHelper.Param("@ghiChu", txtGhiChu.Text.Trim())
                };

                if (_currentMode == FormMode.New)
                {
                    string sql = "INSERT INTO dbo.DANHMUCKHACHHANG (MAKH, TENKH, DIACHI, SDT, EMAIL, GHICHU) VALUES (@ma, @ten, @diaChi, @sdt, @email, @ghiChu)";
                    DbHelper.Execute(sql, p);
                }
                else // Edit
                {
                    string sql = "UPDATE dbo.DANHMUCKHACHHANG SET TENKH=@ten, DIACHI=@diaChi, SDT=@sdt, EMAIL=@email, GHICHU=@ghiChu WHERE MAKH=@ma";
                    DbHelper.Execute(sql, p);
                }

                LoadData();
                _bs.Position = _bs.Find("MAKH", txtMaKH.Text.Trim());
                SetFormMode(FormMode.View);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                    MessageBox.Show("Mã khách hàng này đã tồn tại. Vui lòng chọn mã khác.", "Lỗi");
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
                    DbHelper.Execute("DELETE FROM dbo.DANHMUCKHACHHANG WHERE MAKH=@ma", DbHelper.Param("@ma", txtMaKH.Text));
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa khách hàng này. Có thể do đã có phát sinh giao dịch liên quan.\n\nChi tiết: " + ex.Message, "Lỗi ràng buộc dữ liệu");
                }
            }
        }
    }
}