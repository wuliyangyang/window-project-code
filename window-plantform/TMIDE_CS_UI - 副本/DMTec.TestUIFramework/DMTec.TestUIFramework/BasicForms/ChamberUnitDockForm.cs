#region#Copyright Information#
///Created by JimmyGong 2017.01.01
///For IA Project UI Design.
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
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using DMTec.TMListener;
using DMTec.TestUIFramework.DataModel;
using DMTec.TestUIFramework.CommonAPI;
using DMTec.TestUIFramework.Common;
using DMTec.TestUIFramework.AdvancedControls;
//
namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ChamberUnitDockForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {

        #region//Constructors//

        /// <summary>
        /// Constructor.
        /// </summary>
        public ChamberUnitDockForm()
        {
            InitializeComponent();
            //chamberSimple = new ChamberSimple();
            //InitUI();
            InitMembers();
        }

        /// <summary>
        /// Constructor(with Assigned ChamberName).
        /// </summary>
        /// <param name="chamberNo"></param>
        public ChamberUnitDockForm(int chamberNo)//
        {
            InitializeComponent();
            ChamberName = "C" + chamberNo.ToString();
            ChamberNo = chamberNo;
            chamberSimple = new ChamberSimple(chamberNo);
            InitUI();
            InitMembers();
        }

        /// <summary>
        /// New One Instance with one assigned chamber's configuration.
        /// </summary>
        /// <param name="config"></param>
        public ChamberUnitDockForm(ChamberConfig config)//
        {
            InitializeComponent();

            this.tlp_Main.SuspendLayout();
            this.SuspendLayout();

            myConfig = config;
            ChamberName = config.ChamberID;
            chamberSimple.evt_StatisticsClear += OnStatisticsClear;
            chamberSimple.SetStatisticsClearButtonVisible(false);
            chamberSimple.YieldCountRange = 0;//No dynamic count yield.
            InitMembers();
            myStatistics = JsonHelper.ReadJsonFile<ChamberStatistics>(GetStatisticsFilePath()) as ChamberStatistics;
            if (null == myStatistics) myStatistics = new ChamberStatistics();
            chamberSimple.SetStatisticsDisplay(myStatistics);

            myHardwareData = ReadHardwareData(GetHardwareDataFilePath());
            if (null == myHardwareData) myHardwareData = new ChamberHardwareData();

            mySeqListener = new SequencerListener();
            mySeqListener.IsOriginalMsgOutput = config.IsOriginalMsgOutput;
            BindSeqListenerEvents(ref mySeqListener);
            SubAddress2Seq = config.SequencerSubAddress;
            mySeqListener.SubscribeAddress = config.SequencerSubAddress;

            myTMSyncConnector = new TMSyncConnector();

            myTMSyncConnector.IsSpectrographAlarm = config.IsSpectrometerAlarm;
            myTMSyncConnector.IsSpectrographDrain = false;

            myTMSyncConnector.IsPogoPinAlarm = config.IsPogoPinAlarm;
            myTMSyncConnector.IsPogoPinDrain = true;

            BindTMSyncConnectorEvents(ref myTMSyncConnector);
            myTMSyncConnector.ReplyAddress = config.TMSyncRepAddress;

            mySMConnector = new StateMachineConnector();
            BindSMCEvents(ref mySMConnector);
            mySMConnector.SubscribeAddress = config.StateMachineSubAddress;

            myEngineConnector = new EngineConnector();
            BindEngineListenerEvents(ref myEngineConnector);
            myEngineConnector.SubscribeAddress = config.EngineSubAddress;

            myHBPanel = new ChamberHeartbeatPanel();
            myHBPanel.Dock = DockStyle.Fill;
            tlp_Main.Controls.Add(myHBPanel, 0, 0);

            myLastResultPanel = new LastResultPanel();
            //myLastResultPanel.Dock = DockStyle.Fill;
            //tlp_InfoPanel.Controls.Add(myLastResultPanel, 0, 0);

            myOutsideDutPanel = new OutsideDutStatusPanel();//Added by JM 2017.06.14
            if(config.IsShowOutsideDutStatus)
            {
                myOutsideDutPanel.Dock = DockStyle.Fill;
                tlp_Main.Controls.Add(myOutsideDutPanel, 0, 2);
            }
            else
            {
                this.tlp_Main.RowStyles[2].Height = 0;
            }

            //if (config.IsShowLastResult)
            //{
            //    myLastResultPanel = new LastResultPanel();
            //    myLastResultPanel.Dock = DockStyle.Fill;
            //    tlp_InfoPanel.Controls.Add(myLastResultPanel, 0, 0);
            //}
            //else
            //{
            //    //this.tlp_Main.RowStyles[2].Height = 0;
            //}
            myHardwareInfoPanel = new HardwareInfoPanel();
            if(config.IsShowHardwareInfo)
            {
                myHardwareInfoPanel.Dock = DockStyle.Fill;
                tlp_Main.Controls.Add(myHardwareInfoPanel, 0, 8);
            }
            else
            {
                this.tlp_Main.RowStyles[8].Height = 0;
            }

            if (!config.IsNeedScanBarcode)
            {
                this.tlp_Main.RowStyles[0].Height = 0; //删除第1行
                //this.tlp_Main.Controls.Remove(barcodeInputBox);
                //this.tlp_Main.RowCount--;
            }
            else
            {
                myDutSnInputBox = new DutSNInputPanel();
                this.tlp_Main.Controls.Add(myDutSnInputBox, 0, 1);
                //myDutSnInputBox.evt_SendSocketBarcode += OnNewSocketSNInputHandle;
                myDutSnInputBox.evt_NewDutBarcode += OnNewDutSNInputHandle;
            }

            if(config.IsSpectrometerAlarm)
            {
                UpdateSpectrometerLeftTime();
                CheckIsSpectrometerExpire();
            }

            if(config.IsPogoPinAlarm) CheckIsPogoPinExpire(myHardwareData.CurrentPogoPinTested);
            
            this.tlp_Main.PerformLayout();
            this.tlp_Main.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion//Constructors...End//

        #region//Members//

        private int  hbCounter = 1;
        private bool isLastSeqEnded = true;
        private bool isNewDutTest = false;
        private string lastDutSN = "N/A";
        private string lastSktSN = "N/A";
        private string lastDutResult = "Wait";
        private string newDutSN = "";
        private string newSktSN = "";
        private int spectrometerLeftTime = 0;
        private int pogopinLeftTimes = 0;
        //
        private Tester myTester;
        //
        private ChamberConfig myConfig;
        private ChamberStatistics myStatistics;
        private ChamberHardwareData myHardwareData;
        //
        private SequencerListener mySeqListener;
        private TMSyncConnector myTMSyncConnector;
        private StateMachineConnector mySMConnector;
        private EngineConnector myEngineConnector;
        private FlexPublisher myPublisher;
        //
        private DutSNInputPanel myDutSnInputBox;
        private ChamberHeartbeatPanel myHBPanel;
        private LastResultPanel myLastResultPanel;
        private OutsideDutStatusPanel myOutsideDutPanel;//
        private HardwareInfoPanel myHardwareInfoPanel;
        //
        private System.Timers.Timer myTestTimer;//For Execute Time Display
        private System.Timers.Timer myHeartBeatTimer;//For Heartbeat Led Update
        //
        public delegate void Dlgt_Color_String(Color color, string text);
        public delegate void Dlgt_String(string str);
        public delegate void Dlgt_Color(Color c);
        public delegate void Dlgt();

        public delegate void NewSnHandler(string sn);
        public NewSnHandler NewDutSnReceived;
        public NewSnHandler NewSktSnReceived;

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
        /// The flag of Test Engine heart beat is OK.
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

        bool _isShowLastResult = false;
        /// <summary>
        /// If need show the last test result
        /// </summary>
        public bool IsShowLastTestResult
        {
            get
            {
                return _isShowLastResult;
            }
            set
            {
                _isShowLastResult = value;
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
                _isEngineHeartBeatOK = value;
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
        [Browsable(true), Description("Set and get <SeqSubAddress> string"), Category("CustomProperties")]
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
                //chamberSimple.SocketSN = _socketSN;
            }
        }

        /// <summary>
        /// The Property of DUT Serial Number.
        /// </summary>
        public string DutSN
        {
            get
            {
                return _dutSN;
            }
            set
            {
                _dutSN = value;
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
        /// The Property of chamber's configuration.
        /// </summary>
        public ChamberConfig Config
        {
            get
            {
                return myConfig;
            }
            set
            {
                myConfig = value;
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
                return Convert.ToInt32(lable_ExtTime.Text);
            }
            set
            {
                SetExtTime(value.ToString());
            }
        }
        private void SetExtTime(string str)
        {
            if (InvokeRequired)
            {
                Invoke(new Dlgt_String(SetExtTime), str);
            }
            else
            {
                lable_ExtTime.Text = str;
            }
        }

        /// <summary>
        /// The Property of DPH(DUTs Per Hour).
        /// </summary>
        public int DPH
        {
            get
            {
                //return Convert.ToInt32(label_DPH.Text);
                return _uph;
            }
            set
            {
                _uph = value;
                SetDPH(value.ToString());
            }
        }
        private void SetDPH(string str)
        {
            if (InvokeRequired)
            {
                Invoke(new Dlgt_String(SetDPH), str);
            }
            else
            {
                label_DPH.Text = str;
            }
        }

        #endregion//Properties//

        #region//Events//

        /// <summary>
        /// Test Timer Tick Event Handle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTickHandle(object sender, EventArgs e)
        {
            ExcuteTime += (int)myTestTimer.Interval;
        }

        private void HBTimerTickHandle(object sender, EventArgs e)
        {
            //RefrushSeqHeartbeatLED(IsSeqHeartBeatOK);
            //RefrushSMHeartbeatLED(IsSMHeartBeatOK);
            //RefrushEngineHeartbeatLED(IsEngineHeartBeatOK);
            myHBPanel.SetBulbStatus(IsSeqHeartBeatOK, IsSMHeartBeatOK, IsEngineHeartBeatOK);
            if (hbCounter++ > 7)
            {
                IsSeqHeartBeatOK = GetSeqHeartBeatStatus();
                IsSMHeartBeatOK  = GetSMHeartBeatStatus();
                IsEngineHeartBeatOK = GetEngineHeartBeatStatus();
                hbCounter = 1;
            }
            UpdateSpectrometerLeftTime();
        }

        /// <summary>
        /// Control's Load Event Handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChamberUnit_Load(object sender, EventArgs e)
        {
            if(null != mySeqListener) mySeqListener.StartListener();
            if (null != myTMSyncConnector) myTMSyncConnector.Start();
            if (null != mySMConnector) mySMConnector.Start();
            if (null != myEngineConnector) myEngineConnector.StartListener();
            myHeartBeatTimer.Enabled = true;
        }

        /// <summary>
        /// Call This When New Lot Info Is Loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void NewLotHandle(object sender, EventArgs arg)
        {
            chamberSimple.InitStatistics();
        }

        private void OnNewDutSNInputHandle(string snStr)
        {
            newDutSN = snStr;
            DutSN = snStr;
            //chamberSimple.DutSN = DutSN;

            SendSN2TMSync();
            SendNewDutSN(DutSN);
        }

        private void OnNewSocketSNInputHandle(string snStr)
        {
            //newSktSN = snStr;
            SocketSN = snStr;
            //chamberSimple.SocketSN = snStr;
            SendSN2TMSync();
        }

        private void SendSN2TMSync()
        {
            if (string.IsNullOrEmpty(newDutSN)) return;
            if (string.IsNullOrEmpty(newSktSN)) return;

            myTMSyncConnector.SetSN(newDutSN, newSktSN);
            newDutSN = newSktSN = "";
        }

        #region//Sequencer Related//

        /// <summary>
        /// Handle sequencer msg received event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void SeqOriginalMsgHandle(object sender, string originalMsg)
        {
            //OnLogInfo(this, "---Original Msg From Sequencer---" + originalMsg);

        }

        /// <summary>
        /// Handle sequence start event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqStartHandle(object sender, SeqStartArgs arg)
        {
            Update_SEQ_HB_DT();

            if (!isLastSeqEnded)//No End
            {
                string errMsgStr = GetNowDateTimeString() + "\r\nTesterID:" + myTester.TesterID + "-ChamberName:" + ChamberName + "\r\nSequenceEnd Signal Lost!!!";
                ErrorHandler((int)ErrorCode.ERR_SEQ_END_LOST, errMsgStr);
            }
            isLastSeqEnded = false;
            InitExcuteTime();

            myTestTimer.Enabled = true;
            ClearLogWindow();

            string msg = "Slot:" + arg.Slot + "\r\nVersion:" + arg.version + "\r\nname:" + arg.name;
            LogInfo("Sequencer Start Event:\r\n" + msg);

            chamberSimple.OnSeqStartHandle(sender, arg);
            testItemsList.SeqStartHandle(sender, arg);

            PubDutStatus("Start");
        }

        private void PubDutStatus(string status)
        {
            StatusMsg stsMsg = new StatusMsg();//Added by JM 2017.06.14
            stsMsg.ChamberID = ChamberName;
            stsMsg.Status = status;
            myPublisher.PubStatusMsg(stsMsg);
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
            lastDutResult = arg.result;//
            myTestTimer.Enabled = false;

            string str = "Slot:" + arg.Slot + "\r\nResult:" + arg.result + "\r\nLog:" + arg.logs;
            LogInfo("Sequencer End Event:\r\n" + str);

            chamberSimple.OnSeqEndHandle(sender, arg);
            testItemsList.SeqEndHandle(sender, arg);

            if (myConfig.IsShowLastResult)
            {
                myLastResultPanel.OnSeqEndEvent(arg);
            }

            DoStatistics(arg);
            CheckIsSpectrometerExpire();
            CheckIsPogoPinExpire(myHardwareData.CurrentPogoPinTested);

            PubDutResult(arg);//
        }


        private void PubDutResult(SeqEndArgs arg)
        {
            ResultMsg msg = new ResultMsg();
            msg.ChamberID = ChamberName;
            msg.DutSN     = DutSN;
            msg.SocketSN  = SocketSN;
            msg.DutResult = arg.result == Consts.TESTEND_PASS_STRING ? "Pass" : "Fail";
            myPublisher.PubResultMsg(msg);
        }

        /// <summary>
        /// Handle test item start event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqItemStartHandle(object sender, SeqItemStartArgs arg)
        {
            Update_SEQ_HB_DT();
            chamberSimple.OnSeqItemStartHandle(sender, arg);
            testItemsList.ItemStartHandle(sender, arg);
        }

        /// <summary>
        /// Handle test item end event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqItemEndHandle(object sender, SeqItemEndArgs arg)
        {
            Update_SEQ_HB_DT();
            chamberSimple.OnSeqItemEndHandle(sender, arg);
            testItemsList.ItemEndHandle(sender, arg);
            //
        }

        /// <summary>
        /// Handle test item error event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void OnSeqReportErrHandle(object sender, SeqReportErrorArgs arg)
        {
            Update_SEQ_HB_DT();
            LogError("Sequencer Error Event:\r\n" + arg.ToString());

            //ErrorMsg errMsg = new ErrorMsg();//Added by JM 2017.06.14
            //errMsg.ErrorCode = arg.error_code;
            //errMsg.ErrorString = arg.ToString();
            //myPublisher.PubErrorMsg(errMsg);
        }

        /// <summary>
        /// UOP Detect Event Handle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void OnSeqUopDetectHandle(object sender, SeqUopDetectArgs arg)
        {
            Update_SEQ_HB_DT();
            LogInfo("Sequencer UopDectect Event:\r\n" + arg.ToString());
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
            chamberSimple.OnSeqAttributeFoundHandle(sender, arg);
            string module = arg.Publisher;
            string name = arg.name;
            string value = arg.value;

            if ("MLBSN" == name)
            {
                DutSN = value;

                if (DutSN != lastDutSN)
                {
                    myOutsideDutPanel.UpdateResult(lastDutSN, lastDutResult);
                    lastDutSN = DutSN;
                }
                //lastDutSN = DutSN;
            }
            else if ("SocketSN" == name)
            {
                SocketSN = value;
            }
            else { }
            //string str = "Slot:" + arg.slot + "\r\nName:" + arg.name + "\r\nValue:" + arg.value;
            //LogInfo("AttributeFound Event Received!\r\n" + str);
        }

        #endregion//Sequencer Related...End//

        #region//TMSycConnector Related//

        private void OnTMSyncOriginalMsg(object sender, string msgStr)
        {
            LogInfo(msgStr);
        }

        private void OnTMSyncLogInfo(object sender, string msgStr)
        {
            LogInfo(msgStr);
        }

        private void OnTMSyncNewSN(object sender, TMSyncSNInfo snInfo)
        {
            string str = "New Sn Info Received! MLBSN:" + snInfo.DutSN + "SocketSN:" + snInfo.SocketSN;
            LogInfo(str);
            //SocketSN = snInfo.SocketSN;
            //DutSN    = snInfo.DutSN;
            myOutsideDutPanel.UpdateNewSN(snInfo.DutSN);
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
            ErrorMsg errMsg = new ErrorMsg();
            errMsg.ChamberID = ChamberName;
            int code;
            bool isParseOK = Int32.TryParse(errInfo.ErrCode, out code);
            if (!isParseOK) code = 0;
            errMsg.ErrorCode = code;
            errMsg.ErrorString = errInfo.ErrMsg;
            myPublisher.PubErrorMsg(errMsg);
            MessageBox.Show(errStr, "TMSync("+myTMSyncConnector.ReplyAddress+") Error Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion//TMSycConnector Related//

        #region//SMConnector Related//

        public void OnSMInfo(object sender, string msg)
        {
            LogInfo(msg);
        }

        public void OnSMWarn(object sender, string msg)
        {
            LogWarning(msg);
        }

        public void OnSMError(object sender, string msg)
        {
            LogError(msg);
        }

        private void OnSMOriginalMsg(object sender, string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
            LogWarning(msg);
        }

        private void OnSMHeartbeatEvent(object sender, SMHeartBeatEventArgs arg)
        {
            string msgStr = arg.ToString();
            chamberSimple.OnSMHeartbeatEvent(sender, arg);
            Update_SM_HB_DT();
            //LogInfo(msgStr);
        }

        private void OnSMStateIdleEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            LogInfo(msgStr);
        }

        private void OnSMStateTestingEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
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
            chamberSimple.OnSMStateErrorEvent(sender, arg);
            LogError(msgStr);
        }

        private void OnSMNotTestEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            LogWarning(msgStr);
            System.Diagnostics.Debug.WriteLine(msgStr);
            chamberSimple.OnSMNotTestEvent(sender, arg);
        }

        private void OnSMNotTestFailCntEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            LogWarning(msgStr);
            System.Diagnostics.Debug.WriteLine(msgStr);
            chamberSimple.OnSMNotTestFailCntEvent(sender, arg);
        }

        private void OnSMNotTestFailLoadEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            LogWarning(msgStr);
            System.Diagnostics.Debug.WriteLine(msgStr);
            chamberSimple.OnSMNotTestFailLoadEvent(sender, arg);
        }

        private void OnSMNotTestFailCSVEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            LogWarning(msgStr);
            System.Diagnostics.Debug.WriteLine(msgStr);
            chamberSimple.OnSMNotTestFailCSVEvent(sender, arg);
        }

        private void OnSMNotTestFailReqEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            LogWarning(msgStr);
            System.Diagnostics.Debug.WriteLine(msgStr);
            chamberSimple.OnSMNotTestFailReqEvent(sender, arg);
        }

        private void OnSMNotTestFailUnknowEvent(object sender, SMStateEventArgs arg)
        {
            string msgStr = arg.ToString();
            LogWarning(msgStr);
            System.Diagnostics.Debug.WriteLine(msgStr);
            chamberSimple.OnSMNotTestFailUnknowEvent(sender, arg);
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

        #endregion//Events...End//

        #region//Methods//

        #region//Private Methods//

        public void RegisterNewDutSnHandler(NewSnHandler handler)
        {
            NewDutSnReceived += handler;
        }

        private void SendNewDutSN(string sn)
        {
            if (null != NewDutSnReceived)
                NewDutSnReceived.Invoke(sn);
        }

        public void RegisterNewSktSnHandler(NewSnHandler handler)
        {
            NewSktSnReceived += handler;
        }

        private void SendNewSktSn(string sn)
        {
            if (null != NewSktSnReceived)
                NewSktSnReceived.Invoke(sn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private ChamberHardwareData ReadHardwareData(string filePath)
        {
            //object obj = JsonHelper.ReadJsonFile<ChamberHardwareData>(filePath);
            object obj = BinConverter.ReadBinFile<ChamberHardwareData>(filePath);
            
            if (null == obj) return null;
            return obj as ChamberHardwareData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="cbHdData"></param>
        private void SaveHardwareDataFile(string filePath, ChamberHardwareData cbHdData)
        {
            BinConverter.WriteBinFile(filePath, cbHdData);
        }

        /// <summary>
        /// Call This Method When PogoPin Used Times Cleared.
        /// </summary>
        public void ClearHardwareData()
        {
            myHardwareData.CurrentPogoPinTested = 0;
            CheckIsPogoPinExpire(myHardwareData.CurrentPogoPinTested);
            SaveHardwareDataFile(GetHardwareDataFilePath(), myHardwareData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private ChamberStatistics ReadStatisticsData(string filePath)
        {
            object obj = JsonHelper.ReadJsonFile<ChamberStatistics>(GetStatisticsFilePath());
            if (null == obj) return null;
            return obj as ChamberStatistics;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="cbstat"></param>
        private void SaveStatisticsDataFile(string filePath, ChamberStatistics cbstat)
        {
            BinConverter.WriteBinFile(filePath, cbstat);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="str"></param>
        private void SaveChamberDataCsv(string filePath, string str)
        {
            CsvHelper.WriteCSVFile(filePath, str);
        }

        #endregion//Private Methods...End//


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
        /// Get the path string of files where save the statistics data.
        /// </summary>
        /// <returns></returns>
        private string GetStatisticsFilePath()
        {
            //return Consts.CHAMBER_HARDWARE_RUNDATA_DIR_PATH + ChamberName + ".json";
            return Consts.ChamberStatisticsDataSaveDirPath + ChamberName + ".json";

        }

        /// <summary>
        /// Get the path string of files where save the dut test csv data.
        /// </summary>
        /// <returns></returns>
        private string GetCSVDataFilePath()
        {
            //return Consts.CHAMBER_DATA_CSV_DIR_PATH + ChamberName + "/" + DateTime.Now.ToShortDateString() + ".csv";
            return Consts.TestCsvDataSaveDirPath + ChamberName + "/" + DateTime.Now.ToShortDateString() + ".csv";
        }

        /// <summary>
        /// Get the path string of files where save the hardware running data.
        /// </summary>
        /// <returns></returns>
        private string GetHardwareDataFilePath()
        {
            return Consts.HardwareDataSaveDirPath + ChamberName + ".BIN";
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

        private void OnLogInfo(object sender, string msg)
        {
            //string msgString = GetNowDateTimeString() + " " + msg + "\r\n";
            //Console.WriteLine(msgString);
            LogInfo(msg);
        }

        private void OnLogWarn(object sender, string msg)
        {
            //string msgString = GetNowDateTimeString() + " " + msg + "\r\n";
            LogWarning(msg);
            //Console.WriteLine(msgString);
        }

        private void OnLogError(object sender, string msg)
        {
            //string msgString = GetNowDateTimeString() + " " + msg + "\r\n";
            LogError(msg);
            //Console.WriteLine(msgString);
        }

        private void InitUI()
        {
            this.SuspendLayout();
            tlp_Main.Controls.Add(chamberSimple, 0, 1);
            this.ResumeLayout();
        }

        private void InitMembers()
        {
            myStatistics = new ChamberStatistics();
            myHardwareData = new ChamberHardwareData();
            myPublisher = FlexPublisher.GetInstance();
            InitTimers();
        }

        private void InitTimers()
        {
            myTestTimer = new System.Timers.Timer();
            myTestTimer.Interval = 100;//500ms
            myTestTimer.Elapsed  += this.TimerTickHandle;

            myHeartBeatTimer = new System.Timers.Timer();
            myHeartBeatTimer.Interval = 1000;//10s
            myHeartBeatTimer.Elapsed += this.HBTimerTickHandle;
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

        private void InitStatistics()
        {
            myStatistics = new ChamberStatistics();

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
        /// 
        /// </summary>
        /// <param name="arg"></param>
        private void DoStatistics(SeqEndArgs arg)
        {
            myStatistics.Total += 1;
            myHardwareData.CurrentPogoPinTested += 1;

            string resultStr = "";
            if (Consts.TESTEND_PASS_STRING == arg.result)//Pass
            {
                myStatistics.Passed += 1;
                resultStr = "Pass";
            }
            else
            {
                resultStr = "Fail";
            }

            myStatistics.PassRate = (double)myStatistics.Passed * 100 / myStatistics.Total;

            string str = DateTime.Now.ToString() + ",";
            str += DutSN + ",";
            str += resultStr;
//             str += resultStr + ",";
//             str += myStatistics.Tested.ToString() + ",";
//             str += myStatistics.Passed.ToString() + ",";
//             str += myStatistics.PassRate.ToString() + "%";

            SaveChamberDataCsv(GetCSVDataFilePath(), str);

            JsonHelper.WriteJsonFile(GetStatisticsFilePath(), myStatistics);
            //JsonHelper.WriteJsonFile(GetHardwareDataFilePath(), myHardwareData);
            SaveHardwareDataFile(GetHardwareDataFilePath(), myHardwareData);

        }

        private void OnStatisticsClear(object sender, EventArgs arg)
        {
            myStatistics = new ChamberStatistics();
            //
            JsonHelper.WriteJsonFile(GetStatisticsFilePath(), myStatistics);
            //chamberSimple.SetStatisticsDisplay(myStatistics);
            chamberSimple.ResetStatistic();
        }

        private void CheckIsPogoPinExpire(int pogoPinTested)
        {
            int leftTimes = 0;
            if(pogoPinTested >= myConfig.PogoPinTimesLimit)
            {
                leftTimes = 0;
                myTMSyncConnector.IsPogoPinDrain = true;
                ShowMessage(MessageBoxType.Warn, "Warning", myConfig.ChamberID + " PogoPin is run out of times, please change a new one!");
            }
            else
            {
                leftTimes = myConfig.PogoPinTimesLimit - pogoPinTested;
                myTMSyncConnector.IsPogoPinDrain = false;
            }
            myHardwareInfoPanel.SetPogoPinLeftTimes(leftTimes);
            return;
        }

        private void UpdateSpectrometerLeftTime()
        {
            TimeSpan ts = DateTime.Now - myConfig.SpectrometerCalibrateDateTime;
            int usedHours = (int)ts.TotalHours;
            spectrometerLeftTime = (usedHours >= myConfig.SpectrometerTimeLimit) ? 0 : (myConfig.SpectrometerTimeLimit - usedHours);
            myHardwareInfoPanel.SetSpectrographLeftTime(spectrometerLeftTime);
        }

        private void CheckIsSpectrometerExpire()
        {
            if (0 >= spectrometerLeftTime)
            {
                myTMSyncConnector.IsSpectrographDrain = true;
                //MessageBox.Show("Spectrometer is run out of time, please do a calibration!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowMessage(MessageBoxType.Warn, "Warning", myConfig.ChamberID + " Spectrometer is run out of time, please do a calibration!");
                return;
            }
            else
            {
                myTMSyncConnector.IsSpectrographDrain = false;
                return;
            }
        }

        private void ShowMessage(MessageBoxType msgType, string title, string content)
        {
            MessageBoxEx newMsgBox = new MessageBoxEx();
            newMsgBox.ShowMessage(msgType, title, content);
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

        #region//<LED Related>

        bool seqHBFlag = true;
        private void RefrushSeqHeartbeatLED(bool isOK)
        {
            Color c;

            if (seqHBFlag)
            {
                c = isOK ? Color.Green : Color.Red;
            }
            else
            {
                c = Color.Transparent;
            }
            seqHBFlag = !seqHBFlag;
            SetSeqLedColor(c);
        }

        private void SetSeqLedColor(Color c)
        {
            //if (lb_SeqHB_LED.InvokeRequired)
            //{
            //    lb_SeqHB_LED.Invoke(new Dlgt_Color(SetSeqLedColor), c);
            //}
            //else
            //{
            //    lb_SeqHB_LED.BackColor = c;
            //}
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
            //if (lb_SMHB_LED.InvokeRequired)
            //{
            //    lb_SMHB_LED.Invoke(new Dlgt_Color(SetSmLedColor), c);
            //}
            //else
            //{
            //    lb_SMHB_LED.BackColor = c;
            //}
        }

        bool egnHBFlag = true;
        private void RefrushEngineHeartbeatLED(bool isOK)
        {
            myHBPanel.IsEngHBOK = isOK;
            //Color c;

            //if (egnHBFlag)
            //{
            //    c = isOK ? Color.Green : Color.Red;
            //}
            //else
            //{
            //    c = Color.Transparent;
            //}
            //egnHBFlag = !egnHBFlag;

            //SetSmLedColor(c);
        }

        #endregion//<LED Related>

        #region//LoggerWindow Related!//

        private void ClearLogWindow()
        {
            if (rtb_LogWindow.InvokeRequired)
            {
                this.Invoke(new Dlgt(ClearLogWindow));
            }
            else
            {
                rtb_LogWindow.Clear();
            }
        }

        /// <summary> 
        /// Append log text in windows.
        /// </summary> 
        /// <param name="color">text color</param> 
        /// <param name="logStr">The text string need to display.</param> 
        public void AppendLog(Color color, string logStr)
        {
            if(null == rtb_LogWindow) return;
            if (rtb_LogWindow.InvokeRequired)
            {
                this.Invoke(new Action<Color, string>(WriteLogWindowText), color, logStr);
            }
            else
            {
                WriteLogWindowText(color, logStr);
            }
        }

        private void WriteLogWindowText(Color color, string logStr)
        {
            try
            {
                rtb_LogWindow.AppendText("\r\n");
                rtb_LogWindow.SelectionColor = color;
                rtb_LogWindow.AppendText(logStr);
                rtb_LogWindow.ScrollToCaret();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message,"FlexChamberUnit Write Log Window Exception",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary> 
        /// Show error msg in logger window.
        /// </summary> 
        /// <param name="text"></param> 
        public void LogError(string text)
        {
            AppendLog(Color.Red, GetNowDateTimeString() + "---[Error]---" + text);
        }

        /// <summary> 
        /// Show warn msg in logger window.
        /// </summary> 
        /// <param name="text"></param> 
        public void LogWarning(string text)
        {
            AppendLog(Color.Yellow, GetNowDateTimeString() + "---[Warning]---" + text);
        }

        /// <summary> 
        /// Show info msg in logger window.
        /// </summary> 
        /// <param name="text"></param> 
        public void LogInfo(string text)
        {
            AppendLog(Color.Black, GetNowDateTimeString() + "---[Info]---" + text);
        }

        #endregion//

        #region//Database Related!//

        ///// <summary>
        ///// Add One New Record of Sequence End to Database.
        ///// </summary>
        ///// <param name="arg"></param>
        //private void AddOneSeqEndRecord(SeqEndArguments arg)
        //{
        //    string result = arg.result==1?"true":"false";
        //    string cmdText = "insert into chamberTest (RecordDt, LotId, ChamberName, DutSn, Result) VALUES ('" + DateTime.Now.ToString() + "', '" + LotID + "', '" + ChamberName + "','" + DutSN + "','" + result + "')";

        //    int rst = MySqlHelper.ExecuteNonQuery(MySqlHelper.CONNECTION_STRING, CommandType.Text, cmdText, null);
        //}

        ///// <summary>
        ///// Get The DPH Data From Database.
        ///// </summary>
        ///// <returns></returns>
        //private int GetDPHFromDB()
        //{
        //    string cmdText = "select * from chamberTest where ChamberName = '"+ChamberName+"'";
        //    int dph = MySqlHelper.GetDataSet(MySqlHelper.CONNECTION_STRING, CommandType.Text, cmdText, null).Tables[0].DefaultView.Count;
        //    return dph;
        //}

        #endregion//Database Related!//

        #region //TMListener Related!//

        /// <summary>
        /// Binding Sequencer's Events To Trigger UI Display Update.
        /// </summary>
        /// <param name="listener"></param>
        public void BindSeqListenerEvents(ref SequencerListener listener)
        {
            //--- Bind Events of SeqListener with local function ---//
            if (null == listener)
            {
                ShowErrorMessage("Bind Sequencer Events Failed, Because It's Not Exist!");
                return;
            }
            //Component msg to ui display
            listener.evt_LogInfo += OnLogInfo;
            listener.evt_LogWarn += OnLogWarn;
            listener.evt_LogError += OnLogError;
            listener.evt_SequencerMessage += SeqOriginalMsgHandle;   //Bind Msg Received Event
            //
            //Bind SeqStart Event
            listener.evt_SeqStart  += OnSeqStartHandle;
            //Bind SeqEnd Event
            listener.evt_SeqEnd    += OnSeqEndHandle;

            //Bind ItemStart Event
            listener.evt_SeqItemStart += OnSeqItemStartHandle;
            //Bind ItemEnd Event
            listener.evt_SeqItemEnd   += OnSeqItemEndHandle;
            //Bind ErrorReport Event
            listener.evt_SeqReportError += OnSeqReportErrHandle;
            //Bind UopDetect Event
            listener.evt_SeqUopDetect += OnSeqUopDetectHandle;
            //Bind Attribute Found Event
            listener.evt_SeqAttributeFound += OnSeqAttributeFoundHandle;
            //Bind HeartBeat Event
            listener.evt_SeqHeartBeat += OnSeqHeartbeatHandle;
        }

        public void UnbindSequencerEvents(ref SequencerListener listener)
        {
            if (null == listener)
            {
                ShowErrorMessage("Unbind Sequencer Events Failed, Because It's Not Exist!");
                return;
            }

            //--- Bind Events of SeqListener with local function ---//
            //SeqListener msg to ui display
            listener.evt_LogInfo     -= OnLogInfo;
            listener.evt_SequencerMessage -= SeqOriginalMsgHandle;   //Bind Msg Received Event
            //
            //Bind SeqStart Event
            listener.evt_SeqStart -= OnSeqStartHandle;
            //Bind SeqEnd Event
            listener.evt_SeqEnd -= OnSeqEndHandle;
            //Bind ItemStart Event
            listener.evt_SeqItemStart -= OnSeqItemStartHandle;
            //Bind ItemEnd Event
            listener.evt_SeqItemEnd -= OnSeqItemEndHandle;
            //Bind ErrorReport Event
            listener.evt_SeqReportError -= OnSeqReportErrHandle;
            //Bind UopDetect Event
            listener.evt_SeqUopDetect -= OnSeqUopDetectHandle;
            //Bind Attribute Found Event
            listener.evt_SeqAttributeFound -= OnSeqAttributeFoundHandle;
            //Bind HeartBeat Event
            listener.evt_SeqHeartBeat -= OnSeqHeartbeatHandle;
        }

        public void BindTMSyncConnectorEvents(ref TMSyncConnector tmsc)
        {
            if (null == tmsc){
                ShowErrorMessage("Bind TMSyncConnector Failed Because It's Not Exist!");
                return;
            }
            tmsc.evt_Message    += OnTMSyncOriginalMsg;
            tmsc.evt_LogInfo        += OnTMSyncLogInfo;
            tmsc.evt_NewSN          += OnTMSyncNewSN;
            tmsc.evt_NewErrorReport += OnTMSyncNewErrorReport;
        }

        public void UnbindTMSyncConnectorEvents(ref TMSyncConnector tmsc)
        {
            if (null == tmsc){
                ShowErrorMessage("Unbind TMSyncConnector Failed Because It's Not Exist!");
                return;
            }
            tmsc.evt_Message -= OnTMSyncOriginalMsg;
            tmsc.evt_LogInfo -= OnTMSyncLogInfo;
            tmsc.evt_NewSN -= OnTMSyncNewSN;
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
            egnc.evt_LogInfo     += OnLogInfo;
            egnc.evt_LogWarn     += OnLogWarn;
            egnc.evt_LogError    += OnLogError;
            egnc.evt_OriginalMsg += EngineOriginalMsgHandle;   //Bind Msg Received Event
            //Bind HeartBeat Event
            egnc.evt_Engine_HEARTBEAT   += EngineHeartbeatHandle;
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
            egnc.evt_LogInfo   -= OnLogInfo;
            egnc.evt_LogWarn   -= OnLogWarn;
            egnc.evt_LogError  -= OnLogError;
            egnc.evt_OriginalMsg -= EngineOriginalMsgHandle;   //Bind Msg Received Event
            //Bind HeartBeat Event
            egnc.evt_Engine_HEARTBEAT -= EngineHeartbeatHandle;
        }

        #endregion //TMListener Related!...End//

        private void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "ErrorReport", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarnMessage(string msg)
        {
            MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion//Methods...End//

        private void FlexChamberUnit_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
