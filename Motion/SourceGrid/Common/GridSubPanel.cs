using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SourceGrid
{
    /// <summary>
    /// An abstract class that represents a panel of the grid.
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    public abstract class GridSubPanelBase : System.Windows.Forms.UserControl
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gridContainer"></param>
        /// <param name="containerType"></param>
        public GridSubPanelBase(GridVirtual gridContainer, GridSubPanelType containerType)
        {

            ContainerType = containerType;
            toolTip = new System.Windows.Forms.ToolTip();

            ToolTipText = "";
            m_GridContainer = gridContainer;
        }

        #endregion

        #region Grid
        private GridVirtual m_GridContainer;
        /// <summary>
        /// Grid
        /// </summary>
        public GridVirtual Grid
        {
            get { return m_GridContainer; }
        }

        /// <summary>
        /// Returns a Range that represents the complete cells of the panel
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Range CompleteRange
        {
            get
            {
                if (Grid.Rows.Count == 0 || Grid.Columns.Count == 0)
                    return Range.Empty;

                if (ContainerType == GridSubPanelType.Left)
                {
                    if (Grid.FixedColumns > 0)
                        return new Range(Grid.FixedRows, 0, Grid.Rows.Count - 1, Grid.FixedColumns - 1);
                    else
                        return Range.Empty;
                }
                else if (ContainerType == GridSubPanelType.Top)
                {
                    if (Grid.FixedRows > 0)
                        return new Range(0, Grid.FixedColumns, Grid.FixedRows - 1, Grid.Columns.Count - 1);
                    else
                        return Range.Empty;
                }
                else if (ContainerType == GridSubPanelType.TopLeft)
                {
                    if (Grid.FixedColumns > 0 && Grid.FixedRows > 0)
                        return new Range(0, 0, Grid.FixedRows - 1, Grid.FixedColumns - 1);
                    else
                        return Range.Empty;
                }
                else
                {
                    return new Range(Grid.FixedRows, Grid.FixedColumns, Grid.Rows.Count - 1, Grid.Columns.Count - 1);
                }
            }
        }
        #endregion
        
        #region InputKeys
        /// <summary>
        /// Allow the grid to handle specials keys like Arrows and Tab. See also Grid.SpecialKeys
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool IsInputKey(Keys keyData)
        {
            //Handle arrows and tab keys only if OverrideCommonCmdKey
            if (Grid.OverrideCommonCmdKey)
            {
                if ((Grid.SpecialKeys & GridSpecialKeys.Arrows) == GridSpecialKeys.Arrows)
                {
                    switch (keyData)
                    {
                        case Keys.Up:
                        case Keys.Down:
                        case Keys.Left:
                        case Keys.Right:
                        //altrimenti venivano (per qualche strana ragione) selezionate le scrollbars
                        case (Keys.Up | Keys.Shift):
                        case (Keys.Down | Keys.Shift):
                        case (Keys.Left | Keys.Shift):
                        case (Keys.Right | Keys.Shift):
                            return true;
                    }
                }

                if ((Grid.SpecialKeys & GridSpecialKeys.Tab) == GridSpecialKeys.Tab)
                {
                    switch (keyData)
                    {
                        case Keys.Tab:
                        case (Keys.Tab | Keys.Shift):
                            return true;
                    }
                }
            }

            return base.IsInputKey(keyData);
        }

        /// <summary>
        /// IsInputChar method.
        /// </summary>
        /// <param name="charCode"></param>
        /// <returns></returns>
        protected override bool IsInputChar(char charCode)
        {
            return Grid.AcceptsInputChar;
        }
        #endregion

        #region ToolTip
        private System.Windows.Forms.ToolTip toolTip;
        /// <summary>
        /// ToolTip text
        /// </summary>
        public string ToolTipText
        {
            get { return toolTip.GetToolTip(this); }
            set { toolTip.SetToolTip(this, value); }
        }

        /// <summary>
        /// Gets the tooltip control used when showing tooltip text.
        /// </summary>
        public ToolTip ToolTip
        {
            get { return toolTip; }
        }
        #endregion

        #region Point and Rectangle Conversion
        /// <summary>
        /// Convert a grid relative point to a panel relative point
        /// </summary>
        /// <param name="p_GridPoint"></param>
        /// <returns></returns>
        public Point PointGridToPanel(Point p_GridPoint)
        {
            return new Point(p_GridPoint.X - Left, p_GridPoint.Y - Top);
        }

        /// <summary>
        /// Convert a panel relative point to a grid relative point
        /// </summary>
        /// <param name="p_PanelPoint"></param>
        /// <returns></returns>
        public Point PointPanelToGrid(Point p_PanelPoint)
        {
            return new Point(p_PanelPoint.X + Left, p_PanelPoint.Y + Top);
        }

        /// <summary>
        /// Converts a panel coordinate rectangle to Grid coordinate rectangle
        /// </summary>
        /// <param name="p_PanelRectangle"></param>
        /// <returns></returns>
        public Rectangle RectanglePanelToGrid(Rectangle p_PanelRectangle)
        {
            Point l_Point = PointPanelToGrid(p_PanelRectangle.Location);

            return new Rectangle(l_Point.X, l_Point.Y, p_PanelRectangle.Size.Width, p_PanelRectangle.Size.Height);
        }

        /// <summary>
        /// Converts a grid coordinate rectangle to Panel coordinate rectangle
        /// </summary>
        /// <param name="p_GridRectangle"></param>
        /// <returns></returns>
        public Rectangle RectangleGridToPanel(Rectangle p_GridRectangle)
        {
            Point l_Point = PointGridToPanel(p_GridRectangle.Location);

            return new Rectangle(l_Point.X, l_Point.Y, p_GridRectangle.Size.Width, p_GridRectangle.Size.Height);
        }

        #endregion

        #region Layout
        /// <summary>
        /// Returns the DisplayRectangle of the current panel relative to the grid area.
        /// </summary>
        public Rectangle DisplayRectangleForGrid
        {
            get
            {
                return RectanglePanelToGrid(DisplayRectangle);
            }
        }
        #endregion

        #region ContainerType
        private GridSubPanelType ContainerType;
        public GridSubPanelType SubPanelType
        {
            get { return ContainerType; }
        }
        #endregion

        #region Events Dispatcher
        protected override void OnPaint(PaintEventArgs e)
        {
            GridSubPanelType panelType = ContainerType;
            if (panelType == GridSubPanelType.Hidden)
                return;

            base.OnPaint(e);

            using (DevAge.Drawing.GraphicsCache graphics = new DevAge.Drawing.GraphicsCache(e.Graphics, e.ClipRectangle))
            {
                if (panelType == GridSubPanelType.TopLeft)
                    m_GridContainer.OnTopLeftPanelPaint(graphics);
                else if (panelType == GridSubPanelType.Left)
                    m_GridContainer.OnLeftPanelPaint(graphics);
                else if (panelType == GridSubPanelType.Top)
                    m_GridContainer.OnTopPanelPaint(graphics);
                else //scrollable
                    m_GridContainer.OnScrollablePanelPaint(graphics);
            }

            ////Use this code to test panels
            //e.Graphics.DrawEllipse(Pens.Black, ClientRectangle);
            //e.Graphics.DrawString(ContainerType.ToString(), Font, Brushes.Black, ClientRectangle);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Point l_GridPoint = PointPanelToGrid(new Point(e.X, e.Y));
#if !MINI
            MouseEventArgs l_MouseArgs = new MouseEventArgs(e.Button, e.Clicks, l_GridPoint.X, l_GridPoint.Y, e.Delta);
#else
			MouseEventArgs l_MouseArgs = new MouseEventArgs(e.Button, 1, l_GridPoint.X, l_GridPoint.Y, 0);
#endif
            m_GridContainer.OnGridMouseDown(l_MouseArgs);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Point l_GridPoint = PointPanelToGrid(new Point(e.X, e.Y));
#if !MINI
            MouseEventArgs l_MouseArgs = new MouseEventArgs(e.Button, e.Clicks, l_GridPoint.X, l_GridPoint.Y, e.Delta);
#else
			MouseEventArgs l_MouseArgs = new MouseEventArgs(e.Button, 1, l_GridPoint.X, l_GridPoint.Y, 0);
#endif
            m_GridContainer.OnGridMouseUp(l_MouseArgs);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point l_GridPoint = PointPanelToGrid(new Point(e.X, e.Y));
#if !MINI
            MouseEventArgs l_MouseArgs = new MouseEventArgs(e.Button, e.Clicks, l_GridPoint.X, l_GridPoint.Y, e.Delta);
#else
			MouseEventArgs l_MouseArgs = new MouseEventArgs(e.Button, 1, l_GridPoint.X, l_GridPoint.Y, 0);
#endif
            m_GridContainer.OnGridMouseMove(l_MouseArgs);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            m_GridContainer.OnGridClick(e);
        }

#if !MINI
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            m_GridContainer.OnGridDoubleClick(e);
        }
#endif
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            m_GridContainer.OnGridKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            m_GridContainer.OnGridKeyUp(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            m_GridContainer.OnGridKeyPress(e);
        }
#if !MINI
        #region Drag Events
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            m_GridContainer.OnGridDragDrop(drgevent);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);

            m_GridContainer.OnGridDragEnter(drgevent);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
            m_GridContainer.OnGridDragLeave(e);
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);

            m_GridContainer.OnGridDragOver(drgevent);
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
        {
            base.OnGiveFeedback(gfbevent);

            m_GridContainer.OnGridGiveFeedback(gfbevent);
        }

        #endregion
#endif

#if !MINI
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            m_GridContainer.OnPanelMouseEnter(e);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            m_GridContainer.OnGridMouseHover(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            m_GridContainer.OnPanelMouseLeave(e);
        }
#endif

        #endregion
    }


    /// <summary>
    /// This is the only selectable control. Internally when a cell receive the focus is this panel that receive the focus
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    public class GridSubPanelHidden : GridSubPanelBase
    {
        public GridSubPanelHidden(GridVirtual gridContainer)
            : base(gridContainer, GridSubPanelType.Hidden)
        {
            SetStyle(ControlStyles.Selectable, true);

            TabStop = true;
        }


        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            //Move the focus on the first cell if there isn't an active cell and the FocusStyle FocusFirstCellOnEnter is on.
            if ((Grid.Selection.FocusStyle & FocusStyle.FocusFirstCellOnEnter) == FocusStyle.FocusFirstCellOnEnter &&
                Grid.Selection.ActivePosition.IsEmpty())
            {
                Grid.Selection.FocusFirstCell(false);
            }
        }
    }

    [System.ComponentModel.ToolboxItem(false)]
    public class GridSubPanel : GridSubPanelBase
    {
        public GridSubPanel(GridVirtual gridContainer, GridSubPanelType containerType)
            : base(gridContainer, containerType)
        {
            AllowDrop = true;

            //to remove flicker and use custom draw
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.Selectable, false);

            m_ControlsRepository = new ControlsRepository(this);

            TabStop = false; //The grid control is a special control and usually don't receive the focus with the Tab
        }

        #region ControlsRepository
        private ControlsRepository m_ControlsRepository;

        /// <summary>
        /// A collection of controls used for editing operations
        /// </summary>
        public ControlsRepository ControlsRepository
        {
            get { return m_ControlsRepository; }
        }
        #endregion
    }

    public enum GridSubPanelType
    {
        TopLeft,
        Top,
        Left,
        Scrollable,
        Hidden
    }
}