using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.Logs
{
	public delegate void LogViewerCountEventHandler(object sender, LogViewerCountChangedEventArgs e);

	public class LogViewerCountChangedEventArgs : EventArgs
	{
		private int mCountE;
		private int mCountW;
		private int mCountI;

		public LogViewerCountChangedEventArgs(int e, int w, int i)
		{
			this.mCountE = e;
			this.mCountW = w;
			this.mCountI = i;
		}

		public int E
		{
			get
			{
				return this.mCountE;
			}
		}

		public int W
		{
			get
			{
				return this.mCountW;
			}
		}

		public int I
		{
			get
			{
				return this.mCountI;
			}
		}
	}
}
