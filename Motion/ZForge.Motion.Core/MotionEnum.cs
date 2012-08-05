using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Core
{

	public enum CAPTUREFLAG : int
	{
		NOCAPTURE = 0,
		ONALARM,
		ALWAYS
	}

	public enum RecordMark : int
	{
		INVALID = 0,
		UNREAD,
		READ,
		IMPORTANT
	}

	public enum DETECTMODE : int
	{
		MOTION = 0,
		STILLNESS
	}

	public static class ValueRange
	{
		public static int AlarmElapseMin = 5;
		public static int AlarmElapseMax = 30;
		public static int AlarmElapseDefault = 5;

		public static int SensibilityMin = 1;
		public static int SensibilityMax = 200;
		public static int SensibilityDefault = 15;

		public static int DifferenceThresholdMin = 1;
		public static int DifferenceThresholdMax = 255;
		public static int DifferenceThresholdDefault = 15;
	}
}
