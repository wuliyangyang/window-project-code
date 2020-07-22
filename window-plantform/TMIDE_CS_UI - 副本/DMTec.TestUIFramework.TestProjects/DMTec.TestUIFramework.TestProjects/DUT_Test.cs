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
    public partial class DUT_Test : Form
    {

        private const int COLUMNS = 4;
        private const int ROWS = 5;

        readonly int dutCount = 10;

        List<BasicControls.DUT> DutList;

        public DUT_Test()
        {
            InitializeComponent();
            InitUI();
        }


        public DUT_Test(int i):this()
        {
            dutCount = (i > 0) ? i : 10;
        }


        public int DutAmount { get { return dutCount; } }

        private void InitUI()
        {
            DutList = new List<BasicControls.DUT>();
        }


        private void DUT_Test_Load(object sender, EventArgs e)
        {

            TableLayoutPanel tlp_Main = new TableLayoutPanel();
            tlp_Main.AutoScroll = true;
            tlp_Main.Padding = new System.Windows.Forms.Padding(9);
            tlp_Main.Dock = DockStyle.Fill;
            this.Controls.Add(tlp_Main);
            this.SuspendLayout();

            tlp_Main.SuspendLayout();

            tlp_Main.ColumnCount = COLUMNS ;
            tlp_Main.RowCount = DutAmount / COLUMNS;

            int width = (int)((this.Width - COLUMNS * 6 - 50) / COLUMNS);

            int height = (int)((this.Height - ROWS * 6 - 50) / ROWS);

            int column = 0;
            int row = 0;

            for(int i = 0; i < DutAmount; i++ )
            {
                DMTec.TestUIFramework.BasicControls.DUT dut = new BasicControls.DUT(i.ToString());
                dut.Width = width;
                dut.Height = height;
                dut.Margin = new Padding(3);

                column = (int)(i % COLUMNS);
                row = (int)(i / COLUMNS);

                tlp_Main.Controls.Add(dut, column, row);
                tlp_Main.SetColumn(dut, column);
                tlp_Main.SetRow(dut, row);

                dut.Dock = DockStyle.Fill;
                DutList.Add(dut);//Push new dut to list.
            }

            tlp_Main.ResumeLayout(false);
            tlp_Main.PerformLayout();

            this.ResumeLayout(false);
            this.PerformLayout();

        }




    }
}
