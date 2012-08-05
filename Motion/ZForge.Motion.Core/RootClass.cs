using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Motion.Util;

namespace ZForge.Motion.Core
{
	public sealed class RootClass : GroupClass
	{
		private static readonly RootClass mInstance = new RootClass();

		public static RootClass Instance
		{
			get { return mInstance; }
		}

		private RootClass()
		{
			this.LoadConfig();
			this.Name = "Root";
			this.ID = "r.f6244f011036e1b3de69ffa87a144726"; //"ZDJJHBB"
		}

		protected override string IDPrefix
		{
			get
			{
				return "r.";
			}
		}

		public void Reload()
		{
			ItemClassCollection items = new ItemClassCollection();
			items.LoadConfig(MotionConfiguration.Instance.ItemSettings);
			items.SetGroup(this);

			foreach (ItemClass i in items.Values)
			{
				this.Add(i);
			}
		}

		public void LoadConfig()
		{
			this.Children.Clear();
			this.Children.LoadConfig(MotionConfiguration.Instance.ItemSettings);
			this.Children.SetGroup(this);
		}

		public void SaveConfig()
		{
			this.Children.SaveConfig(MotionConfiguration.Instance.ItemSettings);
		}
	}
}
