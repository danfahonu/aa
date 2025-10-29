/* Nội dung file WindowsFormsApp2/FormAttach.cs */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormAttach : Form
    {
        public FormAttach() { InitializeComponent(); }

        private void FormAttach_Load(object sender, EventArgs e)
        {
            // Pre-fill DB name from connection string if exists
            try
            {
                var c = ConfigurationManager.ConnectionStrings["Db"];
                if (c != null && !string.IsNullOrWhiteSpace(c.ConnectionString))
                {
                    var csb = new SqlConnectionStringBuilder(c.ConnectionString);
                    if (!string.IsNullOrEmpty(csb.InitialCatalog))
                        txtDb.Text = csb.InitialCatalog;
                }
            }
            catch { /* ignore */ }
        }

        private void btnBrowseMdf_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "SQL Server Database (*.mdf)|*.mdf|All files (*.*)|*.*";
                dlg.Title = "Chọn file .mdf để attach";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    txtMdf.Text = dlg.FileName;
                    if (string.IsNullOrWhiteSpace(txtDb.Text))
                    {
                        var name = Path.GetFileNameWithoutExtension(dlg.FileName);
                        // clean db name
                        name = Regex.Replace(name, @"[^A-Za-z0-9_\-]", "_");
                        txtDb.Text = name;
                    }
                }
            }
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            var dbName = txtDb.Text.Trim();
            var mdfPath = txtMdf.Text.Trim();
            if (string.IsNullOrWhiteSpace(dbName) || string.IsNullOrWhiteSpace(mdfPath))
            {
                MessageBox.Show("Vui lòng nhập Tên CSDL và chọn file MDF.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!File.Exists(mdfPath))
            {
                MessageBox.Show("Không tìm thấy file MDF.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Build connection to master from existing 'Db' string or a default to (local)\\SQLEXPRESS
            string masterCs = BuildMasterConnectionString();

            using (var conn = new SqlConnection(masterCs))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không kết nối được SQL Server: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Try attach using sp_attach_single_file_db (only MDF)
                try
                {
                    using (var cmd = new SqlCommand("EXEC sp_attach_single_file_db @dbname, @physname", conn))
                    {
                        cmd.Parameters.AddWithValue("@dbname", dbName);
                        cmd.Parameters.AddWithValue("@physname", mdfPath);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Attach CSDL thành công (sp_attach_single_file_db).", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                catch
                {
                    // ignore and try next strategy
                }

                // Try CREATE DATABASE ... FOR ATTACH
                try
                {
                    var sql = $"CREATE DATABASE [{dbName}] ON (FILENAME = @mdf) FOR ATTACH;";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@mdf", mdfPath);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Attach CSDL thành công (FOR ATTACH).", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                catch
                {
                    // ignore and try rebuild log
                }

                try
                {
                    var sql = $"CREATE DATABASE [{dbName}] ON (FILENAME = @mdf) FOR ATTACH_REBUILD_LOG;";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@mdf", mdfPath);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Attach CSDL thành công (REBUILD LOG).", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Attach thất bại.\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string BuildMasterConnectionString()
        {
            var c = ConfigurationManager.ConnectionStrings["Db"];
            if (c != null && !string.IsNullOrWhiteSpace(c.ConnectionString))
            {
                try
                {
                    var csb = new SqlConnectionStringBuilder(c.ConnectionString);
                    csb.InitialCatalog = "master";
                    // If Database keyword present under a different key, InitialCatalog covers it
                    return csb.ConnectionString;
                }
                catch { /* fall back */ }
            }
            // Fallback: Integrated security to local SQLEXPRESS
            return @"Server=.\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True";
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void grpNote_Enter(object sender, EventArgs e)
        {

        }

        private void lblNote1_Click(object sender, EventArgs e)
        {

        }
    }
}