using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Motion.Sound
{
	public interface IAlarmPlayer
	{
		bool Check();
		bool IsRunning { get; }
		void AlarmStart();
		void AlarmEnd();
	}
}
