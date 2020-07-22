using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
using DMTec.TestUIFramework.Common;
//
namespace DMTec.TestUIFramework.BasicControls
{
    public partial class DutSNInputPanel : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public DutSNInputPanel()
        {
            InitializeComponent();
        }

        #region//Members//

        public StringHandler event_NewDutBarcode;

        public StringHandler event_SendSocketBarcode;

        #endregion//Members...End//

        #region//Properties//

        [Browsable(true)]
        [Description("Set and Get Input TextBox's Background Color.")]
        [Category("CustomProperties")]
        public Color InputBoxBackgroudColor
        {
            get { return tb_DutBarcode.BackColor; }
            set { tb_DutBarcode.BackColor = value; }
        }

        [Browsable(true)]
        [Description("Set and Get If Scan BarCode Automatically")]
        [Category("CustomProperties")]
        public bool IsAutoScanBarcode { get; set; }

        #endregion//Properties...End//

        #region//Methods//
        
        private void SendDutSn(string sn)
        {
            if (null != event_NewDutBarcode)
            {
                event_NewDutBarcode.Invoke(sn);
            }
        }

        private void InputDutSN()
        {
            string sn = tb_DutBarcode.Text.Trim();
            if (string.IsNullOrEmpty(sn)) return;
            tb_DutBarcode.Text = "";
            SendDutSn(sn);
        }

        private bool IsSNOK(string sn)
        {
            //if(sn.Length<21) 
            //MessageBox.Show("","",MessageBoxButtons.OK,MessageBoxIcon.Error);
            return true;
        }

        #endregion//Methods...End//

        #region//Events//

        private void tb_DutBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InputDutSN();
            }
        }

        private void tb_DutBarcode_Enter(object sender, EventArgs e)
        {
            tb_DutBarcode.BackColor = Color.DarkOrange;
            tb_DutBarcode.Clear();
        }

        private void tb_DutBarcode_Leave(object sender, EventArgs e)
        {
            tb_DutBarcode.BackColor = Color.Gray;
        }

        #endregion//Events...End//

    }
}
