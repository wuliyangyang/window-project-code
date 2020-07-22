namespace DMTec.TestUIFramework.BasicForms
{
    partial class frmSplash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblLog = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLog
            // 
            this.lblLog.BackColor = System.Drawing.SystemColors.Control;
            this.lblLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLog.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lblLog.ForeColor = System.Drawing.Color.Blue;
            this.lblLog.Location = new System.Drawing.Point(0, 302);
            this.lblLog.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(1024, 466);
            this.lblLog.TabIndex = 4;
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DMTec.TestUIFramework.Properties.Resources.splashImage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.lblLog);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.Name = "frmSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSplash";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLog;

    }
}