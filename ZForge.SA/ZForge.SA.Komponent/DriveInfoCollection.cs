using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace ZForge.SA.Komponent
{
	internal class DriveInfoCollection : List<string>
	{
		public DriveInfoCollection()
		{
			string sn;

			try
			{
				ManagementObjectSearcher searcher;
				searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

				foreach (ManagementObject o in searcher.Get())
				{
					// get the hardware serial no.
					if (o["SerialNumber"] != null)
					{
						sn = o["SerialNumber"].ToString().Trim();
						this.Add(sn);
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
