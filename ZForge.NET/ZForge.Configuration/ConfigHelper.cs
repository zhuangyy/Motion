using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Configuration
{
	public static class ConfigHelper
	{
		public static void Save(IConfigSetting s, System.Windows.Forms.Form f)
		{
			s["w"].intValue = f.Width;
			s["h"].intValue = f.Height;
			s["l"].intValue = f.Left;
			s["t"].intValue = f.Top;
		}

		public static void Load(IConfigSetting s, System.Windows.Forms.Form f)
		{
			int w = s["w"].intValue;
			int h = s["h"].intValue;
			int l = s["l"].intValue;
			int t = s["t"].intValue;

			if (w > 0) f.Width = w;
			if (h > 0) f.Height = h;
			if (s["l"].Exists) f.Left = l;
			if (s["t"].Exists) f.Top = t;
		}
	}
}
