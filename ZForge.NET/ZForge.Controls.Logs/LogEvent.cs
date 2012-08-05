using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.Logs
{
	public delegate void LogEventHandler(object sender, LogEventArgs e);

	public class LogEventArgs : EventArgs
	{
		private LogLevel mLevel;
		private string mMsg;

		public LogEventArgs(LogLevel l, string msg)
		{
			this.mLevel = l;
			this.mMsg = msg;
		}

		public LogLevel Level
		{
			get { return this.mLevel; }
		}

		public string Message
		{
			get { return this.mMsg; }
		}
	}
}
