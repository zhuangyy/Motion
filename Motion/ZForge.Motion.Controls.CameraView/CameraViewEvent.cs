using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Controls
{
	public delegate void CameraViewStatusChangedEventHandler(object sender, CameraViewStatusChangedEventArgs e);
	public delegate void CameraViewBeforeStartEventHandler(object sender, CameraViewBeforeStartEventArgs e);
	public delegate void CameraViewAlarmCountChangedEventHandler(object sender, CameraViewAlarmCountChangedEventArgs e);
	public delegate void CameraViewCaptureStartedEventHandler(object sender, CameraViewCaptureEventArgs e);
	public delegate void CameraViewCaptureFinishedEventHandler(object sender, CameraViewCaptureEventArgs e);
	public delegate void CameraViewLogEventHandler(object sender, CameraViewLogEventArgs e);

	public class CameraViewEventArgs : EventArgs
	{
		protected CameraView v;

		public CameraViewEventArgs(CameraView v)
		{
			this.v = v;
		}

		public CameraView CameraView
		{
			get
			{
				return this.v;
			}
		}
	}

	public class CameraViewStatusChangedEventArgs : CameraViewEventArgs
	{
		protected int status;

		public CameraViewStatusChangedEventArgs(int status, CameraView v)
			: base(v)
		{
			this.status = status;
		}

		public int Status
		{
			get
			{
				return this.status;
			}
		}
	}

	public class CameraViewBeforeStartEventArgs : CameraViewStatusChangedEventArgs
	{
		protected bool cancel;

		public CameraViewBeforeStartEventArgs(int status, CameraView v)
			: base(status, v)
		{
			this.cancel = false;
		}

		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}
	}

	public class CameraViewAlarmCountChangedEventArgs : CameraViewEventArgs
	{
		private int alarmCount;

		public CameraViewAlarmCountChangedEventArgs(int c, CameraView v)
			: base(v)
		{
			this.alarmCount = c;
		}

		public int AlarmCount
		{
			get
			{
				return this.alarmCount;
			}
		}
	}

	public class CameraViewCaptureEventArgs : CameraViewEventArgs
	{
		private DateTime time;

		public CameraViewCaptureEventArgs(CameraView v)
			: base(v)
		{
			this.time = DateTime.Now;
		}

		public DateTime Time
		{
			get { return this.time; }
		}
	}

	public class CameraViewLogEventArgs : CameraViewEventArgs
	{
		private ZForge.Controls.Logs.LogLevel loglevel;
		private string msg;

		public CameraViewLogEventArgs(ZForge.Controls.Logs.LogLevel loglevel, string msg, CameraView v)
			: base(v)
		{
			this.loglevel = loglevel;
			this.msg = msg;
		}

		public string Message
		{
			get { return this.msg; }
		}

		public ZForge.Controls.Logs.LogLevel LogLevel
		{
			get { return this.loglevel; }
		}
	}
}
