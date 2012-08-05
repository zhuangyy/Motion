using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Motion.Komponent;

namespace Motion
{
	public class MotionMainKomponent : MotionKomponent
	{
		private MainForm mMainForm = null;

		public override System.Windows.Forms.Form MainForm
		{
			get
			{
				if (this.mMainForm == null)
				{
					this.mMainForm = new MainForm();
				}
				return this.mMainForm;
			}
		}
	}
}
