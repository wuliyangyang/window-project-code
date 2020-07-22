namespace DMTec.TestUIFramework.BasicForms
{
    partial class SnInputDockPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_TOP = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_SN = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tlp_Main.SuspendLayout();
            this.tlp_TOP.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.tlp_TOP, 0, 0);
            this.tlp_Main.Controls.Add(this.listView1, 0, 1);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Margin = new System.Windows.Forms.Padding(4);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Size = new System.Drawing.Size(1136, 658);
            this.tlp_Main.TabIndex = 0;
            // 
            // tlp_TOP
            // 
            this.tlp_TOP.ColumnCount = 3;
            this.tlp_TOP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlp_TOP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_TOP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_TOP.Controls.Add(this.label1, 0, 0);
            this.tlp_TOP.Controls.Add(this.tb_SN, 1, 0);
            this.tlp_TOP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_TOP.Location = new System.Drawing.Point(3, 3);
            this.tlp_TOP.Name = "tlp_TOP";
            this.tlp_TOP.RowCount = 1;
            this.tlp_TOP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_TOP.Size = new System.Drawing.Size(1130, 44);
            this.tlp_TOP.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 13F);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "SN:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_SN
            // 
            this.tb_SN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_SN.Font = new System.Drawing.Font("Microsoft YaHei UI", 13F);
            this.tb_SN.Location = new System.Drawing.Point(103, 3);
            this.tb_SN.Name = "tb_SN";
            this.tb_SN.Size = new System.Drawing.Size(1004, 41);
            this.tb_SN.TabIndex = 1;
            this.tb_SN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_SN_KeyDown);
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(10, 60);
            this.listView1.Margin = new System.Windows.Forms.Padding(10);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1116, 588);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // SnInputDockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 658);
            this.Controls.Add(this.tlp_Main);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SnInputDockPanel";
            this.Text = "SnInputPanel";
            this.tlp_Main.ResumeLayout(false);
            this.tlp_TOP.ResumeLayout(false);
            this.tlp_TOP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Main;
        private System.Windows.Forms.TableLayoutPanel tlp_TOP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_SN;
        private System.Windows.Forms.ListView listView1;
    }
}