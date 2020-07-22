using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.Common
{
    public static class Consts
    {
        //public const string VESION_STRING = "v1.1.9.3";

        public const string SYSTEM_CONFIG_DATA_PATH = "C:/TesterUI/SysConfig";

        public const string CHAMBER_DATA_CSV_DIR_PATH = "C:/vault/Data/CSV/";
        public const string CHAMBER_HARDWARE_RUNDATA_DIR_PATH = "C:/vault/Data/Hardware/";
        public const string CHAMBER_STATISTICS_DATA_DIR_PATH = "C:/vault/Data/Statistics/";

        public const string TESTEND_PASS_STRING = "1";
        public const string TESTEND_FAIL_STRING = "0";

        public static string TestCsvDataSaveDirPath = CHAMBER_DATA_CSV_DIR_PATH;
        public static string HardwareDataSaveDirPath = CHAMBER_HARDWARE_RUNDATA_DIR_PATH;
        public static string ChamberStatisticsDataSaveDirPath = CHAMBER_STATISTICS_DATA_DIR_PATH;

        public const int NoTestEndAlarmTime = 50000;//ms
        public const int Fixture_Station_Slots_Max = 8;

        public static Color LED_ON_OK_COLOR     = Color.Green;
        public static Color LED_ON_ERROR_COLOR  = Color.Red;
        public static Color LED_ON_WARN_COLOR   = Color.Yellow;
        public static Color LED_ON_UNDEFINED_COLOR = Color.Silver;
        public static Color LED_OFF_COLOR       = Color.Silver;

        public const int HeartbeatCheckInterval = 8;//unit:s


        public static string MessageType_UserLevel_Changed = "msgType_UserLevelChanged";
        public static string MessageType_BarCode_Input = "msgType_BarcodeInput";
        public static string MessageType_Log_WriteLine = "msgType_Log_WriteLine";


    }

}
