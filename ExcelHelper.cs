using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy
{
    public static class ExcelHelper
    {
        public static void ExportGridToCSV(DataGridView dgv, string filename)
        {
            try
            {
                var sb = new StringBuilder();

                // Headers
                var headers = new string[dgv.Columns.Count];
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    headers[i] = dgv.Columns[i].HeaderText;
                }
                sb.AppendLine(string.Join(",", headers));

                // Rows
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        var cells = new string[dgv.Columns.Count];
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            var value = row.Cells[i].Value?.ToString() ?? "";
                            // Escape commas and quotes
                            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                            {
                                value = $"\"{value.Replace("\"", "\"\"")}\"";
                            }
                            cells[i] = value;
                        }
                        sb.AppendLine(string.Join(",", cells));
                    }
                }

                File.WriteAllText(filename, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Xuất dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
