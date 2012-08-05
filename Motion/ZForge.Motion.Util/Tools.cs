using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Util
{
	public static class Tools
	{
		public static void PrintStackTrace()
		{
			System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace();
			for (int frameIndex = 0; frameIndex < trace.FrameCount; frameIndex++)
			{
				System.Diagnostics.StackFrame frame = trace.GetFrame(frameIndex);
				Console.WriteLine(" ".PadLeft(frameIndex) // indention
				+ frame.GetMethod().ReflectedType.FullName // method's object
				+ "." + frame.GetMethod().Name); // method name
			}
		}

	}
}
