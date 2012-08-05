using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGrid
{
    public partial class Grid
    {
        public class GridRow : RowInfo
        {
            public GridRow(Grid grid)
                : base(grid)
            {
            }

            private Dictionary<GridColumn, Cells.ICell> mCells = new Dictionary<GridColumn, SourceGrid.Cells.ICell>();

            public Cells.ICell this[GridColumn column]
            {
                get
                {
                    Cells.ICell cell;
                    if (mCells.TryGetValue(column, out cell))
                        return cell;
                    else
                        return null;
                }
                set
                {
                    mCells[column] = value;
                }
            }
        }

        public class GridRows : RowInfoCollection
        {
            public GridRows(Grid grid)
                : base(grid)
            {
            }

            /// <summary>
            /// Insert a row at the specified position
            /// </summary>
            /// <param name="p_Index"></param>
            public void Insert(int p_Index)
            {
                InsertRange(p_Index, 1);
            }

            /// <summary>
            /// Insert the specified number of rows at the specified position
            /// </summary>
            /// <param name="p_StartIndex"></param>
            /// <param name="p_Count"></param>
            public void InsertRange(int p_StartIndex, int p_Count)
            {
                RowInfo[] rows = new RowInfo[p_Count];
                for (int i = 0; i < rows.Length; i++)
                    rows[i] = CreateRow();

                InsertRange(p_StartIndex, rows);
            }

            protected GridRow CreateRow()
            {
                return new GridRow((Grid)Grid);
            }

            public new GridRow this[int index]
            {
                get { return (GridRow)base[index]; }
            }

            public void SetCount(int value)
            {
                if (Count < value)
                    InsertRange(Count, value - Count);
                else if (Count > value)
                    RemoveRange(value, Count - value);
            }
        }
    }
}
