using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.TestProjects
{
    public partial class LoggerTextBox_Test : Form
    {
        System.Timers.Timer myTimer;
        System.Timers.Timer myTimer2;
        System.Timers.Timer myTimer3;

        static int i;
        static int j;
        static int k;

        public LoggerTextBox_Test()
        {
            InitializeComponent();
        }

        private void LoggerTextBoxTest_Load(object sender, EventArgs e)
        {
            myTimer = new System.Timers.Timer();
            myTimer.Interval = 100;
            myTimer.Elapsed += myTimer_Elapsed;

            myTimer2 = new System.Timers.Timer();
            myTimer2.Interval = 500;
            myTimer2.Elapsed += myTimer2_Elapsed;

            myTimer3 = new System.Timers.Timer();
            myTimer3.Interval = 1000;
            myTimer3.Elapsed += myTimer3_Elapsed;

            myTimer.Enabled  = true;
            myTimer2.Enabled = true;
            myTimer3.Enabled = true;
        }

        void myTimer3_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            myTimer3.Enabled = false;
            string str = "-[Timer3]--Counter: " + k++.ToString() + "--";

            //throw new NotImplementedException();
            loggerTextBox1.LogInfo(str + "This is info test string...\r\n");
            loggerTextBox1.LogWarn(str + "This is warn test string...\r\n");
            loggerTextBox1.LogError(str + "This is error test string...\r\n");
            myTimer3.Enabled = true;
        }

        void myTimer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            myTimer2.Enabled = false;
            string str = "-[Timer2]--Counter: " + j++.ToString() + "--";

            //throw new NotImplementedException();
            loggerTextBox1.LogInfo(str + "This is info test string...\r\n");
            loggerTextBox1.LogWarn(str + "This is warn test string...\r\n");
            loggerTextBox1.LogError(str + "This is error test string...\r\n");
            myTimer2.Enabled = true;
        }

        private void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            myTimer.Enabled = false;
            string str = "-[Timer1]--Counter: " + i++.ToString() + "--";

            //throw new NotImplementedException();
            loggerTextBox1.LogInfo(str + "This is info test string...\r\n");
            loggerTextBox1.LogWarn(str + "This is warn test string...\r\n");
            loggerTextBox1.LogError(str + "This is error test string...\r\n");
            myTimer.Enabled = true;
        }
    }
}
