using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Motion.Core;

namespace ZForge.Motion.Controls
{
	public class ExtendedLogViewerItem
	{
		private ZForge.Controls.Logs.LogLevel mLogLevel;
		private string mCameraID;
		private string mCameraName;
		private string mMsg;
		private DateTime mLogTime;

		public ExtendedLogViewerItem(ZForge.Controls.Logs.LogLevel level, CameraView view, string msg)
		{
			this.mLogLevel = level;
			this.mCameraID = view.CameraClass.ID;
			this.mCameraName = view.CameraClass.Name;
			this.mMsg = msg;
			this.mLogTime = DateTime.Now.ToLocalTime();
		}

		public ZForge.Controls.Logs.LogLevel LogLevel
		{
			get { return this.mLogLevel; }
		}

		public string ID
		{
			get { return this.mCameraID; }
		}

		public string Name
		{
			get { return this.mCameraName; }
		}

		public string Message
		{
			get { return this.mMsg; }
		}

		public DateTime TimeStamp
		{
			get { return this.mLogTime; }
		}
	}
}
