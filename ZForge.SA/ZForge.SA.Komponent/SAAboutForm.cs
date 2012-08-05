using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.SA.Komponent
{
	public class SAAboutForm : ZForge.Forms.AboutForm
	{
		public SAAboutForm(SAPreference pref)
		{
			this.Product = pref.ProductName;
			this.Version = pref.Version;
			this.Company = pref.Company;
			this.URL = pref.URL;

			pref.UpdateUI(this);
		}
	}
}
