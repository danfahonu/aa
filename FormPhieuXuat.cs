using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormPhieuXuat : Form
    {
        private bool isAdding = false;
        private long currentSoPhieu = -1;

        public FormPhieuXuat()
        {
            InitializeComponent();
        }

        private void FormPhieuXuat_Load(object sender, EventArgs e)
        {
            LoadComboBoxKhachHang();
            SetupDataGridViewComboBox();
            SetInputMode(false);
            ClearInputs();
        }

        #region Xử lý dữ liệu và ComboBox

        private void LoadComboBoxKhachHang()
        {
            try
            {
                string query = "SELECT MAKH, TENKH FROM DANHMUCKHACHHANG";
                DataTable dt = DbHelper.Query(query);
                cboKhachHang.DataSource = dt;
                cboKhachHang.DisplayMember = "TENKH";
                cboKhachHang.ValueMember = "MAKH";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message);
            }
        }

        private void SetupDataGridViewComboBox()
        {
            try
            {
                string query = "SELECT MAHH, TENHH FROM DM_HANGHOA WHERE ACTIVE = 1";
                DataTable dt = DbHelper.Query(query);

                DataGridViewComboBoxColumn cmbCol = dgvPhieuXuat.Columns["MAHH"] as DataGridViewComboBoxColumn;
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
            cboKhachHang.SelectedIndex = -1;
            txtGhiChu.Text = "";
            dgvPhieuXuat.Rows.Clear();
            lblTongTien.Text = "0";
            currentSoPhieu = -1;
        }

        #endregion

        #region Quản lý trạng thái giao diện (UX)

        private void SetInputMode(bool enable)
        {
            dtpNgayLap.Enabled = enable;
            cboKhachHang.Enabled = enable;
            txtGhiChu.ReadOnly = !enable;
            dgvPhieuXuat.ReadOnly = !enable;

            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;
            btnThem.Enabled = !enable;
            btnGhiSo.Enabled = !enable && currentSoPhieu != -1;
        }

        #endregion

        #region Sự kiện

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearInputs();
            SetInputMode(true);
            cboKhachHang.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboKhachHang.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isAdding)
                {
                    string queryPhieu = @"
                        INSERT INTO PHIEU (NGAYLAP, LOAI, MAKH, GHICHU, TRANGTHAI)
                        OUTPUT INSERTED.SOPHIEU
                        VALUES (@NgayLap, 'X', @MaKH, @GhiChu, 0)";
                    object generatedId = DbHelper.Scalar(queryPhieu,
                        DbHelper.Param("@NgayLap", dtpNgayLap.Value),
                        DbHelper.Param("@MaKH", cboKhachHang.SelectedValue),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text)
                    );
                    currentSoPhieu = Convert.ToInt64(generatedId);
                }
                else
                {
                    string queryPhieu = @"
                        UPDATE PHIEU SET NGAYLAP = @NgayLap, MAKH = @MaKH, GHICHU = @GhiChu
                        WHERE SOPHIEU = @SoPhieu";
                    DbHelper.Execute(queryPhieu,
                         DbHelper.Param("@NgayLap", dtpNgayLap.Value),
                         DbHelper.Param("@MaKH", cboKhachHang.SelectedValue),
                         DbHelper.Param("@GhiChu", txtGhiChu.Text),
                         DbHelper.Param("@SoPhieu", currentSoPhieu)
                    );
                    DbHelper.Execute("DELETE FROM PHIEU_CT WHERE SOPHIEU = @SoPhieu", DbHelper.Param("@SoPhieu", currentSoPhieu));
                }

                foreach (DataGridViewRow row in dgvPhieuXuat.Rows)
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
                    // Lấy chi tiết phiếu xuất
                    var dtChiTietXuat = DbHelper.QueryInTran(conn, tran,
                        "SELECT ID, MAHH, SL FROM PHIEU_CT WHERE SOPHIEU = @SoPhieu",
                        DbHelper.Param("@SoPhieu", currentSoPhieu));

                    foreach (DataRow rowXuat in dtChiTietXuat.Rows)
                    {
                        string maHH = rowXuat["MAHH"].ToString();
                        int soLuongXuat = Convert.ToInt32(rowXuat["SL"]);
                        long idPhieuXuatCT = Convert.ToInt64(rowXuat["ID"]);
                        decimal tongGiaVon = 0;

                        // Lấy các lô hàng tồn kho theo thứ tự FIFO
                        var dtTonKhoFIFO = DbHelper.QueryInTran(conn, tran,
                            @"SELECT ID, SO_LUONG_TON, DON_GIA_NHAP 
                              FROM KHO_CHITIET_TONKHO 
                              WHERE MAHH = @MaHH AND SO_LUONG_TON > 0 
                              ORDER BY NGAY_NHAP ASC, ID ASC",
                            DbHelper.Param("@MaHH", maHH));

                        // Kiểm tra tồn kho
                        int tongTon = 0;
                        foreach (DataRow r in dtTonKhoFIFO.Rows) tongTon += Convert.ToInt32(r["SO_LUONG_TON"]);
                        if (soLuongXuat > tongTon)
                        {
                            throw new Exception($"Không đủ tồn kho cho mặt hàng '{maHH}'. Tồn: {tongTon}, Xuất: {soLuongXuat}");
                        }

                        // Trừ tồn kho theo FIFO
                        foreach (DataRow rowTon in dtTonKhoFIFO.Rows)
                        {
                            if (soLuongXuat <= 0) break;

                            long idTonKho = Convert.ToInt64(rowTon["ID"]);
                            int soLuongTonTrongLo = Convert.ToInt32(rowTon["SO_LUONG_TON"]);
                            decimal donGiaNhap = Convert.ToDecimal(rowTon["DON_GIA_NHAP"]);

                            int slXuatTuLo = Math.Min(soLuongXuat, soLuongTonTrongLo);

                            // Cập nhật lại số lượng tồn trong lô
                            DbHelper.ExecuteInTran(conn, tran,
                                "UPDATE KHO_CHITIET_TONKHO SET SO_LUONG_TON = SO_LUONG_TON - @sl WHERE ID = @id",
                                DbHelper.Param("@sl", slXuatTuLo),
                                DbHelper.Param("@id", idTonKho));

                            // Tính tổng giá vốn
                            tongGiaVon += slXuatTuLo * donGiaNhap;
                            soLuongXuat -= slXuatTuLo;
                        }

                        // Cập nhật giá vốn trung bình cho dòng chi tiết phiếu xuất
                        decimal giaVonTrungBinh = tongGiaVon / Convert.ToInt32(rowXuat["SL"]);
                        DbHelper.ExecuteInTran(conn, tran,
                            "UPDATE PHIEU_CT SET GIAVON = @GiaVon WHERE ID = @id",
                            DbHelper.Param("@GiaVon", giaVonTrungBinh),
                            DbHelper.Param("@id", idPhieuXuatCT));

                        // Cập nhật tồn kho tổng
                        DbHelper.ExecuteInTran(conn, tran,
                            "UPDATE DM_HANGHOA SET TONKHO = TONKHO - @sl WHERE MAHH = @maHH",
                            DbHelper.Param("@sl", Convert.ToInt32(rowXuat["SL"])),
                            DbHelper.Param("@maHH", maHH));
                    }

                    // Cập nhật trạng thái phiếu
                    DbHelper.ExecuteInTran(conn, tran,
                        "UPDATE PHIEU SET TRANGTHAI = 1 WHERE SOPHIEU = @SoPhieu",
                        DbHelper.Param("@SoPhieu", currentSoPhieu));

                    return 1; // return > 0 để commit transaction
                });

                MessageBox.Show("Ghi sổ thành công! Hàng đã được xuất khỏi kho.", "Thành công");
                SetInputMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ghi sổ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPhieuXuat_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvPhieuXuat.Rows[e.RowIndex];

            // Tự động điền giá bán khi chọn hàng hóa
            if (e.ColumnIndex == dgvPhieuXuat.Columns["MAHH"].Index)
            {
                string maHH = row.Cells["MAHH"].Value?.ToString();
                if (!string.IsNullOrEmpty(maHH))
                {
                    object giaBan = DbHelper.Scalar("SELECT GIABAN FROM DM_HANGHOA WHERE MAHH = @maHH", DbHelper.Param("@maHH", maHH));
                    row.Cells["DONGIA"].Value = giaBan;
                }
            }

            // Tự động tính thành tiền
            if (e.ColumnIndex == dgvPhieuXuat.Columns["SL"].Index || e.ColumnIndex == dgvPhieuXuat.Columns["DONGIA"].Index)
            {
                int soLuong = Convert.ToInt32(row.Cells["SL"].Value ?? 0);
                decimal donGia = Convert.ToDecimal(row.Cells["DONGIA"].Value ?? 0);
                row.Cells["THANHTIEN"].Value = soLuong * donGia;

                // Cập nhật tổng tiền
                decimal tongTien = 0;
                foreach (DataGridViewRow r in dgvPhieuXuat.Rows)
                {
                    tongTien += Convert.ToDecimal(r.Cells["THANHTIEN"].Value ?? 0);
                }
                lblTongTien.Text = tongTien.ToString("N0");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputs();
            SetInputMode(false);
            isAdding = false;
        }

        #endregion
    }
}