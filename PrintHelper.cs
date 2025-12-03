using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public static class PrintHelper
    {
        public static void PrintDataGridView(DataGridView dgv, string title)
        {
            if (dgv == null || dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = true; // Default to Landscape for grids

            // Variables for pagination
            int currentRowIndex = 0;

            pd.PrintPage += (s, e) =>
            {
                Graphics g = e.Graphics;
                float y = e.MarginBounds.Top;
                float x = e.MarginBounds.Left;
                float tableWidth = e.MarginBounds.Width;
                float rowHeight = dgv.RowTemplate.Height + 5;

                // 1. Draw Title
                Font titleFont = new Font("Segoe UI", 16, FontStyle.Bold);
                SizeF titleSize = g.MeasureString(title, titleFont);
                g.DrawString(title, titleFont, Brushes.Black, x + (tableWidth - titleSize.Width) / 2, y);
                y += titleSize.Height + 10;

                // 2. Draw Date
                string dateStr = $"Ngày in: {DateTime.Now:dd/MM/yyyy HH:mm}";
                Font dateFont = new Font("Segoe UI", 10, FontStyle.Italic);
                g.DrawString(dateStr, dateFont, Brushes.Gray, x + tableWidth - g.MeasureString(dateStr, dateFont).Width, y);
                y += 20;

                // 3. Calculate Column Widths (Proportional)
                List<float> colWidths = new List<float>();
                List<DataGridViewColumn> visibleCols = new List<DataGridViewColumn>();

                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Visible) visibleCols.Add(col);
                }

                float totalGridWidth = 0;
                foreach (var col in visibleCols) totalGridWidth += col.Width;

                foreach (var col in visibleCols)
                {
                    colWidths.Add((col.Width / totalGridWidth) * tableWidth);
                }

                // 4. Draw Header
                float currentX = x;
                Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
                SolidBrush headerBrush = new SolidBrush(Color.FromArgb(44, 62, 80)); // Slate

                for (int i = 0; i < visibleCols.Count; i++)
                {
                    RectangleF rect = new RectangleF(currentX, y, colWidths[i], rowHeight);
                    g.FillRectangle(new SolidBrush(Color.LightGray), rect);
                    g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);

                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(visibleCols[i].HeaderText, headerFont, Brushes.Black, rect, sf);

                    currentX += colWidths[i];
                }
                y += rowHeight;

                // 5. Draw Rows
                Font rowFont = new Font("Segoe UI", 10);
                while (currentRowIndex < dgv.Rows.Count)
                {
                    DataGridViewRow row = dgv.Rows[currentRowIndex];
                    if (row.IsNewRow)
                    {
                        currentRowIndex++;
                        continue;
                    }

                    // Check if we need a new page
                    if (y + rowHeight > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        return; // Exit PrintPage to start new page
                    }

                    currentX = x;
                    for (int i = 0; i < visibleCols.Count; i++)
                    {
                        RectangleF rect = new RectangleF(currentX, y, colWidths[i], rowHeight);
                        g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);

                        string cellValue = row.Cells[visibleCols[i].Name].Value?.ToString() ?? "";
                        StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

                        // Align numbers to right
                        if (visibleCols[i].ValueType == typeof(decimal) || visibleCols[i].ValueType == typeof(int) || visibleCols[i].ValueType == typeof(double))
                        {
                            sf.Alignment = StringAlignment.Far;
                        }

                        g.DrawString(cellValue, rowFont, Brushes.Black, rect, sf);
                        currentX += colWidths[i];
                    }
                    y += rowHeight;
                    currentRowIndex++;
                }

                e.HasMorePages = false; // Done
            };

            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            ppd.WindowState = FormWindowState.Maximized;
            try
            {
                ppd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở khung xem trước in: " + ex.Message);
            }
        }

        public static void PrintInvoice(string title, Dictionary<string, string> info, DataGridView dgv)
        {
            if (dgv == null || dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = false; // Invoices are usually Portrait

            pd.PrintPage += (s, e) =>
            {
                Graphics g = e.Graphics;
                float y = e.MarginBounds.Top;
                float x = e.MarginBounds.Left;
                float width = e.MarginBounds.Width;

                Font headerFont = new Font("Segoe UI", 12, FontStyle.Bold);
                Font titleFont = new Font("Segoe UI", 20, FontStyle.Bold);
                Font infoFont = new Font("Segoe UI", 10);
                Font tableHeaderFont = new Font("Segoe UI", 10, FontStyle.Bold);
                Font tableFont = new Font("Segoe UI", 10);

                // 1. Header (Company Info)
                g.DrawString("STOREGEARVN", headerFont, Brushes.DarkSlateGray, x, y);
                y += 25;
                g.DrawString("Địa chỉ: 123 Đường ABC, Quận XYZ, TP.HCM", infoFont, Brushes.Gray, x, y);
                y += 20;
                g.DrawString("Hotline: 0909.123.456", infoFont, Brushes.Gray, x, y);
                y += 40;

                // 2. Title
                SizeF titleSize = g.MeasureString(title, titleFont);
                g.DrawString(title, titleFont, Brushes.Black, x + (width - titleSize.Width) / 2, y);
                y += 50;

                // 3. Info Section
                float infoX = x + 50;
                foreach (var pair in info)
                {
                    g.DrawString($"{pair.Key}: {pair.Value}", infoFont, Brushes.Black, infoX, y);
                    y += 25;
                }
                y += 20;

                // 4. Table Header
                float[] colWidths = { width * 0.4f, width * 0.15f, width * 0.15f, width * 0.3f }; // Name, Unit, Qty, Price
                string[] headers = { "Tên Hàng", "ĐVT", "SL", "Đơn Giá" }; // Simplified for now

                // If DGV has specific columns, try to map them. Otherwise use indices.
                // Mapping strategy: 
                // Col 0 or "TENHH" -> Name
                // Col "DVT" -> Unit
                // Col "SL" -> Qty
                // Col "DONGIA" -> Price

                float currentX = x;
                float rowHeight = 30;

                // Draw Header Background
                g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), x, y, width, rowHeight);
                g.DrawRectangle(Pens.Black, x, y, width, rowHeight);

                for (int i = 0; i < headers.Length; i++)
                {
                    g.DrawString(headers[i], tableHeaderFont, Brushes.Black, currentX + 5, y + 5);
                    currentX += colWidths[i];
                }
                y += rowHeight;

                // 5. Table Rows
                decimal totalAmount = 0;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;

                    string name = row.Cells["TENHH"].Value?.ToString() ?? "";
                    string unit = row.Cells["DVT"].Value?.ToString() ?? "";
                    string sl = row.Cells["SL"].Value?.ToString() ?? "0";
                    string price = row.Cells["DONGIA"].Value?.ToString() ?? "0";

                    decimal dSl = 0; decimal.TryParse(sl, out dSl);
                    decimal dPrice = 0; decimal.TryParse(price, out dPrice);
                    totalAmount += dSl * dPrice;

                    currentX = x;
                    string[] values = { name, unit, dSl.ToString("N0"), dPrice.ToString("N0") };

                    for (int i = 0; i < values.Length; i++)
                    {
                        g.DrawRectangle(Pens.Black, currentX, y, colWidths[i], rowHeight);
                        g.DrawString(values[i], tableFont, Brushes.Black, currentX + 5, y + 5);
                        currentX += colWidths[i];
                    }
                    y += rowHeight;
                }

                // 6. Footer (Total)
                y += 20;
                string totalStr = $"Tổng cộng: {totalAmount:N0} VNĐ";
                SizeF totalSize = g.MeasureString(totalStr, tableHeaderFont);
                g.DrawString(totalStr, tableHeaderFont, Brushes.Black, x + width - totalSize.Width, y);

                // 7. Signatures
                y += 60;
                g.DrawString("Người lập phiếu", tableHeaderFont, Brushes.Black, x + 50, y);
                g.DrawString("Người nhận hàng", tableHeaderFont, Brushes.Black, x + width - 200, y);

                y += 20;
                g.DrawString("(Ký, họ tên)", new Font("Segoe UI", 9, FontStyle.Italic), Brushes.Gray, x + 60, y);
                g.DrawString("(Ký, họ tên)", new Font("Segoe UI", 9, FontStyle.Italic), Brushes.Gray, x + width - 190, y);
            };

            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            ppd.WindowState = FormWindowState.Maximized;
            try
            {
                ppd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở khung xem trước in: " + ex.Message);
            }
        }
    }
}
