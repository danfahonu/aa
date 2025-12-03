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

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormPhieuNhap : Form
    {
        private string _mode = "";
        private long _idYeuCau = 0;
        private DataTable dtChiTiet;

        public FormPhieuNhap()
        {
            InitializeComponent();

            // Khởi tạo DataTable chi tiết
            dtChiTiet = new DataTable();
            dtChiTiet.Columns.Add("MAHH", typeof(string));
            dtChiTiet.Columns.Add("TENHH", typeof(string));
            dtChiTiet.Columns.Add("DVT", typeof(string));
            dtChiTiet.Columns.Add("SL", typeof(int));
            dtChiTiet.Columns.Add("DONGIA", typeof(decimal));
            dtChiTiet.Columns.Add("THANHTIEN", typeof(decimal), "SL * DONGIA");

            dgvChiTiet.DataSource = dtChiTiet;
        }

        private void FormPhieuNhap_Load(object sender, EventArgs e)
        {
            try
            {
                ThemeManager.Apply(this);
                LoadComboBoxNhaCungCap();
                LoadProductComboboxColumn();
                dgvChiTiet.CellValueChanged += DgvChiTiet_CellValueChanged;
                SetInputMode(false);
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải form: " + ex.Message);
            }
        }

        private void LoadComboBoxNhaCungCap()
        {
            string query = "SELECT MA_NCC, TEN_NCC FROM DM_NHACUNGCAP";
            DataTable dt = DbHelper.Query(query);
            cboNhaCungCap.DataSource = dt;
            cboNhaCungCap.DisplayMember = "TEN_NCC";
            cboNhaCungCap.ValueMember = "MA_NCC";
            cboNhaCungCap.SelectedIndex = -1;
        }

        private void SetInputMode(bool enable)
        {
            dtpNgayLap.Enabled = enable;
            cboNhaCungCap.Enabled = enable;
            txtGhiChu.Enabled = enable;
            dgvChiTiet.ReadOnly = !enable;

            btnThem.Enabled = !enable;
            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;
            btnChonYeuCau.Enabled = !enable; // Chỉ cho chọn khi không ở chế độ nhập liệu
        }

        private void ClearInputs()
        {
            dtpNgayLap.Value = DateTime.Now;
            cboNhaCungCap.SelectedIndex = -1;
            txtGhiChu.Text = "";
            dtChiTiet?.Clear();
            _idYeuCau = 0;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            _mode = "add_manual";
            ClearInputs();
            SetInputMode(true);
        }

        // === LOGIC MỚI 1: CHỌN PHIẾU YÊU CẦU ===
        private void BtnChonYeuCau_Click(object sender, EventArgs e)
        {
            // Giả lập là ta chọn được phiếu YC có ID = 1002
            long sampleId = 1002;
            MessageBox.Show($"Giả lập: Chọn Phiếu Yêu Cầu ID = {sampleId} (Đã duyệt)");
            LoadDataFromYeuCau(sampleId);
        }

        // === LOGIC MỚI 2: TẢI DATA TỪ YÊU CẦU SANG PHIẾU NHẬP ===
        private void LoadDataFromYeuCau(long idYeuCau)
        {
            try
            {
                _idYeuCau = idYeuCau;

                string queryDetail = @"
                    SELECT 
                        ct.MAHH, 
                        hh.TENHH, 
                        hh.DVT, 
                        ct.SOLUONG_YEUCAU AS SL,
                        hh.GIAVON AS DONGIA
                    FROM PHIEU_YEUCAU_NHAPKHO_CT ct
                    JOIN DM_HANGHOA hh ON ct.MAHH = hh.MAHH
                    WHERE ct.ID_YEUCAU = @ID";

                dtChiTiet = DbHelper.Query(queryDetail, DbHelper.Param("@ID", idYeuCau));

                if (dtChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show("Phiếu yêu cầu này không có hàng hóa.");
                    _idYeuCau = 0;
                    return;
                }

                // Thêm cột thành tiền nếu chưa có (vì query trên chưa có)
                if (!dtChiTiet.Columns.Contains("THANHTIEN"))
                {
                    dtChiTiet.Columns.Add("THANHTIEN", typeof(decimal), "SL * DONGIA");
                }

                dgvChiTiet.DataSource = dtChiTiet;

                _mode = "add_from_request";
                SetInputMode(true);

                // UX: Lock Product, Enable Quantity, Change Color
                dgvChiTiet.Columns["MAHH"].ReadOnly = true;
                dgvChiTiet.Columns["SL"].ReadOnly = false;
                dgvChiTiet.BackgroundColor = ColorTranslator.FromHtml("#3c3c3c"); // Dark Mode Highlight
                MessageBox.Show("Đã tải dữ liệu từ Phiếu Yêu Cầu. Vui lòng kiểm tra lại Số lượng, Đơn giá và bấm Lưu.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ Phiếu Yêu Cầu: " + ex.Message);
            }
        }

        // === LOGIC LƯU ===
        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_mode)) return;
            if (cboNhaCungCap.SelectedValue == null || string.IsNullOrEmpty(cboNhaCungCap.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn Nhà Cung Cấp!");
                return;
            }
            dgvChiTiet.EndEdit();

            // Lọc các dòng hợp lệ
            var validRows = (dgvChiTiet.DataSource as DataTable).AsEnumerable()
                .Where(r => r.RowState != DataRowState.Deleted &&
                            r["MAHH"] != DBNull.Value &&
                            !string.IsNullOrEmpty(r["MAHH"].ToString()))
                .ToList();

            if (validRows.Count == 0)
            {
                MessageBox.Show("Phiếu phải có ít nhất 1 dòng hàng hóa!");
                return;
            }

            try
            {
                using var scope = new TransactionScope();

                // --- BƯỚC 1: Lưu Master (Bảng PHIEU) ---
                string queryPhieu = @"
                    INSERT INTO PHIEU (NGAYLAP, LOAI, MA_NCC, GHICHU, TRANGTHAI, ID_YEUCAU)
                    VALUES (@NGAYLAP, 'N', @MA_NCC, @GHICHU, 1, @ID_YEUCAU);
                    SELECT SCOPE_IDENTITY();";

                long soPhieuMoi = Convert.ToInt64(DbHelper.ExecuteScalar(queryPhieu,
                    DbHelper.Param("@NGAYLAP", dtpNgayLap.Value.Date),
                    DbHelper.Param("@MA_NCC", cboNhaCungCap.SelectedValue.ToString()),
                    DbHelper.Param("@GHICHU", txtGhiChu.Text),
                    DbHelper.Param("@ID_YEUCAU", _idYeuCau > 0 ? (object)_idYeuCau : DBNull.Value)
                ));

                // --- BƯỚC 2: Lưu Detail (Loop qua DataGridView) ---
                foreach (DataRow row in validRows)
                {
                    string mahh = row["MAHH"].ToString();
                    int soLuong = Convert.ToInt32(row["SL"]);
                    decimal donGia = Convert.ToDecimal(row["DONGIA"]);

                    // 2a. LƯU VÀO PHIEU_CT
                    string queryPhieuCT = @"
                        INSERT INTO PHIEU_CT (SOPHIEU, MAHH, SL, DONGIA)
                        VALUES (@SOPHIEU, @MAHH, @SL, @DONGIA);
                        SELECT SCOPE_IDENTITY();";

                    long idPhieuCtMoi = Convert.ToInt64(DbHelper.ExecuteScalar(queryPhieuCT,
                        DbHelper.Param("@SOPHIEU", soPhieuMoi),
                        DbHelper.Param("@MAHH", mahh),
                        DbHelper.Param("@SL", soLuong),
                        DbHelper.Param("@DONGIA", donGia)
                    ));

                    // 2b. LƯU VÀO KHO_CHITIET_TONKHO
                    string queryKho = @"
                        INSERT INTO KHO_CHITIET_TONKHO (ID_PHIEUNHAP_CT, MAHH, NGAY_NHAP, SO_LUONG_NHAP, DON_GIA_NHAP, SO_LUONG_TON)
                        VALUES (@ID_PHIEUNHAP_CT, @MAHH, @NGAY_NHAP, @SO_LUONG, @DON_GIA, @SO_LUONG_TON)";

                    DbHelper.Execute(queryKho,
                        DbHelper.Param("@ID_PHIEUNHAP_CT", idPhieuCtMoi),
                        DbHelper.Param("@MAHH", mahh),
                        DbHelper.Param("@NGAY_NHAP", dtpNgayLap.Value.Date),
                        DbHelper.Param("@SO_LUONG", soLuong),
                        DbHelper.Param("@DON_GIA", donGia),
                        DbHelper.Param("@SO_LUONG_TON", soLuong)
                    );

                    // 2c. Cập nhật TỒN KHO trong DM_HANGHOA
                    UpdateTonKho(mahh, soLuong, true); // true = nhập kho
                }

                // --- BƯỚC 3: Cập nhật lại Yêu cầu gốc là "Đã nhập kho" (TRANGTHAI = 3) ---
                if (_idYeuCau > 0)
                {
                    DbHelper.Execute("UPDATE PHIEU_YEUCAU_NHAPKHO SET TRANGTHAI = 3 WHERE ID = @ID",
                        DbHelper.Param("@ID", _idYeuCau));
                }

                scope.Complete();

                MessageBox.Show("Đã lưu Phiếu Nhập kho và CẬP NHẬT KHO thành công!");
                _mode = "";
                SetInputMode(false);
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nghiêm trọng khi lưu Phiếu Nhập: " + ex.Message);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            _mode = "";
            ClearInputs();
            SetInputMode(false);
        }

        private void UpdateTonKho(string mahh, int soLuong, bool isNhapKho)
        {
            string op = isNhapKho ? "+" : "-";
            string query = $"UPDATE DM_HANGHOA SET TONKHO = TONKHO {op} @SL WHERE MAHH = @MAHH";
            DbHelper.Execute(query,
                DbHelper.Param("@SL", soLuong),
                DbHelper.Param("@MAHH", mahh)
            );
        }
        private void LoadProductComboboxColumn()
        {
            try
            {
                // Remove existing text column if it exists (auto-generated)
                if (dgvChiTiet.Columns.Contains("MAHH"))
                {
                    dgvChiTiet.Columns.Remove("MAHH");
                }

                DataGridViewComboBoxColumn cmbCol = new DataGridViewComboBoxColumn();
                cmbCol.HeaderText = "Hàng Hóa";
                cmbCol.Name = "MAHH";
                cmbCol.DataPropertyName = "MAHH"; // Bind to DataTable

                string query = "SELECT MAHH, TENHH FROM DM_HANGHOA";
                DataTable dt = DbHelper.Query(query);

                cmbCol.DataSource = dt;
                cmbCol.DisplayMember = "TENHH";
                cmbCol.ValueMember = "MAHH";
                cmbCol.Width = 200;
                cmbCol.AutoComplete = true;

                // Add to grid at first position
                dgvChiTiet.Columns.Insert(0, cmbCol);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách hàng hóa: " + ex.Message);
            }
        }

        private void DgvChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // If Product changed, auto-fill Unit and Price
            if (dgvChiTiet.Columns[e.ColumnIndex].Name == "MAHH")
            {
                var row = dgvChiTiet.Rows[e.RowIndex];
                var cellValue = row.Cells["MAHH"].Value;

                if (cellValue != null && cellValue != DBNull.Value)
                {
                    string mahh = cellValue.ToString();
                    string query = "SELECT DVT, GIAVON FROM DM_HANGHOA WHERE MAHH = @MAHH";
                    DataTable dt = DbHelper.Query(query, DbHelper.Param("@MAHH", mahh));

                    if (dt.Rows.Count > 0)
                    {
                        row.Cells["DVT"].Value = dt.Rows[0]["DVT"];
                        row.Cells["DONGIA"].Value = dt.Rows[0]["GIAVON"];
                        // Trigger calc for Total Amount
                        row.Cells["SL"].Value = row.Cells["SL"].Value ?? 1;
                    }
                }
            }
        }

    }
}