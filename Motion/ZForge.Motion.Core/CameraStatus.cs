using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Core
{
	public class CameraStatus
	{
		private int status;
		public const int STARTED = 0x01;
		public const int STOPPED = 0x02;
		public const int PAUSED = 0x04;

		public CameraStatus(int status)
		{
			this.status = status;
		}

		#region Properties
		public int Value
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}
		#endregion

		public void SetStatus(int s, bool set)
		{
			if (set)
			{
				this.Value = this.Value | s;
			}
			else
			{
				this.Value = this.Value & (~s);
			}
		}

		public bool IsStatusSet(int s)
		{
			return ((this.Value & s) != 0) ? true : false;
		}

		public static bool IsStatusSet(int status, int s)
		{
			return ((status & s) != 0) ? true : false;
		}
	}
}
