using DMTec.TestUIFramework.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.TabControl;

namespace DMTec.TestSuite.CommonGUI
{
    public partial class Log_Panel : Form
    {
        private SystemConfigInfo myConfig;
        private List<TabPage> tabPageList = new List<TabPage>();
        private List<TextBox> logBoxList = new List<TextBox>();
        public Log_Panel()
        {
            InitializeComponent();
            myConfig = Global.GetConfigInstance();
            if (myConfig == null) return;
            InitPanel();
        }

        private void InitPanel()
        {
            this.tabControl1.TabPages.Clear();
            for (int i = 0; i < myConfig.Slots; i++)
            {
                TabPage page = new TabPage();
                page.Text = "LogPanel" + i.ToString();
                TextBox tb = GetTextBox(i);
                page.Controls.Add(tb);
                logBoxList.Add(tb);
                tabPageList.Add(page);
                this.tabControl1.TabPages.Add(page);
            }
        }
        private TextBox GetTextBox(int n)
        {
            TextBox tb = new TextBox();
            tb.Name = "LogTextBox" + n.ToString();
            tb.Font = new Font(new FontFamily("新宋体"), 12,FontStyle.Bold);
            tb.Multiline = true;
            tb.ScrollBars = ScrollBars.Both;
            tb.Dock = DockStyle.Fill;
            return tb;
        }
        public void ShowLog(string msg,int index)
        {
            if (tabPageList[index].HasChildren)
            {
                logBoxList[index].AppendText(msg + "\r\n");
            }
        }
        public new void Close()
        {
            this.Dispose();
        }
    }
}
