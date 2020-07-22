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

namespace DMTec.TestUIFramework.BasicControls
{
    public partial class ChamberHeartbeatPanel : UserControl
    {
        
        #region//Constructors//

        public ChamberHeartbeatPanel()
        {
            InitializeComponent();
            
            IsSeqHBOK = false;
            IsSMHBOK = true;
            IsEngHBOK = true;

            //seqBulb = new ColorBulb();
            //seqBulb.Dock = DockStyle.Fill;
            //smBulb = new ColorBulb();
            //smBulb.Dock = DockStyle.Fill;
            //engBulb = new ColorBulb();
            //engBulb.Dock = DockStyle.Fill;

            //tlp_Main.Controls.Add(seqBulb, 2, 0);
            //tlp_Main.Controls.Add(smBulb, 4, 0);
            //tlp_Main.Controls.Add(engBulb, 6, 0);

            //seqLED = new LEDControl();
            //seqLED.Dock = DockStyle.Left;
            //seqLED.LEDCircleColor = Color.Transparent;
            //smLED = new LEDControl();
            //smLED.Dock = DockStyle.Left;
            //smLED.LEDCircleColor = Color.Transparent;
            //engLED = new LEDControl();
            //engLED.Dock = DockStyle.Left;
            //engLED.LEDCircleColor = Color.Transparent;

            //tlp_Main.Controls.Add(seqLED, 2, 0);
            //tlp_Main.Controls.Add(smLED, 4, 0);
            //tlp_Main.Controls.Add(engLED, 6, 0);
        }

        #endregion//Constructors//

        #region//Members//

        //ColorBulb seqBulb;
        //ColorBulb smBulb;
        //ColorBulb engBulb;
        
        System.Timers.Timer flushTimer;

        #endregion//Members...End//

        #region//Properties//

        public bool IsSeqHBOK { get; set; }

        public bool IsSMHBOK { get; set; }

        public bool IsEngHBOK { get; set; }

        #endregion//Properties...End//

        private void ChamberHeartbeatPanel_Load(object sender, EventArgs e)
        {
            //flushTimer = new System.Timers.Timer();
            //flushTimer.Interval = 1000;
            //flushTimer.Elapsed += TimeoutEventHandler;
            //flushTimer.Enabled = true;
        }

        private void TimeoutEventHandler(object sender, ElapsedEventArgs e)
        {
            //RefrushLeds();
        }

        private void RefrushLeds()
        {
            if (IsSeqHBOK)
            {
                seqBulb.Color = Color.Green;
            }
            else
            {
                seqBulb.Color = Color.Red;

            }
            seqBulb.On = !seqBulb.On;

            if (IsSMHBOK)
            {
                smBulb.Color = Color.Green;
            }
            else
            {
                smBulb.Color = Color.Red;
            }
            smBulb.On = !smBulb.On;
            if (IsEngHBOK)
            {
                engBulb.Color = Color.Green;
            }
            else
            {
                engBulb.Color = Color.Red;
            }
            engBulb.On = !engBulb.On;
        }

        public void SetBulbStatus(bool isSeqOK, bool isSMOK, bool isEngOK)
        {
            IsSeqHBOK = isSeqOK;
            IsSMHBOK  = isSMOK;
            IsEngHBOK = isEngOK;
            RefrushLeds();
        }
    }
}
