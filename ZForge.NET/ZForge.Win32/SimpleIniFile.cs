using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ZForge.Win32
{
	public class SimpleIniFile
	{
		private string mFile;

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section,
				string key, string val, string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section,
						 string key, string def, StringBuilder retVal,
				int size, string filePath);

		public SimpleIniFile(string path)
		{
			this.mFile = path;
		}

		public void WriteValue(string Section, string Key, string Value)
		{
			WritePrivateProfileString(Section, Key, Value, this.mFile);
		}

		public string ReadValue(string Section, string Key, string Def)
		{
			StringBuilder temp = new StringBuilder(255);
			int i = GetPrivateProfileString(Section, Key, "", temp,
																			255, this.mFile);
			string sTemp = temp.ToString();
			if (sTemp == string.Empty)
			{
				sTemp = Def;
			}
			return sTemp;
		}
	}
}
