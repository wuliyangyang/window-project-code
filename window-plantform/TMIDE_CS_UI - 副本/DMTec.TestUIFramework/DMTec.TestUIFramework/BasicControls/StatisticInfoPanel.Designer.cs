namespace DMTec.TestUIFramework.BasicControls
{
    partial class StatisticInfoPanel
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
            this.lb_PassRateName = new System.Windows.Forms.Label();
            this.lb_TotalName = new System.Windows.Forms.Label();
            this.lb_PassRateValue = new System.Windows.Forms.Label();
            this.lb_TotalCountValue = new System.Windows.Forms.Label();
            this.lb_PassedName = new System.Windows.Forms.Label();
            this.lb_PassedCountValue = new System.Windows.Forms.Label();
            this.lb_FailName = new System.Windows.Forms.Label();
            this.lb_FailCountValue = new System.Windows.Forms.Label();
            this.btn_ResetStatistics = new System.Windows.Forms.Button();
            this.tlp_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tlp_Main.ColumnCount = 5;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tlp_Main.Controls.Add(this.lb_PassRateName, 3, 0);
            this.tlp_Main.Controls.Add(this.lb_TotalName, 0, 0);
            this.tlp_Main.Controls.Add(this.lb_PassRateValue, 3, 1);
            this.tlp_Main.Controls.Add(this.lb_TotalCountValue, 0, 1);
            this.tlp_Main.Controls.Add(this.lb_PassedName, 2, 0);
            this.tlp_Main.Controls.Add(this.lb_PassedCountValue, 2, 1);
            this.tlp_Main.Controls.Add(this.lb_FailName, 1, 0);
            this.tlp_Main.Controls.Add(this.lb_FailCountValue, 1, 1);
            this.tlp_Main.Controls.Add(this.btn_ResetStatistics, 4, 0);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.Size = new System.Drawing.Size(368, 107);
            this.tlp_Main.TabIndex = 9;
            // 
            // lb_PassRateName
            // 
            this.lb_PassRateName.AutoSize = true;
            this.lb_PassRateName.BackColor = System.Drawing.Color.White;
            this.lb_PassRateName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_PassRateName.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_PassRateName.Location = new System.Drawing.Point(243, 3);
            this.lb_PassRateName.Margin = new System.Windows.Forms.Padding(0);
            this.lb_PassRateName.Name = "lb_PassRateName";
            this.lb_PassRateName.Size = new System.Drawing.Size(77, 49);
            this.lb_PassRateName.TabIndex = 2;
            this.lb_PassRateName.Text = "Passrate";
            this.lb_PassRateName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_TotalName
            // 
            this.lb_TotalName.AutoSize = true;
            this.lb_TotalName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lb_TotalName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_TotalName.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_TotalName.Location = new System.Drawing.Point(3, 3);
            this.lb_TotalName.Margin = new System.Windows.Forms.Padding(0);
            this.lb_TotalName.Name = "lb_TotalName";
            this.lb_TotalName.Size = new System.Drawing.Size(77, 49);
            this.lb_TotalName.TabIndex = 0;
            this.lb_TotalName.Text = "Total";
            this.lb_TotalName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_PassRateValue
            // 
            this.lb_PassRateValue.AutoSize = true;
            this.lb_PassRateValue.BackColor = System.Drawing.Color.White;
            this.lb_PassRateValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_PassRateValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_PassRateValue.Location = new System.Drawing.Point(243, 55);
            this.lb_PassRateValue.Margin = new System.Windows.Forms.Padding(0);
            this.lb_PassRateValue.Name = "lb_PassRateValue";
            this.lb_PassRateValue.Size = new System.Drawing.Size(77, 49);
            this.lb_PassRateValue.TabIndex = 5;
            this.lb_PassRateValue.Text = "100.00%";
            this.lb_PassRateValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_TotalCountValue
            // 
            this.lb_TotalCountValue.AutoSize = true;
            this.lb_TotalCountValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lb_TotalCountValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_TotalCountValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_TotalCountValue.Location = new System.Drawing.Point(3, 55);
            this.lb_TotalCountValue.Margin = new System.Windows.Forms.Padding(0);
            this.lb_TotalCountValue.Name = "lb_TotalCountValue";
            this.lb_TotalCountValue.Size = new System.Drawing.Size(77, 49);
            this.lb_TotalCountValue.TabIndex = 3;
            this.lb_TotalCountValue.Text = "0";
            this.lb_TotalCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_PassedName
            // 
            this.lb_PassedName.AutoSize = true;
            this.lb_PassedName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lb_PassedName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_PassedName.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_PassedName.Location = new System.Drawing.Point(163, 3);
            this.lb_PassedName.Margin = new System.Windows.Forms.Padding(0);
            this.lb_PassedName.Name = "lb_PassedName";
            this.lb_PassedName.Size = new System.Drawing.Size(77, 49);
            this.lb_PassedName.TabIndex = 1;
            this.lb_PassedName.Text = "Pass";
            this.lb_PassedName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_PassedCountValue
            // 
            this.lb_PassedCountValue.AutoSize = true;
            this.lb_PassedCountValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lb_PassedCountValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_PassedCountValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_PassedCountValue.Location = new System.Drawing.Point(163, 55);
            this.lb_PassedCountValue.Margin = new System.Windows.Forms.Padding(0);
            this.lb_PassedCountValue.Name = "lb_PassedCountValue";
            this.lb_PassedCountValue.Size = new System.Drawing.Size(77, 49);
            this.lb_PassedCountValue.TabIndex = 4;
            this.lb_PassedCountValue.Text = "0";
            this.lb_PassedCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_FailName
            // 
            this.lb_FailName.AutoSize = true;
            this.lb_FailName.BackColor = System.Drawing.Color.Pink;
            this.lb_FailName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_FailName.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_FailName.Location = new System.Drawing.Point(83, 3);
            this.lb_FailName.Margin = new System.Windows.Forms.Padding(0);
            this.lb_FailName.Name = "lb_FailName";
            this.lb_FailName.Size = new System.Drawing.Size(77, 49);
            this.lb_FailName.TabIndex = 6;
            this.lb_FailName.Text = "Fail";
            this.lb_FailName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_FailCountValue
            // 
            this.lb_FailCountValue.AutoSize = true;
            this.lb_FailCountValue.BackColor = System.Drawing.Color.Pink;
            this.lb_FailCountValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_FailCountValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lb_FailCountValue.Location = new System.Drawing.Point(83, 55);
            this.lb_FailCountValue.Margin = new System.Windows.Forms.Padding(0);
            this.lb_FailCountValue.Name = "lb_FailCountValue";
            this.lb_FailCountValue.Size = new System.Drawing.Size(77, 49);
            this.lb_FailCountValue.TabIndex = 6;
            this.lb_FailCountValue.Text = "0";
            this.lb_FailCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_ResetStatistics
            // 
            this.btn_ResetStatistics.AccessibleRole = System.Windows.Forms.AccessibleRole.Equation;
            this.btn_ResetStatistics.BackColor = System.Drawing.Color.Silver;
            this.btn_ResetStatistics.BackgroundImage = global::DMTec.TestUIFramework.Properties.Resources.if__svg_2093659;
            this.btn_ResetStatistics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_ResetStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ResetStatistics.FlatAppearance.BorderSize = 0;
            this.btn_ResetStatistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ResetStatistics.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btn_ResetStatistics.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_ResetStatistics.Location = new System.Drawing.Point(323, 3);
            this.btn_ResetStatistics.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ResetStatistics.Name = "btn_ResetStatistics";
            this.tlp_Main.SetRowSpan(this.btn_ResetStatistics, 2);
            this.btn_ResetStatistics.Size = new System.Drawing.Size(42, 101);
            this.btn_ResetStatistics.TabIndex = 7;
            this.btn_ResetStatistics.UseVisualStyleBackColor = false;
            this.btn_ResetStatistics.Visible = false;
            this.btn_ResetStatistics.Click += new System.EventHandler(this.btn_ResetStatistics_Click);
            // 
            // StatisticInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp_Main);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "StatisticInfoPanel";
            this.Size = new System.Drawing.Size(368, 107);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.Label lb_PassRateName;
        private System.Windows.Forms.Label lb_TotalName;
        private System.Windows.Forms.Label lb_PassRateValue;
        private System.Windows.Forms.Label lb_TotalCountValue;
        private System.Windows.Forms.Label lb_PassedName;
        private System.Windows.Forms.Label lb_PassedCountValue;
        private System.Windows.Forms.Label lb_FailName;
        private System.Windows.Forms.Label lb_FailCountValue;
        private System.Windows.Forms.Button btn_ResetStatistics;
    }
}
