using System;

namespace SourceGrid
{
	/// <summary>
	/// HighlightedRange allows to highlight a range of cells.
	/// </summary>
	public class HighlightedRange : IDisposable
	{
		public HighlightedRange(GridVirtual grid)
		{
			mGrid = grid;

            Grid.RangePaint += new RangePaintEventHandler(Grid_RangePaint);

            Region = new RangeRegion();
		}

		private GridVirtual mGrid;
		/// <summary>
		/// The Grid to highlight
		/// </summary>
		public GridVirtual Grid
		{
			get{return mGrid;}
		}

        private RangeRegion mRegion;
		/// <summary>
		/// The Range to highlight
		/// </summary>
        public RangeRegion Region
		{
            get { return mRegion; }
			set
			{
                if (value == null)
                    throw new ArgumentNullException("Region");

                if (mRegion != null)
                    mRegion.Changed -= new EventHandler(mRegion_Changed);

                mRegion = value;

                mRegion.Changed += new EventHandler(mRegion_Changed);
			}
		}

        void mRegion_Changed(object sender, EventArgs e)
        {
            Grid.Invalidate(true);
        }

        private DevAge.Drawing.RectangleBorder mBorder = DevAge.Drawing.RectangleBorder.NoBorder;
		/// <summary>
		/// The Border used to highlight the range
		/// </summary>
		public DevAge.Drawing.RectangleBorder Border
		{
			get{return mBorder;}
			set{mBorder = value;Grid.Invalidate(true);}
		}

        private System.Drawing.Color mBackColor = System.Drawing.Color.FromArgb(75, System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Highlight));

        /// <summary>
        /// Gets or set the highlight backcolor.
        /// Usually is a color with a transparent value so you can see the color of the cell. Default is: Color.FromArgb(75, Color.FromKnownColor(KnownColor.Highlight))
        /// </summary>
        public System.Drawing.Color BackColor
        {
            get { return mBackColor; }
            set { mBackColor = value; }
        }

		/// <summary>
		/// Draw the highlighted cells.
		/// </summary>
		/// <param name="graphics"></param>
        /// <param name="drawingRange">The range of cells that must be redrawed. Consider that can contains also not selected cells.</param>
        protected virtual void DrawHighlight(DevAge.Drawing.GraphicsCache graphics, Range drawingRange)
        {
            if (Region.IsEmpty() ||
                Region.IntersectsWith(drawingRange) == false)
                return;

            System.Drawing.Brush brush = graphics.BrushsCache.GetBrush(BackColor);

            foreach (Range rng in Region.GetRanges())
            {
                System.Drawing.Rectangle rectToDraw = Grid.RangeToRectangle(rng);
                if (rectToDraw == System.Drawing.Rectangle.Empty)
                    continue;

                System.Drawing.RectangleF contentRect = Border.GetContentRectangle(rectToDraw);

                graphics.Graphics.FillRectangle(brush, contentRect);

                Border.Draw(graphics, rectToDraw);
            }
        }

        void Grid_RangePaint(GridVirtual sender, RangePaintEventArgs e)
        {
            DrawHighlight(e.GraphicsCache, e.DrawingRange);
        }


        #region IDisposable Members
        public void Dispose()
        {
            Grid.RangePaint -= new RangePaintEventHandler(Grid_RangePaint);
        }
        #endregion
    }
}
