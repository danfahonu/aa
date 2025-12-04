using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Core;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public class BaseForm : Form
    {
        // P/Invoke for Resize/Drag
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        // Custom Title Bar Controls
        protected Panel pnlTitleBar;
        protected Label lblTitle;
        protected Button btnClose;
        protected Button btnMaximize;
        protected Button btnMinimize;

        public BaseForm()
        {
            // 1. Borderless & Performance
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();

            // 2. Initialize Title Bar
            InitializeTitleBar();
        }

        private void InitializeTitleBar()
        {
            pnlTitleBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(37, 37, 38) // Secondary Color
            };
            pnlTitleBar.MouseDown += PnlTitleBar_MouseDown;

            // Buttons
            btnClose = CreateTitleButton("✕", Color.FromArgb(232, 17, 35));
            btnClose.Click += (s, e) => this.Close();

            btnMaximize = CreateTitleButton("☐", Color.FromArgb(62, 62, 66));
            btnMaximize.Click += (s, e) =>
            {
                this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            };

            btnMinimize = CreateTitleButton("─", Color.FromArgb(62, 62, 66));
            btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            // Title Label
            lblTitle = new Label
            {
                Text = this.Text,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(10, 10)
            };

            // Add to Panel
            pnlTitleBar.Controls.Add(btnClose);
            pnlTitleBar.Controls.Add(btnMaximize);
            pnlTitleBar.Controls.Add(btnMinimize);
            pnlTitleBar.Controls.Add(lblTitle);

            this.Controls.Add(pnlTitleBar);
        }

        private Button CreateTitleButton(string text, Color hoverColor)
        {
            Button btn = new Button
            {
                Text = text,
                Dock = DockStyle.Right,
                Width = 45,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = hoverColor;
            return btn;
        }

        private void PnlTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (lblTitle != null) lblTitle.Text = this.Text;
        }

        // Optimized Rendering: Enable WS_EX_COMPOSITED
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        public bool UseCustomTitleBar { get; set; } = true;

        public void ConfigureAsEmbeddedContent()
        {
            UseCustomTitleBar = false;
            TopLevel = false;
            FormBorderStyle = FormBorderStyle.None;
            Dock = DockStyle.Fill;
            Padding = new Padding(0);
            Margin = new Padding(0);
        }
    }
}
