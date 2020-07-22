using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTec.TestSuite.CommonGUI.Forms
{
    public partial class LogInWin : Form
    {
        public delegate void LogInHandler();
        public event LogInHandler LogInSuccess_Evt;
        public event LogInHandler LogInFail_Evt;
        private string _userName = "admin";
        private string _passWord = "admin";
        public LogInWin()
        {
            InitializeComponent();
        }

        private void LogInWin_Load(object sender, EventArgs e)
        {

        }

        private void LogIn_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text==this._userName&& this.textBox2.Text == this._passWord)
            {
                if (LogInSuccess_Evt != null)
                {
                    this.LogInSuccess_Evt();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("账号或者密码错误！！！！！");
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
