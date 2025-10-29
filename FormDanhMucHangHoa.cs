using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormDanhMucHangHoa : Form
    {
        private enum FormMode { View, Edit, New }
        private FormMode _currentMode = FormMode.View;
        private DataTable _dt;
        private BindingSource _bs = new BindingSource();
        private DataTable _dtNhomHang;
        private string _currentImagePath = null; // Biến tạm để lưu đường dẫn ảnh mới

        public FormDanhMucHangHoa()
        {
            InitializeComponent();
        }

        private void FormDanhMucHangHoa_Load(object sender, EventArgs e)
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
            _dtNhomHang = DbHelper.Query("SELECT MANHOM, TENNHOM FROM dbo.DM_NHOMHANG ORDER BY TENNHOM");
            cboNhomHang.DataSource = _dtNhomHang;
            cboNhomHang.ValueMember = "MANHOM";
            cboNhomHang.DisplayMember = "TENNHOM";
        }

        private void LoadData()
        {
            // Thêm cột ANH vào câu truy vấn
            _dt = DbHelper.Query("SELECT MAHH, TENHH, MANHOM, DVT, GIABAN, GIAVON, ACTIVE, ANH FROM dbo.DM_HANGHOA ORDER BY TENHH");
            _bs.DataSource = _dt;
            gridMain.DataSource = _bs;
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            gridMain.Columns["MAHH"].HeaderText = "Mã Hàng";
            gridMain.Columns["TENHH"].HeaderText = "Tên Hàng Hóa";
            gridMain.Columns["MANHOM"].HeaderText = "Mã Nhóm";
            gridMain.Columns["DVT"].HeaderText = "ĐVT";
            gridMain.Columns["GIABAN"].HeaderText = "Giá Bán";
            gridMain.Columns["GIABAN"].DefaultCellStyle.Format = "N0";
            gridMain.Columns["GIAVON"].HeaderText = "Giá Vốn";
            gridMain.Columns["GIAVON"].DefaultCellStyle.Format = "N0";
            gridMain.Columns["ACTIVE"].HeaderText = "Kinh Doanh";
            gridMain.Columns["ANH"].Visible = false; // Ẩn cột đường dẫn ảnh
            gridMain.AutoResizeColumns();
        }

        private void DataBindingControl()
        {
            // Xóa binding cũ
            foreach (Control c in grpChiTiet.Controls)
            {
                c.DataBindings.Clear();
            }

            // Binding mới
            txtMaHH.DataBindings.Add("Text", _bs, "MAHH", true, DataSourceUpdateMode.Never);
            txtTenHH.DataBindings.Add("Text", _bs, "TENHH", true, DataSourceUpdateMode.Never);
            cboNhomHang.DataBindings.Add("SelectedValue", _bs, "MANHOM", true, DataSourceUpdateMode.Never);
            txtDVT.DataBindings.Add("Text", _bs, "DVT", true, DataSourceUpdateMode.Never);
            txtGiaBan.DataBindings.Add("Text", _bs, "GIABAN", true, DataSourceUpdateMode.Never, "N0");
            txtGiaVon.DataBindings.Add("Text", _bs, "GIAVON", true, DataSourceUpdateMode.Never, "N0");
            chkActive.DataBindings.Add("Checked", _bs, "ACTIVE", true, DataSourceUpdateMode.Never);
        }

        private void SetFormMode(FormMode mode)
        {
            _currentMode = mode;
            bool isEditing = (mode != FormMode.View);

            txtTenHH.ReadOnly = !isEditing;
            cboNhomHang.Enabled = isEditing;
            txtDVT.ReadOnly = !isEditing;
            txtGiaBan.ReadOnly = !isEditing;
            txtGiaVon.ReadOnly = !isEditing;
            chkActive.Enabled = isEditing;
            btnChonAnh.Enabled = isEditing;

            txtMaHH.ReadOnly = (mode != FormMode.New);

            btnMoi.Enabled = !isEditing;
            btnSua.Enabled = !isEditing && _bs.Current != null;
            btnLuu.Enabled = isEditing;
            btnXoa.Enabled = !isEditing && _bs.Current != null;
            gridMain.Enabled = !isEditing;

            if (mode == FormMode.New)
            {
                ClearDataBindings();
                ClearInputControls();
                txtMaHH.Focus();
            }
            else
            {
                EnsureDataBindings();
            }
        }

        // --- CÁC HÀM HỖ TRỢ CHO VIỆC BINDING/UNBINDING ---
        private void ClearDataBindings()
        {
            foreach (Control c in grpChiTiet.Controls) c.DataBindings.Clear();
            picAnh.DataBindings.Clear();
        }

        private void EnsureDataBindings()
        {
            if (txtMaHH.DataBindings.Count == 0) DataBindingControl();
        }

        private void ClearInputControls()
        {
            txtMaHH.Text = "";
            txtTenHH.Text = "";
            txtDVT.Text = "";
            txtGiaBan.Text = "0";
            txtGiaVon.Text = "0";
            cboNhomHang.SelectedIndex = -1;
            chkActive.Checked = true;
            picAnh.Image = null;
            _currentImagePath = null;
        }

        private void gridMain_SelectionChanged(object sender, EventArgs e)
        {
            if (_bs.Current == null) return;
            if (_currentMode != FormMode.View) SetFormMode(FormMode.View);

            // Hiển thị ảnh khi chọn dòng
            DataRowView drv = (DataRowView)_bs.Current;
            string relativePath = drv["ANH"]?.ToString();

            if (!string.IsNullOrEmpty(relativePath))
            {
                string fullPath = Path.Combine(Application.StartupPath, relativePath);
                try
                {
                    // Tải ảnh một cách an toàn để không khóa file
                    using (var bmpTemp = new Bitmap(fullPath))
                    {
                        picAnh.Image = new Bitmap(bmpTemp);
                    }
                }
                catch (Exception)
                {
                    picAnh.Image = null; // Hiển thị ảnh trống nếu file không tồn tại hoặc lỗi
                }
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
                _currentImagePath = null; // Reset đường dẫn ảnh tạm khi bắt đầu sửa
                SetFormMode(FormMode.Edit);
            }
        }
        private void btnNap_Click(object sender, EventArgs e) => LoadData();

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaHH.Text) || string.IsNullOrWhiteSpace(txtTenHH.Text))
            {
                MessageBox.Show("Mã và Tên hàng hóa không được để trống.", "Cảnh báo");
                return;
            }

            try
            {
                string finalImagePath = null;

                // Xử lý lưu ảnh
                if (!string.IsNullOrEmpty(_currentImagePath))
                {
                    string imageDir = Path.Combine(Application.StartupPath, "Images");
                    if (!Directory.Exists(imageDir))
                    {
                        Directory.CreateDirectory(imageDir);
                    }

                    string fileName = Path.GetFileName(_currentImagePath);
                    string destPath = Path.Combine(imageDir, fileName);

                    // Sao chép file ảnh vào thư mục Images (ghi đè nếu đã có)
                    File.Copy(_currentImagePath, destPath, true);

                    // Lưu đường dẫn tương đối
                    finalImagePath = Path.Combine("Images", fileName);
                }
                else if (_currentMode == FormMode.Edit)
                {
                    // Nếu không chọn ảnh mới khi sửa, giữ lại ảnh cũ
                    finalImagePath = ((DataRowView)_bs.Current)["ANH"]?.ToString();
                }

                var p = new[] {
                    DbHelper.Param("@ma", txtMaHH.Text.Trim()),
                    DbHelper.Param("@ten", txtTenHH.Text.Trim()),
                    DbHelper.Param("@maNhom", cboNhomHang.SelectedValue ?? (object)DBNull.Value),
                    DbHelper.Param("@dvt", txtDVT.Text.Trim()),
                    DbHelper.Param("@giaBan", decimal.TryParse(txtGiaBan.Text, out var gb) ? gb : 0),
                    DbHelper.Param("@giaVon", decimal.TryParse(txtGiaVon.Text, out var gv) ? gv : 0),
                    DbHelper.Param("@active", chkActive.Checked),
                    DbHelper.Param("@anh", (object)finalImagePath ?? DBNull.Value)
                };

                if (_currentMode == FormMode.New)
                {
                    string sql = "INSERT INTO dbo.DM_HANGHOA (MAHH, TENHH, MANHOM, DVT, GIABAN, GIAVON, ACTIVE, ANH) VALUES (@ma, @ten, @maNhom, @dvt, @giaBan, @giaVon, @active, @anh)";
                    DbHelper.Execute(sql, p);
                }
                else // Edit
                {
                    string sql = "UPDATE dbo.DM_HANGHOA SET TENHH=@ten, MANHOM=@maNhom, DVT=@dvt, GIABAN=@giaBan, GIAVON=@giaVon, ACTIVE=@active, ANH=@anh WHERE MAHH=@ma";
                    DbHelper.Execute(sql, p);
                }

                LoadData();
                _bs.Position = _bs.Find("MAHH", txtMaHH.Text.Trim());
                SetFormMode(FormMode.View);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                    MessageBox.Show("Mã hàng hóa này đã tồn tại. Vui lòng chọn mã khác.", "Lỗi");
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
                    DbHelper.Execute("DELETE FROM dbo.DM_HANGHOA WHERE MAHH=@ma", DbHelper.Param("@ma", txtMaHH.Text));
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa hàng hóa này. Có thể do đã có phát sinh giao dịch liên quan.\n\nChi tiết: " + ex.Message, "Lỗi ràng buộc dữ liệu");
                }
            }
        }
    }
}