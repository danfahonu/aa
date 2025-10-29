using DoAnLapTrinhQuanLy.Data; // Sử dụng DbHelper
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormBangBaoGia : Form
    {
        public FormBangBaoGia() { InitializeComponent(); }

        // --- Dữ liệu và trạng thái ---
        private enum FormMode { View, Edit, New }
        private FormMode _currentMode = FormMode.View;
        private Control[] _dataControls; // Các controls nhập liệu cần khóa/mở

        // --- Dynamic detection variables ---
        bool _hasPriceTable;
        string _tblPrice;
        string _colMaHH;
        string _colNgay;
        string _colDonGia;

        DataTable _dt;
        DataTable _hh;
        BindingSource _bs = new BindingSource();

        private void FormBangBaoGia_Load(object sender, EventArgs e)
        {
            // Định nghĩa các controls nhập liệu
            _dataControls = new Control[] { cboHang, dtpNgay, numGia };

            try
            {
                DetectPriceTable();
                LoadLookups();
                LoadData();
                DataBindingControl();
                SetFormMode(FormMode.View);

                // Khóa input nếu là chế độ Fallback (không có cột ngày)
                if (!_hasPriceTable)
                {
                    dtpNgay.Enabled = false;
                    cboHang.DropDownStyle = ComboBoxStyle.Simple;
                    cboHang.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================================================================================
        // LOGIC TẢI DỮ LIỆU VÀ BINDING
        // ===================================================================================

        void DetectPriceTable()
        {
            var tbls = DbHelper.Query("SELECT UPPER(TABLE_NAME) AS T FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'");
            var tnames = tbls.AsEnumerable().Select(r => Convert.ToString(r["T"])).ToList();
            string[] candidates = new[] { "BANGBAOGIA", "DM_BANGBAOGIA", "BAOGIA", "DM_GIABAN", "GIABAN" };
            _tblPrice = tnames.FirstOrDefault(t => candidates.Contains(t));
            if (!string.IsNullOrEmpty(_tblPrice))
            {
                var cols = DbHelper.Query("SELECT UPPER(COLUMN_NAME) C FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@T", DbHelper.Param("@T", _tblPrice));
                Func<string[], string> pick = keys => cols.AsEnumerable().Select(r => Convert.ToString(r["C"])).FirstOrDefault(c => keys.Contains(c));
                _colMaHH = pick(new[] { "MAHH", "MA_HANG", "MAHANG" });
                _colNgay = pick(new[] { "NGAY", "NGAY_APDUNG", "NGAYAPDUNG" });
                _colDonGia = pick(new[] { "DONGIA", "GIA", "GIABAN" });
                if (_colMaHH != null && _colNgay != null && _colDonGia != null)
                {
                    _hasPriceTable = true;
                    return;
                }
            }
            _hasPriceTable = false;
        }

        void LoadLookups()
        {
            // Tải danh mục hàng hóa
            _hh = DbHelper.Query("SELECT MAHH, TENHH FROM dbo.DM_HANGHOA ORDER BY TENHH");
            cboHang.DataSource = _hh;
            cboHang.ValueMember = "MAHH";
            cboHang.DisplayMember = "TENHH";
        }

        void LoadData()
        {
            if (_hasPriceTable)
            {
                // Chế độ giá theo ngày (từ bảng _tblPrice)
                _dt = DbHelper.Query($@"SELECT hh.TENHH, p.{_colNgay} AS NGAY, p.{_colDonGia} AS DONGIA, p.{_colMaHH} AS MAHH
                                  FROM dbo.{_tblPrice} p
                                  LEFT JOIN dbo.DM_HANGHOA hh ON hh.MAHH = p.{_colMaHH}
                                  ORDER BY hh.TENHH, p.{_colNgay} DESC");
            }
            else
            {
                // Fallback: show one row per item from DM_HANGHOA (chỉ có 1 giá duy nhất)
                _dt = DbHelper.Query(@"SELECT TENHH, CAST(GETDATE() AS date) AS NGAY, GIABAN AS DONGIA, MAHH FROM dbo.DM_HANGHOA ORDER BY TENHH");
            }
            _bs.DataSource = _dt;
            grid.DataSource = _bs;
            if (grid.Columns.Contains("MAHH")) grid.Columns["MAHH"].Visible = false;
        }

        private void DataBindingControl()
        {
            // Xóa binding cũ
            cboHang.DataBindings.Clear();
            dtpNgay.DataBindings.Clear();
            numGia.DataBindings.Clear();

            // Gán binding mới
            cboHang.DataBindings.Add("SelectedValue", _bs, "MAHH", true, DataSourceUpdateMode.Never);
            dtpNgay.DataBindings.Add("Value", _bs, "NGAY", true, DataSourceUpdateMode.Never);
            numGia.DataBindings.Add("Value", _bs, "DONGIA", true, DataSourceUpdateMode.Never);

            // Nếu có dữ liệu, chuyển về dòng đầu tiên
            if (_dt.Rows.Count > 0) _bs.Position = 0;
        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            if (_bs.Current == null) return;
            // Data Binding đã xử lý gán giá trị từ dòng được chọn vào controls

            // Đảm bảo trở về chế độ View khi chuyển dòng
            if (_currentMode != FormMode.View)
            {
                SetFormMode(FormMode.View);
            }
        }

        // ===================================================================================
        // LOGIC CHỨC NĂNG (MODE SWITCHING & CRUD)
        // ===================================================================================

        private void SetFormMode(FormMode mode)
        {
            _currentMode = mode;
            bool isEditing = (mode == FormMode.Edit || mode == FormMode.New);

            // Khóa/Mở các controls nhập liệu
            foreach (Control ctrl in _dataControls)
            {
                ctrl.Enabled = isEditing;
            }

            // Logic đặc thù:
            if (_hasPriceTable)
            {
                // Khi sửa, không được thay đổi Hàng Hóa (MAHH) và Ngày (NGAY)
                if (mode == FormMode.Edit)
                {
                    cboHang.Enabled = false;
                    dtpNgay.Enabled = false;
                }
            }
            else
            {
                // Chế độ Fallback chỉ cho phép sửa giá GIABAN (không có INSERT/DELETE)
                if (mode == FormMode.New) isEditing = false; // Ngăn chế độ New trong Fallback
                cboHang.Enabled = false; // Luôn khóa Hàng Hóa trong Fallback vì chỉ sửa trực tiếp
                dtpNgay.Enabled = false; // Luôn khóa Ngày trong Fallback
            }

            // Cập nhật trạng thái các nút
            btnMoi.Enabled = (mode == FormMode.View && _hasPriceTable); // Chỉ cho phép New nếu có bảng giá
            btnSua.Enabled = (mode == FormMode.View && _bs.Current != null);
            btnLuu.Enabled = isEditing;
            btnXoa.Enabled = (mode == FormMode.View && _bs.Current != null && _hasPriceTable);
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            if (!_hasPriceTable) return; // Không hỗ trợ New trong Fallback

            // Clear data for new entry
            cboHang.SelectedValue = null;
            dtpNgay.Value = DateTime.Today;
            numGia.Value = 0;

            SetFormMode(FormMode.New);
            cboHang.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_bs.Current == null) { MessageBox.Show("Vui lòng chọn dòng để sửa."); return; }
            SetFormMode(FormMode.Edit);
            numGia.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_currentMode == FormMode.View) return;

            var mahh = Convert.ToString(cboHang.SelectedValue);
            var ngay = dtpNgay.Value.Date;
            var gia = numGia.Value;

            if (string.IsNullOrEmpty(mahh)) { MessageBox.Show("Vui lòng chọn Hàng hóa.", "Cảnh báo"); return; }
            if (gia <= 0) { MessageBox.Show("Giá phải lớn hơn 0.", "Cảnh báo"); return; }

            try
            {
                if (_hasPriceTable)
                {
                    // Chế độ Bảng giá theo ngày
                    var existed = DbHelper.Scalar($@"SELECT 1 FROM dbo.{_tblPrice} WHERE {_colMaHH}=@MAHH AND CAST({_colNgay} AS date)=@NGAY",
                        DbHelper.Param("@MAHH", mahh), DbHelper.Param("@NGAY", ngay));

                    if (_currentMode == FormMode.Edit || (_currentMode == FormMode.New && existed != null))
                    {
                        // Update
                        DbHelper.Execute($@"UPDATE dbo.{_tblPrice} SET {_colDonGia}=@GIA WHERE {_colMaHH}=@MAHH AND CAST({_colNgay} AS date)=@NGAY",
                            DbHelper.Param("@MAHH", mahh), DbHelper.Param("@NGAY", ngay), DbHelper.Param("@GIA", gia));
                    }
                    else // Insert (New mode and not existed)
                    {
                        DbHelper.Execute($@"INSERT INTO dbo.{_tblPrice}({_colMaHH},{_colNgay},{_colDonGia}) VALUES(@MAHH,@NGAY,@GIA)",
                            DbHelper.Param("@MAHH", mahh), DbHelper.Param("@NGAY", ngay), DbHelper.Param("@GIA", gia));
                    }
                }
                else
                {
                    // Chế độ Fallback: Update GIABAN trong DM_HANGHOA
                    DbHelper.Execute(@"UPDATE dbo.DM_HANGHOA SET GIABAN=@GIA WHERE MAHH=@MAHH",
                        DbHelper.Param("@GIA", gia), DbHelper.Param("@MAHH", mahh));
                }

                LoadData();
                SetFormMode(FormMode.View);
                MessageBox.Show("Đã lưu giá thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu: " + ex.Message, "Lỗi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow == null) { MessageBox.Show("Chưa chọn dòng."); return; }
            var drv = _bs.Current as DataRowView;
            if (drv == null) return;
            var mahh = Convert.ToString(drv.Row["MAHH"]);
            var ngay = Convert.ToDateTime(drv.Row["NGAY"]).Date;

            if (!_hasPriceTable)
            {
                MessageBox.Show("Chế độ đơn giá theo DM_HANGHOA không hỗ trợ xóa (chỉ cập nhật).");
                return;
            }
            if (MessageBox.Show("Xóa giá của hàng đã chọn tại ngày áp dụng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                DbHelper.Execute($@"DELETE FROM dbo.{_tblPrice} WHERE {_colMaHH}=@MAHH AND CAST({_colNgay} AS date)=@NGAY",
                    DbHelper.Param("@MAHH", mahh), DbHelper.Param("@NGAY", ngay));

                LoadData();
                MessageBox.Show("Đã xóa.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa: " + ex.Message, "Lỗi");
            }
        }
    }
}