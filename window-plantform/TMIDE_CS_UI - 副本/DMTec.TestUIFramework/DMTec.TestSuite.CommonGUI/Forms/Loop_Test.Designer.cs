namespace DMTec.TestSuite.CommonGUI
{
    partial class Loop_Test
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
            Hide();
            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            //base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flash = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.Curr_Time = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LoopOut = new System.Windows.Forms.Button();
            this.LoopIn = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Intetval = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Loop_Time = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // flash
            // 
            this.flash.AutoSize = true;
            this.flash.BackColor = System.Drawing.Color.Chartreuse;
            this.flash.Font = new System.Drawing.Font("宋体", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.flash.Location = new System.Drawing.Point(276, 34);
            this.flash.Name = "flash";
            this.flash.Size = new System.Drawing.Size(173, 43);
            this.flash.TabIndex = 20;
            this.flash.Text = "Testing";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(162, 165);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(99, 35);
            this.textBox3.TabIndex = 19;
            this.textBox3.Text = "0";
            // 
            // Curr_Time
            // 
            this.Curr_Time.AutoSize = true;
            this.Curr_Time.Location = new System.Drawing.Point(26, 168);
            this.Curr_Time.Name = "Curr_Time";
            this.Curr_Time.Size = new System.Drawing.Size(130, 24);
            this.Curr_Time.TabIndex = 18;
            this.Curr_Time.Text = "Curr Time:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(268, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 37);
            this.label1.TabIndex = 17;
            this.label1.Text = "ms";
            // 
            // LoopOut
            // 
            this.LoopOut.Location = new System.Drawing.Point(224, 274);
            this.LoopOut.Name = "LoopOut";
            this.LoopOut.Size = new System.Drawing.Size(132, 45);
            this.LoopOut.TabIndex = 16;
            this.LoopOut.Text = "loop out";
            this.LoopOut.UseVisualStyleBackColor = true;
            this.LoopOut.Click += new System.EventHandler(this.LoopOut_Click);
            // 
            // LoopIn
            // 
            this.LoopIn.Location = new System.Drawing.Point(30, 274);
            this.LoopIn.Name = "LoopIn";
            this.LoopIn.Size = new System.Drawing.Size(132, 45);
            this.LoopIn.TabIndex = 15;
            this.LoopIn.Text = "loop in";
            this.LoopIn.UseVisualStyleBackColor = true;
            this.LoopIn.Click += new System.EventHandler(this.LoopIn_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(162, 104);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(99, 35);
            this.textBox2.TabIndex = 14;
            this.textBox2.Text = "2000";
            // 
            // Intetval
            // 
            this.Intetval.AutoSize = true;
            this.Intetval.Location = new System.Drawing.Point(26, 109);
            this.Intetval.Name = "Intetval";
            this.Intetval.Size = new System.Drawing.Size(118, 24);
            this.Intetval.TabIndex = 13;
            this.Intetval.Text = "Intetval:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(162, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(99, 35);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "10";
            // 
            // Loop_Time
            // 
            this.Loop_Time.AutoSize = true;
            this.Loop_Time.Location = new System.Drawing.Point(26, 43);
            this.Loop_Time.Name = "Loop_Time";
            this.Loop_Time.Size = new System.Drawing.Size(130, 24);
            this.Loop_Time.TabIndex = 11;
            this.Loop_Time.Text = "Loop Time:";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Loop_Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 364);
            this.Controls.Add(this.flash);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.Curr_Time);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoopOut);
            this.Controls.Add(this.LoopIn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Intetval);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Loop_Time);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Loop_Test";
            this.Text = "Loop_Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label flash;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label Curr_Time;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button LoopOut;
        private System.Windows.Forms.Button LoopIn;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label Intetval;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Loop_Time;
        private System.Windows.Forms.Timer timer1;
    }
}