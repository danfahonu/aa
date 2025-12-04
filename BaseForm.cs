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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                try
                {
                    ThemeManager.Apply(this);
                    // Ensure TitleBar stays on top and styled
                    pnlTitleBar.SendToBack(); // Dock Top needs to be last in Z-order to be at top? No, usually first. 
                    // Actually, Dock=Top controls added last are at the bottom. 
                    // But we added it in Constructor, so it might be covered.
                    // Let's bring to front to be safe, or ensure it's the first control.
                    pnlTitleBar.BringToFront();
                }
                catch { }
            }
        }

        // Resize Logic (Native behavior for borderless forms)
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int RESIZE_HANDLE_SIZE = 10;

            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);

                if ((int)m.Result == 0x1) // HTCLIENT
                {
                    Point screenPoint = new Point(m.LParam.ToInt32());
                    Point clientPoint = this.PointToClient(screenPoint);

                    if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                    {
                        if (clientPoint.X <= RESIZE_HANDLE_SIZE) m.Result = (IntPtr)13; // HTTOPLEFT
                        else if (clientPoint.X < (this.Size.Width - RESIZE_HANDLE_SIZE)) m.Result = (IntPtr)12; // HTTOP
                        else m.Result = (IntPtr)14; // HTTOPRIGHT
                    }
                    else if (clientPoint.Y <= (this.Size.Height - RESIZE_HANDLE_SIZE))
                    {
                        if (clientPoint.X <= RESIZE_HANDLE_SIZE) m.Result = (IntPtr)10; // HTLEFT
                        else if (clientPoint.X < (this.Size.Width - RESIZE_HANDLE_SIZE)) m.Result = (IntPtr)2; // HTCAPTION (Default)
                        else m.Result = (IntPtr)11; // HTRIGHT
                    }
                    else
                    {
                        if (clientPoint.X <= RESIZE_HANDLE_SIZE) m.Result = (IntPtr)16; // HTBOTTOMLEFT
                        else if (clientPoint.X < (this.Size.Width - RESIZE_HANDLE_SIZE)) m.Result = (IntPtr)15; // HTBOTTOM
                        else m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    }
                }
                return;
            }
            base.WndProc(ref m);
        }
    }
}
