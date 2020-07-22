namespace DMTec.TestUIFramework.BasicControls
{
    partial class SequenceItemsLister
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
            this.components = new System.ComponentModel.Container();
            this.dgv_TestItems = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel_Main = new System.Windows.Forms.TableLayoutPanel();
            this.myUIBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TestItems)).BeginInit();
            this.tableLayoutPanel_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myUIBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_TestItems
            // 
            this.dgv_TestItems.AllowUserToAddRows = false;
            this.dgv_TestItems.AllowUserToDeleteRows = false;
            this.dgv_TestItems.AllowUserToResizeColumns = false;
            this.dgv_TestItems.AllowUserToResizeRows = false;
            this.dgv_TestItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_TestItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_TestItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_TestItems.Location = new System.Drawing.Point(0, 0);
            this.dgv_TestItems.Margin = new System.Windows.Forms.Padding(0);
            this.dgv_TestItems.Name = "dgv_TestItems";
            this.dgv_TestItems.ReadOnly = true;
            this.dgv_TestItems.RowTemplate.Height = 30;
            this.dgv_TestItems.Size = new System.Drawing.Size(700, 379);
            this.dgv_TestItems.TabIndex = 0;
            this.dgv_TestItems.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_ItemList_RowPostPaint);
            // 
            // tableLayoutPanel_Main
            // 
            this.tableLayoutPanel_Main.ColumnCount = 1;
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Main.Controls.Add(this.dgv_TestItems, 0, 0);
            this.tableLayoutPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Main.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Main.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel_Main.Name = "tableLayoutPanel_Main";
            this.tableLayoutPanel_Main.RowCount = 1;
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Main.Size = new System.Drawing.Size(700, 379);
            this.tableLayoutPanel_Main.TabIndex = 1;
            // 
            // SequenceItemsLister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel_Main);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SequenceItemsLister";
            this.Size = new System.Drawing.Size(700, 379);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TestItems)).EndInit();
            this.tableLayoutPanel_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myUIBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_TestItems;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Main;
        private System.Windows.Forms.BindingSource myUIBindingSource;
    }
}
