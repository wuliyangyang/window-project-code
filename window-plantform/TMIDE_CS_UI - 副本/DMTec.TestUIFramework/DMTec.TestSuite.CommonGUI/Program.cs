using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DMTec.TestSuite.CommonGUI
{
    static class Program
    {
        public static string[] AppArgs;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppArgs = args;
            Application.Run(new frmMultiUUT(AppArgs));
        }
    }
}
