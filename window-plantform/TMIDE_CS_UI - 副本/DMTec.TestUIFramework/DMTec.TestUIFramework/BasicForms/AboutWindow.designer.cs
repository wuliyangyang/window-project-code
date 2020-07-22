namespace DMTec.TestUIFramework.BasicControls
{
    partial class AboutWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
            this.lb_Version = new System.Windows.Forms.Label();
            this.lb_Copyright = new System.Windows.Forms.Label();
            this.picBox_Logo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_Version
            // 
            this.lb_Version.AutoSize = true;
            this.lb_Version.Location = new System.Drawing.Point(338, 65);
            this.lb_Version.Name = "lb_Version";
            this.lb_Version.Size = new System.Drawing.Size(48, 24);
            this.lb_Version.TabIndex = 0;
            this.lb_Version.Text = "V1.0";
            // 
            // lb_Copyright
            // 
            this.lb_Copyright.AutoSize = true;
            this.lb_Copyright.Location = new System.Drawing.Point(10, 111);
            this.lb_Copyright.Name = "lb_Copyright";
            this.lb_Copyright.Size = new System.Drawing.Size(413, 24);
            this.lb_Copyright.TabIndex = 1;
            this.lb_Copyright.Text = "CopyRight @ 2017年 CYGIA. All right reserved.";
            // 
            // picBox_Logo
            // 
            this.picBox_Logo.Image = ((System.Drawing.Image)(resources.GetObject("picBox_Logo.Image")));
            this.picBox_Logo.Location = new System.Drawing.Point(173, 3);
            this.picBox_Logo.Name = "picBox_Logo";
            this.picBox_Logo.Size = new System.Drawing.Size(250, 51);
            this.picBox_Logo.TabIndex = 2;
            this.picBox_Logo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(239, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "TestSuite";
            // 
            // AboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(423, 144);
            this.Controls.Add(this.picBox_Logo);
            this.Controls.Add(this.lb_Copyright);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_Version);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_Version;
        private System.Windows.Forms.Label lb_Copyright;
        private System.Windows.Forms.PictureBox picBox_Logo;
        private System.Windows.Forms.Label label1;
    }
}