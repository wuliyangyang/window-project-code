namespace DMTec.TestSuite.CommonGUI.Forms
{
    partial class LogInWin
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
            this.Cancel = new System.Windows.Forms.Button();
            this.LogIn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Loop_Time = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Intetval = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Cancel
            // 
            this.Cancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cancel.Location = new System.Drawing.Point(286, 249);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(132, 45);
            this.Cancel.TabIndex = 26;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // LogIn
            // 
            this.LogIn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LogIn.Location = new System.Drawing.Point(92, 249);
            this.LogIn.Name = "LogIn";
            this.LogIn.Size = new System.Drawing.Size(132, 45);
            this.LogIn.TabIndex = 25;
            this.LogIn.Text = "LogIn";
            this.LogIn.UseVisualStyleBackColor = true;
            this.LogIn.Click += new System.EventHandler(this.LogIn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(205, 43);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(212, 35);
            this.textBox1.TabIndex = 22;
            // 
            // Loop_Time
            // 
            this.Loop_Time.AutoSize = true;
            this.Loop_Time.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Loop_Time.Location = new System.Drawing.Point(69, 46);
            this.Loop_Time.Name = "Loop_Time";
            this.Loop_Time.Size = new System.Drawing.Size(118, 24);
            this.Loop_Time.TabIndex = 21;
            this.Loop_Time.Text = "UserName:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(205, 107);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(212, 35);
            this.textBox2.TabIndex = 24;
            // 
            // Intetval
            // 
            this.Intetval.AutoSize = true;
            this.Intetval.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Intetval.Location = new System.Drawing.Point(69, 112);
            this.Intetval.Name = "Intetval";
            this.Intetval.Size = new System.Drawing.Size(118, 24);
            this.Intetval.TabIndex = 23;
            this.Intetval.Text = "PassWord:";
            // 
            // LogInWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 351);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.LogIn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Intetval);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Loop_Time);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LogInWin";
            this.Text = "LogInWin";
            this.Load += new System.EventHandler(this.LogInWin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button LogIn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Loop_Time;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label Intetval;
    }
}