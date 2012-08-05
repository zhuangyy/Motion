using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.Logs
{
	public class ChangeLogItem
	{
		private string mVersion;
		private ChangeLogLevel mT;
		private string mMsg;

		public ChangeLogItem(string version, ChangeLogLevel t, string msg)
		{
			this.mVersion = version;
			this.mT = t;
			this.mMsg = msg;
		}

		public string Version
		{
			get { return mVersion; }
		}

		public ChangeLogLevel T
		{
			get { return mT; }
		}

		public string Message
		{
			get { return mMsg; }
		}
	}

}
