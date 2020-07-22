using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.Interface
{
    /// <summary>
    /// ISortGridGroup specifies the interface of any implementation of a BaseGridGroup class
    /// Each implementation of the ISortGridGroup can override the behavior of the grouping mechanism
    /// Notice also that ICloneable must be implemented. The OutlookGrid makes use of the Clone method of the Group
    /// to create new Group clones. Related to this is the groupGrid.GroupTemplate property, which determines what
    /// type of Group must be cloned.
    /// </summary>
    public interface ISortDataGridGroup : IComparable, ICloneable
    {
        /// <summary>
        /// the text to be displayed in the group row
        /// </summary>
        string GroupName { get; set; }

        /// <summary>
        /// determines the value of the current group. this is used to compare the group value
        /// against each item's value.
        /// </summary>
        object GroupValue { get; set; }

        /// <summary>
        /// indicates whether the group is collapsed. If it is collapsed, it group items (rows) will
        /// not be displayed.
        /// </summary>
        bool IsCollapsed { get; set; }

        /// <summary>
        /// specifies which column is associated with this group
        /// </summary>
        DataGridViewColumn GroupColumn { get; set; }

        /// <summary>
        /// specifies the number of items that are part of the current group
        /// this value is automatically filled each time the grid is re-drawn
        /// e.g. after sorting the grid.
        /// </summary>
        int ItemCount { get; set; }

        /// <summary>
        /// specifies the default height of the group
        /// each group is cloned from the GroupStyle object. Setting the height of this object
        /// will also set the default height of each group.
        /// </summary>
        int GroupHeight { get; set; }
    }

}
