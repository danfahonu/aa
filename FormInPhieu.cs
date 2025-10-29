using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormInPhieu : Form
    {
        private DataTable _headerData;
        private DataTable _detailData;
        private string _reportPath;

        public FormInPhieu(DataTable headerData, DataTable detailData, string reportPath)
        {
            InitializeComponent();
            _headerData = headerData;
            _detailData = detailData;
            _reportPath = reportPath;
        }

        private void FormInPhieu_Load(object sender, EventArgs e)
        {
            try
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = _reportPath;

                ReportDataSource rdsHeader = new ReportDataSource();
                rdsHeader.Name = "dsPhieuNhapHeader"; // Tên này phải khớp với tên DataSet trong Report
                rdsHeader.Value = _headerData;

                ReportDataSource rdsDetail = new ReportDataSource();
                rdsDetail.Name = "dsPhieuNhapDetail"; // Tên này phải khớp với tên DataSet trong Report
                rdsDetail.Value = _detailData;

                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rdsHeader);
                this.reportViewer1.LocalReport.DataSources.Add(rdsDetail);

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message);
                this.Close();
            }
        }
    }
}