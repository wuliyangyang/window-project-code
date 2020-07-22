using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.DataModel
{
    /// <summary>
    /// The statistic info of one chamber.
    /// </summary>
    public class ChamberStatistics
    {
        /// <summary>
        /// Amount of loaded dut in one chamber. 
        /// </summary>
        public int Total;

        /// <summary>
        /// Amount of Untest dut in one chamber. 
        /// </summary>
        public int Untest;

        /// <summary>
        /// Amount of passed dut in one chamber. 
        /// </summary>
        public int Passed;

        /// <summary>
        /// The passrate of dut in one chamber.
        /// </summary>
        public double PassRate;

        /// <summary>
        /// 
        /// </summary>
        public ChamberStatistics()
        {
            Total  = 0;
            Untest = 0;
            Passed = 0;
            PassRate = (double)0;
        }
    }

    /// <summary>
    /// The Bin Statistics Data Structure.
    /// </summary>
    public class BinInfo
    {
        public int Bin1;
        public int Bin2;
        public int Bin5;
        public int Bin6;
        public int Bin7;
        public int Bin8;
        public int BinTotal;
        public int BinPassed;
        public double BinPassRate;

        /// <summary>
        /// 
        /// </summary>
        public BinInfo()
        {
            //Init Bin Val
            Bin1 = 0;
            Bin2 = 0;
            Bin5 = 0;
            Bin6 = 0;
            Bin7 = 0;
            Bin8 = 0;
            BinTotal = 0;
            BinPassed = 0;
            BinPassRate = (double)0;
        }
    }

    public class ChamberSimpleData
    {

    }

    /// <summary>
    /// The Data Info Include In Item End Event.
    /// </summary>
    public class TestItemEndArgs : IDisposable
    {
        public string tid { get; set; }
        public string value { get; set; }
        public string result { get; set; }

        public TestItemEndArgs()
        {
            tid = "";
            value = "";
            result = "";
        }

        public void Dispose()
        {

        }
    }

    /// <summary>
    /// Error Event Class.
    /// </summary>
    public class ErrorEventArgs : IDisposable
    {
        public int errCode { get; set; }
        public string errMsg { get; set; }
        //
        public ErrorEventArgs()
        {
            errCode = -99999;
            errMsg = "";
        }
        //
        public void Dispose()
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ContinuousItemFailArgs : EventArgs, IDisposable
    {
        public string ItemName { get; set; }
        public int FailTimes { get; set; }
        //
        public ContinuousItemFailArgs()
        {
            ItemName = "N/A";
            FailTimes = 0;
        }
        //
        public void Dispose()
        {
            //
        }
    }


    ////////////////////////////////////////

    /// <summary>
    /// Chamber's hardware running data.
    /// </summary>
    [Serializable]
    public class ChamberHardwareData
    {
        public int CurrentPogoPinTested { get; set; }

        public ChamberHardwareData()
        {
            CurrentPogoPinTested = 0;
        }
    }

    [Serializable]
    public class ZmqSocketConfig
    {
        public string SocketName { get; set; }
        public ZSocketType SocketType { get; set; }
        public string SocketAddress { get; set; }
        public bool IsSocketEnable { get; set; }
        public ZmqSocketConfig()
        {
            SocketName = "N/A";
            SocketType = ZSocketType.SUB;
            SocketAddress = "tcp://127.0.0.1:6215";
            IsSocketEnable = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SEQConnectorConfig
    {
        public string Name { get; set; }
        public string RequesterAddress { get; set; }
        public string SubscriberAddress { get; set; }
        public bool   IsRequesterEnable { get; set; }
        public bool   IsSubscriberEnable { get; set; }

    }

    /// <summary>
    /// This class is chamber's general config data structure.
    /// If you need more special options, you need to inherit this class.
    /// </summary>
    [Serializable]
    public class ChamberConfig
    {

        /// <summary>
        /// This chamber's id for setting apart from other chambers.
        /// </summary>
        public string ChamberID { get; set; }//ChamberName...

        /// <summary>
        /// If this chamber Enable
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// If need manual input SN(DutSN/SocketSN).
        /// </summary>
        public bool IsNeedScanBarcode { get; set; }

        /// <summary>
        /// If allow the original msg output to UI display.
        /// </summary>
        public bool IsOriginalMsgOutput { get; set; }

        /// <summary>
        /// If need to show last sequence result.
        /// </summary>
        public bool IsShowLastResult { get; set; }

        /// <summary>
        /// If need to show outside dut status.
        /// </summary>
        public bool IsShowOutsideDutStatus { get; set; }

        /// <summary>
        /// If need to show hardware information.
        /// </summary>
        public bool IsShowHardwareInfo { get; set; }

        /// <summary>
        /// This chamber's module type.
        /// </summary>
        public ChamberType ChambersType { get; set; }

        /// <summary>
        /// This chamber's zmq socket(subscriber) address connect to Sequencer.
        /// </summary>
        public string SequencerSubAddress { get; set; }

        /// <summary>
        /// This chamber's zmq socket(requester) address connect to Sequencer.
        /// </summary>
        public string SequencerReqAddress { get; set; }

        /// <summary>
        /// This chamber's zmq socket(replier) address connect to TMSync.
        /// </summary>
        public string TMSyncRepAddress { get; set; }

        /// <summary>
        /// This chamber's zmq socket(requester) address connect to StateMachine.
        /// </summary>
        public string StateMachineReqAddress { get; set; }

        /// <summary>
        /// This chamber's zmq socket(subscriber) address connect to StateMachine.
        /// </summary>
        public string StateMachineSubAddress { get; set; }

        /// <summary>
        /// This chamber's zmq socket(subscriber) address connect to TestEngine.
        /// </summary>
        public string EngineSubAddress { get; set; }

        /// <summary>
        /// This chamber's zmq socket(requester) address connect to TestEngine.
        /// </summary>
        public string EngineReqAddress { get; set; }

        /// <summary>
        /// If Enable Pogo Pin Drain Alarm.
        /// </summary>
        public bool IsPogoPinAlarm { get; set; }//顶针使用限制报警

        /// <summary>
        /// Pogo Pin Drain Alarm Times' limit.
        /// </summary>
        public int PogoPinTimesLimit { get; set; }//顶针使用次数限定

        /// <summary>
        /// If Enable Spectrograph Drain Alarm.
        /// </summary>
        public bool IsSpectrometerAlarm { get; set; }//光谱仪校准期限报警

        /// <summary>
        /// How Long The Spectrometer Need a Calibration.
        /// </summary>
        public int SpectrometerTimeLimit { get; set; }//光谱仪校准时间限定

        /// <summary>
        /// Spectrograph Last calibration DateTime.
        /// </summary>
        public DateTime SpectrometerCalibrateDateTime { get; set; }//光谱仪校准时间

        public ChamberConfig()
        {
            IsEnable = false;
            IsShowLastResult = false;
            IsShowOutsideDutStatus = false;
            IsOriginalMsgOutput = false;
            IsNeedScanBarcode = false;
            IsShowHardwareInfo = false;
            ChamberID = "Unknown";
            ChambersType = ChamberType.C11;
            SequencerSubAddress = "tcp://127.0.0.1:6250";
            SequencerReqAddress = "tcp://127.0.0.1:6200";
            TMSyncRepAddress = "tcp://127.0.0.1:8800";
            StateMachineReqAddress = "tcp://127.0.0.1:6480";
            StateMachineSubAddress = "tcp://127.0.0.1:6880";
            EngineSubAddress = "tcp://127.0.0.1:6150";
            EngineReqAddress = "tcp://127.0.0.1:6100";
            IsPogoPinAlarm = false;
            PogoPinTimesLimit = 100000;//Times
            IsSpectrometerAlarm = false;
            SpectrometerTimeLimit = 1000;//Hour
            SpectrometerCalibrateDateTime = DateTime.Now;
        }
    }

    [Serializable]
    public class TesterConfig : IDisposable
    {

        public string TesterID { get; set; }

        public string DeviceId { get; set; }

        public TesterType TesterType { get; set; }

        public string UserDataSavePath { get; set; }

        public string TMVersion { get; set; }

        public bool IsNeedPubMsg { get; set; }
        
        public string PubServerAddress { get; set; }
        
        public bool IsEnableSFServer { get; set; }
        
        public string SFServerAddress { get; set; }

        public List<string> HightLightItemsList { get; set; }

        public ChamberConfig C1 { get; set; }

        public ChamberConfig C2 { get; set; }

        //public List<ChamberConfig> list_ChamberConfig;

        public TesterConfig()
        {
            TesterID = "N/A";
            DeviceId = "701003";

            TesterType = TesterType.IQC; 
            UserDataSavePath = "C:/";
            IsNeedPubMsg = false;
            PubServerAddress = "tcp://127.0.0.1:8899";
            IsEnableSFServer = false;
            SFServerAddress = "127.0.0.1";
            HightLightItemsList = new List<string>();
            C1 = new ChamberConfig();
            C2 = new ChamberConfig();
        }

        public void Dispose()
        {
            TesterID = "";
            UserDataSavePath = "";
            HightLightItemsList = null;
            //
            C1 = null;
            C2 = null;
        }
    }


    [Serializable]
    public class PublishChamberData
    {
        public DateTime PubTime { get; set; }
        public string ChamberID { get; set; }
        public string DutSN { get; set; }
        public string SocketSN { get; set; }
        public string Result { get; set; }

        public PublishChamberData()
        {
            PubTime = DateTime.Now;
            ChamberID = "N/A";
            DutSN = "N/A";
            SocketSN = "N/A";
            Result = "N/A";
        }
    }

    public class PogoPinUsedTimesClearArgs : EventArgs
    {
        public string ChamberID { get; set; }

        public PogoPinUsedTimesClearArgs()
        {
            ChamberID = "N/A";

        }
    }
}
