using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.Logs
{
	public class LogItem
	{
		private LogLevel mLogLevel;
		private string mMsg;
		private DateTime mLogTime;
		private object mTag;

		public LogItem(LogLevel level, string msg)
		{
			this.mLogLevel = level;
			this.mMsg = msg;
			this.mLogTime = DateTime.Now.ToLocalTime();
		}

		public string Message
		{
			get { return this.mMsg; }
		}

		public LogLevel Level
		{
			get { return this.mLogLevel; }
		}

		public DateTime Timestamp
		{
			get { return this.mLogTime; }
		}

		public object Tag
		{
			get { return this.mTag; }
			set { this.mTag = value; }
		}
	}
}
