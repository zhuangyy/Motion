using System;

namespace SourceGrid.Styles
{
	/// <summary>
	/// Summary description for Grid.
	/// </summary>
	public class StyleGrid
	{
		public StyleGrid()
		{

		}

		private StyleCellCollection m_StyleCells = new StyleCellCollection();
		public StyleCellCollection StyleCells
		{
			get{return m_StyleCells;}
		}

		public void ApplyStyle(Cells.ICellVirtual cell)
		{
			for (int i = 0; i < m_StyleCells.Count; i++)
			{
				m_StyleCells[i].ApplyToCell(cell);
			}
		}

		public void ApplyStyle(Grid grid, Range range)
		{
			for (int r = range.Start.Row; r <= range.End.Row; r++)
				for (int c = range.Start.Column; c <= range.End.Column; c++)
					ApplyStyle(grid.GetCell(r, c));
		}
	}
}
