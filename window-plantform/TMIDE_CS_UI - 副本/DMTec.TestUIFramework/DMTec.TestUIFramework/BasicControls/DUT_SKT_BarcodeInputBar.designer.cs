namespace DMTec.TestUIFramework.BasicControls
{
    partial class DUT_SKT_BarcodeInputBar
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
            this.lb_Barcode = new System.Windows.Forms.Label();
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.tb_DutBarcode = new DMTec.TestUIFramework.BasicControls.AutoSizeFontTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_SocketBarcode = new DMTec.TestUIFramework.BasicControls.AutoSizeFontTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tlp_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_Barcode
            // 
            this.lb_Barcode.AutoSize = true;
            this.lb_Barcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Barcode.Location = new System.Drawing.Point(234, 0);
            this.lb_Barcode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_Barcode.Name = "lb_Barcode";
            this.lb_Barcode.Size = new System.Drawing.Size(158, 81);
            this.lb_Barcode.TabIndex = 0;
            this.lb_Barcode.Text = "DutBarcode:";
            this.lb_Barcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 6;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlp_Main.Controls.Add(this.lb_Barcode, 1, 0);
            this.tlp_Main.Controls.Add(this.tb_DutBarcode, 2, 0);
            this.tlp_Main.Controls.Add(this.label1, 3, 0);
            this.tlp_Main.Controls.Add(this.tb_SocketBarcode, 4, 0);
            this.tlp_Main.Controls.Add(this.label2, 0, 0);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 1;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(1337, 81);
            this.tlp_Main.TabIndex = 1;
            // 
            // tb_DutBarcode
            // 
            this.tb_DutBarcode.BackColor = System.Drawing.Color.Gray;
            this.tb_DutBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_DutBarcode.FlexibleHeight = 81;
            this.tb_DutBarcode.Font = new System.Drawing.Font("微软雅黑", 56.06807F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tb_DutBarcode.Location = new System.Drawing.Point(397, 1);
            this.tb_DutBarcode.Margin = new System.Windows.Forms.Padding(1);
            this.tb_DutBarcode.Name = "tb_DutBarcode";
            this.tb_DutBarcode.Size = new System.Drawing.Size(330, 81);
            this.tb_DutBarcode.TabIndex = 0;
            this.tb_DutBarcode.Text = "DUTSN";
            this.tb_DutBarcode.Enter += new System.EventHandler(this.tb_DutBarcode_Enter);
            this.tb_DutBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_DutBarcode_KeyDown);
            this.tb_DutBarcode.Leave += new System.EventHandler(this.tb_DutBarcode_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(732, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 81);
            this.label1.TabIndex = 0;
            this.label1.Text = "SocketBarcode:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_SocketBarcode
            // 
            this.tb_SocketBarcode.BackColor = System.Drawing.Color.Gray;
            this.tb_SocketBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_SocketBarcode.FlexibleHeight = 81;
            this.tb_SocketBarcode.Font = new System.Drawing.Font("微软雅黑", 56.06807F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tb_SocketBarcode.Location = new System.Drawing.Point(895, 1);
            this.tb_SocketBarcode.Margin = new System.Windows.Forms.Padding(1);
            this.tb_SocketBarcode.Name = "tb_SocketBarcode";
            this.tb_SocketBarcode.Size = new System.Drawing.Size(330, 81);
            this.tb_SocketBarcode.TabIndex = 1;
            this.tb_SocketBarcode.Text = "SKTSN";
            this.tb_SocketBarcode.Enter += new System.EventHandler(this.tb_SocketBarcode_Enter);
            this.tb_SocketBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_SocketBarcode_KeyDown);
            this.tb_SocketBarcode.Leave += new System.EventHandler(this.tb_SocketBarcode_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 81);
            this.label2.TabIndex = 2;
            this.label2.Text = "---[Barcode Input]---";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DUT_SKT_BarcodeInputBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp_Main);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DUT_SKT_BarcodeInputBar";
            this.Size = new System.Drawing.Size(1337, 81);
            this.tlp_Main.ResumeLayout(false);
            this.tlp_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_Barcode;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        AutoSizeFontTextBox tb_DutBarcode;
        private System.Windows.Forms.Label label1;
        AutoSizeFontTextBox tb_SocketBarcode;
        private System.Windows.Forms.Label label2;
    }
}
