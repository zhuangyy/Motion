using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace Motion.PlugIns.Alarm.EMail
{
	class SmtpMailSender
	{
		private System.Net.Mail.SmtpClient mClient = null;

		public SmtpMailSender()
		{
			mClient = new SmtpClient();
			mClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			mClient.Port = 25;
		}

		public System.Net.Mail.SmtpClient Client
		{
			get { return this.mClient; }
		}

		public void Send(System.Net.Mail.MailMessage m)
		{
			object userState = m;
			//mClient.Send(this.MailMessage);
			mClient.SendAsync(m, userState);
		}

		public void Cancel()
		{
			mClient.SendAsyncCancel();
		}
	}
}
