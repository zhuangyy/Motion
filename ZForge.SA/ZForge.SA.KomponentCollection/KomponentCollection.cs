using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Globalization;
using System.IO;
using ZForge.PlugIn;
using ZForge.SA.Komponent;
using System.Windows.Forms;

namespace ZForge.SA.KomponentCollection
{
	public sealed class KomponentCollection : List<IKomponent>
	{
		private static readonly KomponentCollection mInstance = new KomponentCollection();

		private KomponentCollection()
		{
			FileInfo fi = new FileInfo(Application.ExecutablePath);

			ZForge.PlugIn.PlugIns<IKomponent> plugs = new ZForge.PlugIn.PlugIns<IKomponent>(fi.DirectoryName, "*.Komponent.dll");
			foreach (AvailablePlugIn<IKomponent> p in plugs.AvailablePlugInCollection)
			{
				this.Add(p.Instance);
			}
		}

		public static KomponentCollection Instance
		{
			get { return mInstance; }
		}

		public List<SALicense> LicenseCollection
		{
			get
			{
				List<SALicense> r = new List<SALicense>();
				foreach (IKomponent ik in this)
				{
					SALicense l = ik.License;
					if (l != null)
					{
						r.Add(l);
					}
				}
				return r;
			}
		}
	}
}
