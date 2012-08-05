using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections;

namespace ZForge.Win32
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal class LOGFONT
	{
		public int lfHeight = 0;
		public int lfWidth = 0;
		public int lfEscapement = 0;
		public int lfOrientation = 0;
		public int lfWeight = 0;
		public byte lfItalic = 0;
		public byte lfUnderline = 0;
		public byte lfStrikeOut = 0;
		public byte lfCharSet = 0;
		public byte lfOutPrecision = 0;
		public byte lfClipPrecision = 0;
		public byte lfQuality = 0;
		public byte lfPitchAndFamily = 0;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string lfFaceName = string.Empty;
	}

	public static class FontHelper
	{
		public static string GetFontFaceName(Font ft)
		{
			LOGFONT lt = new LOGFONT();
			ft.ToLogFont(lt);
			return lt.lfFaceName;
		}

		public static string GetFontFaceName(string fn)
		{
			Font ft = new Font(fn, 9F);
			LOGFONT lt = new LOGFONT();
			ft.ToLogFont(lt);
			return lt.lfFaceName;
		}

		public static ArrayList FontList
		{
			get
			{
				ArrayList r = new ArrayList();
				foreach (FontFamily ff in FontFamily.Families)
				{
					try
					{
						Font ft = new Font(ff.Name, 8F);
						LOGFONT lt = new LOGFONT();
						ft.ToLogFont(lt);
						if (lt.lfFaceName != null && lt.lfFaceName.Length != 0)
						{
							r.Add(new KeyValuePair<string, string>(ff.Name, lt.lfFaceName));
						}
					}
					catch (Exception)
					{
					}
				}
				return r;
			}
		}
	}
}
