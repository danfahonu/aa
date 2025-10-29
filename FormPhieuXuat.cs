using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormPhieuXuat : Form
    {
        public FormPhieuXuat() { InitializeComponent(); }

        private enum FormMode { View, Edit, New }
        private FormMode _currentMode = FormMode.View;

        private DataTable _dtPhieu;
        private DataTable _dtPhieuCT;
        private DataTable _dtKH;
        private DataTable _dtHangHoa;

        private BindingSource _bsPhieu = new BindingSource();
        private BindingSource _bsPhieuCT = new BindingSource();

        private void FormPhieuXuat_Load(object sender, EventArgs e)
        {
            try
            {
                // Gán các sự kiện quan trọng
                gridMain.SelectionChanged += gridMain_SelectionChanged;
                gridDetail.CellEndEdit += gridDetail_CellEndEdit;
                gridDetail.EditingControlShowing += gridDetail_EditingControlShowing;
                _bsPhieuCT.ListChanged += (s, ev) => TinhTongTien();
                btnGhiSo.Click += btnGhiSo_Click;
                // Không gán sự kiện cho các nút trên toolstrip ở đây, chúng đã được gán trong designer

                LoadLookups();
                LoadData();
                DataBindingControl();
                SetFormMode(FormMode.View);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Tải dữ liệu và Binding

        void LoadLookups()
        {
            _dtKH = DbHelper.Query("SELECT MAKH, TENKH FROM dbo.DANHMUCKHACHHANG ORDER BY TENKH");
            cboKhachHang.DataSource = _dtKH;
            cboKhachHang.ValueMember = "MAKH";
            cboKhachHang.DisplayMember = "TENKH";

            _dtHangHoa = DbHelper.Query("SELECT MAHH, TENHH, GIABAN, TONKHO FROM dbo.DM_HANGHOA WHERE ACTIVE=1 ORDER BY TENHH");
        }

        void LoadData()
        {
            string sqlPhieu = "SELECT p.SOPHIEU, p.NGAYLAP, p.MAKH, kh.TENKH, p.GHICHU, p.TRANGTHAI FROM dbo.PHIEU p LEFT JOIN dbo.DANHMUCKHACHHANG kh ON p.MAKH = kh.MAKH WHERE p.LOAI='X' ORDER BY p.NGAYLAP DESC, p.SOPHIEU DESC";
            _dtPhieu = DbHelper.Query(sqlPhieu);
            _bsPhieu.DataSource = _dtPhieu;
            gridMain.DataSource = _bsPhieu;
            ConfigureGridMain();
        }

        private void ConfigureGridMain()
        {
            // **SỬA LỖI: Chỉ cấu hình các cột nếu grid thực sự có cột**
            if (gridMain.Columns.Count == 0) return;

            gridMain.Columns["SOPHIEU"].HeaderText = "Số Phiếu";
            gridMain.Columns["NGAYLAP"].HeaderText = "Ngày Lập";
            gridMain.Columns["TENKH"].HeaderText = "Khách Hàng";
            gridMain.Columns["GHICHU"].HeaderText = "Ghi Chú";
            gridMain.Columns["TRANGTHAI"].HeaderText = "Trạng Thái";
            gridMain.Columns["MAKH"].Visible = false;

            gridMain.Columns["TRANGTHAI"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridMain.CellFormatting -= gridMain_CellFormatting;
            gridMain.CellFormatting += gridMain_CellFormatting;

            gridMain.AutoResizeColumns();
        }

        private void gridMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // **SỬA LỖI: Thêm kiểm tra cột có tồn tại không trước khi truy cập**
            if (gridMain.Columns.Contains("TRANGTHAI") && e.ColumnIndex == gridMain.Columns["TRANGTHAI"].Index && e.Value != null)
            {
                if (e.Value is int || e.Value is long)
                {
                    e.Value = Convert.ToInt32(e.Value) == 1 ? "Đã Ghi Sổ" : "Phiếu Tạm";
                    e.FormattingApplied = true;
                }
            }
        }

        private void DataBindingControl()
        {
            txtSoPhieu.DataBindings.Clear();
            dtpNgayLap.DataBindings.Clear();
            cboKhachHang.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();

            txtSoPhieu.DataBindings.Add("Text", _bsPhieu, "SOPHIEU", true, DataSourceUpdateMode.Never);
            dtpNgayLap.DataBindings.Add("Value", _bsPhieu, "NGAYLAP", true, DataSourceUpdateMode.Never);
            cboKhachHang.DataBindings.Add("SelectedValue", _bsPhieu, "MAKH", true, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", _bsPhieu, "GHICHU", true, DataSourceUpdateMode.Never);
        }

        private void gridMain_SelectionChanged(object sender, EventArgs e)
        {
            if (_currentMode != FormMode.View || _bsPhieu.Current == null)
            {
                ClearDetailsAndDisableButtons();
                return;
            }

            DataRowView drv = (DataRowView)_bsPhieu.Current;

            if (drv.IsNew || drv["SOPHIEU"] == DBNull.Value)
            {
                ClearDetailsAndDisableButtons();
                if (_currentMode == FormMode.New) btnLuu.Enabled = true;
                return;
            }

            long soPhieu = Convert.ToInt64(drv["SOPHIEU"]);
            bool daGhiSo = Convert.ToInt32(drv["TRANGTHAI"]) == 1;

            btnSua.Enabled = !daGhiSo;
            btnXoa.Enabled = !daGhiSo;
            btnGhiSo.Enabled = !daGhiSo;

            LoadChiTietPhieu(soPhieu);
        }

        private void ClearDetailsAndDisableButtons()
        {
            _bsPhieuCT.DataSource = null;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhiSo.Enabled = false;
        }

        private void LoadChiTietPhieu(long soPhieu)
        {
            string sqlCT = "SELECT CT.ID, CT.MAHH, HH.TENHH, CT.SL, CT.DONGIA, CT.THANHTIEN FROM dbo.PHIEU_CT CT JOIN dbo.DM_HANGHOA HH ON CT.MAHH = HH.MAHH WHERE CT.SOPHIEU=@SOPHIEU";
            _dtPhieuCT = DbHelper.Query(sqlCT, DbHelper.Param("@SOPHIEU", soPhieu));
            _bsPhieuCT.DataSource = _dtPhieuCT;
            gridDetail.DataSource = _bsPhieuCT;
        }

        #endregion

        #region Quản lý trạng thái và các nút

        private void SetFormMode(FormMode mode)
        {
            _currentMode = mode;
            bool isEditing = (mode != FormMode.View);

            dtpNgayLap.Enabled = isEditing;
            cboKhachHang.Enabled = isEditing;
            txtGhiChu.ReadOnly = !isEditing;
            gridDetail.ReadOnly = !isEditing;

            gridMain.Enabled = !isEditing;
            btnMoi.Enabled = !isEditing;
            btnLuu.Enabled = isEditing;

            if (isEditing)
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnGhiSo.Enabled = false;
            }
            else
            {
                gridMain_SelectionChanged(null, null);
            }

            if (mode == FormMode.New)
            {
                SetupNewVoucher();
            }
        }

        private void SetupNewVoucher()
        {
            _bsPhieu.AddNew();
            txtSoPhieu.Text = "(Tự động)";
            dtpNgayLap.Value = DateTime.Today;

            _dtPhieuCT = new DataTable();
            _dtPhieuCT.Columns.Add("ID", typeof(long));
            _dtPhieuCT.Columns.Add("MAHH", typeof(string));
            _dtPhieuCT.Columns.Add("TENHH", typeof(string));
            _dtPhieuCT.Columns.Add("SL", typeof(int));
            _dtPhieuCT.Columns.Add("DONGIA", typeof(decimal));
            _dtPhieuCT.Columns.Add("THANHTIEN", typeof(decimal), "SL * DONGIA");
            _bsPhieuCT.DataSource = _dtPhieuCT;
            gridDetail.DataSource = _bsPhieuCT;

            if (gridDetail.Columns["cmbMaHH"] == null)
            {
                DataGridViewComboBoxColumn cmbCol = new DataGridViewComboBoxColumn
                {
                    HeaderText = "Hàng Hóa",
                    Name = "cmbMaHH",
                    DataSource = _dtHangHoa,
                    ValueMember = "MAHH",
                    DisplayMember = "TENHH",
                    DataPropertyName = "MAHH",
                    DisplayStyleForCurrentCellOnly = true
                };
                gridDetail.Columns.Insert(0, cmbCol);
            }
            cboKhachHang.Focus();
        }

        private void btnMoi_Click(object sender, EventArgs e) => SetFormMode(FormMode.New);
        private void btnSua_Click(object sender, EventArgs e) => SetFormMode(FormMode.Edit);
        private void btnNap_Click(object sender, EventArgs e)
        {
            SetFormMode(FormMode.View);
            LoadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedValue == null || cboKhachHang.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Vui lòng chọn khách hàng.");
                return;
            }
            if (_dtPhieuCT.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng nhập ít nhất một mặt hàng.");
                return;
            }

            try
            {
                long soPhieu = 0;
                DbHelper.ExecuteTran((conn, tran) =>
                {
                    if (_currentMode == FormMode.New)
                    {
                        string sql = "INSERT INTO dbo.PHIEU(NGAYLAP, LOAI, MAKH, GHICHU, TRANGTHAI) VALUES(@ngay, 'X', @maKH, @ghiChu, 0); SELECT SCOPE_IDENTITY();";
                        soPhieu = Convert.ToInt64(DbHelper.ScalarInTran(conn, tran, sql,
                            DbHelper.Param("@ngay", dtpNgayLap.Value),
                            DbHelper.Param("@maKH", cboKhachHang.SelectedValue),
                            DbHelper.Param("@ghiChu", txtGhiChu.Text)));
                    }
                    else // Edit mode
                    {
                        soPhieu = Convert.ToInt64(txtSoPhieu.Text);
                        string sql = "UPDATE dbo.PHIEU SET NGAYLAP=@ngay, MAKH=@maKH, GHICHU=@ghiChu WHERE SOPHIEU=@soPhieu";
                        DbHelper.ExecuteInTran(conn, tran, sql,
                            DbHelper.Param("@ngay", dtpNgayLap.Value),
                            DbHelper.Param("@maKH", cboKhachHang.SelectedValue),
                            DbHelper.Param("@ghiChu", txtGhiChu.Text),
                            DbHelper.Param("@soPhieu", soPhieu));

                        DbHelper.ExecuteInTran(conn, tran, "DELETE FROM dbo.PHIEU_CT WHERE SOPHIEU=@soPhieu", DbHelper.Param("@soPhieu", soPhieu));
                    }

                    foreach (DataRow r in _dtPhieuCT.Rows)
                    {
                        if (r.RowState == DataRowState.Deleted) continue;
                        DbHelper.ExecuteInTran(conn, tran,
                            "INSERT INTO dbo.PHIEU_CT(SOPHIEU, MAHH, SL, DONGIA) VALUES(@soPhieu, @maHH, @sl, @donGia)",
                            DbHelper.Param("@soPhieu", soPhieu),
                            DbHelper.Param("@maHH", r["MAHH"]),
                            DbHelper.Param("@sl", r["SL"]),
                            DbHelper.Param("@donGia", r["DONGIA"]));
                    }

                    return 1;
                });

                MessageBox.Show("Lưu phiếu tạm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                _bsPhieu.Position = _bsPhieu.Find("SOPHIEU", soPhieu);
                SetFormMode(FormMode.View);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu tạm: " + ex.Message, "Lỗi");
            }
        }

        private void btnGhiSo_Click(object sender, EventArgs e)
        {
            if (_bsPhieu.Current == null || ((DataRowView)_bsPhieu.Current).IsNew) return;
            long soPhieu = Convert.ToInt64(txtSoPhieu.Text);

            if (MessageBox.Show($"Bạn có chắc muốn ghi sổ phiếu '{soPhieu}' không? Thao tác này sẽ trừ kho và hạch toán, phiếu sẽ không thể sửa hoặc xóa.", "Xác nhận Ghi Sổ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                DbHelper.ExecuteTran((conn, tran) =>
                {
                    decimal tongTienBan = 0;
                    decimal tongGiaVon = 0;

                    DataTable dtChiTiet = DbHelper.QueryInTran(conn, tran, "SELECT ID, MAHH, SL, DONGIA FROM dbo.PHIEU_CT WHERE SOPHIEU = @soPhieu", DbHelper.Param("@soPhieu", soPhieu));

                    foreach (DataRow r in dtChiTiet.Rows)
                    {
                        string maHH = r["MAHH"].ToString();
                        int soLuongXuat = Convert.ToInt32(r["SL"]);
                        decimal donGiaBan = Convert.ToDecimal(r["DONGIA"]);

                        tongTienBan += soLuongXuat * donGiaBan;
                        decimal giaVonChoDong = 0;

                        string sqlGetLohang = "SELECT ID, SO_LUONG_TON, DON_GIA_NHAP FROM KHO_CHITIET_TONKHO WHERE MAHH = @mahh AND SO_LUONG_TON > 0 ORDER BY NGAY_NHAP, ID";
                        DataTable dtLohang = DbHelper.QueryInTran(conn, tran, sqlGetLohang, DbHelper.Param("@mahh", maHH));

                        int soLuongCanXuat = soLuongXuat;
                        foreach (DataRow lo in dtLohang.Rows)
                        {
                            if (soLuongCanXuat <= 0) break;

                            long idLo = Convert.ToInt64(lo["ID"]);
                            int soLuongTonTrongLo = Convert.ToInt32(lo["SO_LUONG_TON"]);
                            decimal donGiaNhapCuaLo = Convert.ToDecimal(lo["DON_GIA_NHAP"]);

                            int slXuatTuLo = Math.Min(soLuongCanXuat, soLuongTonTrongLo);

                            giaVonChoDong += slXuatTuLo * donGiaNhapCuaLo;

                            string sqlUpdateKho = "UPDATE KHO_CHITIET_TONKHO SET SO_LUONG_TON = SO_LUONG_TON - @slXuat WHERE ID = @idLo";
                            DbHelper.ExecuteInTran(conn, tran, sqlUpdateKho, DbHelper.Param("@slXuat", slXuatTuLo), DbHelper.Param("@idLo", idLo));

                            soLuongCanXuat -= slXuatTuLo;
                        }

                        if (soLuongCanXuat > 0)
                        {
                            var tenHHObj = DbHelper.ScalarInTran(conn, tran, "SELECT TENHH FROM DM_HANGHOA WHERE MAHH = @mahh", DbHelper.Param("@mahh", maHH));
                            string tenHH = tenHHObj?.ToString() ?? maHH;
                            throw new Exception($"Không đủ tồn kho cho mặt hàng '{tenHH}'. Còn thiếu {soLuongCanXuat}.");
                        }

                        string sqlUpdateGiaVonCT = "UPDATE dbo.PHIEU_CT SET GIAVON = @giaVon WHERE ID = @id";
                        DbHelper.ExecuteInTran(conn, tran, sqlUpdateGiaVonCT, DbHelper.Param("@giaVon", giaVonChoDong), DbHelper.Param("@id", r["ID"]));

                        tongGiaVon += giaVonChoDong;
                    }

                    string sqlHachToan = "INSERT INTO dbo.BUTTOAN_KETOAN (NGAY_HT, SO_CT, MA_CT, DIEN_GIAI, TK_NO, TK_CO, SOTIEN) VALUES (@ngay_ht, @so_ct, @ma_ct, @dien_giai, @tk_no, @tk_co, @sotien)";

                    DbHelper.ExecuteInTran(conn, tran, sqlHachToan,
                        DbHelper.Param("@ngay_ht", dtpNgayLap.Value.Date), DbHelper.Param("@so_ct", soPhieu.ToString()), DbHelper.Param("@ma_ct", "PX"),
                        DbHelper.Param("@dien_giai", $"Ghi nhận doanh thu bán hàng cho {cboKhachHang.Text}"),
                        DbHelper.Param("@tk_no", "131"), DbHelper.Param("@tk_co", "511"), DbHelper.Param("@sotien", tongTienBan));

                    DbHelper.ExecuteInTran(conn, tran, sqlHachToan,
                       DbHelper.Param("@ngay_ht", dtpNgayLap.Value.Date), DbHelper.Param("@so_ct", soPhieu.ToString()), DbHelper.Param("@ma_ct", "PX"),
                       DbHelper.Param("@dien_giai", $"Ghi nhận giá vốn hàng bán cho {cboKhachHang.Text}"),
                       DbHelper.Param("@tk_no", "632"), DbHelper.Param("@tk_co", "156"), DbHelper.Param("@sotien", tongGiaVon));

                    DbHelper.ExecuteInTran(conn, tran, "UPDATE dbo.PHIEU SET TRANGTHAI = 1 WHERE SOPHIEU = @soPhieu", DbHelper.Param("@soPhieu", soPhieu));

                    return 1;
                });

                MessageBox.Show("Ghi sổ phiếu xuất thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                LoadLookups();
                _bsPhieu.Position = _bsPhieu.Find("SOPHIEU", soPhieu);
                SetFormMode(FormMode.View);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ghi sổ: " + ex.Message, "Lỗi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_bsPhieu.Current == null || ((DataRowView)_bsPhieu.Current).IsNew) return;
            long soPhieu = Convert.ToInt64(txtSoPhieu.Text);

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa phiếu tạm '{soPhieu}' không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DbHelper.Execute("DELETE FROM dbo.PHIEU WHERE SOPHIEU=@soPhieu", DbHelper.Param("@soPhieu", soPhieu));
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        #endregion

        #region Xử lý Grid chi tiết

        private void gridDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= NumericOnly;
            if (gridDetail.Columns[gridDetail.CurrentCell.ColumnIndex].DataPropertyName == "SL" || gridDetail.Columns[gridDetail.CurrentCell.ColumnIndex].DataPropertyName == "DONGIA")
            {
                e.Control.KeyPress += NumericOnly;
            }
        }

        private void gridDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _bsPhieuCT.Count <= e.RowIndex) return;

            DataRowView drv = (DataRowView)_bsPhieuCT[e.RowIndex];
            string maHH = drv["MAHH"]?.ToString();

            if (string.IsNullOrEmpty(maHH)) return;

            if (gridDetail.Columns[e.ColumnIndex].Name == "cmbMaHH")
            {
                DataRow hangHoa = _dtHangHoa.AsEnumerable().FirstOrDefault(r => r["MAHH"].ToString() == maHH);
                if (hangHoa != null)
                {
                    drv["DONGIA"] = hangHoa["GIABAN"] == DBNull.Value ? 0 : hangHoa["GIABAN"];
                    if (drv["SL"] == DBNull.Value || Convert.ToInt32(drv["SL"]) == 0)
                    {
                        drv["SL"] = 1;
                    }
                }
            }

            if (gridDetail.Columns[e.ColumnIndex].DataPropertyName == "SL")
            {
                var hangHoaRow = _dtHangHoa.AsEnumerable().FirstOrDefault(r => r["MAHH"].ToString() == maHH);
                if (hangHoaRow != null)
                {
                    int soLuongTon = Convert.ToInt32(hangHoaRow["TONKHO"]);
                    int soLuongXuat = Convert.ToInt32(drv["SL"]);
                    if (soLuongXuat > soLuongTon)
                    {
                        MessageBox.Show($"Số lượng tồn của mặt hàng '{hangHoaRow["TENHH"]}' chỉ còn {soLuongTon}. Vui lòng nhập lại.", "Cảnh báo tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        drv["SL"] = soLuongTon;
                    }
                }
            }

            TinhTongTien();
        }

        private void NumericOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void TinhTongTien()
        {
            decimal tongTien = 0;
            foreach (DataRowView drv in _bsPhieuCT)
            {
                if (drv.Row.RowState != DataRowState.Deleted && drv["THANHTIEN"] != DBNull.Value)
                    tongTien += Convert.ToDecimal(drv["THANHTIEN"]);
            }
            lblTongTien.Text = $"Tổng cộng: {tongTien:N0} VNĐ";
        }

        #endregion
    }
}