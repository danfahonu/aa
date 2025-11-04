using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormPhieuNhap : Form
    {
        private bool isAdding = false;
        private long currentSoPhieu = -1; // -1 nghĩa là phiếu mới

        public FormPhieuNhap()
        {
            InitializeComponent();
        }

        private void FormPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadComboBoxNhaCungCap();
            SetupDataGridViewComboBox();
            SetInputMode(false);
            ClearInputs();
        }

        #region Xử lý dữ liệu và ComboBox

        private void LoadComboBoxNhaCungCap()
        {
            try
            {
                string query = "SELECT MA_NCC, TEN_NCC FROM DM_NHACUNGCAP";
                DataTable dt = DbHelper.Query(query);
                cboNhaCungCap.DataSource = dt;
                cboNhaCungCap.DisplayMember = "TEN_NCC";
                cboNhaCungCap.ValueMember = "MA_NCC";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhà cung cấp: " + ex.Message);
            }
        }

        private void SetupDataGridViewComboBox()
        {
            try
            {
                string query = "SELECT MAHH, TENHH FROM DM_HANGHOA WHERE ACTIVE = 1";
                DataTable dt = DbHelper.Query(query);

                DataGridViewComboBoxColumn cmbCol = dgvPhieuNhap.Columns["MAHH"] as DataGridViewComboBoxColumn;
                if (cmbCol != null)
                {
                    cmbCol.DataSource = dt;
                    cmbCol.DisplayMember = "TENHH";
                    cmbCol.ValueMember = "MAHH";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách hàng hóa: " + ex.Message);
            }
        }

        private void ClearInputs()
        {
            txtSoPhieu.Text = "(Phiếu mới)";
            dtpNgayLap.Value = DateTime.Now;
            cboNhaCungCap.SelectedIndex = -1;
            txtGhiChu.Text = "";
            dgvPhieuNhap.Rows.Clear();
            lblTongTien.Text = "0";
            currentSoPhieu = -1;
        }

        #endregion

        #region Quản lý trạng thái giao diện (UX)

        private void SetInputMode(bool enable)
        {
            dtpNgayLap.Enabled = enable;
            cboNhaCungCap.Enabled = enable;
            txtGhiChu.ReadOnly = !enable;
            dgvPhieuNhap.ReadOnly = !enable;

            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;
            btnThem.Enabled = !enable;

            // Nút Ghi sổ chỉ bật khi đã lưu phiếu nháp và chưa ghi sổ
            btnGhiSo.Enabled = !enable && currentSoPhieu != -1;
        }

        #endregion

        #region Sự kiện

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearInputs();
            SetInputMode(true);
            cboNhaCungCap.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboNhaCungCap.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isAdding)
                {
                    string queryPhieu = @"
                        INSERT INTO PHIEU (NGAYLAP, LOAI, MA_NCC, GHICHU, TRANGTHAI)
                        OUTPUT INSERTED.SOPHIEU
                        VALUES (@NgayLap, 'N', @MaNCC, @GhiChu, 0)";

                    object generatedId = DbHelper.Scalar(queryPhieu,
                        DbHelper.Param("@NgayLap", dtpNgayLap.Value),
                        DbHelper.Param("@MaNCC", cboNhaCungCap.SelectedValue),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text)
                    );
                    currentSoPhieu = Convert.ToInt64(generatedId);
                }
                else
                {
                    string queryPhieu = @"
                        UPDATE PHIEU SET NGAYLAP = @NgayLap, MA_NCC = @MaNCC, GHICHU = @GhiChu
                        WHERE SOPHIEU = @SoPhieu";
                    DbHelper.Execute(queryPhieu,
                         DbHelper.Param("@NgayLap", dtpNgayLap.Value),
                         DbHelper.Param("@MaNCC", cboNhaCungCap.SelectedValue),
                         DbHelper.Param("@GhiChu", txtGhiChu.Text),
                         DbHelper.Param("@SoPhieu", currentSoPhieu)
                    );
                    DbHelper.Execute("DELETE FROM PHIEU_CT WHERE SOPHIEU = @SoPhieu", DbHelper.Param("@SoPhieu", currentSoPhieu));
                }

                foreach (DataGridViewRow row in dgvPhieuNhap.Rows)
                {
                    if (row.IsNewRow) continue;

                    string maHH = row.Cells["MAHH"].Value?.ToString();
                    if (string.IsNullOrEmpty(maHH)) continue;

                    int soLuong = Convert.ToInt32(row.Cells["SL"].Value);
                    decimal donGia = Convert.ToDecimal(row.Cells["DONGIA"].Value);

                    string queryCT = @"
                        INSERT INTO PHIEU_CT (SOPHIEU, MAHH, SL, DONGIA)
                        VALUES (@SoPhieu, @MaHH, @SL, @DonGia)";
                    DbHelper.Execute(queryCT,
                        DbHelper.Param("@SoPhieu", currentSoPhieu),
                        DbHelper.Param("@MaHH", maHH),
                        DbHelper.Param("@SL", soLuong),
                        DbHelper.Param("@DonGia", donGia)
                    );
                }

                MessageBox.Show("Lưu phiếu nháp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoPhieu.Text = currentSoPhieu.ToString();
                isAdding = false;
                SetInputMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGhiSo_Click(object sender, EventArgs e)
        {
            if (currentSoPhieu == -1)
            {
                MessageBox.Show("Vui lòng lưu phiếu trước khi ghi sổ.", "Thông báo");
                return;
            }

            if (MessageBox.Show("Sau khi ghi sổ sẽ không thể sửa đổi. Bạn có chắc chắn muốn tiếp tục?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                DbHelper.ExecuteTran((conn, tran) =>
                {
                    int affectedRows = 0;
                    affectedRows += DbHelper.ExecuteInTran(conn, tran,
                        "UPDATE PHIEU SET TRANGTHAI = 1 WHERE SOPHIEU = @SoPhieu",
                        DbHelper.Param("@SoPhieu", currentSoPhieu));

                    DataTable dtChiTiet = DbHelper.QueryInTran(conn, tran,
                        "SELECT ID, MAHH, SL, DONGIA FROM PHIEU_CT WHERE SOPHIEU = @SoPhieu",
                        DbHelper.Param("@SoPhieu", currentSoPhieu));

                    foreach (DataRow row in dtChiTiet.Rows)
                    {
                        long idPhieuCT = Convert.ToInt64(row["ID"]);
                        string maHH = row["MAHH"].ToString();
                        int soLuong = Convert.ToInt32(row["SL"]);
                        decimal donGiaNhap = Convert.ToDecimal(row["DONGIA"]);

                        affectedRows += DbHelper.ExecuteInTran(conn, tran,
                            @"INSERT INTO KHO_CHITIET_TONKHO (ID_PHIEUNHAP_CT, MAHH, NGAY_NHAP, SO_LUONG_NHAP, DON_GIA_NHAP, SO_LUONG_TON)
                              VALUES (@IdPhieuCT, @MaHH, @NgayNhap, @SoLuong, @DonGia, @SoLuong)",
                            DbHelper.Param("@IdPhieuCT", idPhieuCT),
                            DbHelper.Param("@MaHH", maHH),
                            DbHelper.Param("@NgayNhap", dtpNgayLap.Value),
                            DbHelper.Param("@SoLuong", soLuong),
                            DbHelper.Param("@DonGia", donGiaNhap)
                        );

                        affectedRows += DbHelper.ExecuteInTran(conn, tran,
                            "UPDATE DM_HANGHOA SET TONKHO = TONKHO + @SoLuong WHERE MAHH = @MaHH",
                            DbHelper.Param("@SoLuong", soLuong),
                            DbHelper.Param("@MaHH", maHH)
                        );
                    }

                    return affectedRows;
                });

                MessageBox.Show("Ghi sổ thành công! Hàng đã được nhập vào kho.", "Thành công");
                SetInputMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ghi sổ: " + ex.Message, "Lỗi");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputs();
            SetInputMode(false);
            isAdding = false;
        }

        private void dgvPhieuNhap_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvPhieuNhap.Rows[e.RowIndex];

            if (e.ColumnIndex == dgvPhieuNhap.Columns["SL"].Index || e.ColumnIndex == dgvPhieuNhap.Columns["DONGIA"].Index)
            {
                int soLuong = Convert.ToInt32(row.Cells["SL"].Value ?? 0);
                decimal donGia = Convert.ToDecimal(row.Cells["DONGIA"].Value ?? 0);
                row.Cells["THANHTIEN"].Value = soLuong * donGia;

                decimal tongTien = 0;
                foreach (DataGridViewRow r in dgvPhieuNhap.Rows)
                {
                    tongTien += Convert.ToDecimal(r.Cells["THANHTIEN"].Value ?? 0);
                }
                lblTongTien.Text = tongTien.ToString("N0");
            }
        }

        #endregion
    }
}