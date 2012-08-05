using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;
using VisualStyles = System.Windows.Forms.VisualStyles;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Windows.Forms.Design;

namespace ZForge.Controls.TabStrip
{
	/// <summary>
	/// Represents a TabStrip control
	/// </summary>
	public class TabStrip : ToolStrip
	{
		private TabStripRenderer myRenderer = new TabStripRenderer();
		protected TabStripButton mySelTab;
		DesignerVerb insPage = null;

		public TabStrip()
			: base()
		{
			InitControl();
		}

		public TabStrip(params TabStripButton[] buttons)
			: base(buttons)
		{
			InitControl();
		}

		protected void InitControl()
		{
			base.RenderMode = ToolStripRenderMode.ManagerRenderMode;
			base.Renderer = myRenderer;
			myRenderer.RenderMode = this.RenderStyle;
			insPage = new DesignerVerb("Insert tab page", new EventHandler(OnInsertPageClicked));
		}

		public override ISite Site
		{
			get
			{
				ISite site = base.Site;
				if (site != null && site.DesignMode)
				{
					IContainer comp = site.Container;
					if (comp != null)
					{
						IDesignerHost host = comp as IDesignerHost;
						if (host != null)
						{
							IDesigner designer = host.GetDesigner(site.Component);
							if (designer != null && !designer.Verbs.Contains(insPage))
								designer.Verbs.Add(insPage);
						}
					}
				}
				return site;
			}
			set
			{
				base.Site = value;
			}
		}

		protected void OnInsertPageClicked(object sender, EventArgs e)
		{
			ISite site = base.Site;
			if (site != null && site.DesignMode)
			{
				IContainer container = site.Container;
				if (container != null)
				{
					TabStripButton btn = new TabStripButton();
					container.Add(btn);
					btn.Text = btn.Name;
				}
			}
		}

		/// <summary>
		/// Gets custom renderer for TabStrip. Set operation has no effect
		/// </summary>
		public new ToolStripRenderer Renderer
		{
			get { return myRenderer; }
			set { base.Renderer = myRenderer; }
		}

		/// <summary>
		/// Gets or sets layout style for TabStrip control
		/// </summary>
		public new ToolStripLayoutStyle LayoutStyle
		{
			get { return base.LayoutStyle; }
			set
			{
				switch (value)
				{
					case ToolStripLayoutStyle.StackWithOverflow:
					case ToolStripLayoutStyle.HorizontalStackWithOverflow:
					case ToolStripLayoutStyle.VerticalStackWithOverflow:
						base.LayoutStyle = ToolStripLayoutStyle.StackWithOverflow;
						break;
					case ToolStripLayoutStyle.Table:
						base.LayoutStyle = ToolStripLayoutStyle.Table;
						break;
					case ToolStripLayoutStyle.Flow:
						base.LayoutStyle = ToolStripLayoutStyle.Flow;
						break;
					default:
						base.LayoutStyle = ToolStripLayoutStyle.StackWithOverflow;
						break;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[Obsolete("Use RenderStyle instead")]
		[Browsable(false)]
		public new ToolStripRenderMode RenderMode
		{
			get { return base.RenderMode; }
			set { RenderStyle = value; }
		}

		/// <summary>
		/// Gets or sets render style for TabStrip, use it instead of 
		/// </summary>
		[Category("Appearance")]
		[Description("Gets or sets render style for TabStrip. You should use this property instead of RenderMode.")]
		public ToolStripRenderMode RenderStyle
		{
			get { return myRenderer.RenderMode; }
			set
			{
				myRenderer.RenderMode = value;
				this.Invalidate();
			}
		}

		protected override Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		[Browsable(false)]
		public new Padding Padding
		{
			get { return DefaultPadding; }
			set { }
		}

		/// <summary>
		/// Gets or sets if control should use system visual styles for painting items
		/// </summary>
		[Category("Appearance")]
		[Description("Specifies if TabStrip should use system visual styles for painting items")]
		public bool UseVisualStyles
		{
			get { return myRenderer.UseVS; }
			set
			{
				myRenderer.UseVS = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets if TabButtons should be drawn flipped
		/// </summary>
		[Category("Appearance")]
		[Description("Specifies if TabButtons should be drawn flipped (for right- and bottom-aligned TabStrips)")]
		public bool FlipButtons
		{
			get { return myRenderer.Mirrored; }
			set
			{
				myRenderer.Mirrored = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets currently selected tab
		/// </summary>
		public TabStripButton SelectedTab
		{
			get { return mySelTab; }
			set
			{
				if (value == null)
					return;
				if (mySelTab == value)
					return;
				if (value.Owner != this)
					throw new ArgumentException("Cannot select TabButtons that do not belong to this TabStrip");
				OnItemClicked(new ToolStripItemClickedEventArgs(value));
			}
		}

		public event EventHandler<SelectedTabChangedEventArgs> SelectedTabChanged;

		protected void OnTabSelected(TabStripButton tab)
		{
			this.Invalidate();
			if (SelectedTabChanged != null)
				SelectedTabChanged(this, new SelectedTabChangedEventArgs(tab));
		}

		protected override void OnItemAdded(ToolStripItemEventArgs e)
		{
			base.OnItemAdded(e);
			if (e.Item is TabStripButton)
				SelectedTab = (TabStripButton)e.Item;
		}

		protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
		{
			TabStripButton clickedBtn = e.ClickedItem as TabStripButton;
			if (clickedBtn != null)
			{
				this.SuspendLayout();
				mySelTab = clickedBtn;
				this.ResumeLayout();
				OnTabSelected(clickedBtn);
			}
			base.OnItemClicked(e);
		}
	}
}
