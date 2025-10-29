/* Nội dung file WindowsFormsApp2/FormAttach.Designer.cs */

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormAttach
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.grpNote = new System.Windows.Forms.GroupBox();
            this.lblNote1 = new System.Windows.Forms.Label();
            this.lblNote2 = new System.Windows.Forms.Label();
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.lblDb = new System.Windows.Forms.Label();
            this.txtDb = new System.Windows.Forms.TextBox();
            this.lblMdf = new System.Windows.Forms.Label();
            this.txtMdf = new System.Windows.Forms.TextBox();
            this.btnBrowseMdf = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnAttach = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.table.SuspendLayout();
            this.grpNote.SuspendLayout();
            this.grpInput.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Controls.Add(this.grpNote, 0, 0);
            this.table.Controls.Add(this.grpInput, 0, 1);
            this.table.Controls.Add(this.panelButtons, 0, 2);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.RowCount = 3;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.Size = new System.Drawing.Size(480, 225);
            this.table.TabIndex = 0;
            // 
            // grpNote
            // 
            this.grpNote.Controls.Add(this.lblNote1);
            this.grpNote.Controls.Add(this.lblNote2);
            this.grpNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpNote.Location = new System.Drawing.Point(0, 0);
            this.grpNote.Name = "grpNote";
            this.grpNote.Size = new System.Drawing.Size(171, 87);
            this.grpNote.TabIndex = 0;
            this.grpNote.TabStop = false;
            this.grpNote.Text = "Ghi chú";
            this.grpNote.Enter += new System.EventHandler(this.grpNote_Enter);
            // 
            // lblNote1
            // 
            this.lblNote1.AutoSize = true;
            this.lblNote1.Location = new System.Drawing.Point(10, 21);
            this.lblNote1.Name = "lblNote1";
            this.lblNote1.Size = new System.Drawing.Size(315, 13);
            this.lblNote1.TabIndex = 0;
            this.lblNote1.Text = "- File MDF là bắt buộc. Chọn đường dẫn đến tệp .mdf của CSDL.";
            this.lblNote1.Click += new System.EventHandler(this.lblNote1_Click);
            // 
            // lblNote2
            // 
            this.lblNote2.AutoSize = true;
            this.lblNote2.Location = new System.Drawing.Point(10, 39);
            this.lblNote2.Name = "lblNote2";
            this.lblNote2.Size = new System.Drawing.Size(401, 13);
            this.lblNote2.TabIndex = 1;
            this.lblNote2.Text = "- Nếu không có LDF, chương trình sẽ thử ATTACH_SINGLE_FILE hoặc rebuild log.";
            // 
            // grpInput
            // 
            this.grpInput.Controls.Add(this.lblDb);
            this.grpInput.Controls.Add(this.txtDb);
            this.grpInput.Controls.Add(this.lblMdf);
            this.grpInput.Controls.Add(this.txtMdf);
            this.grpInput.Controls.Add(this.btnBrowseMdf);
            this.grpInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInput.Location = new System.Drawing.Point(0, 0);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(171, 87);
            this.grpInput.TabIndex = 1;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Thông tin đính kèm CSDL";
            // 
            // lblDb
            // 
            this.lblDb.AutoSize = true;
            this.lblDb.Location = new System.Drawing.Point(10, 24);
            this.lblDb.Name = "lblDb";
            this.lblDb.Size = new System.Drawing.Size(57, 13);
            this.lblDb.TabIndex = 0;
            this.lblDb.Text = "Tên CSDL";
            // 
            // txtDb
            // 
            this.txtDb.Location = new System.Drawing.Point(86, 22);
            this.txtDb.Name = "txtDb";
            this.txtDb.Size = new System.Drawing.Size(369, 20);
            this.txtDb.TabIndex = 1;
            // 
            // lblMdf
            // 
            this.lblMdf.AutoSize = true;
            this.lblMdf.Location = new System.Drawing.Point(10, 54);
            this.lblMdf.Name = "lblMdf";
            this.lblMdf.Size = new System.Drawing.Size(86, 13);
            this.lblMdf.TabIndex = 2;
            this.lblMdf.Text = "Đường dẫn MDF";
            // 
            // txtMdf
            // 
            this.txtMdf.Location = new System.Drawing.Point(86, 51);
            this.txtMdf.Name = "txtMdf";
            this.txtMdf.ReadOnly = true;
            this.txtMdf.Size = new System.Drawing.Size(301, 20);
            this.txtMdf.TabIndex = 3;
            // 
            // btnBrowseMdf
            // 
            this.btnBrowseMdf.Location = new System.Drawing.Point(391, 50);
            this.btnBrowseMdf.Name = "btnBrowseMdf";
            this.btnBrowseMdf.Size = new System.Drawing.Size(63, 22);
            this.btnBrowseMdf.TabIndex = 4;
            this.btnBrowseMdf.Text = "Chọn...";
            this.btnBrowseMdf.Click += new System.EventHandler(this.btnBrowseMdf_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnAttach);
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(0, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(171, 87);
            this.panelButtons.TabIndex = 2;
            // 
            // btnAttach
            // 
            this.btnAttach.Location = new System.Drawing.Point(295, 7);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(75, 26);
            this.btnAttach.TabIndex = 0;
            this.btnAttach.Text = "Đồng ý";
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(377, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(77, 26);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormAttach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 225);
            this.Controls.Add(this.table);
            this.Name = "FormAttach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đính kèm (ATTACH) cơ sở dữ liệu";
            this.Load += new System.EventHandler(this.FormAttach_Load);
            this.table.ResumeLayout(false);
            this.grpNote.ResumeLayout(false);
            this.grpNote.PerformLayout();
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.GroupBox grpNote;
        private System.Windows.Forms.Label lblNote1;
        private System.Windows.Forms.Label lblNote2;
        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.Label lblDb;
        private System.Windows.Forms.TextBox txtDb;
        private System.Windows.Forms.Label lblMdf;
        private System.Windows.Forms.TextBox txtMdf;
        private System.Windows.Forms.Button btnBrowseMdf;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.Button btnClose;
    }
}