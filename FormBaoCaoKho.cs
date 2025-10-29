using DoAnLapTrinhQuanLy.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormBaoCaoKho : Form
    {
        private enum ReportType { Nhap, Xuat }
        private ReportType _currentReportType;

        public FormBaoCaoKho(string reportType)
        {
            InitializeComponent();
            _currentReportType = (reportType.ToUpper() == "NHAP") ? ReportType.Nhap : ReportType.Xuat;
        }

        private void FormBaoCaoKho_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = dtpTuNgay.Value.AddMonths(1).AddDays(-1);
            LoadComboBoxDoiTac();
        }

        private void LoadComboBoxDoiTac()
        {
            DataTable dt;
            if (_currentReportType == ReportType.Nhap)
            {
                this.Text = "Báo Cáo Nhập Kho";
                lblDoiTac.Text = "Nhà Cung Cấp:";
                dt = DbHelper.Query("SELECT MA_NCC AS ID, TEN_NCC AS Name FROM dbo.DM_NHACUNGCAP ORDER BY TEN_NCC");
            }
            else // ReportType.Xuat
            {
                this.Text = "Báo Cáo Bán Hàng (Xuất Kho)";
                lblDoiTac.Text = "Khách Hàng:";
                dt = DbHelper.Query("SELECT MAKH AS ID, TENKH AS Name FROM dbo.DANHMUCKHACHHANG ORDER BY TENKH");
            }

            // *** SỬA LỖI TẠI ĐÂY: Cho phép cột ID nhận giá trị NULL ***
            dt.Columns["ID"].AllowDBNull = true;

            // Thêm dòng "Tất cả" vào đầu danh sách
            DataRow allRow = dt.NewRow();
            allRow["ID"] = DBNull.Value;
            allRow["Name"] = "-- Tất cả --";
            dt.Rows.InsertAt(allRow, 0);

            cboDoiTac.DataSource = dt;
            cboDoiTac.ValueMember = "ID";
            cboDoiTac.DisplayMember = "Name";
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                string doiTacJoinField = (_currentReportType == ReportType.Nhap) ? "p.MA_NCC" : "p.MAKH";
                string doiTacTable = (_currentReportType == ReportType.Nhap) ? "dbo.DM_NHACUNGCAP ncc" : "dbo.DANHMUCKHACHHANG kh";
                string doiTacSelectField = (_currentReportType == ReportType.Nhap) ? "ncc.TEN_NCC" : "kh.TENKH";
                string doiTacJoinCondition = (_currentReportType == ReportType.Nhap) ? "p.MA_NCC = ncc.MA_NCC" : "p.MAKH = kh.MAKH";
                char phieuLoai = (_currentReportType == ReportType.Nhap) ? 'N' : 'X';

                var parameters = new List<SqlParameter>
                {
                    DbHelper.Param("@TuNgay", dtpTuNgay.Value.Date),
                    DbHelper.Param("@DenNgay", dtpDenNgay.Value.Date)
                };

                if (chkXemChiTiet.Checked)
                {
                    sql = $@"
                        SELECT 
                            p.SOPHIEU AS [Số Phiếu], p.NGAYLAP AS [Ngày Lập],
                            {doiTacSelectField} AS [Đối Tác], hh.MAHH AS [Mã Hàng],
                            hh.TENHH AS [Tên Hàng Hóa], ct.SL AS [Số Lượng],
                            ct.DONGIA AS [Đơn Giá], ct.THANHTIEN AS [Thành Tiền]
                        FROM dbo.PHIEU p
                        JOIN dbo.PHIEU_CT ct ON p.SOPHIEU = ct.SOPHIEU
                        JOIN dbo.DM_HANGHOA hh ON ct.MAHH = hh.MAHH
                        JOIN {doiTacTable} ON {doiTacJoinCondition}
                        WHERE p.LOAI = '{phieuLoai}' AND p.NGAYLAP BETWEEN @TuNgay AND @DenNgay";
                }
                else
                {
                    sql = $@"
                        SELECT 
                            p.SOPHIEU AS [Số Phiếu], p.NGAYLAP AS [Ngày Lập],
                            {doiTacSelectField} AS [Đối Tác],
                            SUM(ct.THANHTIEN) AS [Tổng Tiền], p.GHICHU AS [Ghi Chú]
                        FROM dbo.PHIEU p
                        JOIN dbo.PHIEU_CT ct ON p.SOPHIEU = ct.SOPHIEU
                        JOIN {doiTacTable} ON {doiTacJoinCondition}
                        WHERE p.LOAI = '{phieuLoai}' AND p.NGAYLAP BETWEEN @TuNgay AND @DenNgay";
                }

                if (cboDoiTac.SelectedValue != DBNull.Value && cboDoiTac.SelectedValue != null)
                {
                    sql += $" AND {doiTacJoinField} = @MaDoiTac";
                    parameters.Add(DbHelper.Param("@MaDoiTac", cboDoiTac.SelectedValue));
                }

                if (chkXemChiTiet.Checked)
                {
                    sql += " ORDER BY p.NGAYLAP, p.SOPHIEU, hh.TENHH";
                }
                else
                {
                    sql += " GROUP BY p.SOPHIEU, p.NGAYLAP, " + doiTacSelectField + ", p.GHICHU ORDER BY p.NGAYLAP, p.SOPHIEU";
                }

                DataTable dtResult = DbHelper.Query(sql, parameters.ToArray());
                gridBaoCao.DataSource = dtResult;

                string colTongTien = chkXemChiTiet.Checked ? "Thành Tiền" : "Tổng Tiền";
                decimal tongTien = 0;
                foreach (DataRow row in dtResult.Rows)
                {
                    tongTien += Convert.ToDecimal(row[colTongTien]);
                }
                staTongTien.Text = $"Tổng cộng: {tongTien.ToString("N0", new CultureInfo("vi-VN"))} đ";

                FormatGrid(colTongTien);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid(string colTongTien)
        {
            if (gridBaoCao.Columns.Contains(colTongTien))
            {
                gridBaoCao.Columns[colTongTien].DefaultCellStyle.Format = "N0";
                gridBaoCao.Columns[colTongTien].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (gridBaoCao.Columns.Contains("Đơn Giá"))
            {
                gridBaoCao.Columns["Đơn Giá"].DefaultCellStyle.Format = "N0";
                gridBaoCao.Columns["Đơn Giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            gridBaoCao.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
    }
}