namespace DMTec.TestUIFramework.TestProjects
{
    partial class LoggerTextBox_Test
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
            this.loggerTextBox1 = new DMTec.TestUIFramework.BasicControls.LoggerTextBox();
            this.SuspendLayout();
            // 
            // loggerTextBox1
            // 
            this.loggerTextBox1.BackColor = System.Drawing.Color.Black;
            this.loggerTextBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.loggerTextBox1.ForeColor = System.Drawing.Color.White;
            this.loggerTextBox1.IsOutputTimeStamp = false;
            this.loggerTextBox1.Location = new System.Drawing.Point(12, 122);
            this.loggerTextBox1.Name = "loggerTextBox1";
            this.loggerTextBox1.ReadOnly = true;
            this.loggerTextBox1.Size = new System.Drawing.Size(1391, 838);
            this.loggerTextBox1.TabIndex = 0;
            this.loggerTextBox1.Text = "";
            // 
            // LoggerTextBoxTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1415, 972);
            this.Controls.Add(this.loggerTextBox1);
            this.Name = "LoggerTextBoxTest";
            this.Text = "LoggerTextBoxTest";
            this.Load += new System.EventHandler(this.LoggerTextBoxTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BasicControls.LoggerTextBox loggerTextBox1;
    }
}