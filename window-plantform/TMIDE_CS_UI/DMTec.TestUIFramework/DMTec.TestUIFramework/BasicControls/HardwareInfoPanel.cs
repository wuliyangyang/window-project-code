using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    public partial class HardwareInfoPanel : UserControl
    {
        public HardwareInfoPanel()
        {
            InitializeComponent();
        }

        private delegate void IntDelegate(int value);

        /// <summary>
        /// 设置顶针剩余使用次数显示
        /// </summary>
        /// <param name="leftTimes"></param>
        public void SetPogoPinLeftTimes(int leftTimes)
        {
            if(this.lb_PogoPInLeftTimes.InvokeRequired)
            {
                Invoke(new IntDelegate(SetPogoPinLeftTimes), leftTimes);
            }
            else
            {
                this.lb_PogoPInLeftTimes.Text = leftTimes.ToString();
            }
        }

        /// <summary>
        /// 设置光谱仪剩余使用时间显示
        /// </summary>
        /// <param name="leftTime"></param>
        public void SetSpectrographLeftTime(int leftTime)
        {
            if(this.lb_SpectrographLeftTime.InvokeRequired)
            {
                Invoke(new IntDelegate(SetSpectrographLeftTime), leftTime);
            }
            else
            {
                this.lb_SpectrographLeftTime.Text = leftTime.ToString();
            }
        }
    }
}
