namespace DMTec.TestUIFramework.AdvancedControls
{
    partial class TesterSimple
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
            this.tlp_HEAD = new System.Windows.Forms.TableLayoutPanel();
            this.btn_TesterID = new System.Windows.Forms.Button();
            this.ckb_IsTesterEnable = new System.Windows.Forms.CheckBox();
            this.tlp_Chambers = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_Main.SuspendLayout();
            this.tlp_HEAD.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.tlp_HEAD, 0, 0);
            this.tlp_Main.Controls.Add(this.tlp_Chambers, 0, 1);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(865, 1009);
            this.tlp_Main.TabIndex = 0;
            // 
            // tlp_HEAD
            // 
            this.tlp_HEAD.ColumnCount = 2;
            this.tlp_HEAD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_HEAD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_HEAD.Controls.Add(this.btn_TesterID, 0, 0);
            this.tlp_HEAD.Controls.Add(this.ckb_IsTesterEnable, 1, 0);
            this.tlp_HEAD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_HEAD.Location = new System.Drawing.Point(2, 2);
            this.tlp_HEAD.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_HEAD.Name = "tlp_HEAD";
            this.tlp_HEAD.RowCount = 1;
            this.tlp_HEAD.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_HEAD.Size = new System.Drawing.Size(861, 23);
            this.tlp_HEAD.TabIndex = 2;
            // 
            // btn_TesterID
            // 
            this.btn_TesterID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_TesterID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_TesterID.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.btn_TesterID.Location = new System.Drawing.Point(0, 0);
            this.btn_TesterID.Margin = new System.Windows.Forms.Padding(0);
            this.btn_TesterID.Name = "btn_TesterID";
            this.btn_TesterID.Size = new System.Drawing.Size(841, 23);
            this.btn_TesterID.TabIndex = 1;
            this.btn_TesterID.Text = "Tester#01";
            this.btn_TesterID.UseVisualStyleBackColor = true;
            this.btn_TesterID.Click += new System.EventHandler(this.btn_TesterID_Click);
            // 
            // ckb_IsTesterEnable
            // 
            this.ckb_IsTesterEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ckb_IsTesterEnable.AutoSize = true;
            this.ckb_IsTesterEnable.BackColor = System.Drawing.SystemColors.Control;
            this.ckb_IsTesterEnable.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckb_IsTesterEnable.Checked = true;
            this.ckb_IsTesterEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_IsTesterEnable.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.ckb_IsTesterEnable.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.ckb_IsTesterEnable.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_IsTesterEnable.Location = new System.Drawing.Point(844, 1);
            this.ckb_IsTesterEnable.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.ckb_IsTesterEnable.Name = "ckb_IsTesterEnable";
            this.ckb_IsTesterEnable.Size = new System.Drawing.Size(16, 21);
            this.ckb_IsTesterEnable.TabIndex = 2;
            this.ckb_IsTesterEnable.UseVisualStyleBackColor = false;
            this.ckb_IsTesterEnable.CheckedChanged += new System.EventHandler(this.ckb_IsTesterEnable_CheckedChanged);
            // 
            // tlp_Chambers
            // 
            this.tlp_Chambers.ColumnCount = 1;
            this.tlp_Chambers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Chambers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_Chambers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Chambers.Location = new System.Drawing.Point(2, 28);
            this.tlp_Chambers.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_Chambers.Name = "tlp_Chambers";
            this.tlp_Chambers.RowCount = 2;
            this.tlp_Chambers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Chambers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Chambers.Size = new System.Drawing.Size(861, 979);
            this.tlp_Chambers.TabIndex = 0;
            // 
            // TesterSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp_Main);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "TesterSimple";
            this.Size = new System.Drawing.Size(865, 1009);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_HEAD.ResumeLayout(false);
            this.tlp_HEAD.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.TableLayoutPanel tlp_Chambers;
        private System.Windows.Forms.Button btn_TesterID;
        private System.Windows.Forms.TableLayoutPanel tlp_HEAD;
        private System.Windows.Forms.CheckBox ckb_IsTesterEnable;
    }
}
