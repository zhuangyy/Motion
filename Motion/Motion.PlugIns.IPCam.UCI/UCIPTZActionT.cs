using System;
using System.Collections.Generic;
using System.Text;

namespace Motion.PlugIns.IPCam.UCI
{
	internal class UCIPTZActionT : UCIPTZ
	{
		public UCIPTZActionT(UCIAction u)
			: base(u)
		{
		}

		protected override string GetCommand(int v)
		{
			return "t=" + v;
		}

		public override bool Auto(bool on)
		{
			return false;
		}

		public override bool CanAutomate
		{
			get { return false; }
		}
	}
}
