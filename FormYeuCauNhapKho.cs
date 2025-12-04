using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;
using DoAnLapTrinhQuanLy.Data;
using DoAnLapTrinhQuanLy.Core;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormYeuCauNhapKho : BaseForm
    {
        private string _mode = "";
        private DataTable dtChiTiet;

        public FormYeuCauNhapKho()
        {
            InitializeComponent();
            UseCustomTitleBar = false;
        }

        private void FormYeuCauNhapKho_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadDanhSachPhieu();
            SetInputMode(false);

            StyleDataGridView(dgvDanhSach);
            StyleDataGridView(dgvChiTiet);

            // Wire events if not already wired in Designer
            // We'll rely on Designer or these manual wirings if they were intended
            // But to be safe, I'll add them here if they aren't duplicates.
            // Actually, usually these are in Designer.cs. If I add them here, I might duplicate.
            // But the previous code had them. I'll assume they are needed.
            btnThem.Click -= btnThem_Click; btnThem.Click += btnThem_Click;
            btnSua.Click -= btnSua_Click; btnSua.Click += btnSua_Click;
            btnXoa.Click -= btnXoa_Click; btnXoa.Click += btnXoa_Click;
            btnLuu.Click -= btnLuu_Click; btnLuu.Click += btnLuu_Click;
            btnHuy.Click -= btnHuy_Click; btnHuy.Click += btnHuy_Click;
            // dgvDanhSach.SelectionChanged -= dgvDanhSach_SelectionChanged; dgvDanhSach.SelectionChanged += dgvDanhSach_SelectionChanged;

            btnDuyet.Visible = true;
            btnTuChoi.Visible = true;
            btnDuyet.Click -= btnDuyet_Click; btnDuyet.Click += btnDuyet_Click;
            btnTuChoi.Click -= btnTuChoi_Click; btnTuChoi.Click += btnTuChoi_Click;
        }

        #region Core Logic (SetInputMode và Load)

        private void SetInputMode(bool mode)
        {
            bool canApprove = (Session.LoggedInUser.TenQuyen == "Administrator" || Session.LoggedInUser.TenQuyen == "Kế toán");

            btnThem.Enabled = !mode;
            btnSua.Enabled = !mode;
            btnXoa.Enabled = !mode;
            btnDuyet.Enabled = !mode && canApprove;
            btnTuChoi.Enabled = !mode && canApprove;

            btnLuu.Enabled = mode;
            btnHuy.Enabled = mode;

            dtpNgayYeuCau.Enabled = mode;
            cboNhanVienYeuCau.Enabled = mode;
            txtLyDo.ReadOnly = !mode;

            cboTrangThai.Enabled = false;
            dtpNgayDuyet.Enabled = false;
            cboNhanVienDuyet.Enabled = false;
            txtGhiChuDuyet.ReadOnly = !canApprove || mode;

            dgvChiTiet.ReadOnly = !mode;
            dgvChiTiet.AllowUserToAddRows = mode;

            dgvDanhSach.Enabled = !mode;
        }

        private void LoadComboBoxes()
        {
            LoadNhanVienToCombo(cboNhanVienYeuCau, "Tải NV Yêu cầu");
            LoadNhanVienToCombo(cboNhanVienDuyet, "Tải NV Duyệt");

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("0 - Mới tạo");
            cboTrangThai.Items.Add("1 - Đã duyệt");
            cboTrangThai.Items.Add("2 - Đã từ chối");
            cboTrangThai.Items.Add("3 - Đã nhập kho"); // Thêm trạng thái mới
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadNhanVienToCombo(ComboBox cbo, string placeholder)
        {
            try
            {
                DataTable dt = DbHelper.Query("SELECT MANV, HOTEN FROM NHANVIEN WHERE HOATDONG = 1 ORDER BY HOTEN");
                DataRow dr = dt.NewRow();
                dr["MANV"] = "";
                dr["HOTEN"] = $"--- {placeholder} ---";
                dt.Rows.InsertAt(dr, 0);

                cbo.DataSource = dt;
                cbo.DisplayMember = "HOTEN";
                cbo.ValueMember = "MANV";
                cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhân viên: " + ex.Message);
            }
        }

        private void LoadDanhSachPhieu()
        {
            try
            {
                string query = @"
                    SELECT ID, NGAY_YEUCAU, MANV_YEUCAU, TRANGTHAI, LYDO 
                    FROM PHIEU_YEUCAU_NHAPKHO 
                    ORDER BY NGAY_YEUCAU DESC, ID DESC";
                dgvDanhSach.DataSource = DbHelper.Query(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phiếu: " + ex.Message);
            }
        }

        private void ClearInputs()
        {
            txtID.Text = "(auto)";
            dtpNgayYeuCau.Value = DateTime.Now;

            if (Session.LoggedInUser != null && !string.IsNullOrEmpty(Session.LoggedInUser.MaNV))
            {
                cboNhanVienYeuCau.SelectedValue = Session.LoggedInUser.MaNV;
            }
            else
            {
                cboNhanVienYeuCau.SelectedIndex = 0;
            }

            txtLyDo.Text = "";
            cboTrangThai.SelectedIndex = 0; // Mới tạo
            dtpNgayDuyet.Value = DateTime.Now;
            cboNhanVienDuyet.SelectedValue = "";
            txtGhiChuDuyet.Text = "";

            // Tạo bảng chi tiết rỗng để bắt đầu nhập
            dtChiTiet = new DataTable();
            dtChiTiet.Columns.Add("MAHH", typeof(string));
            dtChiTiet.Columns.Add("TENHH", typeof(string));
            dtChiTiet.Columns.Add("DVT", typeof(string));
            dtChiTiet.Columns.Add("SOLUONG_YEUCAU", typeof(int));
            dtChiTiet.Columns.Add("GHICHU", typeof(string));
            dgvChiTiet.DataSource = dtChiTiet;
        }

        private void BindData(long selectedId)
        {
            try
            {
                // 1. Tải Master
                string queryMaster = "SELECT * FROM PHIEU_YEUCAU_NHAPKHO WHERE ID = @ID";
                DataTable dtMaster = DbHelper.Query(queryMaster, DbHelper.Param("@ID", selectedId));

                if (dtMaster.Rows.Count > 0)
                {
                    DataRow r = dtMaster.Rows[0];
                    txtID.Text = r["ID"].ToString();
                    dtpNgayYeuCau.Value = (DateTime)r["NGAY_YEUCAU"];
                    cboNhanVienYeuCau.SelectedValue = r["MANV_YEUCAU"].ToString();
                    txtLyDo.Text = r["LYDO"].ToString();
                    cboTrangThai.SelectedIndex = Convert.ToInt32(r["TRANGTHAI"]);

                    dtpNgayDuyet.Value = r["NGAY_DUYET"] == DBNull.Value ? DateTime.Now : (DateTime)r["NGAY_DUYET"];
                    cboNhanVienDuyet.SelectedValue = r["MANV_DUYET"] == DBNull.Value ? "" : r["MANV_DUYET"].ToString();
                    txtGhiChuDuyet.Text = r["GHICHU_DUYET"].ToString();
                }

                // 2. Tải Detail
                string queryDetail = @"
                    SELECT ct.MAHH, hh.TENHH, hh.DVT, ct.SOLUONG_YEUCAU, ct.GHICHU 
                    FROM PHIEU_YEUCAU_NHAPKHO_CT ct
                    JOIN DM_HANGHOA hh ON ct.MAHH = hh.MAHH
                    WHERE ct.ID_YEUCAU = @ID";
                dtChiTiet = DbHelper.Query(queryDetail, DbHelper.Param("@ID", selectedId));
                dgvChiTiet.DataSource = dtChiTiet;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết phiếu: " + ex.Message);
            }
        }

        #endregion

        #region Xử lý Sự kiện Nút bấm

        private void btnThem_Click(object sender, EventArgs e)
        {
            _mode = "add";
            ClearInputs();
            SetInputMode(true);
            dtpNgayYeuCau.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.CurrentRow == null) return;

            int trangThai = Convert.ToInt32(dgvDanhSach.CurrentRow.Cells["TRANGTHAI"].Value);
            if (trangThai != 0)
            {
                MessageBox.Show("Phiếu đã được xử lý, không thể sửa.");
                return;
            }

            _mode = "edit";
            SetInputMode(true);
            dtpNgayYeuCau.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.CurrentRow == null) return;

            int trangThai = Convert.ToInt32(dgvDanhSach.CurrentRow.Cells["TRANGTHAI"].Value);
            if (trangThai != 0)
            {
                MessageBox.Show("Phiếu đã được xử lý, không thể xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa phiếu này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    long id = Convert.ToInt64(dgvDanhSach.CurrentRow.Cells["ID"].Value);
                    DbHelper.Execute("DELETE FROM PHIEU_YEUCAU_NHAPKHO WHERE ID = @ID", DbHelper.Param("@ID", id));
                    LoadDanhSachPhieu();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa phiếu: " + ex.Message);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboNhanVienYeuCau.SelectedValue == null || string.IsNullOrEmpty(cboNhanVienYeuCau.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn người yêu cầu.");
                return;
            }

            dgvChiTiet.EndEdit();
            var validRows = (dgvChiTiet.DataSource as DataTable).AsEnumerable()
                .Where(r => r.RowState != DataRowState.Deleted &&
                            r["MAHH"] != DBNull.Value &&
                            !string.IsNullOrEmpty(r["MAHH"].ToString()))
                .ToList();

            if (validRows.Count == 0)
            {
                MessageBox.Show("Phiếu yêu cầu phải có ít nhất 1 dòng hàng hóa.");
                return;
            }

            try
            {
                using (var scope = new TransactionScope())
                {
                    long phieuId;
                    if (_mode == "add")
                    {
                        string queryMaster = @"
                            INSERT INTO PHIEU_YEUCAU_NHAPKHO (NGAY_YEUCAU, MANV_YEUCAU, LYDO, TRANGTHAI)
                            VALUES (@NGAY_YEUCAU, @MANV_YEUCAU, @LYDO, 0);
                            SELECT SCOPE_IDENTITY();";

                        phieuId = Convert.ToInt64(DbHelper.ExecuteScalar(queryMaster,
                            DbHelper.Param("@NGAY_YEUCAU", dtpNgayYeuCau.Value.Date),
                            DbHelper.Param("@MANV_YEUCAU", cboNhanVienYeuCau.SelectedValue.ToString()),
                            DbHelper.Param("@LYDO", txtLyDo.Text)
                        ));
                    }
                    else
                    {
                        phieuId = Convert.ToInt64(txtID.Text);
                        string queryMaster = @"
                            UPDATE PHIEU_YEUCAU_NHAPKHO 
                            SET NGAY_YEUCAU = @NGAY_YEUCAU, 
                                MANV_YEUCAU = @MANV_YEUCAU, 
                                LYDO = @LYDO
                            WHERE ID = @ID";
                        DbHelper.Execute(queryMaster,
                            DbHelper.Param("@NGAY_YEUCAU", dtpNgayYeuCau.Value.Date),
                            DbHelper.Param("@MANV_YEUCAU", cboNhanVienYeuCau.SelectedValue.ToString()),
                            DbHelper.Param("@LYDO", txtLyDo.Text),
                            DbHelper.Param("@ID", phieuId)
                        );

                        DbHelper.Execute("DELETE FROM PHIEU_YEUCAU_NHAPKHO_CT WHERE ID_YEUCAU = @ID", DbHelper.Param("@ID", phieuId));
                    }

                    // Lưu Detail (KHÔNG ĐỤNG KHO)
                    string queryDetail = @"
                        INSERT INTO PHIEU_YEUCAU_NHAPKHO_CT (ID_YEUCAU, MAHH, SOLUONG_YEUCAU, GHICHU)
                        VALUES (@ID_YEUCAU, @MAHH, @SOLUONG, @GHICHU)";

                    foreach (DataRow row in validRows)
                    {
                        DbHelper.Execute(queryDetail,
                            DbHelper.Param("@ID_YEUCAU", phieuId),
                            DbHelper.Param("@MAHH", row["MAHH"].ToString()),
                            DbHelper.Param("@SOLUONG", Convert.ToInt32(row["SOLUONG_YEUCAU"])),
                            DbHelper.Param("@GHICHU", row["GHICHU"].ToString())
                        );
                    }

                    scope.Complete();
                }

                MessageBox.Show("Đã lưu Phiếu Yêu Cầu thành công!");
                LoadDanhSachPhieu();
                SetInputMode(false);
                _mode = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            _mode = "";
            SetInputMode(false);
            if (dgvDanhSach.CurrentRow != null)
                dgvDanhSach_SelectionChanged(null, null);
            else
                ClearInputs();
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            ProcessApproval(1); // 1 = Đã duyệt
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            ProcessApproval(2); // 2 = Đã từ chối
        }

        private void ProcessApproval(int newStatus)
        {
            if (dgvDanhSach.CurrentRow == null) return;

            int trangThai = Convert.ToInt32(dgvDanhSach.CurrentRow.Cells["TRANGTHAI"].Value);
            if (trangThai != 0)
            {
                MessageBox.Show("Phiếu này đã được xử lý rồi.");
                return;
            }

            long id = Convert.ToInt64(txtID.Text);
            string manv_duyet = Session.LoggedInUser.MaNV;
            string ghiChu = txtGhiChuDuyet.Text;
            string statusText = (newStatus == 1) ? "Duyệt" : "Từ chối";

            if (MessageBox.Show($"Bạn có chắc muốn {statusText} phiếu {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                string query = @"
                    UPDATE PHIEU_YEUCAU_NHAPKHO
                    SET TRANGTHAI = @TrangThai,
                        MANV_DUYET = @MaNVDuyet,
                        NGAY_DUYET = @NgayDuyet,
                        GHICHU_DUYET = @GhiChu
                    WHERE ID = @ID";

                DbHelper.Execute(query,
                    DbHelper.Param("@TrangThai", newStatus),
                    DbHelper.Param("@MaNVDuyet", manv_duyet),
                    DbHelper.Param("@NgayDuyet", DateTime.Now.Date),
                    DbHelper.Param("@GhiChu", ghiChu),
                    DbHelper.Param("@ID", id)
                );

                MessageBox.Show($"Đã {statusText} phiếu thành công!");
                LoadDanhSachPhieu();

                foreach (DataGridViewRow row in dgvDanhSach.Rows)
                {
                    if (Convert.ToInt64(row.Cells["ID"].Value) == id)
                    {
                        row.Selected = true;
                        dgvDanhSach.CurrentCell = row.Cells[0];
                        break;
                    }
                }
                BindData(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi {statusText} phiếu: " + ex.Message);
            }
        }

        #endregion

        #region Xử lý Sự kiện Grid

        private void dgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDanhSach.CurrentRow != null && string.IsNullOrEmpty(_mode))
            {
                long id = Convert.ToInt64(dgvDanhSach.CurrentRow.Cells["ID"].Value);
                BindData(id);

                int trangThai = Convert.ToInt32(dgvDanhSach.CurrentRow.Cells["TRANGTHAI"].Value);
                bool canApprove = (Session.LoggedInUser.TenQuyen == "Administrator" || Session.LoggedInUser.TenQuyen == "Kế toán");
                bool isNew = (trangThai == 0);

                btnSua.Enabled = isNew;
                btnXoa.Enabled = isNew;
                btnDuyet.Enabled = isNew && canApprove;
                btnTuChoi.Enabled = isNew && canApprove;
                txtGhiChuDuyet.ReadOnly = !canApprove || !isNew;
            }
            else if (string.IsNullOrEmpty(_mode))
            {
                ClearInputs();
                btnDuyet.Enabled = false;
                btnTuChoi.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        #endregion

        #region UI Helpers

        private void StyleDataGridView(DataGridView dgv)
        {
            // === Style chung ===
            dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(22)))), ((int)(((byte)(38)))));
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;

            // === Style Header (Tiêu đề cột) ===
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(34)))), ((int)(((byte)(56)))));
            headerStyle.ForeColor = Color.Gainsboro;
            headerStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle = headerStyle;

            // === Style Cell (Ô dữ liệu) ===
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(22)))), ((int)(((byte)(38)))));
            cellStyle.ForeColor = Color.Gainsboro; // <-- Màu chữ sáng đây nè bà
            cellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(170)))), ((int)(((byte)(242)))));
            cellStyle.SelectionForeColor = Color.Black; // Màu chữ khi bôi đen
            cellStyle.WrapMode = DataGridViewTriState.False;
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.DefaultCellStyle = cellStyle;
        }

        #endregion
    }
}