using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormPhieuThu : Form
    {
        private DataTable _dtNhanVien;
        private DataTable _dtDoiTuong;
        private DataTable _dtTaiKhoanKeToan;

        public FormPhieuThu() { InitializeComponent(); }

        private void FormPhieuThu_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLookups();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadLookups()
        {
            // Load Nhân viên
            _dtNhanVien = DbHelper.Query("SELECT MANV, HOTEN FROM dbo.NHANVIEN ORDER BY HOTEN");
            cboNhanVien.DataSource = _dtNhanVien;
            cboNhanVien.ValueMember = "MANV";
            cboNhanVien.DisplayMember = "HOTEN";

            // Load Đối tượng (Chỉ Khách hàng cho Phiếu Thu)
            _dtDoiTuong = DbHelper.Query("SELECT MAKH, TENKH FROM dbo.DANHMUCKHACHHANG ORDER BY TENKH");
            cboDoiTuong.DataSource = _dtDoiTuong;
            cboDoiTuong.ValueMember = "MAKH";
            cboDoiTuong.DisplayMember = "TENKH";

            // Load Tài khoản kế toán
            _dtTaiKhoanKeToan = DbHelper.Query("SELECT SOTK, TENTK FROM dbo.DM_TAIKHOANKETOAN ORDER BY SOTK");
            cboSoTkNo.DataSource = _dtTaiKhoanKeToan.Copy();
            cboSoTkNo.ValueMember = "SOTK";
            cboSoTkNo.DisplayMember = "TENTK";

            cboSoTkCo.DataSource = _dtTaiKhoanKeToan.Copy();
            cboSoTkCo.ValueMember = "SOTK";
            cboSoTkCo.DisplayMember = "TENTK";
        }

        private void ClearForm()
        {
            dtpNgayLap.Value = DateTime.Today;
            cboDoiTuong.SelectedIndex = -1;
            cboNhanVien.SelectedIndex = -1;
            numSoTien.Value = 0;
            txtLyDo.Text = "";
            cboSoTkNo.SelectedValue = "111"; // Mặc định Nợ TK Tiền mặt
            cboSoTkCo.SelectedValue = "131"; // Mặc định Có TK Phải thu KH
            dtpNgayLap.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboDoiTuong.SelectedValue == null) { MessageBox.Show("Vui lòng chọn Khách hàng.", "Cảnh báo"); return; }
            if (numSoTien.Value <= 0) { MessageBox.Show("Số tiền phải lớn hơn 0.", "Cảnh báo"); return; }

            try
            {
                // *** SỬA LỖI: Cập nhật câu lệnh INSERT để dùng cột MAKH ***
                string sqlInsert = "INSERT INTO dbo.PHIEUTHUCHI (NGAYLAP, LOAI, MAKH, SOTIEN, LYDO, MANV, SOTK_NO, SOTK_CO) VALUES (@NGAYLAP, 'T', @MAKH, @SOTIEN, @LYDO, @MANV, @SOTK_NO, @SOTK_CO)";
                DbHelper.Execute(sqlInsert,
                    DbHelper.Param("@NGAYLAP", dtpNgayLap.Value.Date),
                    DbHelper.Param("@MAKH", cboDoiTuong.SelectedValue), // Ghi vào cột MAKH
                    DbHelper.Param("@SOTIEN", numSoTien.Value),
                    DbHelper.Param("@LYDO", txtLyDo.Text.Trim()),
                    DbHelper.Param("@MANV", cboNhanVien.SelectedValue),
                    DbHelper.Param("@SOTK_NO", cboSoTkNo.SelectedValue),
                    DbHelper.Param("@SOTK_CO", cboSoTkCo.SelectedValue)
                );
                ClearForm();
                MessageBox.Show("Đã lưu phiếu thu mới thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e) => this.Close();
    }
}