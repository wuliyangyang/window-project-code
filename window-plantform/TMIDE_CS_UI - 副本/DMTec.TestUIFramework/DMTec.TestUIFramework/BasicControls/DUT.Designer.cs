namespace DMTec.TestUIFramework.BasicControls
{
    partial class DUT
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.lb_DutSN = new DMTec.TestUIFramework.BasicControls.AutoSizeFontLabel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lb_DutStatus = new DMTec.TestUIFramework.BasicControls.AutoSizeFontLabel();
            this.lb_DutName = new System.Windows.Forms.Label();
            this.lb_Counter = new System.Windows.Forms.Label();
            this.tlp_Up = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_Main.SuspendLayout();
            this.tlp_Up.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.lb_DutSN, 0, 0);
            this.tlp_Main.Controls.Add(this.progressBar, 0, 2);
            this.tlp_Main.Controls.Add(this.lb_DutStatus, 0, 1);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 3;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_Main.Size = new System.Drawing.Size(625, 332);
            this.tlp_Main.TabIndex = 4;
            // 
            // lb_DutSN
            // 
            this.lb_DutSN.AutoSize = true;
            this.lb_DutSN.BackColor = System.Drawing.Color.Transparent;
            this.lb_DutSN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_DutSN.FlexibleHeight = 90;
            this.lb_DutSN.Font = new System.Drawing.Font("Microsoft YaHei UI", 65.35332F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lb_DutSN.ForeColor = System.Drawing.Color.Blue;
            this.lb_DutSN.Location = new System.Drawing.Point(3, 3);
            this.lb_DutSN.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lb_DutSN.Name = "lb_DutSN";
            this.lb_DutSN.Size = new System.Drawing.Size(619, 90);
            this.lb_DutSN.TabIndex = 0;
            this.lb_DutSN.Text = "DutSN";
            this.lb_DutSN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_DutSN.Resize += new System.EventHandler(this.lb_DutSN_Resize);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(5, 312);
            this.progressBar.Margin = new System.Windows.Forms.Padding(5, 1, 5, 6);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(615, 14);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 3;
            this.progressBar.Value = 1;
            // 
            // lb_DutStatus
            // 
            this.lb_DutStatus.AutoSize = true;
            this.lb_DutStatus.BackColor = System.Drawing.Color.Transparent;
            this.lb_DutStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_DutStatus.FlexibleHeight = 212;
            this.lb_DutStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 161.4148F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lb_DutStatus.ForeColor = System.Drawing.Color.Black;
            this.lb_DutStatus.Location = new System.Drawing.Point(3, 96);
            this.lb_DutStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lb_DutStatus.Name = "lb_DutStatus";
            this.lb_DutStatus.Size = new System.Drawing.Size(619, 212);
            this.lb_DutStatus.TabIndex = 1;
            this.lb_DutStatus.Text = "IDLE";
            this.lb_DutStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_DutStatus.Resize += new System.EventHandler(this.lb_DutStatus_Resize);
            // 
            // lb_DutName
            // 
            this.lb_DutName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_DutName.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_DutName.Location = new System.Drawing.Point(3, 0);
            this.lb_DutName.Name = "lb_DutName";
            this.lb_DutName.Size = new System.Drawing.Size(74, 25);
            this.lb_DutName.TabIndex = 0;
            this.lb_DutName.Text = "DUT ";
            this.lb_DutName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_Counter
            // 
            this.lb_Counter.BackColor = System.Drawing.Color.White;
            this.lb_Counter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_Counter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Counter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lb_Counter.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_Counter.Location = new System.Drawing.Point(524, 2);
            this.lb_Counter.Margin = new System.Windows.Forms.Padding(0, 2, 2, 0);
            this.lb_Counter.Name = "lb_Counter";
            this.lb_Counter.Size = new System.Drawing.Size(23, 23);
            this.lb_Counter.TabIndex = 1;
            this.lb_Counter.Text = "1";
            this.lb_Counter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Counter.Visible = false;
            // 
            // tlp_Up
            // 
            this.tlp_Up.ColumnCount = 3;
            this.tlp_Up.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlp_Up.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Up.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlp_Up.Controls.Add(this.lb_Counter, 2, 0);
            this.tlp_Up.Controls.Add(this.lb_DutName, 0, 0);
            this.tlp_Up.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tlp_Up.Location = new System.Drawing.Point(712, 754);
            this.tlp_Up.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Up.Name = "tlp_Up";
            this.tlp_Up.RowCount = 1;
            this.tlp_Up.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Up.Size = new System.Drawing.Size(549, 25);
            this.tlp_Up.TabIndex = 0;
            this.tlp_Up.Visible = false;
            // 
            // DUT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.tlp_Main);
            this.Controls.Add(this.tlp_Up);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(35, 60);
            this.Name = "DUT";
            this.Size = new System.Drawing.Size(625, 332);
            this.Load += new System.EventHandler(this.DUT_Load);
            this.Resize += new System.EventHandler(this.DUT_Resize);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_Main.PerformLayout();
            this.tlp_Up.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private AutoSizeFontLabel lb_DutSN;
        private AutoSizeFontLabel lb_DutStatus;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lb_DutName;
        private System.Windows.Forms.Label lb_Counter;
        private System.Windows.Forms.TableLayoutPanel tlp_Up;
    }
}
