#region#Copyright Information#
///Created by JimmyGong 2017.01.01
///For IA978 Project UI Design.
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
//
using DMTec.TMListener;
using DMTec.TestUIFramework.Common;
using DMTec.TestUIFramework.DataModel;
//
namespace DMTec.TestUIFramework.AdvancedControls
{
    public partial class Tester : Component
    {

        #region//Members//

        #region//Fields//
        
        string _lotID;

        string _testerID;

        bool _isTester_Enable = true;

        bool _canManualEnabel = true;

        bool _isChamber1_Enable = true;

        bool _isChamber2_Enable = true;

        string _chamber1_IP;

        string _chamber2_IP;

        string _socket1_SN;

        string _socket2_SN;

        string _chamber1_ID;

        string _chamber2_ID;

        string _chamber1_Address;

        string _chamber2_Address;

        string _testType;
        
        #endregion//Fields...End//

        private bool isBindSMEvent1  = false;
        private bool isBindSMEvent2  = false;
        private bool isBindSEQEvent1 = false;
        private bool isBindSEQEvent2 = false;

        private readonly object objLockStartSM = new object();

        TesterSimple myTesterSimple;

        TesterUnit myTesterUnit;

        SequencerListener mySeqListener1;

        SequencerListener mySeqListener2;

        StateMachineConnector mySMListener1;

        StateMachineConnector mySMListener2;

        //System.Timers.Timer myTimer;
       
        #region//HeartBeat Related//

        /// <summary>
        /// The flag of Chamber1 heart beat OK.
        /// </summary>
        private bool _isHeartBeatOK_C1 = false;

        /// <summary>
        /// The flag of Chamber2 heart beat OK.
        /// </summary>
        private bool _isHeartBeatOK_C2 = false;


        /// <summary>
        /// The Time Span Standard Of Last HeartBeat DataTime and Now.
        /// Less Than This, The HeartBeat OK Flag Will Be False.
        /// </summary>
        private int _heartBeatCheckInterval = 7000;

        #endregion//HeartBeat Related...End//

        #region//Public Events//

        /// <summary>
        /// For ALC Binding To Popup Error Msg.
        /// </summary>
        public TestErrorHandler evt_2ALC_PopupError;

        /// <summary>
        /// For ALC Binding To Display Sequencer Msg.
        /// </summary>
        public TestInfoHandler evt_2ALC_SequencerMsg;

        /// <summary>
        /// For ALC Binding To Display UI Msg.
        /// </summary>
        public MsgHandler evt_2ALC_UIMsg;

        /// <summary>
        /// For ALC Binding To Get Test End Result Info.
        /// </summary>
        public TestResultHandler evt_2ALC_TestResult;

        /// <summary>
        /// For ALC Binding To Notify Chamber Enable Changed.
        /// </summary>
        public ChamberEnableHandler evt_2ALC_ChamberEnableChanged;

        #endregion//Public Events...End//

        #endregion//Members...End//

        #region//Properties//

        /// <summary>
        /// The Property of LotID.
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
                myTesterUnit.LotID = value;
            }
        }

        /// <summary>
        /// The Property of Tester ID.
        /// </summary>
        public string TesterID
        {
            get
            {
                return _testerID;
            }
            set
            {
                _testerID = value;
                myTesterSimple.TesterID = value;
                myTesterUnit.TesterID = value;
            }
        }
        
        /// <summary>
        /// The Property of Tester Type.
        /// </summary>
        public string TesterType
        {
            get
            {
                return _testType;
            }
            set
            {
                _testType = value;
            }
        }

        /// <summary>
        /// The Property of Chamber1's ID.
        /// </summary>
        public string Chamber1_ID
        {
            get
            {
                return _chamber1_ID;
            }
            set
            {
                _chamber1_ID = value;
                myTesterSimple.Chamber1_ID = value;
                myTesterUnit.Chamber1_ID = value;
            }
        }

        /// <summary>
        /// The Property of Chamber2's ID.
        /// </summary>
        public string Chamber2_ID
        {
            get
            {
                return _chamber2_ID;
            }
            set
            {
                _chamber2_ID = value;
                myTesterSimple.Chamber2_ID = value;
                myTesterUnit.Chamber2_ID = value;
            }
        }

        /// <summary>
        /// The Property of Logger Enable.
        /// </summary>
        public bool IsLoggerEnable { get; set; }

        /// <summary>
        /// The Property of Tester Enable.
        /// </summary>
        public bool IsTesterEnable 
        { 
            get
            {
                return _isTester_Enable;
            }
            set
            {
                //if (_isTester_Enable == value) return;
                _isTester_Enable = value;
                myTesterSimple.IsTesterEnable = value;
                myTesterUnit.IsTesterEnable = value;
            }
        }

        /// <summary>
        /// The Property of Chamber1 Is Enabled.
        /// </summary>
        public bool IsChamber1_Enable
        {
            get
            {
                return _isChamber1_Enable;
            }
            set
            {
                if (_isChamber1_Enable == value) return;
                _isChamber1_Enable = value;
                TesterEnableCheck();
                myTesterSimple.Chamber1_Enable = value;
                myTesterUnit.Chamber1_Enable = value;
                SendChamberEnableEvent(Chamber1_ID, value);
            }
        }

        /// <summary>
        /// The Property of Chamber2 Is Enabled.
        /// </summary>
        public bool IsChamber2_Enable
        {
            get
            {
                return _isChamber2_Enable;
            }
            set
            {
                if (_isChamber2_Enable == value) return;
                _isChamber2_Enable = value;
                TesterEnableCheck();
                myTesterSimple.Chamber2_Enable = value;
                myTesterUnit.Chamber2_Enable = value;
                SendChamberEnableEvent(Chamber2_ID, value);
            }
        }

        /// <summary>
        /// Is allow to enable tester or chambers manually.
        /// </summary>
        public bool CanManualEnable
        {
            get
            {
                return _canManualEnabel;
            }
            set
            {
                _canManualEnabel = value;
                myTesterSimple.CanManualEnable = value;
                myTesterUnit.CanManualEnable = value;
            }
        }

        public string Chamber1_IP
        {
            get
            {
                return _chamber1_IP;
            }
            set
            {
                _chamber1_IP = value;
                //TODO
            }
        }

        public string Chamber2_IP
        {
            get
            {
                return _chamber2_IP;
            }
            set
            {
                _chamber2_IP = value;
            }
        }

        public string Chamber1_Address
        {
            get
            {
                return _chamber1_Address;
            }
            set
            {
                _chamber1_Address = value;
                myTesterSimple.Chamber1_Address = value;
                myTesterUnit.Chamber1_Address = value;
            }
        }

        public string Chamber2_Address
        {
            get
            {
                return _chamber2_Address;
            }
            set
            {
                _chamber2_Address = value;
                myTesterSimple.Chamber2_Address = value;
                myTesterUnit.Chamber2_Address = value;
            }
        }

        private string Socket1_SN
        {
            get
            {
                return _socket1_SN;
            }
            set
            {
                _socket1_SN = value;
                myTesterSimple.Chamber1_SocketSN = value;
                myTesterUnit.Chamber1_SocketSN = value;
            }
        }

        private string Socket2_SN
        {
            get
            {
                return _socket2_SN;
            }
            set
            {
                _socket2_SN = value;
                myTesterSimple.Chamber2_SocketSN = value;
                myTesterUnit.Chamber2_SocketSN = value;
            }
        }

        public int Chamber1_DPH
        {
            get
            {
                return myTesterUnit.Chamber1_DPH;
            }
            set
            {
                myTesterUnit.Chamber1_DPH = value;
            }
        }

        public int Chamber2_DPH
        {
            get
            {
                return myTesterUnit.Chamber2_DPH;
            }
            set
            {
                myTesterUnit.Chamber2_DPH = value;
            }
        }

        private string _c1DutSn = "N/A";
        public string Chamber1_DutSN
        {
            get { return _c1DutSn; }
            set { _c1DutSn = value; }
        }

        private string _c2DutSn = "N/A";
        public string Chamber2_DutSN
        {
            get { return _c2DutSn; }
            set { _c2DutSn = value; }
        }

        private string _c1SktSn = "N/A";
        public string Chamber1_SocketSN
        {
            get { return _c1SktSn; }
            set { 
                _c1SktSn = value;
                SetSocketID(1, value);
            }
        }

        private string _c2SktSn = "N/A";
        public string Chamber2_SocketSN
        {
            get { return _c2SktSn; }
            set { 
                _c2SktSn = value;
                SetSocketID(2, value);
            }
        }

        private bool _isC1HeartbeatOK = false;
        public bool IsC1HeartBeatOK
        {
            get{ return _isC1HeartbeatOK; }
            set{ _isC1HeartbeatOK = value; }
        }

        private bool _isC2HeartbeatOK = false;
        public bool IsC2HeartBeatOK
        {
            get{ return _isC2HeartbeatOK; }
            set{ _isC2HeartbeatOK = value; }
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
                myTesterUnit.YieldLowLimit = value;
                myTesterSimple.YieldLowLimit = value;
            }
        }

        private int _yieldCountRange = 0;
        public int YieldCountRange
        {
            get
            {
                return _yieldCountRange;
            }
            set
            {
                _yieldCountRange = value;
                myTesterUnit.YieldCountRange = value;
                myTesterSimple.YieldCountRange = value;
            }
        }

        #endregion//Properties...End//

        #region//Constructors//

        public Tester()
        {
            InitializeComponent();
            Init();
        }

        public Tester(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            Init();
        }
        
        #endregion//Constructors...End//

        #region//Methods//

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            myTesterUnit = new TesterUnit(this);
            myTesterSimple = new TesterSimple(this);

            mySeqListener1 = new SequencerListener();

            mySeqListener1.IsOriginalMsgOutput = false;
            BindSeqEvent1();

            mySeqListener2 = new SequencerListener();
            mySeqListener2.IsOriginalMsgOutput = false;
            BindSeqEvent2();

            mySMListener1 = new StateMachineConnector();
            mySMListener1.IsOriginalMsgOutput = true;
            BindSMEvent1();

            mySMListener2 = new StateMachineConnector();
            mySMListener2.IsOriginalMsgOutput = true;
            BindSMEvent2();

            CanManualEnable = true;
            //InitTimer();
        }

        private void InitTimer()
        {
            //myTimer = new Timer();
            //myTimer.Interval = 10000;//xxxms
            //myTimer.Elapsed += this.TimerTickHandle;
            //myTimer.Enabled = true;
        }

        private void BindSeqEvent1()
        {
            if (null == mySeqListener1) return;
            if (isBindSEQEvent1) return;

            mySeqListener1.evt_SeqAttributeFound += AttributeFoundHandle_C1;
            //mySeqListener1.evt_SeqStart    += SeqStartHandle_C1;           
            mySeqListener1.evt_SeqEnd += SeqEndHandle_C1;
            //mySeqListener1.evt_SeqItemStart   += ItemStartHandle_C1;        
            //mySeqListener1.evt_SeqItemEnd     += ItemEndHandle_C1;        
            //mySeqListener1.evt_SeqHeartBeat   += HeartbeatHandle_C1;       
            mySeqListener1.evt_SeqReportError += ReportErrorHandle_C1;
            
            myTesterUnit.GetChamberUnit(1).BindSeqListenerEvents(ref mySeqListener1);
            myTesterSimple.GetChamberSimple(1).BindSeqListenerEvents(ref mySeqListener1);
            
            isBindSEQEvent1 = true;
        }

        private void UnbindSeqEvent1()
        {
            if (null == mySeqListener1) return;
            if (!isBindSEQEvent1) return;

            //mySeqListener1.evt_SeqAttrFound -= AttributeFoundHandle_C1;
            //mySeqListener1.evt_SeqStart -= SeqStartHandle_C1;
            mySeqListener1.evt_SeqEnd -= SeqEndHandle_C1;
            //mySeqListener1.evt_SeqItemStart -= ItemStartHandle_C1;
            //mySeqListener1.evt_SeqItemEnd -= ItemEndHandle_C1;
            //mySeqListener1.evt_SeqHeartBeat -= HeartbeatHandle_C1;
            mySeqListener1.evt_SeqReportError -= ReportErrorHandle_C1;

            myTesterUnit.GetChamberUnit(1).UnbindSeqListenerEvents(ref mySeqListener1);
            myTesterSimple.GetChamberSimple(1).UnbindSeqListenerEvents(ref mySeqListener1);
            isBindSEQEvent1 = false;
        }

        private void BindSeqEvent2()
        {
            if (null == mySeqListener2) return;
            if (isBindSEQEvent2) return;

            mySeqListener2.evt_SeqAttributeFound += AttributeFoundHandle_C2;
            //mySeqListener2.evt_SeqStart    += SeqStartHandle_C2;   
            mySeqListener2.evt_SeqEnd += SeqEndHandle_C2;
            //mySeqListener2.evt_SeqItemStart   += ItemStartHandle_C2;
            //mySeqListener2.evt_SeqItemEnd     += ItemEndHandle_C2;
            //mySeqListener2.evt_SeqHeartBeat   += HeartbeatHandle_C2;
            mySeqListener2.evt_SeqReportError += ReportErrorHandle_C2;

            myTesterUnit.GetChamberUnit(2).BindSeqListenerEvents(ref mySeqListener2);
            myTesterSimple.GetChamberSimple(2).BindSeqListenerEvents(ref mySeqListener2);
            isBindSEQEvent2 = true;
        }

        private void UnbindSeqEvent2()
        {
            if (null == mySeqListener2) return;
            if (!isBindSEQEvent2) return;

            //mySeqListener2.evt_SeqAttrFound -= AttributeFoundHandle_C2;
            //mySeqListener2.evt_SeqStart    -= SeqStartHandle_C2;
            mySeqListener2.evt_SeqEnd -= SeqEndHandle_C2;
            //mySeqListener2.evt_SeqItemStart   -= ItemStartHandle_C2;
            //mySeqListener2.evt_SeqItemEnd     -= ItemEndHandle_C2;
            //mySeqListener2.evt_SeqHeartBeat   -= HeartbeatHandle_C2;
            mySeqListener2.evt_SeqReportError -= ReportErrorHandle_C2;

            myTesterUnit.GetChamberUnit(2).UnbindSeqListenerEvents(ref mySeqListener2);
            myTesterSimple.GetChamberSimple(2).UnbindSeqListenerEvents(ref mySeqListener2);
            isBindSEQEvent2 = false;
        }

        private void BindSMEvent1()
        {
            if (null == mySMListener1) return;
            if (isBindSMEvent1) return;

            myTesterUnit.GetChamberUnit(1).BindSMCEvents(ref mySMListener1);
            myTesterSimple.GetChamberSimple(1).BindSMCEvents(ref mySMListener1);

            isBindSMEvent1 = true;
        }

        private void UnbindSMEvent1()
        {
            if (null == mySMListener1) return;
            if (!isBindSMEvent1) return;

            myTesterUnit.GetChamberUnit(1).UnbindSMCEvents(ref mySMListener1);
            myTesterSimple.GetChamberSimple(1).UnbindSMCEvents(ref mySMListener1);

            isBindSMEvent1 = false;
        }

        private void BindSMEvent2()
        {
            if (null == mySMListener2) return;
            if (isBindSMEvent2) return;

            myTesterUnit.GetChamberUnit(2).BindSMCEvents(ref mySMListener2);
            myTesterSimple.GetChamberSimple(2).BindSMCEvents(ref mySMListener2);

            isBindSMEvent2 = true;
        }

        private void UnbindSMEvent2()
        {
            if (null == mySMListener2) return;
            if (!isBindSMEvent2) return;

            myTesterUnit.GetChamberUnit(2).UnbindSMCEvents(ref mySMListener1);
            myTesterSimple.GetChamberSimple(2).UnbindSMCEvents(ref mySMListener1);

            isBindSMEvent1 = false;
        }

        /// <summary>
        /// Init Chamber1's Net Connection.
        /// </summary>
        private void EnableNet1(bool isEnable)
        {
            if(isEnable)
            {
                //myListener1.StartListener();
                BindSeqEvent1();
                BindSMEvent1();
                
            }
            else//Disable
            {
                //myListener1.SetKeepRun(false);
                UnbindSeqEvent1();
                UnbindSMEvent1();
            }
        }

        /// <summary>
        /// Init Chamber2's Net Connection.
        /// </summary>
        private void EnableNet2(bool isEnable)
        {
            if(isEnable)
            {
                //myListener2.StartListener();
                BindSeqEvent2();
                BindSMEvent2();
            }
            else//Disable
            {
                //myListener2.SetKeepRun(false);
                UnbindSeqEvent2();
                UnbindSMEvent2();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void TesterEnableCheck()
        {
            if (_isChamber1_Enable && _isChamber2_Enable)
            {
                IsTesterEnable = true;
            }
            else if (!_isChamber1_Enable && !_isChamber2_Enable)
            {
                IsTesterEnable = false;
            }
        }

        private void SetSocketID(int chamberNo, string sktID)
        {
            if (1 == chamberNo)
            {
                myTesterUnit.GetChamberUnit(1).SocketSN = sktID;
                myTesterSimple.GetChamberSimple(1).SocketSN = sktID;
            }
            else if (2 == chamberNo)
            {
                myTesterUnit.GetChamberUnit(2).SocketSN = sktID;
                myTesterSimple.GetChamberSimple(2).SocketSN = sktID;
            }
            else { }

        }

        /// <summary>
        /// Get The Instance TesterSimple Object Class For Outside Operate.
        /// </summary>
        /// <returns></returns>
        public TesterSimple GetSimple()
        {
            return myTesterSimple;
        }

        /// <summary>
        /// Get The Instance TesterSimple Object Class For Outside Operate.
        /// </summary>
        /// <returns></returns>
        public TesterUnit GetUnit()
        {
            return myTesterUnit;
        }

        /// <summary>
        /// Start TMListener.
        /// </summary>
        /// <param name="chamberNo"></param>
        /// <returns></returns>
        public int StartSeqListener(int chamberNo)
        {
            if (1 == chamberNo)
            {
                string address = "tcp://" + Chamber1_IP + ":" + "6250";
                int rst =  mySeqListener1.StartListener(address);
                //if (0 == rst) NLogger.Debug("Tester:" + TesterID + "-Chamber:" + chamberNo + "---Listener Started!" + "---Address:" + address);
                //else NLogger.Debug("Tester:" + TesterID + "-Chamber:" + chamberNo + "---Listener Start Failed !!!" + "---Address:" + address);
                return rst;
            }
            else if(2 == chamberNo)
            {
                string address = "tcp://" + Chamber2_IP + ":" + "6250";
                int rst = mySeqListener2.StartListener(address);
                //if (0 == rst) NLogger.Debug("Tester:" + TesterID + "-Chamber:" + chamberNo + "---Listener Started!" + "---Address:" + address);
                //else NLogger.Debug("Tester:" + TesterID + "-Chamber:" + chamberNo + "---Listener Start Failed !!!" + "---Address:" + address);
                return rst;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Stop TMListener.
        /// </summary>
        /// <param name="chamberNo"></param>
        /// <returns></returns>
        public int StopSeqListener(int chamberNo)
        {
            if (1 == chamberNo)
            {
                //myListener1.SetKeepRun(false);
                mySeqListener1.StopListener();
                return 0;
            }
            else if (2 == chamberNo)
            {
                //myListener2.SetKeepRun(false);
                mySeqListener2.StopListener();
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Start TMListener.
        /// </summary>
        /// <param name="chamberNo"></param>
        /// <returns></returns>
        public int StartSMListener(int chamberNo)
        {
            lock (objLockStartSM)
            {
                if (1 == chamberNo)
                {
                    //string address = "tcp://" + Chamber1_IP + ":" + "6580";
                    //int rst = mySMListener1.StartHeartbeatListen(address);

                    string address = "tcp://" + Chamber1_IP + ":" + "6880";
                    int rst = mySMListener1.StartListener(address);

                    //if (0 == rst) NLogger.Debug("Tester:" + TesterID + "-Chamber:" + chamberNo + "---SM Listener Started!" + "---Address:" + address);
                    //else NLogger.Debug("Tester:" + TesterID + "-Chamber:" + chamberNo + "---SM Listener Start Failed !!!" + "---Address:" + address);

                    return rst;
                }
                else if (2 == chamberNo)
                {
                    //string address = "tcp://" + Chamber2_IP + ":" + "6580";
                    //int rst = mySMListener2.StartHeartbeatListen(address);

                    string address = "tcp://" + Chamber2_IP + ":" + "6880";
                    int rst = mySMListener2.StartListener(address);

                    //if (0 == rst) NLogger.Debug("Tester:" + TesterID + "-Chamber:" + chamberNo + "---SM Listener Started!" + "---Address:" + address);
                    //else NLogger.Debug("Tester:" + TesterID + "-Chamber:" + chamberNo + "---SM Listener Start Failed !!!" + "---Address:" + address);
                    return rst;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// Stop TMListener.
        /// </summary>
        /// <param name="chamberNo"></param>
        /// <returns></returns>
        public int StopSMListener(int chamberNo)
        {
            if (1 == chamberNo)
            {
                mySMListener1.Stop();
                return 0;
            }
            else if (2 == chamberNo)
            {
                mySMListener2.Stop();
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chamberNo"></param>
        /// <param name="tested"></param>
        /// <param name="passed"></param>
        public void SetChamberStatistic(int chamberNo, int tested, int passed, int untested)
        {
            if (1 == chamberNo || 2 == chamberNo)
            {
                myTesterSimple.GetChamberSimple(chamberNo).SetStatistic(tested, passed, untested);
                myTesterUnit.GetChamberUnit(chamberNo).GetChamberSimple().SetStatistic(tested, passed, untested);
            }
        }

        #region //Chamber1 Events//

        /// <summary>
        /// Handle Chamber1's Sequence Start Event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void SeqStartHandle_C1(object sender, SeqStartArgs arg)
        {
            //Debug.WriteLine("---[Tester: "+TesterID+"]---[Chamber1]---SeqStart Event Received.");
            //NLogger.Debug(arg.ToString());

            IsC1HeartBeatOK = true;   
        }

        /// <summary>
        /// Handle Chamber1's Sequence End Event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void SeqEndHandle_C1(object sender, SeqEndArgs arg)
        {
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber1]---SeqEnd Event Received.");
            //NLogger.Debug(arg.ToString());
            //IsC1HeartBeatOK = true;
            using (SeqEndArgs seqEndArg = arg)
            {
                SendTestEndEvent(Chamber1_ID, Chamber1_DutSN, seqEndArg);
            }
        }

        /// <summary>
        /// Handle Chamber1's Item Start Event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void ItemStartHandle_C1(object sender, SeqItemStartArgs arg)
        {
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber1]---ItemStartEvent Received.");
            //NLogger.Debug("---[Tester: " + TesterID + "]---[Chamber1]---ItemStartEvent Received.");

            //myTesterSimple.GetChamberSimple(1).ItemStartHandle(sender, arg);
            //myTesterUnit.GetChamberUnit(1).ItemStartHandle(sender, arg);
        }

        /// <summary>
        /// Handle Chamber1's Item End Event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void ItemEndHandle_C1(object sender, SeqItemEndArgs arg)
        {
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber1]---ItemEnd Event Received.");
            //NLogger.Debug("---[Tester: " + TesterID + "]---[Chamber1]---ItemEnd Event Received.");

            //myTesterSimple.GetChamberSimple(1).ItemEndHandle(sender, arg);
            //myTesterUnit.GetChamberUnit(1).ItemEndHandle(sender, arg);
        }

        /// <summary>
        /// Handle Attribute Found Event
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void AttributeFoundHandle_C1(object sender, SeqAttrFoundArgs arg)
        {
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber1]---AttributeFound Event Received.");
            //NLogger.Debug("---[Tester: " + TesterID + "]---[Chamber1]---AttributeFound Event Received.");
            string module = arg.Publisher;
            string name = arg.name;
            string value = arg.value;

            if ("MLBSN" == name)
            {
                Chamber1_DutSN = value;
            }
            else if ("SocketSN" == name)
            {
                Chamber1_SocketSN = value;
            }
            else { }
            
        }

        /// <summary>
        /// Handle Chamber1's Sequence heartbeat event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void HeartbeatHandle_C1(object sender, SeqHeartBeatArgs arg)
        {
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber1]---Heartbeat Event Received.");
            //NLogger.Debug("---[Tester: " + TesterID + "]---[Chamber1]---Heartbeat Event Received.");
            IsC1HeartBeatOK = true;
            //_lastHeartBeatDT_C1 = DateTime.Now;

        }

        /// <summary>
        /// Chamber 1 Report Error Event Handle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void ReportErrorHandle_C1(object sender, SeqReportErrorArgs arg)
        {
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber1]---ReportError Event Received.");
            //string errMsg = "---[Tester: " + TesterID + "]---[Chamber1]---Error!" + "\r\nError Code:" + arg.error_code.ToString() + "\r\nSite:" + arg.site.ToString() + "\r\nSlot:" + arg.slot.ToString() + "\r\nError String:" + arg.value;
            //NLogger.Error(arg.ToString());

            using(SeqReportErrorArgs sreArg = arg)
            {
                ReprotError2ALC(TesterID, Chamber1_ID, sreArg.error_code, sreArg.value);
            }
        }
        #endregion//Chamber1 Events...End//

        #region //Chamber2 Events//

        /// <summary>
        /// Chamber 2 Report Error Event Handle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private void ReportErrorHandle_C2(object sender, SeqReportErrorArgs arg)
        {
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber2]---ReportError Event Received.");
            //string errMsg = "---[Tester: " + TesterID + "]---[Chamber2]---Error!" + "\r\nError Code:" + arg.error_code.ToString() + "\r\nSite:" + arg.site.ToString() + "\r\nSlot:" + arg.slot.ToString() + "\r\nError String:" + arg.value;
            //NLogger.Debug(arg.ToString());

            using(SeqReportErrorArgs sreArg = arg)
            {
                ReprotError2ALC(TesterID, Chamber2_ID, sreArg.error_code, sreArg.value);
            }
        }

        /// <summary>
        /// Handle Chamber2's Sequence heartbeat event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void HeartbeatHandle_C2(object sender, SeqHeartBeatArgs arg)
        {
            //_lastHeartBeatDT_C2 = DateTime.Now;
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber2]---Heartbeat Event Received.");
            IsC2HeartBeatOK = true;
            //NLogger.Debug("---[Tester: " + TesterID + "]---[Chamber2]---Heartbeat Event Received.");
        }

        /// <summary>
        /// Handle Chamber2's Sequence Start Event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void SeqStartHandle_C2(object sender, SeqStartArgs arg)
        {
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber2]---SeqStartEvent Received.");
            //NLogger.Debug(arg.ToString());
            IsC2HeartBeatOK = true;
            //myTesterSimple.GetChamberSimple(2).SeqStartHandle(sender, arg);
            //myTesterUnit.GetChamberUnit(2).SeqStartHandle(sender, arg);
        }

        /// <summary>
        /// Handle Chamber2's Sequence End Event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void SeqEndHandle_C2(object sender, SeqEndArgs arg)
        {
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber2]---SeqEndEvent Received.");
            //NLogger.Debug(arg.ToString());
            //IsC2HeartBeatOK = true;
            using(SeqEndArgs seArg = arg)
            {
                SendTestEndEvent(Chamber2_ID, Chamber2_DutSN, seArg);
            }
            //myTesterSimple.GetChamberSimple(2).SeqEndHandle(sender, arg);
            //myTesterUnit.GetChamberUnit(2).SeqEndHandle(sender, arg);
        }

        /// <summary>
        /// Handle Chamber2's Item Start Event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void ItemStartHandle_C2(object sender, SeqItemStartArgs arg)
        {
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber2]---ItemStart Event Received.");
            //NLogger.Debug("---[Tester: " + TesterID + "]---[Chamber2]---ItemStart Event Received.");
            
            //myTesterSimple.GetChamberSimple(2).ItemStartHandle(sender, arg);
            //myTesterUnit.GetChamberUnit(2).ItemStartHandle(sender, arg);
        }

        /// <summary>
        /// Handle chamber2's Item End Event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void ItemEndHandle_C2(object sender, SeqItemEndArgs arg)
        {
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber2]---ItemEnd Event Received.");
            //NLogger.Debug("---[Tester: " + TesterID + "]---[Chamber2]---ItemEnd Event Received.");
            
            //myTesterSimple.GetChamberSimple(2).ItemEndHandle(sender, arg);
            //myTesterUnit.GetChamberUnit(2).ItemEndHandle(sender, arg);
        }

        /// <summary>
        /// Handle chamber2's Attribute Found event
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        private void AttributeFoundHandle_C2(object sender, SeqAttrFoundArgs arg)
        {
            //Debug.WriteLine("---[Tester: " + TesterID + "]---[Chamber2]---AttributeFound Event Received.");
            //NLogger.Debug("---[Tester: " + TesterID + "]---[Chamber2]---AttributeFound Event Received.");

            string module = arg.Publisher;
            string name = arg.name;
            string value = arg.value;

            if ("MLBSN" == name)
            {
                Chamber2_DutSN = value;
            }
            else if ("SocketSN" == name)
            {
                Chamber2_SocketSN = value;
            }
            else { }
        }

        #endregion//Chamber2 Events...End//
        
        #region //To ALC Events//
        
        /// <summary>
        /// Test Timer Tick Event Handle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTickHandle(object sender, EventArgs e)
        {
            myTesterSimple.GetChamberSimple(1).IsSeqHBOk = IsC1HeartBeatOK;
            myTesterUnit.GetChamberUnit(1).GetChamberSimple().IsSeqHBOk = IsC1HeartBeatOK;
            IsC1HeartBeatOK = false;
            
            myTesterSimple.GetChamberSimple(2).IsSeqHBOk = IsC2HeartBeatOK;
            myTesterUnit.GetChamberUnit(2).GetChamberSimple().IsSeqHBOk = IsC2HeartBeatOK;
            IsC2HeartBeatOK = false;
        }

        /// <summary>
        /// Active Error Event and ALC Will Popup Dialog To Display Error Msg. 
        /// </summary>
        /// <param name="errCode"></param>
        /// <param name="errStr"></param>
        private void ReprotError2ALC(string testerId, string chamberId, int errCode, string errStr)
        {
            using(ErrorEventArgs arg = new ErrorEventArgs())
            {
                arg.errCode = errCode;
                arg.errMsg = errStr;

                if (this.evt_2ALC_PopupError != null)
                {
                    //evt_2ALC_PopupError.Invoke(sender, errStr);
                    evt_2ALC_PopupError.BeginInvoke(testerId, chamberId, arg, null, null);
                }
            }
        }

        /// <summary>
        /// Active Error Event and ALC Will Popup Dialog To Display Error Msg. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SendTestEndEvent(string chamberName, string dutSn, SeqEndArgs args)
        {
            bool isTestOk = args.result == "1" ? true : false;

            if (this.evt_2ALC_TestResult != null)
            {
                //evt_2ALC_TestResult.Invoke(TesterID, chamberNo, isTestOk);
                evt_2ALC_TestResult.BeginInvoke(TesterID, chamberName, dutSn, isTestOk, null, null);
            }
        }

        /// <summary>
        /// Active Error Event and ALC Will Know Which Chamber Is Enabled/Disabled. 
        /// </summary>
        /// <param name="chamberNo"></param>
        /// <param name="isEnable"></param>
        private void SendChamberEnableEvent(string chamberName, bool isEnable)
        {
            if (null != evt_2ALC_ChamberEnableChanged)
            {
                evt_2ALC_ChamberEnableChanged.BeginInvoke(TesterID, chamberName, isEnable, null, null);
            }
        }
        
        #endregion//To ALC Events...End//

        #endregion//Methods...End//

    }
}
