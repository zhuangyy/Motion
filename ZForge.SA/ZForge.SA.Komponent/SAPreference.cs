using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ZForge.Globalization;
using System.IO;
using System.Diagnostics;

namespace ZForge.SA.Komponent
{
	public abstract class SAPreference
	{
		public virtual void UpdateUI(System.Windows.Forms.Control p)
		{
			Font ft = SystemFonts.MessageBoxFont;
			this.UpdateUI(p, ft);
		}

		protected virtual void UpdateUI(System.Windows.Forms.Control p, Font ft)
		{
			if (p is IGlobalization)
			{
				Translator.Instance.Update(p);
			}
			foreach (Control c in p.Controls)
			{
				Font old = c.Font;
				c.Font = new Font(ft.FontFamily.Name, ft.Size, old.Style);
				this.UpdateUI(c);
			}
		}

		public abstract string Version { get; }
		public abstract string ProductName { get; }
		public abstract string ProductID { get; }
		public abstract string Description { get; }

		public virtual string ProductFullName
		{
			get
			{
				return string.Format("{0} {1}", this.ProductName, this.Version);
			}
		}

		public virtual string MessageBoxCaption
		{
			get
			{
				return string.Format("{0} v{1}", this.ProductName, this.Version);
			}
		}

		public virtual int OEM
		{
			get { return 0; }
		}

    public virtual List<string> RSSFeeds
    {
      get
      {
        List<string> r = new List<string>();
        r.Add("http://www.syan.com.cn/rss.php?feed=rss2");
        return r;
      }
    }

		public virtual string Company
		{
			get
			{
				switch (this.OEM)
				{
					case 1:
						return Translator.Instance.T("江苏省电子商务证书认证中心有限责任公司");
					default:
						return Translator.Instance.T("江苏先安科技有限公司");
				}
			}
		}

		public virtual string URL
		{
			get
			{
				switch (this.OEM)
				{
					case 1:
						return "http://www.jsca.com.cn";
					default:
						return "http://www.syan.com.cn";
				}
			}
		}

		public virtual string CheckUpdateURL
		{
			get
			{
				switch (this.OEM)
				{
					case 1:
						return "http://sa.jsca.com.cn/checkupdate.php";
					default:
						return "http://sa.syan.com.cn/checkupdate.php";
				}
			}
		}

		public virtual void CheckUpdate(bool background)
		{
			if (background)
			{
				SACheckUpdate ck = new SACheckUpdate();
				//SALicense lic = new SALicense();
				//string hostid = lic.HostID;

				ck.URLs.Add(this.CheckUpdateURL);
				//ck.Items.Add("sn", hostid);
				//ck.Items.Add("mac", ck.MAC(hostid));
				ck.Items.Add("id", this.ProductID);
				ck.Items.Add("version", this.Version);
				ck.Items.Add("oem", this.OEM.ToString());
				ck.Quiet = true;
				ck.CheckInBackground();
			}
			else
			{
				FileInfo fi = new FileInfo(Application.ExecutablePath);
				string f = Path.Combine(fi.DirectoryName, "Update.exe");
				if (File.Exists(f))
				{
					Process.Start(f);
				}
			}
		}
	}
}
