using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZForge.Globalization
{
	public sealed class Translator
	{
		private static LanguageFileInfoCollection mCollection = new LanguageFileInfoCollection();
		private static readonly Translator mInstance = new Translator();

		// Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
		static Translator()
    {
    }

		private Translator()
		{
		}

		public string T(string v)
		{
			LanguageFileInfo fi = mCollection.CurrentLanguage;
			string r = fi.GetString(v);
			return (r == null) ? v : r;
		}

		public void Update(object o, bool recursive)
		{
			IGlobalization i = o as IGlobalization;
			if (i != null)
			{
				i.UpdateCulture();
			}
			if (recursive)
			{
				Control c = o as Control;
				if (c != null)
				{
					foreach (Control x in c.Controls)
					{
						this.Update(x, recursive);
					}
				}
			}
		}

		public void Update(object o)
		{
			this.Update(o, true);
		}

		public static Translator Instance
		{
			get { return mInstance; }
		}
	}
}
