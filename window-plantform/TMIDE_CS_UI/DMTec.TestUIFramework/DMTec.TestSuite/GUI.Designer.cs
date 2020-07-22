namespace DMTec.TestSuite
{
    partial class GUI
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
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_FunctionModule = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer_Image_ItemList = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.titleBar = new DMTec.TestUIFramework.BasicControls.TitleBar();
            this.mySignalLamp = new DMTec.TestUIFramework.BasicControls.SignalLamp();
            this.tmHeartbeatLamp = new DMTec.TestUIFramework.BasicControls.TMHeartbeatLamp();
            this.tlp_Main.SuspendLayout();
            this.tlp_FunctionModule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Image_ItemList)).BeginInit();
            this.splitContainer_Image_ItemList.Panel1.SuspendLayout();
            this.splitContainer_Image_ItemList.Panel2.SuspendLayout();
            this.splitContainer_Image_ItemList.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.titleBar, 0, 0);
            this.tlp_Main.Controls.Add(this.tlp_FunctionModule, 0, 1);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(4);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(1278, 903);
            this.tlp_Main.TabIndex = 1;
            // 
            // tlp_FunctionModule
            // 
            this.tlp_FunctionModule.ColumnCount = 2;
            this.tlp_FunctionModule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlp_FunctionModule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_FunctionModule.Controls.Add(this.splitContainer_Image_ItemList, 0, 0);
            this.tlp_FunctionModule.Controls.Add(this.button1, 1, 0);
            this.tlp_FunctionModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_FunctionModule.Location = new System.Drawing.Point(4, 124);
            this.tlp_FunctionModule.Margin = new System.Windows.Forms.Padding(4);
            this.tlp_FunctionModule.Name = "tlp_FunctionModule";
            this.tlp_FunctionModule.RowCount = 2;
            this.tlp_FunctionModule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_FunctionModule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tlp_FunctionModule.Size = new System.Drawing.Size(1270, 775);
            this.tlp_FunctionModule.TabIndex = 1;
            // 
            // splitContainer_Image_ItemList
            // 
            this.splitContainer_Image_ItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_Image_ItemList.Location = new System.Drawing.Point(4, 4);
            this.splitContainer_Image_ItemList.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer_Image_ItemList.Name = "splitContainer_Image_ItemList";
            this.splitContainer_Image_ItemList.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_Image_ItemList.Panel1
            // 
            this.splitContainer_Image_ItemList.Panel1.Controls.Add(this.mySignalLamp);
            // 
            // splitContainer_Image_ItemList.Panel2
            // 
            this.splitContainer_Image_ItemList.Panel2.Controls.Add(this.tmHeartbeatLamp);
            this.splitContainer_Image_ItemList.Size = new System.Drawing.Size(1008, 700);
            this.splitContainer_Image_ItemList.SplitterDistance = 254;
            this.splitContainer_Image_ItemList.SplitterWidth = 5;
            this.splitContainer_Image_ItemList.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1019, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(248, 79);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.Silver;
            this.titleBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleBar.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.titleBar.IsShowOperatorName = true;
            this.titleBar.IsShowOperatorRole = true;
            this.titleBar.IsShowTitle = true;
            this.titleBar.IsShowVersion = true;
            this.titleBar.Location = new System.Drawing.Point(1, 1);
            this.titleBar.Margin = new System.Windows.Forms.Padding(1);
            this.titleBar.MaximumSize = new System.Drawing.Size(3667, 400);
            this.titleBar.MinimumSize = new System.Drawing.Size(0, 107);
            this.titleBar.Name = "titleBar";
            this.titleBar.OperatorName = "JimmyGong";
            this.titleBar.OperatorNameColor = System.Drawing.SystemColors.ControlText;
            this.titleBar.OperatorRole = "Operator";
            this.titleBar.OperatorRoleColor = System.Drawing.Color.Yellow;
            this.titleBar.Size = new System.Drawing.Size(1276, 118);
            this.titleBar.TabIndex = 0;
            this.titleBar.TitleColor = System.Drawing.Color.White;
            this.titleBar.TitleString = "IA Tester";
            this.titleBar.VersionColor = System.Drawing.SystemColors.Control;
            this.titleBar.VersionString = "V1.0.3";
            // 
            // mySignalLamp
            // 
            this.mySignalLamp.BorderColor = System.Drawing.Color.Black;
            this.mySignalLamp.CurrentState = DMTec.TestUIFramework.BasicControls.SignalState.Normal;
            this.mySignalLamp.IsDoubleClickSwitch = false;
            this.mySignalLamp.IsLampOn = true;
            this.mySignalLamp.Location = new System.Drawing.Point(2, 4);
            this.mySignalLamp.Margin = new System.Windows.Forms.Padding(1, 4, 1, 4);
            this.mySignalLamp.Name = "mySignalLamp";
            this.mySignalLamp.OffCenterColor = System.Drawing.Color.LightGray;
            this.mySignalLamp.OffSurroundColor = System.Drawing.Color.Gray;
            this.mySignalLamp.OnCenterColor = System.Drawing.Color.LightGreen;
            this.mySignalLamp.OnSurroundColor = System.Drawing.Color.GreenYellow;
            this.mySignalLamp.Size = new System.Drawing.Size(95, 90);
            this.mySignalLamp.StateAbnormalCenterColor = System.Drawing.Color.Red;
            this.mySignalLamp.StateAbnormalSurroundColor = System.Drawing.Color.DarkRed;
            this.mySignalLamp.StateNormalCenterColor = System.Drawing.Color.LightGreen;
            this.mySignalLamp.StateNormalSurroundColor = System.Drawing.Color.Green;
            this.mySignalLamp.StateWarnCenterColor = System.Drawing.Color.LightYellow;
            this.mySignalLamp.StateWarnSurroundColor = System.Drawing.Color.Yellow;
            this.mySignalLamp.TabIndex = 0;
            // 
            // tmHeartbeatLamp
            // 
            this.tmHeartbeatLamp.BorderColor = System.Drawing.Color.Transparent;
            this.tmHeartbeatLamp.CurrentState = DMTec.TestUIFramework.BasicControls.SignalState.Abnormal;
            this.tmHeartbeatLamp.IsDoubleClickSwitch = false;
            this.tmHeartbeatLamp.IsLampOn = true;
            this.tmHeartbeatLamp.Location = new System.Drawing.Point(8, 4);
            this.tmHeartbeatLamp.Margin = new System.Windows.Forms.Padding(1, 4, 1, 4);
            this.tmHeartbeatLamp.Name = "tmHeartbeatLamp";
            this.tmHeartbeatLamp.OffCenterColor = System.Drawing.Color.LightGray;
            this.tmHeartbeatLamp.OffSurroundColor = System.Drawing.Color.Gray;
            this.tmHeartbeatLamp.OnCenterColor = System.Drawing.Color.Red;
            this.tmHeartbeatLamp.OnSurroundColor = System.Drawing.Color.DarkRed;
            this.tmHeartbeatLamp.Size = new System.Drawing.Size(89, 90);
            this.tmHeartbeatLamp.StateAbnormalCenterColor = System.Drawing.Color.Red;
            this.tmHeartbeatLamp.StateAbnormalSurroundColor = System.Drawing.Color.DarkRed;
            this.tmHeartbeatLamp.StateNormalCenterColor = System.Drawing.Color.LightGreen;
            this.tmHeartbeatLamp.StateNormalSurroundColor = System.Drawing.Color.Green;
            this.tmHeartbeatLamp.StateWarnCenterColor = System.Drawing.Color.LightYellow;
            this.tmHeartbeatLamp.StateWarnSurroundColor = System.Drawing.Color.Yellow;
            this.tmHeartbeatLamp.TabIndex = 0;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 903);
            this.Controls.Add(this.tlp_Main);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GUI";
            this.Text = "GUI";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_FunctionModule.ResumeLayout(false);
            this.splitContainer_Image_ItemList.Panel1.ResumeLayout(false);
            this.splitContainer_Image_ItemList.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Image_ItemList)).EndInit();
            this.splitContainer_Image_ItemList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TestUIFramework.BasicControls.TitleBar titleBar;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.TableLayoutPanel tlp_FunctionModule;
        private System.Windows.Forms.SplitContainer splitContainer_Image_ItemList;
        private TestUIFramework.BasicControls.SignalLamp mySignalLamp;
        private System.Windows.Forms.Button button1;
        private TestUIFramework.BasicControls.TMHeartbeatLamp tmHeartbeatLamp;
    }
}