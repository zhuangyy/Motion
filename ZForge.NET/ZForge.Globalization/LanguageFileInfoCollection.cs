using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Xml;

namespace ZForge.Globalization
{
	public class LanguageFileInfoCollection : Dictionary<string, LanguageFileInfo>
	{
		public LanguageFileInfoCollection()
		{
			this.Load();
		}

		public string LangFilePath
		{
			get
			{
				string e = Application.ExecutablePath;
				FileInfo ei = new FileInfo(e);
				return ei.Directory + @"\Lang";
			}
		}

		public LanguageFileInfo DefaultLanguage
		{
			get
			{
				LanguageFileInfo fi = new LanguageFileInfo("简体中文");
				return this[fi.ID];
			}
		}

		public LanguageFileInfo CurrentLanguage
		{
			get { return this.LoadUserSettings(); }
			set
			{
				this.SaveUserSettings(value);
			}
		}

		private void SaveUserSettings(LanguageFileInfo fi)
		{
			if (fi == null)
			{
				return;
			}
			RegistryKey k = Registry.CurrentUser.OpenSubKey("Software\\DiskSafe", true);
			if (k == null)
			{
				k = Registry.CurrentUser.CreateSubKey("Software\\DiskSafe");
			}
			k.SetValue("LANG", fi.ID);
			k.Close();
		}

		private LanguageFileInfo LoadUserSettings()
		{
			RegistryKey k = Registry.CurrentUser.OpenSubKey("Software\\DiskSafe");
			if (k != null)
			{
				try
				{
					if (null != k.GetValue("LANG"))
					{
						string id;
						id = k.GetValue("LANG") as string;
						if (this[id] != null)
						{
							k.Close();
							return this[id];
						}
					}
				}
				catch (Exception)
				{
				}
				finally
				{
					k.Close();
				}
			}
			return this.DefaultLanguage; 
		}

		private void Add(string lang, string file)
		{
			LanguageFileInfo fi;
			fi = new LanguageFileInfo(lang);
			if (this.ContainsKey(fi.ID))
			{
				fi = this[fi.ID];
				if (file != null)
				{
					fi.Files.Add(file);
				}
			}
			else
			{
				if (file != null)
				{
					fi.Files.Add(file);
				}
				this.Add(fi.ID, fi);
			}
		}

		public void Load()
		{
			try
			{
				string[] locFiles = System.IO.Directory.GetFiles(this.LangFilePath, "*.xml");
				foreach (string f in locFiles)
				{
					XmlDocument doc = new XmlDocument();
					doc.Load(new System.IO.StreamReader(f, Encoding.UTF8));
					XmlNode root = doc["localizer"];
					if (root == null)
					{
						continue;
					}
					foreach (XmlNode lang in root.ChildNodes)
					{
						if (lang.Name == "language" && lang.Attributes["name"] != null)
						{
							this.Add(lang.Attributes["name"].Value, f);
						}
					}
				}
			}
			catch (Exception)
			{
			}
			// Add default language
			this.Add("简体中文", null);
		}
	}
}
