using System;

namespace SourceGrid
{
	/// <summary>
	/// RangeData class represents a range of data. Can represent a range of data in string format. Usually used for drag and drop and clipboard copy/paste operations.
	/// See Controllers\Clipboard, Controllers\SelectionDrag and Controllers\SelectionDrop.
	/// </summary>
	[Serializable]
	public class RangeData
	{
		/// <summary>
		/// The string constant used with the System.Windows.Forms.DataFormats.GetFormat to register the clipboard format RangeData object.
		/// </summary>
		public const string RANGEDATA_FORMAT = "SourceGrid.RangeData";

		/// <summary>
		/// Static constructor
		/// </summary>
		static RangeData()
		{
			//Register custom DataFormats
			System.Windows.Forms.DataFormats.GetFormat(RANGEDATA_FORMAT);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public RangeData()
		{
		}

		private Position mStartDragPosition;
		private Range mSourceRange;
		private string[,] mSourceValues;
		[NonSerialized]
		private GridVirtual mSourceGrid;
		private CutMode mCutMode = CutMode.None;

		[NonSerialized]
		private System.Windows.Forms.DataObject mClipboardDataObject = null;

		/// <summary>
		/// Range source
		/// </summary>
		public Range SourceRange
		{
			get{return mSourceRange;}
		}

		/// <summary>
		/// String array for values.
		/// </summary>
		public string[,] SourceValues
		{
			get{return mSourceValues;}
		}

		/// <summary>
		/// Starting drag position. Used only for calculating drop destination range.
		/// </summary>
		public Position StartDragPosition
		{
			get{return mStartDragPosition;}
		}

		/// <summary>
		/// Working grid.
		/// </summary>
		public GridVirtual SourceGrid
		{
			get{return mSourceGrid;}
		}

		/// <summary>
		/// Cut mode. Default is none.
		/// </summary>
		public CutMode CutMode
		{
			get{return mCutMode;}
		}

		#region Loading data
		/// <summary>
		/// Load the specified range data into a string array. This method use the cell editor to get the value.
		/// </summary>
		/// <param name="sourceGrid"></param>
		/// <param name="sourceRange"></param>
		/// <param name="startDragPosition">Starting drag position. Used only for calculating drop destination range.</param>
		/// <param name="cutMode">Cut mode. Can be used to remove the data from the source when pasting it to the destination or immediately.</param>
		public void LoadData(GridVirtual sourceGrid, Range sourceRange, Position startDragPosition, CutMode cutMode)
		{
			mSourceGrid = sourceGrid;
			mCutMode = cutMode;
			mStartDragPosition = startDragPosition;
			mSourceRange = sourceRange;
			mSourceValues = new string[mSourceRange.RowsCount, mSourceRange.ColumnsCount];

			int arrayRow = 0;
			for (int r = mSourceRange.Start.Row; r <= mSourceRange.End.Row; r++, arrayRow++)
			{
				int arrayCol = 0;
				for (int c = mSourceRange.Start.Column; c <= mSourceRange.End.Column; c++, arrayCol++)
				{
					Position posCell = new Position(r, c);
					Cells.ICellVirtual cell = sourceGrid.GetCell(posCell);
					CellContext cellContext = new CellContext(sourceGrid, posCell, cell);
					if (cell != null && cell.Editor != null && cell.Editor.IsStringConversionSupported())
						mSourceValues[arrayRow, arrayCol] = cell.Editor.ValueToString( cell.Model.ValueModel.GetValue(cellContext) );
					else if (cell != null)
						mSourceValues[arrayRow, arrayCol] = cellContext.DisplayText;
				}	
			}

			//Cut Data
			if (CutMode == CutMode.CutImmediately && sourceGrid != null)
			{
                sourceGrid.ClearValues(new RangeRegion(sourceRange));
                //for (int sr = sourceRange.Start.Row; sr <= sourceRange.End.Row; sr++)
                //    for (int sc = sourceRange.Start.Column; sc <= sourceRange.End.Column; sc++)
                //    {
                //        Position pos = new Position(sr, sc);
                //        Cells.ICellVirtual cell = sourceGrid.GetCell(sr, sc);
                //        CellContext cellContext = new CellContext(sourceGrid, pos, cell);
                //        if (cell.Editor != null)
                //            cell.Editor.ClearCell(cellContext);
                //    }
			}

			mClipboardDataObject = new System.Windows.Forms.DataObject();
			mClipboardDataObject.SetData(RANGEDATA_FORMAT, this);
			mClipboardDataObject.SetData(typeof(string), DataToString(mSourceValues, mSourceRange));
		}

		/// <summary>
		/// Load the data from a Tab delimited string of data. Each column is separated by a Tab and each row by a LineFeed character.
		/// </summary>
		public void LoadData(string data)
		{
			mSourceGrid = null;
			mCutMode = CutMode.None;
			mStartDragPosition = new Position(0, 0);
			StringToData(data, out mSourceRange, out mSourceValues);

			mClipboardDataObject = new System.Windows.Forms.DataObject();
			mClipboardDataObject.SetData(RANGEDATA_FORMAT, this);
			mClipboardDataObject.SetData(typeof(string), DataToString(mSourceValues, mSourceRange));
		}
		#endregion

		#region Write data
		/// <summary>
		/// Write the current loaded array string in the specified grid range. This method use the cell editor to set the value. 
		/// </summary>
		/// <param name="destinationGrid"></param>
		/// <param name="destinationRange"></param>
		public void WriteData(GridVirtual destinationGrid, Range destinationRange)
		{
			//Calculate the destination Range merging the source range
			Range newRange = mSourceRange;
			newRange.MoveTo(destinationRange.Start);
			if (newRange.ColumnsCount > destinationRange.ColumnsCount)
				newRange.ColumnsCount = destinationRange.ColumnsCount;
			if (newRange.RowsCount > destinationRange.RowsCount)
				newRange.RowsCount = destinationRange.RowsCount;

			//Cut Data
			if (CutMode == CutMode.CutOnPaste && mSourceGrid != null)
			{
				for (int sr = mSourceRange.Start.Row; sr <= mSourceRange.End.Row; sr++)
					for (int sc = mSourceRange.Start.Column; sc <= mSourceRange.End.Column; sc++)
					{
						Position pos = new Position(sr, sc);
						Cells.ICellVirtual cell = mSourceGrid.GetCell(sr, sc);
						CellContext cellContext = new CellContext(mSourceGrid, pos, cell);
						if (cell.Editor != null)
							cell.Editor.ClearCell(cellContext);
					}
			}

			int arrayRow = 0;
			for (int r = newRange.Start.Row; r <= newRange.End.Row; r++, arrayRow++)
			{
				int arrayCol = 0;
				for (int c = newRange.Start.Column; c <= newRange.End.Column; c++, arrayCol++)
				{
					Position posCell = new Position(r, c);
					Cells.ICellVirtual cell = destinationGrid.GetCell(posCell);
					CellContext cellContext = new CellContext(destinationGrid, posCell, cell);

					if (cell != null && cell.Editor != null && mSourceValues[arrayRow, arrayCol] != null)
						cell.Editor.SetCellValue(cellContext, mSourceValues[arrayRow, arrayCol] );
				}	
			}
		}
		#endregion

		/// <summary>
		/// Convert a string buffer into a Range object and an array of string.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="range"></param>
		/// <param name="values"></param>
		protected virtual void StringToData(string data, out Range range, out string[,] values)
		{
			//tolgo uno dei due caratteri di a capo per usare lo split
			data = data.Replace("\x0D\x0A","\x0A");
			string[] rowsData = data.Split('\x0A','\x0D');
			
			//Check if the last row is not null (some application put a last \n character at the end of the cells, for example excel)
			int rows = rowsData.Length;
			if (rows > 0 && 
				(rowsData[rows - 1] == null ||
				rowsData[rows - 1].Length == 0) )
				rows--;

			if (rows == 0)
			{
				range = Range.Empty;
				values = new string[0,0];
				return;
			}

			//Calculate the columns based on the first rows! Note: probably is better to calculate the maximum columns.
			string[] firstColumnsData = rowsData[0].Split('\t');
			int cols = firstColumnsData.Length;

			range = new Range(0, 0, rows - 1, cols - 1);
			values = new string[rows, cols];

			int arrayRow = 0;
			for (int r = range.Start.Row; r < range.Start.Row + rows; r++, arrayRow++)
			{
				string rowData = rowsData[arrayRow];
				string[] columnsData = rowData.Split('\t');
				int arrayCol = 0;
				for (int c = range.Start.Column; c <= range.End.Column; c++, arrayCol++)
				{
					if (arrayCol < columnsData.Length)
						values[arrayRow, arrayCol] = columnsData[arrayCol];
					else
						values[arrayRow, arrayCol] = "";
				}
			}
		}

		/// <summary>
		/// Convert a range and an array of string into a string. Normally using a tab delimited for columns and a LineFeed for rows.
		/// </summary>
		/// <param name="values"></param>
		/// <param name="range"></param>
		/// <returns></returns>
		protected virtual string DataToString(string[,] values, Range range)
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			int arrayRow = 0;
			for (int r = range.Start.Row; r <= range.End.Row; r++, arrayRow++)
			{
				int arrayCol = 0;
				for (int c = range.Start.Column; c <= range.End.Column; c++, arrayCol++)
				{
					builder.Append(values[arrayRow, arrayCol]);
					if (c != range.End.Column)
						builder.Append('\t');
				}	

				if (r != range.End.Row)
					builder.Append("\x0D\x0A");
			}

			return builder.ToString();
		}

		/// <summary>
		/// Calculate the destination range for the drop or paste operations.
		/// </summary>
		/// <param name="destinationGrid"></param>
		/// <param name="dropDestination"></param>
		/// <returns></returns>
		public Range FindDestinationRange(GridVirtual destinationGrid, Position dropDestination)
		{
			if (dropDestination.IsEmpty())
				return Range.Empty;

			Position destinationStart = new Position(dropDestination.Row + (mSourceRange.Start.Row - mStartDragPosition.Row),
				dropDestination.Column + (mSourceRange.Start.Column - mStartDragPosition.Column) );

			destinationStart = Position.Max(destinationStart, new Position(0, 0));

			Range destination = mSourceRange;
			destination.MoveTo( destinationStart );

			destination = destination.Intersect(destinationGrid.CompleteRange);

			return destination;
		}


		/// <summary>
		/// Copy the specified RangeData object the the clipboard
		/// </summary>
		/// <param name="rangeData"></param>
		public static void ClipboardSetData(RangeData rangeData)
		{
			if (rangeData.mClipboardDataObject == null)
				throw new SourceGridException("No data loaded, use the LoadData method");
			System.Windows.Forms.Clipboard.SetDataObject(rangeData.mClipboardDataObject);
		}

		/// <summary>
		/// Get a RangeData object from the clipboard. Return null if the clipboard doesn't contains valid data formats.
		/// </summary>
		/// <returns></returns>
		public static RangeData ClipboardGetData()
		{
			System.Windows.Forms.IDataObject dtObj = System.Windows.Forms.Clipboard.GetDataObject();
			RangeData rngData = null;
			if (dtObj.GetDataPresent(RANGEDATA_FORMAT))
				rngData = (RangeData)dtObj.GetData(RANGEDATA_FORMAT);
			else
			{
				if (dtObj.GetDataPresent(System.Windows.Forms.DataFormats.Text, true))
				{
					string buffer = (string)dtObj.GetData(System.Windows.Forms.DataFormats.Text,true);
					rngData = new RangeData();
					rngData.LoadData(buffer);
				}
			}

			return rngData;
		}
	}
}
