using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.TestProjects
{
    public partial class Chamber_Test : Form
    {
        List<BasicControls.ChamberSimple> ChamberList;
        TableLayoutPanel tlp_Main;

        public Chamber_Test()
        {
            InitializeComponent();
            InitMembers();
        }

        public Chamber_Test(int amount):this()
        {
            ChamberAmount = (amount > 0) ? amount : 10;
            for (int i = 0; i < ChamberAmount; i++)
            {
                BasicControls.ChamberSimple cb = new BasicControls.ChamberSimple(i);
                ChamberList.Add(cb);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ChamberAmount
        {
            get { return _chamberAmount; }
            private set { _chamberAmount = value; }
        }private int _chamberAmount = 1;

        /// <summary>
        /// How many columns in TableLayoutPanel.
        /// </summary>
        public int Columns 
        {
            get { return _columns; }
            set { _columns = value; }
        }private int _columns = 3;


        private void InitMembers()
        {
            ChamberList = new List<BasicControls.ChamberSimple>();
            tlp_Main = new TableLayoutPanel();
            Columns = 3;
        }

        private void InitUI()
        {
            if (null == tlp_Main) return;

            this.SuspendLayout();
            tlp_Main.AutoScroll = true;
            tlp_Main.Padding = new System.Windows.Forms.Padding(9);
            tlp_Main.Dock = DockStyle.Fill;
            this.Controls.Add(tlp_Main);

            tlp_Main.SuspendLayout();

            tlp_Main.ColumnCount = Columns;
            tlp_Main.RowCount = ChamberAmount / Columns;

            int width = (int)((this.Width - Columns * 6 - 50) / Columns);

            //int height = (int)((this.Height - ROWS * 6 - 50) / ROWS);

            int column = 0;
            int row    = 0;

            for (int i = 0; i < ChamberAmount; i++)
            {
                BasicControls.ChamberSimple cb = ChamberList[i];
                cb.Width = width;
                //dut.Height = height;
                cb.Margin = new Padding(3);

                column = (int)(i % Columns);
                row = (int)(i / Columns);

                tlp_Main.Controls.Add(cb, column, row);
                tlp_Main.SetColumn(cb, column);
                tlp_Main.SetRow(cb, row);

                cb.Dock = DockStyle.Fill;
            }

            tlp_Main.ResumeLayout(false);
            tlp_Main.PerformLayout();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Chamber_Test_Load(object sender, EventArgs e)
        {
            InitUI();
        }

        private void Chamber_Test_Resize(object sender, EventArgs e)
        {
            InitUI();
        }


    }
}
