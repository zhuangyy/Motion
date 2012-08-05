using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGrid.Selection
{
    public class RowSelection : SelectionBase
    {
        public RowSelection()
        {
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

        private List<int> mList = new List<int>();

        public override bool IsSelectedColumn(int column)
        {
            return false;
        }

        public override void SelectColumn(int column, bool select)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool IsSelectedRow(int row)
        {
            return mList.Contains(row);
        }

        public override void SelectRow(int row, bool select)
        {
            if (select && mList.Contains(row) == false)
            {
                mList.Add(row);

                OnSelectionChanged(EventArgs.Empty);
            }
            else if (!select && mList.Contains(row))
            {
                mList.Remove(row);

                OnSelectionChanged(EventArgs.Empty);
            }
        }

        public override bool IsSelectedCell(Position position)
        {
            return IsSelectedRow(position.Row);
        }

        public override void SelectCell(Position position, bool select)
        {
            SelectRow(position.Row, select);
        }

        public override bool IsSelectedRange(Range range)
        {
            for (int r = range.Start.Row; r <= range.End.Row; r++)
            {
                if (IsSelectedRow(r) == false)
                    return false;
            }

            return true;
        }

        public override void SelectRange(Range range, bool select)
        {
            for (int r = range.Start.Row; r <= range.End.Row; r++)
            {
                SelectRow(r, select);
            }
        }

        protected override void OnResetSelection()
        {
            mList.Clear();

            OnSelectionChanged(EventArgs.Empty);
        }

        public override bool IsEmpty()
        {
            return mList.Count == 0;
        }

        public override RangeRegion GetSelectionRegion()
        {
            RangeRegion region = new RangeRegion();

            if (Grid.Columns.Count > 0)
            {
                foreach (int row in mList)
                {
                    region.Add(ValidateRange(new Range(row, 0, row, Grid.Columns.Count - 1)));
                }
            }

            return region;
        }

        public override bool IntersectsWith(Range rng)
        {
            for (int r = rng.Start.Row; r <= rng.End.Row; r++)
            {
                if (IsSelectedRow(r))
                    return true;
            }

            return false;
        }
    }
}
