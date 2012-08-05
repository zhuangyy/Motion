using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Core
{
	public class DateTimeEx
	{
		private DateTime m_datetime;

		public DateTimeEx()
		{
			m_datetime = DateTime.Now;
		}

		public DateTimeEx(long timestamp)
		{
			this.TimeStamp = timestamp;
		}

		public DateTime DateTime
		{
			get
			{
				return m_datetime;
			}
			set
			{
				m_datetime = value;
			}
		}

		public long TimeStamp
		{
			get
			{
				DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0);
				TimeSpan span = this.DateTime.ToUniversalTime() - d;
				return (long)span.TotalSeconds;
			}
			set
			{
				System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
				this.DateTime = dateTime.AddSeconds(value).ToLocalTime();
			}
		}
	}
}
