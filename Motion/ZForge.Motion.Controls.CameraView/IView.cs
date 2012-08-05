using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZForge.Motion.Controls
{
	public interface IView
	{
		Motion.Core.CameraClass Owner { get; }
		float ViewRatio { get; set; }
		UserControl Me { get; }
		string Title { get; }
		string ID { get; }

		bool Remove();
	}
}
