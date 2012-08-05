using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using ZForge.Globalization;

namespace ZForge.SA.Komponent
{
	public class SACheckUpdate
	{
		public enum CheckUpdateStatus { FAILED, NEW, NOUPDATE };
		private List<string> mURL = new List<string>();
		private Dictionary<string, string> mValue = new Dictionary<string,string>();
		private string mMsg;
		private bool mQuiet = false;
		private CheckUpdateStatus mStatus = CheckUpdateStatus.NOUPDATE;

		#region Properties

		public List<string> URLs
		{
			get { return this.mURL; }
		}

		public string Message
		{
			get { return this.mMsg; }
			set { this.mMsg = value; }
		}

		public bool Quiet
		{
			get { return this.mQuiet; }
			set { this.mQuiet = value; }
		}

		public Dictionary<string, string> Items
		{
			get { return this.mValue; }
		}

		public CheckUpdateStatus Status
		{
			get { return mStatus; }
		}
		#endregion

		public void CheckInBackground()
		{
			Thread t = new Thread(new ThreadStart(Check));
			t.Start();
		}

		public void Check()
		{
			this.mStatus = CheckUpdateStatus.FAILED;
			foreach (string URL in this.URLs)
			{
				try
				{
					WebClient o = new WebClient();

					foreach (KeyValuePair<string, string> kvp in this.Items)
					{
						o.QueryString.Add(kvp.Key, kvp.Value);
					}
					this.Message = o.DownloadString(URL);
					if (this.Message.Length == 0)
					{
						this.Message = Translator.Instance.T("网络通讯错误!");
						this.mStatus = CheckUpdateStatus.FAILED;
					}
					else if (this.Message.StartsWith("400"))
					{
						this.Message = this.Message.Substring(3).Trim();
						this.mStatus = CheckUpdateStatus.FAILED;
					}
					else if (this.Message.StartsWith("200"))
					{
						this.Message = this.Message.Substring(3).Trim();
						this.mStatus = CheckUpdateStatus.NOUPDATE;
					}
					else
					{
						this.mStatus = CheckUpdateStatus.NEW;
					}
					break;
				}
				catch (Exception e)
				{
					this.Message = e.Message;
					this.mStatus = CheckUpdateStatus.FAILED;
				}
			}
			if (this.mQuiet == false)
			{
				//MessageBox.Show(this.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, (this.Status == CheckUpdateStatus.FAILED) ? MessageBoxIcon.Error : MessageBoxIcon.Information);
			}
			else
			{
				if (this.Status == CheckUpdateStatus.NEW)
				{
					//MessageBox.Show(this.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		public string MAC(string p)
		{
			System.Security.Cryptography.SHA1CryptoServiceProvider x = new System.Security.Cryptography.SHA1CryptoServiceProvider();
			byte[] bs = System.Text.Encoding.UTF8.GetBytes(p);
			bs = x.ComputeHash(bs);
			System.Text.StringBuilder s = new System.Text.StringBuilder();

			int n = 0;
			foreach (byte b in bs)
			{
				if (n >= 8)
				{
					break;
				}
				s.Append(b.ToString("x2").ToUpper());
				n++;
			}
			return s.ToString();
		}
	}
}
