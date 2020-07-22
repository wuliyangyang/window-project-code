using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMTec.TestUIFramework.CommonAPI;
//
using DMTec.TMListener.Interface;
using DMTec.TMListener;
//
namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class StatisticInfoPanel : UserControl, ISequencerListenerUser
    {
        private readonly object objLocker = new object();
        string PassRate_PATH;

        DataModel.ChamberStatistics myStatistics;

        public StatisticInfoPanel()
        {
            InitializeComponent();
            myStatistics = new DataModel.ChamberStatistics();
            IsShowResetButton = true;
            
        }
        public void SetPath(string path)
        {
            PassRate_PATH = path+"\\TesterUI\\"+ this.Name + ".json"; ;
        }

        #region//Properties//

        public DataModel.ChamberStatistics Statistics 
        {
            get { return myStatistics; }
            set { myStatistics = value; }
        }

        //public int DataSource { get; set; }

        /// <summary>
        /// The amount of totals.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(0)]
        [Description("The count of total.")]
        [Category("CustomProperty")]
        public string TotalCount
        {
            get { return this.lb_TotalCountValue.Text; }
            set { UpdateTotalCount(value); }
        }
        private void UpdateTotalCount(string tested)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<string>(SetTotalValue), tested);
            }
            else
            {
                SetTotalValue(tested);
            }
        }
        private void SetTotalValue(string tested)
        {
            this.lb_TotalCountValue.Text = tested;
        }

        /// <summary>
        /// The amount of passed.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(0)]
        [Description("The count of passed.")]
        [Category("CustomProperty")]
        public string PassedCount
        {
            get { return this.lb_PassedCountValue.Text; }
            set { UpdatePassedCount(value); }
        }
        private void UpdatePassedCount(string passed)
        {
            if(this.InvokeRequired)
            {
                Invoke(new Action<string>(SetPassedValue), passed);
            }
            else
            {
                SetPassedValue(passed);
            }
        }
        private void SetPassedValue(string passed)
        {
            this.lb_PassedCountValue.Text = passed;
        }

        /// <summary>
        /// The amount of untested.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(0)]
        [Description("The count of untested.")]
        [Category("CustomProperty")]
        public string UntestCount
        {
            get { return this.lb_FailCountValue.Text; }
            set { UpdateUntestCount(value); }
        }
        private void UpdateUntestCount(string untest)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<string>(SetUntestCount), untest);
            }
            else
            {
                SetUntestCount(untest);
            }
        }
        private void SetUntestCount(string untest)
        {
            this.lb_FailCountValue.Text = untest;
        }

        /// <summary>
        /// The passrate of test.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(0)]
        [Description("The passrate.")]
        [Category("CustomProperty")]
        public string PassRate
        {
            get { return this.lb_PassRateValue.Text; }
            set { UpdatePassRate(value); }
        }
        private void UpdatePassRate(string passrate)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<string>(SetPassRateValue), passrate);
            }
            else
            {
                SetPassRateValue(passrate);
            }
        }

        public void updateStatisticsFromFile()
        {
            string debugPath = System.Environment.CurrentDirectory;
            //PassRate_PATH = "C:\\TesterUI\\" + this.Name + ".json";
            object obj = JsonHelper.ReadJsonFile<DataModel.ChamberStatistics>(PassRate_PATH);
            if (null == obj) return;
            DataModel.ChamberStatistics myStatistics = obj as DataModel.ChamberStatistics;
            if (null != myStatistics)
            {
                this.myStatistics = myStatistics;
                SetStatistics(myStatistics);
            }
        }

        private void SetPassRateValue(string passrate)
        {
            this.lb_PassRateValue.Text = passrate;
        }

        /// <summary>
        /// The Visible property of reset button.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(false)]
        [Description("Whether show the reset button.")]
        [Category("CustomProperty")]
        public bool IsShowResetButton
        {
            get { return _isShowClearButton; }
            set {_isShowClearButton = value; 
                SetRstButtonVisible(value);}
        }private bool _isShowClearButton = false;
        private void SetRstButtonVisible(bool isVisible)
        {
            this.btn_ResetStatistics.Visible = isVisible;
            if (isVisible)
            {
                //this.tlp_Main.ColumnStyles[4] = new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F);
                this.tlp_Main.ColumnCount = 5;
                this.tlp_Main.Controls.Add(this.btn_ResetStatistics, 4, 0);
                
                //this.tlp_Main.ColumnStyles.Add(;
            }
            else
            {
                //this.tlp_Main.ColumnStyles[4].Width = (float)0;
                this.tlp_Main.Controls.Remove(btn_ResetStatistics);
                this.tlp_Main.ColumnCount = this.tlp_Main.ColumnCount - 1; 
                //this.tlp_Main.ColumnStyles.RemoveAt(4); 
            }
        }

        #endregion//Properties//

        #region//EventHandlers//

        private void btn_ResetStatistics_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes == MessageBox.Show("Are you sure to reset statistics data?","Reset",MessageBoxButtons.YesNo, MessageBoxIcon.Question) )
            {
                ResetStatistics();
            }
        }

        public void UpdateData()
        {
            UpdateTotalCount(Statistics.Total.ToString());
            UpdateUntestCount(Statistics.Untest.ToString());
            UpdatePassedCount(Statistics.Passed.ToString());
            UpdatePassRate(Statistics.PassRate.ToString("f2") + "%");
            JsonHelper.WriteJsonFile(PassRate_PATH, Statistics);
        }
        public void ResetStatistics()
        {
            SetStatistics(new DataModel.ChamberStatistics());
            UpdateData();
        }

        #endregion//EventHandlers//

        #region//Public Methods//

        public void SetStatistics(DataModel.ChamberStatistics statistics)
        {
            this.myStatistics = statistics;
            UpdateData();
        }

        public void AddPassResult()
        {
            Statistics.Total += 1;
            Statistics.Passed += 1;
            Statistics.PassRate = (double)Statistics.Passed / Statistics.Total * 100;
            UpdateData();
        }

        public void AddFailResult()
        {
            Statistics.Total += 1;
            Statistics.Untest += 1;
            Statistics.PassRate = (double)Statistics.Passed / Statistics.Total * 100;
            UpdateData();
        }

        #endregion//Public Methods//


        public void BindSequencerListeners(ref List<SequencerListener> list)
        {
            foreach (SequencerListener listener in list)
            {
                listener.RegisterUser(this);
            }
        }

        public void BindLoggerListeners(ref List<LoggerListener> list)
        {
            foreach (LoggerListener listener in list)
            {
                listener.event_LoggerEnd += OnLoggerEnd;
            }
        }
        
        public void UnbindSequencerListeners(ref List<SequencerListener> list)
        {
            foreach (SequencerListener listener in list)
            {
                listener.RemoveUser(this);
            }
        }

        #region ISequencerListenerUser Members

        public int BindSequencerListener(ref SequencerListener listener)
        {
            return 0;
        }

        public int UnbindSequencerListener(ref SequencerListener listener)
        {
            return 0;
        }

        public void OnSequenceAttributeFound(object sender, TMListener.SeqAttrFoundArgs arg)
        {
            //TODO
        }

        public void OnSequenceEnd(object sender, TMListener.SeqEndArgs arg)
        {
            lock (objLocker)
            {
                bool isPass = ("1" == arg.result.ToLower()) ? true : false;
                if (isPass)
                {
                    AddPassResult();
                }
                else
                {
                    AddFailResult();
                }
            }
        }

        public void OnLoggerEnd(object sender, LoggerEndArgs arg)
        {
            //lock (objLocker)
            //{
            //    bool rst = (0 == arg.ErrorCode) ? true : false;
            //    AddNewResult(rst);
            //}
        }

        public void OnSequenceErrorReport(object sender, TMListener.SeqReportErrorArgs arg)
        {
            //TODO
        }

        public void OnSequenceHeartbeat(object sender, TMListener.SeqHeartBeatArgs arg)
        {
            //TODO
        }

        public void OnSequenceItemEnd(object sender, TMListener.SeqItemEndArgs arg)
        {
            //TODO
        }

        public void OnSequenceItemList(object sender, TMListener.SeqItemListArgs arg)
        {
            //TODO
        }

        public void OnSequenceItemStart(object sender, TMListener.SeqItemStartArgs arg)
        {
            //TODO
        }

        public void OnSequenceStart(object sender, TMListener.SeqStartArgs arg)
        {
            //TODO
        }

        public void OnSequenceUOPDetect(object sender, TMListener.SeqUopDetectArgs arg)
        {
            //TODO
        }

        public void OnSequencerListenerLogError(object sender, string msg)
        {
            //TODO
        }

        public void OnSequencerListenerLogInfo(object sender, string msg)
        {
            //TODO
        }

        public void OnSequencerListenerLogWarn(object sender, string msg)
        {
            //TODO
        }

        public void OnSequencerMessage(object sender, string msg)
        {
            //TODO
        }

        public void OnSequencerListenerLog(object logSender, int logLevel, string logString)
        {
           
        }

        #endregion

    }
}
