using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormNhanVien : Form
    {
        private enum FormMode { View, Edit, New }
        private FormMode _currentMode = FormMode.View;
        private DataTable _dt;
        private BindingSource _bs = new BindingSource();
        private DataTable _dtChucVu;
        private string _currentImagePath = null;

        public FormNhanVien()
        {
            InitializeComponent();
        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLookups();
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

        private void LoadLookups()
        {
            _dtChucVu = DbHelper.Query("SELECT CHUCVU FROM dbo.HESOLUONG ORDER BY CHUCVU");
            cboChucVu.DataSource = _dtChucVu;
            cboChucVu.DisplayMember = "CHUCVU";
            cboChucVu.ValueMember = "CHUCVU";
        }

        private void LoadData()
        {
            _dt = DbHelper.Query("SELECT MANV, HOTEN, DIACHI, SDT, EMAIL, CHUCVU, ANH FROM dbo.NHANVIEN ORDER BY HOTEN");
            _bs.DataSource = _dt;
            gridMain.DataSource = _bs;
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            gridMain.Columns["MANV"].HeaderText = "Mã NV";
            gridMain.Columns["HOTEN"].HeaderText = "Họ Tên";
            gridMain.Columns["CHUCVU"].HeaderText = "Chức Vụ";
            gridMain.Columns["SDT"].HeaderText = "Số Điện Thoại";
            gridMain.Columns["DIACHI"].HeaderText = "Địa Chỉ";
            gridMain.Columns["EMAIL"].HeaderText = "Email";
            gridMain.Columns["ANH"].Visible = false;
            gridMain.AutoResizeColumns();
        }

        private void DataBindingControl()
        {
            ClearDataBindings();
            txtMaNV.DataBindings.Add("Text", _bs, "MANV", true, DataSourceUpdateMode.Never);
            txtHoTen.DataBindings.Add("Text", _bs, "HOTEN", true, DataSourceUpdateMode.Never);
            txtDiaChi.DataBindings.Add("Text", _bs, "DIACHI", true, DataSourceUpdateMode.Never);
            txtSDT.DataBindings.Add("Text", _bs, "SDT", true, DataSourceUpdateMode.Never);
            txtEmail.DataBindings.Add("Text", _bs, "EMAIL", true, DataSourceUpdateMode.Never);
            cboChucVu.DataBindings.Add("SelectedValue", _bs, "CHUCVU", true, DataSourceUpdateMode.Never);
        }

        private void SetFormMode(FormMode mode)
        {
            _currentMode = mode;
            bool isEditing = (mode != FormMode.View);

            txtHoTen.ReadOnly = !isEditing;
            txtDiaChi.ReadOnly = !isEditing;
            txtSDT.ReadOnly = !isEditing;
            txtEmail.ReadOnly = !isEditing;
            cboChucVu.Enabled = isEditing;
            btnChonAnh.Enabled = isEditing;
            txtMaNV.ReadOnly = (mode != FormMode.New);

            btnMoi.Enabled = !isEditing;
            btnSua.Enabled = !isEditing && _bs.Current != null;
            btnLuu.Enabled = isEditing;
            btnXoa.Enabled = !isEditing && _bs.Current != null;
            gridMain.Enabled = !isEditing;

            if (mode == FormMode.New)
            {
                ClearDataBindings();
                ClearInputControls();
                txtMaNV.Focus();
            }
            else
            {
                if (txtMaNV.DataBindings.Count == 0)
                {
                    DataBindingControl();
                }
            }
        }

        private void ClearDataBindings()
        {
            foreach (Control c in grpChiTiet.Controls) c.DataBindings.Clear();
            picAnh.DataBindings.Clear();
        }

        private void ClearInputControls()
        {
            txtMaNV.Text = "";
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            cboChucVu.SelectedIndex = -1;
            picAnh.Image = null;
            _currentImagePath = null;
        }

        private void gridMain_SelectionChanged(object sender, EventArgs e)
        {
            if (_bs.Current == null) return;
            if (_currentMode != FormMode.View) SetFormMode(FormMode.View);

            DataRowView drv = (DataRowView)_bs.Current;
            string relativePath = drv["ANH"]?.ToString();

            if (!string.IsNullOrEmpty(relativePath))
            {
                string fullPath = Path.Combine(Application.StartupPath, relativePath);
                try
                {
                    using (var bmpTemp = new Bitmap(fullPath))
                    {
                        picAnh.Image = new Bitmap(bmpTemp);
                    }
                }
                catch { picAnh.Image = null; }
            }
            else
            {
                picAnh.Image = null;
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _currentImagePath = openFileDialog1.FileName;
                picAnh.Image = Image.FromFile(_currentImagePath);
            }
        }

        private void btnMoi_Click(object sender, EventArgs e) => SetFormMode(FormMode.New);
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_bs.Current != null)
            {
                _currentImagePath = null;
                SetFormMode(FormMode.Edit);
            }
        }
        private void btnNap_Click(object sender, EventArgs e) => LoadData();

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Mã và Tên nhân viên không được để trống.", "Cảnh báo");
                return;
            }

            try
            {
                string finalImagePath = null;
                if (!string.IsNullOrEmpty(_currentImagePath))
                {
                    string imageDir = Path.Combine(Application.StartupPath, "Images");
                    if (!Directory.Exists(imageDir)) Directory.CreateDirectory(imageDir);

                    string fileName = Path.GetFileName(_currentImagePath);
                    string destPath = Path.Combine(imageDir, fileName);
                    File.Copy(_currentImagePath, destPath, true);
                    finalImagePath = Path.Combine("Images", fileName);
                }
                else if (_currentMode == FormMode.Edit)
                {
                    finalImagePath = ((DataRowView)_bs.Current)["ANH"]?.ToString();
                }

                var p = new[] {
                    DbHelper.Param("@ma", txtMaNV.Text.Trim()),
                    DbHelper.Param("@ten", txtHoTen.Text.Trim()),
                    DbHelper.Param("@diaChi", txtDiaChi.Text.Trim()),
                    DbHelper.Param("@sdt", txtSDT.Text.Trim()),
                    DbHelper.Param("@email", txtEmail.Text.Trim()),
                    DbHelper.Param("@chucVu", cboChucVu.SelectedValue ?? (object)DBNull.Value),
                    DbHelper.Param("@anh", (object)finalImagePath ?? DBNull.Value)
                };

                if (_currentMode == FormMode.New)
                {
                    string sql = "INSERT INTO dbo.NHANVIEN (MANV, HOTEN, DIACHI, SDT, EMAIL, CHUCVU, ANH) VALUES (@ma, @ten, @diaChi, @sdt, @email, @chucVu, @anh)";
                    DbHelper.Execute(sql, p);
                }
                else // Edit
                {
                    string sql = "UPDATE dbo.NHANVIEN SET HOTEN=@ten, DIACHI=@diaChi, SDT=@sdt, EMAIL=@email, CHUCVU=@chucVu, ANH=@anh WHERE MANV=@ma";
                    DbHelper.Execute(sql, p);
                }

                LoadData();
                _bs.Position = _bs.Find("MANV", txtMaNV.Text.Trim());
                SetFormMode(FormMode.View);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                    MessageBox.Show("Mã nhân viên này đã tồn tại. Vui lòng chọn mã khác.", "Lỗi");
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
                    DbHelper.Execute("DELETE FROM dbo.NHANVIEN WHERE MANV=@ma", DbHelper.Param("@ma", txtMaNV.Text));
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa nhân viên này. Có thể do đã có phát sinh giao dịch liên quan.\n\nChi tiết: " + ex.Message, "Lỗi ràng buộc dữ liệu");
                }
            }
        }
    }
}