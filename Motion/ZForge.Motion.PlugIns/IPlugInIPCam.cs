using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.PlugIns
{
	public interface IPlugInIPCam : IPlugIn, IPlugInVideoSource
	{
		string URL { get; set; }
		string Username { get; set; }
		string Password { get; set; }
		IPCAM Stream { get; set; }
	}

	public enum IPCAM
	{
		JPEG = 0,
		MJPEG
	}
}
