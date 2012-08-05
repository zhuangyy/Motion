using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.Logs
{
	public enum LogFilter : byte
	{
		LOG_NONE = 0x00,
		LOG_INFO = 0x01,
		LOG_WARNING = 0x02,
		LOG_ERROR = 0x04,
		LOG_ALL = 0x07
	}
}
