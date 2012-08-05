using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SourceGrid
{
	/// <summary>
	/// The mai grid control with static data.
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public partial class Grid : GridVirtual
	{
		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public Grid()
		{
			this.SuspendLayout();
			this.Name = "Grid";

            //Rows.RowsAdded += new IndexRangeEventHandler(m_Rows_RowsAdded);
            //Rows.RowsRemoved += new IndexRangeEventHandler(m_Rows_RowsRemoved);

            //Columns.ColumnsAdded += new IndexRangeEventHandler(m_Columns_ColumnsAdded);
            //Columns.ColumnsRemoved += new IndexRangeEventHandler(m_Columns_ColumnsRemoved);

            //Selection.AddingRange += new RangeRegionCancelEventHandler(Selection_AddingRange);
            //Selection.RemovingRange += new RangeRegionCancelEventHandler(Selection_RemovingRange);

			this.ResumeLayout(false);
		}

		/// <summary>
		/// Method used to create the rows object, in this class of type RowInfoCollection.
		/// </summary>
		protected override RowsBase CreateRowsObject()
		{
			return new GridRows(this);
		}

		/// <summary>
		/// Method used to create the columns object, in this class of type ColumnInfoCollection.
		/// </summary>
		protected override ColumnsBase CreateColumnsObject()
		{
			return new GridColumns(this);
		}

		#endregion

		#region Rows/Columns
		/// <summary>
		/// Gets or Sets the number of columns
		/// </summary>
		[DefaultValue(0)]
		public int ColumnsCount
		{
			get{return Columns.Count;}
			set{Columns.SetCount(value);}
		}

		/// <summary>
		/// Gets or Sets the number of rows
		/// </summary>
		[DefaultValue(0)]
		public int RowsCount
		{
			get{return Rows.Count;}
			set{Rows.SetCount(value);}
		}

        /// <summary>
        /// RowsCount informations
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new GridRows Rows
        {
            get { return (GridRows)base.Rows; }
        }

        /// <summary>
        /// Columns informations
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new GridColumns Columns
        {
            get { return (GridColumns)base.Columns; }
        }

        private CellOptimizeMode mOptimizeMode = CellOptimizeMode.ForRows;
        /// <summary>
        /// Gets or sets the optimize mode. Default is ForRows
        /// </summary>
        public CellOptimizeMode OptimizeMode
        {
            get { return mOptimizeMode; }
            set { mOptimizeMode = value; }
        }
		#endregion

		#region GetCell methods
		/// <summary>
		/// Set the specified cell int he specified position. Abstract method of the GridVirtual control
		/// </summary>
		/// <param name="p_iRow"></param>
		/// <param name="p_iCol"></param>
		/// <param name="p_Cell"></param>
		public virtual void SetCell(int p_iRow, int p_iCol, Cells.ICellVirtual p_Cell)
		{
			if (p_Cell is Cells.ICell)
				InsertCell(p_iRow, p_iCol, (Cells.ICell)p_Cell);
			else if (p_Cell == null)
				InsertCell(p_iRow, p_iCol, null);
			else
				throw new SourceGridException("Expected ICell class");
		}
		
		/// <summary>
		/// Set the specified cell int he specified position. This method calls SetCell(int p_iRow, int p_iCol, Cells.ICellVirtual p_Cell)
		/// </summary>
		/// <param name="p_Position"></param>
		/// <param name="p_Cell"></param>
		public void SetCell(Position p_Position, Cells.ICellVirtual p_Cell)
		{
			SetCell(p_Position.Row, p_Position.Column, p_Cell);
		}

		/// <summary>
		/// Return the Cell at the specified Row and Col position.
		/// </summary>
		/// <param name="p_iRow"></param>
		/// <param name="p_iCol"></param>
		/// <returns></returns>
		public override Cells.ICellVirtual GetCell(int p_iRow, int p_iCol)
		{
			return this[p_iRow, p_iCol];
		}


        ///// <summary>
        ///// Array of cells
        ///// </summary>
        //private Cells.ICell[,] m_Cells = null;
        //private int CellsRows
        //{
        //    get
        //    {
        //        if (m_Cells==null)
        //            return 0;
        //        else
        //            return m_Cells.GetLength(0);
        //    }
        //}
        //private int CellsCols
        //{
        //    get
        //    {
        //        if (m_Cells==null)
        //            return 0;
        //        else
        //            return m_Cells.GetLength(1);
        //    }
        //}

        private void DirectSetCell(Position position, Cells.ICell cell)
        {
            if (OptimizeMode == CellOptimizeMode.ForRows)
            {
                GridRow row = Rows[position.Row];

                row[Columns[position.Column]] = cell;
            }
            else if (OptimizeMode == CellOptimizeMode.ForColumns)
            {
                GridColumn col = Columns[position.Column];

                col[Rows[position.Row]] = cell;
            }
            else
                throw new SourceGridException("Invalid OptimizeMode");
        }

        private Cells.ICell DirectGetCell(Position position)
        {
            if (OptimizeMode == CellOptimizeMode.ForRows)
            {
                GridRow row = Rows[position.Row];

                return row[Columns[position.Column]];
            }
            else if (OptimizeMode == CellOptimizeMode.ForColumns)
            {
                GridColumn col = Columns[position.Column];

                return col[Rows[position.Row]];
            }
            else
                throw new SourceGridException("Invalid OptimizeMode");
        }

		/// <summary>
		/// Returns or set a cell at the specified row and col. If you get a ICell position occupied by a row/col span cell, and EnableRowColSpan is true, this method returns the cell with Row/Col span.
		/// </summary>
		public Cells.ICell this[int row, int col]
		{
			get
			{
                Cells.ICell cell = DirectGetCell(new Position(row,col));
                if (cell != null)
                    return cell;

				//this alghorithm search the spanned cell with a sqare search:
				//	4	4	4	4	4
				//	4	3	3	3	3
				//	4	3	2	2	2
				//	4	3	2	1	1
				//	4	3	2	1	X
				// the X represents the requestPos, the number represents the searchIteration loop.
				// search first on the row and then on the columns

				Position requestPos = new Position(row, col);
				int startPosRow = requestPos.Row;
				int startPosCol = requestPos.Column;
				bool endRow = false;
				bool endCol = false;
				Cells.ICell testCell;
				for (int searchIteration = 0; searchIteration < MaxSpan; searchIteration++)
				{
					if (endCol == false)
					{
						for (int r = startPosRow; r <= requestPos.Row; r++)
						{
							//if the cell contains the requested cell
							testCell = DirectGetCell(new Position(r,startPosCol));
							if (testCell != null && testCell.Range.Contains(requestPos))
								return testCell;
						}
					}

					if (endRow == false)
					{
						for (int c = startPosCol; c <= requestPos.Column; c++)
						{
							//if the cell contains the requested cell
							testCell = DirectGetCell(new Position(startPosRow,c));
							if (testCell != null && testCell.Range.Contains(requestPos))
								return testCell;
						}
					}

					if (startPosRow == 0 && startPosCol == 0)
						return null; //not found
					
					if (startPosRow == 0)
						endRow = true;
					else
						startPosRow--;

					if (startPosCol == 0)
						endCol = true;
					else
						startPosCol--;
				}

				return null;
			}
			set
			{					
				InsertCell(row,col,value);
			}
		}

		/// <summary>
		/// Remove the specified cell
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
        private void RemoveCell(int row, int col)
		{
			Cells.ICell tmp = DirectGetCell(new Position(row, col));

			if (tmp != null)
			{
				tmp.UnBindToGrid();

                DirectSetCell(new Position(row, col), null);
			}
		}

		/// <summary>
		/// Insert the specified cell (for best performance set Redraw property to false)
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <param name="p_cell"></param>
		private void InsertCell(int row, int col, Cells.ICell p_cell)
		{
			RemoveCell(row,col);

            if (p_cell != null && p_cell.Grid != null)
                throw new ArgumentException("This cell already have a linked grid", "p_cell");

            DirectSetCell(new Position(row, col), p_cell);

			if (p_cell != null)
			{
				p_cell.BindToGrid(this,new Position(row, col));
			}
		}
		#endregion

		#region AddRow/Col, RemoveRow/Col
		/// <summary>
		/// Set the number of columns and rows
		/// </summary>
		public void Redim(int p_Rows, int p_Cols)
		{
			RowsCount = p_Rows;
			ColumnsCount = p_Cols;
		}

        //private void m_Rows_RowsAdded(object sender, IndexRangeEventArgs e)
        //{
        //    //N.B. Uso m_Cells.GetLength(0) anziche' RowsCount e
        //    // m_Cells.GetLength(1) anziche' ColumnsCount per essere sicuro di lavorare sulle righe effetivamente allocate

        //    RedimCellsMatrix(CellsRows + e.Count, CellsCols);

        //    //dopo aver ridimensionato la matrice sposto le celle in modo da fare spazio alla nuove righe
        //    for (int r = CellsRows-1; r > (e.StartIndex + e.Count-1); r--)
        //    {
        //        for (int c = 0; c < CellsCols; c++)
        //        {
        //            Cells.ICell tmp = m_Cells[r - e.Count, c];
        //            RemoveCell(r - e.Count, c);
        //            InsertCell(r,c,tmp);
        //        }
        //    }
        //}

        //private void m_Rows_RowsRemoved(object sender, IndexRangeEventArgs e)
        //{
        //    //N.B. Uso m_Cells.GetLength(0) anziche' RowsCount e
        //    // m_Cells.GetLength(1) anziche' ColumnsCount per essere sicuro di lavorare sulle righe effetivamente allocate

        //    for (int r = (e.StartIndex + e.Count); r < CellsRows; r++)
        //    {
        //        for (int c = 0; c < CellsCols; c++)
        //        {
        //            Cells.ICell tmp = m_Cells[r,c];
        //            RemoveCell(r,c);
        //            InsertCell(r - e.Count, c, tmp);
        //        }
        //    }

        //    RedimCellsMatrix(CellsRows-e.Count, CellsCols);
        //}

        //private void m_Columns_ColumnsAdded(object sender, IndexRangeEventArgs e)
        //{
        //    //N.B. Uso m_Cells.GetLength(0) anziche' RowsCount e
        //    // m_Cells.GetLength(1) anziche' ColumnsCount per essere sicuro di lavorare sulle righe effetivamente allocate

        //    RedimCellsMatrix(CellsRows, CellsCols+e.Count);

        //    //dopo aver ridimensionato la matrice sposto le celle in modo da fare spazio alla nuove righe
        //    for (int c = CellsCols-1; c > (e.StartIndex + e.Count - 1); c--)
        //    {
        //        for (int r = 0; r < CellsRows; r++)
        //        {
        //            Cells.ICell tmp = m_Cells[r, c - e.Count];
        //            RemoveCell(r, c - e.Count);
        //            InsertCell(r,c,tmp);
        //        }
        //    }
        //}

        //private void m_Columns_ColumnsRemoved(object sender, IndexRangeEventArgs e)
        //{
        //    //N.B. Uso m_Cells.GetLength(0) anziche' RowsCount e
        //    // m_Cells.GetLength(1) anziche' ColumnsCount per essere sicuro di lavorare sulle righe effetivamente allocate

        //    for (int c = (e.StartIndex + e.Count); c < CellsCols; c++)
        //    {
        //        for (int r = 0; r < CellsRows; r++)
        //        {
        //            Cells.ICell tmp = m_Cells[r,c];
        //            RemoveCell(r,c);
        //            InsertCell(r, c - e.Count, tmp);
        //        }
        //    }

        //    RedimCellsMatrix(CellsRows, CellsCols-e.Count);
        //}

	
        ///// <summary>
        ///// Ridimensiona la matrice di celle e copia le eventuali vecchie celle presenti nella nuova matrice
        ///// </summary>
        ///// <param name="rows"></param>
        ///// <param name="cols"></param>
        //private void RedimCellsMatrix(int rows, int cols)
        //{
        //    if (m_Cells == null)
        //    {
        //        m_Cells = new Cells.ICell[rows,cols];
        //    }
        //    else
        //    {
        //        if (rows != m_Cells.GetLength(0) || cols != m_Cells.GetLength(1))
        //        {
        //            Cells.ICell[,] l_tmp = m_Cells;
        //            int l_minRows = Math.Min(l_tmp.GetLength(0),rows);
        //            int l_minCols = Math.Min(l_tmp.GetLength(1),cols);

        //            //cancello le celle non più utilizzate
        //            for (int i = l_minRows; i <l_tmp.GetLength(0); i++)
        //                for (int j = 0; j < l_tmp.GetLength(1); j++)
        //                    RemoveCell(i,j);
        //            for (int i = 0; i <l_minRows; i++)
        //                for (int j = l_minCols; j < l_tmp.GetLength(1); j++)
        //                    RemoveCell(i,j);

        //            m_Cells = new Cells.ICell[rows,cols];

        //            //copio le vecchie celle
        //            for (int i = 0; i <l_minRows; i++)
        //                for (int j = 0; j < l_minCols; j++)
        //                    m_Cells[i,j] = l_tmp[i,j];
        //        }
        //    }

        //    //			m_iRows = m_Cells.GetLength(0);
        //    //			m_iCols = m_Cells.GetLength(1);
        //}

		#endregion

		#region Row/Col Span
        private static int m_MaxSpan = 10;
        /// <summary>
        /// Gets the maximum rows or columns number to search when using Row/Col Span.
        /// This is a static property.
        /// This value is automatically calculated based on the current cells. Do not change this value manually.
        /// Default is 10.
        /// </summary>
        public static int MaxSpan
        {
            get { return m_MaxSpan; }
            set { m_MaxSpan = value; }
        }

		/// <summary>
		/// This method converts a Position to the real range of the cell. This is usefull when RowSpan or ColumnSpan is greater than 1.
		/// For example suppose to have at grid[0,0] a cell with ColumnSpan equal to 2. If you call this method with the position 0,0 returns 0,0-0,1 and if you call this method with 0,1 return again 0,0-0,1.
		/// </summary>
		/// <param name="pPosition"></param>
		/// <returns></returns>
		public override Range PositionToCellRange(Position pPosition)
		{
			if (pPosition.IsEmpty())
				return Range.Empty;

			Cells.ICell l_Cell = this[pPosition.Row, pPosition.Column];
			if (l_Cell == null)
				return new Range(pPosition);
			else
				return l_Cell.Range;
		}
		#endregion

		#region InvalidateCell
		/// <summary>
		/// Force a redraw of the specified cell
		/// </summary>
		/// <param name="p_Cell"></param>
		public virtual void InvalidateCell(Cells.ICell p_Cell)
		{
			if (p_Cell!=null)
				base.InvalidateRange(p_Cell.Range);
		}
		
		/// <summary>
		/// Force a cell to redraw. If Redraw is set to false this function has no effects. If ColSpan or RowSpan is greater than 0 this function invalidate the complete range with InvalidateRange
		/// </summary>
		/// <param name="p_Position"></param>
		public override void InvalidateCell(Position p_Position)
		{
			Cells.ICell cell = this[p_Position.Row, p_Position.Column];
            if (cell == null || (cell.Range.ColumnsCount == 1 && cell.Range.RowsCount == 1))
				base.InvalidateCell(p_Position);
			else
				InvalidateRange(cell.Range);
		}

		#endregion

		#region PaintCell
        protected override void PaintCell(DevAge.Drawing.GraphicsCache graphics, CellContext cellContext, RectangleF drawRectangle)
        {
            Range cellRange = PositionToCellRange(cellContext.Position);
            if (cellRange.ColumnsCount == 1 && cellRange.RowsCount == 1)
            {
                base.PaintCell(graphics, cellContext, drawRectangle);
            }
            else //Row/Col Span > 1
            {
                Rectangle spanRect = RangeToRectangle(cellRange);
                base.PaintCell(graphics, cellContext, spanRect);
            }
        }
		#endregion

		#region Sort

		private bool m_CustomSort = false;

		/// <summary>
		/// Gets or sets if when calling SortRangeRows method use a custom sort or an automatic sort. Default = false (automatic)
		/// </summary>
		[DefaultValue(false)]
		public bool CustomSort
		{
			get{return m_CustomSort;}
			set{m_CustomSort = value;}
		}

		/// <summary>
		/// Fired when calling SortRangeRows method. If the range contains all the columns this method move directly the row object otherwise move each cell.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnSortingRangeRows(SortRangeRowsEventArgs e)
		{
			base.OnSortingRangeRows(e);

			if (CustomSort)
				return;

            if (e.KeyColumn > e.Range.End.Column && e.KeyColumn < e.Range.Start.Column)
                throw new ArgumentException("Invalid range", "e.KeyColumn");

			IComparer cellComparer = e.CellComparer;
			if (cellComparer == null)
				cellComparer = new ValueCellComparer();

			//Sort all the columns (in this case I move directly the row object)
			if (e.Range.ColumnsCount == ColumnsCount)
			{
				RowInfo[] rowInfoToSort = new RowInfo[e.Range.End.Row-e.Range.Start.Row+1];
				Cells.ICell[] cellKeys = new Cells.ICell[e.Range.End.Row-e.Range.Start.Row+1];

				int zeroIndex = 0;
				for (int r = e.Range.Start.Row; r <= e.Range.End.Row;r++)
				{
                    cellKeys[zeroIndex] = this[r, e.KeyColumn];

					rowInfoToSort[zeroIndex] = Rows[r];
					zeroIndex++;
				}

				Array.Sort(cellKeys, rowInfoToSort, 0, cellKeys.Length, cellComparer);

				//Apply sort
				if (e.Ascending)
				{
					for (zeroIndex = 0; zeroIndex < rowInfoToSort.Length; zeroIndex++)
					{
						Rows.Swap( rowInfoToSort[zeroIndex].Index, e.Range.Start.Row + zeroIndex);
					}
				}
				else //desc
				{
					for (zeroIndex = rowInfoToSort.Length-1; zeroIndex >= 0; zeroIndex--)
					{
						Rows.Swap( rowInfoToSort[zeroIndex].Index, e.Range.End.Row - zeroIndex);
					}
				}
			}
			else //sort only the specified range
			{
				Cells.ICell[][] l_RangeSort = new Cells.ICell[e.Range.End.Row-e.Range.Start.Row+1][];
				Cells.ICell[] l_CellsKeys = new Cells.ICell[e.Range.End.Row-e.Range.Start.Row+1];

				int zeroRowIndex = 0;
				for (int r = e.Range.Start.Row; r <= e.Range.End.Row;r++)
				{
                    l_CellsKeys[zeroRowIndex] = this[r, e.KeyColumn];

					int zeroColIndex = 0;
					l_RangeSort[zeroRowIndex] = new Cells.ICell[e.Range.End.Column-e.Range.Start.Column+1];
					for (int c = e.Range.Start.Column; c <= e.Range.End.Column; c++)
					{
						l_RangeSort[zeroRowIndex][zeroColIndex] = this[r,c];
						zeroColIndex++;
					}
					zeroRowIndex++;
				}

				Array.Sort(l_CellsKeys, l_RangeSort, 0, l_CellsKeys.Length, cellComparer);

				//Apply sort
				zeroRowIndex = 0;
				if (e.Ascending)
				{
					for (int r = e.Range.Start.Row; r <= e.Range.End.Row;r++)
					{
						int zeroColIndex = 0;
						for (int c = e.Range.Start.Column; c <= e.Range.End.Column; c++)
						{
							RemoveCell(r,c);//rimuovo qualunque cella nella posizione corrente
							Cells.ICell tmp = l_RangeSort[zeroRowIndex][zeroColIndex];

							if (tmp!=null && tmp.Grid!=null && tmp.Range.Start.Row>=0 && tmp.Range.Start.Column>=0) //verifico che la cella sia valida
								RemoveCell(tmp.Range.Start.Row, tmp.Range.Start.Column);//la rimuovo dalla posizione precedente

							this[r,c] = tmp;
							zeroColIndex++;
						}
						zeroRowIndex++;
					}			
				}
				else //desc
				{
					for (int r = e.Range.End.Row; r >= e.Range.Start.Row;r--)
					{
						int zeroColIndex = 0;
						for (int c = e.Range.Start.Column; c <= e.Range.End.Column; c++)
						{
							RemoveCell(r,c);//rimuovo qualunque cella nella posizione corrente
							Cells.ICell tmp = l_RangeSort[zeroRowIndex][zeroColIndex];

							if (tmp!=null && tmp.Grid!=null && tmp.Range.Start.Row >= 0 && tmp.Range.Start.Column >= 0) //verifico che la cella sia valida
								RemoveCell(tmp.Range.Start.Row, tmp.Range.Start.Column);//la rimuovo dalla posizione precedente

							this[r,c] = tmp;
							zeroColIndex++;
						}
						zeroRowIndex++;
					}
				}
			}
		}

		#endregion
	}
}
