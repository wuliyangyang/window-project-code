using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicForms
{
    public partial class SnInputDockPanel : WeifenLuo.WinFormsUI.Docking.DockContent
    {

        public int Slots { private set; get; }

        public string[] SnArray { get; set; }

        private int index = 0;

        public SnInputDockPanel()
        {
            InitializeComponent();
            Slots = 2;
            SnArray = new string[Slots];
        }

        public SnInputDockPanel(int slots, string firstSN) : base()
        {
            Slots = slots;
            SnArray = new string[Slots];
            SnArray[index++] = firstSN;
        }

        private void tb_SN_KeyDown(object sender, KeyEventArgs e)
        {

        }


    }
}
