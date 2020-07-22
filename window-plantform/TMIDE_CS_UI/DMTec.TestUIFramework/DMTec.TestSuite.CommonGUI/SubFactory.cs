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
    class SubFactory
    {
        public SubFactory()
        {

        }

        public static SubSocket GetSuber(string address,string name, RecvMsgHandler _sub_RecvMsgEvt)
        {
            SubSocket _sub = new SubSocket();
            _sub.Connect(address);
            _sub.SetTopic();
            _sub.Name = name;
            _sub.RecvMsgEvt += new MNetMQ.RecvMsgHandler(_sub_RecvMsgEvt);
            _sub.StartSub();
            return _sub;
        }
    }
}
