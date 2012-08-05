using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZForge.Motion.PlugIns
{
	public interface IPlugInUI
	{
		List<ZForge.Controls.PropertyGridEx.CustomProperty> UIPropertyItems { get; }
		List<System.Windows.Forms.ToolStripItem> UIEditFormToolStripItems { get; }
		List<System.Windows.Forms.ToolStripItem> UICameraViewToolStripItems { get; }

		bool UISetValue(PropertyValueChangedEventArgs e);
	}
}
