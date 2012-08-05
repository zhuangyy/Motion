using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZForge.Motion.PlugIns
{
	public delegate void PlugInFeedEventHandler(object sender, EventArgs e);

	public interface IPlugInFeed
	{
		event Motion.PlugIns.PlugInFeedEventHandler FeedImage;
		Bitmap Image { set; }
	}
}
