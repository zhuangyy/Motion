using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Globalization;

namespace Motion.PlugIns.Alarm.EMail
{
	enum EMailActions
	{
		NONE = 0,
		ONALARM
	}

	class EMailArgs
	{
		public KeyValuePair<EMailActions, string>[] ACTIONS
		{
			get
			{
				KeyValuePair<EMailActions, string>[] r = new KeyValuePair<EMailActions, string>[] {
					new KeyValuePair<EMailActions, string>(EMailActions.NONE, Translator.Instance.T("不启用")),
					new KeyValuePair<EMailActions, string>(EMailActions.ONALARM, Translator.Instance.T("启用")),
				};
				return r;
			}
		}

		public string GetValue(EMailActions k)
		{
			foreach (KeyValuePair<EMailActions, string> d in this.ACTIONS)
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
