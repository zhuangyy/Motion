using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;

namespace ZForge.Motion.Core
{
	public static class CameraBanner
	{
		public static Bitmap Render(Bitmap bmp, string name, bool fixedDim)
		{
			Font font = SystemFonts.MessageBoxFont;
			string s = name + " " + DateTime.Now.ToLocalTime();
			Bitmap r;

			int bannerHeight = 20;
			r = new Bitmap(bmp.Width, bmp.Height + ((fixedDim) ? 0 : bannerHeight));
			Graphics g = Graphics.FromImage(r);

			g.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bannerHeight);
			g.TextRenderingHint = TextRenderingHint.AntiAlias;
			g.DrawString(s, font, Brushes.White, 0, 0);
			g.DrawImage(bmp, 0, bannerHeight, bmp.Width, bmp.Height - ((fixedDim) ? bannerHeight : 0));
			g.Dispose();

			return r;
		}
	}
}
