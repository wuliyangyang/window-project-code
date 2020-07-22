#region#Copyright Information#
///Created by JimmyGong 2017.01.01
///For IA978 Project UI Design.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
using DMTec.TestDataProcessor;//JimmyGong Added 2017.06.18
//
using DMTec.TestUIFramework.DataModel;
using DMTec.TestUIFramework.Common;
using DMTec.TestUIFramework.AdvancedControls;
using DMTec.TMListener;
//
namespace DMTec.TestUIFramework.BasicControls
{
    public partial class ChamberSimple : UserControl
    {
        #region//Constructors//

        /// <summary>
        /// 
        /// </summary>
        public ChamberSimple()
        {
            InitializeComponent();
            ChamberName = "C" + counter++;
            InitMembers();
        }

        /// <summary>
        /// Constructor(With Assigned SN)
        /// </summary>
        /// <param name="i">Control's SN</param>
        public ChamberSimple(int i)
        {
            InitializeComponent();
            ChamberName = "C" + i.ToString();
            InitMembers();
        }

        #endregion//Constructors//

        #region//Members//

        private static int counter = 1;

        Tester myTester;

        DMTec.TestDataProcessor.DutDataUnit myDutProcessor;

        DUT dut;

        System.Timers.Timer myTimer;

        private bool _canManualEnable = true;

        private int _chamberNo = 1;

        private bool _isLowYieldAlarm = false;

        /// <summary>
        /// The Serial Number of TestSocket.
        /// </summary>
        private string _socketSN = "N/A";

        /// <summary>
        /// The Serial Number of TestDut.
        /// </summary>
        private int _dutSN = -9999;

        /// <summary>
        /// 
        /// </summary>
        private bool _isChamberEnable = true;

        /// <summary>
        /// The Flag if sequencer connect(HeartBeat) status ok.
        /// </summary>
        private bool _isSeqHBOK = true;

        /// <summary>
        /// Control's BackColor.
        /// </summary>
        private Color _backColor = Color.White;

        /// <summary>
        /// Local ChamberStatistics instance.
        /// For DUT Test Statistics in one chamber.
        /// </summary>
        DataModel.ChamberStatistics myTestStatistics;

        /// <summary>
        /// The TestStatus of DUT.
        /// </summary>
        private DataModel.DutTestStatus myCurrentStatus = DataModel.DutTestStatus.IDLE;

        /// <summary>
        /// 
        /// </summary>
        private Action<Color> setPassrateColorAction;


        public event StatisticsClearHandler event_StatisticsClear;

        #endregion//Members

        #region//CustomProperties//

        public bool IsLowYieldAlarm
        {
            get
            {
                return _isLowYieldAlarm;
            }
            set
            {
                _isLowYieldAlarm = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanManualEnable
        {
            get
            {
                return _canManualEnable;
            }
            set
            {
                _canManualEnable = value;
                ckb_IsEnable.Enabled = value;
            }
        }

        /// <summary>
        /// The Property of Chamber Is Enabled.
        /// </summary>
        public bool IsChamberEnable
        {
            get
            {
                return _isChamberEnable;
            }
            set
            {
                if (_isChamberEnable == value) return;
                _isChamberEnable = value;
                ckb_IsEnable.Checked = value;
                tlp_Middle.Enabled = value;
                tlp_Middle.Visible = value;
            }
        }

        /// <summary>
        /// The property of chamber's connect status with sequencer is ok.
        /// </summary>
        public bool IsSeqHBOk
        {
            get
            {
                return _isSeqHBOK;
            }
            set
            {
                //if(_isSeqHBOK != value) SetHeartBeatStatus(value);
                _isSeqHBOK = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isHBOK"></param>
        public void SetHeartBeatStatus(bool isHBOK)
        {
            Color c = isHBOK ? Color.Transparent:Color.Red;

            SetAddressBackColor(c);
        }

        /// <summary>
        /// The Property of Chamber's Number.
        /// </summary>
        public int ChamberNo
        {
            get
            {
                return _chamberNo;
            }
            set
            {
                _chamberNo = value;
            }
        }

        /// <summary>
        /// The Property of Target Connect Address.
        /// </summary>
        [Browsable(true), DefaultValue("tcp://127.0.0.1:6250"), Description("Set and Read Target Sequencer Address String."), Category("CustomProperties")]
        public string SeqSubAddress
        {
            get
            {
                return lb_SeqSubAddress.Text;
            }
            set
            {
                lb_SeqSubAddress.Text = value;
            }
        }

        /// <summary>
        /// The Property of Chamber's ID.
        /// </summary>
        [Browsable(true), DefaultValue(typeof(string), "C1"), Description("Set and Read Station ID String"), Category("CustomProperties")]
        public string ChamberName
        {
            get
            {
                return lb_ChamberID.Text;
            }
            set
            {
                lb_ChamberID.Text = value;
            }
        }

        /// <summary>
        /// The Property of SocketSN.
        /// </summary>
        [Browsable(true), DefaultValue(""), Description("Set and Read the Socket SN Int Value."), Category("CustomProperties")]
        public string SocketSN
        {
            get
            {
                return _socketSN;
            }
            set
            {
                _socketSN = value;
                SetSocketSN(_socketSN);
            }
        }

        /// <summary>
        /// The DUT Name Property.
        /// </summary>
        [Browsable(true), DefaultValue("DUT Serial Number"), Description("Set and Read DUT SN string"), Category("CustomProperties")]
        public string DutSN
        {
            get
            {
                //return _dutSN;
                return dut.DutName;
            }
            set
            {
                //_dutSN = value;
                dut.DutName = value;
            }
        }

        /// <summary>
        /// The DUT Test Status Property.
        /// </summary>
        [Browsable(true), Description("Set and Read Station Test Status"), Category("CustomProperties")]
        public DutTestStatus DUTStatus
        {
            get
            {
                return myCurrentStatus;
            }
            set
            {
                myCurrentStatus = value;
                //this.Invalidate();
                Color bColor;
                switch(myCurrentStatus)
                {
                    case DutTestStatus.IDLE:
                        bColor = Color.Gray;
                        dut.DutStatusString = "IDLE";
                        break;
                    case DutTestStatus.Testing:
                        bColor = Color.Blue;
                        dut.DutStatusString = "Testing";
                        break;
                    case DutTestStatus.TestDone:
                        bColor = Color.Green;
                        dut.DutStatusString = "TestDone";
                        break;
                    default:
                        bColor = Color.Gray;
                        dut.DutStatusString = "IDLE";
                        break;
                }
                //SetTestStatusLableColor(bColor);
                //Application.DoEvents();
                this.Invalidate();
            }
        }
        
        /// <summary>
        /// The DutTested Property.
        /// </summary>
        [Browsable(true), DefaultValue(typeof(int),"0"), Description("Set and Read the amount of tested duts"), Category("CustomProperties")]
        public int DutTested
        {
            get
            {
                return myTestStatistics.Total;
            }
            set
            {
                myTestStatistics.Total = value;
                SetTested(myTestStatistics.Total);
            }
        }
        private void SetTested(int val)
        {
            if(InvokeRequired)
            {
                this.Invoke(new IntHandler(SetTested), val);
                return;
            }
            else
            {
                lb_Tested.Text = val.ToString();
                lb_Tested.Invalidate();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// The Dut Untest Property.
        /// </summary>
        [Browsable(true), DefaultValue(typeof(int), "0"), Description("Set and Read the amount of Untest duts"), Category("CustomProperties")]
        public int DutUnTest
        {
            get
            {
                return myTestStatistics.Untest;
            }
            set
            {
                myTestStatistics.Untest = value;
                SetUntested(value);
            }
        }
        private void SetUntested(int val)
        {
            if (InvokeRequired)
            {
                this.Invoke(new IntHandler(SetUntested), val);
                return;
            }
            else
            {
                lb_Untest.Text = val.ToString();
                lb_Untest.Invalidate();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// The DutPassed Property.
        /// </summary>
        [Browsable(true), DefaultValue(typeof(int), "0"), Description("Set and Read the amount of passed duts"), Category("CustomProperties")]
        public int DutPassed
        {
            get
            {
                return myTestStatistics.Passed;
            }
            set
            {
                myTestStatistics.Passed = value;
                SetPassed(myTestStatistics.Passed);
            }
        }
        private void SetPassed(int val)
        {
            if (InvokeRequired)
            {
                this.Invoke(new IntHandler(SetPassed), val);
                return;
            }
            else
            {
                lb_Passed.Text = val.ToString();
                lb_Passed.Invalidate();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// The DUT PassRate Property.
        /// </summary>
        [Browsable(true), DefaultValue(typeof(int), "0"), Description("Set and Read the passrate of duts."), Category("CustomProperties")]
        public double DutPassRate
        {
            get
            {
                return myTestStatistics.PassRate;
            }
            set
            {
                myTestStatistics.PassRate = value;
                SetPassRate(myTestStatistics.PassRate.ToString("n2"));
            }
        }
        private void SetPassRate(string s)
        {
            if (InvokeRequired)
            {
                this.Invoke(new StringHandler(SetPassRate), s);
                return;
            }
            else
            {
                lb_PassRate.Text = s+"%";
                lb_PassRate.Invalidate();
                Application.DoEvents();
            }
        }


        public void SetAddressBackColor(Color c)
        {
            if(lb_SeqSubAddress.InvokeRequired)
            {
                lb_SeqSubAddress.BeginInvoke(new ColorHandler(SetAddressBackColor), c);
                return;
            }
            else
            {
                lb_SeqSubAddress.BackColor = c;
                lb_SeqSubAddress.Invalidate();
                Application.DoEvents();
            }
        }

        private int _yieldLowLimit = 0;
        public int YieldLowLimit
        {
            get
            {
                return _yieldLowLimit;
            }
            set
            {
                if (value < 0) _yieldLowLimit = 0;
                else _yieldLowLimit = value;
            }
        }

        private int _yieldCountRange = 100;
        public int YieldCountRange
        {
            get
            {
                return _yieldCountRange;
            }
            set
            {
                _yieldCountRange = value;
                if (_yieldCountRange < 0) _yieldCountRange = 0;
                myDutProcessor.CountRange = _yieldCountRange;
            }
        }

        #endregion//CustomProperties//

        #region//Methods//

        /// <summary>
        /// Init Class Members
        /// </summary>
        private void InitMembers()
        {
            setPassrateColorAction = SetPassRateColor;

            this.tlp_Middle.SuspendLayout();
            this.SuspendLayout();

            myTestStatistics = new ChamberStatistics();
            myDutProcessor = new DutDataUnit();
            dut = new DUT();
            dut.Dock = DockStyle.Fill;
            tlp_Middle.Controls.Add(dut, 0, 1);
            //tlp_Statistics.Controls.Add(btn_ClearStatistics, 4, 1);
            SetStatisticsClearButtonVisible(false);


            this.tlp_Middle.ResumeLayout(false);
            this.tlp_Middle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            InitTimer();
            Application.DoEvents();
        }

        private void InitTimer()
        {
            myTimer = new System.Timers.Timer();
            myTimer.Elapsed += myTimer_Elapsed;
            myTimer.Interval = 1000;
            myTimer.Enabled = true;
        }

        private void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(IsLowYieldAlarm)
            {
                SetPassRateFlashOnce();
            }
            else
            {
                if (Color.White != lb_PassRate.BackColor)
                {
                    setPassrateColorAction(Color.White);
                }
            }
        }

        /// <summary>
        /// Count Sequence End Event Times.
        /// </summary>
        /// <param name="result"></param>
        private void CountSeqEnd(string result)
        {
            myTestStatistics.Total += 1;
            bool isDutTestOK;

            if (Common.Consts.TESTEND_PASS_STRING == result)//Pass
            {
                myTestStatistics.Passed += 1;
                isDutTestOK = true;
            }
            else
            {
                isDutTestOK = false;
            }
            if (200 > myTestStatistics.Total && myTestStatistics.Total > 100) isDutTestOK = false;
            myDutProcessor.InsertNewOne(isDutTestOK);
            
            UpdateStatistic();
        }

        private void UpdateStatistic()
        {
            if (myTestStatistics.Total > 0)
            {
                if (0 >= myTestStatistics.Passed)
                {
                    myTestStatistics.PassRate = (double)0;
                }
                else
                {
                    if(0 == YieldCountRange)
                        myTestStatistics.PassRate = (double)myTestStatistics.Passed * 100 / myTestStatistics.Total;
                    else
                        myTestStatistics.PassRate = myDutProcessor.GetYieldValue();
                }

            }
            else
            {
                myTestStatistics.Total = 0;
                myTestStatistics.Passed = 0;
                myTestStatistics.PassRate = (double)100;
            }

            if (myTestStatistics.PassRate < YieldLowLimit) IsLowYieldAlarm = true;
            else IsLowYieldAlarm = false;

            DutTested   = myTestStatistics.Total;
            DutUnTest   = myTestStatistics.Untest;
            DutPassed   = myTestStatistics.Passed;
            DutPassRate = myTestStatistics.PassRate;
        }

        private void CountUntest()
        {
            myTestStatistics.Untest += 1;
            UpdateStatistic();
        }

        private void SetPassRateColor(Color c)
        {
            lb_PassRate.BackColor = c;
            lb_PassRate.Invalidate();
            Application.DoEvents();
        }

        private void SetSocketSN(string sckSN)
        {
            if (lb_SocketSN.InvokeRequired)
            {
                lb_SocketSN.Invoke(new Action<string>((x) => SetSocketSN(x)), sckSN);
                return;
            }
            else
            {
                lb_SocketSN.Text = sckSN;
                lb_SocketSN.Invalidate();
                Application.DoEvents();
            }
        }

        public void SetStatisticsClearButtonVisible(bool isVisible)
        {
            this.btn_ClearStatistics.Visible = isVisible;
            if (isVisible)
            {
                //this.tlp_Statistics.Controls.Add(btn_ClearStatistics, 4, 0);
                this.tlp_Statistics.ColumnStyles[4].Width = 20;
            }
            else
            {
                this.tlp_Statistics.ColumnStyles[4].Width = 0;
            }
        }

        public void SetStatisticsDisplay(ChamberStatistics cbs)
        {
            DutTested   = cbs.Total;
            DutPassed   = cbs.Passed;
            DutUnTest   = cbs.Untest;
            DutPassRate = cbs.PassRate;
        }

        /// <summary>
        /// Init Chamber's Statistics.
        /// </summary>
        public void InitStatistics()
        {
            myTestStatistics = new ChamberStatistics();
            UpdateStatistic();
            Application.DoEvents();
        }

        public void SetStatistic(int tested, int passed, int untested)
        {
            myTestStatistics.Total = tested;
            myTestStatistics.Passed = passed;
            myTestStatistics.Untest = untested;

            UpdateStatistic();
        }

        public void ResetStatistic()
        {
            myTestStatistics = new ChamberStatistics();
            myDutProcessor.Clear();
            UpdateStatistic();
        }

        public void SetTester(Tester tester)
        {
            myTester = tester;
        }

        /// <summary>
        /// Binding TMListener's Event.
        /// </summary>
        public void BindSeqListenerEvents(ref SequencerListener listener)
        {
            //--- Bind Events of SeqListener with local function ---//
            //Component msg to ui display
            //listener.event_Print2UI += MsgPrint;
            //listener.event_OriginalMsg += SeqOriginalMsgHandle;   //Bind Msg Received Event

            //Bind Seq Events
            listener.event_SeqStart += OnSeqStartHandle;  //Bind SeqStart Event
            listener.event_SeqEnd    += OnSeqEndHandle;    //Bind SeqEnd Event

            //listener.event_ItemStart += ItemStartHandle;
            listener.event_ItemEnd += OnSeqItemEndHandle;

            //listener.event_SeqReportError += ReportErrHandle;
            //listener.event_SeqUopDetect += UopDetectHandle;
            //listener.event_SeqHeartBeat += HeartbeatHandle;
            listener.event_AttributeFound += OnSeqAttributeFoundHandle;//Bind Attribute Found Event
        }

        /// <summary>
        /// Binding TMListener's Event.
        /// </summary>
        public void UnbindSeqListenerEvents(ref SequencerListener listener)
        {
            //--- Bind Events of SeqListener with local function ---//
            //Component msg to ui display
            //listener.event_Print2UI -= MsgPrint;
            //listener.event_OriginalMsg -= SeqOriginalMsgHandle;   //Bind Msg Received Event

            //Bind Seq Events
            listener.event_SeqStart -= OnSeqStartHandle;  //Bind SeqStart Event
            listener.event_SeqEnd -= OnSeqEndHandle;    //Bind SeqEnd Event

            //listener.event_ItemStart -= ItemStartHandle;
            listener.event_ItemEnd -= OnSeqItemEndHandle;

            //listener.event_SeqReportError -= ReportErrHandle;
            //listener.event_SeqUopDetect -= UopDetectHandle;
            //listener.event_SeqHeartBeat -= HeartbeatHandle;

            listener.event_AttributeFound -= OnSeqAttributeFoundHandle;//Bind Attribute Found Event
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="smc"></param>
        public void BindSMCEvents(ref StateMachineConnector smc)
        {
            //smc.event_SMOriginalMsg += OnSMCOriginalMsg;
            //smc.event_Info += OnSMInfo;
            //smc.event_Warn += OnSMWarn;
            //smc.event_Warn += OnSMError;
            //
            //smc.evt_SM_Heartbeat += OnSMHeartbeatEvent;
            ////
            //smc.evt_SM_State_Idle += OnSMStateIdleEvent;
            //smc.evt_SM_State_Testing += OnSMStateTestingEvent;
            //smc.evt_SM_State_TestDone += OnSMStateTestDoneEvent;
            //smc.evt_SM_State_Load += OnSMStateLoadedEvent;
            //smc.evt_SM_State_Unload += OnSMStateUnloadEvent;
            //smc.evt_SM_State_Error += OnSMStateErrorEvent;
            //
            smc.evt_SM_NoTest += OnSMNotTestEvent;
            smc.evt_SM_NoTest_FailCnt += OnSMNotTestFailCntEvent;
            smc.evt_SM_NoTest_FailLoad += OnSMNotTestFailLoadEvent;
            smc.evt_SM_NoTest_FailCsv += OnSMNotTestFailCSVEvent;
            smc.evt_SM_NoTest_FailReq += OnSMNotTestFailReqEvent;
            smc.evt_SM_NoTest_FailUnknow += OnSMNotTestFailUnknowEvent;
        }

        public void UnbindSMCEvents(ref StateMachineConnector smc)
        {
            //smc.event_SMOriginalMsg -= OnSMCOriginalMsg;
            //smc.event_Info -= OnSMInfo;
            //smc.event_Warn -= OnSMWarn;
            //smc.event_Warn -= OnSMError;
            ////
            //smc.evt_SM_Heartbeat -= OnSMHeartbeatEvent;
            ////
            //smc.evt_SM_State_Idle -= OnSMStateIdleEvent;
            //smc.evt_SM_State_Testing -= OnSMStateTestingEvent;
            //smc.evt_SM_State_TestDone -= OnSMStateTestDoneEvent;
            //smc.evt_SM_State_Load -= OnSMStateLoadedEvent;
            //smc.evt_SM_State_Unload -= OnSMStateUnloadEvent;
            //smc.evt_SM_State_Error -= OnSMStateErrorEvent;
            //
            smc.evt_SM_NoTest -= OnSMNotTestEvent;
            smc.evt_SM_NoTest_FailCnt -= OnSMNotTestFailCntEvent;
            smc.evt_SM_NoTest_FailLoad -= OnSMNotTestFailLoadEvent;
            smc.evt_SM_NoTest_FailCsv -= OnSMNotTestFailCSVEvent;
            smc.evt_SM_NoTest_FailReq -= OnSMNotTestFailReqEvent;
            smc.evt_SM_NoTest_FailUnknow -= OnSMNotTestFailUnknowEvent;

        }

        /// <summary>
        /// Let The PassRate Label Flash Once.
        /// </summary>
        public void SetPassRateFlashOnce()
        {
            if (Color.Red == lb_PassRate.BackColor)
            {
                setPassrateColorAction(Color.White);
            }
            else
            {
                setPassrateColorAction(Color.Red);
            }
        }



        #endregion//Methods...End//

        #region//Events//

        /// <summary>
        /// Load Event Handle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChamberSimple_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }

        #region//Sequencer Related//

        /// <summary>
        /// Handle sequencer msg received event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void OnSeqOriginalMsgHandle(object sender, string originalMsg)
        {
            //MsgPrint(originalMsg);
            //Shell.WriteLine(originalMsg);
        }

        /// <summary>
        /// Handle sequence start event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqStartHandle(object sender, SeqStartArgs arg)
        {
            dut.SeqStartHandle(sender, arg);
        }

        /// <summary>
        /// Handle sequence end event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqEndHandle(object sender, SeqEndArgs arg)
        {
            CountSeqEnd(arg.result);
            dut.SeqEndHandle(sender, arg);
        }

        /// <summary>
        /// Handle test item start event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqItemStartHandle(object sender, SeqItemStartArgs arg)
        {
            //dut.ItemStartHandle(sender, arg);
        }

        /// <summary>
        /// Handle test item end event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqItemEndHandle(object sender, SeqItemEndArgs arg)
        {
            dut.ItemEndHandle(sender, arg);
        }


        /// <summary>
        /// Handle test item error event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqReportErrHandle(object sender, SeqReportErrorArgs arg)
        {
            //UpdateHBDT();
            //IsHeartBeatOK = true;
            //TODO
            //Not Use Now!
        }

        /// <summary>
        /// UOP Detect Event Handle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void OnSeqUopDetectHandle(object sender, SeqUopDetectArgs arg)
        {
            //UpdateHBDT();
            //IsHeartBeatOK = true;
            //TODO.
            //Not Use Now!
        }

        /// <summary>
        /// Handle Sequence heartbeat event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqHeartbeatHandle(object sender, SeqHeartBeatArgs arg)
        {
            //UpdateHBDT();
            //IsHeartBeatOK = true;
            //string str = "Slot:" + arg.slot + " Publisher:" + arg.publisher;
            //_lastHeartBeatDT = DateTime.Now;
            //IsHeartBeatOK = true;
            //LogMessage("New HeartBeat Signal Received!\r\n" + str + "\r\n");
        }

        /// <summary>
        /// Handle Attribute Found event
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqAttributeFoundHandle(object sender, SeqAttrFoundArgs arg)
        {
            this.BeginInvoke(new EventHandler(delegate {
                string module = arg.Publisher;
                string name = arg.name;
                string value = arg.value;

                if ("MLBSN" == name)
                {
                    DutSN = value;
                }
                else if ("SocketSN" == name)
                {
                    SocketSN = value;
                }
                else { }
                //this.Invalidate();
                //Application.DoEvents();
            }));
            
        }
        
        #endregion//Sequencer Related//

        #region//SMState Events//

        public void OnSMCOriginalMsg(object sender, string msg)
        {
            //
        }

        public void OnSMInfo(object sender, string msg)
        {
            //
        }

        public void OnSMWarn(object sender, string msg)
        {
            //
        }

        public void OnSMError(object sender, string msg)
        {
            //
        }

        public void OnSMHeartbeatEvent(object sender, SMHeartBeatEventArgs arg)
        {
            //string msgStr = arg.ToString();
            //MsgPrint(msgStr);
        }

        public void OnSMStateIdleEvent(object sender, SMStateEventArgs arg)
        {
            //string msgStr = arg.ToString();
            //MsgPrint(msgStr);
        }

        public void OnSMStateTestingEvent(object sender, SMStateEventArgs arg)
        {
            //string msgStr = arg.ToString();
            //MsgPrint(msgStr);
        }

        public void OnSMStateTestDoneEvent(object sender, SMStateEventArgs arg)
        {
            //string msgStr = arg.ToString();
            //MsgPrint(msgStr);
        }

        public void OnSMStateLoadedEvent(object sender, SMStateEventArgs arg)
        {
            //string msgStr = arg.ToString();
            //MsgPrint(msgStr);
        }

        public void OnSMStateUnloadEvent(object sender, SMStateEventArgs arg)
        {
            //string msgStr = arg.ToString();
            //MsgPrint(msgStr);
        }

        public void OnSMStateErrorEvent(object sender, SMStateEventArgs arg)
        {
            //string msgStr = arg.ToString();
            //MsgPrint(msgStr);
        }

        public void OnSMNotTestEvent(object sender, SMStateEventArgs arg)
        {
            CountUntest();
        }

        public void OnSMNotTestFailCntEvent(object sender, SMStateEventArgs arg)
        {
            CountUntest();
        }

        public void OnSMNotTestFailLoadEvent(object sender, SMStateEventArgs arg)
        {
            CountUntest();
        }

        public void OnSMNotTestFailCSVEvent(object sender, SMStateEventArgs arg)
        {
            CountUntest();
        }

        public void OnSMNotTestFailReqEvent(object sender, SMStateEventArgs arg)
        {
            CountUntest();
        }

        public void OnSMNotTestFailUnknowEvent(object sender, SMStateEventArgs arg)
        {
            CountUntest();
        }

        #endregion//SMState Events...End//

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckb_IsEnable_CheckedChanged(object sender, EventArgs e)
        {
            if(CanManualEnable)
            {
                //if (null == myTester) return;
                //if (1 == ChamberNo)
                //{
                //    if (myTester.IsChamber1_Enable != ckb_IsEnable.Checked)
                //        myTester.IsChamber1_Enable = ckb_IsEnable.Checked;

                //}
                //else if (2 == ChamberNo)
                //{
                //    if (myTester.IsChamber2_Enable != ckb_IsEnable.Checked)
                //        myTester.IsChamber2_Enable = ckb_IsEnable.Checked;
                //}
                //else { }
            }
        }

        private void ChamberSimple_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void btn_ClearStatistics_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure to clear this chamber's statistics?","Are you sure?",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (null != event_StatisticsClear)
                {
                    event_StatisticsClear.BeginInvoke(this, new EventArgs(), null, null);
                }
            }
        }

        #endregion//Events...End//
    }
}
