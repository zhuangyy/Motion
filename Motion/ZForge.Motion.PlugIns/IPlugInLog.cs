using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.PlugIns
{
	public interface IPlugInLog
	{
		event ZForge.Controls.Logs.LogEventHandler Log;
	}
}
