using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using DMTec.TMListener;
using DMTec.TMListener.Interface;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace DMTec.TestUIFramework
{
    public class LoggerEndArgs: EventArgs, IDisposable
    {

        public string Function { get; set; }
        public string DutSN { get; set; }
        public string SocketSN { get; set; }
        public string Slot { get; set; }
        public int ErrorCode { get; set; }
        public int BinCode { get; set; }
        public string ErrorStr { get; set; }

        public LoggerEndArgs()
        {
            Function = "N/A";
            DutSN = "N/A";
            SocketSN = "N/A";
            Slot = "N/A";
            ErrorCode = -999;
            ErrorStr = "N/A";
        }

        public override string ToString()
        {
            string str = "[Function]: " + Function + "\r\n[DutSN]: " + DutSN + "\r\n[SocketSN]: " + SocketSN + "\r\n[Slot]: " + Slot + "\r\n[ErrorCode]: " + ErrorCode.ToString() + "\r\n[ErrorStr]: " + ErrorStr;
            return str;
        }

        public void Dispose()
        {
            Function = null;
            DutSN = null;
            SocketSN = null;
            Slot = null;
            ErrorStr = null;
        }
    }

    public delegate void MsgPostHandler(object sender, string msg);
    public delegate void LoggerEndHandler(object sender, LoggerEndArgs arg);

    public class LoggerListener : ISubscriberUser
    {
        protected Subscriber mySubscriber;
        protected LoggerEndArgs myLoggerEndArgs;

        public LoggerListener()
        {
            InitMembers();
        }

        public virtual string SubscribeAddress
        {
            get { return mySubscriber.Address; }
            set { mySubscriber.Address = value; }
        }
        public string Name { get; set; }

        protected bool _isOriginalMsgOutput = true;

        public string MessageFilter { get; set; }

        string[] msgFrames;
        string msgLevel;
        int    level = -9999;
        string infoMessage;
        string publisher;

        JObject jobj;
        JObject jobjData;

        public virtual bool IsOriginalMsgOutput
        {
            get { return _isOriginalMsgOutput; }
            set { _isOriginalMsgOutput = value; }
        }

        public event MsgPostHandler   evt_LoggerMessage;//All msg show...
        public event LoggerEndHandler evt_LoggerEnd;
        public event MsgPostHandler   evt_LogInfo;//Output Msg String To UI By Binding This Event.
        public event MsgPostHandler   evt_LogError;//Error
        public event MsgPostHandler   evt_LogWarn;//Warn
        public event MsgPostHandler evt_MtcpConnectError;

        protected virtual void InitMembers()
        {
            this.IsOriginalMsgOutput = false;
            this.MessageFilter = Global.PUB_CHANNEL;
            this.Name = "LoggerListener";

            myLoggerEndArgs = new LoggerEndArgs();

            mySubscriber = new Subscriber();
            mySubscriber.Name = "LoggerListener-Call";
            mySubscriber.RegisterUser(this);
        }

        protected virtual string GetMarkedMsg(string msg)
        {
            return " [LoggerListener(" + SubscribeAddress + ")] " + msg;
        }

        protected virtual void LogInfo(string msg)
        {
            //if (IsConsoleEnable) Console.WriteLine(msg);
            //if (IsLoggerEnable)  myLogger.Debug(msg);
            string msgStr = GetMarkedMsg(msg);
            if (evt_LogInfo != null)
            {
                evt_LogInfo.Invoke(this, msgStr);
            }
        }

        protected virtual void LogWarn(string warnInfo)
        {
            string msgStr = GetMarkedMsg(warnInfo);
            if (null != evt_LogWarn)
            {
                evt_LogWarn.Invoke(this, msgStr);
            }
        }

        protected virtual void LogError(string errorInfo)
        {
            string msgStr = GetMarkedMsg(errorInfo);
            if(null != evt_LogError)
            {
                evt_LogError.Invoke(this, msgStr);
            }
        }

        protected virtual void MtcpWarn(string warnInfo)
        {
            if (null != evt_MtcpConnectError)
            {
                evt_MtcpConnectError.Invoke(this, warnInfo);
            }
        }

        public virtual int StartListener()
        {
            int errCode = 0;
            try
            {
                errCode = mySubscriber.Start();
            }
            catch (Exception e)
            {
                LogInfo("--Exception Info: " + e.Message);
                return -9998;
            }

            return errCode;
        }

        public virtual int StopListener()
        {
            return mySubscriber.Stop();
        }

        protected virtual void CloseListener()
        {
            if (null != mySubscriber)
            {
                mySubscriber = null;
            }
            Thread.Sleep(10);
        }

        protected virtual void CallLoggerEndEventHandler(LoggerEndArgs e)
        {
            if (evt_LoggerEnd != null)
            {
                try
                {
                    evt_LoggerEnd.Invoke(this, e);
                }
                catch (Exception ex)
                {
                    LogError("SendLoggerEndEvent Exception>>>\r\nException Info:" + ex.Message + "\r\nMessage:" + e.ToString() + "\r\n");
                }
            }
        }

        protected virtual void CallLoggerOriginalMsgHandler(string om)
        {
            //string msg = " [Listener(" + SubscribeAddress + ")] [Original Msg] " + om;
            if (evt_LoggerMessage != null)
            {
                try
                {
                    evt_LoggerMessage.Invoke(this, om);//evt_AllMsgRcv.BeginInvoke(this, e, null, null);
                }
                catch(Exception ex)
                {
                    LogError("Send Original Msg Exception>>>\r\nException Info:" + ex.Message + "\r\nMessage:" + om + "\r\n");
                }
            }
        }

        //{"function":"MtcpResult","params":{"dutSn":"","socketSn":"","result":0,"errStr":"","BinCode":55},"slot":"CH1"};
        //{"function":"SequencerResult","params":{"dutSn":"","socketSn":"","result":0,"errStr":""},"slot":"CH1"};
        protected virtual void ParseLoggerDataInfo(string msg, string publisher)
        {
            try
            {
                //jobj = (JObject)JsonConvert.DeserializeObject(msg);
                //JsonConvert.DeserializeObject(msg);
                jobj = JObject.Parse(msg);
            }
            catch(Exception ex)
            {
                LogError("One Exception Happened When Parse Logger Msg To Json Object!!!\r\n" + ex.Message + "\r\nMessage:" + msg);
                return;
            }
            
            if (null == jobj)
            {
                LogWarn("Can not parse logger msg string to Json Object!\r\n" + msg);
                return;
            }

            if (null == jobj["function"])
            {
                LogWarn("No [function] param in logger msg string!\r\n" + msg);
                return;
            }

            jobjData = (JObject)jobj["params"];
            if (null == jobjData)
            {
                LogWarn("No [params] param in sequence msg string!\r\n" + msg);
                return;
            }

            if ("MtcpConnectError" == jobj["function"].ToString())
            {
                MtcpWarn("Connect Mtcp error!!!");
                //return;
            }

            if ("MtcpResult" == jobj["function"].ToString() || "SequencerResult" == jobj["function"].ToString())
            {
                try
                {
                    myLoggerEndArgs.ErrorCode = Convert.ToInt32(jobjData["result"].ToString());
                    if (jobjData["binCode"] != null)
                    {
                        myLoggerEndArgs.BinCode = Convert.ToInt32(jobjData["binCode"].ToString());
                    }
                }
                catch (Exception exp)
                {
                    LogError("One Exception Happened When Parse ErrorCode String to int in Logger Msg!!!\r\n" + exp.Message + "\r\nMessage:" + msg);
                    return;
                }

                myLoggerEndArgs.Function = jobj["function"].ToString();
                myLoggerEndArgs.Slot = jobj["slot"].ToString();

                myLoggerEndArgs.ErrorStr = jobjData["errStr"].ToString();

                myLoggerEndArgs.DutSN = jobjData["dutSn"].ToString();
                myLoggerEndArgs.SocketSN = jobjData["socketSn"].ToString();

                CallLoggerEndEventHandler(myLoggerEndArgs);
            }
        }

        protected virtual void ParseLoggerMessage(string msg)
        {
            try
            {
                //msgList = msg.Split(Common.TM_MSG_SEP.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                msgFrames = Regex.Split(msg, Global.TM_MSG_SEP);

                if (msgFrames.Length < 2)
                {
                    LogWarn("LoggerListener Received msg too short to parse!!! \r\nOriginal Msg:" + msg);
                    return;
                }

                if (string.IsNullOrEmpty(msgFrames[2]))
                {
                    LogWarn("No [Msg Level] in Received Logger msg!!!\r\nOriginal Msg:" + msg);
                    return;
                }

                msgLevel = msgFrames[2];
                level = -9999;

                try
                {
                    //level = Convert.ToInt32(levelStr);
                    if(false == Int32.TryParse(msgLevel, out level))
                    {
                        LogWarn("Get msg [level] error!!!\r\nOriginal Msg:" + msg);
                        return;
                    }
                }
                catch(Exception e)
                {
                    LogError("Convert [Level] to int failed when parse sequence msg!!!\r\nOriginal Msg:" + msg + "\r\n" + e.Message);
                }

                if (level < 0)
                {
                    LogWarn("Get msg [level] error!!!\r\nOriginal Msg:" + msg);
                    return;
                }

                if (null == msgFrames[3] || "" == msgFrames[3])
                {
                    LogWarn("No [Publisher] Param in logger msg!!!\r\nOriginal Msg:" + msg);
                    return;
                }
                publisher = msgFrames[3];//Get who published this msg...
                
                if(null == msgFrames[4] || "" == msgFrames[4])
                {
                    LogWarn("No [Data] Param in sequence msg!!!\r\nOriginal Msg:" + msg);
                    return;
                }
                infoMessage = msgFrames[4];

                switch (level)
                {
                    case (int)MSG_LEVEL.MSG_LEVEL_REPORT:
                        if (infoMessage == "FCT_HEARTBEAT")//Heart beat
                        {
                            // {
                            //     myHeartbeatArgs.Publisher = publisher;
                            //     CallHeartBeatEventHandler(myHeartbeatArgs);
                            // }
                        }
                        else
                        {
                            ParseLoggerDataInfo(infoMessage, publisher);   
                        }
                        break;
                    case (int)MSG_LEVEL.MSG_LEVEL_1:
                        //TODO
                        break;
                    case (int)MSG_LEVEL.MSG_LEVEL_2:
                        //TODO
                        break;
                    case (int)MSG_LEVEL.MSG_LEVEL_DATA:
                        {
                            ParseLoggerDataInfo(infoMessage, publisher);//Parse msg type and active event.
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception exp)
            {
                LogError("One Exception Happened When Parse Logger Msg For Getting Event Information!!!\r\n" + exp.Message + "\r\nMessage:" + msg);
            }
        }

        public virtual void OnSubscriberMsgReceived(object sender, string msg)
        {
            if (IsOriginalMsgOutput)//If allow to print all received msg to ui
            {
                CallLoggerOriginalMsgHandler(msg);
            }

            ParseLoggerMessage(msg);
        }

        public virtual void OnSubscriberLogInfo(object sender, string msg)
        {
            LogInfo(msg);
        }

        public virtual void OnSubscriberLogWarn(object sender, string msg)
        {
            LogWarn(msg);
        }

        public virtual void OnSubscriberLogError(object sender, string msg)
        {
            LogError(msg);
        }
    }
}
