namespace DoAnLapTrinhQuanLy.GuiLayer
{
    partial class FormTinhLuong
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblComingSoon = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblComingSoon
            // 
            this.lblComingSoon.AutoSize = true;
            this.lblComingSoon.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblComingSoon.ForeColor = System.Drawing.Color.Gray;
            this.lblComingSoon.Location = new System.Drawing.Point(400, 300); // Approximate center
            this.lblComingSoon.Name = "lblComingSoon";
            this.lblComingSoon.Size = new System.Drawing.Size(400, 37);
            this.lblComingSoon.TabIndex = 0;
            this.lblComingSoon.Text = "TÍNH NĂNG ĐANG PHÁT TRIỂN";
            this.lblComingSoon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormTinhLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.lblComingSoon);
            this.Name = "FormTinhLuong";
            this.Text = "Tính Lương";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblComingSoon;
    }
}