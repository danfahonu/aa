using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy
{
    public static class ThemeManager
    {
        // Dark Mode Palette
        public static Color PrimaryColor = ColorTranslator.FromHtml("#1e1e1e"); // Deep Dark Gray (Background)
        public static Color SecondaryColor = ColorTranslator.FromHtml("#252526"); // Lighter Dark Gray (Panels/Headers)
        public static Color TextColor = ColorTranslator.FromHtml("#d4d4d4"); // Light Gray (Text)
        public static Color AccentColor = ColorTranslator.FromHtml("#007acc"); // VS Blue (Buttons/Highlights)
        public static Color GridLineColor = ColorTranslator.FromHtml("#3e3e42"); // Grid Lines
        public static Color TextBoxBackground = ColorTranslator.FromHtml("#333333"); // TextBox Background

        // Compatibility aliases (if needed by existing code, though we should update them)
        public static Color Primary => SecondaryColor; // Map old Primary (Headers) to new Secondary
        public static Color Accent => AccentColor;
        public static Color Background => PrimaryColor;
        public static Color LightText => TextColor;

        public static Font BaseFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static Font HeaderFont = new Font("Segoe UI", 12F, FontStyle.Bold);

        public static void Apply(Form f)
        {
            f.BackColor = PrimaryColor;
            f.ForeColor = TextColor;
            f.Font = BaseFont;

            foreach (Control c in f.Controls)
            {
                ApplyRecursive(c);
            }
        }

        private static void ApplyRecursive(Control c)
        {
            // 1. Base Text Styling
            c.ForeColor = TextColor;
            if (c is Label || c is CheckBox || c is RadioButton || c is GroupBox)
            {
                c.BackColor = Color.Transparent; // Let parent color show through
            }

            // 2. Specific Control Styling
            if (c is Button btn)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = AccentColor;
                btn.ForeColor = Color.White; // Buttons always white text on blue
                btn.FlatAppearance.BorderSize = 0;
                btn.Cursor = Cursors.Hand;
                btn.Font = new Font(BaseFont, FontStyle.Bold);
            }
            else if (c is TextBox || c is ComboBox || c is DateTimePicker || c is NumericUpDown)
            {
                c.BackColor = TextBoxBackground;
                c.ForeColor = TextColor;
                // Note: Border style for standard controls is hard to change without custom painting, 
                // but setting BackColor helps.
            }
            else if (c is DataGridView dgv)
            {
                StyleDataGridView(dgv);
            }
            else if (c is Panel p)
            {
                if (p.Name.Contains("Header") || p.Name.Contains("Top") || p.Dock == DockStyle.Top)
                {
                    p.BackColor = SecondaryColor;
                }
                else
                {
                    p.BackColor = PrimaryColor;
                }
            }
            else if (c is MenuStrip menu)
            {
                menu.BackColor = SecondaryColor;
                menu.ForeColor = TextColor;
                menu.Renderer = new ToolStripProfessionalRenderer(new DarkThemeColorTable());
            }
            else if (c is StatusStrip status)
            {
                status.BackColor = AccentColor;
                status.ForeColor = Color.White;
            }

            // 3. Recursive Call
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
            dgv.BackgroundColor = PrimaryColor;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = GridLineColor;

            // Header
            dgv.ColumnHeadersDefaultCellStyle.BackColor = SecondaryColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextColor;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(BaseFont, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersHeight = 35;

            // Rows
            dgv.DefaultCellStyle.BackColor = PrimaryColor;
            dgv.DefaultCellStyle.ForeColor = TextColor;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(62, 62, 66); // VS Selection Gray
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Padding = new Padding(4);

            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 30;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = PrimaryColor; // No zebra or subtle zebra
        }

        // Helper class for MenuStrip coloring
        private class DarkThemeColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected => Color.FromArgb(62, 62, 66);
            public override Color MenuItemBorder => Color.Transparent;
            public override Color MenuBorder => Color.FromArgb(62, 62, 66);
            public override Color MenuItemPressedGradientBegin => Color.FromArgb(45, 45, 48);
            public override Color MenuItemPressedGradientEnd => Color.FromArgb(45, 45, 48);
            public override Color ToolStripDropDownBackground => Color.FromArgb(27, 27, 28);
            public override Color ImageMarginGradientBegin => Color.FromArgb(27, 27, 28);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(27, 27, 28);
            public override Color ImageMarginGradientEnd => Color.FromArgb(27, 27, 28);
        }
    }
}
