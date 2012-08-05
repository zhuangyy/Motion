using System;
using System.Collections.Generic;
using System.Text;
using ZForge.SA.Komponent;
using ZForge.Motion.Util;
using ZForge.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ZForge.Motion.Komponent
{
	public class MotionKomponent : SAKomponent
	{
		private SAContextMenu mContextMenu = null;

		public MotionKomponent()
		{
		}

		internal SAContextMenu ContextMenu
		{
			get
			{
				if (this.mContextMenu == null)
				{
					this.mContextMenu = new SAContextMenu(this, true);
				}
				return this.mContextMenu;
			}
		}

		#region ISAApplication Members

		public override System.Windows.Forms.Form MainForm
		{
			get
			{
				return null;
			}
		}

		#endregion

		#region IKomponent Members

		public override System.Drawing.Image Image16
		{
			get { return global::ZForge.Motion.Komponent.Properties.Resources.videocamera_16; }
		}

		public override System.Drawing.Image Image24
		{
			get { return global::ZForge.Motion.Komponent.Properties.Resources.videocamera_24; }
		}

		public override System.Windows.Forms.ToolStripMenuItem ContextMenuItem
		{
			get { return this.ContextMenu.ContextMenuItem; }
		}

		#endregion

		public override SAPreference Preference
		{
			get { return MotionPreference.Instance; }
		}

		public override string Executable
		{
			get { return "Motion.exe"; }
		}

	}
}
