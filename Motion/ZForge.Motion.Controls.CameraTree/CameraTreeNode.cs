using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ZForge.Motion.Core;

namespace ZForge.Motion.Controls
{
	public class CameraTreeNode : TreeNode
	{
		private ItemClass item;

		public CameraTreeNode(ItemClass i)
		{
			this.Init(i);
		}

		public void Init(ItemClass i) 
		{
			this.item = i;
			this.Text = this.Item.Name;
			this.ImageIndex = this.IsGroup() ? 0 : 1;
			this.SelectedImageIndex = this.ImageIndex;
			if (this.Item is GroupClass) {
				return;
			}
			CameraClass c = this.Item as CameraClass;
			if (c != null)
			{
				Motion.PlugIns.IPlugInVideoSource s = c.PlugInVideoSource;
				if (s != null)
				{
					this.ToolTipText = s.LabelText;
				}
			}
		}

		public ItemClass Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
				if (value != null)
				{
					this.Text = value.Name;
				}
			}
		}

		public bool IsGroup()
		{
			return (this.item is GroupClass);
		}

	}
}
