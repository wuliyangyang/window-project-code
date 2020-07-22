using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DMTec.TestUIFramework.BasicControls;
using DMTec.TestUIFramework.BasicForms;
using DMTec.TestUIFramework.CommonAPI;
using DMTec.TestUIFramework.DataModel;
using DMTec.TestUIFramework.Interface;
using DMTec.TestUIFramework;
using DMTec.TMListener.Interface;
using DMTec.TMListener;

namespace DMTec.TestSuite.CommonGUI
{
    public partial class SingleStatisticsPanel : Form, ISequencerListenerUser
    {
        private const int _offsetH = 25;
        private const int _offsetW = 15;
        private const int W = 385;
        private const int H = 120;
        private SystemConfigInfo myConfig;
        private List<StatisticInfoPanel> spList = new List<StatisticInfoPanel>();
        public SingleStatisticsPanel()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            myConfig = Global.GetConfigInstance();
            InitPanel();
            this.Size = new Size(W+_offsetW, myConfig.Slots * H + _offsetH);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void InitPanel()
        {
            for (int i = 0; i < myConfig.Slots; i++)
            {
                StatisticInfoPanel p = new StatisticInfoPanel();
                p.Name = "SinglePanel" + i.ToString();
                p.Location = new Point(_offsetW, i * p.Height);
                p.SetPath(Global.GetGlobalInstance().GetPath());
                p.updateStatisticsFromFile();
                spList.Add(p);
                this.Controls.Add(p);

                Label lb = new Label();
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.Font = new Font(new FontFamily("新宋体"),14);
                lb.Height = p.Height-2;
                lb.Width = _offsetW;
                lb.Location = new Point(0, i * p.Height-2);
                lb.Text = i.ToString();
                this.Controls.Add(lb);
            }

            Button btn = new Button();
            btn.Height = btn.Height+15;
            btn.Location = new Point(0, myConfig.Slots * spList[0].Height);
            btn.Text = "ALlClear";
            btn.Click += Btn_Click;
            this.Controls.Add(btn);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure to reset statistics data?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                foreach (var sp in spList)
                {
                    sp.ResetStatistics();
                }
            }
           
        }

        public void BindSequencerListeners(ref List<SequencerListener> list)
        {
            foreach (SequencerListener listener in list)
            {
                listener.RegisterUser(this);
            }
        }
        public int BindSequencerListener(ref SequencerListener listener)
        {
            listener.RegisterUser(this);
            return 0;
        }

        public int UnbindSequencerListener(ref SequencerListener listener)
        {
            listener.RemoveUser(this);
            return 0;
        }

        public void OnSequenceStart(object sender, SeqStartArgs arg)
        {
            //throw new NotImplementedException();
        }

        private readonly object objLocker = new object();
        public void OnSequenceEnd(object sender, SeqEndArgs arg)
        {
            lock (objLocker)
            {
                int l = arg.Publisher.Length;
                int index = int.Parse(arg.Publisher.Substring(l - 1, 1));
                bool isPass = ("1" == arg.result.ToLower()) ? true : false;
                if (isPass)
                {
                    spList[index].AddPassResult();
                }
                else
                {
                    spList[index].AddFailResult();
                }
            }
        }
        public new void Close()
        {
            this.Dispose();
        }

        public void OnSequenceItemStart(object sender, SeqItemStartArgs arg)
        {
            //throw new NotImplementedException();
        }

        public void OnSequenceItemEnd(object sender, SeqItemEndArgs arg)
        {
            //throw new NotImplementedException();
        }

        public void OnSequenceAttributeFound(object sender, SeqAttrFoundArgs arg)
        {
            //throw new NotImplementedException();
        }

        public void OnSequenceErrorReport(object sender, SeqReportErrorArgs arg)
        {
            //throw new NotImplementedException();
        }

        public void OnSequenceUOPDetect(object sender, SeqUopDetectArgs arg)
        {
            //throw new NotImplementedException();
        }

        public void OnSequenceItemList(object sender, SeqItemListArgs arg)
        {
            //throw new NotImplementedException();
        }

        public void OnSequenceHeartbeat(object sender, SeqHeartBeatArgs arg)
        {
            //throw new NotImplementedException();
        }

        public void OnSequencerListenerLogInfo(object sender, string msg)
        {
            //throw new NotImplementedException();
        }

        public void OnSequencerListenerLogWarn(object sender, string msg)
        {
            //throw new NotImplementedException();
        }

        public void OnSequencerListenerLogError(object sender, string msg)
        {
            //throw new NotImplementedException();
        }

        public void OnSequencerMessage(object sender, string msg)
        {
            //throw new NotImplementedException();
        }
    }
}
