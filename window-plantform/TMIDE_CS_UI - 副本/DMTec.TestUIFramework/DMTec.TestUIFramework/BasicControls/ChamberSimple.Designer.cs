namespace DMTec.TestUIFramework.BasicControls
{
    partial class ChamberSimple
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
            this.lb_ChamberID = new System.Windows.Forms.Label();
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_Up = new System.Windows.Forms.TableLayoutPanel();
            this.lb_SeqSubAddress = new System.Windows.Forms.Label();
            this.ckb_IsEnable = new System.Windows.Forms.CheckBox();
            this.tlp_Middle = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_Statistics = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_PassRate = new System.Windows.Forms.Label();
            this.lb_Tested = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lb_Passed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_Untest = new System.Windows.Forms.Label();
            this.btn_ClearStatistics = new System.Windows.Forms.Button();
            this.lb_SocketSN = new System.Windows.Forms.Label();
            this.tlp_Main.SuspendLayout();
            this.tlp_Up.SuspendLayout();
            this.tlp_Middle.SuspendLayout();
            this.tlp_Statistics.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_ChamberID
            // 
            this.lb_ChamberID.BackColor = System.Drawing.Color.LightSeaGreen;
            this.lb_ChamberID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_ChamberID.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Bold);
            this.lb_ChamberID.Location = new System.Drawing.Point(20, 0);
            this.lb_ChamberID.Margin = new System.Windows.Forms.Padding(0);
            this.lb_ChamberID.Name = "lb_ChamberID";
            this.lb_ChamberID.Size = new System.Drawing.Size(222, 18);
            this.lb_ChamberID.TabIndex = 0;
            this.lb_ChamberID.Text = "C1";
            this.lb_ChamberID.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tlp_Main
            // 
            this.tlp_Main.BackColor = System.Drawing.Color.Transparent;
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.tlp_Up, 0, 0);
            this.tlp_Main.Controls.Add(this.tlp_Middle, 0, 1);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(1130, 328);
            this.tlp_Main.TabIndex = 5;
            // 
            // tlp_Up
            // 
            this.tlp_Up.ColumnCount = 3;
            this.tlp_Up.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_Up.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_Up.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlp_Up.Controls.Add(this.lb_SeqSubAddress, 2, 0);
            this.tlp_Up.Controls.Add(this.ckb_IsEnable, 0, 0);
            this.tlp_Up.Controls.Add(this.lb_ChamberID, 1, 0);
            this.tlp_Up.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Up.Location = new System.Drawing.Point(0, 0);
            this.tlp_Up.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Up.Name = "tlp_Up";
            this.tlp_Up.RowCount = 1;
            this.tlp_Up.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Up.Size = new System.Drawing.Size(1130, 18);
            this.tlp_Up.TabIndex = 0;
            // 
            // lb_SeqSubAddress
            // 
            this.lb_SeqSubAddress.AutoSize = true;
            this.lb_SeqSubAddress.BackColor = System.Drawing.Color.Transparent;
            this.lb_SeqSubAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_SeqSubAddress.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_SeqSubAddress.Location = new System.Drawing.Point(242, 0);
            this.lb_SeqSubAddress.Margin = new System.Windows.Forms.Padding(0);
            this.lb_SeqSubAddress.Name = "lb_SeqSubAddress";
            this.lb_SeqSubAddress.Size = new System.Drawing.Size(888, 18);
            this.lb_SeqSubAddress.TabIndex = 2;
            this.lb_SeqSubAddress.Text = "tcp://*.*";
            this.lb_SeqSubAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckb_IsEnable
            // 
            this.ckb_IsEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ckb_IsEnable.BackColor = System.Drawing.SystemColors.Control;
            this.ckb_IsEnable.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckb_IsEnable.Checked = true;
            this.ckb_IsEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_IsEnable.Location = new System.Drawing.Point(0, 0);
            this.ckb_IsEnable.Margin = new System.Windows.Forms.Padding(0);
            this.ckb_IsEnable.Name = "ckb_IsEnable";
            this.ckb_IsEnable.Size = new System.Drawing.Size(20, 18);
            this.ckb_IsEnable.TabIndex = 3;
            this.ckb_IsEnable.UseVisualStyleBackColor = false;
            this.ckb_IsEnable.Visible = false;
            this.ckb_IsEnable.CheckedChanged += new System.EventHandler(this.ckb_IsEnable_CheckedChanged);
            // 
            // tlp_Middle
            // 
            this.tlp_Middle.ColumnCount = 1;
            this.tlp_Middle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Middle.Controls.Add(this.tlp_Statistics, 0, 2);
            this.tlp_Middle.Controls.Add(this.lb_SocketSN, 0, 0);
            this.tlp_Middle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Middle.Location = new System.Drawing.Point(3, 21);
            this.tlp_Middle.Name = "tlp_Middle";
            this.tlp_Middle.RowCount = 3;
            this.tlp_Middle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlp_Middle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Middle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlp_Middle.Size = new System.Drawing.Size(1124, 304);
            this.tlp_Middle.TabIndex = 4;
            // 
            // tlp_Statistics
            // 
            this.tlp_Statistics.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlp_Statistics.ColumnCount = 5;
            this.tlp_Statistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Statistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Statistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Statistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Statistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 235F));
            this.tlp_Statistics.Controls.Add(this.label8, 3, 0);
            this.tlp_Statistics.Controls.Add(this.label1, 0, 0);
            this.tlp_Statistics.Controls.Add(this.lb_PassRate, 3, 1);
            this.tlp_Statistics.Controls.Add(this.lb_Tested, 0, 1);
            this.tlp_Statistics.Controls.Add(this.label7, 2, 0);
            this.tlp_Statistics.Controls.Add(this.lb_Passed, 2, 1);
            this.tlp_Statistics.Controls.Add(this.label2, 1, 0);
            this.tlp_Statistics.Controls.Add(this.lb_Untest, 1, 1);
            this.tlp_Statistics.Controls.Add(this.btn_ClearStatistics, 4, 0);
            this.tlp_Statistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Statistics.Location = new System.Drawing.Point(0, 274);
            this.tlp_Statistics.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Statistics.Name = "tlp_Statistics";
            this.tlp_Statistics.RowCount = 2;
            this.tlp_Statistics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.72414F));
            this.tlp_Statistics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.27586F));
            this.tlp_Statistics.Size = new System.Drawing.Size(1124, 30);
            this.tlp_Statistics.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.label8.Location = new System.Drawing.Point(664, 1);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(220, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "PassRate";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tested";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_PassRate
            // 
            this.lb_PassRate.AutoSize = true;
            this.lb_PassRate.BackColor = System.Drawing.Color.White;
            this.lb_PassRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_PassRate.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_PassRate.Location = new System.Drawing.Point(664, 15);
            this.lb_PassRate.Margin = new System.Windows.Forms.Padding(0);
            this.lb_PassRate.Name = "lb_PassRate";
            this.lb_PassRate.Size = new System.Drawing.Size(220, 14);
            this.lb_PassRate.TabIndex = 5;
            this.lb_PassRate.Text = "0%";
            this.lb_PassRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Tested
            // 
            this.lb_Tested.AutoSize = true;
            this.lb_Tested.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lb_Tested.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Tested.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_Tested.Location = new System.Drawing.Point(1, 15);
            this.lb_Tested.Margin = new System.Windows.Forms.Padding(0);
            this.lb_Tested.Name = "lb_Tested";
            this.lb_Tested.Size = new System.Drawing.Size(220, 14);
            this.lb_Tested.TabIndex = 3;
            this.lb_Tested.Text = "0";
            this.lb_Tested.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.label7.Location = new System.Drawing.Point(443, 1);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(220, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Passed";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Passed
            // 
            this.lb_Passed.AutoSize = true;
            this.lb_Passed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lb_Passed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Passed.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_Passed.Location = new System.Drawing.Point(443, 15);
            this.lb_Passed.Margin = new System.Windows.Forms.Padding(0);
            this.lb_Passed.Name = "lb_Passed";
            this.lb_Passed.Size = new System.Drawing.Size(220, 14);
            this.lb_Passed.TabIndex = 4;
            this.lb_Passed.Text = "0";
            this.lb_Passed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.label2.Location = new System.Drawing.Point(222, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Untest";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Untest
            // 
            this.lb_Untest.AutoSize = true;
            this.lb_Untest.BackColor = System.Drawing.Color.Transparent;
            this.lb_Untest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Untest.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_Untest.Location = new System.Drawing.Point(222, 15);
            this.lb_Untest.Margin = new System.Windows.Forms.Padding(0);
            this.lb_Untest.Name = "lb_Untest";
            this.lb_Untest.Size = new System.Drawing.Size(220, 14);
            this.lb_Untest.TabIndex = 6;
            this.lb_Untest.Text = "0";
            this.lb_Untest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_ClearStatistics
            // 
            this.btn_ClearStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ClearStatistics.Font = new System.Drawing.Font("Microsoft YaHei UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ClearStatistics.Location = new System.Drawing.Point(885, 1);
            this.btn_ClearStatistics.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ClearStatistics.Name = "btn_ClearStatistics";
            this.tlp_Statistics.SetRowSpan(this.btn_ClearStatistics, 2);
            this.btn_ClearStatistics.Size = new System.Drawing.Size(238, 28);
            this.btn_ClearStatistics.TabIndex = 7;
            this.btn_ClearStatistics.Text = "C";
            this.btn_ClearStatistics.UseVisualStyleBackColor = true;
            this.btn_ClearStatistics.Visible = false;
            this.btn_ClearStatistics.Click += new System.EventHandler(this.btn_ClearStatistics_Click);
            // 
            // lb_SocketSN
            // 
            this.lb_SocketSN.AutoSize = true;
            this.lb_SocketSN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_SocketSN.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_SocketSN.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lb_SocketSN.Location = new System.Drawing.Point(3, 0);
            this.lb_SocketSN.Name = "lb_SocketSN";
            this.lb_SocketSN.Size = new System.Drawing.Size(1118, 18);
            this.lb_SocketSN.TabIndex = 2;
            this.lb_SocketSN.Text = "N/A";
            this.lb_SocketSN.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ChamberSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tlp_Main);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MinimumSize = new System.Drawing.Size(35, 60);
            this.Name = "ChamberSimple";
            this.Size = new System.Drawing.Size(1130, 328);
            this.Load += new System.EventHandler(this.ChamberSimple_Load);
            this.Resize += new System.EventHandler(this.ChamberSimple_Resize);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_Up.ResumeLayout(false);
            this.tlp_Up.PerformLayout();
            this.tlp_Middle.ResumeLayout(false);
            this.tlp_Middle.PerformLayout();
            this.tlp_Statistics.ResumeLayout(false);
            this.tlp_Statistics.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_ChamberID;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.TableLayoutPanel tlp_Up;
        private System.Windows.Forms.TableLayoutPanel tlp_Middle;
        private System.Windows.Forms.Label lb_SeqSubAddress;
        private System.Windows.Forms.Label lb_SocketSN;
        private System.Windows.Forms.TableLayoutPanel tlp_Statistics;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lb_Tested;
        private System.Windows.Forms.Label lb_Passed;
        private System.Windows.Forms.Label lb_PassRate;
        private System.Windows.Forms.CheckBox ckb_IsEnable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_Untest;
        private System.Windows.Forms.Button btn_ClearStatistics;

    }
}
