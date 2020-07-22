using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using DMTec.TMListener;

namespace DMTec.TestUIFramework.BasicControls
{
    public partial class StationHeartbeatPanel : UserControl
    {
        
        #region//Constructors//

        public StationHeartbeatPanel()
        {
            InitializeComponent();
            
            IsSeqHBOK = false;
            IsSMHBOK = false;
            IsEngHBOK = false;

            InitTimer();
        }

        #endregion//Constructors//

        #region//Members//

        private System.Windows.Forms.Timer myTimer;
        private int slots = 8;
        private int modules = 1;

        #endregion//Members...End//

        #region//Properties//

        /// <summary>
        /// 
        /// </summary>
        [DescriptionAttribute("The test module of one test station.")]
        public int Modules
        {
            get { return modules; }
            set 
            {
                modules = value;
                myStateMachineHeartbeats.LEDCount = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DescriptionAttribute("The test channel of one test station.")]
        public int Slots
        {
            get { return slots; }
            set
            {
                slots = value;
                mySeqHeartbeats.LEDCount = value * this.Modules;
                myEngineHeartbeats.LEDCount = value * this.Modules;
            }
        }

        public LedArrayPanel SeqHeartbeatLeds 
        {
            get { return mySeqHeartbeats; }
            set { mySeqHeartbeats = value; }
        }

        public LedArrayPanel EngineHeartbeatLeds
        {
            get { return myEngineHeartbeats; }
            set { myEngineHeartbeats = value; }
        }

        public LedArrayPanel StateMachineHeartbeatLeds
        {
            get { return myStateMachineHeartbeats; }
            set { myStateMachineHeartbeats = value; }
        }

        public bool IsSeqHBOK { get; set; }

        public bool IsSMHBOK { get; set; }

        public bool IsEngHBOK { get; set; }

        #endregion//Properties...End//

        #region//Methods//
        
        #region//Timer Related//

        private void InitTimer()
        {
            myTimer = new System.Windows.Forms.Timer();
            myTimer.Interval = 1000;
            myTimer.Tick += myTimer_Tick;
            myTimer.Enabled = true;
        }

        public List<int> seqStatus = new List<int>();
        public List<int> engStatus = new List<int>();
        public List<int> smStatus = new List<int>();
        private readonly object loacker = new object();
        private void myTimer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            SeqHeartbeatLeds.UpdateAllLed();
            EngineHeartbeatLeds.UpdateAllLed();
            StateMachineHeartbeatLeds.UpdateAllLed();

            List<int> isSeqOk = new List<int>();
            List<int> isEngOk = new List<int>();
            List<int> isSmOk = new List<int>();


            for (int i = 0; i < SeqHeartbeatLeds.LEDCount; i++)
            {
                isSeqOk.Add((int)SeqHeartbeatLeds[i].State);
                isEngOk.Add((int)EngineHeartbeatLeds[i].State);
                
            }

            for (int i = 0; i < StateMachineHeartbeatLeds.LEDCount; i++)
            {
                isSmOk.Add((int)StateMachineHeartbeatLeds[i].State);
            }
            lock (loacker)
            {
                seqStatus.Clear();
                seqStatus = isSeqOk;
                engStatus.Clear();
                engStatus = isEngOk;
                smStatus.Clear();
                smStatus = isSmOk;
            }

            for (int i = 0; i < SeqHeartbeatLeds.LEDCount; i++)
            {
                if (isSeqOk[i] != 0)
                {
                    IsSeqHBOK = false;
                    break;
                }
                IsSeqHBOK = true;
            }
            for (int i = 0; i < EngineHeartbeatLeds.LEDCount; i++)
            {
                if (isEngOk[i] != 0)
                {
                    IsEngHBOK = false;
                    break;
                }
                IsEngHBOK = true;
            }
            for (int i = 0; i < StateMachineHeartbeatLeds.LEDCount; i++)
            {
                if (isSmOk[i] != 0)
                {
                    IsSMHBOK = false;
                    break;
                }
                IsSMHBOK = true;
            }
        }

        #endregion//Timer Related//

        public int BindSequencerListeners(ref List<SequencerListener> listeners)
        {
            if (null == listeners || listeners.Count < 0 || listeners.Count != this.Slots * this.Modules)
            {
                MessageBox.Show("BindSeqListeners failed!!! SequencerListener list is null or it's count is incorrect!");
                return -1;
            }

            for (int i = 0; i < this.Slots * this.Modules; i++)
            {
                int rst = listeners[i].RegisterUser(SeqHeartbeatLeds[i]);
                if (0 != rst)
                {
                    MessageBox.Show("Heartbeat LED Bind Sequencer Listener[" + listeners[i].SubscribeAddress + "] Event Failed!!!");
                }
            }
            return 0;
        }

        public int BindEngineConnectors(ref List<EngineConnector> connectors)
        {
            if (null == connectors || connectors.Count <= 0 || connectors.Count != this.Slots * this.Modules)
            {
                MessageBox.Show("BindEngineConnectors failed!!! EnginConnector list is null or it's count is incorrect!");
                return -1;
            }

            for (int i = 0; i < this.Slots * this.Modules; i++)
            {
                int rst = connectors[i].RegisterUser(EngineHeartbeatLeds[i]);
                if (0 != rst)
                {
                    MessageBox.Show("Heartbeat LED Bind Engine Connector[" + connectors[i].SubscribeAddress + "] Event Failed!!!");
                }
            }
            return 0;
        }

        public int BindStateMachineConnectors(ref List<StateMachineConnector> connectors)
        {
            if (null == connectors || connectors.Count <= 0 || connectors.Count != this.Modules)
            {
                MessageBox.Show("BindStateMachineConnectors failed!!! StateMachineConnector list is null or it's count is incorrect!");
                return -1;
            }

            for (int i = 0; i < this.Modules; i++)
            {
                connectors[i].evt_SM_Heartbeat += StationHeartbeatPanel_evt_SM_Heartbeat;
                connectors[i].RegisterUser(StateMachineHeartbeatLeds[i]);
                //connectors[i].BindSMConnectorEventHandlers(StateMachineHeartbeatLeds[i]);
            }
            return 0;
        }

        private void StationHeartbeatPanel_evt_SM_Heartbeat(object sender, SMHeartBeatEventArgs arg)
        {
            
        }

        #endregion//Methods...END//
    }
}
