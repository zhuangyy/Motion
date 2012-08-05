using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.PlugIns
{
	public interface IPlugInVideoSource
	{
		string LabelText { get; }
		AForge.Video.IVideoSource VideoSource { get; }
	}
}
