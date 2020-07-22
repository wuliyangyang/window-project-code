using DMTec.TestUIFramework.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// each arrange/grouping class must implement the ISortGridGroup interface
    /// the Group object will determine for each object in the grid, whether it
    /// falls in or outside its group.
    /// It uses the IComparable.CompareTo function to determine if the item is in the group.
    /// </summary>
    public class DataGridGroup : ISortDataGridGroup
    {
        protected object val;
        protected string text;
        protected bool collapsed;
        protected DataGridViewColumn column;
        protected int itemCount;
        protected int height;

        public DataGridGroup()
        {
            GroupValue = null;
            GroupColumn = null;
            GroupHeight = 30;
        }

        #region ISortGridGroup Members

        public virtual string GroupName
        {
            get
            {
                if (column == null)
                    return string.Format(" {0}  [{1}] ", GroupValue.ToString(), itemCount == 1 ? "1 item" : itemCount.ToString() + " items");
                else
                    return string.Format("{0} : {1} ({2})", column.HeaderText/*""*/, GroupValue.ToString(), itemCount == 1 ? "1 item" : itemCount.ToString() + " items");
            }
            set { text = value; }
        }

        public virtual object GroupValue
        {
            get { return val; }
            set { val = value; }
        }

        public virtual bool IsCollapsed
        {
            get { return collapsed; }
            set { collapsed = value; }
        }

        public virtual DataGridViewColumn GroupColumn
        {
            get { return column; }
            set { column = value; }
        }

        public virtual int ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }

        public virtual int GroupHeight
        {
            get { return height; }
            set { height = value; }
        }

        #endregion

        #region ICloneable Members

        public virtual object Clone()
        {
            DataGridGroup group = new DataGridGroup();
            group.GroupColumn = this.GroupColumn;
            group.GroupValue = this.GroupValue;
            group.IsCollapsed = this.IsCollapsed;
            group.text = this.text;
            group.GroupHeight = this.GroupHeight;
            return group;
        }

        #endregion

        #region IComparable Members

        /// <summary>
        /// this is a basic string comparison operation. 
        /// all items are grouped and categorized based on their string-appearance.
        /// </summary>
        /// <param name="obj">the value in the related column of the item to compare to</param>
        /// <returns></returns>
        public virtual int CompareTo(object obj)
        {
            return string.Compare(this.GroupValue.ToString(), obj.ToString());
        }

        #endregion
    }

}
