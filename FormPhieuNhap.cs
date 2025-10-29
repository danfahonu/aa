using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;


namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormPhieuNhap : Form
    {
        private enum FormMode { View, Edit, New }
        private FormMode _currentMode = FormMode.View;

        private DataTable _dtPhieu;
        private DataTable _dtPhieuCT;
        private DataTable _dtNCC;
        private DataTable _dtHangHoa;

        private BindingSource _bsPhieu = new BindingSource();
        private BindingSource _bsPhieuCT = new BindingSource();

        public FormPhieuNhap()
        {
            InitializeComponent();
        }

        private void FormPhieuNhap_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLookups();
                LoadData();
                DataBindingControl();
                SetFormMode(FormMode.View);

                // Gán các sự kiện
                gridMain.SelectionChanged += gridMain_SelectionChanged;
                gridDetail.CellEndEdit += gridDetail_CellEndEdit;
                gridDetail.EditingControlShowing += gridDetail_EditingControlShowing;
                _bsPhieuCT.ListChanged += (s, ev) => TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Tải dữ liệu và Binding

        void LoadLookups()
        {
            _dtNCC = DbHelper.Query("SELECT MA_NCC, TEN_NCC FROM dbo.DM_NHACUNGCAP ORDER BY TEN_NCC");
            cboNhaCungCap.DataSource = _dtNCC;
            cboNhaCungCap.ValueMember = "MA_NCC";
            cboNhaCungCap.DisplayMember = "TEN_NCC";

            _dtHangHoa = DbHelper.Query("SELECT MAHH, TENHH, GIAVON FROM dbo.DM_HANGHOA WHERE ACTIVE=1 ORDER BY TENHH");
        }

        void LoadData()
        {
            string sqlPhieu = "SELECT p.SOPHIEU, p.NGAYLAP, p.MA_NCC, ncc.TEN_NCC, p.GHICHU FROM dbo.PHIEU p LEFT JOIN dbo.DM_NHACUNGCAP ncc ON p.MA_NCC = ncc.MA_NCC WHERE p.LOAI='N' ORDER BY p.NGAYLAP DESC, p.SOPHIEU DESC";
            _dtPhieu = DbHelper.Query(sqlPhieu);
            _bsPhieu.DataSource = _dtPhieu;
            gridMain.DataSource = _bsPhieu;
            ConfigureGridMain();
        }

        private void ConfigureGridMain()
        {
            gridMain.Columns["SOPHIEU"].HeaderText = "Số Phiếu";
            gridMain.Columns["NGAYLAP"].HeaderText = "Ngày Lập";
            gridMain.Columns["TEN_NCC"].HeaderText = "Nhà Cung Cấp";
            gridMain.Columns["GHICHU"].HeaderText = "Ghi Chú";
            gridMain.Columns["MA_NCC"].Visible = false;
            gridMain.AutoResizeColumns();
        }

        private void DataBindingControl()
        {
            txtSoPhieu.DataBindings.Clear();
            dtpNgayLap.DataBindings.Clear();
            cboNhaCungCap.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();

            txtSoPhieu.DataBindings.Add("Text", _bsPhieu, "SOPHIEU", true, DataSourceUpdateMode.Never);
            dtpNgayLap.DataBindings.Add("Value", _bsPhieu, "NGAYLAP", true, DataSourceUpdateMode.Never);
            cboNhaCungCap.DataBindings.Add("SelectedValue", _bsPhieu, "MA_NCC", true, DataSourceUpdateMode.Never);
            txtGhiChu.DataBindings.Add("Text", _bsPhieu, "GHICHU", true, DataSourceUpdateMode.Never);
        }

        private void gridMain_SelectionChanged(object sender, EventArgs e)
        {
            if (_currentMode != FormMode.View) return;
            if (_bsPhieu.Current == null)
            {
                _bsPhieuCT.DataSource = null;
                return;
            }

            DataRowView drv = (DataRowView)_bsPhieu.Current;
            long soPhieu = Convert.ToInt64(drv["SOPHIEU"]);
            LoadChiTietPhieu(soPhieu);
        }

        private void LoadChiTietPhieu(long soPhieu)
        {
            string sqlCT = "SELECT CT.ID, CT.MAHH, HH.TENHH, CT.SL, CT.DONGIA, CT.THANHTIEN FROM dbo.PHIEU_CT CT JOIN dbo.DM_HANGHOA HH ON CT.MAHH = HH.MAHH WHERE CT.SOPHIEU=@SOPHIEU";
            _dtPhieuCT = DbHelper.Query(sqlCT, DbHelper.Param("@SOPHIEU", soPhieu));
            _bsPhieuCT.DataSource = _dtPhieuCT;
            gridDetail.DataSource = _bsPhieuCT;
            ConfigureGridDetail();
        }

        private void ConfigureGridDetail()
        {
            // Cấu hình các cột hiển thị (bạn có thể tùy chỉnh thêm ở đây)
        }

        #endregion

        #region Quản lý trạng thái và sự kiện nút

        private void SetFormMode(FormMode mode)
        {
            _currentMode = mode;
            bool isEditing = (mode != FormMode.View);

            dtpNgayLap.Enabled = isEditing;
            cboNhaCungCap.Enabled = isEditing;
            txtGhiChu.ReadOnly = !isEditing;
            gridDetail.ReadOnly = !isEditing;

            btnMoi.Enabled = !isEditing;
            btnSua.Enabled = !isEditing && _bsPhieu.Current != null;
            btnLuu.Enabled = isEditing;
            btnXoa.Enabled = !isEditing && _bsPhieu.Current != null;
            btnInPhieu.Enabled = !isEditing && _bsPhieu.Current != null;
            gridMain.Enabled = !isEditing;

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
                    DataPropertyName = "MAHH"
                };
                gridDetail.Columns.Insert(0, cmbCol);
            }
            cboNhaCungCap.Focus();
        }

        private void btnMoi_Click(object sender, EventArgs e) => SetFormMode(FormMode.New);
        private void btnSua_Click(object sender, EventArgs e) => SetFormMode(FormMode.Edit);
        private void btnNap_Click(object sender, EventArgs e) => LoadData();

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboNhaCungCap.SelectedValue == null) { MessageBox.Show("Vui lòng chọn nhà cung cấp."); return; }
            if (_dtPhieuCT.Rows.Count == 0) { MessageBox.Show("Vui lòng nhập ít nhất một mặt hàng."); return; }

            foreach (DataRow r in _dtPhieuCT.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                if (r["MAHH"] == DBNull.Value || string.IsNullOrEmpty(r["MAHH"].ToString()))
                {
                    MessageBox.Show("Có một dòng chi tiết chưa chọn hàng hóa. Vui lòng kiểm tra lại.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (r["SL"] == DBNull.Value || Convert.ToInt32(r["SL"]) <= 0)
                {
                    MessageBox.Show($"Hàng hóa '{r["TENHH"]}' phải có số lượng lớn hơn 0.", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                long soPhieu = 0;
                decimal tongTienPhieu = 0; // Biến để tính tổng tiền phiếu

                DbHelper.ExecuteTran((conn, tran) =>
                {
                    if (_currentMode == FormMode.New)
                    {
                        string sql = "INSERT INTO dbo.PHIEU(NGAYLAP, LOAI, MA_NCC, GHICHU) VALUES(@ngay, 'N', @maNCC, @ghiChu); SELECT SCOPE_IDENTITY();";
                        soPhieu = Convert.ToInt64(DbHelper.ScalarInTran(conn, tran, sql,
                            DbHelper.Param("@ngay", dtpNgayLap.Value),
                            DbHelper.Param("@maNCC", cboNhaCungCap.SelectedValue),
                            DbHelper.Param("@ghiChu", txtGhiChu.Text)));
                    }
                    else // Edit
                    {
                        soPhieu = Convert.ToInt64(txtSoPhieu.Text);
                        string sql = "UPDATE dbo.PHIEU SET NGAYLAP=@ngay, MA_NCC=@maNCC, GHICHU=@ghiChu WHERE SOPHIEU=@soPhieu";
                        DbHelper.ExecuteInTran(conn, tran, sql,
                            DbHelper.Param("@ngay", dtpNgayLap.Value),
                            DbHelper.Param("@maNCC", cboNhaCungCap.SelectedValue),
                            DbHelper.Param("@ghiChu", txtGhiChu.Text),
                            DbHelper.Param("@soPhieu", soPhieu));

                        DbHelper.ExecuteInTran(conn, tran, "DELETE FROM dbo.KHO_CHITIET_TONKHO WHERE ID_PHIEUNHAP_CT IN (SELECT ID FROM dbo.PHIEU_CT WHERE SOPHIEU=@soPhieu)", DbHelper.Param("@soPhieu", soPhieu));
                        DbHelper.ExecuteInTran(conn, tran, "DELETE FROM dbo.PHIEU_CT WHERE SOPHIEU=@soPhieu", DbHelper.Param("@soPhieu", soPhieu));
                    }

                    foreach (DataRow r in _dtPhieuCT.Rows)
                    {
                        if (r.RowState == DataRowState.Deleted) continue;

                        tongTienPhieu += Convert.ToDecimal(r["SL"]) * Convert.ToDecimal(r["DONGIA"]);

                        string sqlInsertCT = "INSERT INTO dbo.PHIEU_CT(SOPHIEU, MAHH, SL, DONGIA) VALUES(@soPhieu, @maHH, @sl, @donGia); SELECT SCOPE_IDENTITY();";
                        long phieuCtId = Convert.ToInt64(DbHelper.ScalarInTran(conn, tran, sqlInsertCT,
                            DbHelper.Param("@soPhieu", soPhieu),
                            DbHelper.Param("@maHH", r["MAHH"]),
                            DbHelper.Param("@sl", r["SL"]),
                            DbHelper.Param("@donGia", r["DONGIA"])));

                        string sqlInsertKho = @"
                            INSERT INTO KHO_CHITIET_TONKHO 
                                (ID_PHIEUNHAP_CT, MAHH, NGAY_NHAP, SO_LUONG_NHAP, DON_GIA_NHAP, SO_LUONG_TON)
                            VALUES 
                                (@id_phieunhap_ct, @mahh, @ngay_nhap, @so_luong, @don_gia, @so_luong_ton)";

                        DbHelper.ExecuteInTran(conn, tran, sqlInsertKho,
                            DbHelper.Param("@id_phieunhap_ct", phieuCtId),
                            DbHelper.Param("@mahh", r["MAHH"]),
                            DbHelper.Param("@ngay_nhap", dtpNgayLap.Value.Date),
                            DbHelper.Param("@so_luong", r["SL"]),
                            DbHelper.Param("@don_gia", r["DONGIA"]),
                            DbHelper.Param("@so_luong_ton", r["SL"])
                        );
                    }

                    // *** NÂNG CẤP: TỰ ĐỘNG HẠCH TOÁN KẾ TOÁN ***
                    string sqlHachToan = @"
                        INSERT INTO dbo.BUTTOAN_KETOAN (NGAY_HT, SO_CT, MA_CT, DIEN_GIAI, TK_NO, TK_CO, SOTIEN)
                        VALUES (@ngay_ht, @so_ct, @ma_ct, @dien_giai, @tk_no, @tk_co, @sotien)";

                    DbHelper.ExecuteInTran(conn, tran, sqlHachToan,
                        DbHelper.Param("@ngay_ht", dtpNgayLap.Value.Date),
                        DbHelper.Param("@so_ct", soPhieu.ToString()),
                        DbHelper.Param("@ma_ct", "PN"), // Mã chứng từ Phiếu Nhập
                        DbHelper.Param("@dien_giai", $"Nhập kho hàng hóa từ {cboNhaCungCap.Text}"),
                        DbHelper.Param("@tk_no", "156"), // Nợ TK 156 (Hàng hóa)
                        DbHelper.Param("@tk_co", "331"), // Có TK 331 (Phải trả người bán)
                        DbHelper.Param("@sotien", tongTienPhieu)
                    );

                    return 1;
                });

                LoadData();
                _bsPhieu.Position = _bsPhieu.Find("SOPHIEU", soPhieu);
                SetFormMode(FormMode.View);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu: " + ex.Message, "Lỗi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_bsPhieu.Current == null) return;
            long soPhieu = Convert.ToInt64(txtSoPhieu.Text);

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa phiếu nhập '{soPhieu}' không? Thao tác này sẽ xóa cả các lô hàng và bút toán kế toán liên quan.", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DbHelper.ExecuteTran((conn, tran) => {
                        // Xóa bút toán kế toán trước
                        DbHelper.ExecuteInTran(conn, tran, "DELETE FROM dbo.BUTTOAN_KETOAN WHERE SO_CT = @soPhieu AND MA_CT = 'PN'", DbHelper.Param("@soPhieu", soPhieu.ToString()));
                        // Xóa phiếu (chi tiết và lô kho sẽ được xóa theo nhờ ON DELETE CASCADE)
                        DbHelper.ExecuteInTran(conn, tran, "DELETE FROM dbo.PHIEU WHERE SOPHIEU=@soPhieu", DbHelper.Param("@soPhieu", soPhieu));
                        return 1;
                    });

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
            if (e.RowIndex < 0 || e.RowIndex >= _dtPhieuCT.Rows.Count) return;

            if (gridDetail.Columns[e.ColumnIndex].Name == "cmbMaHH")
            {
                object cellValue = gridDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (cellValue != null && cellValue != DBNull.Value)
                {
                    string maHH = cellValue.ToString();
                    DataRow hangHoa = _dtHangHoa.AsEnumerable().FirstOrDefault(r => r["MAHH"].ToString() == maHH);
                    if (hangHoa != null)
                    {
                        _dtPhieuCT.Rows[e.RowIndex]["DONGIA"] = hangHoa["GIAVON"] == DBNull.Value ? 0 : hangHoa["GIAVON"];
                        if (_dtPhieuCT.Rows[e.RowIndex]["SL"] == DBNull.Value || Convert.ToInt32(_dtPhieuCT.Rows[e.RowIndex]["SL"]) == 0)
                        {
                            _dtPhieuCT.Rows[e.RowIndex]["SL"] = 1;
                        }
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
            lblTongTien.Text = $"Tổng cộng: {tongTien.ToString("N0", new CultureInfo("vi-VN"))}";
        }

        #endregion

        #region Chức năng In

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            if (_bsPhieu.Current == null) return;

            try
            {
                DataRowView drvPhieu = (DataRowView)_bsPhieu.Current;
                long soPhieu = Convert.ToInt64(drvPhieu["SOPHIEU"]);

                string sqlHeader = @"
                    SELECT 
                        p.SOPHIEU AS SoPhieu, p.NGAYLAP AS NgayLap, p.GHICHU AS GhiChu,
                        ncc.TEN_NCC AS TenNCC, ncc.DIACHI_NCC AS DiaChiNCC, ncc.SDT AS SDT_NCC,
                        (SELECT SUM(THANHTIEN) FROM dbo.PHIEU_CT WHERE SOPHIEU = p.SOPHIEU) AS TongTien
                    FROM dbo.PHIEU p
                    JOIN dbo.DM_NHACUNGCAP ncc ON p.MA_NCC = ncc.MA_NCC
                    WHERE p.SOPHIEU = @soPhieu";

                DataTable dtHeader = DbHelper.Query(sqlHeader, DbHelper.Param("@soPhieu", soPhieu));

                string sqlDetail = @"
                    SELECT 
                        ROW_NUMBER() OVER (ORDER BY hh.TENHH) as STT,
                        hh.TENHH AS TenHH,
                        hh.DVT,
                        ct.SL,
                        ct.DONGIA AS DonGia,
                        ct.THANHTIEN AS ThanhTien
                    FROM dbo.PHIEU_CT ct
                    JOIN dbo.DM_HANGHOA hh ON ct.MAHH = hh.MAHH
                    WHERE ct.SOPHIEU = @soPhieu";

                DataTable dtDetail = DbHelper.Query(sqlDetail, DbHelper.Param("@soPhieu", soPhieu));

                string reportPath = "DoAnLapTrinhQuanLy.rptPhieuNhap.rdlc";

                FormInPhieu frmIn = new FormInPhieu(dtHeader, dtDetail, reportPath);
                frmIn.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chuẩn bị dữ liệu in: " + ex.Message);
            }
        }

        #endregion
    }
}