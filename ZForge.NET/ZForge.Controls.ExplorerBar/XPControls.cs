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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace ZForge.Controls.ExplorerBar
{
	#region XPCheckedListBox

	/// <summary>
	/// A CheckedListBox that correctly draws themed borders if Windows XP 
	/// Visual Styles are enabled when it recieves a WM_PRINT message
	/// </summary>
	[ToolboxItem(true)]
	public class XPCheckedListBox : ListBox
	{
		/// <summary>
		/// The cached value of whether Xindows XP Visual Styles are enabled
		/// </summary>
		private bool visualStylesEnabled;

		
		/// <summary>
		/// Initializes a new instance of the XPCheckedListBox class with default settings
		/// </summary>
		public XPCheckedListBox() : base()
		{
			// check if visual styles have been enabled
			this.visualStylesEnabled = this.VisualStylesEnabled;
		}


		/// <summary>
		/// Raises the SystemColorsChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);

			// recheck if visual styles have been enabled
			this.visualStylesEnabled = this.VisualStylesEnabled;
		}


		/// <summary>
		/// Returns whether Windows XP Visual Styles are currently enabled
		/// </summary>
		protected bool VisualStylesEnabled
		{
			get
			{
				OperatingSystem os = System.Environment.OSVersion;

				// check if the OS id XP or higher
				if (os.Platform == PlatformID.Win32NT && ((os.Version.Major == 5 && os.Version.Minor >= 1) || os.Version.Major > 5))
				{
					// are themes enabled
					if (UxTheme.IsThemeActive() && UxTheme.IsAppThemed())
					{
						DLLVERSIONINFO version = new DLLVERSIONINFO();
						version.cbSize = Marshal.SizeOf(typeof(DLLVERSIONINFO));

						// are we using Common Controls v6
						if (NativeMethods.DllGetVersion(ref version) == 0)
						{
							return (version.dwMajorVersion > 5);
						}
					}
				}

				return false;
			}
		}


		/// <summary>
		/// Processes Windows messages
		/// </summary>
		/// <param name="m">The Windows Message to process</param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (!this.visualStylesEnabled || this.BorderStyle != BorderStyle.Fixed3D)
			{
				return;
			}

			if (m.Msg == (int) WindowMessageFlags.WM_PRINT)
			{
				if ((m.LParam.ToInt32() & (int) WmPrintFlags.PRF_NONCLIENT) == (int) WmPrintFlags.PRF_NONCLIENT)
				{
					// open theme data
					IntPtr hTheme = UxTheme.OpenThemeData(this.Handle, UxTheme.WindowClasses.ListView);

					if (hTheme != IntPtr.Zero)
					{
						// get the part and state needed
						int partId = (int) UxTheme.Parts.ListView.ListItem;
						int stateId = (int) UxTheme.PartStates.ListItem.Normal;
					
						RECT rect = new RECT();
						rect.right = this.Width;
						rect.bottom = this.Height;

						RECT clipRect = new RECT();

						// draw the left border
						clipRect.left = rect.left;
						clipRect.top = rect.top;
						clipRect.right = rect.left + 2;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the top border
						clipRect.left = rect.left;
						clipRect.top = rect.top;
						clipRect.right = rect.right;
						clipRect.bottom = rect.top + 2;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the right border
						clipRect.left = rect.right - 2;
						clipRect.top = rect.top;
						clipRect.right = rect.right;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the bottom border
						clipRect.left = rect.left;
						clipRect.top = rect.bottom - 2;
						clipRect.right = rect.right;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);
					}

					UxTheme.CloseThemeData(hTheme);
				}
			}
		}
	}

	#endregion



	#region XPDateTimePicker

	/// <summary>
	/// A DateTimePicker that correctly draws themed borders if Windows XP 
	/// Visual Styles are enabled when it recieves a WM_PRINT message
	/// </summary>
	[ToolboxItem(true)]
	public class XPDateTimePicker : DateTimePicker
	{
		/// <summary>
		/// The cached value of whether Xindows XP Visual Styles are enabled
		/// </summary>
		private bool visualStylesEnabled;

		
		/// <summary>
		/// Initializes a new instance of the XPDateTimePicker class with default settings
		/// </summary>
		public XPDateTimePicker() : base()
		{
			// check if visual styles have been enabled
			this.visualStylesEnabled = this.VisualStylesEnabled;
		}


		/// <summary>
		/// Raises the SystemColorsChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);

			// recheck if visual styles have been enabled
			this.visualStylesEnabled = this.VisualStylesEnabled;
		}


		/// <summary>
		/// Returns whether Windows XP Visual Styles are currently enabled
		/// </summary>
		protected bool VisualStylesEnabled
		{
			get
			{
				OperatingSystem os = System.Environment.OSVersion;

				// check if the OS id XP or higher
				if (os.Platform == PlatformID.Win32NT && ((os.Version.Major == 5 && os.Version.Minor >= 1) || os.Version.Major > 5))
				{
					// are themes enabled
					if (UxTheme.IsThemeActive() && UxTheme.IsAppThemed())
					{
						DLLVERSIONINFO version = new DLLVERSIONINFO();
						version.cbSize = Marshal.SizeOf(typeof(DLLVERSIONINFO));

						// are we using Common Controls v6
						if (NativeMethods.DllGetVersion(ref version) == 0)
						{
							return (version.dwMajorVersion > 5);
						}
					}
				}

				return false;
			}
		}


		/// <summary>
		/// Processes Windows messages
		/// </summary>
		/// <param name="m">The Windows Message to process</param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (!this.visualStylesEnabled)
			{
				return;
			}

			if (m.Msg == (int) WindowMessageFlags.WM_PRINT)
			{
				if ((m.LParam.ToInt32() & (int) WmPrintFlags.PRF_NONCLIENT) == (int) WmPrintFlags.PRF_NONCLIENT)
				{
					// open theme data
					IntPtr hTheme = UxTheme.OpenThemeData(this.Handle, UxTheme.WindowClasses.Edit);

					if (hTheme != IntPtr.Zero)
					{
						// get the part and state needed
						int partId = (int) UxTheme.Parts.Edit.EditText;
						int stateId = (int) UxTheme.PartStates.EditText.Normal;
					
						RECT rect = new RECT();
						rect.right = this.Width;
						rect.bottom = this.Height;

						RECT clipRect = new RECT();

						// draw the left border
						clipRect.left = rect.left;
						clipRect.top = rect.top;
						clipRect.right = rect.left + 2;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the top border
						clipRect.left = rect.left;
						clipRect.top = rect.top;
						clipRect.right = rect.right;
						clipRect.bottom = rect.top + 2;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the right border
						clipRect.left = rect.right - 2;
						clipRect.top = rect.top;
						clipRect.right = rect.right;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the bottom border
						clipRect.left = rect.left;
						clipRect.top = rect.bottom - 2;
						clipRect.right = rect.right;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);
					}

					UxTheme.CloseThemeData(hTheme);
				}
			}
		}
	}

	#endregion



	#region XPListBox

	/// <summary>
	/// A ListBox that correctly draws themed borders if Windows XP 
	/// Visual Styles are enabled when it recieves a WM_PRINT message
	/// </summary>
	[ToolboxItem(true)]
	public class XPListBox : ListBox
	{
		/// <summary>
		/// The cached value of whether Xindows XP Visual Styles are enabled
		/// </summary>
		private bool visualStylesEnabled;

		
		/// <summary>
		/// Initializes a new instance of the XPListBox class with default settings
		/// </summary>
		public XPListBox() : base()
		{
			// check if visual styles have been enabled
			this.visualStylesEnabled = this.VisualStylesEnabled;
		}

		
		/// <summary>
		/// Raises the SystemColorsChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);

			// recheck if visual styles have been enabled
			this.visualStylesEnabled = this.VisualStylesEnabled;
		}


		/// <summary>
		/// Returns whether Windows XP Visual Styles are currently enabled
		/// </summary>
		protected bool VisualStylesEnabled
		{
			get
			{
				OperatingSystem os = System.Environment.OSVersion;

				// check if the OS id XP or higher
				if (os.Platform == PlatformID.Win32NT && ((os.Version.Major == 5 && os.Version.Minor >= 1) || os.Version.Major > 5))
				{
					// are themes enabled
					if (UxTheme.IsThemeActive() && UxTheme.IsAppThemed())
					{
						DLLVERSIONINFO version = new DLLVERSIONINFO();
						version.cbSize = Marshal.SizeOf(typeof(DLLVERSIONINFO));

						// are we using Common Controls v6
						if (NativeMethods.DllGetVersion(ref version) == 0)
						{
							return (version.dwMajorVersion > 5);
						}
					}
				}

				return false;
			}
		}


		/// <summary>
		/// Processes Windows messages
		/// </summary>
		/// <param name="m">The Windows Message to process</param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (!this.visualStylesEnabled || this.BorderStyle != BorderStyle.Fixed3D)
			{
				return;
			}

			if (m.Msg == (int) WindowMessageFlags.WM_PRINT)
			{
				if ((m.LParam.ToInt32() & (int) WmPrintFlags.PRF_NONCLIENT) == (int) WmPrintFlags.PRF_NONCLIENT)
				{
					// open theme data
					IntPtr hTheme = UxTheme.OpenThemeData(this.Handle, UxTheme.WindowClasses.ListView);

					if (hTheme != IntPtr.Zero)
					{
						// get the part and state needed
						int partId = (int) UxTheme.Parts.ListView.ListItem;
						int stateId = (int) UxTheme.PartStates.ListItem.Normal;
					
						RECT rect = new RECT();
						rect.right = this.Width;
						rect.bottom = this.Height;

						RECT clipRect = new RECT();

						// draw the left border
						clipRect.left = rect.left;
						clipRect.top = rect.top;
						clipRect.right = rect.left + 2;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the top border
						clipRect.left = rect.left;
						clipRect.top = rect.top;
						clipRect.right = rect.right;
						clipRect.bottom = rect.top + 2;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the right border
						clipRect.left = rect.right - 2;
						clipRect.top = rect.top;
						clipRect.right = rect.right;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the bottom border
						clipRect.left = rect.left;
						clipRect.top = rect.bottom - 2;
						clipRect.right = rect.right;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);
					}

					UxTheme.CloseThemeData(hTheme);
				}
			}
		}
	}

	#endregion



	#region XPListView

	/// <summary>
	/// A ListView that correctly draws themed borders if Windows XP 
	/// Visual Styles are enabled when it recieves a WM_PRINT message
	/// </summary>
	[ToolboxItem(true)]
	public class XPListView : ListView
	{
		/// <summary>
		/// The cached value of whether Xindows XP Visual Styles are enabled
		/// </summary>
		private bool visualStylesEnabled;

		
		/// <summary>
		/// Initializes a new instance of the XPListView class with default settings
		/// </summary>
		public XPListView() : base()
		{
			// check if visual styles have been enabled
			this.visualStylesEnabled = this.VisualStylesEnabled;
		}


		/// <summary>
		/// Raises the SystemColorsChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);

			// recheck if visual styles have been enabled
			this.visualStylesEnabled = this.VisualStylesEnabled;
		}


		/// <summary>
		/// Returns whether Windows XP Visual Styles are currently enabled
		/// </summary>
		protected bool VisualStylesEnabled
		{
			get
			{
				OperatingSystem os = System.Environment.OSVersion;

				// check if the OS id XP or higher
				if (os.Platform == PlatformID.Win32NT && ((os.Version.Major == 5 && os.Version.Minor >= 1) || os.Version.Major > 5))
				{
					// are themes enabled
					if (UxTheme.IsThemeActive() && UxTheme.IsAppThemed())
					{
						DLLVERSIONINFO version = new DLLVERSIONINFO();
						version.cbSize = Marshal.SizeOf(typeof(DLLVERSIONINFO));

						// are we using Common Controls v6
						if (NativeMethods.DllGetVersion(ref version) == 0)
						{
							return (version.dwMajorVersion > 5);
						}
					}
				}

				return false;
			}
		}


		/// <summary>
		/// Processes Windows messages
		/// </summary>
		/// <param name="m">The Windows Message to process</param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (!this.visualStylesEnabled || this.BorderStyle != BorderStyle.Fixed3D)
			{
				return;
			}

			if (m.Msg == (int) WindowMessageFlags.WM_PRINT)
			{
				if ((m.LParam.ToInt32() & (int) WmPrintFlags.PRF_NONCLIENT) == (int) WmPrintFlags.PRF_NONCLIENT)
				{
					// open theme data
					IntPtr hTheme = UxTheme.OpenThemeData(this.Handle, UxTheme.WindowClasses.ListView);

					if (hTheme != IntPtr.Zero)
					{
						// get the part and state needed
						int partId = (int) UxTheme.Parts.ListView.ListItem;
						int stateId = (int) UxTheme.PartStates.ListItem.Normal;

						if (!this.Enabled)
						{
							stateId = (int) UxTheme.PartStates.ListItem.Disabled;
						}
					
						RECT rect = new RECT();
						rect.right = this.Width;
						rect.bottom = this.Height;

						RECT clipRect = new RECT();

						// draw the left border
						clipRect.left = rect.left;
						clipRect.top = rect.top;
						clipRect.right = rect.left + 2;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the top border
						clipRect.left = rect.left;
						clipRect.top = rect.top;
						clipRect.right = rect.right;
						clipRect.bottom = rect.top + 2;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the right border
						clipRect.left = rect.right - 2;
						clipRect.top = rect.top;
						clipRect.right = rect.right;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the bottom border
						clipRect.left = rect.left;
						clipRect.top = rect.bottom - 2;
						clipRect.right = rect.right;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);
					}

					UxTheme.CloseThemeData(hTheme);
				}
			}
		}
	}

	#endregion



	#region XPTextBox

	/// <summary>
	/// A TextBox that correctly draws themed borders if Windows XP 
	/// Visual Styles are enabled when it recieves a WM_PRINT message
	/// </summary>
	[ToolboxItem(true)]
	public class XPTextBox : TextBox
	{
		/// <summary>
		/// The cached value of whether Xindows XP Visual Styles are enabled
		/// </summary>
		private bool visualStylesEnabled;

		
		/// <summary>
		/// Initializes a new instance of the XPTextBox class with default settings
		/// </summary>
		public XPTextBox() : base()
		{
			// check if visual styles have been enabled
			this.visualStylesEnabled = this.VisualStylesEnabled;
		}


		/// <summary>
		/// Raises the SystemColorsChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);

			// recheck if visual styles have been enabled
			this.visualStylesEnabled = this.VisualStylesEnabled;
		}


		/// <summary>
		/// Returns whether Windows XP Visual Styles are currently enabled
		/// </summary>
		protected bool VisualStylesEnabled
		{
			get
			{
				OperatingSystem os = System.Environment.OSVersion;

				// check if the OS id XP or higher
				if (os.Platform == PlatformID.Win32NT && ((os.Version.Major == 5 && os.Version.Minor >= 1) || os.Version.Major > 5))
				{
					// are themes enabled
					if (UxTheme.IsThemeActive() && UxTheme.IsAppThemed())
					{
						DLLVERSIONINFO version = new DLLVERSIONINFO();
						version.cbSize = Marshal.SizeOf(typeof(DLLVERSIONINFO));

						// are we using Common Controls v6
						if (NativeMethods.DllGetVersion(ref version) == 0)
						{
							return (version.dwMajorVersion > 5);
						}
					}
				}

				return false;
			}
		}


		/// <summary>
		/// Processes Windows messages
		/// </summary>
		/// <param name="m">The Windows Message to process</param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (!this.visualStylesEnabled)
			{
				return;
			}

			if (m.Msg == (int) WindowMessageFlags.WM_PRINT)
			{
				if ((m.LParam.ToInt32() & (int) WmPrintFlags.PRF_NONCLIENT) == (int) WmPrintFlags.PRF_NONCLIENT)
				{
					// open theme data
					IntPtr hTheme = UxTheme.OpenThemeData(this.Handle, UxTheme.WindowClasses.Edit);

					if (hTheme != IntPtr.Zero)
					{
						// get the part and state needed
						int partId = (int) UxTheme.Parts.Edit.EditText;
						int stateId = (int) UxTheme.PartStates.EditText.Normal;

						if (this.ReadOnly)
						{
							stateId = (int) UxTheme.PartStates.EditText.Readonly;
						}
						else if (!this.Enabled)
						{
							stateId = (int) UxTheme.PartStates.EditText.Disabled;
						}
					
						RECT rect = new RECT();
						rect.right = this.Width;
						rect.bottom = this.Height;

						RECT clipRect = new RECT();

						// draw the left border
						clipRect.left = rect.left;
						clipRect.top = rect.top;
						clipRect.right = rect.left + 2;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the top border
						clipRect.left = rect.left;
						clipRect.top = rect.top;
						clipRect.right = rect.right;
						clipRect.bottom = rect.top + 2;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the right border
						clipRect.left = rect.right - 2;
						clipRect.top = rect.top;
						clipRect.right = rect.right;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the bottom border
						clipRect.left = rect.left;
						clipRect.top = rect.bottom - 2;
						clipRect.right = rect.right;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);
					}

					UxTheme.CloseThemeData(hTheme);
				}
			}
		}
	}

	#endregion



	#region XPTreeView

	/// <summary>
	/// A TreeView that correctly draws themed borders if Windows XP 
	/// Visual Styles are enabled when it recieves a WM_PRINT message
	/// </summary>
	[ToolboxItem(true)]
	public class XPTreeView : TreeView
	{
		/// <summary>
		/// The cached value of whether Xindows XP Visual Styles are enabled
		/// </summary>
		private bool visualStylesEnabled;

		
		/// <summary>
		/// Initializes a new instance of the XPTreeView class with default settings
		/// </summary>
		public XPTreeView() : base()
		{
			// check if visual styles have been enabled
			this.visualStylesEnabled = this.VisualStylesEnabled;
		}


		/// <summary>
		/// Raises the SystemColorsChanged event
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);

			// recheck if visual styles have been enabled
			this.visualStylesEnabled = this.VisualStylesEnabled;
		}


		/// <summary>
		/// Returns whether Windows XP Visual Styles are currently enabled
		/// </summary>
		protected bool VisualStylesEnabled
		{
			get
			{
				OperatingSystem os = System.Environment.OSVersion;

				// check if the OS id XP or higher
				if (os.Platform == PlatformID.Win32NT && ((os.Version.Major == 5 && os.Version.Minor >= 1) || os.Version.Major > 5))
				{
					// are themes enabled
					if (UxTheme.IsThemeActive() && UxTheme.IsAppThemed())
					{
						DLLVERSIONINFO version = new DLLVERSIONINFO();
						version.cbSize = Marshal.SizeOf(typeof(DLLVERSIONINFO));

						// are we using Common Controls v6
						if (NativeMethods.DllGetVersion(ref version) == 0)
						{
							return (version.dwMajorVersion > 5);
						}
					}
				}

				return false;
			}
		}


		/// <summary>
		/// Processes Windows messages
		/// </summary>
		/// <param name="m">The Windows Message to process</param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (!this.visualStylesEnabled || this.BorderStyle != BorderStyle.Fixed3D)
			{
				return;
			}

			if (m.Msg == (int) WindowMessageFlags.WM_PRINT)
			{
				if ((m.LParam.ToInt32() & (int) WmPrintFlags.PRF_NONCLIENT) == (int) WmPrintFlags.PRF_NONCLIENT)
				{
					// open theme data
					IntPtr hTheme = UxTheme.OpenThemeData(this.Handle, UxTheme.WindowClasses.TreeView);

					if (hTheme != IntPtr.Zero)
					{
						// get the part and state needed
						int partId = (int) UxTheme.Parts.TreeView.TreeItem;
						int stateId = (int) UxTheme.PartStates.TreeItem.Normal;
					
						RECT rect = new RECT();
						rect.right = this.Width;
						rect.bottom = this.Height;

						RECT clipRect = new RECT();

						// draw the left border
						clipRect.left = rect.left;
						clipRect.top = rect.top;
						clipRect.right = rect.left + 2;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the top border
						clipRect.left = rect.left;
						clipRect.top = rect.top;
						clipRect.right = rect.right;
						clipRect.bottom = rect.top + 2;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the right border
						clipRect.left = rect.right - 2;
						clipRect.top = rect.top;
						clipRect.right = rect.right;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);

						// draw the bottom border
						clipRect.left = rect.left;
						clipRect.top = rect.bottom - 2;
						clipRect.right = rect.right;
						clipRect.bottom = rect.bottom;
						UxTheme.DrawThemeBackground(hTheme, m.WParam, partId, stateId, ref rect, ref clipRect);
					}

					UxTheme.CloseThemeData(hTheme);
				}
			}
		}
	}

	#endregion
}
