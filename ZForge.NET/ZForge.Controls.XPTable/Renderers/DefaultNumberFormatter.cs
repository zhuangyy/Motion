using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.XPTable.Renderers
{
	/// <summary>
	/// The default number formmater
	/// </summary>
	public class DefaultNumberFormatter : ICustomNumberFomatter
	{
		#region ICustomNumberFomatter Members
		
		/// <summary>
		/// default number formatter, just return decimalVal.ToString(format)
		/// </summary>
		/// <param name="decimalVal"></param>
		/// <param name="format"></param>
		/// <returns></returns>
		public string Format(decimal decimalVal, string format)
		{
			return decimalVal.ToString(format);
		}

		#endregion
	}
}
