using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SourceGrid.Cells.Controllers
{
    /// <summary>
    /// A cell controller used to handle mouse selection
    /// </summary>
    public class MouseSelection : ControllerBase
    {
        public static MouseSelection Default = new MouseSelection();

        public override void OnMouseDown(CellContext sender, System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseDown(sender, e);

            if (e.Button != MouseButtons.Left)
                return;

            GridVirtual grid = sender.Grid;

            //Check the control and shift key status
            bool controlPress = ((Control.ModifierKeys & Keys.Control) == Keys.Control &&
                (grid.SpecialKeys & GridSpecialKeys.Control) == GridSpecialKeys.Control);

            bool shiftPress = ((Control.ModifierKeys & Keys.Shift) == Keys.Shift &&
                (grid.SpecialKeys & GridSpecialKeys.Shift) == GridSpecialKeys.Shift);

            if (shiftPress == false ||
                grid.Selection.EnableMultiSelection == false)
            {
                //Handle Control key
                bool mantainSelection = grid.Selection.EnableMultiSelection && controlPress;

                grid.Selection.Focus(sender.Position, !mantainSelection);
            }
            else //handle shift key
            {
                grid.Selection.ResetSelection(true);

                Range rangeToSelect = new Range(grid.Selection.ActivePosition, sender.Position);
                grid.Selection.SelectRange(rangeToSelect, true);
            }
        }

        public override void OnMouseUp(CellContext sender, MouseEventArgs e)
        {
            base.OnMouseUp(sender, e);

            sender.Grid.MouseSelectionFinish();
        }

        public override void OnMouseMove(CellContext sender, MouseEventArgs e)
        {
            base.OnMouseMove(sender, e);

            //Mouse multi selection

            //First check if the multi selection is enabled and the active position is valid
            if (sender.Grid.Selection.EnableMultiSelection == false ||
                sender.Grid.MouseDownPosition.IsEmpty() ||
                sender.Grid.MouseDownPosition != sender.Grid.Selection.ActivePosition)
                return;

            //Check if the mouse position is valid
            Position mousePosition = sender.Grid.PositionAtPoint(new Point(e.X, e.Y));
            if (mousePosition.IsEmpty())
                return;

            //If the position type is different I don't continue
            // bacause this can cause problem for example when selection the fixed rows when the scroll is on a position > 0
            // that cause all the rows to be selected
            if (sender.Grid.GetPositionType(mousePosition) != 
                sender.Grid.GetPositionType(sender.Grid.Selection.ActivePosition))
                return;

            sender.Grid.ChangeMouseSelectionCorner(mousePosition);
        }
    }
}
