namespace DMTec.TestUIFramework.BasicControls
{
    partial class SNScanner
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SerialNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.autoSizeFontTextBox1 = new DMTec.TestUIFramework.BasicControls.AutoSizeFontTextBox();
            this.asflbInputSN = new DMTec.TestUIFramework.BasicControls.AutoSizeFontLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.listView1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tlpTop, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1181, 692);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Index,
            this.SerialNumber});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(3, 73);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1175, 566);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Index
            // 
            this.Index.Text = "Index";
            // 
            // SerialNumber
            // 
            this.SerialNumber.Text = "SerialNumber";
            // 
            // tlpTop
            // 
            this.tlpTop.ColumnCount = 3;
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTop.Controls.Add(this.autoSizeFontTextBox1, 1, 0);
            this.tlpTop.Controls.Add(this.asflbInputSN, 0, 0);
            this.tlpTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTop.Location = new System.Drawing.Point(3, 23);
            this.tlpTop.Name = "tlpTop";
            this.tlpTop.RowCount = 1;
            this.tlpTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTop.Size = new System.Drawing.Size(1175, 44);
            this.tlpTop.TabIndex = 1;
            // 
            // autoSizeFontTextBox1
            // 
            this.autoSizeFontTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoSizeFontTextBox1.FlexibleHeight = 44;
            this.autoSizeFontTextBox1.Font = new System.Drawing.Font("宋体", 32.43835F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.autoSizeFontTextBox1.Location = new System.Drawing.Point(201, 1);
            this.autoSizeFontTextBox1.Margin = new System.Windows.Forms.Padding(1);
            this.autoSizeFontTextBox1.Name = "autoSizeFontTextBox1";
            this.autoSizeFontTextBox1.Size = new System.Drawing.Size(953, 44);
            this.autoSizeFontTextBox1.TabIndex = 0;
            // 
            // asflbInputSN
            // 
            this.asflbInputSN.AutoSize = true;
            this.asflbInputSN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.asflbInputSN.FlexibleHeight = 42;
            this.asflbInputSN.Font = new System.Drawing.Font("Microsoft YaHei UI", 27.55863F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.asflbInputSN.Location = new System.Drawing.Point(1, 1);
            this.asflbInputSN.Margin = new System.Windows.Forms.Padding(1);
            this.asflbInputSN.Name = "asflbInputSN";
            this.asflbInputSN.Size = new System.Drawing.Size(198, 42);
            this.asflbInputSN.TabIndex = 1;
            this.asflbInputSN.Text = "Input SN:";
            this.asflbInputSN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SNScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SNScanner";
            this.Size = new System.Drawing.Size(1181, 692);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tlpTop.ResumeLayout(false);
            this.tlpTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Index;
        private System.Windows.Forms.ColumnHeader SerialNumber;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private AutoSizeFontTextBox autoSizeFontTextBox1;
        private AutoSizeFontLabel asflbInputSN;
    }
}
