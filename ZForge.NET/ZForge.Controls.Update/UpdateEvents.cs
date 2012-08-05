using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.Update
{
	public delegate void UpdateStepChangedEventHandler(object sender, UpdateStepChangedEventArgs e);

	public class UpdateStepChangedEventArgs : EventArgs
	{
		private UpdateSteps mStep;

		public UpdateStepChangedEventArgs(UpdateSteps step)
		{
			this.mStep = step;
		}

		public UpdateSteps Step
		{
			get { return this.mStep; }
		}
	}
}
