using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Controls
{
	public delegate void CameraBoardAddNewHandler(object sender, CameraBoardAddNewEventArgs e);

	public class CameraBoardAddNewEventArgs : EventArgs
	{
		protected IVideoView v;

		public CameraBoardAddNewEventArgs(IVideoView v)
		{
			this.v = v;
		}

		public IVideoView VideoView
		{
			get
			{
				return this.v;
			}
		}
	}
}
