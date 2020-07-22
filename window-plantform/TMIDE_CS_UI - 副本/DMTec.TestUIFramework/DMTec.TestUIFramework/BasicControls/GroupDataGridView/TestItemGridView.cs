using DMTec.TestUIFramework.Common;
using DMTec.TestUIFramework.Interface;
using DMTec.TMListener;
using DMTec.TMListener.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// One Extended DataGridView Control Who Can Group TestItems by Group Property of Sequence Item.
    /// </summary>
    [Description("One Extended DataGridView Control Who Can Group TestItems by Group Property of Sequence Item.")]
    public class TestItemGridView : GroupDataGridView, ISequencerListenerUser 
    {
        #region//Members//
        private readonly object objLocker1 = new object();
        private readonly object objLocker2 = new object();
        private const int UUT_START_COLUMN = 3;

        List<DataGridViewColumn> myColumnsList;
        List<DataGridViewRow> myRowsList;

        List<ToolStripMenuItem> myMenuItemsList;
        ContextMenuStrip myContextMenuStrip;

        List<SequencerListener> mySeqListenerList;

        private int _slots = Consts.Fixture_Station_Slots_Max;

        Dictionary<string, bool> myColumnVisibleDict;

        DataGridViewCellStyle passCellStyle;
        DataGridViewCellStyle failCellStyle;
        

        #endregion//Members---END//

        #region//Constructors//

        public TestItemGridView(int slots)
        {
            Slots = slots;
            Init();
        }

        public TestItemGridView(): this(Consts.Fixture_Station_Slots_Max){}

        #endregion//Constructors---END//

        #region//Propeties//

        [Browsable(true)]
        [DefaultValue("")]
        [Description("")]
        [Category("")]
        public int Slots
        {
            get { return _slots; }
            set
            {
                _slots = (value < Consts.Fixture_Station_Slots_Max || value > 0) ? value : Consts.Fixture_Station_Slots_Max;
                //InitColumns(value);
            }
        }

        //[Browsable(true)]
        //[DefaultValue("")]
        //[Description("")]
        //[Category("")]
        //public Dictionary<string, bool> ColumnVisibleList
        //{
        //    get { return myColumnVisibleDict; }
        //    private set { myColumnVisibleDict = value; }
        //}

        #endregion//Properties---END//

        #region//Event Handlers//

        #region ISequencerListenerUser Members

        public void OnSequenceStart(object sender, TMListener.SeqStartArgs arg)
        {
            lock(objLocker1)
            {
                int index = -1;
                if (null == sender) return;
                if (sender is SequencerListener)
                {
                    index = mySeqListenerList.IndexOf(sender as SequencerListener);
                    if (index < 0) return;//If SequencerListener is not in list, do nothing.
                }

                if (null == this.Columns["UUT" + index.ToString()]) return;//
                if (null == myRowsList) return;
                foreach (GroupDataGridViewRow row in myRowsList)
                {
                    row.Cells["UUT" + index.ToString()].Value = "";
                }
                this.InvalidateColumn(this.Columns["UUT" + index.ToString()].Index);
            }
        }

        public void OnSequenceEnd(object sender, TMListener.SeqEndArgs arg)
        {
            
        }

        public void OnSequenceItemStart(object sender, TMListener.SeqItemStartArgs arg)
        {

        }
        private int lastRowIndex = -1;
        private int curRowIndex = -1;

        public void OnSequenceItemEnd(object sender, TMListener.SeqItemEndArgs arg)
        {
            lock(objLocker2)
            {
                int listenerIndex = -1;
                if (null == sender) return;
                if (sender is SequencerListener)
                {
                    listenerIndex = mySeqListenerList.IndexOf(sender as SequencerListener);
                    if (listenerIndex < 0) return;//If SequencerListener is not in list, do nothing.
                }

                if (null == this.Columns["UUT" + listenerIndex.ToString()]) return;//
                if (null == myRowsList) return;

                foreach (GroupDataGridViewRow row in myRowsList)
                {
                    if (arg.tid == row.Cells["TID"].Value as string)
                    {
                        curRowIndex = row.Index;
                        DataGridViewCell cell = row.Cells["UUT" + listenerIndex.ToString()];
                        if (null == cell) continue;

                        if ("true" == arg.result.ToLower())
                        {
                            cell.Style.Font = passCellStyle.Font;
                            cell.Style.ForeColor = passCellStyle.ForeColor;
                        }
                        else
                        {
                            cell.Style.Font = failCellStyle.Font;
                            cell.Style.ForeColor = failCellStyle.ForeColor;
                        }
                        cell.Value = arg.value;
                        //this.CurrentCell = cell;CurrentCell
                        //FirstDisplayedScrollingRowIndex = cell.RowIndex;
                        this.InvalidateRow(row.Index);
                        //if (curRowIndex != lastRowIndex)
                        //{
                        //    FirstDisplayedScrollingRowIndex = curRowIndex;
                        //    lastRowIndex = curRowIndex;
                        //}
                    }
                }
            }
        }

        public void OnSequenceAttributeFound(object sender, TMListener.SeqAttrFoundArgs arg)
        {

        }

        public void OnSequenceErrorReport(object sender, TMListener.SeqReportErrorArgs arg)
        {
            
        }

        public void OnSequenceUOPDetect(object sender, TMListener.SeqUopDetectArgs arg)
        {

        }

        public void OnSequenceItemList(object sender, TMListener.SeqItemListArgs arg)
        {

        }

        public void OnSequenceHeartbeat(object sender, TMListener.SeqHeartBeatArgs arg)
        {
            
        }

        public void OnSequencerListenerLogError(object sender, string msg)
        {
            
        }

        public void OnSequencerListenerLogInfo(object sender, string msg)
        {
            
        }

        public void OnSequencerListenerLogWarn(object sender, string msg)
        {
            
        }

        public void OnSequencerMessage(object sender, string msg)
        {
            
        }

        #endregion

        #endregion//Event Handlers---END//

        #region//Methods//


        private void Init()
        {
            myRowsList = new List<DataGridViewRow>();
            myColumnsList = new List<DataGridViewColumn>();
            myMenuItemsList = new List<ToolStripMenuItem>();
            mySeqListenerList = new List<SequencerListener>();

            passCellStyle = new DataGridViewCellStyle();
            passCellStyle.BackColor = System.Drawing.SystemColors.Window;
            passCellStyle.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            passCellStyle.ForeColor = System.Drawing.Color.Green;
            passCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            passCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

            failCellStyle = new DataGridViewCellStyle();
            failCellStyle.BackColor = System.Drawing.SystemColors.Window;
            failCellStyle.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            failCellStyle.ForeColor = System.Drawing.Color.Red;
            failCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            failCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

            myColumnVisibleDict = new Dictionary<string, bool>();
            myColumnVisibleDict.Add("DESCRIPTION", true);
            myColumnVisibleDict.Add("FUNCTION", true);
            //myColumnVisibleDict.Add("KEY", true);
            //myColumnVisibleDict.Add("VAL", true);
            myColumnVisibleDict.Add("PARAM1", true);
            myColumnVisibleDict.Add("PARAM2", true);
            myColumnVisibleDict.Add("TIMEOUT", true);
            
            InitContextMenu();
            InitContextMenuItem();
        }

        private void InitContextMenu()
        {
            myContextMenuStrip = new ContextMenuStrip();
            myContextMenuStrip.ShowCheckMargin = true;
            myContextMenuStrip.ShowImageMargin = false;
            myContextMenuStrip.ShowItemToolTips = true;
            myContextMenuStrip.TextDirection = ToolStripTextDirection.Horizontal;
            //this.ContextMenuStrip = myContextMenuStrip;
            this.CellMouseClick += TestItemGridView_CellMouseClick;
        }

        private void TestItemGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex < 0) return;
                myContextMenuStrip.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void InitContextMenuItem()
        {
            if (null == myColumnVisibleDict || 0 == myColumnVisibleDict.Count) return;

            myMenuItemsList.Clear();
            foreach (KeyValuePair<string, bool> item in myColumnVisibleDict)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(item.Key);
                menuItem.Checked = item.Value;
                menuItem.Click += OnMenuItemClicked;
                myMenuItemsList.Add(menuItem);
                myContextMenuStrip.Items.Add(menuItem);
            }
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            if (null == sender) return;
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                item.Checked = !item.Checked;
                if (null == this.Columns[item.Text]) return;
                this.Columns[item.Text].Visible = item.Checked;
            }
        }

        private void ColumnsInit(PropertyInfo[] properties , int slots)
        {
            this.Columns.Clear();
            myColumnsList.Clear();

            DataGridViewColumn indexColumn = new DataGridViewColumn(new DataGridViewTextBoxCell());
            indexColumn.Name = "IndexColumn";
            indexColumn.Width = 200;
            indexColumn.HeaderText = "GROUP";
            indexColumn.DisplayIndex = 0;
            myColumnsList.Add(indexColumn);

            foreach (PropertyInfo pi in properties)
            {
                DataGridViewColumn column = new DataGridViewColumn(new DataGridViewTextBoxCell());
                column.Name = pi.Name;
                column.HeaderText = pi.Name;
                myColumnsList.Add(column);
            }

            for (int n = 0; n < slots; n++)
            {
                DataGridViewColumn column = new DataGridViewColumn(new DataGridViewTextBoxCell());
                column.CellTemplate = new DataGridViewTextBoxCell();
                column.Name = "UUT" + n.ToString();
                column.HeaderText = "UUT" + n.ToString();
                myColumnsList.Add(column);
            }

            foreach (DataGridViewColumn c in myColumnsList)
            {
                this.Columns.Add(c);
            }

            this.Columns["GROUP"].Visible = false;
            foreach(KeyValuePair<string, bool> item in  myColumnVisibleDict)
            {
                this.Columns[item.Key].Visible = item.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int LoadSequencerItemList(List<SequenceItemArgs> list)
        {
            if(null == list || list.Count <= 0) return -1;

            try 
            {
                int columnCount = 0;
                //int rowIndex = 0;

                PropertyInfo[] properties = list[0].GetType().GetProperties();
                columnCount = Slots + properties.Length + 1;

                ColumnsInit(properties, Slots);

                object[] rowCells = new object[columnCount];

                int r = 0;
                ISortDataGridGroup curGroup = null;

                object result = null;
                int counter = 0; // counts number of items in the group
                //Firstly, create one group row...
                GroupDataGridViewRow row = (GroupDataGridViewRow)this.RowTemplate.Clone();
                row.IsGroupRow = true;
                result = list[0].GROUP;

                curGroup = new DataGridGroup(); // new one group
                curGroup.GroupValue = result;
                row.Group = curGroup;
                row.IsGroupRow = true;
                row.Height = curGroup.GroupHeight;
                row.CreateCells(this, curGroup.GroupValue);
                Rows.Add(row); 
                myRowsList.Add(row);

                foreach(SequenceItemArgs item in list)
                {
                    int c = 0;
                    row = (GroupDataGridViewRow)this.RowTemplate.Clone();
                    result = item.GROUP;

                    if (curGroup != null && curGroup.CompareTo(result) == 0) // item is part of the group
                    {
                        row.Group = curGroup;
                        counter++;
                    }
                    else // item is not part of the group, so create new group
                    {
                        if (curGroup != null)
                            curGroup.ItemCount = counter;

                        curGroup = new DataGridGroup(); // init
                        curGroup.GroupValue = result;
                        row.Group = curGroup;
                        row.IsGroupRow = true;
                        row.Height = curGroup.GroupHeight;
                        row.CreateCells(this, curGroup.GroupValue);
                        Rows.Add(row);
                        myRowsList.Add(row);
                        // add content row after this
                        row = (GroupDataGridViewRow)this.RowTemplate.Clone();
                        row.Group = curGroup;
                        counter = 1; // reset counter for next group
                    }

                    rowCells[c++] = counter.ToString();
                    foreach (PropertyInfo pi in properties)
                    {
                        rowCells[c++] = pi.GetValue(item, null);
                    }

                    for (int n = 0; n < Slots; n++)
                    {
                        rowCells[c++] = "";
                    }
                    row.CreateCells(this, rowCells);
                    this.Rows.Add(row);
                    myRowsList.Add(row);
                    curGroup.ItemCount = counter;
                }

                this.AllowUserToOrderColumns = true;
                for (int n = 0; n < Slots; n++)
                {
                    this.Columns["UUT" + n.ToString()].DisplayIndex = UUT_START_COLUMN + n;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -2;
            }

            return 0;
        }

        public void BindSeqListeners(ref List<SequencerListener> list)
        {
            mySeqListenerList = list;
            foreach (SequencerListener listener in mySeqListenerList)
            {
                listener.RegisterUser(this);
            }
        }

        public int LoadSequencerItemList(List<SequenceItemArgs> list, int slots)
        {
            Slots = (slots < Consts.Fixture_Station_Slots_Max || slots > 0) ? slots : Consts.Fixture_Station_Slots_Max;
            return LoadSequencerItemList(list);
        }

        public int LoadListData<T>(List<T> list) where T : new()
        {
            try
            {
                T temp = new T();
                PropertyInfo[] properties = temp.GetType().GetProperties();
                IList<T> tempList = new List<T>();

                foreach(PropertyInfo pi in properties)
                {
                    this.Columns.Add(pi.Name, pi.Name.ToLower());
                }

                for (int n = 0; n < Slots; n++)
                {
                    this.Columns.Add("UUT" + n.ToString(), "UUT" + n.ToString());
                }

                foreach (T t in list)
                {
                    DataGridViewRow row = (GroupDataGridViewRow)this.RowTemplate.Clone();

                    object[] propertyValues = new object[properties.Length + Slots];
                    int i = 0;
                    foreach (PropertyInfo pi in properties)
                    {
                        propertyValues[i++] = pi.GetValue(t, null);
                    }

                    for (int n = 0; n < Slots; n++)
                    {
                        propertyValues[i + n] = "";
                    }

                    row.CreateCells(this, propertyValues);
                    this.Rows.Add(row);
                }

                this.Columns["UUT0"].DisplayIndex = 3;


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            return 0;
        }

        private object[] GetPropertyValues<T>(T t) where T : new()
        {
            T temp = new T();
            PropertyInfo[] properties = temp.GetType().GetProperties();
            object[] propertyValues = new object[properties.Length];
            int i = 0;
            foreach(PropertyInfo pi in properties)
            {
                propertyValues[i++] = pi.GetValue(t,null);
            }
            return propertyValues;
        }

        private int UpdateItemResult(string group, string tid, string result)
        {
            return 0;
        }

        #endregion//Methods...END//


        #region ISequencerListenerUser 成员

        public int BindSequencerListener(ref SequencerListener listener)
        {
            return 0;
        }

        public int UnbindSequencerListener(ref SequencerListener listener)
        {
            return 0;
        }

        #endregion
    }
}
