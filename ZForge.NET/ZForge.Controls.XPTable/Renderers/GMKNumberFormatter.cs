using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.XPTable.Renderers
{
	/// <summary>
	/// Format number to G, M, K style
	/// </summary>
	public class GMKNumberFormatter : ICustomNumberFomatter
	{
		#region ICustomNumberFomatter Members

		/// <summary>
		/// format decimalVal to G, M, K, then append format as surffix
		/// </summary>
		/// <param name="decimalVal"></param>
		/// <param name="format"></param>
		/// <returns></returns>
		public string Format(decimal decimalVal, string format)
		{
			decimal g = 1024 * 1024 * 1024;
			decimal m = 1024 * 1024;
			decimal k = 1024m;

			decimal x;
			string v;

			x = decimalVal / g;
			if (x >= 1.0m)
			{
				v = x.ToString("F02") + "G";
			}
			else
			{
				x = decimalVal / m;
				if (x >= 1.0m)
				{
					v = x.ToString("F02") + "M";
				}
				else
				{
					x = decimalVal / k;
					if (x >= 1.0m)
					{
						v = x.ToString("F02") + "K";
					}
					else
					{
						v = decimalVal.ToString("G");
					}
				}
			}
			return v + format;
		}

		#endregion
	}
}
