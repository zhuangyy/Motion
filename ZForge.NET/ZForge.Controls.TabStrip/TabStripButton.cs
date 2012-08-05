using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace ZForge.Controls.TabStrip
{
	/// <summary>
	/// Represents a TabButton for TabStrip control
	/// </summary>
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
	public class TabStripButton : ToolStripButton
	{
		public TabStripButton() : base() { InitButton(); }
		public TabStripButton(Image image) : base(image) { InitButton(); }
		public TabStripButton(string text) : base(text) { InitButton(); }
		public TabStripButton(string text, Image image) : base(text, image) { InitButton(); }
		public TabStripButton(string Text, Image Image, EventHandler Handler) : base(Text, Image, Handler) { InitButton(); }
		public TabStripButton(string Text, Image Image, EventHandler Handler, string name) : base(Text, Image, Handler, name) { InitButton(); }

		private void InitButton()
		{
			m_SelectedFont = this.Font;
		}

		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size sz = base.GetPreferredSize(constrainingSize);
			if (this.Owner != null && this.Owner.Orientation == Orientation.Vertical)
			{
				sz.Width += 3;
				sz.Height += 10;
			}
			return sz;
		}

		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(0);
			}
		}

		[Browsable(false)]
		public new Padding Margin
		{
			get { return base.Margin; }
			set { }
		}

		[Browsable(false)]
		public new Padding Padding
		{
			get { return base.Padding; }
			set { }
		}

		private Color m_HotTextColor = Control.DefaultForeColor;

		[Category("Appearance")]
		[Description("Text color when TabButton is highlighted")]
		public Color HotTextColor
		{
			get { return m_HotTextColor; }
			set { m_HotTextColor = value; }
		}

		private Color m_SelectedTextColor = Control.DefaultForeColor;

		[Category("Appearance")]
		[Description("Text color when TabButton is selected")]
		public Color SelectedTextColor
		{
			get { return m_SelectedTextColor; }
			set { m_SelectedTextColor = value; }
		}

		private Font m_SelectedFont;

		[Category("Appearance")]
		[Description("Font when TabButton is selected")]
		public Font SelectedFont
		{
			get { return (m_SelectedFont == null) ? this.Font : m_SelectedFont; }
			set { m_SelectedFont = value; }
		}

		[Browsable(false)]
		[DefaultValue(false)]
		public new bool Checked
		{
			get { return IsSelected; }
			set { }
		}

		/// <summary>
		/// Gets or sets if this TabButton is currently selected
		/// </summary>
		[Browsable(false)]
		public bool IsSelected
		{
			get
			{
				TabStrip owner = Owner as TabStrip;
				if (owner != null)
					return (this == owner.SelectedTab);
				return false;
			}
			set
			{
				if (value == false) return;
				TabStrip owner = Owner as TabStrip;
				if (owner == null) return;
				owner.SelectedTab = this;
			}
		}

		protected override void OnOwnerChanged(EventArgs e)
		{
			if (Owner != null && !(Owner is TabStrip))
				throw new Exception("Cannot add TabStripButton to " + Owner.GetType().Name);
			base.OnOwnerChanged(e);
		}

	}
}
