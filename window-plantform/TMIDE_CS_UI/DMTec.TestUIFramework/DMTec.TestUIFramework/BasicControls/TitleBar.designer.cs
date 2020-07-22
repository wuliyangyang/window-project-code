namespace DMTec.TestUIFramework.BasicControls
{
    partial class TitleBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TitleBar));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lb_Logo = new System.Windows.Forms.Label();
            this.lb_Title = new System.Windows.Forms.Label();
            this.lb_OperatorRole = new System.Windows.Forms.Label();
            this.lb_Version = new System.Windows.Forms.Label();
            this.lb_OperatorName = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 6;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpMain.Controls.Add(this.lb_Logo, 0, 0);
            this.tlpMain.Controls.Add(this.lb_Title, 1, 0);
            this.tlpMain.Controls.Add(this.lb_OperatorRole, 2, 0);
            this.tlpMain.Controls.Add(this.lb_Version, 4, 0);
            this.tlpMain.Controls.Add(this.lb_OperatorName, 3, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1203, 80);
            this.tlpMain.TabIndex = 0;
            // 
            // lb_Logo
            // 
            this.lb_Logo.AutoSize = true;
            this.lb_Logo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Logo.Image = ((System.Drawing.Image)(resources.GetObject("lb_Logo.Image")));
            this.lb_Logo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb_Logo.Location = new System.Drawing.Point(3, 0);
            this.lb_Logo.Name = "lb_Logo";
            this.lb_Logo.Size = new System.Drawing.Size(294, 80);
            this.lb_Logo.TabIndex = 1;
            // 
            // lb_Title
            // 
            this.lb_Title.AutoSize = true;
            this.lb_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Title.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold);
            this.lb_Title.ForeColor = System.Drawing.Color.White;
            this.lb_Title.Location = new System.Drawing.Point(303, 0);
            this.lb_Title.Name = "lb_Title";
            this.lb_Title.Size = new System.Drawing.Size(527, 80);
            this.lb_Title.TabIndex = 3;
            this.lb_Title.Text = "IA978";
            this.lb_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_OperatorRole
            // 
            this.lb_OperatorRole.AutoSize = true;
            this.lb_OperatorRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_OperatorRole.ForeColor = System.Drawing.Color.Yellow;
            this.lb_OperatorRole.Location = new System.Drawing.Point(836, 3);
            this.lb_OperatorRole.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.lb_OperatorRole.Name = "lb_OperatorRole";
            this.lb_OperatorRole.Size = new System.Drawing.Size(114, 69);
            this.lb_OperatorRole.TabIndex = 2;
            this.lb_OperatorRole.Text = "Operator";
            this.lb_OperatorRole.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lb_Version
            // 
            this.lb_Version.AutoSize = true;
            this.lb_Version.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Version.ForeColor = System.Drawing.Color.Blue;
            this.lb_Version.Location = new System.Drawing.Point(1076, 8);
            this.lb_Version.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.lb_Version.Name = "lb_Version";
            this.lb_Version.Size = new System.Drawing.Size(114, 69);
            this.lb_Version.TabIndex = 4;
            this.lb_Version.Text = "V1.0.3";
            this.lb_Version.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lb_OperatorName
            // 
            this.lb_OperatorName.AutoSize = true;
            this.lb_OperatorName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_OperatorName.Location = new System.Drawing.Point(956, 3);
            this.lb_OperatorName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.lb_OperatorName.Name = "lb_OperatorName";
            this.lb_OperatorName.Size = new System.Drawing.Size(114, 69);
            this.lb_OperatorName.TabIndex = 2;
            this.lb_OperatorName.Text = "JimmyGong";
            this.lb_OperatorName.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // TitleBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.tlpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximumSize = new System.Drawing.Size(3000, 300);
            this.MinimumSize = new System.Drawing.Size(0, 80);
            this.Name = "TitleBar";
            this.Size = new System.Drawing.Size(1203, 80);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lb_Logo;
        private System.Windows.Forms.Label lb_Title;
        private System.Windows.Forms.Label lb_Version;
        private System.Windows.Forms.Label lb_OperatorRole;
        private System.Windows.Forms.Label lb_OperatorName;
    }
}
