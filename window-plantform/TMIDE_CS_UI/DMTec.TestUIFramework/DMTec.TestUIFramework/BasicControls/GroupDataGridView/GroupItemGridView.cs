using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using DMTec.TestUIFramework.Interface;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// One Extended Data Grid View Control that can group item by column value.
    /// </summary>
    [Description("One Extended DataGridView Control Who Can Group TestItems by Column Value.")]
    public class GroupDataGridView : DataGridView
    {
        public GroupDataGridView() : base()
        {
            this.ReadOnly = true;
            this.EditMode = DataGridViewEditMode.EditProgrammatically;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            // very important, this indicates that a new default row class is going to be used to fill the grid
            // in this case our custom GroupDataGridViewRow class
            base.RowTemplate = new GroupDataGridViewRow();//
            this.GroupTemplate = new DataGridGroup();

            //this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;

            DataGridViewCellStyle defaultCellStyle = new DataGridViewCellStyle();
            defaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            defaultCellStyle.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            defaultCellStyle.ForeColor = System.Drawing.Color.Black;
            defaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            defaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.DefaultCellStyle = defaultCellStyle;

            DataGridViewCellStyle alternatingRowsCellStyle = new DataGridViewCellStyle();
            alternatingRowsCellStyle.BackColor = System.Drawing.SystemColors.Control;
            alternatingRowsCellStyle.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            alternatingRowsCellStyle.ForeColor = System.Drawing.Color.Black;
            alternatingRowsCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            alternatingRowsCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.AlternatingRowsDefaultCellStyle = alternatingRowsCellStyle;

            //DataGridViewCellStyle rowHeadersCellStyle = new DataGridViewCellStyle();
            //rowHeadersCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            //rowHeadersCellStyle.BackColor = System.Drawing.Color.Red;
            //rowHeadersCellStyle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //rowHeadersCellStyle.ForeColor = System.Drawing.Color.Blue;
            //rowHeadersCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            //rowHeadersCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            //this.testItemGroupGridView.RowHeadersDefaultCellStyle = rowHeadersCellStyle;

            this.GridColor = System.Drawing.Color.Black;
            this.RowTemplate.Height = 30;
            this.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CellBorderStyle = DataGridViewCellBorderStyle.RaisedHorizontal;
            this.RowHeadersVisible = false;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeRows = false;
            this.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.ClearGroups(); // reset
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewRow RowTemplate//Hide Base [RowTemplate] Property(ReadOnly).
        {
            get { return base.RowTemplate; }
        }

        private ISortDataGridGroup groupTemplate;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISortDataGridGroup GroupTemplate
        {
            get
            {
                return groupTemplate;
            }
            set
            {
                groupTemplate = value;
            }
        }

        private Image iconCollapse;
        [Category("Appearance")]
        public Image CollapseIcon
        {
            get { return iconCollapse; }
            set { iconCollapse = value; }
        }

        private Image iconExpand;
        [Category("Appearance")]
        public Image ExpandIcon
        {
            get { return iconExpand; }
            set { iconExpand = value; }
        }

        private DataSourceManager dataSource;

        /// <summary>
        /// New DataSource 
        /// Hide Base DataSource(Read Only)
        /// You can set DataSource by BindData() function.
        /// </summary>
        public new object DataSource//
        {
            get
            {
                if (dataSource == null) return null;

                // special case, data source is bound to itself.
                // for client it must look like no binding is set,so return null in this case
                if (dataSource.DataSource.Equals(this)) return null;

                // return the original data source.
                return dataSource.DataSource;
            }
        }

        #region new methods

        public void CollapseAll()
        {
            SetGroupCollapse(true);
        }

        public void ExpandAll()
        {
            SetGroupCollapse(false);
        }

        public void ClearGroups()
        {
            GroupTemplate.GroupColumn = null; //reset
            FillGrid(null);
        }

        public void BindData(object dataSource, string dataMember)
        {
            this.DataMember = dataMember;
            if (dataSource == null)
            {
                this.dataSource = null;
                Columns.Clear();
            }
            else
            {
                this.dataSource = new DataSourceManager(dataSource, dataMember);
                SetupColumns();
                FillGrid(null);
            }
        }

        public void BindData(DataTable dataSource)
        {
            this.DataMember = null;
            if (dataSource == null)
            {
                this.dataSource = null;
                Columns.Clear();
            }
            else
            {
                this.dataSource = new DataSourceManager(dataSource, null);
                SetupColumns();
                FillGrid(null);
            }
        }

        public override void Sort(System.Collections.IComparer comparer)
        {
            if (dataSource == null) // if no data source is set, then bind to the grid itself
                dataSource = new DataSourceManager(this, null);

            dataSource.Sort(comparer);
            FillGrid(groupTemplate);
        }


        public override void Sort(DataGridViewColumn dataGridViewColumn, ListSortDirection direction)
        {
            if (dataSource == null) // if no data source is set, then bind to the grid itself
                dataSource = new DataSourceManager(this, null);

            dataSource.Sort(new GroupGridRowComparer(dataGridViewColumn.Index, direction));
            FillGrid(groupTemplate);
        }

        #endregion//new methods...END//

        #region//event handlers//

        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            GroupDataGridViewRow row = (GroupDataGridViewRow)base.Rows[e.RowIndex];
            if (row.IsGroupRow)
                e.Cancel = true;
            else
                base.OnCellBeginEdit(e);
        }

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                GroupDataGridViewRow row = (GroupDataGridViewRow)base.Rows[e.RowIndex];
                if (row.IsGroupRow)
                {
                    row.Group.IsCollapsed = !row.Group.IsCollapsed;

                    //this is a workaround to make the grid re-calculate it's contents and background bounds
                    // so the background is updated correctly.
                    // this will also invalidate the control, so it will redraw itself
                    row.Visible = false;
                    row.Visible = true;
                    return;
                }
            }
            base.OnCellClick(e);
        }

        // the OnCellMouseDown is overridden so the control can check to see if the
        // user clicked the + or - sign of the group-row
        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            GroupDataGridViewRow row = (GroupDataGridViewRow)base.Rows[e.RowIndex];
            if (row.IsGroupRow && row.IsIconHit(e))
            {
                System.Diagnostics.Debug.WriteLine("OnCellMouseDown " + DateTime.Now.Ticks.ToString());
                row.Group.IsCollapsed = !row.Group.IsCollapsed;

                //this is a workaround to make the grid re-calculate it's contents and background bounds
                // so the background is updated correctly.
                // this will also invalidate the control, so it will redraw itself
                row.Visible = false;
                row.Visible = true;
            }
            else
                base.OnCellMouseDown(e);
        }

        #endregion//event handlers...END//

        #region//Grid Fill functions//

        private void SetGroupCollapse(bool isCollapsed)
        {
            if (Rows.Count == 0) return;
            if (GroupTemplate == null) return;

            // set the default grouping style template collapsed property
            GroupTemplate.IsCollapsed = isCollapsed;

            // loop through all rows to find the GroupRows
            foreach (GroupDataGridViewRow row in Rows)
            {
                if (row.IsGroupRow) row.Group.IsCollapsed = isCollapsed;
            }

            // workaround, make the grid refresh properly
            Rows[0].Visible = !Rows[0].Visible;
            Rows[0].Visible = !Rows[0].Visible;
        }

        private void SetupColumns()
        {
            ArrayList list;

            // clear all columns, this is a somewhat crude implementation
            // refinement may be welcome.
            Columns.Clear();

            // start filling the grid
            if (dataSource == null)
                return;
            else
                list = dataSource.Rows;
            if (list.Count <= 0) return;

            foreach (string c in dataSource.Columns)
            {
                int index;
                DataGridViewColumn column = Columns[c];
                if (column == null)
                    index = Columns.Add(c, c);
                else
                    index = column.Index;
                Columns[index].SortMode = DataGridViewColumnSortMode.Programmatic; // always programmatic!
            }

        }

        /// <summary>
        /// the fill grid method fills the grid with the data from the DataSourceManager
        /// It takes the grouping style into account, if it is set.
        /// </summary>
        private void FillGrid(ISortDataGridGroup groupingStyle)
        {

            ArrayList itemList;
            GroupDataGridViewRow row;

            this.Rows.Clear();

            // start filling the grid
            if (dataSource == null)
                return;
            else
                itemList = dataSource.Rows;
            if (itemList.Count <= 0) return;

            // this block is used of grouping is turned off
            // this will simply list all attributes of each object in the list
            if (groupingStyle == null)
            {
                foreach (DataSourceRow item in itemList)
                {
                    row = (GroupDataGridViewRow)this.RowTemplate.Clone();
                    foreach (object val in item)
                    {
                        DataGridViewCell cell = new DataGridViewTextBoxCell();
                        cell.Value = val.ToString();
                        row.Cells.Add(cell);
                    }
                    Rows.Add(row);
                }
            }

            // this block is used when grouping is used
            // items in the list must be sorted, and then they will automatically be grouped
            else
            {
                ISortDataGridGroup curGroup = null;
                object result = null;
                int counter = 0; // counts number of items in the group

                foreach (DataSourceRow itemRow in itemList)
                {
                    row = (GroupDataGridViewRow)this.RowTemplate.Clone();
                    result = itemRow[groupingStyle.GroupColumn.Index];
                    if (curGroup != null && curGroup.CompareTo(result) == 0) // item is part of the group
                    {
                        row.Group = curGroup;
                        counter++;
                    }
                    else // item is not part of the group, so create new group
                    {
                        if (curGroup != null)
                            curGroup.ItemCount = counter;

                        curGroup = (ISortDataGridGroup)groupingStyle.Clone(); // init
                        curGroup.GroupValue = result;
                        row.Group = curGroup;
                        row.IsGroupRow = true;
                        row.Height = curGroup.GroupHeight;
                        row.CreateCells(this, curGroup.GroupValue);
                        Rows.Add(row);

                        // add content row after this
                        row = (GroupDataGridViewRow)this.RowTemplate.Clone();
                        row.Group = curGroup;
                        counter = 1; // reset counter for next group
                    }

                    foreach (object obj in itemRow)
                    {
                        DataGridViewCell cell = new DataGridViewTextBoxCell();
                        cell.Value = obj.ToString();
                        row.Cells.Add(cell);
                    }
                    Rows.Add(row);
                    curGroup.ItemCount = counter;
                }
            }
        }

        #endregion//Grid Fill functions...END//
    }

    /// <summary>
    /// the OutlookGridRowComparer object is used to sort unbound data in the OutlookGrid.
    /// currently the comparison is only done for string values. 
    /// therefore dates or numbers may not be sorted correctly.
    /// Note: this class is not implemented optimally. It is merely used for demonstration purposes
    /// </summary>
    internal class GroupGridRowComparer : IComparer
    {
        ListSortDirection direction;
        int columnIndex;

        public GroupGridRowComparer(int columnIndex, ListSortDirection direction)
        {
            this.columnIndex = columnIndex;
            this.direction = direction;
        }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            GroupDataGridViewRow obj1 = (GroupDataGridViewRow)x;
            GroupDataGridViewRow obj2 = (GroupDataGridViewRow)y;
            return string.Compare(obj1.Cells[this.columnIndex].Value.ToString(), obj2.Cells[this.columnIndex].Value.ToString()) * (direction == ListSortDirection.Ascending ? 1 : -1);
        }
        #endregion
    }


    /// <summary>
    /// because the DataSourceRow class is a wrapper class around the real data,
    /// the compared object used to sort the real data is wrapped by this DataSourceRowComparer class.
    /// </summary>
    internal class DataSourceRowComparer : IComparer
    {
        IComparer baseComparer;
        public DataSourceRowComparer(IComparer baseComparer)
        {
            this.baseComparer = baseComparer;
        }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            DataSourceRow r1 = (DataSourceRow)x;
            DataSourceRow r2 = (DataSourceRow)y;
            return baseComparer.Compare(r1.BoundItem, r2.BoundItem);
        }

        #endregion
    }

    /// <summary>
    /// The DataSourceRow is a wrapper row class around the real bound data. This row is an abstraction
    /// so different types of data can be encapsulated in this class, although for the GroupItemGrid it will
    /// simply look as one type of data. 
    /// Note: this class does not implement all row wrappers optimally. It is merely used for demonstration purposes
    /// </summary>
    internal class DataSourceRow : CollectionBase
    {
        DataSourceManager manager;
        object boundItem;
        public DataSourceRow(DataSourceManager manager, object boundItem)
        {
            this.manager = manager;
            this.boundItem = boundItem;
        }

        public object this[int index]
        {
            get
            {
                return List[index];
            }
        }

        public object BoundItem
        {
            get
            {
                return boundItem;
            }
        }

        public int Add(object val)
        {
            return List.Add(val);
        }

    }


    /// <summary>
    /// the DataDourceManager class is a wrapper class around different types of data sources.
    /// in this case the DataSet, object list using reflection and the GroupDataGridRow objects are supported
    /// by this class. Basically the DataDourceManager works like a facade that provides access in a uniform
    /// way to the data source.
    /// Note: this class is not implemented optimally. It is merely used for demonstration purposes
    /// </summary>
    internal class DataSourceManager
    {
        private object dataSource;
        private string dataMember;

        public ArrayList Columns;
        public ArrayList Rows;

        public DataSourceManager(object dataSource, string dataMember)
        {
            this.dataSource = dataSource;
            this.dataMember = dataMember;
            InitManager();
        }

        public DataSourceManager(object dataSource) : this(dataSource,null)
        {
        }

        /// <summary>
        /// data member readonly for now
        /// </summary>
        public string DataMember
        {
            get { return dataMember; }
        }

        /// <summary>
        /// data source is readonly for now
        /// </summary>
        public object DataSource
        {
            get { return dataSource; }
        }

        /// <summary>
        /// this function initializes the DataSourceManager's internal state.
        /// it will analyze the data source taking the following source into account:
        /// - DataSet
        /// - Object array (must implement IList)
        /// - OutlookGrid
        /// </summary>
        private void InitManager()
        {
            if (dataSource is IListSource)
                InitIListSource();
            if (dataSource is IList)
                InitIList();
            if (dataSource is GroupDataGridView)
                InitGrid();
        }

        private void InitIListSource()
        {
            Columns = new ArrayList();
            Rows = new ArrayList();
            DataTable table;

            if (null == this.DataMember && dataSource is DataTable)
            {
                table = (DataTable)dataSource;
            }
            else
            {
                table = ((DataSet)dataSource).Tables[this.dataMember];
            }
 
            // use reflection to discover all properties of the object
            foreach (DataColumn c in table.Columns)
                Columns.Add(c.ColumnName);

            foreach (DataRow r in table.Rows)
            {
                DataSourceRow row = new DataSourceRow(this, r);
                for (int i = 0; i < Columns.Count; i++)
                    row.Add(r[i]);
                Rows.Add(row);
            }
        }

        private void InitIList()
        {
            Columns = new ArrayList();
            Rows = new ArrayList();
            IList list = (IList)dataSource;

            // use reflection to discover all properties of the object
            BindingFlags bf = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;
            PropertyInfo[] props = list[0].GetType().GetProperties();

            foreach (PropertyInfo pi in props)
                Columns.Add(pi.Name);

            foreach (object obj in list)
            {
                DataSourceRow row = new DataSourceRow(this, obj);
                foreach (PropertyInfo pi in props)
                {
                    object result = obj.GetType().InvokeMember(pi.Name, bf, null, obj, null);
                    row.Add(result);
                }
                Rows.Add(row);
            }
        }

        private void InitGrid()
        {
            Columns = new ArrayList();
            Rows = new ArrayList();

            GroupDataGridView grid = (GroupDataGridView)dataSource;
            // use reflection to discover all properties of the object
            foreach (DataGridViewColumn c in grid.Columns)
                Columns.Add(c.Name);

            foreach (GroupDataGridViewRow r in grid.Rows)
            {
                if (!r.IsGroupRow && !r.IsNewRow)
                {
                    DataSourceRow row = new DataSourceRow(this, r);
                    for (int i = 0; i < Columns.Count; i++)
                        row.Add(r.Cells[i].Value);
                    Rows.Add(row);
                }
            }


        }

        public void Sort(System.Collections.IComparer comparer)
        {
            Rows.Sort(new DataSourceRowComparer(comparer));
        }

    }
    

}
