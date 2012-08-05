using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGrid.Decorators
{
    public class DecoratorSelection : DecoratorBase
    {
        public DecoratorSelection(Selection.SelectionBase selection)
        {
            mSelection = selection;
        }

        private Selection.SelectionBase mSelection;

        public override bool IntersectWith(Range range)
        {
            return mSelection.IntersectsWith(range);
        }

        public override void Draw(RangePaintEventArgs e)
        {
            RangeRegion region = mSelection.GetSelectionRegion();

            if (region.IsEmpty())
                return;

            System.Drawing.Brush brush = e.GraphicsCache.BrushsCache.GetBrush(mSelection.BackColor);

            CellContext focusContext = new CellContext(e.Grid, mSelection.ActivePosition);
            System.Drawing.Rectangle focusRect = e.Grid.PositionToRectangle(mSelection.ActivePosition);

            RangeCollection ranges = region.GetRanges();

            //Draw each selection range
            foreach (Range rng in ranges)
            {
                System.Drawing.Rectangle rectToDraw = e.Grid.RangeToRectangle(rng);
                if (rectToDraw == System.Drawing.Rectangle.Empty)
                    continue;

                System.Drawing.Region regionToDraw = new System.Drawing.Region(rectToDraw);

                if (rectToDraw.IntersectsWith(focusRect))
                    regionToDraw.Exclude(focusRect);

                e.GraphicsCache.Graphics.FillRegion(brush, regionToDraw);

                //Draw the border only if there isn't a editing cell
                // and is the range that contains the focus or there is a single range
                if (rng.Contains(mSelection.ActivePosition) || ranges.Count == 1)
                {
                    if (focusContext == null || focusContext.IsEditing() == false)
                        mSelection.Border.Draw(e.GraphicsCache, rectToDraw);
                }
            }

            //Draw Focus
            System.Drawing.Brush brushFocus = e.GraphicsCache.BrushsCache.GetBrush(mSelection.FocusBackColor);
            e.GraphicsCache.Graphics.FillRectangle(brushFocus, focusRect);
        }
    }
}
