namespace DMTec.TestUIFramework.AdvancedControls
{
    partial class TesterUnit
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
            this.tlp_Units = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_HEAD = new System.Windows.Forms.TableLayoutPanel();
            this.ckb_IsTesterEnable = new System.Windows.Forms.CheckBox();
            this.lb_TesterID = new System.Windows.Forms.Label();
            this.tlp_Main.SuspendLayout();
            this.tlp_HEAD.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Units
            // 
            this.tlp_Units.ColumnCount = 2;
            this.tlp_Units.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Units.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Units.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Units.Location = new System.Drawing.Point(0, 30);
            this.tlp_Units.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Units.Name = "tlp_Units";
            this.tlp_Units.RowCount = 1;
            this.tlp_Units.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Units.Size = new System.Drawing.Size(742, 255);
            this.tlp_Units.TabIndex = 0;
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.tlp_HEAD, 0, 0);
            this.tlp_Main.Controls.Add(this.tlp_Units, 0, 1);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(742, 285);
            this.tlp_Main.TabIndex = 1;
            // 
            // tlp_HEAD
            // 
            this.tlp_HEAD.ColumnCount = 2;
            this.tlp_HEAD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_HEAD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlp_HEAD.Controls.Add(this.ckb_IsTesterEnable, 1, 0);
            this.tlp_HEAD.Controls.Add(this.lb_TesterID, 0, 0);
            this.tlp_HEAD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_HEAD.Location = new System.Drawing.Point(3, 3);
            this.tlp_HEAD.Name = "tlp_HEAD";
            this.tlp_HEAD.RowCount = 1;
            this.tlp_HEAD.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_HEAD.Size = new System.Drawing.Size(736, 24);
            this.tlp_HEAD.TabIndex = 2;
            // 
            // ckb_IsTesterEnable
            // 
            this.ckb_IsTesterEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ckb_IsTesterEnable.AutoSize = true;
            this.ckb_IsTesterEnable.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckb_IsTesterEnable.Checked = true;
            this.ckb_IsTesterEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_IsTesterEnable.Location = new System.Drawing.Point(709, 3);
            this.ckb_IsTesterEnable.Name = "ckb_IsTesterEnable";
            this.ckb_IsTesterEnable.Size = new System.Drawing.Size(24, 18);
            this.ckb_IsTesterEnable.TabIndex = 3;
            this.ckb_IsTesterEnable.UseVisualStyleBackColor = true;
            this.ckb_IsTesterEnable.Visible = false;
            this.ckb_IsTesterEnable.CheckedChanged += new System.EventHandler(this.ckb_IsTesterEnable_CheckedChanged);
            // 
            // lb_TesterID
            // 
            this.lb_TesterID.AutoSize = true;
            this.lb_TesterID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_TesterID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_TesterID.Location = new System.Drawing.Point(3, 0);
            this.lb_TesterID.Name = "lb_TesterID";
            this.lb_TesterID.Size = new System.Drawing.Size(700, 24);
            this.lb_TesterID.TabIndex = 0;
            this.lb_TesterID.Text = "TesterID:";
            this.lb_TesterID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TesterUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp_Main);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "TesterUnit";
            this.Size = new System.Drawing.Size(742, 285);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_HEAD.ResumeLayout(false);
            this.tlp_HEAD.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Units;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.Label lb_TesterID;
        private System.Windows.Forms.TableLayoutPanel tlp_HEAD;
        private System.Windows.Forms.CheckBox ckb_IsTesterEnable;
    }
}
