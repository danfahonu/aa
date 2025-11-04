using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormQuanLyTaiKhoanNganHang : Form
    {
        private bool isAdding = false;
        private int selectedId = -1; // Dùng ID để xác định dòng đang chọn

        public FormQuanLyTaiKhoanNganHang()
        {
            InitializeComponent();
        }

        private void FormQuanLyTaiKhoanNganHang_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBoxNganHang();
            // Ban đầu chưa chọn loại TK nên chưa load đối tượng
            cboLoaiTK.SelectedIndex = -1;
            SetInputMode(false);
        }

        #region Xử lý dữ liệu và ComboBox

        private void LoadData()
        {
            try
            {
                // Câu query phức tạp hơn để JOIN và hiển thị TÊN thay vì MÃ
                string query = @"
                    SELECT 
                        tk.ID, tk.SO_TK, nh.TENNH, tk.CHU_TK,
                        CASE 
                            WHEN tk.LOAI_TK = 'CTY' THEN N'Công ty'
                            WHEN tk.LOAI_TK = 'NCC' THEN ncc.TEN_NCC
                            ELSE '' 
                        END as TEN_DOITUONG,
                        tk.GHICHU
                    FROM DM_TAIKHOAN_NGANHANG tk
                    LEFT JOIN DM_NGANHANG nh ON tk.NGANHANG = nh.MANH
                    LEFT JOIN DM_NHACUNGCAP ncc ON tk.MA_DOITUONG = ncc.MA_NCC AND tk.LOAI_TK = 'NCC'";
                DataTable dt = DbHelper.Query(query);
                dgvTaiKhoan.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxNganHang()
        {
            try
            {
                string query = "SELECT MANH, TENNH FROM DM_NGANHANG";
                DataTable dt = DbHelper.Query(query);
                cboNganHang.DataSource = dt;
                cboNganHang.DisplayMember = "TENNH";
                cboNganHang.ValueMember = "MANH";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách ngân hàng: " + ex.Message);
            }
        }

        private void LoadComboBoxDoiTuong()
        {
            try
            {
                DataTable dt = new DataTable();
                if (cboLoaiTK.SelectedItem != null && cboLoaiTK.SelectedItem.ToString() == "Nhà cung cấp")
                {
                    dt = DbHelper.Query("SELECT MA_NCC, TEN_NCC FROM DM_NHACUNGCAP");
                    cboDoiTuong.DataSource = dt;
                    cboDoiTuong.DisplayMember = "TEN_NCC";
                    cboDoiTuong.ValueMember = "MA_NCC";
                }
                else if (cboLoaiTK.SelectedItem != null && cboLoaiTK.SelectedItem.ToString() == "Công ty")
                {
                    // Nếu là công ty, có thể tạo 1 đối tượng mặc định hoặc để trống
                    dt.Columns.Add("MA_DOITUONG");
                    dt.Columns.Add("TEN_DOITUONG");
                    dt.Rows.Add("CTY", "Công ty");
                    cboDoiTuong.DataSource = dt;
                    cboDoiTuong.DisplayMember = "TEN_DOITUONG";
                    cboDoiTuong.ValueMember = "MA_DOITUONG";
                }
                else
                {
                    cboDoiTuong.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách đối tượng: " + ex.Message);
            }
        }


        private void ClearInputs()
        {
            txtSoTK.Text = "";
            txtChuTK.Text = "";
            cboNganHang.SelectedIndex = -1;
            cboLoaiTK.SelectedIndex = -1;
            cboDoiTuong.DataSource = null;
            txtGhiChu.Text = "";
            selectedId = -1;
        }

        #endregion

        #region Quản lý trạng thái giao diện (UX)

        private void SetInputMode(bool enable)
        {
            txtSoTK.ReadOnly = !enable;
            txtChuTK.ReadOnly = !enable;
            cboNganHang.Enabled = enable;
            cboLoaiTK.Enabled = enable;
            cboDoiTuong.Enabled = enable;
            txtGhiChu.ReadOnly = !enable;

            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;
            btnThem.Enabled = !enable;
            btnSua.Enabled = !enable;
            btnXoa.Enabled = !enable;
        }

        #endregion

        #region Sự kiện

        private void cboLoaiTK_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tự động load danh sách đối tượng tương ứng khi thay đổi Loại TK
            if (cboLoaiTK.SelectedItem != null)
            {
                LoadComboBoxDoiTuong();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearInputs();
            SetInputMode(true);
            txtSoTK.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAdding = false;
            SetInputMode(true);
            txtChuTK.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM DM_TAIKHOAN_NGANHANG WHERE ID = @ID";
                    DbHelper.Execute(query, DbHelper.Param("@ID", selectedId));

                    LoadData();
                    ClearInputs();
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (string.IsNullOrWhiteSpace(txtSoTK.Text) || cboNganHang.SelectedValue == null || cboLoaiTK.SelectedItem == null || cboDoiTuong.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các thông tin bắt buộc.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string loaiTK = (cboLoaiTK.SelectedItem.ToString() == "Công ty") ? "CTY" : "NCC";

                if (isAdding)
                {
                    string query = @"
                        INSERT INTO DM_TAIKHOAN_NGANHANG (SO_TK, NGANHANG, CHU_TK, LOAI_TK, MA_DOITUONG, GHICHU)
                        VALUES (@SoTK, @NganHang, @ChuTK, @LoaiTK, @MaDoiTuong, @GhiChu)";
                    DbHelper.Execute(query,
                        DbHelper.Param("@SoTK", txtSoTK.Text),
                        DbHelper.Param("@NganHang", cboNganHang.SelectedValue),
                        DbHelper.Param("@ChuTK", txtChuTK.Text),
                        DbHelper.Param("@LoaiTK", loaiTK),
                        DbHelper.Param("@MaDoiTuong", cboDoiTuong.SelectedValue),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text)
                    );
                }
                else
                {
                    string query = @"
                        UPDATE DM_TAIKHOAN_NGANHANG SET
                            SO_TK = @SoTK, NGANHANG = @NganHang, CHU_TK = @ChuTK, 
                            LOAI_TK = @LoaiTK, MA_DOITUONG = @MaDoiTuong, GHICHU = @GhiChu
                        WHERE ID = @ID";
                    DbHelper.Execute(query,
                        DbHelper.Param("@SoTK", txtSoTK.Text),
                        DbHelper.Param("@NganHang", cboNganHang.SelectedValue),
                        DbHelper.Param("@ChuTK", txtChuTK.Text),
                        DbHelper.Param("@LoaiTK", loaiTK),
                        DbHelper.Param("@MaDoiTuong", cboDoiTuong.SelectedValue),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text),
                        DbHelper.Param("@ID", selectedId)
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
                dgvTaiKhoan_SelectionChanged(null, null);
            }
            else
            {
                ClearInputs();
            }
            SetInputMode(false);
            isAdding = false;
        }

        private void dgvTaiKhoan_SelectionChanged(object sender, EventArgs e)
        {
            if (!isAdding && dgvTaiKhoan.SelectedRows.Count > 0)
            {
                try
                {
                    selectedId = Convert.ToInt32(dgvTaiKhoan.SelectedRows[0].Cells["ID"].Value);

                    // Lấy lại dữ liệu chi tiết từ DB để đảm bảo tính chính xác
                    var dt = DbHelper.Query("SELECT * FROM DM_TAIKHOAN_NGANHANG WHERE ID = @ID", DbHelper.Param("@ID", selectedId));
                    if (dt.Rows.Count > 0)
                    {
                        var row = dt.Rows[0];
                        txtSoTK.Text = row["SO_TK"].ToString();
                        txtChuTK.Text = row["CHU_TK"].ToString();
                        cboNganHang.SelectedValue = row["NGANHANG"];
                        txtGhiChu.Text = row["GHICHU"].ToString();

                        string loaiTK = row["LOAI_TK"].ToString();
                        if (loaiTK == "CTY")
                        {
                            cboLoaiTK.SelectedItem = "Công ty";
                        }
                        else if (loaiTK == "NCC")
                        {
                            cboLoaiTK.SelectedItem = "Nhà cung cấp";
                        }
                        // Sau khi set Loại TK, cboDoiTuong sẽ tự load lại, ta set value cho nó
                        cboDoiTuong.SelectedValue = row["MA_DOITUONG"];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message);
                }
            }
        }

        #endregion
    }
}