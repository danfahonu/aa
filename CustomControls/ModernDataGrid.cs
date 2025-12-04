using System;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Core;

namespace DoAnLapTrinhQuanLy.Controls
{
    public class ModernDataGrid : DataGridView
    {
        public ModernDataGrid()
        {
            this.DoubleBuffered = true;
            this.BackgroundColor = ThemeManager.PrimaryColor;
            this.BorderStyle = BorderStyle.None;
            this.EnableHeadersVisualStyles = false;
            this.GridColor = ThemeManager.BorderColor;
            this.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Header
            this.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.SecondaryColor;
            this.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            this.ColumnHeadersDefaultCellStyle.Font = new Font(ThemeManager.BaseFont, FontStyle.Bold);
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.ColumnHeadersHeight = 40;

            // Rows
            this.DefaultCellStyle.BackColor = ThemeManager.PrimaryColor;
            this.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
            this.DefaultCellStyle.SelectionBackColor = ThemeManager.SelectionColor;
            this.DefaultCellStyle.SelectionForeColor = Color.White;
            this.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);

            this.RowHeadersVisible = false;
            this.RowTemplate.Height = 40;

            // Alternating Rows
            this.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.PrimaryColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Custom painting if needed
        }
    }
}
