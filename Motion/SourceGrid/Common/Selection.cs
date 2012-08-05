//using System;
//using System.Drawing;
//using System.Collections;
//using System.Windows.Forms;
//using System.Runtime.Serialization;
//using System.ComponentModel;

//namespace SourceGrid
//{
//    /// <summary>
//    /// Represent the selected cells of the grid.
//    /// </summary>
//    public class Selection : RangeRegion
//    {
//        #region Member Variables
//        private GridVirtual m_Grid;
//        private HighlightedRange mRangeHighlight;
//        #endregion

//        #region Constructor
//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="p_Grid"></param>
//        public Selection(GridVirtual p_Grid)
//        {
//            m_Grid = p_Grid;
//            mRangeHighlight = new HighlightedRange(m_Grid);

//            DevAge.Drawing.BorderLine border1 = new DevAge.Drawing.BorderLine(Color.Black, 2, System.Drawing.Drawing2D.DashStyle.Solid, 0);
//            DevAge.Drawing.BorderLine border2 = new DevAge.Drawing.BorderLine(Color.Black, 2, System.Drawing.Drawing2D.DashStyle.Solid, 0);

//            mRangeHighlight.Border = new DevAge.Drawing.RectangleBorder(border2, border1, border2, border1);
//            m_SelectionColor = Color.FromArgb(75, Color.FromKnownColor(KnownColor.Highlight));
//        }

//        #endregion

//        #region Grid
//        /// <summary>
//        /// Linked grid
//        /// </summary>
//        public GridVirtual Grid
//        {
//            get{return m_Grid;}
//        }

//        #endregion

//        #region GetCells
//        /// <summary>
//        /// Returns the union of all the selected range as Position collection
//        /// </summary>
//        /// <returns></returns>
//        public virtual CellCollection GetCells()
//        {
//            CellCollection cells = new CellCollection();
//            PositionCollection l_Positions = GetCellsPositions();
//            for (int i = 0; i < l_Positions.Count; i++)
//            {
//                cells.Add(m_Grid.GetCell(l_Positions[i]));
//            }

//            return cells;
//        }

//        #endregion

//        #region Invalidate
//        /// <summary>
//        /// Invalidate all the selected cells
//        /// </summary>
//        public virtual void Invalidate()
//        {
//            RangeCollection ranges = GetRanges();
//            for (int i = 0;i < ranges.Count; i++)
//                m_Grid.InvalidateRange(ranges[i]);
//        }

//        public override void OnChanged(EventArgs e)
//        {
//            base.OnChanged (e);
//            Grid.InvalidateCells();
//        }


//        #endregion

//        #region SelectionMode
//        /// <summary>
//        /// Selection type
//        /// </summary>
//        public GridSelectionMode SelectionMode
//        {
//            get
//            {
//                if (Grid == null)
//                    return GridSelectionMode.Cell;
//                else if (Grid.Controller.FindController(typeof(Cells.Controllers.FullRowSelection)) != null)
//                    return GridSelectionMode.Row;
//                else if (Grid.Controller.FindController(typeof(Cells.Controllers.FullColumnSelection)) != null)
//                    return GridSelectionMode.Column;
//                else
//                    return GridSelectionMode.Cell;
//            }
//            set
//            {
//                if (Grid == null)
//                {
//                }
//                else
//                {
//                    Cells.Controllers.IController controller;

//                    controller = Grid.Controller.FindController(typeof(Cells.Controllers.FullRowSelection));
//                    if (controller != null)
//                        Grid.Controller.RemoveController(controller);

//                    controller = Grid.Controller.FindController(typeof(Cells.Controllers.FullColumnSelection));
//                    if (controller != null)
//                        Grid.Controller.RemoveController(controller);

//                    if (value == GridSelectionMode.Row)
//                        Grid.Controller.AddController(Cells.Controllers.FullRowSelection.Default);
//                    else if (value == GridSelectionMode.Column)
//                        Grid.Controller.AddController(Cells.Controllers.FullColumnSelection.Default);
//                }
//            }
//        }

//        private bool m_bEnableMultiSelection = true;

//        /// <summary>
//        /// True=Enable multi selection with the Ctrl key or Shift Key or with mouse.
//        /// </summary>
//        public bool EnableMultiSelection
//        {
//            get{return m_bEnableMultiSelection;}
//            set{m_bEnableMultiSelection = value;}
//        }

//        #endregion

//        #region Reset
//        /// <summary>
//        /// Reset the object to its original state. It is similar to the Clear method but doesn't call any events when removeing the saved positions, usually used when refreshing the cells with new data.
//        /// To simply clear the object use the Clear method, only use this method when you want to force a reset of the object without calling additional methods.
//        /// Only the OnChanged method is called.
//        /// </summary>
//        public virtual void Reset()
//        {
//            base.ResetRange();
//            //Reset all the variable
//            m_ActivePosition = Position.Empty;
//            RecalcBorderRange();
//            OnChanged(EventArgs.Empty);
//        }
//        #endregion

//        #region Rows/Columns
//        /// <summary>
//        /// Move the Focus to the first cell that can receive the focus of the current column otherwise put the focus to null.
//        /// </summary>
//        /// <returns></returns>
//        public bool FocusColumn(int column)
//        {
//            if (Grid.Columns.Count > column)
//            {
//                for (int r = 0; r < Grid.Rows.Count; r++)
//                {
//                    Position l_NewFocus = new Position(r, column);

//                    if (Grid.Controller.CanReceiveFocus(new CellContext(Grid, l_NewFocus), EventArgs.Empty ) )
//                        return Grid.Selection.Focus(l_NewFocus);
//                }

//                return Grid.Selection.Focus(Position.Empty);
//            }
//            else
//                return Grid.Selection.Focus(Position.Empty);;
//        }

//        /// <summary>
//        /// Check if the column is selected. If only a column of the row is selected this property returns true.
//        /// </summary>
//        public bool IsSelectedColumn(int column)
//        {
//            return Grid.Selection.ContainsColumn(column);
//        }
//        public void SelectColumn(int column, bool select)
//        {
//            if (Grid.Columns.Count > column && Grid.Rows.Count > 0)
//            {
//                if (select)
//                    Grid.Selection.Add(Grid.Columns.GetRange(column));
//                else
//                    Grid.Selection.Remove(Grid.Columns.GetRange(column));
//            }
//        }



//        /// <summary>
//        /// Move the Focus to the first cell that can receive the focus of the current row otherwise put the focus to null.
//        /// </summary>
//        /// <returns></returns>
//        public bool FocusRow(int row)
//        {
//            if (Grid.Rows.Count > row)
//            {
//                for (int c = 0; c < Grid.Columns.Count; c++)
//                {
//                    Position l_NewFocus = new Position(row, c);
//                    if (Grid.Controller.CanReceiveFocus(new CellContext(Grid, l_NewFocus), EventArgs.Empty) )
//                        return Grid.Selection.Focus(l_NewFocus);
//                }

//                return Grid.Selection.Focus(Position.Empty);
//            }
//            else
//                return Grid.Selection.Focus(Position.Empty);;
//        }

//        /// <summary>
//        /// Gets or sets if the current row is selected. If only a column of the row is selected this property returns true.
//        /// </summary>
//        public bool IsSelectedRow(int row)
//        {
//            return Grid.Selection.ContainsRow(row);
//        }
//        public void SelectRow(int row, bool select)
//        {
//            if (Grid.Columns.Count > 0 && Grid.Rows.Count > row)
//            {
//                if (select)
//                    Grid.Selection.Add(Grid.Rows.GetRange(row));
//                else
//                    Grid.Selection.Remove(Grid.Rows.GetRange(row));
//            }
//        }
//        #endregion

//        #region Draw Properties and Methods

//        private Color m_FocusBackColor = Color.Transparent;

//        /// <summary>
//        /// BackColor of the cell with the Focus. Default is Color.Transparent.
//        /// </summary>
//        public Color FocusBackColor
//        {
//            get{return m_FocusBackColor;}
//            set{m_FocusBackColor = value;Invalidate();}
//        }

//        private Color m_SelectionColor;

//        /// <summary>
//        /// Selection backcolor. Usually is a color with a transparent value so you can see the color of the cell. Default is: Color.FromArgb(75, Color.FromKnownColor(KnownColor.Highlight))
//        /// </summary>
//        public Color BackColor
//        {
//            get{return m_SelectionColor;}
//            set{m_SelectionColor = value;Invalidate();}
//        }

//        /// <summary>
//        /// Border of the selection. Default is new RectangleBorder(new Border(Color.Black, 2));
//        /// </summary>
//        public DevAge.Drawing.RectangleBorder Border
//        {
//            get{return mRangeHighlight.Border;}
//            set{mRangeHighlight.Border = value;}
//        }

//        private SelectionBorderMode m_SelectionBorderMode = SelectionBorderMode.Auto;

//        /// <summary>
//        /// Style of the selection border. Default is Auto.
//        /// </summary>
//        public SelectionBorderMode BorderMode
//        {
//            get{return m_SelectionBorderMode;}
//            set{m_SelectionBorderMode = value;RecalcBorderRange();}
//        }

//        private SelectionMaskStyle m_MaskStyle = SelectionMaskStyle.Default;

//        /// <summary>
//        /// Style of the selection mask.
//        /// </summary>
//        public SelectionMaskStyle MaskStyle
//        {
//            get{return m_MaskStyle;}
//            set{m_MaskStyle = value;Invalidate();}
//        }

//        /// <summary>
//        /// Defines the range of cells of the current selection inside the selection border. Returns Range.Empty is the range isn't valid.
//        /// </summary>
//        public Range BorderRange
//        {
//            get{return mRangeHighlight.Range;}
//        }

//        public System.Drawing.Rectangle GetDrawingRectangle()
//        {
//            return mRangeHighlight.GetDrawingRectangle();
//        }

//        /// <summary>
//        /// Draw the selection using the SelectionColor property over the selected cells. Draw a Border around the selection using Border and BorderMode properties.
//        /// </summary>
//        /// <param name="p_Panel"></param>
//        /// <param name="graphics"></param>
//        /// <param name="pRangeToRedraw">The range of cells that must be redrawed. Consider that can contains also not selected cells.</param>
//        public virtual void DrawSelectionMask(GridSubPanel p_Panel, DevAge.Drawing.GraphicsCache graphics, RangeRegion pRangeToRedraw)
//        {
//            if ( IsEmpty() )
//                return;

//            Region oldClip = graphics.Graphics.Clip;
//            SolidBrush brushFillMask = graphics.BrushsCache.GetBrush(BackColor);
//            try
//            {
//                graphics.Graphics.Clip = new Region(graphics.ClipRectangle);

//                Range rangeFocus = Range.Empty;
//                Rectangle rectFocus = Rectangle.Empty;
//                if (m_ActivePosition.IsEmpty() == false && pRangeToRedraw.Contains(m_ActivePosition))
//                {
//                    rectFocus = p_Panel.RectangleGridToPanel( Grid.PositionToRectangle(m_ActivePosition) );
//                    rangeFocus = Grid.PositionToCellRange(m_ActivePosition);
//                }
//                Cells.ICellVirtual cellFocus = Grid.GetCell(m_ActivePosition);

//                //Draw selection mask and border
//                //Draw each cell separately
//                if ( (m_MaskStyle & SelectionMaskStyle.DrawOnlyInitializedCells) == SelectionMaskStyle.DrawOnlyInitializedCells && 
//                    (MaskStyle & SelectionMaskStyle.DrawSeletionOverCells) == SelectionMaskStyle.DrawSeletionOverCells ) //Draw Over cells enabled?
//                {
//                    PositionCollection selectedCells = GetCellsPositions();
//                    for (int i = 0; i < selectedCells.Count; i++)
//                    {
//                        //if must be redrawed, is is not the cell with the focus and contains a cell
//                        if (pRangeToRedraw.Contains(selectedCells[i]) && rangeFocus.Contains( selectedCells[i] ) == false &&
//                            Grid.GetCell(selectedCells[i]) != null)
//                        {
//                            Rectangle rect = p_Panel.RectangleGridToPanel( Grid.PositionToRectangle(selectedCells[i]) );
//                            graphics.Graphics.FillRectangle(brushFillMask, rect);
//                        }
//                    }
//                }
//                //draw all the selected ranges (Default) //Draw Over cells enabled?
//                else if ( (MaskStyle & SelectionMaskStyle.DrawSeletionOverCells) == SelectionMaskStyle.DrawSeletionOverCells )
//                {
//                    RangeCollection selectedRanges = GetRanges();
//                    for (int i = 0; i < selectedRanges.Count; i++)
//                    {
//                        Range range = selectedRanges[i];
//                        if (pRangeToRedraw.IntersectsWith(range))
//                        {
//                            Rectangle rect = p_Panel.RectangleGridToPanel( Grid.RangeToRectangle(range) );
							
//                            if (range.Contains(m_ActivePosition))
//                            {
//                                Region region = new Region(rect);
//                                region.Exclude( rectFocus );
//                                graphics.Graphics.FillRegion(brushFillMask, region);
//                            }
//                            else
//                                graphics.Graphics.FillRectangle(brushFillMask, rect);
//                        }
//                    }
//                }
				
//                //Draw focus mask and focus border (only if there is a fucus cell and is not in editng mode)
//                CellContext focusCellContext = new CellContext(Grid, m_ActivePosition, cellFocus);
//                if (cellFocus != null && focusCellContext.IsEditing() == false &&
//                    pRangeToRedraw.Contains(m_ActivePosition))
//                {
//                    //Draw Over cells enabled?
//                    if ( (MaskStyle & SelectionMaskStyle.DrawSeletionOverCells) == SelectionMaskStyle.DrawSeletionOverCells )
//                    {
//                        if (m_FocusBackColor != Color.Transparent)
//                        {
//                            Brush focusBrush = graphics.BrushsCache.GetBrush(m_FocusBackColor);
//                            graphics.Graphics.FillRectangle(focusBrush, rectFocus);
//                        }
//                    }
//                }

//                if (focusCellContext.IsEditing() == false)
//                {
//                    mRangeHighlight.DrawHighlight(p_Panel, graphics, pRangeToRedraw);
//                }
//            }
//            finally
//            {
//                graphics.Graphics.Clip = oldClip;
//            }
//        }

//        private void RecalcBorderRange()
//        {
//            if ( IsEmpty() )
//                mRangeHighlight.Range = Range.Empty;
//            else
//            {
//                if (BorderMode == SelectionBorderMode.FocusCell)
//                {
//                    if (m_ActivePosition.IsEmpty() == false)
//                        mRangeHighlight.Range = Grid.PositionToCellRange(m_ActivePosition);
//                    else
//                        mRangeHighlight.Range = Range.Empty;
//                }
//                else if (BorderMode == SelectionBorderMode.FocusRange)
//                {
//                    RangeCollection selectedRanges = GetRanges();
//                    for (int i = 0; i < selectedRanges.Count; i++)
//                    {
//                        Range range = selectedRanges[i];
//                        if (range.Contains(m_ActivePosition))
//                        {
//                            mRangeHighlight.Range = range;
//                            return;
//                        }
//                    }
//                    mRangeHighlight.Range = Range.Empty;
//                }
//                else if (BorderMode == SelectionBorderMode.UniqueRange)
//                {
//                    RangeCollection selectedRanges = GetRanges();
//                    if (selectedRanges.Count == 1)
//                        mRangeHighlight.Range = selectedRanges[0];
//                    else
//                        mRangeHighlight.Range = Range.Empty;
//                }
//                else if (BorderMode == SelectionBorderMode.Auto)
//                {
//                    RangeCollection selectedRanges = GetRanges();
//                    if (selectedRanges.Count == 1)
//                        mRangeHighlight.Range = selectedRanges[0];
//                    else if (m_ActivePosition.IsEmpty() == false)
//                        mRangeHighlight.Range = Grid.PositionToCellRange(m_ActivePosition);
//                    else
//                        mRangeHighlight.Range = Range.Empty;
//                }
//                else
//                    mRangeHighlight.Range = Range.Empty;

//                //Set if the selected cells have the OwnerDrawSelectionBorder enabled.
//                if (mRangeHighlight.Range.ColumnsCount == 1 && mRangeHighlight.Range.RowsCount == 1)
//                {
//                    SourceGrid.Cells.ICellVirtual cell = Grid.GetCell(mRangeHighlight.Range.Start);
//                    if (cell != null && cell.View.OwnerDrawSelectionBorder)
//                    {
//                        mRangeHighlight.Range = Range.Empty;
//                    }
//                }
//            }
//        }

//        #endregion

//        #region Active Cell

//        private Position m_ActivePosition = Position.Empty;

//        /// <summary>
//        /// Returns the active cell position. The cell with the focus.
//        /// </summary>
//        public Position ActivePosition
//        {
//            get{return m_ActivePosition;}
//        }

//        /// <summary>
//        /// Change the focus of the grid. 
//        /// The calls order is: 
//        /// 
//        /// (the user select CellX) 
//        /// CellX.FocusEntering
//        /// Grid.CellGotFocus(CellX), 
//        /// CellX.FocusEntered, 
//        /// [OnFocusRowEntered],
//        /// [OnFocusColumnEntered]
//        /// 
//        /// (the user select CellY), 
//        /// CellY.FocusEntering 
//        /// CellX.FocusLeaving
//        /// Grid.CellLostFocus(CellX), 
//        /// [OnFocusRowLeaving],
//        /// [OnFocusColumnLeaving],
//        /// CellX.FocusLeft,
//        /// Grid.CellGotFocus(CellY), 
//        /// CellY.FocusEntered,
//        /// [OnFocusRowEntered],
//        /// [OnFocusColumnEntered]
//        /// 
//        /// Use Position.Empty to remove the focus cell.
//        /// </summary>
//        /// <param name="pCellToActivate"></param>
//        /// <returns></returns>
//        public virtual bool Focus(Position pCellToActivate)
//        {
//            //Check the control key status
//            bool bControlPress = ((Control.ModifierKeys & Keys.Control) == Keys.Control &&
//                (Grid.SpecialKeys & GridSpecialKeys.Control) == GridSpecialKeys.Control);

//            return Focus(pCellToActivate, !(bControlPress && EnableMultiSelection));
//        }

//        /// <summary>
//        /// Change the focus of the grid. 
//        /// The calls order is: 
//        /// 
//        /// (the user select CellX) 
//        /// CellX.FocusEntering
//        /// Grid.CellGotFocus(CellX), 
//        /// CellX.FocusEntered, 
//        /// [OnFocusRowEntered],
//        /// [OnFocusColumnEntered]
//        /// 
//        /// (the user select CellY), 
//        /// CellY.FocusEntering 
//        /// CellX.FocusLeaving
//        /// Grid.CellLostFocus(CellX), 
//        /// [OnFocusRowLeaving],
//        /// [OnFocusColumnLeaving],
//        /// CellX.FocusLeft,
//        /// Grid.CellGotFocus(CellY), 
//        /// CellY.FocusEntered,
//        /// [OnFocusRowEntered],
//        /// [OnFocusColumnEntered]
//        /// 
//        /// Use Position.Empty to remove the focus cell.
//        /// </summary>
//        /// <param name="pCellToActivate"></param>
//        /// <param name="pResetSelection">Reset the other selected cells.</param>
//        /// <returns></returns>
//        public virtual bool Focus(Position pCellToActivate, bool pResetSelection)
//        {
//            //If control key is pressed, enableMultiSelection is true and the cell that will receive the focus is not empty leave the cell selected otherwise deselect other cells
//            bool deselectOtherCells = false;
//            if (pCellToActivate.IsEmpty() == false && pResetSelection)
//                deselectOtherCells = true;

//            pCellToActivate = Grid.PositionToStartPosition(pCellToActivate);

//            //Questo controllo è necessario altrimenti l'evento verrebbe scatenato due volte per la stessa cella
//            if (pCellToActivate != ActivePosition)
//            {
//                //GotFocus Event Arguments
//                Cells.ICellVirtual newCellToFocus = Grid.GetCell(pCellToActivate);
//                CellContext newCellContext = new CellContext(Grid, pCellToActivate, newCellToFocus);
//                ChangeActivePositionEventArgs gotFocusEventArgs = new ChangeActivePositionEventArgs(ActivePosition, pCellToActivate);

//                //LostFocus Event Arguments
//                Cells.ICellVirtual oldCellFocus = Grid.GetCell(ActivePosition);
//                CellContext oldCellContext = new CellContext(Grid, ActivePosition, oldCellFocus);
//                ChangeActivePositionEventArgs lostFocusEventArgs = new ChangeActivePositionEventArgs(ActivePosition, pCellToActivate);

//                if (newCellToFocus != null)
//                {
//                    //Cell Focus Entering
//                    Grid.Controller.OnFocusEntering(newCellContext, gotFocusEventArgs);
//                    if (gotFocusEventArgs.Cancel)
//                        return false;

//                    //If the cell can't receive the focus stop the focus operation
//                    if (Grid.Controller.CanReceiveFocus(newCellContext, gotFocusEventArgs) == false)
//                        return false;
//                }

//                //This old code seems to be wrong because cause a focus change also when the cell is null (leave focus from the grid)
//                ////If the focus is already inside the grid 
//                //// (but maybe in another control) or the new cell is valid
//                //if (Grid.ContainsFocus || newCellToFocus != null)

//                //If the new cell is valid I check if I can move the focus inside the grid
//                if (newCellToFocus != null)
//                {
//                    //This method cause any cell editor to leave the focus if the validation is ok, otherwise returns false. 
//                    // This is useful for 2 reason:
//                    //	-To validate the editor
//                    //	-To check if I can move the focus on another cell
//                    bool canFocus = Grid.SetFocusOnCells(true);
//                    if (canFocus == false)
//                        return false;
//                }

//                //If there is a cell with the focus fire the focus leave events
//                if (oldCellFocus != null)
//                {
//                    //Cell Focus Leaving
//                    Grid.Controller.OnFocusLeaving(oldCellContext, lostFocusEventArgs);
//                    if (lostFocusEventArgs.Cancel)
//                        return false;

//                    //Cell Lost Focus
//                    OnCellLostFocus(lostFocusEventArgs);
//                    if (lostFocusEventArgs.Cancel)
//                        return false;
//                }

//                //Deselect previous selected cells
//                if (deselectOtherCells)
//                    Clear();

//                bool success;
//                if (newCellToFocus != null)
//                {
//                    //Cell Got Focus
//                    OnCellGotFocus(gotFocusEventArgs);

//                    success = (!gotFocusEventArgs.Cancel);
//                }
//                else
//                {
//                    success = true;
//                }

//                return success;
//            }
//            else
//            {

//                return true;
//            }
//        }

//        /// <summary>
//        /// Set the focus on the first available cells starting from the not fixed cells.
//        /// If there is an active selection set the focus on the first selected cells.
//        /// </summary>
//        /// <param name="pResetSelection"></param>
//        /// <returns></returns>
//        public virtual bool FocusFirstCell(bool pResetSelection)
//        {
//            Position focusPos;

//            PositionCollection selectedPos = Grid.Selection.GetCellsPositions();
//            //Check if there is a valid selection
//            if (selectedPos.Count > 0 && CanReceiveFocus(selectedPos[0]))
//                focusPos = selectedPos[0];
//            else
//                focusPos = SearchForValidFocusPosition();

//            if (focusPos.IsEmpty() == false)
//                return Focus(focusPos, pResetSelection);
//            else
//                return false;
//        }

//        private Position SearchForValidFocusPosition()
//        {
//            for (int r = Grid.FixedRows; r < Grid.Rows.Count; r++)
//            {
//                for (int c = Grid.FixedColumns; c < Grid.Columns.Count; c++)
//                {
//                    Position startPosition = new Position(r, c);
//                    if (CanReceiveFocus(startPosition))
//                        return startPosition;
//                }
//            }

//            return Position.Empty;
//        }

//        /// <summary>
//        /// Returns true if the specified position can receive the focus.
//        /// </summary>
//        /// <param name="position"></param>
//        /// <returns></returns>
//        public bool CanReceiveFocus(Position position)
//        {
//            if (Grid.CompleteRange.Contains(position))
//            {
//                Cells.ICellVirtual cell = Grid.GetCell(position);
//                if (cell != null)
//                {
//                    CellContext context = new CellContext(Grid, position, cell);
//                    if (Grid.Controller.CanReceiveFocus(context, EventArgs.Empty))
//                        return true;
//                    else
//                        return false;
//                }
//                else
//                    return false;
//            }
//            else
//                return false;
//        }


//        /// <summary>
//        /// Fired before a cell receive the focus (FocusCell is populated after this event, use e.Cell to read the cell that will receive the focus)
//        /// </summary>
//        public event ChangeActivePositionEventHandler CellGotFocus;

//        /// <summary>
//        /// Fired before a cell lost the focus
//        /// </summary>
//        public event ChangeActivePositionEventHandler CellLostFocus;

//        #region Focus Column/Row event and properties
//        /// <summary>
//        /// Fired before a row lost the focus
//        /// </summary>
//        public event RowCancelEventHandler FocusRowLeaving;
//        /// <summary>
//        /// Fired after a row receive the focus
//        /// </summary>
//        public event RowEventHandler FocusRowEntered;
//        /// <summary>
//        /// Fired before a column lost the focus
//        /// </summary>
//        public event ColumnCancelEventHandler FocusColumnLeaving;
//        /// <summary>
//        /// Fired after a column receive the focus
//        /// </summary>
//        public event ColumnEventHandler FocusColumnEntered;

//        /// <summary>
//        /// Fired before a row lost the focus
//        /// </summary>
//        /// <param name="e"></param>
//        protected virtual void OnFocusRowLeaving(RowCancelEventArgs e)
//        {
//            if (FocusRowLeaving != null)
//                FocusRowLeaving(this, e);
//        }
//        /// <summary>
//        /// Fired after a row receive the focus
//        /// </summary>
//        /// <param name="e"></param>
//        protected virtual void OnFocusRowEntered(RowEventArgs e)
//        {
//            if (FocusRowEntered != null)
//                FocusRowEntered(this, e);
//        }
//        /// <summary>
//        /// Fired before a column lost the focus
//        /// </summary>
//        /// <param name="e"></param>
//        protected virtual void OnFocusColumnLeaving(ColumnCancelEventArgs e)
//        {
//            if (FocusColumnLeaving != null)
//                FocusColumnLeaving(this, e);
//        }
//        /// <summary>
//        /// Fired after a column receive the focus
//        /// </summary>
//        /// <param name="e"></param>
//        protected virtual void OnFocusColumnEntered(ColumnEventArgs e)
//        {
//            if (FocusColumnEntered != null)
//                FocusColumnEntered(this, e);
//        }
//        #endregion

//        /// <summary>
//        /// Fired when a cell receive the focus
//        /// </summary>
//        /// <param name="e"></param>
//        protected virtual void OnCellGotFocus(ChangeActivePositionEventArgs e)
//        {
//            //Check if the position is valid (this is an useful check because there are cases when the rows are changed inside the leaving events, for example on the DataGrid extension when adding new rows), so I check if the row is still valid
//            if (Grid.CompleteRange.Contains(e.NewFocusPosition) == false)
//                e.Cancel = true;

//            if (e.Cancel)
//                return;

//            //Evento Got Focus
//            if (CellGotFocus != null)
//                CellGotFocus(this,e);
//            if (e.Cancel)
//                return;

//            //N.B. E' importante impostare prima la variabile m_FocusCell e dopo chiamare l'evento OnEnter, altrimenti nel caso in cui la cella sia impostata in edit sul focus, l'eseguzione va in loop (cerca di fare l'edit ma per far questo è necessario avere il focus ...)
//            m_ActivePosition = e.NewFocusPosition; //Set the focus on the cell

//            //Select the cell
//            Add(m_ActivePosition);

//            //Invalidate the selection
//            Invalidate();

//            //Recalculate the rectangle border
//            RecalcBorderRange();

//            //Cell Focus Entered
//            Grid.Controller.OnFocusEntered(new CellContext(Grid, e.NewFocusPosition), EventArgs.Empty);

//            //Column/Row Focus Enter
//            //If the row is different from the previous row, fire a row focus entered
//            if (e.NewFocusPosition.Row != e.OldFocusPosition.Row)
//                OnFocusRowEntered(new RowEventArgs(ActivePosition.Row));
//            //If the column is different from the previous column, fire a column focus entered
//            if (e.NewFocusPosition.Column != e.OldFocusPosition.Column)
//                OnFocusColumnEntered(new ColumnEventArgs(ActivePosition.Column));
//        }

//        /// <summary>
//        /// Fired when a cell lost the focus
//        /// </summary>
//        /// <param name="e"></param>
//        protected virtual void OnCellLostFocus(ChangeActivePositionEventArgs e)
//        {
//            if (e.Cancel)
//                return;

//            //This code is not necessary because when the cell receive a focus I check 
//            // if the grid can receive the focus using the SetFocusOnCells method. 
//            // The SetFocusOnCells method cause any editor to automatically close itself.
//            // If I leave this code there are problem when the cell lost the focus because the entire grid lost the focus, 
//            // in this case the EndEdit cause the grid to receive again the focus. (this problem is expecially visible when using the grid inside a tab and you click on the second tab after set an invalid cell value inside the first tab)
//            //CellContext cellLostContext = new CellContext(Grid, e.OldFocusPosition);
//            ////Stop the Edit operation
//            //if (cellLostContext.EndEdit(false) == false)
//            //    e.Cancel = true;
//            //if (e.Cancel)
//            //    return;

//            //evento Lost Focus
//            if (CellLostFocus != null)
//                CellLostFocus(this, e);
//            if (e.Cancel)
//                return;

//            //Row/Column leaving
//            //If the new Row is different from the current focus row calls a Row Leaving event
//            int focusRow = ActivePosition.Row;
//            if (ActivePosition.IsEmpty() == false && focusRow != e.NewFocusPosition.Row)
//            {
//                RowCancelEventArgs rowArgs = new RowCancelEventArgs(focusRow);
//                OnFocusRowLeaving(rowArgs);
//                if (rowArgs.Cancel)
//                {
//                    e.Cancel = true;
//                    return;
//                }
//            }
//            //If the new Row is different from the current focus row calls a Row Leaving event
//            int focusColumn = ActivePosition.Column;
//            if (ActivePosition.IsEmpty() == false && focusColumn != e.NewFocusPosition.Column)
//            {
//                ColumnCancelEventArgs columnArgs = new ColumnCancelEventArgs(focusColumn);
//                OnFocusColumnLeaving(columnArgs);
//                if (columnArgs.Cancel)
//                {
//                    e.Cancel = true;
//                    return;
//                }
//            }

//            //Change the focus cell to Empty
//            m_ActivePosition = Position.Empty; //from now the cell doesn't have the focus

//            //Cell Focus Left
//            Grid.Controller.OnFocusLeft(new CellContext(Grid, e.OldFocusPosition), EventArgs.Empty);
//        }

//        private FocusStyle m_FocusStyle = FocusStyle.Default;

//        /// <summary>
//        /// Specify the behavior of the focus and selection. Default is FocusStyle.None.
//        /// </summary>
//        public FocusStyle FocusStyle
//        {
//            get{return m_FocusStyle;}
//            set{m_FocusStyle = value;}
//        }
//        #endregion

//        #region Cell navigation

//        /// <summary>
//        /// Move the active cell (focus), moving the row and column as specified. Returns true if the focus can be moved.
//        /// Returns false if there aren't any cell to move.
//        /// </summary>
//        /// <param name="rowShift"></param>
//        /// <param name="colShift"></param>
//        /// <returns></returns>
//        public bool MoveActiveCell(int rowShift, int colShift)
//        {
//            return MoveActiveCell(ActivePosition, rowShift, colShift);
//        }

//        /// <summary>
//        /// Move the active cell (focus), moving the row and column as specified. Returns true if the focus can be moved.
//        /// Returns false if there aren't any cell to move.
//        /// </summary>
//        /// <param name="start"></param>
//        /// <param name="rowShift"></param>
//        /// <param name="colShift"></param>
//        /// <returns></returns>
//        public bool MoveActiveCell(Position start, int rowShift, int colShift)
//        {
//            Position newPosition = Position.Empty;

//            //If there isn't a current active cell I try to put the focus on the 0, 0 cell.
//            if (start.IsEmpty())
//            {
//                newPosition = new Position(0, 0);
//                if (CanReceiveFocus(newPosition))
//                    return Focus(newPosition);
//                else
//                {
//                    start = newPosition;
//                    newPosition = Position.Empty;
//                }
//            }

//            int currentRow = start.Row;
//            int currentCol = start.Column;

//            currentRow += rowShift;
//            currentCol += colShift;

//            while (newPosition.IsEmpty() && currentRow < Grid.Rows.Count && currentCol < Grid.Columns.Count &&
//                currentRow >= 0 && currentCol >= 0)
//            {
//                newPosition = new Position(currentRow, currentCol);

//                //verifico che la posizione di partenza non coincida con quella di focus, altrimenti significa che ci stiamo spostando sulla stessa cella perchè usa un RowSpan/ColSpan
//                if (Grid.PositionToStartPosition(newPosition) == start)
//                    newPosition = Position.Empty;
//                else
//                {
//                    if (CanReceiveFocus(newPosition) == false)
//                        newPosition = Position.Empty;
//                }

//                currentRow += rowShift;
//                currentCol += colShift;
//            }

//            if (newPosition.IsEmpty() == false)
//                return Focus(newPosition);
//            else
//                return false;
//        }

//        /// <summary>
//        /// Move the active cell (focus), moving the row and column as specified.
//        /// Try to set the focus using the first shift, if failed try to use the second shift (rowShift2, colShift2). 
//        /// If rowShift2 or colShift2 is int.MaxValue the next start position is the maximum row or column, if is int.MinValue 0 is used, otherwise the current position is shifted using the specified value.
//        /// This method is usually used for the Tab navigation using this code : MoveActiveCell(0,1,1,0);
//        /// Returns true if the focus can be moved.
//        /// Returns false if there aren't any cell to move.
//        /// </summary>
//        /// <param name="rowShit1"></param>
//        /// <param name="colShift1"></param>
//        /// <param name="rowShift2"></param>
//        /// <param name="colShift2"></param>
//        /// <returns></returns>
//        public bool MoveActiveCell(int rowShift1, int colShift1, int rowShift2, int colShift2)
//        {
//            return MoveActiveCell(ActivePosition, rowShift1, colShift1, rowShift2, colShift2);
//        }

//        /// <summary>
//        /// Move the active cell (focus), moving the row and column as specified.
//        /// Try to set the focus using the first shift, if failed try to use the second shift (rowShift2, colShift2). 
//        /// If rowShift2 or colShift2 is int.MaxValue the next start position is the maximum row or column, if is int.MinValue 0 is used, otherwise the current position is shifted using the specified value.
//        /// This method is usually used for the Tab navigation using this code : MoveActiveCell(0,1,1,0);
//        /// Returns true if the focus can be moved.
//        /// Returns false if there aren't any cell to move.
//        /// </summary>
//        /// <param name="start"></param>
//        /// <param name="rowShift1"></param>
//        /// <param name="colShift1"></param>
//        /// <param name="rowShift2"></param>
//        /// <param name="colShift2"></param>
//        /// <returns></returns>
//        public bool MoveActiveCell(Position start, int rowShift1, int colShift1, int rowShift2, int colShift2)
//        {
//            bool ret = MoveActiveCell(start, rowShift1, colShift1);

//            if (ret)
//                return true;
//            else
//            {
//                Position newPosition = Position.Empty;

//                //If there isn't a current active cell I try to put the focus on the 0, 0 cell.
//                if (start.IsEmpty())
//                {
//                    newPosition = new Position(0, 0);
//                    if (CanReceiveFocus(newPosition))
//                        return Focus(newPosition);
//                    else
//                        start = newPosition;
//                }

//                int row;
//                if (rowShift2 == int.MinValue)
//                    row = 0;
//                else if (rowShift2 == int.MaxValue)
//                    row = Grid.Rows.Count - 1;
//                else
//                    row = start.Row + rowShift2;

//                int column;
//                if (colShift2 == int.MinValue)
//                    column = 0;
//                else if (colShift2 == int.MaxValue)
//                    column = Grid.Columns.Count - 1;
//                else
//                    column = start.Column + colShift2;

//                newPosition = new Position(row, column);

//                if (newPosition == start || Grid.CompleteRange.Contains(newPosition) == false)
//                    return false;

//                if (CanReceiveFocus(newPosition))
//                    return Focus(newPosition);
//                else
//                    start = newPosition;
//                    return MoveActiveCell(start, rowShift1, colShift1, rowShift2, colShift2);
//            }
//        }

//        #endregion


//        #region ClearValues
//        private void Selection_ClearValues(object sender, EventArgs e)
//        {
//            ClearValues();
//        }
//        /// <summary>
//        /// Clear all the selected cells with a valid Model.
//        /// </summary>
//        public virtual void ClearValues()
//        {
//            try
//            {
//                if (ClearCells!=null)
//                    ClearCells(this, EventArgs.Empty);

//                if (AutoClear)
//                {
//                    PositionCollection positions = GetCellsPositions();
//                    foreach(Position pos in positions)
//                    {
//                        CellContext cellContext = new CellContext(Grid, pos);
//                        if (cellContext.Cell != null)
//                        {
//                            if (cellContext.Cell.Editor != null)
//                                cellContext.Cell.Editor.ClearCell(cellContext);
//                        }
//                    }
//                }
//            }
//            catch(Exception err)
//            {
//                MessageBox.Show(err.Message,"Clear error");
//            }
//        }
	
//        private bool m_bAutoClear = true;
//        /// <summary>
//        /// True to enable the default clear operation
//        /// </summary>
//        public bool AutoClear
//        {
//            get{return m_bAutoClear;}
//            set{m_bAutoClear = value;}
//        }


//        /// <summary>
//        /// Clear event
//        /// </summary>
//        public event EventHandler ClearCells;

//        #endregion

//        #region Selection Events
//        public override void OnAddedRange(RangeRegionEventArgs e)
//        {
//            base.OnAddedRange (e);
//            PositionCollection positions = e.RangeRegion.GetCellsPositions();
//            for (int i = 0; i < positions.Count; i++)
//            {
//                CellContext cellContext = new CellContext(Grid, positions[i]);
//                Grid.Controller.OnSelectionAdded(cellContext, e);
//            }

//            //Recalculate the rectangle border
//            RecalcBorderRange();
//        }
//        public override void OnAddingRange(RangeRegionCancelEventArgs e)
//        {
//            base.OnAddingRange (e);

//            RangeRegionChangingEventArgs regionChanging = new RangeRegionChangingEventArgs(e.RangeRegion, new SourceGrid.RangeRegion(), new SourceGrid.RangeRegion());
//            PositionCollection exploredPos = new PositionCollection();
//            do
//            {
//                regionChanging.RegionToExclude.Clear();
//                regionChanging.RegionToInclude.Clear();

//                PositionCollection positions = e.RangeRegion.GetCellsPositions();
//                for (int i = 0; i < positions.Count; i++)
//                {
//                    if (exploredPos.Contains(positions[i]) == false)
//                    {
//                        CellContext cellContext = new CellContext(Grid, positions[i]);
//                        Grid.Controller.OnSelectionAdding(cellContext, regionChanging);
//                        exploredPos.Add(positions[i]);
//                    }
//                }

//                regionChanging.CurrentRegion.Add(regionChanging.RegionToInclude);
//                regionChanging.CurrentRegion.Remove(regionChanging.RegionToExclude);

//            } while (!regionChanging.RegionToExclude.IsEmpty() || !regionChanging.RegionToInclude.IsEmpty());
//        }
//        public override void OnRemovedRange(RangeRegionEventArgs e)
//        {
//            base.OnRemovedRange (e);
//            PositionCollection positions = e.RangeRegion.GetCellsPositions();
//            for (int i = 0; i < positions.Count; i++)
//            {
//                CellContext cellContext = new CellContext(Grid, positions[i]);
//                Grid.Controller.OnSelectionRemoved(cellContext, e);
//            }

//            //Recalculate the rectangle border
//            RecalcBorderRange();
//        }
//        public override void OnRemovingRange(RangeRegionCancelEventArgs e)
//        {
//            base.OnRemovingRange (e);

//            RangeRegionChangingEventArgs regionChanging = new RangeRegionChangingEventArgs(e.RangeRegion, new SourceGrid.RangeRegion(), new SourceGrid.RangeRegion());
//            PositionCollection exploredPos = new PositionCollection();
//            do
//            {
//                regionChanging.RegionToExclude.Clear();
//                regionChanging.RegionToInclude.Clear();

//                PositionCollection positions = e.RangeRegion.GetCellsPositions();
//                for (int i = 0; i < positions.Count; i++)
//                {
//                    if (exploredPos.Contains(positions[i]) == false)
//                    {
//                        CellContext cellContext = new CellContext(Grid, positions[i]);
//                        Grid.Controller.OnSelectionRemoving(cellContext, regionChanging);
//                        exploredPos.Add(positions[i]);
//                    }
//                }

//                regionChanging.CurrentRegion.Add(regionChanging.RegionToInclude);
//                regionChanging.CurrentRegion.Remove(regionChanging.RegionToExclude);

//            } while (!regionChanging.RegionToExclude.IsEmpty() || !regionChanging.RegionToInclude.IsEmpty());
//        }
//        #endregion
//    }
//}