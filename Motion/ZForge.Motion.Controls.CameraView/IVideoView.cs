using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZForge.Motion.Controls
{
	public interface IVideoView : IView
	{
		Motion.Core.CameraClass CameraClass { get; set; }

		bool IsRunning();
		void Start();
		void Stop();
		void Pause(bool on);
	}
}
