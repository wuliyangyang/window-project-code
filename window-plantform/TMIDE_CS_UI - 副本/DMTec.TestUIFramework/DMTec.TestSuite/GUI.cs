using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestSuite
{
    public partial class GUI : Form
    {
        Timer myTimer;

        public GUI()
        {
            InitializeComponent();
            myTimer = new Timer();
            myTimer.Interval = 1000;
            myTimer.Tick += MyTimer_Tick;
            mySignalLamp.CurrentState = TestUIFramework.BasicControls.SignalState.Abnormal;

        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            mySignalLamp.IsLampOn = !mySignalLamp.IsLampOn;
            tmHeartbeatLamp.UpdateDisply();
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            myTimer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tmHeartbeatLamp.FeedDog();
        }
    }
}
