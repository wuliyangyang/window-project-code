using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OperatorIdInputWindow : Form
    {
        public OperatorIdInputWindow()
        {
            InitializeComponent();
        }

        public string OperatorID { get; set; }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            string str = this.tb_Opid_String.Text;
            if (string.IsNullOrEmpty(str))
            {
                //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                MessageBox.Show("Can't input empty operator id!","Warning!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                OperatorID = str;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Hide();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            this.Hide();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private string GetOperatorID()
        {
            string str = this.tb_Opid_String.Text;
            return str;
        }

    }
}
