using System;
using System.Reflection;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormThongTinPhanMem : Form
    {
        public FormThongTinPhanMem()
        {
            InitializeComponent();
        }

        private void FormThongTinPhanMem_Load(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ Assembly của ứng dụng
                Assembly assembly = Assembly.GetExecutingAssembly();

                // Lấy các thuộc tính để hiển thị
                var titleAttr = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTitleAttribute));
                var version = assembly.GetName().Version.ToString();
                var companyAttr = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute));

                // Gán thông tin vào các TextBox
                txtTitle.Text = titleAttr != null ? titleAttr.Title : "DoAnLapTrinhQuanLy";
                txtVersion.Text = version;
                txtAuthor.Text = companyAttr != null ? companyAttr.Company : "Your Company Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lấy thông tin phiên bản. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}