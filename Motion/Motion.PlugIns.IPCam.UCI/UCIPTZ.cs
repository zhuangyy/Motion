using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Motion.PlugIns;
using System.Net;

namespace Motion.PlugIns.IPCam.UCI
{
	internal abstract class UCIPTZ : PTZAction
	{
		private string mURL = "";

		public UCIPTZ(UCIAction u)
		{
			this.mURL = string.Format("http://{0}:{1}/ptz.cgi?cam={2}", u.Host, u.Port, u.CamID);
			if (string.IsNullOrEmpty(u.Username) == false)
			{
				mURL += "&username=" + u.Username;
			}
			if (string.IsNullOrEmpty(u.Password) == false)
			{
				mURL += "&password=" + u.Password;
			}
		}

		public override bool Step(int v)
		{
			v = (v > 0) ? 1 : -1;
			string u = this.URL + "&" + this.GetCommand(v);
			return this.Run(u);
		}

		public override bool Next(int v)
		{
			v = (v > 0) ? 255 : -255;
			string u = this.URL + "&" + this.GetCommand(v);
			return this.Run(u);
		}

		protected bool Run(string URL) {
			WebClient o = new WebClient();
			try
			{
				o.DownloadString(URL);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		protected string URL
		{
			get { return this.mURL; }
		}

		protected abstract string GetCommand(int v);
	}
}
