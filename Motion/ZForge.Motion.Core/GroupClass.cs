using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ZForge.Configuration;

namespace ZForge.Motion.Core
{
	public class GroupClass : ItemClass, IConfigurable
	{
		protected ItemClassCollection mChildren;

		public GroupClass()
		{
		}

		public GroupClass(IConfigSetting i)
		{
			this.LoadConfig(i);
		}

		public GroupClass(IConfigSetting i, GroupClass parent)
		{
			this.LoadConfig(i);
			this.Group = parent;
		}

		protected override string IDPrefix
		{
			get
			{
				return "g.";
			}
		}

		public override void Clean()
		{
			foreach (ItemClass i in this.Children.Values)
			{
				i.Clean();
			}
		}

		public ItemClassCollection Children
		{
			get
			{
				if (this.mChildren == null)
				{
					this.mChildren = new ItemClassCollection();
				}
				return this.mChildren;
			}
		}

		public virtual void Add(ItemClass c)
		{
			if (false == this.Children.ContainsKey(c.ID))
			{
				this.Children.Add(c.ID, c);
				c.Group = this;
			}
		}

		public ItemClass Find(string id)
		{
			foreach (ItemClass i in this.Children.Values)
			{
				if (i.ID == id)
				{
					return i;
				}
				if (i is GroupClass)
				{
					GroupClass g = i as GroupClass;
					ItemClass r = g.Find(id);
					if (r != null)
					{
						return r;
					}
				}
			}
			return null;
		}

		#region IConfigurable Members

		public virtual void SaveConfig(IConfigSetting s)
		{
			s.RemoveChildren();
			if (this.mChildren != null)
			{
				this.mChildren.SaveConfig(s);
			}
			// this 2 lines must be the last
			s["id"].Value = this.ID;
			s["name"].Value = this.Name;
		}

		public virtual void LoadConfig(IConfigSetting s)
		{
			this.ID = s["id"].Value;
			this.Name = s["name"].Value;

			this.mChildren = new ItemClassCollection(s, this);
		}

		#endregion
	}
}
