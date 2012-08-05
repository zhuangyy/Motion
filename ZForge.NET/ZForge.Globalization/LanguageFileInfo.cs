using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace ZForge.Globalization
{
	public class LanguageFileInfo
	{
		private string mLangName;
		private List<string> mFiles;
		private Dictionary<string, string> mStrings;

		public LanguageFileInfo(string langName)
		{
			this.mLangName = langName;
			this.mFiles = new List<string>();
			//this.mID = this.MD5(langName.ToLower());
		}

		private string MD5(string p)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] bs = System.Text.Encoding.UTF8.GetBytes(p);
			bs = x.ComputeHash(bs);
			System.Text.StringBuilder s = new System.Text.StringBuilder();

			foreach (byte b in bs)
			{
				s.Append(b.ToString("x2").ToLower());
			}
			return s.ToString();
		}

		public string ID
		{
			get { return this.Language.ToLower(); }
		}

		public string Language
		{
			get { return this.mLangName; }
		}

		public List<string> Files
		{
			get { return this.mFiles; }
		}

		public void Load()
		{
			this.mStrings = new Dictionary<string, string>();
			foreach (string f in this.Files)
			{
				try
				{
					XmlDocument doc = new XmlDocument();
					doc.Load(new System.IO.StreamReader(f, Encoding.UTF8));
					XmlNode root = doc["localizer"];
					foreach (XmlNode lang in root.ChildNodes)
					{
						if (lang.Name == "language" && lang.Attributes["name"] != null && lang.Attributes["name"].Value.ToLower() == this.Language.ToLower())
						{
							foreach (XmlNode str in lang)
							{
								if (str.Name == "string" && str.Attributes["id"] != null)
								{
									this.mStrings.Add(str.Attributes["id"].Value, str.InnerText);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Load Language File " + f + " Failed!\n" + ex.Message, "Globalization", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		public string GetString(string v)
		{
			if (this.mStrings == null)
			{
				this.Load();
			}
			string id = this.MD5(v.ToLower());
			string r = null;
			this.mStrings.TryGetValue(id, out r); 
			return r;
		}
	}
}
