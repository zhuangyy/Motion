using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using ZForge.Configuration;
using ZForge.Globalization;
using ZForge.SA.Komponent;

namespace ZForge.Motion.Util
{
	public sealed class MotionPreference : SAPreference
	{
		private static readonly MotionPreference mInstance = new MotionPreference();

		private MotionPreference()
			: base()
		{
		}

		public static MotionPreference Instance
		{
			get { return mInstance; }
		}

		public override void UpdateUI(System.Windows.Forms.Control p)
		{
			Font ft = this.Font;
			this.UpdateUI(p, ft);
		}

		public string LangPath
		{
			get
			{
				string e = Application.ExecutablePath;
				FileInfo ei = new FileInfo(e);
				return ei.Directory + @"\Lang";
			}
		}

		private Font Font
		{
			get
			{
				return new System.Drawing.Font(MotionConfiguration.Instance.FontName, MotionConfiguration.Instance.FontSize);
			}
		}

    public override List<string> RSSFeeds
    {
      get
      {
        List<string> r = base.RSSFeeds;
        r.Add("http://alexxjoe.3322.org:8080/wp/?cat=3&feed=rss2");
        return r;
      }
    }
		public override string Version
		{
			get { return "2.0.3"; }
		}

		public override string ProductName
		{
			get { return Translator.Instance.T("Motion Detector 视频监控"); }
		}

		public override string ProductID
		{
			get { return "4949aad32d01d28d0dbfaa0b922bdb8a"; }
		}
		
		public override string Description
		{
			get
			{
				string r = Translator.Instance.T("{0}是{1}开发的视频监控应用程序, 主要用于本地或者远程的视频监控, 支持各类数字摄像头，包括普通电脑上使用的USB摄像头，或者网络IP摄像头. 包含移动侦测, 静止侦测等先进技术, 支持声音, 邮件等多种报警模式.");
#if SAFE_ANYWHERE_WITHUK
				r += "\n";
				r += Translator.Instance.T("通过固化在硬件USB Key中, {0}无需安装. 具有安全性高, 移动性强和可靠性好等特点. 您可以随时在任一计算机上连接远程摄像头.");
#endif
				return string.Format(r, this.ProductName, this.Company);
			}
		}
	}
}
