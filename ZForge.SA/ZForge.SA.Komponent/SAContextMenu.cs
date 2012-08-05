using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ZForge.Globalization;

namespace ZForge.SA.Komponent
{
	public class SAContextMenu
	{
		private System.Windows.Forms.ToolStripMenuItem mContextMenuItem = null;
		private System.Windows.Forms.ToolStripMenuItem mMenuItemOff = null;
		private IKomponent mKomponent;
		private bool mNoMenuItemOff = false;

		public SAContextMenu(IKomponent k)
		{
			this.mKomponent = k;
			this.mNoMenuItemOff = false;
		}

		public SAContextMenu(IKomponent k, bool b)
		{
			this.mKomponent = k;
			this.mNoMenuItemOff = b;
		}

		internal IKomponent Komponent
		{
			get { return this.mKomponent; }
		}

		public System.Windows.Forms.ToolStripMenuItem MenuItemOff
		{
			get { return this.mMenuItemOff; }
		}

		public ToolStripMenuItem ContextMenuItem
		{
			get
			{
				if (this.mContextMenuItem == null)
				{
					mContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
					mContextMenuItem.Image = this.Komponent.Image16;
					mContextMenuItem.Text = string.Format(this.Komponent.FullName);
					mContextMenuItem.Tag = this;

					System.Windows.Forms.ToolStripMenuItem tr = new ToolStripMenuItem();
					tr.Image = global::ZForge.SA.Komponent.Properties.Resources.window_new;
					tr.Text = Translator.Instance.T("启动/显示窗口");
					tr.Click += new System.EventHandler(this.toolStripMenuItemRun_Click);
					mContextMenuItem.DropDownItems.Add(tr);

					if (this.mNoMenuItemOff == false)
					{
						System.Windows.Forms.ToolStripMenuItem tx = new ToolStripMenuItem();
						tx.Image = global::ZForge.SA.Komponent.Properties.Resources.window_delete;
						tx.Text = Translator.Instance.T("退出");
						tx.Click += new System.EventHandler(this.toolStripMenuItemOff_Click);
						tx.Enabled = false;
						mContextMenuItem.DropDownItems.Add(tx);

						mMenuItemOff = tx;
					}
				}
				return this.mContextMenuItem;
			}
		}

		private void toolStripMenuItemRun_Click(object sender, EventArgs e)
		{
			this.Komponent.Run(null);
		}

		private void toolStripMenuItemOff_Click(object sender, EventArgs e)
		{
			this.Komponent.Off();
			if (this.mMenuItemOff != null)
			{
				this.mMenuItemOff.Enabled = false;
			}
		}
	}
}
