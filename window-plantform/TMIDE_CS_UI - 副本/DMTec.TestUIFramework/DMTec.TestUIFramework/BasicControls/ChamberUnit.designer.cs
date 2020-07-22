namespace DMTec.TestUIFramework.BasicControls
{
    partial class ChamberUnit
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
            this.tbl_Main = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.myItemsList = new DMTec.TestUIFramework.BasicControls.SequenceItemsLister();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtb_LogWindow = new System.Windows.Forms.RichTextBox();
            this.tlp_Info = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label_DPH = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lable_ExtTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_DRTPMonitorSn = new System.Windows.Forms.Label();
            this.chamberSimple = new DMTec.TestUIFramework.BasicControls.ChamberSimple();
            this.tlp_Lable = new System.Windows.Forms.TableLayoutPanel();
            this.lb_SMHB_LED = new System.Windows.Forms.Label();
            this.lb_SeqHB_LED = new System.Windows.Forms.Label();
            this.lb_RTMonitorSn = new System.Windows.Forms.Label();
            this.tbl_Main.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tlp_Info.SuspendLayout();
            this.tlp_Lable.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbl_Main
            // 
            this.tbl_Main.ColumnCount = 1;
            this.tbl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_Main.Controls.Add(this.tabControl, 0, 3);
            this.tbl_Main.Controls.Add(this.tlp_Info, 0, 4);
            this.tbl_Main.Controls.Add(this.lb_DRTPMonitorSn, 0, 2);
            this.tbl_Main.Controls.Add(this.chamberSimple, 0, 1);
            this.tbl_Main.Controls.Add(this.tlp_Lable, 0, 0);
            this.tbl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_Main.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbl_Main.Location = new System.Drawing.Point(0, 0);
            this.tbl_Main.Margin = new System.Windows.Forms.Padding(2);
            this.tbl_Main.Name = "tbl_Main";
            this.tbl_Main.RowCount = 5;
            this.tbl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tbl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tbl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tbl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tbl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tbl_Main.Size = new System.Drawing.Size(878, 974);
            this.tbl_Main.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(2, 413);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(874, 531);
            this.tabControl.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.myItemsList);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Size = new System.Drawing.Size(866, 494);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TestItemList";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // myItemsList
            // 
            this.myItemsList.ChamberID = null;
            this.myItemsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myItemsList.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myItemsList.IsConsoleEnable = false;
            this.myItemsList.Location = new System.Drawing.Point(2, 3);
            this.myItemsList.Margin = new System.Windows.Forms.Padding(0);
            this.myItemsList.Name = "myItemsList";
            this.myItemsList.Size = new System.Drawing.Size(862, 488);
            this.myItemsList.TabIndex = 10;
            this.myItemsList.TesterID = null;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtb_LogWindow);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage2.Size = new System.Drawing.Size(866, 494);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "LogWindow";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtb_LogWindow
            // 
            this.rtb_LogWindow.BackColor = System.Drawing.Color.Silver;
            this.rtb_LogWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_LogWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_LogWindow.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtb_LogWindow.Location = new System.Drawing.Point(2, 3);
            this.rtb_LogWindow.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rtb_LogWindow.Name = "rtb_LogWindow";
            this.rtb_LogWindow.ReadOnly = true;
            this.rtb_LogWindow.Size = new System.Drawing.Size(862, 488);
            this.rtb_LogWindow.TabIndex = 0;
            this.rtb_LogWindow.Text = "This is log window...";
            // 
            // tlp_Info
            // 
            this.tlp_Info.ColumnCount = 5;
            this.tlp_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlp_Info.Controls.Add(this.label1, 0, 0);
            this.tlp_Info.Controls.Add(this.label_DPH, 1, 0);
            this.tlp_Info.Controls.Add(this.label3, 2, 0);
            this.tlp_Info.Controls.Add(this.lable_ExtTime, 3, 0);
            this.tlp_Info.Controls.Add(this.label5, 4, 0);
            this.tlp_Info.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.tlp_Info.Location = new System.Drawing.Point(0, 947);
            this.tlp_Info.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Info.Name = "tlp_Info";
            this.tlp_Info.RowCount = 1;
            this.tlp_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Info.Size = new System.Drawing.Size(376, 26);
            this.tlp_Info.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "DPH:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // label_DPH
            // 
            this.label_DPH.AutoSize = true;
            this.label_DPH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_DPH.Location = new System.Drawing.Point(86, 0);
            this.label_DPH.Margin = new System.Windows.Forms.Padding(0);
            this.label_DPH.Name = "label_DPH";
            this.label_DPH.Size = new System.Drawing.Size(86, 26);
            this.label_DPH.TabIndex = 0;
            this.label_DPH.Text = "0";
            this.label_DPH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_DPH.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(172, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "ExtTime:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lable_ExtTime
            // 
            this.lable_ExtTime.AutoSize = true;
            this.lable_ExtTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lable_ExtTime.Location = new System.Drawing.Point(258, 0);
            this.lable_ExtTime.Margin = new System.Windows.Forms.Padding(0);
            this.lable_ExtTime.Name = "lable_ExtTime";
            this.lable_ExtTime.Size = new System.Drawing.Size(86, 26);
            this.lable_ExtTime.TabIndex = 0;
            this.lable_ExtTime.Text = "0";
            this.lable_ExtTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(344, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 26);
            this.label5.TabIndex = 1;
            this.label5.Text = "ms";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_DRTPMonitorSn
            // 
            this.lb_DRTPMonitorSn.AutoSize = true;
            this.lb_DRTPMonitorSn.BackColor = System.Drawing.Color.Transparent;
            this.lb_DRTPMonitorSn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_DRTPMonitorSn.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lb_DRTPMonitorSn.ForeColor = System.Drawing.Color.Blue;
            this.lb_DRTPMonitorSn.Location = new System.Drawing.Point(2, 386);
            this.lb_DRTPMonitorSn.Margin = new System.Windows.Forms.Padding(2);
            this.lb_DRTPMonitorSn.Name = "lb_DRTPMonitorSn";
            this.lb_DRTPMonitorSn.Size = new System.Drawing.Size(874, 22);
            this.lb_DRTPMonitorSn.TabIndex = 0;
            this.lb_DRTPMonitorSn.Text = "---Detail Information";
            this.lb_DRTPMonitorSn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chamberSimple
            // 
            this.chamberSimple.BackColor = System.Drawing.SystemColors.Control;
            this.chamberSimple.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chamberSimple.CanManualEnable = true;
            this.chamberSimple.ChamberName = "C2";
            this.chamberSimple.ChamberNo = 1;
            this.chamberSimple.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chamberSimple.DutPassRate = 0D;
            this.chamberSimple.DutSN = "N/A";
            this.chamberSimple.DUTStatus = DMTec.TestUIFramework.DataModel.DutTestStatus.IDLE;
            this.chamberSimple.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chamberSimple.IsChamberEnable = true;
            this.chamberSimple.IsLowYieldAlarm = false;
            this.chamberSimple.IsSeqHBOk = true;
            this.chamberSimple.Location = new System.Drawing.Point(2, 26);
            this.chamberSimple.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.chamberSimple.MinimumSize = new System.Drawing.Size(29, 52);
            this.chamberSimple.Name = "chamberSimple";
            this.chamberSimple.SeqSubAddress = "tcp://*.*";
            this.chamberSimple.Size = new System.Drawing.Size(874, 358);
            this.chamberSimple.SocketSN = "N/A";
            this.chamberSimple.TabIndex = 13;
            this.chamberSimple.YieldCountRange = 100;
            this.chamberSimple.YieldLowLimit = 0;
            // 
            // tlp_Lable
            // 
            this.tlp_Lable.ColumnCount = 3;
            this.tlp_Lable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Lable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlp_Lable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlp_Lable.Controls.Add(this.lb_SMHB_LED, 0, 0);
            this.tlp_Lable.Controls.Add(this.lb_SeqHB_LED, 2, 0);
            this.tlp_Lable.Controls.Add(this.lb_RTMonitorSn, 0, 0);
            this.tlp_Lable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Lable.Location = new System.Drawing.Point(0, 0);
            this.tlp_Lable.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Lable.Name = "tlp_Lable";
            this.tlp_Lable.RowCount = 1;
            this.tlp_Lable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Lable.Size = new System.Drawing.Size(878, 26);
            this.tlp_Lable.TabIndex = 14;
            // 
            // lb_SMHB_LED
            // 
            this.lb_SMHB_LED.AutoSize = true;
            this.lb_SMHB_LED.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_SMHB_LED.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lb_SMHB_LED.Location = new System.Drawing.Point(781, 3);
            this.lb_SMHB_LED.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.lb_SMHB_LED.Name = "lb_SMHB_LED";
            this.lb_SMHB_LED.Size = new System.Drawing.Size(47, 20);
            this.lb_SMHB_LED.TabIndex = 1;
            this.lb_SMHB_LED.Text = "SM";
            this.lb_SMHB_LED.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_SeqHB_LED
            // 
            this.lb_SeqHB_LED.AutoSize = true;
            this.lb_SeqHB_LED.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_SeqHB_LED.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lb_SeqHB_LED.Location = new System.Drawing.Point(830, 3);
            this.lb_SeqHB_LED.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.lb_SeqHB_LED.Name = "lb_SeqHB_LED";
            this.lb_SeqHB_LED.Size = new System.Drawing.Size(47, 20);
            this.lb_SeqHB_LED.TabIndex = 0;
            this.lb_SeqHB_LED.Text = "SEQ";
            this.lb_SeqHB_LED.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_RTMonitorSn
            // 
            this.lb_RTMonitorSn.AutoSize = true;
            this.lb_RTMonitorSn.BackColor = System.Drawing.Color.Transparent;
            this.lb_RTMonitorSn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_RTMonitorSn.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lb_RTMonitorSn.ForeColor = System.Drawing.Color.Blue;
            this.lb_RTMonitorSn.Location = new System.Drawing.Point(2, 2);
            this.lb_RTMonitorSn.Margin = new System.Windows.Forms.Padding(2);
            this.lb_RTMonitorSn.Name = "lb_RTMonitorSn";
            this.lb_RTMonitorSn.Size = new System.Drawing.Size(776, 22);
            this.lb_RTMonitorSn.TabIndex = 0;
            this.lb_RTMonitorSn.Text = "---Basic Information";
            this.lb_RTMonitorSn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChamberUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbl_Main);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChamberUnit";
            this.Size = new System.Drawing.Size(878, 974);
            this.Load += new System.EventHandler(this.ChamberUnit_Load);
            this.Resize += new System.EventHandler(this.ChamberUnit_Resize);
            this.tbl_Main.ResumeLayout(false);
            this.tbl_Main.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tlp_Info.ResumeLayout(false);
            this.tlp_Info.PerformLayout();
            this.tlp_Lable.ResumeLayout(false);
            this.tlp_Lable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbl_Main;
        private System.Windows.Forms.Label lb_RTMonitorSn;
        private System.Windows.Forms.Label lb_DRTPMonitorSn;
        private System.Windows.Forms.TableLayoutPanel tlp_Info;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_DPH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lable_ExtTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private SequenceItemsLister myItemsList;
        private System.Windows.Forms.RichTextBox rtb_LogWindow;
        private ChamberSimple chamberSimple;
        private System.Windows.Forms.TableLayoutPanel tlp_Lable;
        private System.Windows.Forms.Label lb_SeqHB_LED;
        private System.Windows.Forms.Label lb_SMHB_LED;
    }
}
