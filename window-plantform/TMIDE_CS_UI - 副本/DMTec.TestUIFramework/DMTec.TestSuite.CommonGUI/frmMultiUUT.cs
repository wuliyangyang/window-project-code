//
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// Use DMTec TM Components...
using DMTec.TestUIFramework.BasicControls;
using DMTec.TestUIFramework.BasicForms;
using DMTec.TestUIFramework.CommonAPI;
using DMTec.TestUIFramework.DataModel;
using DMTec.TestUIFramework.Interface;
using DMTec.TestUIFramework;
using DMTec.TMListener;
using ZedGraph;
using LogTool;

//
namespace DMTec.TestSuite.CommonGUI
{
    public partial class frmMultiUUT : Form, ILogger
    {

        #region//Members//
        private string sysConfigPath;

        List<DMTec.TMListener.SequencerListener> mySeqListenerList;
        string[] mySeqListenerAddressArray;

        List<DMTec.TMListener.EngineConnector> myEngineConnectorList;
        string[] myEngineConnectorAddressArray;

        List<DMTec.TMListener.StateMachineConnector> myStateMachineConnectorList;
        string[] myStateMachineConnectorAddressArray;
        string[] myStateMachineConnectorRequestAddressArray;

        List<LoggerListener> myLoggerListenerList;
        string[] myLoggerListenerAddressArray;

        //DMTec.TMListener.StateMachineConnector myStateMachineConnector;//Only one SM.


       
        SystemConfigInfo myConfig;

        ZmqPortInfo myZmqPort;

        TabPageLogger systemLogger;

        SaveLogHelper logHelper;

        Loop_Test myLoopTestPanel;

        SingleStatisticsPanel mySingleStatisticsPanel;

        Log_Panel myLogPanel;

        DMTec.TestUIFramework.BasicControls.LoggerTabControl myLoggerTabControl;

        #endregion//Members___END//

        #region//Constructors//

        public frmMultiUUT(string[] args)
        {
            InitializeComponent();
            InitSystem(args);

        }

        #endregion//Constructors___END//

        private void InitSystem(string[] Args)
        {
#if DEBUG
            myTitleBar.OperatorRole = "Debug";
#else
            myTitleBar.OperatorRole = "Release";
#endif

            myTitleBar.VersionString = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string workPath = System.Environment.CurrentDirectory;
            string path1 = workPath + "\\TesterUI\\SysConfig.json";
            string path2 = workPath + "\\ProxUI\\TesterUI\\SysConfig.json";

            myConfig = Global.GetConfigInstance();
            if (myConfig == null) return;


            myLogPanel = new Log_Panel();

            logHelper = new SaveLogHelper();
            logHelper.Log_Evt += LogHelper_Log_Evt;

            myStatisticInfoPanel.SetPath(Global.GetGlobalInstance().GetPath());
            myStatisticInfoPanel.updateStatisticsFromFile();

            mySeqListenerList = new List<DMTec.TMListener.SequencerListener>();
            myLoggerListenerList = new List<LoggerListener>();

            myZmqPort = GetZmqPortInfo(myConfig.ZmqPortConfigFilePath);

            //Init SequenceListener
            mySeqListenerAddressArray = GetSeqListenerAddressList(myZmqPort.SEQUENCER_PUB, myConfig.Slots * myConfig.Modules);
            InitSeqListeners(mySeqListenerAddressArray, out mySeqListenerList);
            //Init EngineListener
            myEngineConnectorAddressArray = GetEngineListenerAddressList(myZmqPort.TEST_ENGINE_PUB, myConfig.Slots * myConfig.Modules);
            InitEngineConnectors(myEngineConnectorAddressArray, out myEngineConnectorList);
            //Init StateMachineConnector
            myStateMachineConnectorAddressArray = GetStateMachineListenerAddressList(myZmqPort.SM_PUB, myConfig.Modules);
            myStateMachineConnectorRequestAddressArray = GetStateMachineRequesterAddressList(myZmqPort.SM_PORT, myConfig.Modules);
            InitStateMachineConnectors(myStateMachineConnectorAddressArray, myStateMachineConnectorRequestAddressArray, out myStateMachineConnectorList);

            //Init LoggerListener
            myLoggerListenerAddressArray = GetLoggerListenerAddressList(myZmqPort.LOGGER_PUB, myConfig.Slots * myConfig.Modules);
            InitLoggerListeners(myLoggerListenerAddressArray, out myLoggerListenerList);

            myTestItemGridView.Slots = myConfig.Slots * myConfig.Modules;
            myTestItemGridView.BindSeqListeners(ref mySeqListenerList);

            myStatisticInfoPanel.BindSequencerListeners(ref mySeqListenerList);
            myStatisticInfoPanel.BindLoggerListeners(ref myLoggerListenerList);


            myUUTStatePanel.Slots = myConfig.Slots * myConfig.Modules;
            myUUTStatePanel.SM_Slots = myConfig.Modules;
            myUUTStatePanel.BindSeqListeners(ref mySeqListenerList);
            myUUTStatePanel.BindLoggerListeners(ref myLoggerListenerList);
            myUUTStatePanel.SetTestItemCount(myConfig.Slots * myConfig.Modules);
            myUUTStatePanel.myStateMachineConnectorList = myStateMachineConnectorList;

            // myStationHeartbeatPanel.BindSeqListeners(ref mySeqListenerList);
            myStationHeartbeatPanel.Modules = myConfig.Modules;
            myStationHeartbeatPanel.Slots = myConfig.Slots;
            myStationHeartbeatPanel.BindStateMachineConnectors(ref myStateMachineConnectorList);
            myStationHeartbeatPanel.BindEngineConnectors(ref myEngineConnectorList);
            myStationHeartbeatPanel.BindSequencerListeners(ref mySeqListenerList);

            //init looptest
            myLoopTestPanel = new Loop_Test();
            myLoopTestPanel.myStateMachineConnectorList = myStateMachineConnectorList;
            myLoopTestPanel.uutPanel = myUUTStatePanel;
            myLoopTestPanel.BindStateMachineConnectors(ref myStateMachineConnectorList);
            myLoopTestPanel.LoopStartTestEvent += new Loop_Test.LoopStartTestHandler(LoopTest);


            mySingleStatisticsPanel = new SingleStatisticsPanel();
            mySingleStatisticsPanel.BindSequencerListeners(ref mySeqListenerList);  

            //Add one logger window in ui.
            myLoggerTabControl = new LoggerTabControl();
            systemLogger = myLoggerTabControl.AddLogger("UI Logger");

            this.tlp_TestItem.Controls.Add(myLoggerTabControl, 0, 1);
            myLoggerTabControl.Dock = DockStyle.Fill;
        }
        private ZmqPortInfo GetZmqPortInfo(string zmqPortFilePath)
        {
            return JsonHelper.ReadJsonFile<ZmqPortInfo>(zmqPortFilePath) as ZmqPortInfo;
        }

#region // Seq SM Eng//
        private int InitSeqListeners(string[] addressList, out List<SequencerListener> seqListenerList)
        {
            seqListenerList = new List<SequencerListener>();

            for (int i = 0; i < addressList.Length; i++)
            {
                SequencerListener listener = new SequencerListener();
                listener.SubscribeAddress = addressList[i];
                listener.evt_SeqStart += Listener_evt_SeqStart;
                listener.evt_SeqEnd += Listener_evt_SeqEnd;
                //listener.IsOriginalMsgOutput = true;
                seqListenerList.Add(listener);
            }
            return 0;
        }
        private void Listener_evt_SeqEnd(object sender, SeqEndArgs arg)
        {

        }
        private void Listener_evt_SeqStart(object sender, SeqStartArgs arg)
        {

        }
        private int InitLoggerListeners(string[] addressList, out List<LoggerListener> loggerListenerList)
        {
            loggerListenerList = new List<LoggerListener>();

            for (int i = 0; i < addressList.Length; i++)
            {
                LoggerListener listener = new LoggerListener();
                listener.SubscribeAddress = addressList[i];
                listener.evt_LoggerEnd += OnLoggerEnd;
                loggerListenerList.Add(listener);
            }
            return 0;
        }
        private string[] GetSerialListenerAddressList(string libPubPort, int slots)
        {
            string[] addressList = new string[slots];
            int port = 0;
            if (false == int.TryParse(libPubPort, out port)) port = 6250;
            for (int i = 0; i < slots; i++)
            {
                string address = string.Format("tcp://{0}:{1}", "127.0.0.1", (port + i).ToString());
                addressList[i] = address;
            }
            return addressList;
        }
        private string[] GetTCPListenerAddressList(string libPubPort, int slots)
        {
            string[] addressList = new string[slots];
            int port = 0;
            if (false == int.TryParse(libPubPort, out port)) port = 6250;
            for (int i = 0; i < slots; i++)
            {
                string address = string.Format("tcp://{0}:{1}", "127.0.0.1", (port + i).ToString());
                addressList[i] = address;
            }
            return addressList;
        }
        private string[] GetSeqListenerAddressList(string seqPubPort, int slots)
        {
            string[] addressList = new string[slots];
            int port = 0;
            if (false == int.TryParse(seqPubPort, out port)) port = 6250;
            for (int i = 0; i < slots; i++)
            {
                string address = string.Format("tcp://{0}:{1}", "127.0.0.1", (port + i).ToString());
                addressList[i] = address;// "tcp://127.0.0.1:6250";
            }
            return addressList;
        }
        private string[] GetEngineListenerAddressList(string enginePubPort, int slots)
        {
            string[] addressList = new string[slots];
            int port = 0;
            if (false == int.TryParse(enginePubPort, out port)) port = 6250;
            for (int i = 0; i < slots; i++)
            {
                string address = string.Format("tcp://{0}:{1}", "127.0.0.1", (port + i).ToString());
                addressList[i] = address;//"tcp://127.0.0.1:6150";
            }
            return addressList;
        }
        private string[] GetStateMachineListenerAddressList(string stateMachinePubPort, int slots)
        {
            string[] addressList = new string[slots];
            int port = 0;
            if (false == int.TryParse(stateMachinePubPort, out port)) port = 6580;
            for (int i = 0; i < slots; i++)
            {
                string address = string.Format("tcp://{0}:{1}", "127.0.0.1", (port + i).ToString());
                addressList[i] = address;//"tcp://127.0.0.1:6150";
            }
            return addressList;
        }
        private string[] GetStateMachineRequesterAddressList(string stateMachineReqPort, int slots)
        {
            string[] addressList = new string[slots];
            int port = 0;
            if (false == int.TryParse(stateMachineReqPort, out port)) port = 6480;
            for (int i = 0; i < slots; i++)
            {
                string address = string.Format("tcp://{0}:{1}", "127.0.0.1", (port + i).ToString());
                addressList[i] = address;//"tcp://127.0.0.1:6150";
            }
            return addressList;
        }
        private string[] GetLoggerListenerAddressList(string loggerPubPort, int slots)
        {
            string[] addressList = new string[slots];
            int port = 0;
            if (false == int.TryParse(loggerPubPort, out port)) port = 6900;
            for (int i = 0; i < slots; i++)
            {
                string address = string.Format("tcp://{0}:{1}", "127.0.0.1", (port + i).ToString());
                addressList[i] = address;// "tcp://127.0.0.1:6900";
            }
            return addressList;
        }
        private int InitEngineConnectors(string[] addressList, out List<EngineConnector> engineConnectorList)
        {
            engineConnectorList = new List<EngineConnector>();

            for (int i = 0; i < addressList.Length; i++)
            {
                EngineConnector listener = new EngineConnector();
                listener.SubscribeAddress = addressList[i];
                listener.IsOriginalMsgOutput = true;
                listener.evt_LogInfo += LogInfo;
                listener.evt_LogError += LogError;
                listener.evt_LogWarn += LogWarn;
                listener.evt_OriginalMsg += LogInfo;
                engineConnectorList.Add(listener);
            }
            return 0;
        }
        private int InitStateMachineConnectors(string[] addressList, string[] addressReqList, out List<StateMachineConnector> connectorList)
        {
            connectorList = new List<StateMachineConnector>();

            for (int i = 0; i < addressList.Length; i++)
            {
                StateMachineConnector listener = new StateMachineConnector();
                listener.SubscribeAddress = addressList[i];
                listener.RequestAddress = addressReqList[i];
                listener.evt_SM_State_TestDone += OnStateMachineEnd;
                listener.evt_SM_State_Testing += OnStateMachineStart;
                listener.evt_SM_NoTest += OnStateMachineNotTest;
                listener.evt_SM_NoTest_FailCnt += OnStateMachineNotTest;
                listener.evt_SM_NoTest_FailCsv += OnStateMachineNotTest;
                listener.evt_SM_NoTest_FailLoad += OnStateMachineNotTest;
                listener.evt_SM_NoTest_FailReq += OnStateMachineNotTest;
                listener.evt_SM_NoTest_FailUnknow += OnStateMachineNotTest;
                listener.IsOriginalMsgOutput = true;
                listener.evt_LogInfo += LogInfo;
                listener.evt_LogError += LogError;
                listener.evt_LogWarn += LogWarn;
                listener.evt_OriginalMsg += LogInfo;
                connectorList.Add(listener);
            }
            return 0;
        }
        private bool StartSequencerListeners()
        {
            try
            {
                foreach (SequencerListener listener in mySeqListenerList)
                {
                    int rst = listener.StartListener();

                    if (0 != rst)
                    {
                        if (-1 == rst)
                        {
                            LogWarn(this, "SeqListener[" + listener.SubscribeAddress + "] is running already!!!\r\nDo not start again!!!");
                            MessageBox.Show("SeqListener[" + listener.SubscribeAddress + "] is running already!!!\r\nDo not start again!!!");
                        }
                        else
                        {
                            LogError(this, "Start SeqListener[" + listener.SubscribeAddress + "] Failed!!!");
                            MessageBox.Show("Start SeqListener[" + listener.SubscribeAddress + "] Failed!!!");
                        }
                        return false;
                    }
                    else
                    {
                        LogInfo(this, "Start SeqListener[" + listener.SubscribeAddress + "] Successfully!!!");
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                string msg = string.Format("Exception When Starting SequencerListeners: {0}", ex.Message);
                LogError(this, msg);
                return false;
            }
        }
        private bool StartLoggerListeners()
        {
            try
            {
                foreach (LoggerListener listener in myLoggerListenerList)
                {
                    int rst = listener.StartListener();

                    if (0 != rst)
                    {
                        if (-1 == rst)
                        {
                            LogWarn(this, "LoggerListener[" + listener.SubscribeAddress + "] is running already!!!\r\nDo not start again!!!");
                            MessageBox.Show("LoggerListener[" + listener.SubscribeAddress + "] is running already!!!\r\nDo not start again!!!");
                        }
                        else
                        {
                            LogError(this, "Start LoggerListener[" + listener.SubscribeAddress + "] Failed!!!");
                            MessageBox.Show("Start LoggerListener[" + listener.SubscribeAddress + "] Failed!!!");
                        }
                        return false;
                    }
                    else
                    {
                        LogInfo(this, "Start LoggerListener[" + listener.SubscribeAddress + "] Successfully!!!");
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                string msg = string.Format("Exception When Starting LoggerListeners: {0}", ex.Message);
                LogError(this, msg);
                return false;
            }
        }
        private bool StartEngineConnectors()
        {
            try
            {
                foreach (EngineConnector connector in myEngineConnectorList)
                {
                    int rst = connector.StartListener();

                    if (0 != rst)
                    {
                        if (-1 == rst)
                        {
                            LogWarn(this, "EngineConnector[" + connector.SubscribeAddress + "] is running already!!!\r\nDo not start again!!!");
                            MessageBox.Show("EngineConnector[" + connector.SubscribeAddress + "] is running already!!!\r\nDo not start again!!!");
                        }
                        else
                        {
                            LogError(this, "Start EngineConnector[" + connector.SubscribeAddress + "] Failed!!!");
                            MessageBox.Show("Start EngineConnector[" + connector.SubscribeAddress + "] Failed!!!");
                        }
                        return false;
                    }
                    else
                    {
                        LogInfo(this, "Start EngineConnector[" + connector.SubscribeAddress + "] Successfully!!!");
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                string msg = string.Format("Exception When Starting Engine Listeners: {0}", ex.Message);
                LogError(this, msg);
                return false;
            }

        }
        private bool StartStateMachineConnectors()
        {
            try
            {
                foreach (StateMachineConnector connector in myStateMachineConnectorList)
                {
                    int rst = connector.StartListener();

                    if (0 != rst)
                    {
                        if (-1 == rst)
                        {
                            LogWarn(this, "StateMachineConnector[" + connector.SubscribeAddress + "] is running already!!!\r\nDo not start again!!!");
                            MessageBox.Show("StateMachineConnector[" + connector.SubscribeAddress + "] is running already!!!\r\nDo not start again!!!");
                        }
                        else
                        {
                            LogError(this, "Start StateMachineConnector[" + connector.SubscribeAddress + "] Failed!!!");
                            MessageBox.Show("Start StateMachineConnector[" + connector.SubscribeAddress + "] Failed!!!");
                        }
                        return false;
                    }
                    else
                    {
                        LogInfo(this, "Start StateMachineConnector[" + connector.SubscribeAddress + "] Successfully!!!");
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                string msg = string.Format("Exception When Starting StateMachine Listeners: {0}", ex.Message);
                LogError(this, msg);
                return false;
            }
        }
        private void LoadTestCsv(string csvPath)
        {
            try
            {
                //Load CSV to datatable.
                DataTable dt = new DataTable();
                dt = CsvHelper.GetTableFromCSV(csvPath);
                //Convert datatable to List.
                List<SequenceItemArgs> itemsList = new List<SequenceItemArgs>();
                itemsList = (List<SequenceItemArgs>)ModelConvertHelper<SequenceItemArgs>.ConvertToModel(dt);
                if (null == itemsList)
                {
                    LogError(this, "Fail to load csv file!");
                    MessageBox.Show("Fail to load csv file!");

                    return;
                }
                LogInfo(this, "Loaded CSV successfully!");
                LogInfo(this, Path.GetFullPath(csvPath));


                //
                myUUTStatePanel.SetTestItemCount(itemsList.Count);//Set item count to set progress max.
                myTestItemGridView.LoadSequencerItemList(itemsList);//Load csv items to DataGridView.
            }
            catch (Exception ex)
            {
                LogError(this, ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
#endregion//Methods___END//

#region//Events Handler//
        private void LogHelper_Log_Evt(object sender, string msg, int index)
        {
            this.Invoke(new Action(() =>
            {
                myLogPanel.ShowLog(msg, index);
            }));
        }
        void OnLoggerEnd(object sender, LoggerEndArgs arg)
        {
            myUUTStatePanel.EnableInputBox();
        }
        private void BtnStateChange(string flag)
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    if (flag == "start")
                    {
                        btnStart.Enabled = false;
                        btnStop.Enabled = true;
                    }
                    else if (flag == "end")
                    {
                        btnStart.Enabled = true;
                        btnStop.Enabled = false;
                    }
                }));

            }
            else
            {
                if (flag == "start")
                {
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                }
                else if (flag == "end")
                {
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                }
            }
        }
        void OnStateMachineStart(object sender, SMStateEventArgs arg)
        {
            BtnStateChange("start");
            systemLogger.LogClear();
        }
        void OnStateMachineEnd(object sender, SMStateEventArgs arg)
        {
            BtnStateChange("end");
        }

        void OnStateMachineNotTest(object sender, SMStateEventArgs arg)
        {
            BtnStateChange("end");
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            //if (myStationHeartbeatPanel.IsSMHBOK && myStationHeartbeatPanel.IsEngHBOK && myStationHeartbeatPanel.IsSeqHBOK)
            //{
            //    List<string> snList = myUUTStatePanel.GetSnList();
            //    TellSMEnableSlot();
            //    myStateMachineConnectorList[0].StartTest(snList.ToArray());
            //}
            List<string> snList = myUUTStatePanel.GetSnList();
            TellSMEnableSlot();
            myStateMachineConnectorList[0].StartTest(snList.ToArray());
        }
        private void btnStopTest_Click(object sender, EventArgs e)
        {
            //if (myStationHeartbeatPanel.IsSMHBOK && myStationHeartbeatPanel.IsEngHBOK && myStationHeartbeatPanel.IsSeqHBOK)
            //{
            //    List<string> snList = myUUTStatePanel.GetSnList();
            //    myStateMachineConnectorList[0].StopTest();
            //}
            List<string> snList = myUUTStatePanel.GetSnList();
            myStateMachineConnectorList[0].StopTest();
        }

        private void frmMultiUUT_Load(object sender, EventArgs e)
        {
            LogInfo(this, "UI loading complete!");
            StartSequencerListeners();
            StartEngineConnectors();
            StartStateMachineConnectors();
            StartLoggerListeners();
            LoadTestCsv(myConfig.CsvFilePath);
            InitMenu(false);
        }

   
        private void frmMultiUUT_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (myLoopTestPanel != null)
            {
                myLoopTestPanel.Close();
            }
            if (mySingleStatisticsPanel != null)
            {
                mySingleStatisticsPanel.Close();
            }
            if (myLogPanel != null)
            {
                myLogPanel.Close();
            }
        }
#endregion//Events Handlers___END//

#region//ILogger Members//

        public void LogInfo(object sender, string info)
        {
            if (null == systemLogger) return;
            if (info.Contains("!@#"))
            {
                if (!info.Contains("@#FCT_HEARTBEAT"))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int posion = info.IndexOf("!@#");
                        info = info.Substring(posion + 3);
                    }
                    int index = info.IndexOf("!@#");
                    string header = info.Substring(0, index);
                    string infomation = info.Substring(index + 3);
                    info = string.Concat("  [", header, "] : ", infomation);
                    index = info.IndexOf("!@#");
                    info = info.Substring(0, index);
                    systemLogger.LogInfo(sender, info);
                }
            }
            else
            {
                systemLogger.LogInfo(sender, info);
            }
        }

        public void LogWarn(object sender, string warn)
        {
            if (null == systemLogger) return;
            systemLogger.LogWarn(sender, warn);
        }

        public void LogError(object sender, string error)
        {
            if (null == systemLogger) return;
            systemLogger.LogError(sender, error);
        }

#endregion//ILogger Members___END//

#region //Others//
        private void TellSMEnableSlot()
        {
            List<string> snList = myUUTStatePanel.GetSnList();
            List<UUT> uutList = myUUTStatePanel.UUTList;
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
        private void btnStartTest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int StartIndex = myUUTStatePanel.inputNewBarcode();
                if (StartIndex >= 0)
                {
                    Button[] btArray = { btnStart, btnStop };
                    TellSMEnableSlot();
                }
            }
        }

        private void LoopTest()
        {
            btnStartTest_Click(null, null);
        }
        private void InitMenu(bool isEnabled)
        {
            for (int i = 1; i < this.menuStrip1.Items.Count; i++)
            {
                this.menuStrip1.Items[i].Enabled = isEnabled;
            }
        }
        private void LogIn()
        {
            InitMenu(true);
            this.menuStrip1.Items[0].Text = "LogOut";
        }
        private void LogOut()
        {
            InitMenu(false);
            this.menuStrip1.Items[0].Text = "LogIn";
        }
#endregion

#region //Menu Click//
        private void loopTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                myLoopTestPanel.Show(this);
            }
            catch (Exception)
            {
            }
        }

        private void singleInfoPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                mySingleStatisticsPanel.Show(this);
            }
            catch (Exception)
            {
            }
        }

        private void logInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem ts = this.menuStrip1.Items[0];
            if (ts.Text== "LogIn")
            {
                Forms.LogInWin myLogInWin;
                myLogInWin = new Forms.LogInWin();
                myLogInWin.LogInSuccess_Evt += LogIn;
                myLogInWin.ShowDialog(this);
            }
            else
            {
                LogOut();
            }
        }
        

        private void logPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                myLogPanel.Show(this);
            }
            catch (Exception)
            {
            }
        }
#endregion
    }
}
