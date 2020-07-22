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
using System.Diagnostics;
//
using DMTec.TMListener;
//
//using MySql;
using System.Collections.Concurrent;
//
using System.Threading;
using DMTec.TestUIFramework.DataModel;
using DMTec.TestUIFramework.AdvancedControls;
//
namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ChamberUnit : UserControl
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ChamberUnit()
        {
            InitializeComponent();
            InitUI();
            InitMembers();
        }

        public ChamberUnit(int chamberNo, Tester tester)
        {
            InitializeComponent();
            myTester = tester;
            ChamberNo = chamberNo;
            chamberSimple.SetTester(tester);
            chamberSimple.ChamberNo = chamberNo;
            InitUI();
            InitMembers();
        }

        /// <summary>
        /// Constructor(with Assigned ChamberName).
        /// </summary>
        /// <param name="chamberNo"></param>
        public ChamberUnit(int chamberNo)//
        {
            InitializeComponent();
            ChamberNo = chamberNo;
            chamberSimple.ChamberNo = chamberNo;
            InitUI();
            InitMembers();
        }

        ~ChamberUnit()//
        {
            myTestTimer.Enabled = false;
            myHeartBeatTimer.Enabled = false;

            this.Dispose();
        }

        #region//Members// 

        private List<SeqItemEndArgs> myItemEndList;
        private List<SeqItemEndArgs> myItemEndBuffer;

        private int hbCounter = 1;
        private bool isLastSeqEnded = true;

        private Tester myTester;

        private SequencerListener mySeqListener;//
        private StateMachineConnector mySMListener;
        private TMSyncConnector myTMSyncConnector;
        private EngineConnector myEngineConnector;

        //public delegate void Dlgt_Color_String(Color color, string text);
        //public delegate void Dlgt_String(string str);
        public delegate void Dlgt_Color(Color c);
        public delegate void Dlgt();

        private System.Timers.Timer myTestTimer;//For Excute Time Display
        private System.Timers.Timer myHeartBeatTimer;//For Heartbeat Led Update

        private StringBuilder myStringBuilder;

        private DMTec.TestDataProcessor.ItemDataUnit itemDataUnit;

        #endregion//Members...End//
        
        #region//Fields//

        /// <summary>
        /// The flag if can manual set chamber enable.
        /// </summary>
        private bool _canManualEnable = false;

        /// <summary>
        /// The flag if chamber enabled.
        /// </summary>
        private bool _isChamberEnable = true;

        /// <summary>
        /// The Field of Target Sequencer Net Address.
        /// </summary>
        private string _seqSubAddress = "N/A";

        /// <summary>
        /// The Field of Chamber Name.
        /// </summary>
        private string _chamberName = "N/A";

        /// <summary>
        /// The Field of Chamber Number.
        /// </summary>
        private int _chamberNo = 1;

        /// <summary>
        /// The Field of Socket Serial Number.
        /// </summary>
        private string _socketSN = "N/A";

        /// <summary>
        /// The Field of DUT Serial Number.
        /// </summary>
        private string _dutSN = "N/A";

        /// <summary>
        /// The Field of Lot ID.
        /// </summary>
        private string _lotID = "N/A";

        /// <summary>
        /// UPH Start DateTime.
        /// </summary>
        private DateTime _uphStartDT;

        /// <summary>
        /// UPH
        /// </summary>
        private int _uph;


        #region//HeartBeat Related//

        /// <summary>
        /// The flag of Sequencer heart beat is OK.
        /// </summary>
        private bool _isSeqHeartBeatOK = false;

        /// <summary>
        /// The flag of StateMachine heart beat is OK.
        /// </summary>
        private bool _isSMHeartBeatOK = false;

        /// <summary>
        /// The flag of Engine heart beat is OK.
        /// </summary>
        private bool _isEngineHeartBeatOK = false;

        /// <summary>
        /// The DateTime of Last Time Received HeartBeat from Sequencer.
        /// </summary>
        private DateTime lastSeqHeartBeatDT = DateTime.Now;

        /// <summary>
        /// The DateTime of Last Time Received HeartBeat from StateMachine.
        /// </summary>
        private DateTime lastSMHeartBeatDT = DateTime.Now;

        /// <summary>
        /// The DateTime of Last Time Received HeartBeat from Test Engine.
        /// </summary>
        private DateTime lastEngineHeartBeatDT = DateTime.Now;

        /// <summary>
        /// The Time Span Standard Of Last HeartBeat DataTime and Now.
        /// Less Than This, The HeartBeat OK Flag Will Be False.
        /// </summary>
        private int heartBeatCheckInterval = 7000;

        /// <summary>
        /// The Standard Of HeartBeat OK Times.
        /// Less Than This, The HeartBeat OK Flag Will Be False.
        /// </summary>
        private int _heartBeat_Ok_Times_Standard = 1;

        #endregion//HeartBeat Related...End//

        #endregion//Fields...End//
        
        #region//Properties//

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
                chamberSimple.CanManualEnable = value;
            }
        }

        /// <summary>
        /// The Property of Chamber's Enable.
        /// </summary>
        public bool ChamberEnable
        {
            get
            {
                return _isChamberEnable;
            }
            set
            {
                if (_isChamberEnable == value) return;
                _isChamberEnable = value;
                //tbl_Main.Visible = value;
                chamberSimple.IsChamberEnable = value;
                tabControl.Enabled = value;
                tlp_Info.Enabled = value;
                //tabControl.Visible = value;
                //tlp_Info.Visible = value;
                //testItemsList.Visible = value;
            }
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
        /// The Property of Is HeartBeat of Sequencer Ok.
        /// </summary>
        public bool IsSeqHeartBeatOK
        {
            get
            {
                return _isSeqHeartBeatOK;
            }
            set
            {
                _isSeqHeartBeatOK = value;
            }
        }

        /// <summary>
        /// The Property of Is HeartBeat of StateMachine Ok.
        /// </summary>
        public bool IsSMHeartBeatOK
        {
            get
            {
                return _isSMHeartBeatOK;
            }
            set
            {
                _isSMHeartBeatOK = value;
            }
        }

        /// <summary>
        /// The Property of Is HeartBeat of StateMachine Ok.
        /// </summary>
        public bool IsEngineHeartBeatOK
        {
            get
            {
                return _isEngineHeartBeatOK;
            }
            set
            {
                _isSMHeartBeatOK = value;
            }
        }

        /// <summary>
        /// HeartBeat Signal Check Interval.
        /// Scan Timer Parameter.
        /// </summary>
        public int HeartBeatCheckInterval
        {
            get
            {
                return heartBeatCheckInterval;
            }
            set
            {
                heartBeatCheckInterval = value;
            }
        }

        /// <summary>
        /// HeartBeat Signal Check OK Times Standard.
        /// Less Than This, The Flag of HeartBeat OK is False.
        /// </summary>
        public int HeartBeatOKTimesStandard
        {
            get
            {
                return _heartBeat_Ok_Times_Standard;
            }
            set
            {
                _heartBeat_Ok_Times_Standard = value;
            }
        }

        /// <summary>
        /// The Property of Subscriber Net Address(Connect to Sequencer).
        /// </summary>
        [Browsable(true), Description("Set and get <NetAddress> string"), Category("CustomProperties")]
        public string SubAddress2Seq
        {
            get
            {
                return _seqSubAddress;
            }
            set
            {
                _seqSubAddress = value;
                chamberSimple.SeqSubAddress = _seqSubAddress;
            }
        }

        /// <summary>
        /// The Property of Socket Serial Number.
        /// </summary>
        [Browsable(true), Description("Set and get <SocketSN> int value"), Category("CustomProperties")]
        public string SocketSN
        {
            get
            {
                return _socketSN;
            }
            set
            {
                _socketSN = value;
                chamberSimple.SocketSN = _socketSN;
            }
        }

        /// <summary>
        /// The Property of DUT Serial Number.
        /// </summary>
        [Browsable(true), Description("Set and get <DutSN> int value"), Category("CustomProperties")]
        public string DutSN
        {
            get
            {
                return _dutSN;
            }
            set
            {
                _dutSN = value;
                chamberSimple.DutSN = _dutSN;
            }
        }

        /// <summary>
        /// The Property of Lot ID.
        /// </summary>
        public string LotID
        {
            get
            {
                return _lotID;
            }
            set
            {
                _lotID = value;
            }
        }

        /// <summary>
        /// The Property of Chamber Name.
        /// </summary>
        [Browsable(true), Description("Set and get <ChamberName> string"), Category("CustomProperties")]
        public string ChamberName
        {
            get
            {
                return _chamberName;
            }
            set
            {
                _chamberName = value;
                chamberSimple.ChamberName = _chamberName;
            }
        }

        /// <summary>
        /// The Property of ExtTime.
        /// </summary>
        public int ExcuteTime
        {
            get
            {
                return Convert.ToInt32(lable_ExtTime.Text.Equals(string.Empty) ? "0" : lable_ExtTime.Text);
            }
            set
            {
                SetExtTime(value.ToString());
            }
        }
        private void SetExtTime(string str)
        {
            try
            {
                if (null == lable_ExtTime) return;
                if (InvokeRequired)
                {
                    this.BeginInvoke(new Action<string>(SetExtTime), str);
                    return;
                }
                else
                {
                    lable_ExtTime.Text = str;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// The Property of DPH(DUTs Per Hour).
        /// </summary>
        public int DPH
        {
            get{
                //return Convert.ToInt32(label_DPH.Text);
                return _uph;
            }
            set{
                _uph = value;
                SetDPH(value.ToString());
            }
        }
        private void SetDPH(string str)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action<string>(SetDPH), str);
                }
                else
                {
                    label_DPH.Text = str;
                }
            }
            catch (Exception)
            {
                
                throw;
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
                _yieldLowLimit = value;

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
            }
        }

        #endregion//Properties//

        #region//Methods//

        public ChamberSimple GetChamberSimple()
        {
            return chamberSimple;
        }

        /// <summary>
        /// Get Sequencer HeartBeat Status.
        /// </summary>
        /// <returns></returns>
        private bool GetSeqHeartBeatStatus()
        {
            bool isOK;
            int interval = (int)DateTime.Now.Subtract(lastSeqHeartBeatDT).TotalMilliseconds;
            if (interval > heartBeatCheckInterval)
            {
                isOK = false;
            }
            else
            {
                isOK = true;
            }
            return isOK;
        }

        /// <summary>
        /// Get StateMachine HeartBeat Status.
        /// </summary>
        /// <returns></returns>
        private bool GetSMHeartBeatStatus()
        {
            bool isOK;
            int interval = (int)DateTime.Now.Subtract(lastSMHeartBeatDT).TotalMilliseconds;
            if (interval > heartBeatCheckInterval)
            {
                isOK = false;
            }
            else
            {
                isOK = true;
            }
            return isOK;
        }

        /// <summary>
        /// Get Test Engine HeartBeat Status.
        /// </summary>
        /// <returns></returns>
        private bool GetEngineHeartBeatStatus()
        {
            bool isOK;
            int interval = (int)DateTime.Now.Subtract(lastEngineHeartBeatDT).TotalMilliseconds;
            if (interval > heartBeatCheckInterval)
            {
                isOK = false;
            }
            else
            {
                isOK = true;
            }
            return isOK;
        }

        private void Update_SEQ_HB_DT()
        {
            lastSeqHeartBeatDT = DateTime.Now;
            IsSeqHeartBeatOK = true;
        }

        private void Update_SM_HB_DT()
        {
            lastSMHeartBeatDT = DateTime.Now;
            IsSMHeartBeatOK = true;
        }

        private void Update_Engine_HB_DT()
        {
            lastEngineHeartBeatDT = DateTime.Now;
            IsEngineHeartBeatOK = true;
        }

        /// <summary>
        /// Print msg on ui control(TestEdit)
        /// </summary>
        /// <param name="msg">Msg need to print</param>
        private void OnSeqLogInfo(object sender, string msg)
        {
            //string msgString = GetNowDateTimeString() + " " + msg + "\r\n";
            LogInfo(msg);
            //Console.WriteLine(msgString);
        }

        private void OnSeqLogWarn(object sender, string msg)
        {
            //string msgString = GetNowDateTimeString() + " " + msg + "\r\n";
            LogWarn(msg);
            //Console.WriteLine(msgString);
        }

        private void OnSeqLogError(object sender, string msg)
        {
            //string msgString = GetNowDateTimeString() + " " + msg + "\r\n";
            LogError(msg);
            ShowErrorMessage(msg);
            //Console.WriteLine(msgString);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitUI()
        {
            //myStringBuilder = new StringBuilder();

        }

        /// <summary>
        /// Init Members.
        /// </summary>
        private void InitMembers()
        {
            myItemEndList = new List<SeqItemEndArgs>();
            myItemEndBuffer = new List<SeqItemEndArgs>();
            
            itemDataUnit = new DMTec.TestDataProcessor.ItemDataUnit(new DMTec.TestDataProcessor.AlgorithmClass.ContinuouslyItemFailCounter());
            itemDataUnit.ItemFailLimit = 5;

            InitTimers();
        }

        /// <summary>
        /// Init Timers.
        /// </summary>
        private void InitTimers()
        {
            myTestTimer = new System.Timers.Timer();
            myTestTimer.Interval = 100;//500ms
            myTestTimer.Elapsed  += this.TimerTickHandle;

            myHeartBeatTimer = new System.Timers.Timer();
            myHeartBeatTimer.Interval = 1000;//10s
            myHeartBeatTimer.Elapsed += this.HBTimerTickHandle;
            myHeartBeatTimer.Enabled = true;
        }

        /// <summary>
        /// Init DPH Label.
        /// </summary>
        private void InitDPH()
        {
            DPH = 0;
        }

        /// <summary>
        /// Init ExtTime Label.
        /// </summary>
        private void InitExcuteTime()
        {
            ExcuteTime = 0;
        }

        /// <summary>
        /// Set DPH To Assigned Number.
        /// </summary>
        /// <param name="dph"></param>
        private void SetDPH(int dph)
        {
            DPH = dph;
        }

        /// <summary>
        /// Set ExtTime To Assigned Number.
        /// </summary>
        /// <param name="extTime"></param>
        private void SetEXT(int extTime)
        {
            ExcuteTime = extTime;
        }
        
        private void ErrorHandler(int errorCode, string errorMsg)
        {
            string errMsgstr = "ErrorCode:" + errorCode + "\r\nErrorMessage:" + errorMsg;
            //MessageBox.Show(errMsgstr, "Error Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            LogError(errMsgstr);
        }

        /// <summary>
        /// Get System DateTime.
        /// For Log output
        /// </summary>
        /// <returns>DateTime string</returns>
        private String GetNowDateTimeString()
        {
            DateTime dt = DateTime.Now;//get now DateTime.
            return dt.ToString("yyyy-MM-dd HH:mm:ss.fffff");//format now DateTime: 2013-6-19 09:45:30.12345
        }

        bool seqHBFlag = true;
        private void RefrushSeqHeartbeatLED(bool isOK)
        {
            Color c;

            if(seqHBFlag){
                c = isOK ? Color.Green : Color.Red;
            }
            else{
                c = Color.Transparent;
            }
            seqHBFlag = !seqHBFlag;
            SetSeqLedColor(c);
        }

        private void SetSeqLedColor(Color c)
        {
            try
            {
                if (lb_SeqHB_LED.InvokeRequired)
                {
                    lb_SeqHB_LED.BeginInvoke(new Dlgt_Color(SetSeqLedColor), c);
                    return;
                }
                else
                {
                    lb_SeqHB_LED.BackColor = c;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            //Application.DoEvents();
        }

        bool smHBFlag = true;
        private void RefrushSMHeartbeatLED(bool isOK)
        {
            Color c;

            if (smHBFlag)
            {
                c = isOK ? Color.Green : Color.Red;
            }
            else
            {
                c = Color.Transparent;
            }
            smHBFlag = !smHBFlag;
            SetSmLedColor(c);
        }

        private void SetSmLedColor(Color c)
        {
            try
            {
                if (lb_SMHB_LED.InvokeRequired)
                {
                    lb_SMHB_LED.BeginInvoke(new Dlgt_Color(SetSmLedColor), c);
                    return;
                }
                else
                {
                    lb_SMHB_LED.BackColor = c;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            //Application.DoEvents();
        }

        #region//LoggerWindow Related!//

        private void ClearLogWindow()
        {
            try
            {
                if (null == rtb_LogWindow) return;
                if (rtb_LogWindow.InvokeRequired)
                {
                    this.BeginInvoke(new Dlgt(ClearLogWindow));
                    return;
                }
                else
                {
                    rtb_LogWindow.Clear();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            //Application.DoEvents();
        }

        /// <summary> 
        /// Append log text in windows.
        /// </summary> 
        /// <param name="color">text color</param> 
        /// <param name="logStr">The text string need to display.</param> 
        public void WriteLog(Color color, string logStr)
        {
            SetLogColor(color);
            AppendLogString(logStr);
        }

        public void AppendLogString(string str)
        {
            try
            {
                if (null == this.rtb_LogWindow) return;

                if (this.rtb_LogWindow.InvokeRequired)
                {
                    this.BeginInvoke(new Action<string>(AppendLogString), str);
                    return;
                }
                else
                {
                    this.rtb_LogWindow.AppendText(str + "\r\n");
                    //this.rtb_LogWindow.ScrollToCaret();
                    //
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void SetLogColor(Color c)
        {
            try
            {
                if (null == this.rtb_LogWindow) return;
                if (this.rtb_LogWindow.InvokeRequired)
                {
                    this.BeginInvoke(new Dlgt_Color(SetLogColor), c);
                    return;
                }
                else
                {
                    //this.rtb_LogWindow.SuspendLayout(); 
                    this.rtb_LogWindow.SelectionColor = c;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary> 
        /// Show error msg in logger window.
        /// </summary> 
        /// <param name="text"></param> 
        public void LogError(string text)
        {
            WriteLog(Color.Red, GetNowDateTimeString() + " [Error] "+text);
        }

        /// <summary> 
        /// Show warn msg in logger window.
        /// </summary> 
        /// <param name="text"></param> 
        public void LogWarn(string text)
        {
            WriteLog(Color.Yellow, GetNowDateTimeString() + " [Warn] "+ text);
        }

        /// <summary> 
        /// Show info msg in logger window.
        /// </summary> 
        /// <param name="text"></param> 
        public void LogInfo(string text)
        {
            WriteLog(Color.Black, GetNowDateTimeString() + " [Info] " + text);
        }

        #endregion//LoggerWindow Related...End//

        #region //TMListener Related!//

        /// <summary>
        /// Binding TMListener's Event.
        /// </summary>
        public void BindSeqListenerEvents(ref SequencerListener listener)
        {
            //--- Bind Events of SeqListener with local function ---//
            //Component msg to ui display
            listener.evt_LogInfo  += OnSeqLogInfo;
            listener.evt_LogWarn  += OnSeqLogWarn;
            listener.evt_LogError += OnSeqLogError;
            listener.evt_SequencerMessage += OnSeqOriginalMsgHandle;   //Bind Msg Received Event

            //Bind Seq Events
            listener.evt_SeqStart  += OnSeqStartHandle;
            listener.evt_SeqEnd    += OnSeqEndHandle;
            listener.evt_SeqItemStart += OnSeqItemStartHandle;
            listener.evt_SeqItemEnd   += OnSeqItemEndHandle;
            listener.evt_SeqReportError += OnSeqReportErrHandle;
            listener.evt_SeqUopDetect += OnSeqUopDetectHandle;
            listener.evt_SeqHeartBeat += OnSeqHeartbeatHandle;
            //Bind Attribute Found Event
            listener.evt_SeqAttributeFound += OnSeqAttributeFoundHandle;

            //myHeartBeatTimer.Enabled = true;
        }

        public void UnbindSeqListenerEvents(ref SequencerListener listener)
        {
            //--- Bind Events of SeqListener with local function ---//
            //Component msg to ui display
            listener.evt_LogInfo -= OnSeqLogInfo;
            listener.evt_LogWarn -= OnSeqLogWarn;
            listener.evt_LogError -= OnSeqLogError;
            listener.evt_SequencerMessage -= OnSeqOriginalMsgHandle;
            //Bind Seq Events
            listener.evt_SeqStart -= OnSeqStartHandle;
            listener.evt_SeqEnd -= OnSeqEndHandle;
            listener.evt_SeqItemStart -= OnSeqItemStartHandle;
            listener.evt_SeqItemEnd  -= OnSeqItemEndHandle;
            listener.evt_SeqReportError -= OnSeqReportErrHandle;
            listener.evt_SeqUopDetect -= OnSeqUopDetectHandle;
            listener.evt_SeqHeartBeat -= OnSeqHeartbeatHandle;
            //Bind Attribute Found Event
            listener.evt_SeqAttributeFound -= OnSeqAttributeFoundHandle;

            //myHeartBeatTimer.Enabled = false;
        }

        public void BindNetEvents(ref SequencerListener listener, ref SeqRequester requester)
        {
            BindSeqListenerEvents(ref listener);
            requester.evt_ItemList += myItemsList.ItemListHandle;
        }

        public void UnbindNetEvents(ref SequencerListener listener, ref SeqRequester requester)
        {
            UnbindSeqListenerEvents(ref listener);
            requester.evt_ItemList -= myItemsList.ItemListHandle;
        }

        public void BindTMSCEvents(ref TMSyncConnector tmsc)
        {
            tmsc.evt_Message += OnTMSyncOriginalMsg;
            tmsc.evt_LogInfo     += OnTMSyncLogInfo;
            tmsc.evt_NewSN       += OnTMSyncNewSN;
            tmsc.evt_NewErrorReport += OnTMSyncNewErrorReport;
        }

        public void UnbindTMSCEvents(ref TMSyncConnector tmsc)
        {
            tmsc.evt_Message -= OnTMSyncOriginalMsg;
            tmsc.evt_LogInfo     -= OnTMSyncLogInfo;
            tmsc.evt_NewSN       -= OnTMSyncNewSN;
            tmsc.evt_NewErrorReport -= OnTMSyncNewErrorReport;
        }

        public void BindSMCEvents(ref StateMachineConnector smc)
        {
            smc.evt_OriginalMsg += OnSMOriginalMsg;
            smc.evt_LogInfo += OnSMInfo;
            smc.evt_LogWarn += OnSMWarn;
            smc.evt_LogError += OnSMError;
            //
            smc.evt_SM_Heartbeat += OnSMHeartbeatEvent;
            //
            smc.evt_SM_State_Idle += OnSMStateIdleEvent;
            smc.evt_SM_State_Testing += OnSMStateTestingEvent;
            smc.evt_SM_State_TestDone += OnSMStateTestDoneEvent;
            smc.evt_SM_State_Load += OnSMStateLoadedEvent;
            smc.evt_SM_State_Unload += OnSMStateUnloadEvent;
            smc.evt_SM_State_Error += OnSMStateErrorEvent;
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
            smc.evt_OriginalMsg -= OnSMOriginalMsg;
            smc.evt_LogInfo -= OnSMInfo;
            smc.evt_LogWarn -= OnSMWarn;
            smc.evt_LogWarn -= OnSMError;
            //
            smc.evt_SM_Heartbeat -= OnSMHeartbeatEvent;
            //
            smc.evt_SM_State_Idle -= OnSMStateIdleEvent;
            smc.evt_SM_State_Testing -= OnSMStateTestingEvent;
            smc.evt_SM_State_TestDone -= OnSMStateTestDoneEvent;
            smc.evt_SM_State_Load -= OnSMStateLoadedEvent;
            smc.evt_SM_State_Unload -= OnSMStateUnloadEvent;
            smc.evt_SM_State_Error -= OnSMStateErrorEvent;
            //
            smc.evt_SM_NoTest -= OnSMNotTestEvent;
            smc.evt_SM_NoTest_FailCnt -= OnSMNotTestFailCntEvent;
            smc.evt_SM_NoTest_FailLoad -= OnSMNotTestFailLoadEvent;
            smc.evt_SM_NoTest_FailCsv -= OnSMNotTestFailCSVEvent;
            smc.evt_SM_NoTest_FailReq -= OnSMNotTestFailReqEvent;
            smc.evt_SM_NoTest_FailUnknow -= OnSMNotTestFailUnknowEvent;
        }

        public void BindEngineListenerEvents(ref EngineConnector egnc)
        {
            //--- Bind Events of SeqListener with local function ---//
            if (null == egnc)
            {
                ShowErrorMessage("Bind Engine Events Failed, Because It's Not Exist!");
                return;
            }

            //Component msg to ui display
            egnc.evt_LogInfo += OnSeqLogInfo;
            egnc.evt_LogWarn += OnSeqLogWarn;
            egnc.evt_LogError += OnSeqLogError;
            egnc.evt_OriginalMsg += EngineOriginalMsgHandle;   //Bind Msg Received Event
            //Bind HeartBeat Event
            egnc.evt_Engine_HEARTBEAT += EngineHeartbeatHandle;
        }

        public void UnbindEngineListenerEvents(ref EngineConnector egnc)
        {
            //--- Unbind Events of SeqListener with local function ---//
            if (null == egnc)
            {
                ShowErrorMessage("Unbind Engine Events Failed, Because It's Not Exist!");
                return;
            }

            //Component msg to ui display
            egnc.evt_LogInfo -= OnSeqLogInfo;
            egnc.evt_LogWarn -= OnSeqLogWarn;
            egnc.evt_LogError -= OnSeqLogError;
            egnc.evt_OriginalMsg -= EngineOriginalMsgHandle;   //Bind Msg Received Event
            //Bind HeartBeat Event
            egnc.evt_Engine_HEARTBEAT -= EngineHeartbeatHandle;
        }

        #endregion //TMListener Related!...End//

        private void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, ChamberName+"-ErrorReport", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarnMessage(string msg)
        {
            MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion//Methods...End//

        #region//Events//

        /// <summary>
        /// Test Timer Tick Event Handle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTickHandle(object sender, EventArgs e)
        {
            ExcuteTime += (int)myTestTimer.Interval;

            if (ExcuteTime > Common.Consts.NoTestEndAlarmTime)
            {
                myTestTimer.Enabled = false;
                string errMsgStr = GetNowDateTimeString() + "\r\n-ChamberName:" + ChamberName + "\r\nSequence End OverTime.";
                ErrorHandler((int)ErrorCode.ERR_SEQ_END_OVERTIME, errMsgStr);
            }
        }

        private void HBTimerTickHandle(object sender, EventArgs e)
        {
            RefrushSeqHeartbeatLED(IsSeqHeartBeatOK);
            RefrushSMHeartbeatLED(IsSMHeartBeatOK);
            if(hbCounter++ > 7)
            {
                IsSeqHeartBeatOK = GetSeqHeartBeatStatus();
                IsSMHeartBeatOK = GetSMHeartBeatStatus();
                hbCounter = 1;
            }
            HandleItemEndList();
            
        }
        
        private void HandleItemEndList()
        {
            try
            {
                if (null == myItemEndList) return;
	            if (0 == myItemEndList.Count) return;
	            else
	            {
                    //for (int i = 0; i < myItemEndList.Count; i++)
                    //{
                    //    myItemsList.ItemEndHandle(this, myItemEndList[i]);
                    //}
                    //myItemEndList.Clear();

                    lock (myItemEndList)
                    {
                        foreach (SeqItemEndArgs arg in myItemEndList)//Handle Item End...                    {
                        {
                            using (SeqItemEndArgs sieArg = arg)
                            {
                                myItemsList.ItemEndHandle(this, sieArg);
                                SeqItemEndArgs item = arg;
                                //myItemEndBuffer.Add(sieArg);
                            }
                        }
                        myItemEndList.Clear();
                    }

                    //foreach (SeqItemEndArgs arg in myItemEndBuffer)//Handle Item End...
                    //{
                    //    myItemsList.ItemEndHandle(this, arg);
                    //}

                    //myItemEndBuffer.Clear();
                    //System.GC.Collect();
	            }
			}
			catch(Exception ex)
            {
                //throw ex;
            }
        }


        /// <summary>
        /// Call This When New Lot Info Is Loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void NewLotHandle(object sender, EventArgs arg)
        {
            //TODO.
            //Do Something like clear statistics info...
            chamberSimple.InitStatistics();
        }

        #region //SeqListener Related//
        
        /// <summary>
        /// Handle sequencer msg received event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void OnSeqOriginalMsgHandle(object sender, string originalMsg)
        {
            //System.Diagnostics.Debug.WriteLine(originalMsg);
        }

        /// <summary>
        /// Handle sequence start event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqStartHandle(object sender, SeqStartArgs arg)
        {
            Update_SEQ_HB_DT();

            if(!isLastSeqEnded)//No End
            {
                string errMsgStr = GetNowDateTimeString()+"\r\n-ChamberName:" + ChamberName + "\r\nSequenceEnd Signal Lost!!!";
                ErrorHandler((int)ErrorCode.ERR_SEQ_END_LOST, errMsgStr);
            }
            isLastSeqEnded = false;
            myTestTimer.Enabled = true;
            InitExcuteTime();
            ClearLogWindow();

            string msg = "Slot:"+arg.Slot+"\r\nVersion:" + arg.version + "\r\nname:" + arg.name;
            LogInfo("Sequencer Start Event:\r\n" + msg);

            using (SeqStartArgs stArg = arg)
            {
                object obj = sender;
                chamberSimple.OnSeqStartHandle(obj, stArg);
                myItemsList.SeqStartHandle(obj, stArg);
            }
        }

        /// <summary>
        /// Handle sequence end event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqEndHandle(object sender, SeqEndArgs arg)
        {
            Update_SEQ_HB_DT();
            isLastSeqEnded = true;

            myTestTimer.Enabled = false;

            string str = "Slot:"+arg.Slot+"\r\nResult:" + arg.result+"\r\nLog:" + arg.logs;
            LogInfo("Sequencer End Event:\r\n" + str);

            using (SeqEndArgs seArg = arg)
            {
                object obj = sender;
                chamberSimple.OnSeqEndHandle(obj, seArg);
                myItemsList.SeqEndHandle(obj, seArg);
            }
        }

        /// <summary>
        /// Handle test item start event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqItemStartHandle(object sender, SeqItemStartArgs arg)
        {
            Update_SEQ_HB_DT();
            //string str = "Slot:" + arg.slot + "\r\nGroup:" + arg.group + "\r\nTid:" + arg.tid;
            //LogInfo("Item Start Event Received!\r\n" + str);

            //chamberSimple.ItemStartHandle(sender, arg);
            //myItemsList.ItemStartHandle(sender, arg);
        }

        /// <summary>
        /// Handle test item end event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqItemEndHandle(object sender, SeqItemEndArgs arg)
        {
            Update_SEQ_HB_DT();

            //string str = "Slot:" + arg.slot + "\r\nResult:" + arg.result + "\r\nTid:" + arg.tid;
            //LogInfo("Item End Event Received!\r\n" + str);
            myItemEndList.Add(arg);

            //chamberSimple.ItemEndHandle(sender, arg);
            //myItemsList.ItemEndHandle(sender, arg);
        }

        /// <summary>
        /// Handle test item error event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqReportErrHandle(object sender, SeqReportErrorArgs arg)
        {
            Update_SEQ_HB_DT();
            LogError("Sequencer Error Event:\r\n"+arg.ToString());
        }

        /// <summary>
        /// UOP Detect Event Handle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void OnSeqUopDetectHandle(object sender, SeqUopDetectArgs arg)
        {
            Update_SEQ_HB_DT();
            LogInfo("Sequencer UopDectect Event:\r\n"+arg.ToString());
        }

        /// <summary>
        /// Handle Sequence heartbeat event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqHeartbeatHandle(object sender, SeqHeartBeatArgs arg)
        {
            Update_SEQ_HB_DT();
            //string str = "Slot:" + arg.slot + " Publisher:" + arg.publisher;
            //LogInfo("Sequencer HeartBeat Received!\r\n"+str+"\r\n");
        }

        /// <summary>
        /// Handle Attribute Found event
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqAttributeFoundHandle(object sender, SeqAttrFoundArgs arg)
        {
            Update_SEQ_HB_DT();
            //string str = "Slot:" + arg.slot + "\r\nName:" + arg.name + "\r\nValue:" + arg.value;
            //LogInfo("AttributeFound Event Received!\r\n" + str);
            using(SeqAttrFoundArgs safArg = arg)
            {
                object obj = sender;
                chamberSimple.OnSeqAttributeFoundHandle(obj, safArg);
            }
        }

        #endregion//SeqListener Related...End//

        #region//TMSycConnector Related//

        private void OnTMSyncOriginalMsg(object sender, string msgStr)
        {
            //Console.WriteLine(msgStr);
            LogInfo(msgStr);
        }

        private void OnTMSyncLogInfo(object sender, string msgStr)
        {
            //Console.WriteLine(msgStr);
            LogInfo(msgStr);
        }

        private void OnTMSyncNewSN(object sender, TMSyncSNInfo snInfo)
        {
            string str = "New Sn Info Received! MLBSN:" + snInfo.DutSN + "SocketSN:" + snInfo.SocketSN;
            //Console.WriteLine(str);
            LogInfo(str);
            SocketSN = snInfo.SocketSN;
            DutSN = snInfo.DutSN;

            //SetMLBSN(snInfo.MLBSN);
            //SetSocketSN(snInfo.SOCKETSN);
            //TMErrorInfo err = new TMErrorInfo();
            //err.errCode = "-99999";
            //err.errMsg = "Some Error in TMSync!";
            //OnNewErrorReport(this, err);
        }

        private void OnTMSyncNewErrorReport(object sender, TMSyncErrorInfo errInfo)
        {
            string errStr = "Error Code:" + errInfo.ErrCode + "\r\nError Msg:" + errInfo.ErrMsg;
            MessageBox.Show(errStr, "TMSync Error Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion//TMSycConnector Related//

        #region//SMConnector Related//

        public void OnSMInfo(object sender, string msg)
        {
            LogInfo(msg);
        }

        public void OnSMWarn(object sender, string msg)
        {
            LogWarn(msg);
        }

        public void OnSMError(object sender, string msg)
        {
            LogError(msg);
        }

        private void OnSMOriginalMsg(object sender, string msg)
        {
            //System.Diagnostics.Debug.WriteLine(msg);
            //LogWarning(msg);
        }

        private void OnSMHeartbeatEvent(object sender, SMHeartBeatEventArgs arg)
        {
            //string msgStr = arg.ToString();
            //chamberSimple.OnSMHeartbeatEvent(sender, arg);
            Update_SM_HB_DT();
            //LogInfo(msgStr);
        }

        private void OnSMStateIdleEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            Update_SM_HB_DT();
            LogInfo(msgStr);
        }

        private void OnSMStateTestingEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            Update_SM_HB_DT();
            LogInfo(msgStr);
        }

        private void OnSMStateTestDoneEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            LogInfo(msgStr);
        }

        private void OnSMStateLoadedEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            LogInfo(msgStr);
        }

        private void OnSMStateUnloadEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            LogInfo(msgStr);
        }

        private void OnSMStateErrorEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            Update_SM_HB_DT();
            LogError(msgStr);
        }

        private void OnSMNotTestEvent(object sender, SMStateEventArgs arg)
        {
            Update_SM_HB_DT();
            string msgStr = arg.ToString();
            LogWarn(msgStr);

            using (SMStateEventArgs smArg = arg)
            {
                object obj = sender;
                chamberSimple.OnSMNotTestEvent(obj, smArg);
            }
        }

        private void OnSMNotTestFailCntEvent(object sender, SMStateEventArgs arg)
        {
            Update_SM_HB_DT();
            string msgStr = arg.ToString();
            LogWarn(msgStr);

            using (SMStateEventArgs smArg = arg)
            {
                object obj = sender;
                chamberSimple.OnSMNotTestFailCntEvent(obj, smArg);
            }
        }

        private void OnSMNotTestFailLoadEvent(object sender, SMStateEventArgs arg)
        {
            Update_SM_HB_DT();
            string msgStr = arg.ToString();
            LogWarn(msgStr);

            using (SMStateEventArgs smArg = arg)
            {
                object obj = sender;
                chamberSimple.OnSMNotTestFailLoadEvent(obj, smArg);
            }
        }

        private void OnSMNotTestFailCSVEvent(object sender, SMStateEventArgs arg)
        {
            Update_SM_HB_DT();
            string msgStr = arg.ToString();
            LogWarn(msgStr);

            using (SMStateEventArgs smArg = arg)
            {
                object obj = sender;
                chamberSimple.OnSMNotTestFailCSVEvent(obj, smArg);
            }
        }

        private void OnSMNotTestFailReqEvent(object sender, SMStateEventArgs arg)
        {
            Update_SM_HB_DT();
            string msgStr = arg.ToString();
            LogWarn(msgStr);

            using (SMStateEventArgs smArg = arg)
            {
                object obj = sender;
                chamberSimple.OnSMNotTestFailReqEvent(obj, smArg);
            }
        }

        private void OnSMNotTestFailUnknowEvent(object sender, SMStateEventArgs arg)
        {
            Update_SM_HB_DT();
            string msgStr = arg.ToString();
            LogWarn(msgStr);

            using (SMStateEventArgs smArg = arg)
            {
                object obj = sender;
                chamberSimple.OnSMNotTestFailUnknowEvent(obj, smArg);
            }
        }

        #endregion//SMConnector Related...End//

        #region//EngineConnector Related//

        /// <summary>
        /// Handle test engine msg received event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void EngineOriginalMsgHandle(object sender, string originalMsg)
        {
            //OnLogInfo(this, "---Original Msg From Sequencer---" + originalMsg);

        }

        /// <summary>
        /// Handle Engine heartbeat event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void EngineHeartbeatHandle(object sender, EngineHeartBeatEventArgs arg)
        {
            Update_Engine_HB_DT();
        }

        #endregion//EngineConnector Related...End//

        /// <summary>
        /// Control's Load Event Handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChamberUnit_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }

        private void ChamberUnit_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        #endregion//Events...End//

    }
}
