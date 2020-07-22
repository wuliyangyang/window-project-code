using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogTool;
using MNetMQ;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace DMTec.TestSuite.CommonGUI
{
    public class SaveLogHelper
    {
        private string jsonFile;
        private RootObject _JsonObj;
        private string _ip = "tcp://127.0.0.1";
        private string _path = @"C:/vault/TestLog/";
       
        private List<object> objList = new List<object>();
        private List<LogTool.LogTool> logAll = new List<LogTool.LogTool>();
        private Dictionary<string, LogTool.LogTool> loggrtDic = new Dictionary<string, LogTool.LogTool>();

        public event LogMsgHandler Log_Evt;
        public SaveLogHelper()
        {
            Init();
        }
        private void Init()
        { 
            int ret = GetJsonInfo();
            if (ret != 0) return;
            InitTotalLogger();
            InitListeners();
        }
        private void GetJsonFilePath()
        {
            string RELPATH = Global.GetGlobalInstance().GetPath();
            jsonFile = RELPATH + "\\TesterUI\\logInfoConfig.json";
        }
        private int GetJsonInfo()
        {
            try
            {
                GetJsonFilePath();
                if (!File.Exists(jsonFile))
                {
                    Log.LogError(jsonFile + "dose not exist");
                    System.Windows.Forms.MessageBox.Show(jsonFile + "dose not exist");
                    return -1;
                }
                string jsonString = File.ReadAllText(jsonFile);
                RootObject obj = JsonConvert.DeserializeObject<RootObject>(jsonString);
                _JsonObj = obj;
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        private void InitTotalLogger()
        {
            for (int i = 0; i < _JsonObj.Slots; i++)
            {
                logAll.Add(new LogTool.LogTool(this._path, DateTime.Now.ToString("yyyy-MM-dd") +"-ALLLog" + i.ToString()));

                objList.Add(new object());
            }
        }
        private void InitListeners()
        {
            try
            {
                foreach (var item in _JsonObj.Devices)
                {
                    for (int i = 0; i < _JsonObj.Slots; i++)
                    {
                        string address = this._ip + ":" + (item.Port + i).ToString();
                        CreatSingleLogger(address, item.DeviceName + i.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Log.LogError("LibLogHelper InitListeners:" + e.Message);
            }
        }
        public void CreatSingleLogger(string address, string name)
        {
            SubSocket sub = SubFactory.GetSuber(address, name, _sub_RecvMsgEvt);
            string logName = DateTime.Now.ToString("yyyy-MM-dd") + "-" + name;
            LogTool.LogTool log = new LogTool.LogTool(_path, logName);
            loggrtDic.Add(sub.Name, log);
        }
        private void AsyncPostMsg(string logMsg,int index)
        {
            if (_JsonObj.IsEnableLog)
            {
                Task.Run(() =>
                {
                    if (Log_Evt != null)
                    {
                        this.Log_Evt(this, logMsg, index);//在UI上显示 log
                    }
                });
            }
        }
        private void LogAllMsgToOne(string logName,string msg)
        {
            int l = logName.Length - 1;
            int index = int.Parse(logName.Substring(l, 1));
            string logMsg = "[" + logName + "]" + msg;
            lock (objList[index])
            {
                logAll[index].log(logMsg);
                AsyncPostMsg(logMsg, index);
            }
        }
        private void _sub_RecvMsgEvt(object sender, string msg)
        {
            SubSocket sub = sender as SubSocket;
            LogTool.LogTool logger;
            bool isOK = loggrtDic.TryGetValue(sub.Name, out logger);
            if (isOK)
            {
                logger.log(msg);//一个设备log单独存一个文件
                LogAllMsgToOne(sub.Name, msg);//一个通道存一个文件
            }
            else
            {
                Log.LogError("[" + sub.Address + "]" + "获取log对象失败");
            }

        }
    }

}
