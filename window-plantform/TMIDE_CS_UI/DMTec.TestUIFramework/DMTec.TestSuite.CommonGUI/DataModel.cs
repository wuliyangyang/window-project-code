using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTec.TestSuite.CommonGUI
{
    public delegate void PostMsgHandler(object sender, string msg);
    public delegate void RecvMsgHandler(object sender, string msg);
    public delegate void LogMsgHandler(object sender, string msg,int index);
    class DataModel
    {
    }
    public class Device
    {
        public string DeviceName { get; set; }
        public int Slot { get; set; }
        public int Port { get; set; }
    }


    public class RootObject
    {
        public int Slots { get; set; }
        public List<Device> Devices { get; set; }

        public bool IsEnableLog { get; set; }
    }
}
