using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.PlugIns
{
	public interface IPlugInAlarm : IPlugIn, IPlugInLog
	{
		bool IsRunning { get; }
		bool Alarm();
		bool Stop();
	}
}
