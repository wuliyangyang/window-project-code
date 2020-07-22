namespace DMTec.TestUIFramework.BasicControls
{
    partial class ChamberHeartbeatPanel
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
            this.label4 = new System.Windows.Forms.Label();
            this.seqBulb = new DMTec.TestUIFramework.BasicControls.ColorBulb();
            this.smBulb = new DMTec.TestUIFramework.BasicControls.ColorBulb();
            this.engBulb = new DMTec.TestUIFramework.BasicControls.ColorBulb();
            this.tlp_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 8;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_Main.Controls.Add(this.label1, 1, 0);
            this.tlp_Main.Controls.Add(this.label2, 3, 0);
            this.tlp_Main.Controls.Add(this.label3, 5, 0);
            this.tlp_Main.Controls.Add(this.label4, 0, 0);
            this.tlp_Main.Controls.Add(this.seqBulb, 2, 0);
            this.tlp_Main.Controls.Add(this.smBulb, 4, 0);
            this.tlp_Main.Controls.Add(this.engBulb, 6, 0);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 1;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(1075, 58);
            this.tlp_Main.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(532, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sequencer:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(707, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 58);
            this.label2.TabIndex = 1;
            this.label2.Text = "StateMachine:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(912, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 58);
            this.label3.TabIndex = 2;
            this.label3.Text = "Engine:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(2, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(526, 58);
            this.label4.TabIndex = 3;
            this.label4.Text = "---[Heartbeat Monitor]---";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // seqBulb
            // 
            this.seqBulb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seqBulb.Location = new System.Drawing.Point(643, 3);
            this.seqBulb.Name = "seqBulb";
            this.seqBulb.On = true;
            this.seqBulb.Size = new System.Drawing.Size(59, 52);
            this.seqBulb.TabIndex = 4;
            // 
            // smBulb
            // 
            this.smBulb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smBulb.Location = new System.Drawing.Point(848, 3);
            this.smBulb.Name = "smBulb";
            this.smBulb.On = true;
            this.smBulb.Size = new System.Drawing.Size(59, 52);
            this.smBulb.TabIndex = 5;
            // 
            // engBulb
            // 
            this.engBulb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.engBulb.Location = new System.Drawing.Point(993, 3);
            this.engBulb.Name = "engBulb";
            this.engBulb.On = true;
            this.engBulb.Size = new System.Drawing.Size(59, 52);
            this.engBulb.TabIndex = 6;
            // 
            // ChamberHeartbeatPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp_Main);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "ChamberHeartbeatPanel";
            this.Size = new System.Drawing.Size(1075, 58);
            this.Load += new System.EventHandler(this.ChamberHeartbeatPanel_Load);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private ColorBulb seqBulb;
        private ColorBulb smBulb;
        private ColorBulb engBulb;
    }
}
