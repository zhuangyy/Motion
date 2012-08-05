using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGrid.Selection
{
    /// <summary>
    /// A selection class that support free selection of cells (ranges)
    /// </summary>
    public class FreeSelection : SelectionBase
    {
        private RangeRegion mRegion = new RangeRegion();

        public FreeSelection()
        {
            mRegion.Changed += delegate { OnSelectionChanged(EventArgs.Empty); };
        }

        public override void BindToGrid(GridVirtual p_grid)
        {
            base.BindToGrid(p_grid);

            mDecorator = new Decorators.DecoratorSelection(this);
            Grid.Decorators.Add(mDecorator);
        }

        public override void UnBindToGrid()
        {
            Grid.Decorators.Remove(mDecorator);

            base.UnBindToGrid();
        }

        private Decorators.DecoratorSelection mDecorator;

        public override bool IsSelectedColumn(int column)
        {
            return mRegion.ContainsColumn(column);
        }

        public override void SelectColumn(int column, bool select)
        {
            Range rng = Grid.Columns.GetRange(column);

            SelectRange(rng, select);
        }

        public override bool IsSelectedRow(int row)
        {
            return mRegion.ContainsRow(row);
        }

        public override void SelectRow(int row, bool select)
        {
            Range rng = Grid.Rows.GetRange(row);

            SelectRange(rng, select);
        }

        public override bool IsSelectedCell(Position position)
        {
            return mRegion.Contains(position);
        }

        public override void SelectCell(Position position, bool select)
        {
            SelectRange(Grid.PositionToCellRange(position), select);
        }

        public override bool IsSelectedRange(Range range)
        {
            return mRegion.Contains(range);
        }

        public override void SelectRange(Range range, bool select)
        {
            if (select)
                mRegion.Add(ValidateRange(range));
            else
                mRegion.Remove(range);
        }

        protected override void OnResetSelection()
        {
            mRegion.Clear();
        }

        /// <summary>
        /// Returns true if the selection is empty
        /// </summary>
        /// <returns></returns>
        public override bool IsEmpty()
        {
            return mRegion.IsEmpty();
        }

        /// <summary>
        /// Returns the selected region.
        /// </summary>
        /// <returns></returns>
        public override RangeRegion GetSelectionRegion()
        {
            return new RangeRegion(mRegion);
        }

        /// <summary>
        /// Returns true if the specified selection intersect with the range
        /// </summary>
        /// <param name="rng"></param>
        /// <returns></returns>
        public override bool IntersectsWith(Range rng)
        {
            return mRegion.IntersectsWith(rng);
        }
    }
}
