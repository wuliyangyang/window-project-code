namespace DMTec.TestUIFramework.BasicControls
{
    partial class HardwareInfoPanel
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
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_PogoPInLeftTimes = new System.Windows.Forms.Label();
            this.lb_SpectrographLeftTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tlp_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 7;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.label4, 0, 0);
            this.tlp_Main.Controls.Add(this.label1, 1, 0);
            this.tlp_Main.Controls.Add(this.label2, 3, 0);
            this.tlp_Main.Controls.Add(this.lb_PogoPInLeftTimes, 2, 0);
            this.tlp_Main.Controls.Add(this.lb_SpectrographLeftTime, 4, 0);
            this.tlp_Main.Controls.Add(this.label3, 5, 0);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 1;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(1017, 73);
            this.tlp_Main.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(1, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(228, 73);
            this.label4.TabIndex = 3;
            this.label4.Text = "---[Key Hardware Information]---";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(231, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 73);
            this.label1.TabIndex = 0;
            this.label1.Text = "PogoPin Left Times:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(431, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 73);
            this.label2.TabIndex = 0;
            this.label2.Text = "Spectrometer Left Time:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_PogoPInLeftTimes
            // 
            this.lb_PogoPInLeftTimes.AutoSize = true;
            this.lb_PogoPInLeftTimes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_PogoPInLeftTimes.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_PogoPInLeftTimes.Location = new System.Drawing.Point(363, 0);
            this.lb_PogoPInLeftTimes.Name = "lb_PogoPInLeftTimes";
            this.lb_PogoPInLeftTimes.Size = new System.Drawing.Size(64, 73);
            this.lb_PogoPInLeftTimes.TabIndex = 1;
            this.lb_PogoPInLeftTimes.Text = "N/A";
            this.lb_PogoPInLeftTimes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_SpectrographLeftTime
            // 
            this.lb_SpectrographLeftTime.AutoSize = true;
            this.lb_SpectrographLeftTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_SpectrographLeftTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_SpectrographLeftTime.Location = new System.Drawing.Point(583, 0);
            this.lb_SpectrographLeftTime.Name = "lb_SpectrographLeftTime";
            this.lb_SpectrographLeftTime.Size = new System.Drawing.Size(64, 73);
            this.lb_SpectrographLeftTime.TabIndex = 1;
            this.lb_SpectrographLeftTime.Text = "N/A";
            this.lb_SpectrographLeftTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(653, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 73);
            this.label3.TabIndex = 2;
            this.label3.Text = "Hour";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HardwareInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp_Main);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HardwareInfoPanel";
            this.Size = new System.Drawing.Size(1017, 73);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_PogoPInLeftTimes;
        private System.Windows.Forms.Label lb_SpectrographLeftTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
