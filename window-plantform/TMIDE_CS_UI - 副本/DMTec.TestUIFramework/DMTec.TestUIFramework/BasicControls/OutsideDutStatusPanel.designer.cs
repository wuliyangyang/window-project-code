namespace DMTec.TestUIFramework.BasicControls
{
    partial class OutsideDutStatusPanel
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
            this.lb_DutStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_DutSN = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tlp_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 5;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.Controls.Add(this.label1, 0, 0);
            this.tlp_Main.Controls.Add(this.lb_DutStatus, 4, 0);
            this.tlp_Main.Controls.Add(this.label3, 3, 0);
            this.tlp_Main.Controls.Add(this.label2, 1, 0);
            this.tlp_Main.Controls.Add(this.lb_DutSN, 2, 0);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 1;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(821, 59);
            this.tlp_Main.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(1, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 59);
            this.label1.TabIndex = 0;
            this.label1.Text = "---[Outside DUT]---";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_DutStatus
            // 
            this.lb_DutStatus.AutoSize = true;
            this.lb_DutStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_DutStatus.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_DutStatus.Location = new System.Drawing.Point(588, 0);
            this.lb_DutStatus.Name = "lb_DutStatus";
            this.lb_DutStatus.Size = new System.Drawing.Size(230, 59);
            this.lb_DutStatus.TabIndex = 3;
            this.lb_DutStatus.Text = "IDLE";
            this.lb_DutStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(488, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 59);
            this.label3.TabIndex = 2;
            this.label3.Text = "Status:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_DutSN
            // 
            this.lb_DutSN.AutoSize = true;
            this.lb_DutSN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_DutSN.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_DutSN.Location = new System.Drawing.Point(251, 0);
            this.lb_DutSN.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.lb_DutSN.Name = "lb_DutSN";
            this.lb_DutSN.Size = new System.Drawing.Size(234, 59);
            this.lb_DutSN.TabIndex = 1;
            this.lb_DutSN.Text = "DutSn1234567890";
            this.lb_DutSN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(201, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 59);
            this.label2.TabIndex = 0;
            this.label2.Text = "SN:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OutsideDutStatusPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tlp_Main);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "OutsideDutStatusPanel";
            this.Size = new System.Drawing.Size(821, 59);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_DutSN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_DutStatus;
        private System.Windows.Forms.Label label2;
    }
}
