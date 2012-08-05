using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Globalization;

namespace Motion.PlugIns.Alarm.Sound
{
	enum ToneEnum
	{
		NONE = 0,
		ALARM1,
		ALARM2,
		ALARM3,
		BABECRY,
		RAIN_SMALL,
		RAIN_BIG,
		RAIN_THUNDER,
		WIND,
		BEAT1,
		BEAT2,
		KISS,
		MP3,
		MASTERWARNING1,
		MASTERWARNING2,
		WHEELS,
		RADIATION
	}

	class SoundArgs
	{
		public KeyValuePair<ToneEnum, string>[] TONES
		{
			get
			{
				KeyValuePair<ToneEnum, string>[] r = new KeyValuePair<ToneEnum, string>[] {
					new KeyValuePair<ToneEnum, string>(ToneEnum.NONE, Translator.Instance.T("无声")),
					new KeyValuePair<ToneEnum, string>(ToneEnum.ALARM1, Translator.Instance.T("报警音1 (MP3)")),
					new KeyValuePair<ToneEnum, string>(ToneEnum.ALARM2, Translator.Instance.T("报警音2 (MP3)")),
					new KeyValuePair<ToneEnum, string>(ToneEnum.ALARM3, Translator.Instance.T("报警音3 (MP3)")),
					new KeyValuePair<ToneEnum, string>(ToneEnum.BABECRY, Translator.Instance.T("婴儿哭声 (MP3)")),
					new KeyValuePair<ToneEnum, string>(ToneEnum.RAIN_SMALL, Translator.Instance.T("小雨声 (MP3)")),
					new KeyValuePair<ToneEnum, string>(ToneEnum.RAIN_BIG, Translator.Instance.T("暴雨声 (MP3)")),
					new KeyValuePair<ToneEnum, string>(ToneEnum.RAIN_THUNDER, Translator.Instance.T("雷雨声 (MP3)")),
					new KeyValuePair<ToneEnum, string>(ToneEnum.WIND, Translator.Instance.T("风声 (MP3)")),
					new KeyValuePair<ToneEnum, string>(ToneEnum.BEAT1, Translator.Instance.T("音乐1 (MP3)")),
					new KeyValuePair<ToneEnum, string>(ToneEnum.BEAT2, Translator.Instance.T("音乐2 (MP3)")),
					new KeyValuePair<ToneEnum, string>(ToneEnum.KISS, Translator.Instance.T("亲吻声 (MP3:-)")),
					new KeyValuePair<ToneEnum, string>(ToneEnum.MP3, Translator.Instance.T("自定义的MP3文件")),
				};
				return r;
			}
		}

		public string GetValue(ToneEnum k)
		{
			foreach (KeyValuePair<ToneEnum, string> d in this.TONES)
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
