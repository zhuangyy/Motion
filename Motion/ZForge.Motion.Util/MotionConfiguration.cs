using System;
using System.Collections.Generic;
using System.Text;
using ZForge.SA.Komponent;
using System.Windows.Forms;
using System.IO;
using ZForge.Configuration;
using System.Drawing;
using ZForge.Globalization;
using ZForge.Win32;

namespace ZForge.Motion.Util
{
	public sealed class MotionConfiguration : SAConfiguration
	{
		private static readonly MotionConfiguration mInstance = new MotionConfiguration();

		public event StorageChangedEventHandler StorageChanged;

		private MotionConfiguration()
			: base()
		{
			this.Import();
		}

		#region Properties

		public static MotionConfiguration Instance
		{
			get { return mInstance; }
		}

		public bool StorageIsValid
		{
			get
			{
				return Directory.Exists(this.Storage);
			}
		}

		public string Storage
		{
			get
			{
				string r = this.Config.Settings["preference"]["datapath"].Value;
				if (string.IsNullOrEmpty(r))
				{
					FileInfo fi = new FileInfo(Application.ExecutablePath);
					r = Path.Combine(fi.DirectoryName, "Motion.Storage");
				}
				FileInfo si = new FileInfo(r);
				r = si.FullName;
				try
				{
					if (Directory.Exists(r) == false)
					{
						Directory.CreateDirectory(r);
					}
				}
				catch { }
				return r;
			}
			set
			{
				string v = this.Storage;
				this.Config.Settings["preference"]["datapath"].Value = value;
				if (DirectoryX.Compare(v, this.Storage) != 0)
				{
					if (this.StorageChanged != null)
					{
						this.StorageChanged(this, new StorageChangedEventArgs(v, this.Storage));
					}
				}
			}
		}

		public string StorageAVI
		{
			get
			{
				string r = Path.Combine(this.Storage, @"Motion.AVI"); 
				try
				{
					if (Directory.Exists(r) == false)
					{
						Directory.CreateDirectory(r);
					}
				}
				catch { }
				return r;
			}
		}

		public string StoragePIC
		{
			get
			{
				string r = Path.Combine(this.Storage, @"Motion.PIC");
				try
				{
					if (Directory.Exists(r) == false)
					{
						Directory.CreateDirectory(r);
					}
				}
				catch { }
				return r;
			}
		}

		public IConfigSetting PlugInGlobalSettings
		{
			get { return this.Config.Settings["preference"]["plugins"]; }
		}

		public IConfigSetting ItemSettings
		{
			get { return this.Config.Settings["motion"]; }
		}

		public string FontName
		{
			get
			{
				string r = this.Config.Settings["preference"]["fontname"].Value;
				if (string.IsNullOrEmpty(r))
				{
					Font ft = SystemFonts.MessageBoxFont;
					this.Config.Settings["preference"]["fontname"].Value = ft.Name;
					r = ft.Name;
				}
				return r;
			}
			set
			{
				this.Config.Settings["preference"]["fontname"].Value = value;
			}
		}

		public float FontSize
		{
			get
			{
				float r = this.Config.Settings["preference"]["fontsize"].floatValue;
				if (r <= 0F)
				{
					Font ft = SystemFonts.MessageBoxFont;
					this.Config.Settings["preference"]["fontsize"].floatValue = ft.Size;
					r = ft.Size;
				}
				return r;
			}
			set
			{
				this.Config.Settings["preference"]["fontsize"].floatValue = value;
			}
		}

		#endregion

		#region Import previous settings

		private void Import()
		{
			FileInfo fi = new FileInfo(Application.ExecutablePath);
			this.Import(fi.DirectoryName, true);
		}

		public bool Import(string dir, bool clean)
		{
			bool b = false;
			if (this.ImportPreference(dir, clean))
			{
				b = true;
			}
			if (this.ImportPlugInSettings(Path.Combine(this.Storage, "Conf.Global"), this.Config.Settings["preference"]["plugins"], clean))
			{
				if (clean)
				{
					string d = Path.Combine(this.Storage, "Conf.Global");
					Directory.Delete(d, true);
				}
				b = true;
			}
			if (this.ImportCameraSettings(clean))
			{
				b = true;
			}
			if (b)
			{
				this.Save();
			}
			return b;
		}

		private bool ImportPreference(string dir, bool clean)
		{
			bool r = false;
			string f = Path.Combine(dir, "Motion.perf.XML");
			if (File.Exists(f))
			{
				XMLConfig x = new XMLConfig(f, true);
				XMLConfigSetting i = x.Settings["motion"] as XMLConfigSetting;
				this.Storage = i["data"].Value;
				this.FontName = i["fontname"].Value;
				this.FontSize = i["fontsize"].floatValue;

				r = true;
				if (clean)
				{
					File.Delete(f);
				}
			}
			return r;
		}

		private bool ImportPlugInSettings(string dir, IConfigSetting set, bool clean)
		{
			bool r = false;
			Dictionary<string, string> gs = new Dictionary<string, string>();
			gs.Add("p17a988de71b3fb81d5cc2d6612dfe072", "Motion.PlugIns.Alarm.EMail.xml");
			gs.Add("p4f2679987e0e6e85cf70ca40feaac595", "Motion.PlugIns.Alarm.Sound.xml");
			gs.Add("p1f25f05340a17e6e6acdf656d3360ea0", "Motion.PlugIns.IPCam.General.xml");
			gs.Add("p532c301e3373bcd1b7a0b63e9367e5d1", "Motion.PlugIns.IPCam.Axis.xml");
			gs.Add("pe5087ee4b071dafd9744d6201dbe65b8", "Motion.PlugIns.USBCam.General.xml");
			foreach (KeyValuePair<string, string> kv in gs)
			{
				string f = Path.Combine(dir, kv.Value);
				if (File.Exists(f))
				{
					XMLConfig x = new XMLConfig(f, true);
					XMLConfigSetting i = x.Settings["motion"] as XMLConfigSetting;
					XMLConfigSetting d = set[kv.Key] as XMLConfigSetting;
					d.Copy(i);
					r = true;
					
					if (clean)
					{
						File.Delete(f);
					}
				}
			}
			return r;
		}

		private bool ImportCameraSettings(bool clean)
		{
			bool r = false;
			DirectoryInfo di = new DirectoryInfo(this.Storage);
			if (di.Exists)
			{
				DirectoryInfo[] dis = di.GetDirectories();
				foreach (DirectoryInfo d in dis)
				{
					if (d.Name.StartsWith("g.") || d.Name.StartsWith("c."))
					{
						this.ImportCameraSettings(d.FullName, this.Config.Settings["motion"], clean);
						r = true;
						if (clean)
						{
							d.Delete(true);
						}
					}
				}
			}
			return r;
		}

		private bool ImportCameraSettings(string path, IConfigSetting set, bool clean)
		{
			bool r = false;
			DirectoryInfo di = new DirectoryInfo(path);
			if (di.Name.StartsWith("g."))
			{
				r = true;
				XMLConfigSetting dst = set["group##"] as XMLConfigSetting;
				dst["id"].Value = di.Name;

				string f = Path.Combine(di.FullName, "group.xml");
				if (File.Exists(f))
				{
					XMLConfig x = new XMLConfig(f, true);
					dst["name"].Value = x.Settings["group"]["name"].Value;
					if (clean)
					{
						File.Delete(f);
					}
				}

				DirectoryInfo[] dis = di.GetDirectories();
				foreach (DirectoryInfo d in dis)
				{
					this.ImportCameraSettings(d.FullName, dst, clean);
				}
			}
			else
			{
				if (di.Name.StartsWith("c."))
				{
					r = true;
					string f = Path.Combine(di.FullName, "camera.xml");
					if (File.Exists(f))
					{
						XMLConfig x = new XMLConfig(f, true);
						XMLConfigSetting dst = set["camera##"] as XMLConfigSetting;
						dst.Copy(x.Settings["camera"] as XMLConfigSetting);
						dst["id"].Value = di.Name;
						dst["version"].floatValue = x.Settings["version"].floatValue;

						if (clean)
						{
							File.Delete(f);
						}

						f = Path.Combine(di.FullName, "regions.osl");
						if (File.Exists(f))
						{
							byte[] bs = File.ReadAllBytes(f);
							dst["regions"].Value = Convert.ToBase64String(bs);
							if (clean)
							{
								File.Delete(f);
							}
						}
						this.ImportPlugInSettings(di.FullName, dst["plugins"] as XMLConfigSetting, clean);

						this.RelocateAVIs(di);
						this.RelocatePICs(di);
					}
				}
			}
			return r;
		}

		private void RelocatePICs(DirectoryInfo di)
		{
			List<FileInfo> list = new List<FileInfo>();
			FileInfo[] files;

			files = di.GetFiles("*.png");
			list.AddRange(files);
			files = di.GetFiles("*.jpg");
			list.AddRange(files);

			string id = di.Name;
			foreach (FileInfo fi in list)
			{
				string dst = string.Format("{0}.{1}.{2}", di.Name, 1 /* RecordMark.UNREAD */, fi.Name);
				dst = Path.Combine(this.StoragePIC, dst);
				try
				{
					fi.CopyTo(dst, true);
					fi.Delete();
				}
				catch (Exception) { }
			}
		}

		private void RelocateAVIs(DirectoryInfo di)
		{
			List<FileInfo> list = new List<FileInfo>();
			FileInfo[] files;

			files = di.GetFiles("*.avi");
			list.AddRange(files);

			string id = di.Name;
			foreach (FileInfo fi in list)
			{
				string dst = string.Format("{0}.{1}.{2}", di.Name, 1 /* RecordMark.UNREAD */, fi.Name);
				dst = Path.Combine(this.StorageAVI, dst);
				try
				{
					fi.CopyTo(dst, true);
					fi.Delete();
				}
				catch (Exception) { }
			}
		}

		#endregion

		protected override string ConfigFile
		{
			get
			{
				FileInfo fi = new FileInfo(Application.ExecutablePath);
				return Path.Combine(fi.DirectoryName, "ZForge.Motion.Config.XML");
			}
		}
	}
}
