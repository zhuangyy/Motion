using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.SA.Komponent
{
	public interface IKomponentAccessorial
	{
		event KomponentNotifyTipEventHandler KomponentNotifyTip;
		event KomponentOffEventHandler KomponentOff;
	}
}
