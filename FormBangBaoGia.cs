using System;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormBangBaoGia : Form
    {
        public FormBangBaoGia()
        {
            InitializeComponent();
            this.Load += FormBangBaoGia_Load;

            this.Resize += (s, e) =>
            {
                if (lblComingSoon != null)
                {
                    lblComingSoon.Left = (this.ClientSize.Width - lblComingSoon.Width) / 2;
                    lblComingSoon.Top = (this.ClientSize.Height - lblComingSoon.Height) / 2;
                }
            };
        }

        private void FormBangBaoGia_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng này đang được xây dựng. Vui lòng quay lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}