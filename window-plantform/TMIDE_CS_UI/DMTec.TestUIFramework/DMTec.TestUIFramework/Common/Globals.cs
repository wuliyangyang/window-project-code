using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework
{
    public class Globals
    {
        public const string TM_MSG_SEP = "!@#";
        public const string PUB_CHANNEL = "101";

        public const string SEQ_END_RESULT_PASS = "True";
        public const string SEQ_ITEM_END_RESULT_PASS = "1";

        public const int TEST_ENGINE_PUB = 6150;//engine 的 PUB 端口
        public const int TE_PROXY_PUB = 6200;
        public const int SEQUENCER_PORT = 6150;//Sequencer 的 REP 端口
        public const int TEST_ENGINE_PORT = 6100;//engine 的 REP 端口
        public const int SEQUENCER_PUB = 6250;//Sequencer 的 PUB 端口
        public const int DEBUG_LOGGER_PORT = 6350;
        public const int SEQUENCER_PROXY_PUB = 6150;
        public const int PUB_PORT = 50;
        public const int LOGGER_PUB = 6900;//Logger 的 PUB 端口

        public const int SM_PUB = 6880;//State Machine 的 PUB 端口
        public const int SM_PORT = 6480;//State Machine 的 REP 端口
        public const int SM_HEARTBEAT = 6580;//State Machine 的 Heart beat 端口

        //Below is SM State List...//
        public const string SM_IDLE = "SM_IDLE";//空闲
        public const string SM_UUT_LOADED = "SM_UUT_LOADED";//产品已装载
        public const string SM_TESTING = "SM_TESTING";//测试中
        public const string SM_TESTING_DONE = "SM_TESTING_DONE";//测试完成
        public const string SM_UUT_UNLOADED = "SM_UUT_UNLOADED";//产品取出
        public const string SM_ERROR = "SM_ERROR";//错误

        //Below Items is add since 2017.03.14.
        public const string SM_NOT_TEST = "SM_NOT_TEST";//通讯正常，被屏蔽（跳过）测试
        public const string SM_NOT_TEST_FAIL_CNT = "SM_NOT_TEST_FAIL_CNT";//MTCP连接失败，造成无法测试
        public const string SM_NOT_TEST_FAIL_LOAD = "SM_NOT_TEST_FAIL_LOAD";//sequence load测试csv失败，造成无法测试
        public const string SM_NOT_TEST_FAIL_CREATE_CSV = "SM_NOT_TEST_FAIL_CREATE_CSV";//生成请求用csv失败，造成无法测试
        public const string SM_NOT_TEST_FAIL_REQ = "SM_NOT_TEST_FAIL_REQ";//请求csv过程中发生错误
        public const string SM_NOT_TEST_FAIL_UNKNOW = "SM_NOT_TEST_FAIL_UNKNOW";//其他未知错误


    }

}
