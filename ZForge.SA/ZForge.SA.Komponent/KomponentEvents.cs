using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZForge.SA.Komponent
{
	public delegate void KomponentNotifyTipEventHandler(object sender, KomponentNotifyTipEventArgs e);
	public delegate void KomponentOffEventHandler(object sender, EventArgs e);

	public class KomponentNotifyTipEventArgs : EventArgs
	{
		private string mTipTitle;
		private string mTipText;
		private ToolTipIcon mTipIcon;

		public KomponentNotifyTipEventArgs(string tipTitle, string tipText, ToolTipIcon tipIcon)
		{
			this.mTipIcon = tipIcon;
			this.mTipText = tipText;
			this.mTipTitle = tipTitle;
		}

		public string TipTitle
		{
			get { return this.mTipTitle; }
		}

		public string TipText
		{
			get { return this.mTipText; }
		}

		public ToolTipIcon TipIcon
		{
			get { return this.mTipIcon; }
		}
	}
}
