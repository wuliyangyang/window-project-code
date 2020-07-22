namespace DMTec.TestUIFramework.BasicControls
{
    partial class ChamberSimpleInfoPanel
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
            DMTec.TestUIFramework.DataModel.ChamberStatistics chamberStatistics1 = new DMTec.TestUIFramework.DataModel.ChamberStatistics();
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.statisticInfoPanel = new DMTec.TestUIFramework.BasicControls.StatisticInfoPanel();
            this.dut = new DMTec.TestUIFramework.BasicControls.DUT();
            this.flp_Top = new System.Windows.Forms.FlowLayoutPanel();
            this.chk_IsEnable = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_SocketID = new System.Windows.Forms.Label();
            this.tlp_Main.SuspendLayout();
            this.flp_Top.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.statisticInfoPanel, 0, 3);
            this.tlp_Main.Controls.Add(this.dut, 0, 2);
            this.tlp_Main.Controls.Add(this.flp_Top, 0, 0);
            this.tlp_Main.Controls.Add(this.lbl_SocketID, 0, 1);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 4;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tlp_Main.Size = new System.Drawing.Size(628, 324);
            this.tlp_Main.TabIndex = 2;
            // 
            // statisticInfoPanel
            // 
            this.statisticInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statisticInfoPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statisticInfoPanel.IsShowResetButton = true;
            this.statisticInfoPanel.Location = new System.Drawing.Point(1, 258);
            this.statisticInfoPanel.Margin = new System.Windows.Forms.Padding(1);
            this.statisticInfoPanel.Name = "statisticInfoPanel";
            this.statisticInfoPanel.PassedCount = "0";
            this.statisticInfoPanel.PassRate = "100.00%";
            this.statisticInfoPanel.Size = new System.Drawing.Size(626, 65);
            this.statisticInfoPanel.Statistics = chamberStatistics1;
            this.statisticInfoPanel.TabIndex = 0;
            this.statisticInfoPanel.TotalCount = "0";
            this.statisticInfoPanel.UntestCount = "0";
            // 
            // dut
            // 
            this.dut.BackColor = System.Drawing.Color.LightGray;
            this.dut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dut.DutBarcode = "DutSN012345678";
            this.dut.DUTColor = System.Drawing.Color.LightGray;
            this.dut.DutName = "DutSN012345678";
            this.dut.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.dut.Location = new System.Drawing.Point(3, 70);
            this.dut.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.dut.MinimumSize = new System.Drawing.Size(43, 80);
            this.dut.Name = "dut";
            this.dut.Size = new System.Drawing.Size(622, 187);
            this.dut.TabIndex = 1;
            // 
            // flp_Top
            // 
            this.flp_Top.Controls.Add(this.chk_IsEnable);
            this.flp_Top.Controls.Add(this.label1);
            this.flp_Top.Controls.Add(this.label2);
            this.flp_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_Top.Location = new System.Drawing.Point(1, 1);
            this.flp_Top.Margin = new System.Windows.Forms.Padding(1);
            this.flp_Top.Name = "flp_Top";
            this.flp_Top.Size = new System.Drawing.Size(626, 38);
            this.flp_Top.TabIndex = 2;
            // 
            // chk_IsEnable
            // 
            this.chk_IsEnable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chk_IsEnable.AutoSize = true;
            this.chk_IsEnable.Location = new System.Drawing.Point(3, 1);
            this.chk_IsEnable.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.chk_IsEnable.Name = "chk_IsEnable";
            this.chk_IsEnable.Size = new System.Drawing.Size(22, 22);
            this.chk_IsEnable.TabIndex = 0;
            this.chk_IsEnable.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label1.Location = new System.Drawing.Point(30, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Champer_X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "tcp://*.*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_SocketID
            // 
            this.lbl_SocketID.AutoSize = true;
            this.lbl_SocketID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_SocketID.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_SocketID.Location = new System.Drawing.Point(3, 40);
            this.lbl_SocketID.Name = "lbl_SocketID";
            this.lbl_SocketID.Size = new System.Drawing.Size(622, 30);
            this.lbl_SocketID.TabIndex = 3;
            this.lbl_SocketID.Text = "SocketSN";
            this.lbl_SocketID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChamberSimpleInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp_Main);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "ChamberSimpleInfoPanel";
            this.Size = new System.Drawing.Size(628, 324);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_Main.PerformLayout();
            this.flp_Top.ResumeLayout(false);
            this.flp_Top.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private StatisticInfoPanel statisticInfoPanel;
        private DUT dut;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.FlowLayoutPanel flp_Top;
        private System.Windows.Forms.CheckBox chk_IsEnable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_SocketID;
    }
}
