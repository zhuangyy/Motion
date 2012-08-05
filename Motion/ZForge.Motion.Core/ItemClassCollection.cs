using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Configuration;

namespace ZForge.Motion.Core
{
	public class ItemClassCollection : Dictionary<string, ItemClass>, IConfigurable
	{
		public ItemClassCollection()
		{
		}

		public ItemClassCollection(IConfigSetting s, GroupClass parent)
		{
			this.LoadConfig(s);
			this.SetGroup(parent);
		}

		public void SetGroup(GroupClass parent)
		{
			foreach (ItemClass i in this.Values)
			{
				i.Group = parent;
			}
		}

		public void Remove(ItemClass c)
		{
			if (this.ContainsKey(c.ID))
			{
				this.Remove(c.ID);
			}
		}

		public void Add(ItemClass c, GroupClass parent)
		{
			if (false == this.ContainsKey(c.ID))
			{
				c.Group = parent;
				this.Add(c.ID, c);
			}
		}

		#region IConfigurable Members

		public void SaveConfig(IConfigSetting s)
		{
			s.RemoveChildren();
			foreach (ItemClass i in this.Values)
			{
				GroupClass g = i as GroupClass;
				if (g != null)
				{
					IConfigSetting gs = s["group##"];
					g.SaveConfig(gs);
				}
				else
				{
					CameraClass c = i as CameraClass;
					if (c != null)
					{
						IConfigSetting cs = s["camera##"];
						c.SaveConfig(cs);
					}
				}
			}
		}

		public void LoadConfig(IConfigSetting s)
		{
			XMLConfigSetting section = s as XMLConfigSetting;
			IList<XMLConfigSetting> list = section.GetNamedChildren("group");
			foreach (XMLConfigSetting x in list)
			{
				GroupClass g = new GroupClass(x);
				if (this.ContainsKey(g.ID) == false)
				{
					this.Add(g.ID, g);
				}
			}
			list = section.GetNamedChildren("camera");
			foreach (XMLConfigSetting x in list)
			{
				CameraClass c = new CameraClass(x);
				if (this.ContainsKey(c.ID) == false)
				{
					this.Add(c.ID, c);
				}
			}
		}

		#endregion
	}
}
