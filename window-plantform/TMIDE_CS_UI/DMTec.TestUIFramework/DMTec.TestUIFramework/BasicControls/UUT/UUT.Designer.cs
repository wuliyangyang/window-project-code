namespace DMTec.TestUIFramework.BasicControls
{
    partial class UUT
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lb_DutSN = new DMTec.TestUIFramework.BasicControls.AutoSizeFontLabel();
            this.asflbl_TestStatus = new DMTec.TestUIFramework.BasicControls.AutoSizeFontLabel();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.lbl_Number = new System.Windows.Forms.Label();
            this.tlp_Timer = new System.Windows.Forms.TableLayoutPanel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.tlp_Main.SuspendLayout();
            this.tlp_Timer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 3;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_Main.Controls.Add(this.progressBar, 0, 2);
            this.tlp_Main.Controls.Add(this.lb_DutSN, 1, 0);
            this.tlp_Main.Controls.Add(this.asflbl_TestStatus, 1, 1);
            this.tlp_Main.Controls.Add(this.checkBox, 0, 1);
            this.tlp_Main.Controls.Add(this.lbl_Number, 2, 0);
            this.tlp_Main.Controls.Add(this.tlp_Timer, 2, 1);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 4;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlp_Main.Size = new System.Drawing.Size(520, 137);
            this.tlp_Main.TabIndex = 0;
            // 
            // progressBar
            // 
            this.tlp_Main.SetColumnSpan(this.progressBar, 3);
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(5, 121);
            this.progressBar.Margin = new System.Windows.Forms.Padding(5, 1, 5, 1);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(510, 10);
            this.progressBar.TabIndex = 0;
            this.progressBar.Value = 1;
            // 
            // lb_DutSN
            // 
            this.lb_DutSN.AutoSize = true;
            this.lb_DutSN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_DutSN.FlexibleHeight = 25;
            this.lb_DutSN.Font = new System.Drawing.Font("微软雅黑", 13.63818F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lb_DutSN.Location = new System.Drawing.Point(22, 0);
            this.lb_DutSN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_DutSN.Name = "lb_DutSN";
            this.lb_DutSN.Size = new System.Drawing.Size(444, 25);
            this.lb_DutSN.TabIndex = 1;
            this.lb_DutSN.Text = "DutSN0123456789ABCDE";
            this.lb_DutSN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // asflbl_TestStatus
            // 
            this.asflbl_TestStatus.AutoSize = true;
            this.asflbl_TestStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.asflbl_TestStatus.FlexibleHeight = 93;
            this.asflbl_TestStatus.Font = new System.Drawing.Font("微软雅黑", 65.16019F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.asflbl_TestStatus.Location = new System.Drawing.Point(19, 26);
            this.asflbl_TestStatus.Margin = new System.Windows.Forms.Padding(1);
            this.asflbl_TestStatus.Name = "asflbl_TestStatus";
            this.asflbl_TestStatus.Size = new System.Drawing.Size(450, 93);
            this.asflbl_TestStatus.TabIndex = 2;
            this.asflbl_TestStatus.Text = "IDLE";
            this.asflbl_TestStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.BackColor = System.Drawing.Color.Transparent;
            this.checkBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBox.Checked = true;
            this.checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black;
            this.checkBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.checkBox.ForeColor = System.Drawing.Color.Black;
            this.checkBox.Location = new System.Drawing.Point(1, 26);
            this.checkBox.Margin = new System.Windows.Forms.Padding(1);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(16, 93);
            this.checkBox.TabIndex = 3;
            this.checkBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // lbl_Number
            // 
            this.lbl_Number.AutoSize = true;
            this.lbl_Number.BackColor = System.Drawing.SystemColors.Highlight;
            this.lbl_Number.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Number.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Number.Location = new System.Drawing.Point(470, 1);
            this.lbl_Number.Margin = new System.Windows.Forms.Padding(0, 1, 1, 0);
            this.lbl_Number.Name = "lbl_Number";
            this.lbl_Number.Size = new System.Drawing.Size(49, 24);
            this.lbl_Number.TabIndex = 4;
            this.lbl_Number.Text = "0";
            this.lbl_Number.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlp_Timer
            // 
            this.tlp_Timer.ColumnCount = 1;
            this.tlp_Timer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Timer.Controls.Add(this.lblTimer, 0, 0);
            this.tlp_Timer.Location = new System.Drawing.Point(470, 25);
            this.tlp_Timer.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Timer.Name = "tlp_Timer";
            this.tlp_Timer.RowCount = 2;
            this.tlp_Timer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.44444F));
            this.tlp_Timer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.55556F));
            this.tlp_Timer.Size = new System.Drawing.Size(50, 90);
            this.tlp_Timer.TabIndex = 5;
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTimer.Location = new System.Drawing.Point(3, 0);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(44, 57);
            this.lblTimer.TabIndex = 0;
            this.lblTimer.Text = "0.0";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // UUT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tlp_Main);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.Name = "UUT";
            this.Size = new System.Drawing.Size(520, 137);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_Main.PerformLayout();
            this.tlp_Timer.ResumeLayout(false);
            this.tlp_Timer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.ProgressBar progressBar;
        private AutoSizeFontLabel lb_DutSN;
        private AutoSizeFontLabel asflbl_TestStatus;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Label lbl_Number;
        private System.Windows.Forms.TableLayoutPanel tlp_Timer;
        private System.Windows.Forms.Label lblTimer;
    }
}
