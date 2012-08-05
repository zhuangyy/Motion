using System;
using System.Collections.Generic;
using System.Text;

namespace Motion.PlugIns.IPCam.UCI
{
	internal class UCIPTZActionP : UCIPTZ
	{
		public UCIPTZActionP(UCIAction u)
			: base(u)
		{
		}

		protected override string GetCommand(int v)
		{
			return "p=" + v;
		}

		public override bool Auto(bool on)
		{
			int v = (on) ? 1 : -1;
			string u = this.URL + "&pauto=" + v;
			return this.Run(u);
		}

		public override bool CanAutomate
		{
			get { return true; }
		}
	}
}
