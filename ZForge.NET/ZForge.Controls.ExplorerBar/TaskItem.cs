/*
 * Copyright ?2004-2005, Mathew Hall
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 *
 *    - Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 * 
 *    - Redistributions in binary form must reproduce the above copyright notice, 
 *      this list of conditions and the following disclaimer in the documentation 
 *      and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
 * OF SUCH DAMAGE.
 */


using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Serialization;


namespace ZForge.Controls.ExplorerBar
{
	#region TaskItem
	
	/// <summary>
	/// A Label-like Control used to display text and/or an 
	/// Image in an Expando
	/// </summary>
	[ToolboxItem(true), 
	DesignerAttribute(typeof(TaskItemDesigner))]
	public class TaskItem : Button
	{
		#region Event Handlers

		/// <summary>
		/// Occurs when a value in the CustomSettings proterty changes
		/// </summary>
		public event EventHandler CustomSettingsChanged;

		#endregion
		
		
		#region Class Data

		/// <summary>
		/// System defined settings for the TaskItem
		/// </summary>
		private ExplorerBarInfo systemSettings;

		/// <summary>
		/// The Expando the TaskItem belongs to
		/// </summary>
		private Expando expando;

		/// <summary>
		/// The cached preferred width of the TaskItem
		/// </summary>
		private int preferredWidth;
		
		/// <summary>
		/// The cached preferred height of the TaskItem
		/// </summary>
		private int preferredHeight;

		/// <summary>
		/// The focus state of the TaskItem
		/// </summary>
		private FocusStates focusState;

		/// <summary>
		/// The rectangle where the TaskItems text is drawn
		/// </summary>
		private Rectangle textRect;

		/// <summary>
		/// Specifies whether the TaskItem should draw a focus rectangle 
		/// when it has focus
		/// </summary>
		private bool showFocusCues;

		/// <summary>
		/// Specifies the custom settings for the TaskItem
		/// </summary>
		private TaskItemInfo customSettings;

		/// <summary>
		/// Specifies whether the TaskItem's text should be drawn and measured 
		/// using GDI instead of GDI+
		/// </summary>
		private bool useGdiText;

		/// <summary>
		/// 
		/// </summary>
		private StringFormat stringFormat;

		/// <summary>
		/// 
		/// </summary>
		private DrawTextFlags drawTextFlags;

		#endregion	
		
		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the TaskItem class with default settings
		/// </summary>
		public TaskItem() : base()
		{
			// set control styles
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.Selectable, true);

			this.TabStop = true;

			this.BackColor = Color.Transparent;

			// get the system theme settings
			this.systemSettings = ThemeManager.GetSystemExplorerBarSettings();

			this.customSettings = new TaskItemInfo();
			this.customSettings.TaskItem = this;
			this.customSettings.SetDefaultEmptyValues();

			// preferred size
			this.preferredWidth = -1;
			this.preferredHeight = -1;

			// unfocused item
			this.focusState = FocusStates.None;

			this.Cursor = Cursors.Hand;

			this.textRect = new Rectangle();
			this.TextAlign = ContentAlignment.TopLeft;

			this.showFocusCues = false;
			this.useGdiText = false;

			this.InitStringFormat();
			this.InitDrawTextFlags();
		}

		#endregion


		#region Properties

		#region Colors

		/// <summary>
		/// Gets the color of the TaskItem's text
		/// </summary>
		[Browsable(false)]
		public Color LinkColor
		{
			get
			{
				if (this.CustomSettings.LinkColor != Color.Empty)
				{
					return this.CustomSettings.LinkColor;
				}

				return this.systemSettings.TaskItem.LinkColor;
			}
		}


		/// <summary>
		/// Gets the color of the TaskItem's text when highlighted.
		/// </summary>
		[Browsable(false)]
		public Color LinkHotColor
		{
			get
			{
				if (this.CustomSettings.HotLinkColor != Color.Empty)
				{
					return this.CustomSettings.HotLinkColor;
				}

				return this.systemSettings.TaskItem.HotLinkColor;
			}
		}


		/// <summary>
		/// Gets the current color of the TaskItem's text
		/// </summary>
		[Browsable(false)]
		public Color FocusLinkColor
		{
			get
			{
				if (this.FocusState == FocusStates.Mouse)
				{
					return this.LinkHotColor;
				}

				return this.LinkColor;
			}
		}

		#endregion

		#region Expando

		/// <summary>
		/// Gets or sets the Expando the TaskItem belongs to
		/// </summary>
		[Browsable(false),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Expando Expando
		{
			get
			{
				return this.expando;
			}

			set
			{
				this.expando = value;

				if (value != null)
				{
					this.SystemSettings = this.expando.SystemSettings;
				}
			}
		}

		#endregion

		#region FlatStyle
	
		/// <summary>
		/// Overrides Button.FlatStyle
		/// </summary>
		public new FlatStyle FlatStyle
		{
			get
			{
				throw new NotSupportedException();
			}

			set
			{
				throw new NotSupportedException();
			}
		}

		#endregion

		#region Focus

		/// <summary>
		/// Gets or sets a value indicating whether the TaskItem should
		/// display focus rectangles
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Determines whether the TaskItem should display a focus rectangle.")]
		public new bool ShowFocusCues
		{
			get
			{
				return this.showFocusCues;
			}

			set
			{
				if (this.showFocusCues != value)
				{
					this.showFocusCues = value;

					if (this.Focused)
					{
						this.Invalidate();
					}
				}
			}
		}

		#endregion

		#region Fonts

		/// <summary>
		/// Gets the decoration to be used on the text when the TaskItem is 
		/// in a highlighted state 
		/// </summary>
		[Browsable(false)]
		public FontStyle FontDecoration
		{
			get
			{
				if (this.CustomSettings.FontDecoration != FontStyle.Underline)
				{
					return this.CustomSettings.FontDecoration;
				}

				return this.systemSettings.TaskItem.FontDecoration;
			}
		}


		/// <summary>
		/// Gets or sets the font of the text displayed by the TaskItem
		/// </summary>
		public override Font Font
		{
			get
			{
				if (this.FocusState == FocusStates.Mouse)
				{
					return new Font(base.Font.Name, base.Font.SizeInPoints, this.FontDecoration);
				}
				
				return base.Font;
			}

			set
			{
				base.Font = value;
			}
		}

		#endregion

		#region Images

		/// <summary>
		/// Gets or sets the Image displayed by the TaskItem
		/// </summary>
		public new Image Image
		{
			get
			{
				return base.Image;
			}

			set
			{
				// make sure the image is 16x16
				if (value != null && (value.Width != 16 || value.Height != 16))
				{
					Bitmap bitmap = new Bitmap(value, 16, 16);

					base.Image = bitmap;
				}
				else
				{
					base.Image = value;
				}

				// invalidate the preferred size cache
				this.preferredWidth = -1;
				this.preferredHeight = -1;

				this.textRect.Width = 0;
				this.textRect.Height = 0;

				if (this.Expando != null)
				{
					this.Expando.DoLayout();
				}

				this.Invalidate();
			}
		}


		/// <summary>
		/// Gets or sets the ImageList that contains the images to 
		/// display in the TaskItem
		/// </summary>
		public new ImageList ImageList
		{
			get
			{
				return base.ImageList;
			}

			set
			{
				// make sure the images inside the ImageList are 16x16
				if (value != null && (value.ImageSize.Width != 16 || value.ImageSize.Height != 16))
				{
					// make a copy of the imagelist and resize all the images
					ImageList imageList = new ImageList();
					imageList.ColorDepth = value.ColorDepth;
					imageList.TransparentColor = value.TransparentColor;
					imageList.ImageSize = new Size(16, 16);

					foreach (Image image in value.Images)
					{
						Bitmap bitmap = new Bitmap(image, 16, 16);

						imageList.Images.Add(bitmap);
					}

					base.ImageList = imageList;
				}
				else
				{
					base.ImageList = value;
				}

				// invalidate the preferred size cache
				this.preferredWidth = -1;
				this.preferredHeight = -1;

				this.textRect.Width = 0;
				this.textRect.Height = 0;

				if (this.Expando != null)
				{
					this.Expando.DoLayout();
				}

				this.Invalidate();
			}
		}


		/// <summary>
		/// Gets or sets the index value of the image displayed on the TaskItem
		/// </summary>
		public new int ImageIndex
		{
			get
			{
				return base.ImageIndex;
			}

			set
			{
				base.ImageIndex = value;

				// invalidate the preferred size cache
				this.preferredWidth = -1;
				this.preferredHeight = -1;

				this.textRect.Width = 0;
				this.textRect.Height = 0;

				if (this.Expando != null)
				{
					this.Expando.DoLayout();
				}

				this.Invalidate();
			}
		}

		#endregion

		#region Margins

		/// <summary>
		/// Gets the amount of space between individual TaskItems 
		/// along each side of the TaskItem
		/// </summary>
		[Browsable(false)]
        public new Margin Margin
		{
			get
			{
				if (this.CustomSettings.Margin != Margin.Empty)
				{
					return this.CustomSettings.Margin;
				}

				return this.systemSettings.TaskItem.Margin;
			}
		}

		#endregion

		#region Padding

		/// <summary>
		/// Gets the amount of space around the text along each 
		/// side of the TaskItem
		/// </summary>
		[Browsable(false)]
        public new Padding Padding
		{
			get
			{
				if (this.CustomSettings.Padding != Padding.Empty)
				{
					return this.CustomSettings.Padding;
				}

				return this.systemSettings.TaskItem.Padding;
			}
		}

		#endregion

		#region Preferred Size

		/// <summary>
		/// Gets the preferred width of the TaskItem.
		/// Assumes that the text is required to fit on a single line
		/// </summary>
		[Browsable(false)]
		public int PreferredWidth
		{
			get
			{
				//
				if (this.preferredWidth != -1)
				{
					return this.preferredWidth;
				}

				//
				if (this.Text.Length == 0)
				{
					this.preferredWidth = 0;

					return 0;
				}

				using (Graphics g = this.CreateGraphics())
				{
					if (this.UseGdiText)
					{
						this.preferredWidth = this.CalcGdiPreferredWidth(g);
					}
					else
					{
						this.preferredWidth = this.CalcGdiPlusPreferredWidth(g);
					}
				}

				return this.preferredWidth;
			}
		}


		/// <summary>
		/// Calculates the preferred width of the TaskItem using GDI+
		/// </summary>
		/// <param name="g">The Graphics used to measure the TaskItem</param>
		/// <returns>The preferred width of the TaskItem</returns>
		protected int CalcGdiPlusPreferredWidth(Graphics g)
		{
			SizeF size = g.MeasureString(this.Text, this.Font, new SizeF(0, 0), this.StringFormat);

			int width = (int) Math.Ceiling(size.Width) + 18 + this.Padding.Left + this.Padding.Right;

			return width;
		}


		/// <summary>
		/// Calculates the preferred width of the TaskItem using GDI
		/// </summary>
		/// <param name="g">The Graphics used to measure the TaskItem</param>
		/// <returns>The preferred width of the TaskItem</returns>
		protected int CalcGdiPreferredWidth(Graphics g)
		{
			IntPtr hdc = g.GetHdc();

			int width = 0;

			if (hdc != IntPtr.Zero)
			{
				IntPtr hFont = this.Font.ToHfont();
				IntPtr oldFont = NativeMethods.SelectObject(hdc, hFont);

				RECT rect = new RECT();

				NativeMethods.DrawText(hdc, this.Text, this.Text.Length, ref rect, DrawTextFlags.DT_CALCRECT | this.DrawTextFlags);

				width = rect.right - rect.left + 18 + this.Padding.Left + this.Padding.Right;

				NativeMethods.SelectObject(hdc, oldFont);
				NativeMethods.DeleteObject(hFont);
			}
			else
			{
				width = this.CalcGdiPlusPreferredWidth(g);
			}

			g.ReleaseHdc(hdc);

			return width;
		}

        
		/// <summary>
		/// Gets the preferred height of the TaskItem.
		/// Assumes that the text is required to fit within the
		/// current width of the TaskItem
		/// </summary>
		[Browsable(false)]
		public int PreferredHeight
		{
			get
			{
				//
				if (this.preferredHeight != -1)
				{
					return this.preferredHeight;
				}

				//
				if (this.Text.Length == 0)
				{
					return 16;
				}

				int textHeight = 0;

				using (Graphics g = this.CreateGraphics())
				{
					if (this.UseGdiText)
					{
						textHeight = this.CalcGdiPreferredHeight(g);
					}
					else
					{
						textHeight = this.CalcGdiPlusPreferredHeight(g);
					}
				}

				//
				if (textHeight > 16)
				{
					this.preferredHeight = textHeight;
				}
				else
				{
					this.preferredHeight = 16;
				}

				return this.preferredHeight;
			}
		}


		/// <summary>
		/// Calculates the preferred height of the TaskItem using GDI+
		/// </summary>
		/// <param name="g">The Graphics used to measure the TaskItem</param>
		/// <returns>The preferred height of the TaskItem</returns>
		protected int CalcGdiPlusPreferredHeight(Graphics g)
		{
			//
			int width = this.Width - this.Padding.Right;

			if (this.Image != null)
			{
				width -= 16 + this.Padding.Left;
			}

			//
			SizeF size = g.MeasureString(this.Text, this.Font, width, this.StringFormat);

			//
			int height = (int) Math.Ceiling(size.Height);

			return height;
		}


		/// <summary>
		/// Calculates the preferred height of the TaskItem using GDI
		/// </summary>
		/// <param name="g">The Graphics used to measure the TaskItem</param>
		/// <returns>The preferred height of the TaskItem</returns>
		protected int CalcGdiPreferredHeight(Graphics g)
		{
			IntPtr hdc = g.GetHdc();

			int height = 0;

			if (hdc != IntPtr.Zero)
			{
				IntPtr hFont = this.Font.ToHfont();
				IntPtr oldFont = NativeMethods.SelectObject(hdc, hFont);

				RECT rect = new RECT();

				int width = this.Width - this.Padding.Right;

				if (this.Image != null)
				{
					width -= 16 + this.Padding.Left;
				}

				rect.right = width;

				NativeMethods.DrawText(hdc, this.Text, this.Text.Length, ref rect, DrawTextFlags.DT_CALCRECT | this.DrawTextFlags);

				height = rect.bottom - rect.top;

				NativeMethods.SelectObject(hdc, oldFont);
				NativeMethods.DeleteObject(hFont);
			}
			else
			{
				height = this.CalcGdiPlusPreferredHeight(g);
			}

			g.ReleaseHdc(hdc);

			return height;
		}


		/// <summary>
		/// This member overrides Button.DefaultSize
		/// </summary>
		[Browsable(false)]
		protected override Size DefaultSize
		{
			get
			{
				return new Size(162, 16);
			}
		}

		#endregion

		#region State

		/// <summary>
		/// Gets or sets whether the TaskItem is in a highlighted state.
		/// </summary>
		protected FocusStates FocusState
		{
			get
			{
				return this.focusState;
			}

			set
			{
				if (this.focusState != value)
				{
					this.focusState = value;

					this.Invalidate();
				}
			}
		}

		#endregion

		#region System Settings

		/// <summary>
		/// Gets or sets System settings for the TaskItem
		/// </summary>
		[Browsable(false)]
		protected internal ExplorerBarInfo SystemSettings
		{
			get
			{
				return this.systemSettings;
			}
			
			set
			{
				// make sure we have a new value
				if (this.systemSettings != value)
				{
					this.SuspendLayout();
					
					// get rid of the old settings
					this.systemSettings = null;

					// set the new settings
					this.systemSettings = value;

					this.ResumeLayout(true);
				}
			}
		}


		/// <summary>
		/// Gets the custom settings for the TaskItem
		/// </summary>
		[Category("Appearance"),
		Description(""),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		TypeConverter(typeof(TaskItemInfoConverter))]
		public TaskItemInfo CustomSettings
		{
			get
			{
				return this.customSettings;
			}
		}


		/// <summary>
		/// Resets the custom settings to their default values
		/// </summary>
		public void ResetCustomSettings()
		{
			this.CustomSettings.SetDefaultEmptyValues();

			this.FireCustomSettingsChanged(EventArgs.Empty);
		}

		#endregion

		#region Text

		/// <summary>
		/// Gets or sets the text associated with this TaskItem
		/// </summary>
		public override string Text
		{
			get
			{
				return base.Text;
			}

			set
			{
				base.Text = value;

				// reset the preferred width and height
				this.preferredHeight = -1;
				this.preferredWidth = -1;

				if (this.Expando != null)
				{
					this.Expando.DoLayout();
				}

				this.Invalidate();
			}
		}


		/// <summary>
		/// Gets or sets whether the TaskItem's text should be drawn 
		/// and measured using GDI instead of GDI+
		/// </summary>
		[Browsable(false), 
		DefaultValue(false)]
		public bool UseGdiText
		{
			get
			{
				return this.useGdiText;
			}

			set
			{
				if (this.useGdiText != value)
				{
					this.useGdiText = value;

					// reset the preferred width and height
					this.preferredHeight = -1;
					this.preferredWidth = -1;

					if (this.Expando != null)
					{
						this.Expando.DoLayout();
					}

					this.Invalidate();
				}
			}
		}


		/// <summary>
		/// Gets or sets the alignment of the text on the TaskItem
		/// </summary>
		public override ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}

			set
			{
				if (value != base.TextAlign)
				{
					this.InitStringFormat();
					this.InitDrawTextFlags();
					
					// should the text be aligned to the left/center/right
					switch (value)
					{
						case ContentAlignment.MiddleLeft:
						case ContentAlignment.TopLeft:
						case ContentAlignment.BottomLeft:	
						{
							this.stringFormat.Alignment = StringAlignment.Near;

							this.drawTextFlags &= ~DrawTextFlags.DT_CENTER;
							this.drawTextFlags &= ~DrawTextFlags.DT_RIGHT;
							this.drawTextFlags |= DrawTextFlags.DT_LEFT;

							break;
						}

						case ContentAlignment.MiddleCenter:
						case ContentAlignment.TopCenter:
						case ContentAlignment.BottomCenter:	
						{
							this.stringFormat.Alignment = StringAlignment.Center;

							this.drawTextFlags &= ~DrawTextFlags.DT_LEFT;
							this.drawTextFlags &= ~DrawTextFlags.DT_RIGHT;
							this.drawTextFlags |= DrawTextFlags.DT_CENTER;

							break;
						}

						case ContentAlignment.MiddleRight:
						case ContentAlignment.TopRight:
						case ContentAlignment.BottomRight:	
						{
							this.stringFormat.Alignment = StringAlignment.Far;

							this.drawTextFlags &= ~DrawTextFlags.DT_LEFT;
							this.drawTextFlags &= ~DrawTextFlags.DT_CENTER;
							this.drawTextFlags |= DrawTextFlags.DT_RIGHT;

							break;
						}
					}

					base.TextAlign = value;
				}
			}
		}


		/// <summary>
		/// Gets the StringFormat object used to draw the TaskItem's text
		/// </summary>
		protected StringFormat StringFormat
		{
			get
			{
				return this.stringFormat;
			}
		}


		/// <summary>
		/// Initializes the TaskItem's StringFormat object
		/// </summary>
		private void InitStringFormat()
		{
			if (this.stringFormat == null)
			{
				this.stringFormat = new StringFormat();
				this.stringFormat.LineAlignment = StringAlignment.Near;
				this.stringFormat.Alignment = StringAlignment.Near;
			}
		}


		/// <summary>
		/// Gets the DrawTextFlags object used to draw the TaskItem's text
		/// </summary>
		protected DrawTextFlags DrawTextFlags
		{
			get
			{
				return this.drawTextFlags;
			}
		}


		/// <summary>
		/// Initializes the TaskItem's DrawTextFlags object
		/// </summary>
		private void InitDrawTextFlags()
		{
			if (this.drawTextFlags == (int) 0)
			{
				this.drawTextFlags = (DrawTextFlags.DT_LEFT | DrawTextFlags.DT_TOP | DrawTextFlags.DT_WORDBREAK);
			}
		}


		/// <summary>
		/// Gets the Rectangle that the TaskItem's text is drawn in
		/// </summary>
		protected Rectangle TextRect
		{
			get
			{
				return this.textRect;
			}
		}

		#endregion

		#endregion


		#region Events

		#region Custom Settings

		/// <summary>
		/// Raises the CustomSettingsChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		internal void FireCustomSettingsChanged(EventArgs e)
		{
			if (this.Expando != null)
			{
				this.Expando.DoLayout();
			}

			this.Invalidate();

			this.OnCustomSettingsChanged(e);
		}


		/// <summary>
		/// Raises the CustomSettingsChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected virtual void OnCustomSettingsChanged(EventArgs e)
		{
			if (CustomSettingsChanged != null)
			{
				CustomSettingsChanged(this, e);
			}
		}

		#endregion

		#region Focus

		/// <summary>
		/// Raises the GotFocus event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnGotFocus(EventArgs e)
		{
			// if we get focus and our expando is collapsed, give
			// it focus instead
			if (this.Expando != null && this.Expando.Collapsed)
			{
				this.Expando.Select();
			}
			
			base.OnGotFocus(e);
		}


		/// <summary>
		/// Raises the VisibleChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnVisibleChanged(EventArgs e)
		{
			// if we become invisible and have focus, give the 
			// focus to our expando instead
			if (!this.Visible && this.Focused && this.Expando != null && this.Expando.Collapsed)
			{
				this.Expando.Select();
			}
			
			base.OnVisibleChanged(e);
		}

		#endregion

		#region Mouse

		/// <summary>
		/// Raises the MouseEnter event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);

			this.FocusState = FocusStates.Mouse;
		}


		/// <summary>
		/// Raises the MouseLeave event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			this.FocusState = FocusStates.None;
		}

		#endregion

		#region Paint

		/// <summary>
		/// Raises the PaintBackground event
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data</param>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			// don't let windows paint our background as it will be black
			// (we'll paint the background in OnPaint instead)
			//base.OnPaintBackground (pevent);
		}


		/// <summary>
		/// Raises the Paint event
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			
			//base.OnPaint(e);
			
			// do we have an image to draw
			if (this.Image != null)
			{
				if (this.Enabled)
				{
					if (this.RightToLeft == RightToLeft.Yes)
					{
						e.Graphics.DrawImage(this.Image, this.Width-16, 0, 16, 16);
					}
					else
					{
						e.Graphics.DrawImage(this.Image, 0, 0, 16, 16);
					}
				}
				else
				{
					// fix: use ControlPaint.DrawImageDisabled() to draw 
					//      the disabled image
					//      Brad Jones (brad@bradjones.com)
					//      26/08/2004
					//      v1.3

					if (this.RightToLeft == RightToLeft.Yes)
					{
						ControlPaint.DrawImageDisabled(e.Graphics, this.Image, this.Width-16, 0, this.BackColor);
					}
					else
					{
						ControlPaint.DrawImageDisabled(e.Graphics, this.Image, 0, 0, this.BackColor);
					}
				}
			}

			// do we have any text to draw
			if (this.Text.Length > 0)
			{
				if (this.textRect.Width == 0 && this.textRect.Height == 0)
				{
					this.textRect.X = 0;
					this.textRect.Y = 0;
					this.textRect.Height = this.PreferredHeight;
					
					if (this.RightToLeft == RightToLeft.Yes)
					{
						this.textRect.Width = this.Width - this.Padding.Right;

						if (this.Image != null)
						{
							this.textRect.Width -= 16;
						}
					}
					else
					{
						if (this.Image != null)
						{
							this.textRect.X = 16 + this.Padding.Left;
						}
					
						this.textRect.Width = this.Width - this.textRect.X - this.Padding.Right;
					}
				}
				
				if (this.RightToLeft == RightToLeft.Yes)
				{
					this.stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					this.drawTextFlags |= DrawTextFlags.DT_RTLREADING;
				}
				else
				{
					this.stringFormat.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
					this.drawTextFlags &= ~DrawTextFlags.DT_RTLREADING;
				}

				if (this.UseGdiText)
				{
					this.DrawGdiText(e.Graphics);
				}
				else
				{
					this.DrawText(e.Graphics);
				}
			}

			// check if windows will let us show a focus rectangle 
			// if we have focus
			if (this.Focused && base.ShowFocusCues)
			{
				if (this.ShowFocusCues)
				{
					ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="g"></param>
		protected void DrawText(Graphics g)
		{
			if (this.Enabled)
			{
				using (SolidBrush brush = new SolidBrush(this.FocusLinkColor))
				{
					g.DrawString(this.Text, this.Font, brush, this.TextRect, this.StringFormat);
				}
			}
			else
			{
				// draw disable text the same way as a Label
				ControlPaint.DrawStringDisabled(g, this.Text, this.Font, this.DisabledColor, (RectangleF) this.TextRect, this.StringFormat);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="g"></param>
		protected void DrawGdiText(Graphics g)
		{
			IntPtr hdc = g.GetHdc();

			if (hdc != IntPtr.Zero)
			{
				IntPtr hFont = this.Font.ToHfont();
				IntPtr oldFont = NativeMethods.SelectObject(hdc, hFont);

				int oldBkMode = NativeMethods.SetBkMode(hdc, 1);
				
				if (this.Enabled)
				{
					int oldColor = NativeMethods.SetTextColor(hdc, ColorTranslator.ToWin32(this.FocusLinkColor));

					RECT rect = RECT.FromRectangle(this.TextRect);
				
					NativeMethods.DrawText(hdc, this.Text, this.Text.Length, ref rect, this.DrawTextFlags);

					NativeMethods.SetTextColor(hdc, oldColor);
				}
				else
				{
					Rectangle layoutRectangle = this.TextRect;
					layoutRectangle.Offset(1, 1);

					Color color = ControlPaint.LightLight(this.DisabledColor);
			
					int oldColor = NativeMethods.SetTextColor(hdc, ColorTranslator.ToWin32(color));
					RECT rect = RECT.FromRectangle(layoutRectangle);
					NativeMethods.DrawText(hdc, this.Text, this.Text.Length, ref rect, this.DrawTextFlags);

					layoutRectangle.Offset(-1, -1);
					color = ControlPaint.Dark(this.DisabledColor);

					NativeMethods.SetTextColor(hdc, ColorTranslator.ToWin32(color));
					rect = RECT.FromRectangle(layoutRectangle);
					NativeMethods.DrawText(hdc, this.Text, this.Text.Length, ref rect, this.DrawTextFlags);

					NativeMethods.SetTextColor(hdc, oldColor);
				}
				
				NativeMethods.SetBkMode(hdc, oldBkMode);
				NativeMethods.SelectObject(hdc, oldFont);
				NativeMethods.DeleteObject(hFont);
			}
			else
			{
				this.DrawText(g);
			}

			g.ReleaseHdc(hdc);
		}


		/// <summary>
		/// Calculates the disabled color for text when the control is disabled
		/// </summary>
		internal Color DisabledColor
		{
			get
			{
				if (this.BackColor.A != 0)
				{
					return this.BackColor;
				}

				Color c = this.BackColor;

				for (Control control = this.Parent; (c.A == 0); control = control.Parent)
				{
					if (control == null)
					{
						return SystemColors.Control;
					}

					c = control.BackColor;
				}

				return c;
			}
		}

		#endregion

		#region Size

		/// <summary>
		/// Raises the SizeChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			// invalidate the preferred size cache
			this.preferredWidth = -1;
			this.preferredHeight = -1;

			this.textRect.Width = 0;
			this.textRect.Height = 0;
		}

		#endregion

		#endregion


		#region TaskItemSurrogate

		/// <summary>
		/// A class that is serialized instead of a TaskItem (as 
		/// TaskItems contain objects that cause serialization problems)
		/// </summary>
		[Serializable()]
			public class TaskItemSurrogate : ISerializable
		{
			#region Class Data

			/// <summary>
			/// See TaskItem.Name.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public string Name;

			/// <summary>
			/// See TaskItem.Size.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public Size Size;
			
			/// <summary>
			/// See TaskItem.Location.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public Point Location;
			
			/// <summary>
			/// See TaskItem.BackColor.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public string BackColor;
			
			/// <summary>
			/// See TaskItem.CustomSettings.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public TaskItemInfo.TaskItemInfoSurrogate CustomSettings;
			
			/// <summary>
			/// See TaskItem.Text.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public string Text;
			
			/// <summary>
			/// See TaskItem.ShowFocusCues.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public bool ShowFocusCues;

			/// <summary>
			/// See TaskItem.Image.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			[XmlElementAttribute("TaskItemImage", typeof(Byte[]), DataType="base64Binary")]
			public byte[] Image;
			
			/// <summary>
			/// See TaskItem.Enabled.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public bool Enabled;
			
			/// <summary>
			/// See TaskItem.Visible.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public bool Visible;
			
			/// <summary>
			/// See TaskItem.Anchor.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public AnchorStyles Anchor;
			
			/// <summary>
			/// See TaskItem.Dock.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public DockStyle Dock;
			
			/// <summary>
			/// See Font.Name.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public string FontName;
			
			/// <summary>
			/// See Font.Size.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public float FontSize;
			
			/// <summary>
			/// See Font.Style.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			public FontStyle FontDecoration;

			/// <summary>
			/// See TaskItem.UseGdiText.  This member is not intended to 
			/// be used directly from your code.
			/// </summary>
			public bool UseGdiText;

			/// <summary>
			/// See Control.Tag.  This member is not intended to be used 
			/// directly from your code.
			/// </summary>
			[XmlElementAttribute("Tag", typeof(Byte[]), DataType="base64Binary")]
			public byte[] Tag;

			/// <summary>
			/// Version number of the surrogate.  This member is not intended 
			/// to be used directly from your code.
			/// </summary>
			public int Version = 3300;

			#endregion
			

			#region Constructor
			
			/// <summary>
			/// Initializes a new instance of the TaskItemSurrogate class with default settings
			/// </summary>
			public TaskItemSurrogate()
			{
				this.Name = null;

				this.Size = Size.Empty;
				this.Location = Point.Empty;

				this.BackColor = ThemeManager.ConvertColorToString(Color.Empty);

				this.CustomSettings = null;

				this.Text = null;
				this.ShowFocusCues = false;
				this.Image = new byte[0];

				this.Enabled = true;
				this.Visible = true;

				this.Anchor = AnchorStyles.None;
				this.Dock = DockStyle.None;

				this.FontName = null;
				this.FontSize = 8.25f;
				this.FontDecoration = FontStyle.Regular;
				this.UseGdiText = false;

				this.Tag = new byte[0];
			}

			#endregion


			#region Methods

			/// <summary>
			/// Populates the TaskItemSurrogate with data that is to be 
			/// serialized from the specified TaskItem
			/// </summary>
			/// <param name="taskItem">The TaskItem that contains the data 
			/// to be serialized</param>
			public void Load(TaskItem taskItem)
			{
				this.Name = taskItem.Name;
				this.Size = taskItem.Size;
				this.Location = taskItem.Location;

				this.BackColor = ThemeManager.ConvertColorToString(taskItem.BackColor);

				this.CustomSettings = new TaskItemInfo.TaskItemInfoSurrogate();
				this.CustomSettings.Load(taskItem.CustomSettings);

				this.Text = taskItem.Text;
				this.ShowFocusCues = taskItem.ShowFocusCues;
				this.Image = ThemeManager.ConvertImageToByteArray(taskItem.Image);

				this.Enabled = taskItem.Enabled;
				this.Visible = taskItem.Visible;

				this.Anchor = taskItem.Anchor;
				this.Dock = taskItem.Dock;

				this.FontName = taskItem.Font.FontFamily.Name;
				this.FontSize = taskItem.Font.SizeInPoints;
				this.FontDecoration = taskItem.Font.Style;
				this.UseGdiText = taskItem.UseGdiText;

				this.Tag = ThemeManager.ConvertObjectToByteArray(taskItem.Tag);
			}


			/// <summary>
			/// Returns a TaskItem that contains the deserialized TaskItemSurrogate data
			/// </summary>
			/// <returns>A TaskItem that contains the deserialized TaskItemSurrogate data</returns>
			public TaskItem Save()
			{
				TaskItem taskItem = new TaskItem();

				taskItem.Name = this.Name;
				taskItem.Size = this.Size;
				taskItem.Location = this.Location;

				taskItem.BackColor = ThemeManager.ConvertStringToColor(this.BackColor);

				taskItem.customSettings = this.CustomSettings.Save();
				taskItem.customSettings.TaskItem = taskItem;

				taskItem.Text = this.Text;
				taskItem.ShowFocusCues = this.ShowFocusCues;
				taskItem.Image = ThemeManager.ConvertByteArrayToImage(this.Image);

				taskItem.Enabled = this.Enabled;
				taskItem.Visible = this.Visible;

				taskItem.Anchor = this.Anchor;
				taskItem.Dock = this.Dock;

				taskItem.Font = new Font(this.FontName, this.FontSize, this.FontDecoration);
				taskItem.UseGdiText = this.UseGdiText;

				taskItem.Tag = ThemeManager.ConvertByteArrayToObject(this.Tag);

				return taskItem;
			}


			/// <summary>
			/// Populates a SerializationInfo with the data needed to serialize the TaskItemSurrogate
			/// </summary>
			/// <param name="info">The SerializationInfo to populate with data</param>
			/// <param name="context">The destination for this serialization</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.AddValue("Version", this.Version);
				
				info.AddValue("Name", this.Name);
				info.AddValue("Size", this.Size);
				info.AddValue("Location", this.Location);

				info.AddValue("BackColor", this.BackColor);

				info.AddValue("CustomSettings", this.CustomSettings);

				info.AddValue("Text", this.Text);
				info.AddValue("ShowFocusCues", this.ShowFocusCues);
				info.AddValue("Image", this.Image);

				info.AddValue("Enabled", this.Enabled);
				info.AddValue("Visible", this.Visible);

				info.AddValue("Anchor", this.Anchor);
				info.AddValue("Dock", this.Dock);
				
				info.AddValue("FontName", this.FontName);
				info.AddValue("FontSize", this.FontSize);
				info.AddValue("FontDecoration", this.FontDecoration);
				info.AddValue("UseGdiText", this.UseGdiText);
				
				info.AddValue("Tag", this.Tag);
			}


			/// <summary>
			/// Initializes a new instance of the TaskItemSurrogate class using the information 
			/// in the SerializationInfo
			/// </summary>
			/// <param name="info">The information to populate the TaskItemSurrogate</param>
			/// <param name="context">The source from which the TaskItemSurrogate is deserialized</param>
			[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
			protected TaskItemSurrogate(SerializationInfo info, StreamingContext context) : base()
			{
				int version = info.GetInt32("Version");

				this.Name = info.GetString("Name");
				this.Size = (Size) info.GetValue("Size", typeof(Size));
				this.Location = (Point) info.GetValue("Location", typeof(Point));
				
				this.BackColor = info.GetString("BackColor");

				this.CustomSettings = (TaskItemInfo.TaskItemInfoSurrogate) info.GetValue("CustomSettings", typeof(TaskItemInfo.TaskItemInfoSurrogate));

				this.Text = info.GetString("Text");
				this.ShowFocusCues = info.GetBoolean("ShowFocusCues");
				this.Image = (byte[]) info.GetValue("Image", typeof(byte[]));

				this.Enabled = info.GetBoolean("Enabled");
				this.Visible = info.GetBoolean("Visible");
				
				this.Anchor = (AnchorStyles) info.GetValue("Anchor", typeof(AnchorStyles));
				this.Dock = (DockStyle) info.GetValue("Dock", typeof(DockStyle));

				this.FontName = info.GetString("FontName");
				this.FontSize = info.GetSingle("FontSize");
				this.FontDecoration = (FontStyle) info.GetValue("FontDecoration", typeof(FontStyle));

				if (version >= 3300)
				{
					this.UseGdiText = info.GetBoolean("UseGdiText");
				}

				this.Tag = (byte[]) info.GetValue("Tag", typeof(byte[]));
			}

			#endregion
		}

		#endregion
	}

	#endregion



	#region TaskItemDesigner

	/// <summary>
	/// A custom designer used by TaskItems to remove unwanted 
	/// properties from the Property window in the designer
	/// </summary>
	internal class TaskItemDesigner : ControlDesigner
	{
		/// <summary>
		/// Initializes a new instance of the TaskItemDesigner class
		/// </summary>
		public TaskItemDesigner()
		{
			
		}


		/// <summary>
		/// Adjusts the set of properties the component exposes through 
		/// a TypeDescriptor
		/// </summary>
		/// <param name="properties">An IDictionary containing the properties 
		/// for the class of the component</param>
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);

			properties.Remove("BackgroundImage");
			properties.Remove("Cursor");
			properties.Remove("ForeColor");
			properties.Remove("FlatStyle");
		}
	}

	#endregion
}
