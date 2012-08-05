using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Motion.Core;

namespace ZForge.Motion.Controls
{
	public delegate void CameraTreeOnBoardEventHandler(object sender, CameraTreeEventArgs e);
	public delegate void CameraTreeBeforeDeleteEventHandler(object sender, CameraTreeNodeDeleteEventArgs e);
	public delegate void CameraTreeAfterDeleteEventHandler(object sender, CameraTreeEventArgs e);

	public class CameraTreeEventArgs : EventArgs
	{
		private CameraClass mItem;

		public CameraTreeEventArgs(CameraClass c)
		{
			this.mItem = c;
		}

		public CameraClass Camera
		{
			get { return this.mItem; }
		}
	}

	public class CameraTreeNodeDeleteEventArgs : CameraTreeEventArgs
	{
		private bool mCancel;

		public CameraTreeNodeDeleteEventArgs(CameraClass c)
			: base(c)
		{
			this.mCancel = false;
		}

		public bool Cancel
		{
			get { return this.mCancel; }
			set { this.mCancel = value; }
		}
	}
}
