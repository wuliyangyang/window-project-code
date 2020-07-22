using DMTec.TestUIFramework.BasicControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.TestProjects
{
    static class Program
    {
        static LogWindow myLogWindow;

        static int i;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //myLogWindow = new LogWindow();
            //myLogWindow.Show();
            //System.Timers.Timer myTimer = new System.Timers.Timer();
            //myTimer.Interval = 10;
            //myTimer.Elapsed += myTimer_Elapsed;
            //myTimer.Enabled = true;
            //Application.Run(new Form1());
            //Application.Run(new LoggerTextBoxTest());
            //Application.Run(new DUT_Test(30));
            Application.Run(new Chamber_Test(24));

        }

        static void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string str = "--Counter: " + i++.ToString() + "--";
            //throw new NotImplementedException();
            myLogWindow.OnLogInfo(new object(), str + "This is info test string...\r\n");
            myLogWindow.OnLogWarn(new object(), str + "This is warn test string...\r\n");
            myLogWindow.OnLogError(new object(), str + "This is error test string...\r\n");

        }
    }
}
