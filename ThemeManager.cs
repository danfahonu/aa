using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.Core
{
    public static class ThemeManager
    {
        // Dark Mode Palette - VS Code Style
        public static Color PrimaryColor = ColorTranslator.FromHtml("#1E1E1E"); // Main Background
        public static Color SecondaryColor = ColorTranslator.FromHtml("#252526"); // Sidebar/Headers
        public static Color TextColor = ColorTranslator.FromHtml("#CCCCCC"); // Light Gray (Text)
        public static Color AccentColor = ColorTranslator.FromHtml("#007ACC"); // Blue (Buttons/Highlights)
        public static Color BorderColor = ColorTranslator.FromHtml("#3E3E42"); // Subtle dividers
        public static Color SelectionColor = ColorTranslator.FromHtml("#264F78"); // Grid Selection
        public static Color TextBoxBackground = ColorTranslator.FromHtml("#3C3C3C"); // Input Background

        // Semantic Colors
        public static Color SuccessColor = ColorTranslator.FromHtml("#4EC9B0"); // Greenish
        public static Color WarningColor = ColorTranslator.FromHtml("#DCDCAA"); // Yellowish
        public static Color DangerColor = ColorTranslator.FromHtml("#F44747"); // Reddish

        // Compatibility Aliases
        public static Color LightText => TextColor;
        public static Color Primary => SecondaryColor;
        public static Color Accent => AccentColor;
        public static Color Background => PrimaryColor;
        public static Color GridLineColor => BorderColor;

        public static Font BaseFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static Font HeaderFont = new Font("Segoe UI", 12F, FontStyle.Bold);

        public static void Apply(Form f)
        {
            if (f == null) return;

            f.SuspendLayout();
            try
            {
                f.BackColor = PrimaryColor;
                f.ForeColor = TextColor;
                f.Font = BaseFont;

                if (f.HasChildren)
                {
                    foreach (Control c in f.Controls)
                    {
                        ApplyRecursive(c);
                    }
                }
            }
            finally
            {
                f.ResumeLayout();
            }
        }

        private static void ApplyRecursive(Control c)
        {
            // 1. Base Text Styling
            c.ForeColor = TextColor;

            switch (c)
            {
                case Label _:
                case CheckBox _:
                case RadioButton _:
                case GroupBox _:
                    c.BackColor = Color.Transparent;
                    break;

                case DoAnLapTrinhQuanLy.Controls.ModernButton mBtn:
                    mBtn.BackColor = AccentColor;
                    mBtn.ForeColor = Color.White;
                    mBtn.BorderColor = BorderColor;
                    break;

                case DoAnLapTrinhQuanLy.Controls.MaterialTextBox mTxt:
                    mTxt.BackColor = TextBoxBackground;
                    mTxt.ForeColor = TextColor;
                    mTxt.BorderColor = BorderColor;
                    mTxt.BorderFocusColor = AccentColor;
                    mTxt.PlaceholderColor = Color.Gray;
                    break;

                case DoAnLapTrinhQuanLy.Controls.DarkTabControl dTab:
                    dTab.BackColor = PrimaryColor;
                    dTab.ForeColor = TextColor;
                    break;

                case Button btn:
                    if (btn.Parent != null && btn.Parent.Name == "pnlTitleBar") break;

                    btn.FlatStyle = FlatStyle.Flat;
                    btn.BackColor = AccentColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                    btn.Font = new Font(BaseFont, FontStyle.Bold);
                    break;

                case TextBox _:
                case ComboBox _:
                case DateTimePicker _:
                case NumericUpDown _:
                    c.BackColor = TextBoxBackground;
                    c.ForeColor = TextColor;
                    break;

                case DataGridView dgv:
                    StyleDataGridView(dgv);
                    break;

                case TableLayoutPanel tlp:
                    ThemeControlExtensions.SetDoubleBuffered(tlp, true);
                    break;

                case Panel p:
                    if (p is DoAnLapTrinhQuanLy.Controls.ModernPanel)
                    {
                        // Do nothing
                    }
                    else if (p.Name == "pnlTitleBar")
                    {
                        p.BackColor = SecondaryColor;
                    }
                    else if (p.Name.Contains("Header") || p.Name.Contains("Top") || p.Dock == DockStyle.Top)
                    {
                        p.BackColor = SecondaryColor;
                    }
                    else if (p.Name.Contains("Sidebar") || p.Name.Contains("Left") || p.Dock == DockStyle.Left)
                    {
                        p.BackColor = SecondaryColor;
                    }
                    else
                    {
                        p.BackColor = PrimaryColor;
                    }
                    ThemeControlExtensions.SetDoubleBuffered(p, true);
                    break;

                case MenuStrip menu:
                    menu.BackColor = SecondaryColor;
                    menu.ForeColor = TextColor;
                    menu.Renderer = new ToolStripProfessionalRenderer(new DarkThemeColorTable());
                    break;

                case StatusStrip status:
                    status.BackColor = AccentColor;
                    status.ForeColor = Color.White;
                    break;
            }

            // 2. Recursive Call
            if (c.HasChildren)
            {
                foreach (Control child in c.Controls)
                {
                    ApplyRecursive(child);
                }
            }
        }

        private static void StyleDataGridView(DataGridView dgv)
        {
            ThemeControlExtensions.SetDoubleBuffered(dgv, true);
            dgv.BackgroundColor = PrimaryColor;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = BorderColor;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Header
            dgv.ColumnHeadersDefaultCellStyle.BackColor = SecondaryColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextColor;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(BaseFont, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersHeight = 40;

            // Rows
            dgv.DefaultCellStyle.BackColor = PrimaryColor;
            dgv.DefaultCellStyle.ForeColor = TextColor;
            dgv.DefaultCellStyle.SelectionBackColor = SelectionColor;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4); // Spacious

            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 40; // Spacious

            // Alternating Rows
            dgv.AlternatingRowsDefaultCellStyle.BackColor = PrimaryColor;
        }

        private class DarkThemeColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected => Color.FromArgb(60, 60, 60);
            public override Color MenuItemBorder => Color.Transparent;
            public override Color MenuBorder => Color.FromArgb(60, 60, 60);
            public override Color MenuItemPressedGradientBegin => Color.FromArgb(40, 40, 40);
            public override Color MenuItemPressedGradientEnd => Color.FromArgb(40, 40, 40);
            public override Color ToolStripDropDownBackground => Color.FromArgb(30, 30, 30);
            public override Color ImageMarginGradientBegin => Color.FromArgb(30, 30, 30);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(30, 30, 30);
            public override Color ImageMarginGradientEnd => Color.FromArgb(30, 30, 30);
        }
    }
    public static class ThemeControlExtensions
    {
        public static void SetDoubleBuffered(this Control c, bool value)
        {
            // Use Reflection to access the protected 'DoubleBuffered' property
            System.Reflection.PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(c, value, null);
            }
        }
    }
}
