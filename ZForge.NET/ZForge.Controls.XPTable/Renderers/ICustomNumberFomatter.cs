using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.XPTable.Renderers
{
	/// <summary>
	/// ICustomNumberFomatter interface
	/// </summary>
	public interface ICustomNumberFomatter
	{
		/// <summary>
		/// format function
		/// </summary>
		/// <param name="decimalVal"></param>
		/// <param name="format"></param>
		/// <returns></returns>
		string Format(decimal decimalVal, string format);
	}
}
