using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    public partial class StatusLight : UserControl
    {
        public StatusLight()
        {
            InitializeComponent();
        }

        public void InitUI()
        {
            Pen p = new Pen(Color.Black, 2);
            Graphics g = CreateGraphics();
            g.DrawEllipse(p, 200, 200, 100, 100);
        }


    }
}
