using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormDanhMucHangHoa : Form
    {
        private bool isAdding = false;
        private string currentImagePath = null;

        public FormDanhMucHangHoa()
        {
            InitializeComponent();
        }

        private void FormDanhMucHangHoa_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBoxNhomHang();
            // ==> ĐÂY LÀ DÒNG LỆNH "KHÓA" GIAO DIỆN KHI VỪA MỞ <==
            SetInputMode(false);
        }

        #region Các phương thức xử lý dữ liệu

        private void LoadData()
        {
            try
            {
                string query = "SELECT MAHH, TENHH, MANHOM, DVT, GIAVON, GIABAN, TONKHO, ANH, ACTIVE FROM DM_HANGHOA";
                DataTable dt = DbHelper.Query(query);
                dgvHangHoa.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxNhomHang()
        {
            try
            {
                string query = "SELECT MANHOM, TENNHOM FROM DM_NHOMHANG";
                DataTable dt = DbHelper.Query(query);
                cboNhomHang.DataSource = dt;
                cboNhomHang.DisplayMember = "TENNHOM";
                cboNhomHang.ValueMember = "MANHOM";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhóm hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaHH.Text = "";
            txtTenHH.Text = "";
            txtDVT.Text = "";
            txtGiaBan.Text = "0";
            txtGiaVon.Text = "0";
            cboNhomHang.SelectedIndex = -1;
            chkActive.Checked = true;
            picHinhAnh.Image = null;
            currentImagePath = null;
        }

        #endregion

        #region UX Mới: Quản lý trạng thái giao diện

        private void SetInputMode(bool enable)
        {
            txtMaHH.ReadOnly = !isAdding;
            txtTenHH.ReadOnly = !enable;
            txtDVT.ReadOnly = !enable;
            txtGiaBan.ReadOnly = !enable;
            cboNhomHang.Enabled = enable;
            chkActive.Enabled = enable;
            btnBrowse.Enabled = enable;

            txtGiaVon.ReadOnly = true;
            txtGiaVon.BackColor = Color.LightGray;

            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;
            btnThem.Enabled = !enable;
            btnSua.Enabled = !enable;
            btnXoa.Enabled = !enable;
            btnIn.Enabled = !enable;
        }

        #endregion

        #region Sự kiện của các nút

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearInputs();
            SetInputMode(true);
            txtMaHH.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvHangHoa.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAdding = false;
            SetInputMode(true);
            txtTenHH.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHangHoa.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mặt hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string maHH = dgvHangHoa.SelectedRows[0].Cells["MAHH"].Value.ToString();
                    string query = "DELETE FROM DM_HANGHOA WHERE MAHH = @MaHH";
                    DbHelper.Execute(query, DbHelper.Param("@MaHH", maHH));

                    LoadData();
                    MessageBox.Show("Xóa mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaHH.Text) || string.IsNullOrWhiteSpace(txtTenHH.Text))
                {
                    MessageBox.Show("Mã và Tên hàng hóa không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isAdding)
                {
                    string query = @"
                        INSERT INTO DM_HANGHOA (MAHH, TENHH, MANHOM, DVT, GIABAN, ANH, ACTIVE)
                        VALUES (@MaHH, @TenHH, @MaNhom, @DVT, @GiaBan, @Anh, @Active)";
                    DbHelper.Execute(query,
                        DbHelper.Param("@MaHH", txtMaHH.Text),
                        DbHelper.Param("@TenHH", txtTenHH.Text),
                        DbHelper.Param("@MaNhom", cboNhomHang.SelectedValue),
                        DbHelper.Param("@DVT", txtDVT.Text),
                        DbHelper.Param("@GiaBan", decimal.Parse(txtGiaBan.Text)),
                        DbHelper.Param("@Anh", currentImagePath),
                        DbHelper.Param("@Active", chkActive.Checked)
                    );
                }
                else
                {
                    string query = @"
                        UPDATE DM_HANGHOA SET
                            TENHH = @TenHH, MANHOM = @MaNhom, DVT = @DVT, GIABAN = @GiaBan, 
                            ANH = @Anh, ACTIVE = @Active
                        WHERE MAHH = @MaHH";
                    DbHelper.Execute(query,
                        DbHelper.Param("@TenHH", txtTenHH.Text),
                        DbHelper.Param("@MaNhom", cboNhomHang.SelectedValue),
                        DbHelper.Param("@DVT", txtDVT.Text),
                        DbHelper.Param("@GiaBan", decimal.Parse(txtGiaBan.Text)),
                        DbHelper.Param("@Anh", currentImagePath),
                        DbHelper.Param("@Active", chkActive.Checked),
                        DbHelper.Param("@MaHH", txtMaHH.Text)
                    );
                }
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                SetInputMode(false);
                isAdding = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (!isAdding)
            {
                dgvHangHoa_SelectionChanged(null, null);
            }
            else
            {
                ClearInputs();
            }
            SetInputMode(false);
            isAdding = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                currentImagePath = openFile.FileName;
                picHinhAnh.Image = new Bitmap(currentImagePath);
            }
        }

        private void dgvHangHoa_SelectionChanged(object sender, EventArgs e)
        {
            if (!isAdding && dgvHangHoa.SelectedRows.Count > 0)
            {
                var row = dgvHangHoa.SelectedRows[0];
                txtMaHH.Text = row.Cells["MAHH"].Value?.ToString();
                txtTenHH.Text = row.Cells["TENHH"].Value?.ToString();
                txtDVT.Text = row.Cells["DVT"].Value?.ToString();
                txtGiaVon.Text = row.Cells["GIAVON"].Value?.ToString();
                txtGiaBan.Text = row.Cells["GIABAN"].Value?.ToString();
                cboNhomHang.SelectedValue = row.Cells["MANHOM"].Value;

                chkActive.Checked = row.Cells["ACTIVE"].Value != null && (bool)row.Cells["ACTIVE"].Value;

                currentImagePath = row.Cells["ANH"].Value?.ToString();
                if (!string.IsNullOrEmpty(currentImagePath) && File.Exists(currentImagePath))
                {
                    picHinhAnh.Image = Image.FromFile(currentImagePath);
                }
                else
                {
                    picHinhAnh.Image = null;
                }
            }
        }

        #endregion
    }
}