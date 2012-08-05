using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.Net
{
	public delegate void GotContentLengthEventHandler(object sender, GotContentLengthEventArgs e);

	public class GotContentLengthEventArgs : EventArgs
	{
		private long mLength;

		public GotContentLengthEventArgs(Int64 size)
		{
			this.mLength = size;
		}

		public long Length
		{
			get { return this.mLength; }
		}
	}

}
