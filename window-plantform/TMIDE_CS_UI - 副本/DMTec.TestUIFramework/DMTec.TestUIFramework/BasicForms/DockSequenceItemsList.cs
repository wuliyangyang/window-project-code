#region#Copyright Information#
///Created by JimmyGong 2017.01.01
///For IA978 Project UI Design.
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;//
using System.Reflection;
//
using DMTec.TestUIFramework.DataModel;
using DMTec.TMListener;
//
namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DockSequenceItemsList : WeifenLuo.WinFormsUI.Docking.DockContent
    {

        #region//Members//
        /// <summary>
        /// DataTable for Test Item List
        /// </summary>
        DataTable m_dt_ItemList;

        /// <summary>
        /// 
        /// </summary>
        TestItemEndArgs myTestEndArg;

        /// <summary>
        /// Just for unit test
        /// </summary>
        System.Windows.Forms.Timer myTimer;

        int counter = 0;

        bool _isConsoleEnable = false;

        //Random myRandom;
        //private List<TestItemEndArgs> myListItems;

        private MultiThreadBindingList<TestItemEndArgs> myBdListItems;
        //private BindingList<TestItemEndArgs> myBdListItems = new BindingList<TestItemEndArgs>();
        //private BindingSource myDgvBindSource;

        delegate void delegate_Dgv_BindTable(DataTable table);
        //delegate void delegate_Dgv_BindList(BindingList<TestItemEndArgs> listItems);
        delegate void delegate_Dgv_BindList(MultiThreadBindingList<TestItemEndArgs> listItems);
        delegate void delegate_Dgv_BindListClear();

        delegate void delegate_Dgv_BindSource(BindingSource bds);

        #endregion//Members...End//

        #region//Constructors//
        /// <summary>
        /// 
        /// </summary>
        public DockSequenceItemsList()
        {
            InitializeComponent();
            InitMembers();
        }

        #endregion//Constructors...End//

        #region//Methods//

        /// <summary>
        /// 
        /// </summary>
        private void InitMembers()
        {
            //m_dt_ItemList = new DataTable();
            //myRandom = new Random(100);
            myTestEndArg = new TestItemEndArgs();
            InitItemList();
            //For self test...
            //m_dt_ItemList = CreateDT();
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void InitTimer()
        {
            myTimer = new System.Windows.Forms.Timer();
            myTimer.Interval = 50;
            myTimer.Tick += TimerOutHandle;
        }

        /// <summary>
        /// Init itemList DataGridView.
        /// </summary>
        private void InitItemList()
        {
            SetDgvDoubleBuffer(ref dgv_TestItems);

            dgv_TestItems.Font = new Font("Microsoft YaHei UI", 10);  
            dgv_TestItems.GridColor = Color.Blue;
            dgv_TestItems.RowsDefaultCellStyle.BackColor = Color.White;
            dgv_TestItems.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            dgv_TestItems.ReadOnly = true;
            dgv_TestItems.AllowUserToAddRows = false;
            dgv_TestItems.AllowUserToDeleteRows = false;
            dgv_TestItems.AllowUserToOrderColumns = false;
            dgv_TestItems.AllowUserToResizeColumns = false;
            dgv_TestItems.AllowUserToResizeRows = false;
            dgv_TestItems.AutoResizeColumnHeadersHeight();
            dgv_TestItems.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dgv_TestItems.ScrollBars = ScrollBars.Both;
            dgv_TestItems.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            dgv_TestItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_TestItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_TestItems.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_TestItems.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_TestItems.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn TID = new DataGridViewTextBoxColumn();
            TID.Name = "tidColumn";
            TID.DataPropertyName ="tid";
            TID.HeaderText = "TID";
            dgv_TestItems.Columns.Add(TID);

            DataGridViewTextBoxColumn VALUE = new DataGridViewTextBoxColumn();
            VALUE.Name = "valueColumn";
            VALUE.DataPropertyName = "value";
            VALUE.HeaderText = "VALUE";
            dgv_TestItems.Columns.Add(VALUE);

            DataGridViewTextBoxColumn RESULT = new DataGridViewTextBoxColumn();
            RESULT.Name = "resultColumn";
            RESULT.DataPropertyName = "result";
            RESULT.HeaderText = "RESULT";
            dgv_TestItems.Columns.Add(RESULT);

            //myListItems = new List<TestItemEndArgs>() { new TestItemEndArgs() };
            //myBdListItems = new BindingList<TestItemEndArgs>(myListItems);
            //DgvBindListSource(myBdListItems);
            //dgv_TestItems.DataSource = myBdListItems;
            //myDgvBindSource = new BindingSource(myBdListItems, null);
            //myDgvItemList.DataSource = myDgvBindSource;

            myBdListItems = new MultiThreadBindingList<TestItemEndArgs>();
            myBdListItems.SynchronizationContext = SynchronizationContext.Current;

            //myDgvBindSource.DataSource = myBdListItems;
            //myUIBindingSource.DataSource = myBdListItems;
            //myUIBindingSource.DataMember = myUIBindingSource.DataMember;
            if (null != dgv_TestItems.DataSource) dgv_TestItems.DataSource = null;
            SetDgvBindList(myBdListItems);
            //Application.DoEvents();
        }

        /// <summary>
        /// Open Double Buffer of DataGridView in case of flicker.
        /// </summary>
        /// <param name="dgv"></param>
        private void SetDgvDoubleBuffer(ref DataGridView dgv)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, true, null);
        }

        /// <summary>
        /// Binding DataGridView's Source.
        /// </summary>
        /// <param name="dt">DataSource is DataTable.</param>
        public void SetDgvBindTable(DataTable dt)
        {
            if (dgv_TestItems.InvokeRequired)
            {
                this.BeginInvoke(new delegate_Dgv_BindTable(SetDgvBindTable), new object[] { m_dt_ItemList });
                return;
            }
            else
            {
                dgv_TestItems.DataSource = dt;
                dgv_TestItems.Invalidate();
            }
            //Application.DoEvents();
            /*dgv_ItemList.Columns[0].Visible = false;
            dgv_ItemList.Columns[2].Visible = false;
            dgv_ItemList.Columns[3].Visible = false;
            dgv_ItemList.Columns[4].Visible = false;
            dgv_ItemList.Columns[5].Visible = false;
            dgv_ItemList.Columns[6].Visible = false;
            dgv_ItemList.Columns[7].Visible = false;
            dgv_ItemList.Columns[8].Visible = false;
            dgv_ItemList.Columns[9].Visible = false;
            dgv_ItemList.Columns[10].Visible = false;
            myTimer.Start();*/
        }

        private void SetDgvBindList(BindingList<TestItemEndArgs> listItem)
        {
            if (dgv_TestItems.InvokeRequired)
            {
                this.BeginInvoke(new delegate_Dgv_BindList(SetDgvBindList), new object[] { listItem });
                return;
            }
            else
            {
                dgv_TestItems.DataSource = listItem;
                if (listItem.Count < 1) return;
                //dgv_TestItems.FirstDisplayedScrollingRowIndex = listItem.Count();
                dgv_TestItems.Invalidate();
            }
            //Application.DoEvents();
        }

        private void SetDgvBindSource(BindingSource bds)
        {
            if (dgv_TestItems.InvokeRequired)
            {
                BeginInvoke(new delegate_Dgv_BindSource(SetDgvBindSource), new object[] { bds });
                return;
            }
            else
            {
                dgv_TestItems.DataSource = bds;
            }
            //Application.DoEvents();
        }

        private void SetBindingListClear()
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new delegate_Dgv_BindListClear(SetBindingListClear));
                return;
            }
            else
            {
                myBdListItems.Clear();
            }
            //Application.DoEvents();
        }
        #endregion//Methods...End//

        #region//Events//

        /// <summary>
        /// For timer out event call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TimerOutHandle(object sender, EventArgs e)
        {
            //string tidStr = "testitem"+ (++counter).ToString();
            //string valStr = "VAL_"+ counter.ToString();
            //string rst;

            //int r = myRandom.Next(50, 120);
            //if (0 == r % 2)
            //{
            //    rst = "Pass";
            //}
            //else
            //{
            //    rst = "Fail";
            //}
            //myTestEndArg.tid = tidStr;
            //myTestEndArg.value = valStr;
            //myTestEndArg.result = rst;
            //TestItemUpdateHandle(myTestEndArg);
            //if (counter >= m_dt_ItemList.Rows.Count) counter = 0;
        }

        /// <summary>
        /// Handle sequence start event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void SeqStartHandle(object sender, SeqStartArgs arg)
        {
            //if (IsConsoleEnable) Console.WriteLine("---[ItemsListMSG]---One Seq Start! ");

            if (0 != myBdListItems.Count)
            {
                SetBindingListClear();
                SetDgvBindList(myBdListItems);
            }

            //Console.WriteLine("---[ItemsListMSG]---BindingList Count = [" + myBdListItems.Count.ToString() + "]");
        }

        /// <summary>
        /// Handle sequence end event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void SeqEndHandle(object sender, SeqEndArgs arg)
        {//TODO
        }

        /// <summary>
        /// Handle test item start event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void ItemStartHandle(object sender, SeqItemStartArgs arg)
        {//TODO
        }

        /// <summary>
        /// Handle test item end event.
        /// </summary>
        /// <param name="sender">Sequence Listener Instance</param>
        /// <param name="arg">Msg Information</param>
        public void ItemEndHandle(object sender, SeqItemEndArgs arg)
        {
            
            //string msg = "---[ItemsListMSG]---One Item End! \r\n" + "tid:" + itemEndArg.tid + " value:" + itemEndArg.value + " Result:" + itemEndArg.result;
            //if(IsConsoleEnable) Console.WriteLine(msg);
            using(var itemEndArg = new TestItemEndArgs())
            {
                itemEndArg.tid = arg.tid;
                itemEndArg.value = arg.value;
                itemEndArg.result = arg.result.ToString();

                myBdListItems.Add(itemEndArg);
            }
            //dgv_TestItems.CurrentCell(,) ;

            //if (IsConsoleEnable) Console.WriteLine("---[ItemsListMSG]---One Item Add to BindList[" + myBdListItems.Count.ToString() + "]");
            
            //myItemEndArgsBindList.Add(testEndArg);
            //if (myDgvItemList.InvokeRequired)
            //{
            //    Invoke(new delegateDvgOperate(updateTestItem), new object[] { testEndArg });
            //}
            //else
            //{
            //    updateTestItem(testEndArg);
            //}
            //if (counter >= m_dt_ItemList.Rows.Count) counter = 0;            
        }

        /// <summary>
        /// Update One Test Item. 
        /// </summary>
        /// <param name="tid">TestItem ID</param>
        /// <param name="value">Test Value</param>
        /// <param name="result">Test Result</param>
        private void TestItemUpdateHandle(TestItemEndArgs arg)
        {
            int count = dgv_TestItems.RowCount;
            for (int i = 0; i < count; i++)
            {
                string tidStr = dgv_TestItems.Rows[i].Cells["tid"].Value as string;
                if (tidStr == arg.tid)
                {
                    dgv_TestItems.Rows[i].Cells["val"].Value = arg.value;
                    if (arg.result == "0")
                    {
                        //dgv_ItemList.Rows[i].Cells["result"].Style.ForeColor = Color.Red;
                        dgv_TestItems.Rows[i].Cells["result"].Style.BackColor = Color.OrangeRed;
                    }
                    else if (arg.result == "1")
                    {
                        //dgv_ItemList.Rows[i].Cells["result"].Style.ForeColor = Color.LawnGreen;
                        dgv_TestItems.Rows[i].Cells["result"].Style.BackColor = Color.LightGreen;
                    }
                    dgv_TestItems.Rows[i].Cells["result"].Value = arg.result;
                    //dgv_ItemList.Rows[i].Selected = true;
                    dgv_TestItems.CurrentCell = dgv_TestItems.Rows[i].Cells["tid"];
                    //dgv_ItemList.FirstDisplayedScrollingRowIndex = dgv_ItemList.Rows[i].Index; 
                    //Application.DoEvents();
                    dgv_TestItems.Invalidate();
                }
            }
        }

        /// <summary>
        /// Item List Event Handle.______Not Use Now!!!__________2017.01.24...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="list"></param>
        public void ItemListHandle(object sender, SeqItemListArgs list)
        {
            m_dt_ItemList = new DataTable();
            m_dt_ItemList.Columns.Add("tid", Type.GetType("System.String"));
            m_dt_ItemList.Columns.Add("val", Type.GetType("System.String"));
            m_dt_ItemList.Columns.Add("result", Type.GetType("System.String"));

            foreach (SequenceItemArgs arg in list.ItemList)
            {
                m_dt_ItemList.Rows.Add(new object[] { arg.TID, arg.VAL, null });
            }

            SetDgvBindTable(m_dt_ItemList);
        }

        /// <summary>
        /// Draw index name in ColumnHead.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_ItemList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgv_TestItems.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((Convert.ToInt32(e.RowIndex) + 1).ToString(System.Globalization.CultureInfo.CurrentCulture), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            } 
        }

        #endregion//Events...End//

        #region//Properties//
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsConsoleEnable
        {
            get
            {
                return _isConsoleEnable;
            }
            set
            {
                _isConsoleEnable = value;
            }
        }

        public string TesterID { get; set; }

        public string ChamberID { get; set; }

        #endregion//Properties...End//

    }

}

