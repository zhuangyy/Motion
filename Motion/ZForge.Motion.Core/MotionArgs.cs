using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Globalization;

namespace ZForge.Motion.Core
{
	public class MotionArgs
	{		
		public KeyValuePair<CAPTUREFLAG, string>[] CAPTUREFLAGS
		{
			get
			{
				KeyValuePair<CAPTUREFLAG, string>[] r = new KeyValuePair<CAPTUREFLAG, string>[] {
					new KeyValuePair<CAPTUREFLAG, string>(CAPTUREFLAG.NOCAPTURE, Translator.Instance.T("不录像")),
					new KeyValuePair<CAPTUREFLAG, string>(CAPTUREFLAG.ONALARM, Translator.Instance.T("报警时录像")),
					new KeyValuePair<CAPTUREFLAG, string>(CAPTUREFLAG.ALWAYS, Translator.Instance.T("持续录像")),
				};
				return r;
			}
		}

		public string GetValue(CAPTUREFLAG k)
		{
			foreach (KeyValuePair<CAPTUREFLAG, string> d in this.CAPTUREFLAGS)
			{
				if (d.Key == k)
				{
					return d.Value;
				}
			}
			return null;
		}

		public KeyValuePair<DETECTMODE, string>[] DETECTMODES
		{
			get
			{
				KeyValuePair<DETECTMODE, string>[] r = new KeyValuePair<DETECTMODE, string>[] {
					new KeyValuePair<DETECTMODE, string>(DETECTMODE.MOTION, Translator.Instance.T("动作监测")),
					new KeyValuePair<DETECTMODE, string>(DETECTMODE.STILLNESS, Translator.Instance.T("静止监测")),
				};
				return r;
			}
		}

		public string GetValue(DETECTMODE k)
		{
			foreach (KeyValuePair<DETECTMODE, string> d in this.DETECTMODES)
			{
				if (d.Key == k)
				{
					return d.Value;
				}
			}
			return null;
		}

		public KeyValuePair<int, string>[] CAPTURE_ELAPSE
		{
			get
			{
				KeyValuePair<int, string>[] r = new KeyValuePair<int, string>[] {
					new KeyValuePair<int, string>(0, Translator.Instance.T("连续存储")),
					new KeyValuePair<int, string>(1, Translator.Instance.T("分时存储 (1小时)")),
					new KeyValuePair<int, string>(2, Translator.Instance.T("分时存储 (2小时)")),
					new KeyValuePair<int, string>(6, Translator.Instance.T("分时存储 (6小时)")),
					new KeyValuePair<int, string>(12, Translator.Instance.T("分时存储 (12小时)")),
					new KeyValuePair<int, string>(24, Translator.Instance.T("分时存储 (24小时)")),
				};
				return r;
			}
		}

		public string GetValue(int k)
		{
			foreach (KeyValuePair<int, string> d in this.CAPTURE_ELAPSE)
			{
				if (d.Key == k)
				{
					return d.Value;
				}
			}
			return null;
		}
	}
}
