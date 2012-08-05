using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Motion.PlugIns;
using System.Windows.Forms;

namespace ZForge.Motion.Controls
{
	internal class PTZControl
	{
		private System.Windows.Forms.ToolStripSeparator mToolStripSeparator = null;
		private List<System.Windows.Forms.ToolStripItem> mListToolStripItem = null;
		private IPlugInPTZ mPTZ = null;
		private bool mMirror = false;
		private bool mFlip = false;
		private Control mParent = null;

		public PTZControl(Control parent)
		{
			this.PlugIn = null;
			this.mParent = parent;
		}

		public List<System.Windows.Forms.ToolStripItem> ToolStripItemCollection
		{
			get
			{
				if (this.mListToolStripItem == null)
				{
					mListToolStripItem = new List<System.Windows.Forms.ToolStripItem>();

					mToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
					mToolStripSeparator.Name = "toolStripSeparator";
					mToolStripSeparator.Size = new System.Drawing.Size(6, 23);
					mToolStripSeparator.Tag = null;
					this.mListToolStripItem.Add(mToolStripSeparator);

					PTZControlItemCollection pc = new PTZControlItemCollection();
					int i = 0;
					foreach (PTZControlItem pi in pc)
					{
						System.Windows.Forms.ToolStripButton tb = new System.Windows.Forms.ToolStripButton();
						tb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
						tb.Image = pi.Image;
						tb.ImageTransparentColor = System.Drawing.Color.Magenta;
						tb.Name = string.Format("toolStripButtonPTZ{0}", i);
						tb.Size = new System.Drawing.Size(23, 20);
						tb.Text = pi.Caption;
						tb.Tag = pi;
						tb.Click += new System.EventHandler(this.PTZToolStripButton_Click);
						this.mListToolStripItem.Add(tb);

						i++;
					}
				}
				return this.mListToolStripItem;
			}
		}

		public IPlugInPTZ PlugIn
		{
			get
			{
				return this.mPTZ;
			}
			set
			{
				bool p = false;
				bool t = false;
				bool z = false;

				this.mPTZ = value;
				if (this.mPTZ != null)
				{
					p = (this.mPTZ.P != null);
					t = (this.mPTZ.T != null);
					z = (this.mPTZ.Z != null);
				}
				foreach (ToolStripItem ti in this.ToolStripItemCollection)
				{
					PTZControlItem pi = ti.Tag as PTZControlItem;
					if (pi != null)
					{
						switch (pi.Group)
						{
							case PTZControlItemGroup.P:
								ti.Visible = p;
								ti.Enabled = p;
								break;
							case PTZControlItemGroup.T:
								ti.Visible = t;
								ti.Enabled = t;
								break;
							case PTZControlItemGroup.Z:
								ti.Visible = z;
								ti.Enabled = z;
								break;
						}
					}
				}
				this.mToolStripSeparator.Visible = this.Visible;
			}
		}

		public bool Mirror
		{
			get { return this.mMirror; }
			set { this.mMirror = value; }
		}

		public bool Flip
		{
			get { return this.mFlip; }
			set { this.mFlip = value; }
		}

		private bool Visible
		{
			get
			{
				foreach (ToolStripItem ti in this.ToolStripItemCollection)
				{
					ToolStripButton tb = ti as ToolStripButton;
					if (tb != null)
					{
						if (tb.Visible)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		private void PTZToolStripButton_Click(object sender, EventArgs e)
		{
			ToolStripButton tb = sender as ToolStripButton;
			if (this.PlugIn == null || tb == null)
			{
				return;
			}
			PTZControlItem pi = tb.Tag as PTZControlItem;
			if (pi == null)
			{
				return;
			}
			PTZAction pa = null;
			int v = 1;
			switch (pi.Group)
			{
				case PTZControlItemGroup.P:
					pa = this.PlugIn.P;
					v = (int)(pi.Direction);
					if (this.Mirror)
					{
						v = 0 - v;
					}
					break;
				case PTZControlItemGroup.T:
					pa = this.PlugIn.T;
					v = (int)(pi.Direction);
					if (this.Flip)
					{
						v = 0 - v;
					}
					break;
				case PTZControlItemGroup.Z:
					pa = this.PlugIn.Z;
					v = (int)(pi.Direction);
					break;
				default:
					v = 1;
					break;
			}
			if (pa == null)
			{
				return;
			}
			this.mParent.Cursor = Cursors.WaitCursor;
			switch (pi.Move)
			{
				case PTZControlItemMoveAction.STEP:
					pa.Step(v);
					break;
				case PTZControlItemMoveAction.NEXT:
					pa.Next(v);
					break;
			}
			this.mParent.Cursor = Cursors.Default;
		}
	}
}
