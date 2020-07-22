
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using DMTec.TMListener;
//
namespace DMTec.TestUIFramework
{
    public class FlexPublisher
    {
        private static object lockObj = new object();
        private static object lockObj2 = new object();
        private static object lockObj3 = new object();
        private static object lockObj4 = new object();

        private static FlexPublisher uniqueInstance;

        Publisher myPublisher;

        IMsgData msgData;

        FlexPubMsg myPubMsg = new FlexPubMsg();

        private FlexPublisher()
        {
            myPublisher = new Publisher();
        }

        ~ FlexPublisher()
        {
            myPublisher = null;
        }

        public static FlexPublisher GetInstance()
        {
            if(null == uniqueInstance)
            {
                uniqueInstance = new FlexPublisher();
            }

            return uniqueInstance;
        }

        public void Bind(string address)
        {
            myPublisher.StartServer(address);
        }

        public void PubMsg(string str)
        {
            //lock (lockObj)
            //{
            //    myPublisher.PubOnsMsg(str);
            //    //Thread.Sleep(10);
            //}
            myPublisher.PubOneMsg(str);
        }

        public string MakeJsonString(IMsgData data)
        {
            string jStr = "";
            try
            {
                myPubMsg.MsgType   = data.GetTypeName();
                myPubMsg.MsgData   = data;
                jStr = JsonConvert.SerializeObject(myPubMsg);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(),"Exception",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return jStr;

        }

        public void PubErrorMsg(ErrorMsg errMsg)
        {
            lock (lockObj)
            {
                string pubMsg = MakeJsonString(errMsg);
                PubMsg(pubMsg);
            }
        }

        public void PubStatusMsg(StatusMsg stsMsg)
        {
            lock(lockObj2)
            {
                string pubMsg = MakeJsonString(stsMsg);
                PubMsg(pubMsg);
            }
        }

        public void PubResultMsg(ResultMsg rstMsg)
        {
            lock (lockObj2)
            {
                string pubMsg = MakeJsonString(rstMsg);
                PubMsg(pubMsg);
            }
        }
    }


    public class FlexPubMsg
    {
        //public string ChamberID { get; set; }
        public string MsgType { get; set; }
        public IMsgData MsgData { get; set; }
    }

    public interface IMsgData
    {
        string GetTypeName();
    }

    public class ResultMsg : IMsgData
    {
        public string ChamberID { get; set; }
        public string DutSN { get; set; }
        public string SocketSN { get; set; }
        public string DutResult { get; set; }

        public string GetTypeName()
        {
            return "ResultMsg";
        }
    }

    public class ErrorMsg : IMsgData
    {
        public string ChamberID { get; set; }
        public int    ErrorCode { get; set; }
        public string ErrorString { get; set; }

        public string GetTypeName()
        {
            return "ErrorMsg";
        }
    }

    public class StatusMsg : IMsgData
    {
        public string ChamberID { get; set; }
        public string Status { get; set; }
        public string GetTypeName()
        {
            return "StatusMsg";
        }
    }
}
