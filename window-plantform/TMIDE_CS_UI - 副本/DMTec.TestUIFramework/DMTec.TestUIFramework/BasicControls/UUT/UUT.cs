using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMTec.TMListener.Interface;

namespace DMTec.TestUIFramework.BasicControls
{
    public partial class UUT : UserControl, ISequencerListenerUser
    {

        #region//Members//

        private System.Diagnostics.Stopwatch myStopWatch = new System.Diagnostics.Stopwatch();

        System.Windows.Forms.Timer myTimer;
        DateTime StartTime;
        public enum TestStatus
        {
            IDLE = 0,
            TESTING = 1,
            FAIL = 2,
            PASS = 3,
        }

        readonly Color DUT_IDLE_COLOR    = Color.Silver;
        readonly Color DUT_TESTING_COLOR = Color.Yellow;
        readonly Color DUT_FAIL_COLOR    = Color.Red;
        readonly Color DUT_PASS_COLOR    = Color.Green;

        #endregion//Members//

        #region//Constructors//

        public UUT()
        {
            InitializeComponent();
            DutSN = "UUT";
            DutNo = 0;
            InitTimer();
        }

        public UUT(int num)
            : this()
        {
            DutNo = num;
        }

        public UUT(string sn, int num)
            : this()
        {
            DutSN = sn;
            DutNo = num;
        }

        #endregion//Constructors___END//

        #region//Properties and Related private methods//

        /// <summary>
        /// Set and get the property of UUT name label.
        /// </summary>
        [Browsable(true)]
        [DefaultValue("DUTSN0123456789")]
        [Description("DUT's SerialNumber")]
        [Category("CustomProperty")]

        public CheckBox mycheckBox
        {
            get { return this.checkBox; }
        }
        public string DutSN 
        {
            get { return this.lb_DutSN.Text; }
            set { UpdateDutSN(value); }
        }
        private void UpdateDutSN(string name)
        {
            if (this.InvokeRequired) Invoke(new Action<string>(SetSnString), name);
            else SetSnString(name);
        }
        private void SetSnString(string name)
        {
            this.lb_DutSN.Text = name;
        }

        /// <summary>
        /// The progressbar's value.
        /// </summary>
        [Browsable(true)]
        [DefaultValue("")]
        [Description("")]
        [Category("CustomProperty")]
        public int ProgressValue 
        {
            get { return this.progressBar.Value; }
            set { UpdateProgress(value); }
        }
        private void UpdateProgress(int val)
        {
            if (this.InvokeRequired) Invoke(new Action<int>(SetProgressValue), val);
            else SetProgressValue(val);
        }
        private void SetProgressValue(int val)
        {
            this.progressBar.Value = (val < 0 || val > progressBar.Maximum) ? progressBar.Value : val;
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [DefaultValue(typeof(int), "100")]
        [Description("Set and Get the max value of progress bar.")]
        [Category("CustomProperty")]
        public int ProgressMaxValue 
        {
            get { return this.progressBar.Maximum; }
            set { UpdateProgressMaxVlaue(value); }
        }
        private void UpdateProgressMaxVlaue(int max)
        {
            if (this.InvokeRequired) Invoke(new Action<int>(SetProgressMaxValue), max);
            else SetProgressMaxValue(max);
        }
        private void SetProgressMaxValue(int max)
        {
            this.progressBar.Maximum = (max < 1)? 100 : max ;
        }

        [Browsable(true)]
        [DefaultValue(typeof(TestStatus), "IDLE")]
        [Description("The test state(IDLE/TESTING/FAIL/PASS)")]
        [Category("CustomProperty")]
        public TestStatus TestState 
        {
            get { return _testStatus; }
            set 
            { 
                _testStatus = value; 
                UpdateTestState(_testStatus); 
            }
        }private TestStatus _testStatus = TestStatus.IDLE;
        private void UpdateTestState(TestStatus status)
        {
            if (this.InvokeRequired) Invoke(new Action<TestStatus>(SetTestState), status);
            else SetTestState(status);
        }

        private void SetTestState(TestStatus status)
        {
            Color c = new Color();
            string statusString = "IDLE";
            if (TestStatus.IDLE == status) 
            {
                c = DUT_IDLE_COLOR;
                UpdateProgress(0);
                statusString = "IDLE";
            }
            else if (TestStatus.TESTING == status)
            {
                c = DUT_TESTING_COLOR;
                statusString = "TESTING";
                UpdateProgress(0);
            }
            else if (TestStatus.FAIL == status)
            {
                c = DUT_FAIL_COLOR;
                UpdateProgress(ProgressMaxValue);
                statusString = "FAIL";
            }
            else if (TestStatus.PASS == status)
            {
                c = DUT_PASS_COLOR;
                UpdateProgress(ProgressMaxValue);
                statusString = "PASS";
            }
            else
            {
                c = Color.White;
                statusString = "IDLE";
            }
            if (checkBox.Checked)
            {
                UpdateBackColor(c);
            }
            UpdateStatusString(statusString);
        }

        public void UpdateBackColor(Color bc)
        {
            if (this.InvokeRequired) Invoke(new Action<Color>(SetBackColor), bc);
            else SetBackColor(bc);
        }
        private void SetBackColor(Color bc)
        {
            this.BackColor = bc;
        }

        public void UpdateStatusString(string statusString)
        {
            if (this.InvokeRequired) Invoke(new Action<string>(SetStatusString), statusString);
            else SetStatusString(statusString);
        }
        private void SetStatusString(string statusString)
        {
            asflbl_TestStatus.Text = statusString;
        }

        /// <summary>
        /// DUT's index number.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(typeof(int), "1")]
        [Description("DUT Number Display in Label.")]
        [Category("CustomProperty")]
        public int DutNo
        {
            get 
            { 
                int no = 0;
                if(int.TryParse(this.lbl_Number.Text, out no))
                {
                    return no;
                }
                return 0;
            }
            set
            {
                UpdateDutNo(value);
            }
        }
        public void UpdateDutNo(int number)
        {
            if (this.InvokeRequired) Invoke(new Action<int>(SetDutNo), number);
            else SetDutNo(number);
        }
        private void SetDutNo(int number)
        {
            if (number < 0) return;
            this.lbl_Number.Text = number.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [DefaultValue(typeof(int), "100")]
        [Description("DUT Number Display in Label.")]
        [Category("CustomProperty")]
        public int ItemCount
        {
            get { return progressBar.Maximum; }
            set { SetItemCount(value); }
        }
        public void SetItemCount(int count)
        {
            if (this.InvokeRequired) Invoke(new Action<int>(SetProgressBarMaximum), count);
            else SetProgressBarMaximum(count);
        }
        private void SetProgressBarMaximum(int max)
        {
            if (max < 1) return;
            this.progressBar.Maximum = max + 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsUUTEnable
        {
            get { return this.checkBox.Checked; }
            set { 
                if(this.InvokeRequired)
                {
                    Invoke(new Action<bool>((x) => this.checkBox.Checked = x), value);
                }
                else
                {
                    this.checkBox.Checked = value;
                }
            }
        }

        [Browsable(true)]
        [DefaultValue(typeof(bool), "false")]
        [Description("Is show check box in control.")]
        [Category("CustomProperty")]
        public bool IsShowCheckBox 
        {
            get { return this.checkBox.Visible; }
            set {
                if (this.InvokeRequired)
                {
                    Invoke(new Action<bool>((x) => this.checkBox.Visible = x), value);
                }
                else
                {
                    this.checkBox.Visible = value;
                }
            }
        }

        #endregion//Properties and Related private methods//

        #region //Private Methods//

        private void InitTimer()
        {
            myTimer = new System.Windows.Forms.Timer();
            myTimer.Interval = 100;
            myTimer.Enabled = false;  
            myTimer.Tick += myTimer_Tick;
        }

        private void SetTimerValue(string timerVal)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateTimerLabel), timerVal);
            } 
            else
            {
                UpdateTimerLabel(timerVal);
            }
        }

        private void UpdateTimerLabel(string timerString)
        {
            this.lblTimer.Text = timerString;
        }

        private void UpdateTestTime(TimeSpan ts)
        {
            //string s = ts.TotalSeconds.ToString("0.00");
            string s = ts.TotalSeconds.ToString() + "." + ts.TotalMilliseconds.ToString();
            SetTimerValue(s);
        }

        #endregion

        #region//Public Methods//

        /// <summary>
        /// For outside calling.
        /// </summary>
        public void UpdateUI()
        {
            UpdateDutSN(DutSN);
            UpdateProgress(ProgressValue);
            UpdateTestState(TestState);
        }


        #endregion//Public Methods//

        #region//EventHandlers//


        private void myTimer_Tick(object sender, EventArgs e)
        {
            UpdateTestTime(myStopWatch.Elapsed);//
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            if (box.Checked)
            {
                UpdateBackColor(Color.White);
            }
            else
            {
                UpdateBackColor(Color.Gray);
            }
            //Console.WriteLine("UUT State:" + box.Checked);
        }

        #region ISequencerListenerUser Members


        public int BindSequencerListener(ref TMListener.SequencerListener listener)
        {
            if (null == listener) return -1;
            //listener.evt_SeqAttributeFound += OnSequenceAttributeFound;
            return listener.RegisterUser(this);
        }

        public int BindLoggerListener(ref LoggerListener listener)
        {
            if (null == listener) return -1;
            listener.evt_LoggerEnd += OnLoggerEnd;
            return 0;
        }

        public int UnbindSequencerListener(ref TMListener.SequencerListener listener)
        {
            if (null == listener) return -1;
            //listener.evt_SeqAttributeFound -= OnSequenceAttributeFound;
            return listener.RemoveUser(this);
        }

        public void OnSequenceAttributeFound(object sender, TMListener.SeqAttrFoundArgs arg)
        {
            if ("MLBSN" == arg.name) DutSN = arg.value;
        }


        public void OnSequenceStart(object sender, TMListener.SeqStartArgs arg)
        {
            //this.TestState = TestStatus.TESTING;

            //if (myStopWatch.IsRunning) myStopWatch.Stop();
            //myStopWatch.Start();
            //myTimer.Start();

            //StartTime = DateTime.Now;
            this.TestState = TestStatus.TESTING;
            this.Invoke(new Action(() => {
                if (myStopWatch.IsRunning) myStopWatch.Stop();
                myStopWatch.Restart();
                myTimer.Start();
            }));
        }


        public void OnSequenceEnd(object sender, TMListener.SeqEndArgs arg)
        {
            if ("1" == arg.result)
            {
                this.TestState = TestStatus.PASS;
            }
            else
            {
                this.TestState = TestStatus.FAIL;
            }

            this.Invoke(new Action(() => {
                myTimer.Stop();
                myStopWatch.Stop();
                myStopWatch.Reset();
            }));
        }

        public void OnLoggerEnd(object sender, LoggerEndArgs arg)
        {
            //if (0 == arg.ErrorCode)
            //{
            //    this.TestState = TestStatus.PASS;
            //    this.DutSN = "UUT" + this.DutNo.ToString();
            //}
            //else
            //{
            //    this.TestState = TestStatus.FAIL;
            //    this.DutSN = "UUT" + this.DutNo.ToString();
            //}
            //myStopWatch.Stop();
            //myTimer.Stop();

            //TimeSpan Tspan = DateTime.Now - StartTime;
            //SetTimerValue(Tspan.TotalSeconds.ToString() + "." + Tspan.TotalMilliseconds.ToString());
        }

        public void OnSequenceErrorReport(object sender, TMListener.SeqReportErrorArgs arg)
        {
            /*throw new NotImplementedException();*/
        }

        public void OnSequenceHeartbeat(object sender, TMListener.SeqHeartBeatArgs arg)
        {
            /*throw new NotImplementedException();*/
        }

        public void OnSequenceItemEnd(object sender, TMListener.SeqItemEndArgs arg)
        {
            /*throw new NotImplementedException();*/
        }

        public void OnSequenceItemStart(object sender, TMListener.SeqItemStartArgs arg)
        {
            /*throw new NotImplementedException();*/
            ProgressValue += 1;
        }

        public void OnSequenceUOPDetect(object sender, TMListener.SeqUopDetectArgs arg)
        {

            /*throw new NotImplementedException();*/
        }

        public void OnSequenceItemList(object sender, TMListener.SeqItemListArgs arg)
        {
            /*throw new NotImplementedException();*/
        }

        public void OnSequencerListenerLogError(object sender, string msg)
        {
            /*throw new NotImplementedException();*/
        }

        public void OnSequencerListenerLogInfo(object sender, string msg)
        {
            /*throw new NotImplementedException();*/
        }

        public void OnSequencerListenerLogWarn(object sender, string msg)
        {
            /*throw new NotImplementedException();*/
        }

        public void OnSequencerMessage(object sender, string msg)
        {
            /*throw new NotImplementedException();*/
        }

        #endregion

        #endregion//EventHandlers___END//

    }
}
