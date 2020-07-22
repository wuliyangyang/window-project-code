using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using DMTec.TestUIFramework.DataModel;

namespace DMTec.TestUIFramework.BasicControls
{
    public partial class MessageBoxEx : Form
    {
        private delegate void StringDelegate(string str);

        public MessageBoxEx()
        {
            InitializeComponent();
        }

        public string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                SetTitle(value);
            }
        }

        private void SetTitle(string str)
        {
            if(this.InvokeRequired)
            {
                Invoke(new StringDelegate(SetTitle), str);
            }
            else
            {
                this.Text = str;
            }
        }

        public string Content
        {
            get
            {
                return this.lb_Content.Text;
            }
            set
            {
                SetContent(value);
            }
        }

        private void SetContent(string str)
        {
            if (this.lb_Content.InvokeRequired)
            {
                Invoke(new StringDelegate(SetContent), str);
            }
            else
            {
                this.lb_Content.Text = str;
            }
        }


        public void ShowMessage(MessageBoxType msgType, string title, string content)
        {
            if (MessageBoxType.Info == msgType)
            {
                picBox_Logo.Image = Properties.Resources.SecurityAndMaintenance;
            }
            else if (MessageBoxType.Warn == msgType)
            {
                picBox_Logo.Image = Properties.Resources.SecurityAndMaintenance_Alert;
            }
            else if (MessageBoxType.Error == msgType)
            {
                picBox_Logo.Image = Properties.Resources.SecurityAndMaintenance_Error;
            }
            else
            {
                picBox_Logo.Image = Properties.Resources.SecurityAndMaintenance;
            }

            Title = title;
            Content = content;
            this.Validate();
            this.Show();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
