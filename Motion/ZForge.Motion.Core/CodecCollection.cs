using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using ZForge.Globalization;
using System.Runtime.InteropServices;

namespace ZForge.Motion.Core
{
	public class CodecCollection : Dictionary<string, string>
	{
		public CodecCollection()
			: base()
		{
      uint ICTYPE_VIDEO = Codec.mmioFOURCC('v', 'i', 'd', 'c');
      uint n;
      ICINFO info = new ICINFO();
      for (n = 0; MSVFW32.ICInfo(ICTYPE_VIDEO, n, ref info); n++)
      {
        System.IntPtr iIC = MSVFW32.ICOpen(info.fccType, info.fccHandler, ICMODE.QUERY);
        if (iIC != System.IntPtr.Zero)
        {
          int cb = Marshal.SizeOf(typeof(ICINFO));
          MSVFW32.ICGetInfo(iIC, ref info, (uint)cb);
          //if ((info.dwFlags & (uint)VIDCF.COMPRESSFRAMES) != 0)
          //{
            string s = Codec.FOURCCmmio(info.fccHandler);
            if (!this.ContainsKey(s))
            {
              this.Add(s, info.szDescription + " (" + s + ")");
            }
          //}
          MSVFW32.ICClose(iIC);
        }
      }
    }

		public List<KeyValuePair<string, string>> KVs
		{
			get
			{
				List<KeyValuePair<string, string>> r = new List<KeyValuePair<string, string>>();
				foreach (KeyValuePair<string, string> v in this)
				{
					r.Add(v);
				}
				r.Sort(delegate(KeyValuePair<string, string> obj1, KeyValuePair<string, string> obj2) { return obj1.Value.CompareTo(obj2.Value); });
				return r;
			}
		}

		public string GetValue(string key, string def)
		{
			if (key == null || this.ContainsKey(key) == false)
			{
				return def;
			}
			return this[key];
		}

		public string GetValue(string key)
		{
			return this.GetValue(key, null);
		}
	}
}
