using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ZForge.PlugIn;
using ZForge.Controls.PropertyGridEx;
using ZForge.Globalization;
using ZForge.Configuration;
using ZForge.Motion.Util;

namespace ZForge.Motion.PlugIns
{
	public sealed class MotionPlugIns : ZForge.PlugIn.PlugIns<IPlugIn>
	{
		public MotionPlugIns()
			: base()
		{
			FileInfo fi = new FileInfo(Application.ExecutablePath);
			string dir = Path.Combine(fi.DirectoryName, "Motion.PlugIns");
			string pattern = "Motion.PlugIns.*.dll";
			this.Load(dir, pattern);
		}

		public void AlarmAll(bool run)
		{
			foreach (AvailablePlugIn<IPlugIn> p in this.AvailablePlugInCollection)
			{
				IPlugInAlarm i = p.Instance as IPlugInAlarm;
				if (i != null)
				{
					if (run)
					{
						if (i.Enabled)
						{
							i.Alarm();
						}
					}
					else
					{
						i.Stop();
					}
				}
			}
		}

		public void ReleaseAll()
		{
			foreach (AvailablePlugIn<IPlugIn> p in this.AvailablePlugInCollection)
			{
				p.Instance.Release();
			}
		}

		public void ValidateAll(List<string> msgs, bool global)
		{
			foreach (AvailablePlugIn<IPlugIn> p in this.AvailablePlugInCollection)
			{
				if (global)
				{
					IPlugInUIWithGlobal i = p.Instance as IPlugInUIWithGlobal;
					if (i != null)
					{
						p.Instance.ValidCheck(msgs);
					}
				}
				else
				{
					p.Instance.ValidCheck(msgs);
				}
			}
		}

		public string GetPlugInCategory(IPlugIn p)
		{
			string r = "";
			if (p is IPlugInAlarm)
			{
				r += Translator.Instance.T("[报警]");
			}
			if (p is IPlugInVideoSource)
			{
				r += Translator.Instance.T("[视频来源]");
			}
			if (r.Length == 0)
			{
				r = Translator.Instance.T("[未知]");
			}
			return r.Trim();
		}

		public void ToolStripItemsInitialization(ToolStripItemCollection items)
		{
			foreach (AvailablePlugIn<IPlugIn> pi in this.AvailablePlugInCollection)
			{
				IPlugInUI u = pi.Instance as IPlugInUI;
				if (u == null)
				{
					continue;
				}
				List<System.Windows.Forms.ToolStripItem> list = u.UIEditFormToolStripItems;
				if (list == null)
				{
					continue;
				}
				foreach (System.Windows.Forms.ToolStripItem b in list)
				{
					items.Add(b);
				}
			}
		}

		public void PropertyGridItemsInitialization(ZForge.Controls.PropertyGridEx.CustomPropertyCollection items)
		{
			this.PropertyGridItemsInitialization(items, false);
		}

		public void PropertyGridItemsInitialization(ZForge.Controls.PropertyGridEx.CustomPropertyCollection items, bool global)
		{
			foreach (AvailablePlugIn<IPlugIn> pi in this.AvailablePlugInCollection)
			{
				IPlugInUI u = pi.Instance as IPlugInUI;
				if (u == null)
				{
					continue;
				}
				if (global == false)
				{
					List<ZForge.Controls.PropertyGridEx.CustomProperty> plist = u.UIPropertyItems;
					if (plist == null)
					{
						continue;
					}
					foreach (ZForge.Controls.PropertyGridEx.CustomProperty t in plist)
					{
						if (pi.Instance is IPlugInVideoSource)
						{
							t.Visible = false;
						}
						items.Add(t);
					}
				}
				else
				{
					IPlugInUIWithGlobal ig = pi.Instance as IPlugInUIWithGlobal;
					if (ig != null)
					{
						ig.GlobalOperation = true;
						List<ZForge.Controls.PropertyGridEx.CustomProperty> plist = u.UIPropertyItems;
						if (plist == null)
						{
							continue;
						}
						foreach (CustomProperty t in plist)
						{
							items.Add(t);
						}
					}
				}
			}
		}

		public bool PropertyGridItemsSetValue(PropertyValueChangedEventArgs e)
		{
			bool r = false;
			foreach (AvailablePlugIn<IPlugIn> p in this.AvailablePlugInCollection)
			{
				CustomProperty.CustomPropertyDescriptor d = e.ChangedItem.PropertyDescriptor as CustomProperty.CustomPropertyDescriptor;
				if (d == null)
				{
					continue;
				}
				CustomProperty c = d.CustomProperty as CustomProperty;
				if (c == null || false == c.Tag.Equals(p.Instance.ID))
				{
					continue;
				}
				IPlugInUI u = p.Instance as IPlugInUI;
				if (u != null)
				{
					u.UISetValue(e);
					r = true;
					break;
				}
			}
			return r;
		}

		public void LoadGlobalConfig()
		{
			foreach (AvailablePlugIn<IPlugIn> p in this.AvailablePlugInCollection)
			{
				IPlugInUIWithGlobal i = p.Instance as IPlugInUIWithGlobal;
				if (i != null && i.UseGlobal == true)
				{
					p.Instance.LoadConfig(MotionConfiguration.Instance.PlugInGlobalSettings);
				}
			}
		}

		public void LoadConfigAll(IConfigSetting section)
		{
			foreach (AvailablePlugIn<IPlugIn> p in this.AvailablePlugInCollection)
			{
				p.Instance.LoadConfig(section);
			}
		}

		public void SaveGlobalConfig()
		{
			foreach (AvailablePlugIn<IPlugIn> p in this.AvailablePlugInCollection)
			{
				IPlugInUIWithGlobal i = p.Instance as IPlugInUIWithGlobal;
				if (i != null && i.UseGlobal == true)
				{
					p.Instance.SaveConfig(MotionConfiguration.Instance.PlugInGlobalSettings);
				}
			}
		}

		public void SaveConfigAll(IConfigSetting section)
		{
			foreach (AvailablePlugIn<IPlugIn> p in this.AvailablePlugInCollection)
			{
				p.Instance.SaveConfig(section);
			}
		}

		public void UpdateCultureAll()
		{
			foreach (AvailablePlugIn<IPlugIn> p in this.AvailablePlugInCollection)
			{
				IGlobalization i = p.Instance as IGlobalization;
				if (i != null)
				{
					Translator.Instance.Update(i);
				}
			}
		}
	}
}
