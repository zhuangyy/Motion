using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZForge.Motion.PlugIns
{
	public interface IPlugInUIWithGlobal : IPlugInUI
	{
		bool GlobalOperation { get; set; }
		bool UseGlobal { get; set; }
	}
}
