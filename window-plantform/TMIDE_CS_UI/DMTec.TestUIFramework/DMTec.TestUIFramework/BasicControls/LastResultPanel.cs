using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using DMTec.TMListener;
//
namespace DMTec.TestUIFramework.BasicControls
{
    public partial class LastResultPanel : UserControl
    {

        #region Constructor

        public LastResultPanel()
        {
            InitializeComponent();
        } 

        #endregion
        
        #region LocalMember

        private delegate void boolDelegate(bool isOK);

        private delegate void stringDelegate(string str);

        private delegate void colorStringDelegate(Color txtColor, string txtStr);

        private SeqEndArgs lastSeqEndArg = new SeqEndArgs();

        #endregion
        
        private void SetLastResultLabel(Color strColor, string str)
        {
            if (this.lb_LastResult.InvokeRequired)
            {
                Invoke(new colorStringDelegate(SetLastResultLabel), strColor, str);
            }
            else
            {
                this.lb_LastResult.ForeColor = strColor;
                this.lb_LastResult.Text = str;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastResult"></param>
        public void SetLastResultShow(string lastResult)
        {
            if ("1" == lastResult)
            {
                SetLastResultLabel(Color.ForestGreen, "PASS");
            }
            else if("0" == lastResult)
            {
                SetLastResultLabel(Color.Red, "FAIL");
            }
            else
            {
                SetLastResultLabel(Color.Black, "IDLE");
            }
        }

        public void OnSeqEndEvent(SeqEndArgs arg)
        {
            SetLastResultShow(lastSeqEndArg.result);
            lastSeqEndArg = arg;
        }
    }
}
