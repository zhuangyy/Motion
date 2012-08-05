using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace ZForge.Controls
{
	/// <summary>
	/// Specifies the border style of a control.
	/// </summary>
	public enum HeaderPanelBorderStyles
	{
		None,
		Single,
		Shadow,
		Inset,
		Outset,
		Groove,
		Ridge
	};

	/// <summary>
	/// Specifies the position of caption bar.
	/// </summary>
	public enum HeaderPanelCaptionPositions
	{
		Top,
		Bottom,
		Left,
		Right
	};
}