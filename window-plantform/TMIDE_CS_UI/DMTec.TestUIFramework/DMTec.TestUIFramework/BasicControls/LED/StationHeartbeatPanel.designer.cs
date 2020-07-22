namespace DMTec.TestUIFramework.BasicControls
{
    partial class StationHeartbeatPanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mySeqHeartbeats = new DMTec.TestUIFramework.BasicControls.LedArrayPanel();
            this.myEngineHeartbeats = new DMTec.TestUIFramework.BasicControls.LedArrayPanel();
            this.myStateMachineHeartbeats = new DMTec.TestUIFramework.BasicControls.LedArrayPanel();
            this.tlp_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 6;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 218F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_Main.Controls.Add(this.label1, 0, 0);
            this.tlp_Main.Controls.Add(this.label2, 2, 0);
            this.tlp_Main.Controls.Add(this.label3, 4, 0);
            this.tlp_Main.Controls.Add(this.mySeqHeartbeats, 1, 0);
            this.tlp_Main.Controls.Add(this.myEngineHeartbeats, 3, 0);
            this.tlp_Main.Controls.Add(this.myStateMachineHeartbeats, 5, 0);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 1;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(1348, 70);
            this.tlp_Main.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 70);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sequencer:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(502, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 70);
            this.label2.TabIndex = 1;
            this.label2.Text = "Engine:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(982, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 70);
            this.label3.TabIndex = 2;
            this.label3.Text = "StateMachine:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mySeqHeartbeats
            // 
            this.mySeqHeartbeats.BackColor = System.Drawing.Color.Transparent;
            this.mySeqHeartbeats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mySeqHeartbeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mySeqHeartbeats.LEDCount = 8;
            this.mySeqHeartbeats.Location = new System.Drawing.Point(101, 8);
            this.mySeqHeartbeats.Margin = new System.Windows.Forms.Padding(1, 8, 1, 8);
            this.mySeqHeartbeats.Name = "mySeqHeartbeats";
            this.mySeqHeartbeats.Size = new System.Drawing.Size(398, 54);
            this.mySeqHeartbeats.TabIndex = 4;
            this.mySeqHeartbeats.TimerEnable = false;
            // 
            // myEngineHeartbeats
            // 
            this.myEngineHeartbeats.BackColor = System.Drawing.Color.Transparent;
            this.myEngineHeartbeats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myEngineHeartbeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myEngineHeartbeats.LEDCount = 8;
            this.myEngineHeartbeats.Location = new System.Drawing.Point(581, 8);
            this.myEngineHeartbeats.Margin = new System.Windows.Forms.Padding(1, 8, 1, 8);
            this.myEngineHeartbeats.Name = "myEngineHeartbeats";
            this.myEngineHeartbeats.Size = new System.Drawing.Size(398, 54);
            this.myEngineHeartbeats.TabIndex = 5;
            this.myEngineHeartbeats.TimerEnable = false;
            // 
            // myStateMachineHeartbeats
            // 
            this.myStateMachineHeartbeats.BackColor = System.Drawing.Color.Transparent;
            this.myStateMachineHeartbeats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myStateMachineHeartbeats.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myStateMachineHeartbeats.LEDCount = 4;
            this.myStateMachineHeartbeats.Location = new System.Drawing.Point(1131, 8);
            this.myStateMachineHeartbeats.Margin = new System.Windows.Forms.Padding(1, 8, 1, 8);
            this.myStateMachineHeartbeats.Name = "myStateMachineHeartbeats";
            this.myStateMachineHeartbeats.Size = new System.Drawing.Size(216, 54);
            this.myStateMachineHeartbeats.TabIndex = 6;
            this.myStateMachineHeartbeats.TimerEnable = false;
            // 
            // StationHeartbeatPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.tlp_Main);
            this.Margin = new System.Windows.Forms.Padding(1, 5, 1, 5);
            this.Name = "StationHeartbeatPanel";
            this.Size = new System.Drawing.Size(1348, 70);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private LedArrayPanel myEngineHeartbeats;
        private LedArrayPanel myStateMachineHeartbeats;
        private LedArrayPanel mySeqHeartbeats;
    }
}
