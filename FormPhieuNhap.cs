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
using DoAnLapTrinhQuanLy.Data;
using System.Transactions; // <-- Thư viện này sẽ OK sau khi bà "Add Reference"

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormPhieuNhap : Form
    {
        private string _mode = "";
        private DataTable dtChiTiet;
        private long _idYeuCau = 0; // Biến để lưu ID của Yêu cầu gốc

        public FormPhieuNhap()
        {
            InitializeComponent();
        }

        private void FormPhieuNhap_Load(object sender, EventArgs e)
        {
            // (Bà tự thêm hàm Load CBO Nhà cung cấp ở đây nhé)
            // LoadNhaCungCap(); 
            StyleDataGridView(dgvDanhSach);
            StyleDataGridView(dgvChiTiet);

            SetInputMode(false);

            // Gán sự kiện cho các nút
            btnThem.Click += btnThem_Click;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;

            // Phiếu nhập đã lưu thì không Sửa/Xóa (theo logic ERP)
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // NÚT MỚI QUAN TRỌNG
            // Thêm nút này vào panel nút bấm
            Button btnChonYeuCau = new Button();
            btnChonYeuCau.Text = "Chọn YC đã duyệt";
            btnChonYeuCau.Name = "btnChonYeuCau";
            btnChonYeuCau.FlatStyle = FlatStyle.Flat;
            btnChonYeuCau.ForeColor = Color.Gainsboro;
            btnChonYeuCau.Location = new Point(330, 10); // Đặt cạnh nút Xóa
            btnChonYeuCau.Size = new System.Drawing.Size(130, 30);
            btnChonYeuCau.Click += btnChonYeuCau_Click;
            pnlActions.Controls.Add(btnChonYeuCau);
        }

        private void SetInputMode(bool mode)
        {
            btnThem.Enabled = !mode;
            // Tìm nút "btnChonYeuCau" và Enabled nó
            Button btnChon = this.pnlActions.Controls.Find("btnChonYeuCau", true).FirstOrDefault() as Button;
            if (btnChon != null)
            {
                btnChon.Enabled = !mode;
            }

            btnLuu.Enabled = mode;
            btnHuy.Enabled = mode;

            // Khóa các trường master
            dtpNgayLap.Enabled = mode;
            cboNhaCungCap.Enabled = mode;
            txtGhiChu.ReadOnly = !mode;

            // dgvChiTiet cho phép sửa (ví dụ sửa SL thực nhập)
            dgvChiTiet.ReadOnly = !mode;
            dgvChiTiet.AllowUserToAddRows = mode; // Cho phép thêm/xóa dòng
        }

        private void ClearInputs()
        {
            _idYeuCau = 0; // Reset ID
            txtSoPhieu.Text = "(auto)";
            dtpNgayLap.Value = DateTime.Now;
            // cboNhaCungCap.SelectedIndex = 0;
            txtGhiChu.Text = "";

            // Tạo bảng chi tiết rỗng
            dtChiTiet = new DataTable();
            dtChiTiet.Columns.Add("MAHH", typeof(string));
            dtChiTiet.Columns.Add("TENHH", typeof(string));
            dtChiTiet.Columns.Add("DVT", typeof(string));
            dtChiTiet.Columns.Add("SL", typeof(int));
            dtChiTiet.Columns.Add("DONGIA", typeof(decimal));

            dgvChiTiet.DataSource = dtChiTiet;
        }

        // NÚT THÊM (Tạo phiếu nhập tay, không cần YC)
        private void btnThem_Click(object sender, EventArgs e)
        {
            _mode = "add_manual";
            ClearInputs();
            SetInputMode(true);
        }

        // === LOGIC MỚI 1: CHỌN PHIẾU YÊU CẦU ===
        private void btnChonYeuCau_Click(object sender, EventArgs e)
        {
            // *** BÀ SẼ TẠO 1 FORM POPUP Ở ĐÂY ***
            // Form này query: SELECT * FROM PHIEU_YEUCAU_NHAPKHO WHERE TRANGTHAI = 1
            // Khi user chọn, nó trả về ID

            // Giả lập là ta chọn được phiếu YC có ID = 1002 (từ code cũ của bà)
            // Bà phải thay bằng Form Popup thật
            long sampleId = 1002; // ID này là ID PHIẾU YÊU CẦU
            MessageBox.Show($"Giả lập: Chọn Phiếu Yêu Cầu ID = {sampleId} (Đã duyệt)");
            LoadDataFromYeuCau(sampleId);
        }

        // === LOGIC MỚI 2: TẢI DATA TỪ YÊU CẦU SANG PHIẾU NHẬP ===
        private void LoadDataFromYeuCau(long idYeuCau)
        {
            try
            {
                _idYeuCau = idYeuCau; // LƯU LẠI ID YÊU CẦU GỐC

                // 2. Lấy thông tin Chi tiết của Yêu Cầu
                string queryDetail = @"
                    SELECT 
                        ct.MAHH, 
                        hh.TENHH, 
                        hh.DVT, 
                        ct.SOLUONG_YEUCAU AS SL,
                        hh.GIAVON AS DONGIA  -- Tạm lấy Giá vốn làm Đơn giá nhập
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

                dgvChiTiet.DataSource = dtChiTiet;

                _mode = "add_from_request";
                SetInputMode(true);
                MessageBox.Show("Đã tải dữ liệu từ Phiếu Yêu Cầu. Vui lòng kiểm tra lại Số lượng, Đơn giá và bấm Lưu.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ Phiếu Yêu Cầu: " + ex.Message);
            }
        }

        // === LOGIC LƯU (CODE GÂY LỖI HÔM TRƯỚC) ===
        private void btnLuu_Click(object sender, EventArgs e)
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
                using (var scope = new TransactionScope())
                {
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

                        // 2b. LƯU VÀO KHO_CHITIET_TONKHO (Đây là bước gây lỗi hôm trước)
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
                    // LoadDanhSachPhieuNhap(); // Bà gọi hàm tải lưới master ở đây
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nghiêm trọng khi lưu Phiếu Nhập: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
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
    }
}