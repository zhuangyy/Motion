using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.PlugIns
{
	public interface IPlugInUSBCam : IPlugIn, IPlugInVideoSource
	{
		ZForge.Win32.DirectShow.Filter Filter { get; }
	}
}
