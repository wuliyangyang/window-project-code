namespace DMTec.TestUIFramework.BasicControls
{
    partial class DutSNInputPanel
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
            this.label2 = new System.Windows.Forms.Label();
            this.tlp_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_Barcode
            // 
            this.lb_Barcode.AutoSize = true;
            this.lb_Barcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Barcode.Location = new System.Drawing.Point(184, 0);
            this.lb_Barcode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_Barcode.Name = "lb_Barcode";
            this.lb_Barcode.Size = new System.Drawing.Size(122, 69);
            this.lb_Barcode.TabIndex = 0;
            this.lb_Barcode.Text = "DutBarcode:";
            this.lb_Barcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 3;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.lb_Barcode, 1, 0);
            this.tlp_Main.Controls.Add(this.tb_DutBarcode, 2, 0);
            this.tlp_Main.Controls.Add(this.label2, 0, 0);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(1);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 1;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(650, 69);
            this.tlp_Main.TabIndex = 1;
            // 
            // tb_DutBarcode
            // 
            this.tb_DutBarcode.BackColor = System.Drawing.Color.Gray;
            this.tb_DutBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_DutBarcode.FlexibleHeight = 69;
            this.tb_DutBarcode.Font = new System.Drawing.Font("微软雅黑", 46.97595F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tb_DutBarcode.Location = new System.Drawing.Point(310, 0);
            this.tb_DutBarcode.Margin = new System.Windows.Forms.Padding(0);
            this.tb_DutBarcode.Name = "tb_DutBarcode";
            this.tb_DutBarcode.Size = new System.Drawing.Size(340, 69);
            this.tb_DutBarcode.TabIndex = 0;
            this.tb_DutBarcode.Text = "SN";
            this.tb_DutBarcode.Enter += new System.EventHandler(this.tb_DutBarcode_Enter);
            this.tb_DutBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_DutBarcode_KeyDown);
            this.tb_DutBarcode.Leave += new System.EventHandler(this.tb_DutBarcode_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 69);
            this.label2.TabIndex = 2;
            this.label2.Text = "---[Barcode Input]---";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DutSNInputPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 69);
            this.Controls.Add(this.tlp_Main);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DutSNInputPanel";
            this.tlp_Main.ResumeLayout(false);
            this.tlp_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_Barcode;
        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private AutoSizeFontTextBox tb_DutBarcode;
        private System.Windows.Forms.Label label2;
    }
}
