using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMTec.TestUIFramework.DataModel
{
    /// <summary>
    /// The entity class of zmq client port.
    /// </summary>
    [Serializable]
    public class ZmqPortInfo
    {
        public string ARM_PORT { get; set; }
        public string ARM_PUB { get; set; }
        public string AUDIO_PUB { get; set; }
        public string BACKLIGHT_PUB { get; set; }
        public string BKLT_PUB { get; set; }
        public string DATALOGGER_PORT { get; set; }
        public string DATALOGGER_PUB { get; set; }
        public string DCSD_PORT { get; set; }
        public string DCSD_PUB { get; set; }
        public string DEBUG_LOGGER_PORT { get; set; }
        public string FIXTURE_CTRL_PORT { get; set; }
        public string FIXTURE_CTRL_PUB { get; set; }
        public string HDMI_PUB { get; set; }
        public string LOGGER_HEARTBEAT { get; set; }
        public string LOGGER_PUB { get; set; }
        public string LOG_PATH_PORT { get; set; }
        public string PUB_CHANNEL { get; set; }
        public string PUB_PORT { get; set; }
        public string PWR_SEQUENCER_PUB { get; set; }
        public string SEQUENCER_PORT { get; set; }
        public string SEQUENCER_PROXY_PUB { get; set; }
        public string SEQUENCER_PUB { get; set; }
        public string SM_HEARTBEAT { get; set; }
        public string SM_PORT { get; set; }
        public string SM_PROXY_PUB { get; set; }
        public string SM_PUB { get; set; }
        public string SM_RPC_PUB { get; set; }
        public string SPDIF_PUB { get; set; }
        public string TEST_ENGINE_PORT { get; set; }
        public string TEST_ENGINE_PUB { get; set; }
        public string TE_PROXY_PUB { get; set; }
        public string SERIAL_LOG_PUB { get; set; }
        public string TCP_LOG_PUB { get; set; }
        public string LUA_LOG_PUB { get; set; }
        public string UART_PORT { get; set; }
        public string UART_PUB { get; set; }
        public string UIREQ_PORT{ get; set; }
        public string TMSyncREQ_PORT { get; set; }
        public string UI_PUB { get; set; }
    }

}
