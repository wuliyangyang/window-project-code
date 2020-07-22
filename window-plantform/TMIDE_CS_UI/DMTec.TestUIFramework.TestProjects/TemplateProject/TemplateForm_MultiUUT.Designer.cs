namespace TemplateProject
{
    partial class TemplateForm_MultiUUT
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
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.titleBar = new DMTec.TestUIFramework.BasicControls.TitleBar();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 2;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 100);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Main.Size = new System.Drawing.Size(1256, 582);
            this.tlp_Main.TabIndex = 1;
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.LightGray;
            this.titleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.titleBar.IsShowOperatorName = true;
            this.titleBar.IsShowOperatorRole = true;
            this.titleBar.IsShowTitle = true;
            this.titleBar.IsShowVersion = true;
            this.titleBar.Location = new System.Drawing.Point(0, 0);
            this.titleBar.MaximumSize = new System.Drawing.Size(3000, 300);
            this.titleBar.MinimumSize = new System.Drawing.Size(0, 80);
            this.titleBar.Name = "titleBar";
            this.titleBar.OperatorName = "JimmyGong";
            this.titleBar.OperatorNameColor = System.Drawing.Color.SandyBrown;
            this.titleBar.OperatorRole = "Operator";
            this.titleBar.OperatorRoleColor = System.Drawing.Color.Yellow;
            this.titleBar.Size = new System.Drawing.Size(1256, 100);
            this.titleBar.TabIndex = 0;
            this.titleBar.TitleColor = System.Drawing.Color.Snow;
            this.titleBar.TitleString = "IA978";
            this.titleBar.VersionColor = System.Drawing.Color.DeepPink;
            this.titleBar.VersionString = "V1.0.3";
            // 
            // TemplateForm_MultiUUT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1256, 682);
            this.Controls.Add(this.tlp_Main);
            this.Controls.Add(this.titleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TemplateForm_MultiUUT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TemplateForm_MultiUUT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private DMTec.TestUIFramework.BasicControls.TitleBar titleBar;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
    }
}

