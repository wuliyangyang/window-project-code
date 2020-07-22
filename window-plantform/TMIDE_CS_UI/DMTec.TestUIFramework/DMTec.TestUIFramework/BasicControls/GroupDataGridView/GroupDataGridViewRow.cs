using DMTec.TestUIFramework.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMTec.TestUIFramework.BasicControls
{
    /// <summary>
    /// In order to support grouping with the same look & feel as Outlook, the behavior
    /// of the DataGridViewRow is overridden by the GroupDataGridViewRow.
    /// The GroupDataGridViewRow has 2 main additional properties: the Group it belongs to and
    /// a the IsRowGroup flag that indicates whether the GroupDataGridViewRow object behaves like
    /// a regular row (with data) or should behave like a Group row.
    /// </summary>
    public class GroupDataGridViewRow : DataGridViewRow
    {
        protected bool isGroupRow;
        protected ISortDataGridGroup group;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISortDataGridGroup Group
        {
            get { return group; }
            set { group = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsGroupRow
        {
            get { return isGroupRow; }
            set { isGroupRow = value; }
        }

        public GroupDataGridViewRow()
            : this(null, false)
        {
        }

        public GroupDataGridViewRow(ISortDataGridGroup group)
            : this(group, false)
        {
        }

        public GroupDataGridViewRow(ISortDataGridGroup group, bool isGroupRow)
            : base()
        {
            this.group = group;
            this.IsGroupRow = isGroupRow;
        }

        public override DataGridViewElementStates GetState(int rowIndex)
        {
            if (!IsGroupRow && group != null && group.IsCollapsed)
            {
                return base.GetState(rowIndex) & DataGridViewElementStates.Selected;
            }

            return base.GetState(rowIndex);
        }

        /// <summary>
        /// the main difference with a Group row and a regular row is the way it is painted on the control.
        /// the Paint method is therefore overridden and specifies how the Group row is painted.
        /// Note: this method is not implemented optimally. It is merely used for demonstration purposes
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipBounds"></param>
        /// <param name="rowBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="rowState"></param>
        /// <param name="isFirstDisplayedRow"></param>
        /// <param name="isLastVisibleRow"></param>
        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow)
        {
            if (this.IsGroupRow)
            {
                GroupDataGridView grid = (GroupDataGridView)this.DataGridView;
                int rowHeadersWidth = grid.RowHeadersVisible ? grid.RowHeadersWidth : 0;

                // this can be optimized
                Brush brush = new SolidBrush(grid.DefaultCellStyle.BackColor);
                Brush brush2 = new SolidBrush(Color.FromKnownColor(KnownColor.GradientActiveCaption));

                //Brush brush = new SolidBrush(Color.SkyBlue);
                //Brush brush2 = new SolidBrush(Color.FromKnownColor(KnownColor.GradientActiveCaption));

                int gridwidth = grid.Columns.GetColumnsWidth(DataGridViewElementStates.None);
                Rectangle rowBounds2 = grid.GetRowDisplayRectangle(this.Index, true);

                // draw the background
                graphics.FillRectangle(brush, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset, rowBounds.Top, gridwidth, rowBounds.Height - 1);

                // draw text, using the current grid font
                graphics.DrawString(group.GroupName, grid.Font, Brushes.Black, rowHeadersWidth - grid.HorizontalScrollingOffset + 23, rowBounds.Bottom - 23);

                //draw bottom line
                graphics.FillRectangle(brush2, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset, rowBounds.Bottom - 2, gridwidth - 1, 2);

                // draw right vertical bar
                if (grid.CellBorderStyle == DataGridViewCellBorderStyle.SingleVertical || grid.CellBorderStyle == DataGridViewCellBorderStyle.Single)
                    graphics.FillRectangle(brush2, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + gridwidth - 1, rowBounds.Top, 1, rowBounds.Height);

                if (group.IsCollapsed)
                {
                    if (grid.ExpandIcon != null)
                        graphics.DrawImage(grid.ExpandIcon, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 4, rowBounds.Bottom - 18, 11, 11);
                }
                else
                {
                    if (grid.CollapseIcon != null)
                        graphics.DrawImage(grid.CollapseIcon, rowBounds.Left + rowHeadersWidth - grid.HorizontalScrollingOffset + 4, rowBounds.Bottom - 18, 11, 11);
                }
                brush.Dispose();
                brush2.Dispose();
            }
            base.Paint(graphics, clipBounds, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow);
        }

        protected override void PaintCells(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
        {
            if (!this.IsGroupRow)//If GroupRow,then do nothing.
                base.PaintCells(graphics, clipBounds, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, paintParts);
        }

        /// <summary>
        /// this function checks if the user hit the expand (+) or collapse (-) icon.
        /// if it was hit it will return true
        /// </summary>
        /// <param name="e">mouse click event arguments</param>
        /// <returns>returns true if the icon was hit, false otherwise</returns>
        internal bool IsIconHit(DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0) return false;

            GroupDataGridView grid = (GroupDataGridView)this.DataGridView;
            Rectangle rowBounds = grid.GetRowDisplayRectangle(this.Index, false);
            int x = e.X;

            DataGridViewColumn c = grid.Columns[e.ColumnIndex];
            if (this.IsGroupRow &&
                (c.DisplayIndex == 0) &&
                (x > rowBounds.Left + 4) &&
                (x < rowBounds.Left + 16) &&
                (e.Y > rowBounds.Height - 18) &&
                (e.Y < rowBounds.Height - 7))
                return true;

            return false;


            //System.Diagnostics.Debug.WriteLine(e.ColumnIndex);
        }
    }

}
