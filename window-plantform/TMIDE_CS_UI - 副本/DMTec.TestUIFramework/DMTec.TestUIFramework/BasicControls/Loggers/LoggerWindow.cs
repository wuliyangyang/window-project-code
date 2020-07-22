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
    public partial class LogWindow : UserControl
    {
        public event Common.MsgHandler evt_LogInfo;
        public event Common.MsgHandler evt_LogWarn;
        public event Common.MsgHandler evt_LogError;

        public LogWindow()
        {
            InitializeComponent();
        }

        #region//Private Methods//

        private void WriteInfoString(string infoStr)
        {
            if (tb_Info.InvokeRequired)
            {
                Invoke(new Action<string>((x) => tb_Info.Text += x), infoStr);
            }
            else
            {
                tb_Info.Text += infoStr;
            }
        }

        private void WriteWarnString(string warnStr)
        {
            if (tb_Warning.InvokeRequired)
            {
                Invoke(new Action<string>((x) => tb_Warning.Text += x), warnStr);
            }
            else
            {
                tb_Warning.Text += warnStr;
            }
        }


        private void WriteErrorString(string errStr)
        {
            if (tb_ErrorInfo.InvokeRequired)
            {
                Invoke(new Action<string>((x) => tb_ErrorInfo.Text += x), errStr);
            }
            else
            {
                tb_ErrorInfo.Text += errStr;
            }
        }

        #endregion//Private Methods...End//

        #region//Public Methods//

        public void OnLogInfo(object sender, string msg)
        {
            WriteInfoString(msg);
        }


        public void OnLogWarn(object sender, string msg)
        {
            WriteWarnString(msg);
        }


        public void OnLogError(object sender, string msg)
        {
            WriteErrorString(msg);
        }

        #endregion//Public Methods...End//

        private void tb_Info_TextChanged(object sender, EventArgs e)
        {
            tb_Info.SelectionStart = tb_Info.Text.Length;
            tb_Info.ScrollToCaret();
        }

        private void tb_Warning_TextChanged(object sender, EventArgs e)
        {
            tb_Warning.SelectionStart = tb_Warning.Text.Length;
            tb_Warning.ScrollToCaret();
        }

        private void tb_ErrorInfo_TextChanged(object sender, EventArgs e)
        {
            tb_ErrorInfo.SelectionStart = tb_Info.Text.Length;
            tb_ErrorInfo.ScrollToCaret();
        }

    }
}
