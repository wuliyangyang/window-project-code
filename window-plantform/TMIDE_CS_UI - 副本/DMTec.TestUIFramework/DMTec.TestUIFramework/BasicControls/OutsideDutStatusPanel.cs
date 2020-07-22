using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OutsideDutStatusPanel : UserControl
    {
        public OutsideDutStatusPanel()
        {
            InitializeComponent();
        }

        private void UpdateDutSNDisplay(string sn)
        {
            if(this.lb_DutSN.InvokeRequired)
            {
                Invoke(new Action<string>((x) => this.lb_DutSN.Text = x),sn);
            }
            else
            {
                this.lb_DutSN.Text = sn;
            }
        }

        private void UpdateDutStatusDisplay(string status)
        {
            if(this.lb_DutStatus.InvokeRequired)
            {
                Invoke(new Action<string>((x) => this.lb_DutStatus.Text = x), status);
            }
            else
            {
                this.lb_DutStatus.Text = status;
            }
        }

        public void UpdateNewSN(string dutSn)
        {
            UpdateDutSNDisplay(dutSn);
            UpdateDutStatusDisplay("Wait");
        }

        public void UpdateResult(string sn, string rst)
        {
            UpdateDutSNDisplay(sn);
            string rstStr;
            if (rst != "Wait")
            {
                rstStr = "1" == rst ? "Pass" : "Fail";
            }else rstStr = "Wait";
            UpdateDutStatusDisplay(rstStr);
        }
    }
}
