using DMTec.TestUIFramework.AdvancedControls;
using DMTec.TestUIFramework.BasicControls;
namespace DMTec.TestUIFramework.TestProjects
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.loggerWindow1 = new DMTec.TestUIFramework.BasicControls.LoggerTextBox();
            this.testerSimple1 = new DMTec.TestUIFramework.AdvancedControls.TesterSimple();
            this.barcodeInputPanel1 = new DMTec.TestUIFramework.BasicControls.DutSNInputPanel();
            this.chamberHeartbeatPanel1 = new DMTec.TestUIFramework.BasicControls.ChamberHeartbeatPanel();
            this.titleBar1 = new DMTec.TestUIFramework.BasicControls.TitleBar();
            this.SuspendLayout();
            // 
            // loggerWindow1
            // 
            this.loggerWindow1.BackColor = System.Drawing.Color.Black;
            this.loggerWindow1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.loggerWindow1.ForeColor = System.Drawing.Color.White;
            this.loggerWindow1.IsOutputTimeStamp = false;
            this.loggerWindow1.Location = new System.Drawing.Point(25, 176);
            this.loggerWindow1.Name = "loggerWindow1";
            this.loggerWindow1.ReadOnly = true;
            this.loggerWindow1.Size = new System.Drawing.Size(367, 347);
            this.loggerWindow1.TabIndex = 3;
            this.loggerWindow1.Text = "hello";
            // 
            // testerSimple1
            // 
            this.testerSimple1.CanManualEnable = true;
            this.testerSimple1.Chamber1_Address = "N/A";
            this.testerSimple1.Chamber1_Enable = true;
            this.testerSimple1.Chamber1_ID = "C2";
            this.testerSimple1.Chamber1_SocketSN = "N/A";
            this.testerSimple1.Chamber2_Address = "N/A";
            this.testerSimple1.Chamber2_Enable = true;
            this.testerSimple1.Chamber2_ID = "C3";
            this.testerSimple1.Chamber2_SocketSN = "N/A";
            this.testerSimple1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.testerSimple1.IsTesterEnable = true;
            this.testerSimple1.Location = new System.Drawing.Point(929, 176);
            this.testerSimple1.LotID = "N/A";
            this.testerSimple1.Margin = new System.Windows.Forms.Padding(1);
            this.testerSimple1.Name = "testerSimple1";
            this.testerSimple1.Size = new System.Drawing.Size(654, 580);
            this.testerSimple1.SubAddress1 = "N/A";
            this.testerSimple1.SubAddress2 = "N/A";
            this.testerSimple1.TabIndex = 2;
            this.testerSimple1.TestType = "N/A";
            this.testerSimple1.YieldCountRange = 100;
            this.testerSimple1.YieldLowLimit = 0;
            // 
            // barcodeInputPanel1
            // 
            this.barcodeInputPanel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.barcodeInputPanel1.InputBoxBackgroudColor = System.Drawing.Color.Gray;
            this.barcodeInputPanel1.IsAutoScanBarcode = false;
            this.barcodeInputPanel1.Location = new System.Drawing.Point(25, 107);
            this.barcodeInputPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.barcodeInputPanel1.Name = "barcodeInputPanel1";
            this.barcodeInputPanel1.Size = new System.Drawing.Size(1558, 42);
            this.barcodeInputPanel1.TabIndex = 1;
            // 
            // chamberHeartbeatPanel1
            // 
            this.chamberHeartbeatPanel1.IsEngHBOK = true;
            this.chamberHeartbeatPanel1.IsSeqHBOK = false;
            this.chamberHeartbeatPanel1.IsSMHBOK = true;
            this.chamberHeartbeatPanel1.Location = new System.Drawing.Point(10, 10);
            this.chamberHeartbeatPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.chamberHeartbeatPanel1.Name = "chamberHeartbeatPanel1";
            this.chamberHeartbeatPanel1.Size = new System.Drawing.Size(1599, 58);
            this.chamberHeartbeatPanel1.TabIndex = 0;
            // 
            // titleBar1
            // 
            this.titleBar1.BackColor = System.Drawing.Color.DimGray;
            this.titleBar1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.titleBar1.IsShowOperatorName = true;
            this.titleBar1.IsShowOperatorRole = true;
            this.titleBar1.IsShowTitle = true;
            this.titleBar1.IsShowVersion = true;
            this.titleBar1.Location = new System.Drawing.Point(25, 821);
            this.titleBar1.MaximumSize = new System.Drawing.Size(3000, 300);
            this.titleBar1.MinimumSize = new System.Drawing.Size(0, 80);
            this.titleBar1.Name = "titleBar1";
            this.titleBar1.OperatorName = "JimmyGong";
            this.titleBar1.OperatorNameColor = System.Drawing.SystemColors.ControlText;
            this.titleBar1.OperatorRole = "Operator";
            this.titleBar1.OperatorRoleColor = System.Drawing.Color.Yellow;
            this.titleBar1.Size = new System.Drawing.Size(1558, 80);
            this.titleBar1.TabIndex = 4;
            this.titleBar1.TitleColor = System.Drawing.Color.White;
            this.titleBar1.TitleString = "IA978";
            this.titleBar1.VersionColor = System.Drawing.Color.Blue;
            this.titleBar1.VersionString = "V1.0.3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1687, 1126);
            this.Controls.Add(this.titleBar1);
            this.Controls.Add(this.loggerWindow1);
            this.Controls.Add(this.testerSimple1);
            this.Controls.Add(this.barcodeInputPanel1);
            this.Controls.Add(this.chamberHeartbeatPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ChamberHeartbeatPanel chamberHeartbeatPanel1;
        private DutSNInputPanel barcodeInputPanel1;
        private TesterSimple testerSimple1;
        private LoggerTextBox loggerWindow1;
        private TitleBar titleBar1;

    }
}

