using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.CollapsibleSplitter
{
	/// <summary>
	/// A simple designer class for the CollapsibleSplitter control to remove 
	/// unwanted properties at design time.
	/// </summary>
	public class CollapsibleSplitterDesigner : System.Windows.Forms.Design.ControlDesigner
	{
		public CollapsibleSplitterDesigner() 
		{
		}

		protected override void PreFilterProperties(System.Collections.IDictionary properties)
		{
			properties.Remove("IsCollapsed");
			properties.Remove("BorderStyle");
			properties.Remove("Size");
		}
	}
}
