using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ZForge.Configuration;

#if SAFE_ANYWHERE_WITHUK
using ZForge.UK.Util;
#endif

namespace ZForge.SA.Komponent
{
	public abstract class SAConfiguration
	{
		protected XMLConfig mConfig = null;

		public SAConfiguration()
		{
			this.Load();
		}

		protected abstract string ConfigFile { get; }

		protected virtual float CurrentVersion
		{
			get { return 1.0F; }
		}

		public IConfigSetting MainFormSettings
		{
			get
			{
				return this.Config.Settings["mainform"];
			}
		}

		protected XMLConfig Config
		{
			get { return this.mConfig; }
		}

		protected virtual bool Upgrade(XMLConfig config, float from)
		{
			return true;
		}

		public virtual void Load()
		{
#if SAFE_ANYWHERE_WITHUK
			FileInfo fi = new FileInfo(this.ConfigFile);
			string x = UKInterOp.Instance.Hidden.LoadConfig(@"\" + fi.Name);
			mConfig = new XMLConfig(x);
#else
			mConfig = new XMLConfig(this.ConfigFile, true);
#endif
			mConfig.CleanUpOnSave = true;
			if (mConfig.Settings["version"].Exists)
			{
				if (this.CurrentVersion > mConfig.Settings["version"].floatValue)
				{
					this.Upgrade(mConfig, mConfig.Settings["version"].floatValue);
				}
			}
			mConfig.Settings["version"].floatValue = this.CurrentVersion;
		}

		public void SaveConfig(IConfigSetting x, IConfigurable c)
		{
			c.SaveConfig(x);
		}

		public void LoadConfig(IConfigSetting x, IConfigurable c)
		{
			c.LoadConfig(x);
		}

		public virtual void Save()
		{
#if SAFE_ANYWHERE_WITHUK
			FileInfo fi = new FileInfo(this.ConfigFile);

			MemoryStream ms = new MemoryStream();
			mConfig.Save(ms);
			UKInterOp.Instance.Hidden.SaveConfig(@"\" + fi.Name, ms.ToArray());
#else
			mConfig.Commit();
#endif
		}
	}
}
