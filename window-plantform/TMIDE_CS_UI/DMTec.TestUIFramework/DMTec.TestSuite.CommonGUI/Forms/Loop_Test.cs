using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMTec.TestUIFramework;
using DMTec.TMListener;
using DMTec.TMListener.Interface;
using DMTec.TestUIFramework.BasicControls;
using System.Threading;

namespace DMTec.TestSuite.CommonGUI
{
    public partial class Loop_Test : Form
    {
        public delegate void LoopStartTestHandler();
        public event LoopStartTestHandler LoopStartTestEvent;
        public List<string> snList { get; set; }
        public List<StateMachineConnector> myStateMachineConnectorList { get; set; }


        public DMTec.TestUIFramework.BasicControls.UutStateArrayPanel uutPanel;

        private Thread th;

        private static int looptime;

        private static int interval;

        private static bool IsTestEnd;

        private readonly object objLocker = new object();

        public Loop_Test()
        {
            InitializeComponent();
            this.TopMost = false;
            this.BringToFront();
            this.TopMost = true;

            IsTestEnd = false;
            this.flash.Visible = false;


        }

        private void LoopIn_Click(object sender, EventArgs e)
        {
            this.textBox3.Text = "0";
            th = new Thread(StartTest);
            th.IsBackground = true;
            th.Start();

        }
        private void TellSMEnableSlot()
        {
            List<string> snList = uutPanel.GetSnList();
            List<UUT> uutList = uutPanel.UUTList;
            string reqMsg = "UISN";
            for (int i = 0; i < myStateMachineConnectorList.Count; i++)
            {

                for (int j = 0; j < snList.Count; j++)
                {
                    if (uutList[j].IsUUTEnable)
                    {
                        reqMsg = reqMsg + ":" + j.ToString() + "_" + snList[j];
                    }

                }
                myStateMachineConnectorList[i].RequestMsg(reqMsg);
            }
        }
        private void StartTest()
        {
            try
            {
                bool ret1 = Int32.TryParse(this.textBox1.Text, out looptime);

                bool ret2 = Int32.TryParse(this.textBox2.Text, out interval);

                int current = 0;

                this.Invoke(new Action(() =>
                {
                    this.timer1.Start();
                    this.LoopIn.Enabled = false;
                }));


                while (looptime > 0)
                {
                    Thread.Sleep(interval);
                    IsTestEnd = false;
                    //TellSMEnableSlot();
                    //myStateMachineConnectorList[0].StartTest(uutPanel.GetSnList().ToArray());
                    if (LoopStartTestEvent!=null)
                    {
                        LoopStartTestEvent.Invoke();
                    }
                    while (!IsTestEnd) { };
                    //Thread.Sleep(interval);
                    current++;
                    this.Invoke(new Action(() =>
                    {
                        this.textBox3.Text = current.ToString();
                    }));
                    looptime--;
                }

                this.Invoke(new Action(() =>
                {
                    this.timer1.Stop();
                    this.flash.Visible = false;
                    this.LoopIn.Enabled = true;
                }));

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void LoopOut_Click(object sender, EventArgs e)
        {
            looptime = -1;
            this.Invoke(new Action(() =>
            {
                this.timer1.Stop();
                this.flash.Visible = false;
                this.LoopIn.Enabled = true;
            }));

        }

        public int BindStateMachineConnectors(ref List<StateMachineConnector> connectors)
        {
            if (null == connectors)
            {
                MessageBox.Show("BindStateMachineConnectors failed!!! StateMachineConnector list is null or it's count is incorrect!");
                return -1;
            }

            //LoopSM = connectors;

            for (int i = 0; i < connectors.Count; i++)
            {
                connectors[i].evt_SM_State_TestDone += OnStateMachineEnd;
 
            }
            return 0;
        }

        void OnStateMachineEnd(object sender, SMStateEventArgs arg)
        {
            lock (objLocker)
            {
                IsTestEnd = true;
            }
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.flash.Visible = !this.flash.Visible;
            }));

        }

        private void Loop_Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }

        public new void Close()
        {
            this.Dispose();
        }
    }
}