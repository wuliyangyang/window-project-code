using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMTec.TestUIFramework.Common;
using DMTec.TestUIFramework.DataModel;

namespace DMTec.TestUIFramework.BasicControls
{
    public partial class LedArrayPanel : UserControl
    {

        #region//Members//

        protected int count = 1;
        protected List<LedLabel> myLabelLeds;
        protected Timer myTimer;

        #endregion//Members___END//

        #region//Constructors//

        public LedArrayPanel()
        {
            InitializeComponent();
            InitTimer();
            InitMembers();
        }

        public LedArrayPanel(int count) : this()
        {
            LEDCount = count;
        }

        #endregion//Constructors___END//

        #region//Inderer//

        public LedLabel this[int i]
        {
            get 
            {
                if (i < 0 || i > (myLabelLeds.Count - 1)) return null;
                else return myLabelLeds[i]; 
            }
            set 
            {
                if (i < 0 || i > (myLabelLeds.Count - 1)) return;
                myLabelLeds[i] = value;
            }
        }

        #endregion//Inderer___END//

        #region//Properties//

        [BrowsableAttribute(true)]
        [DescriptionAttribute("How many Leds will init and display.")]
        public virtual int LEDCount
        {
            get { return count;}
            set { InitLeds(value);}
        }

        [BrowsableAttribute(false)]
        [DescriptionAttribute("The timers enable property.")]
        public virtual bool TimerEnable
        {
            get
            {
                return myTimer.Enabled;
            }
            set
            {
                myTimer.Enabled = value;
            }
        }

        #endregion//Properties___END//

        #region//Protect Methods//

        protected virtual void InitLeds(int amount)
        {
            //if (amount <= 0) return;
            count = amount;
            if (count <= 0) count = 1;
            if (count > Consts.Fixture_Station_Slots_Max) count = Consts.Fixture_Station_Slots_Max;
            
            myLabelLeds = new List<LedLabel>();

            this.tlpMain.SuspendLayout();
            this.SuspendLayout();

            tlpMain.Controls.Clear();
            tlpMain.ColumnStyles.Clear();
            float percentPerColumn = (float)100 / count;
            tlpMain.ColumnCount = count+1;
            for(int i=0; i<count; i++)
            {
                LedLabel led = new LedLabel(i.ToString());
                led.Dock  = DockStyle.Fill;
                led.State = LedState.ERROR;
                led.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                myLabelLeds.Add(led);

                tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
                tlpMain.Controls.Add(led);
                tlpMain.SetColumn(led, i);
                tlpMain.SetRow(led, 0);
            }
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            //tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.tlpMain.PerformLayout();
            this.tlpMain.ResumeLayout();
            this.ResumeLayout();
        }

        protected virtual void InitMembers()
        {
            count = 1;
        }

        protected virtual void InitTimer()
        {
            myTimer = new Timer();
            myTimer.Interval = 1000;
            myTimer.Tick += myTimer_Tick;
        }

        protected virtual void myTimer_Tick(object sender, EventArgs e)
        {
            UpdateAllLed();
        }

        #endregion//Protect Methods___END//

        #region//Public Methods//

        public virtual void UpdateAllLed()
        {
            if (null == myLabelLeds || myLabelLeds.Count <= 0) return;
            foreach (LedLabel led in myLabelLeds)
            {
                led.UpdateState();
            }
        }

        public virtual void SetLedStatus(int i, LedState state)
        {
            this[i].State = state;
        }

        #endregion//Public Methods___END//

    }
}
