namespace DMTec.TestUIFramework.BasicControls
{
    partial class LogWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogWindow));
            this.tbCtrl_Log = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tb_Info = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tb_Warning = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tb_ErrorInfo = new System.Windows.Forms.TextBox();
            this.tbCtrl_Log.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbCtrl_Log
            // 
            this.tbCtrl_Log.Controls.Add(this.tabPage1);
            this.tbCtrl_Log.Controls.Add(this.tabPage2);
            this.tbCtrl_Log.Controls.Add(this.tabPage3);
            this.tbCtrl_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCtrl_Log.Location = new System.Drawing.Point(0, 0);
            this.tbCtrl_Log.Name = "tbCtrl_Log";
            this.tbCtrl_Log.SelectedIndex = 0;
            this.tbCtrl_Log.Size = new System.Drawing.Size(680, 323);
            this.tbCtrl_Log.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tb_Info);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(672, 286);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tb_Info
            // 
            this.tb_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Info.Location = new System.Drawing.Point(3, 3);
            this.tb_Info.Multiline = true;
            this.tb_Info.Name = "tb_Info";
            this.tb_Info.ReadOnly = true;
            this.tb_Info.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_Info.Size = new System.Drawing.Size(666, 280);
            this.tb_Info.TabIndex = 0;
            this.tb_Info.Text = "Log information...\r\n\r\n";
            this.tb_Info.TextChanged += new System.EventHandler(this.tb_Info_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tb_Warning);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(672, 286);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Warn";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tb_Warning
            // 
            this.tb_Warning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Warning.Location = new System.Drawing.Point(3, 3);
            this.tb_Warning.Multiline = true;
            this.tb_Warning.Name = "tb_Warning";
            this.tb_Warning.ReadOnly = true;
            this.tb_Warning.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_Warning.Size = new System.Drawing.Size(666, 280);
            this.tb_Warning.TabIndex = 1;
            this.tb_Warning.Text = "Warning information...\r\n\r\n";
            this.tb_Warning.TextChanged += new System.EventHandler(this.tb_Warning_TextChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tb_ErrorInfo);
            this.tabPage3.Location = new System.Drawing.Point(4, 33);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(672, 286);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Error";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tb_ErrorInfo
            // 
            this.tb_ErrorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_ErrorInfo.Location = new System.Drawing.Point(0, 0);
            this.tb_ErrorInfo.Multiline = true;
            this.tb_ErrorInfo.Name = "tb_ErrorInfo";
            this.tb_ErrorInfo.ReadOnly = true;
            this.tb_ErrorInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_ErrorInfo.Size = new System.Drawing.Size(672, 286);
            this.tb_ErrorInfo.TabIndex = 2;
            this.tb_ErrorInfo.Text = "Error information...\r\n\r\n";
            this.tb_ErrorInfo.TextChanged += new System.EventHandler(this.tb_ErrorInfo_TextChanged);
            // 
            // LogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 323);
            this.Controls.Add(this.tbCtrl_Log);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LogWindow";
            //this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogWindow";
            this.tbCtrl_Log.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbCtrl_Log;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tb_Info;
        private System.Windows.Forms.TextBox tb_Warning;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tb_ErrorInfo;
    }
}