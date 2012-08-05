using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZForge.Configuration;
using ZForge.Globalization;

namespace ZForge.Controls.Update
{
	public partial class UpdateChecker : ZForge.Controls.Net.Downloader
	{
		private XMLConfig mUpdateInformation;

		public UpdateChecker()
		{
			InitializeComponent();
		}

		public string LocalVersion
		{
			get
			{
				if (this.Post.ContainsKey("version"))
				{
					return this.Post["version"].ToString();
				}
				return null;
			}
			set
			{
				if (this.Post.ContainsKey("version"))
				{
					this.Post.Remove("version");
				}
				this.Post.Add("version", value);
			}
		}

		public string ProductID
		{
			get
			{
				if (this.Post != null && this.Post.ContainsKey("pid"))
				{
					return this.Post["pid"].ToString();
				}
				return null;
			}
			set
			{
				if (this.Post != null && this.Post.ContainsKey("pid"))
				{
					this.Post.Remove("pid");
				}
				this.Post.Add("pid", value);
			}
		}

		public string UserID
		{
			set
			{
				if (this.Post.ContainsKey("uid"))
				{
					this.Post.Remove("uid");
				}
				this.Post.Add("uid", value);
			}
		}

		public string CurrentVersion
		{
			get
			{
				if (mUpdateInformation != null)
				{
					return mUpdateInformation.Settings["version"].Value;
				}
				return null;
			}
		}

		public string Message
		{
			get
			{
				if (mUpdateInformation != null)
				{
					return mUpdateInformation.Settings["message"].Value;
				}
				return null;
			}
		}

		public List<string> AdditionalKomponentIDCollection
		{
			get
			{
				if (mUpdateInformation != null && mUpdateInformation.Settings["newk"] != null)
				{
					List<string> r = new List<string>();
					XMLConfigSetting s = mUpdateInformation.Settings["newk"] as XMLConfigSetting;
					foreach (IConfigSetting x in s.GetNamedChildren("item"))
					{
						r.Add(x["id"].Value);
					}
					return r;
				}
				return null;
			}
		}

		public int CompareVersion(string v1, string v2)
		{
			if (v2 == null)
			{
				return -1;
			}
			if (v1 == null)
			{
				return 1;
			}
			string[] lvs = v1.Split(new char[] { '.' });
			string[] rvs = v2.Split(new char[] { '.' });
			int c = Math.Min(lvs.Length, rvs.Length);
			for (int n = 0; n < c; n++)
			{
				try
				{
					int lv = Convert.ToInt32(lvs[n]);
					int rv = Convert.ToInt32(rvs[n]);
					if (lv > rv) return -1;
					if (lv < rv) return 1;
				}
				catch (Exception)
				{
				}
			}
			return (rvs.Length - lvs.Length);
		}

		public int CompareVersion()
		{
			return this.CompareVersion(this.LocalVersion, this.CurrentVersion);
		}

		protected override void OnDoWork()
		{
			this.Output = new MemoryStream();
			base.OnDoWork();

			try
			{
				MemoryStream ms = this.Output as MemoryStream;
				string v = System.Text.Encoding.UTF8.GetString(ms.ToArray());
				mUpdateInformation = new XMLConfig(v);
			}
			catch (Exception)
			{
				throw new Exception(Translator.Instance.T("服务器返回的数据有误."));
			}
		}

	}
}
