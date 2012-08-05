using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Zip;
using System.ComponentModel;
using ZForge.Win32;
using System.Windows.Forms;
using System.IO;

namespace ZForge.Controls.Update
{
	public class ZipDefaultInstaller : IInstaller
	{
		#region IInstaller Members

		public event System.ComponentModel.ProgressChangedEventHandler ProgressChanged;

		public void Install(System.IO.Stream inputStream, string destination)
		{
			using (ZipFile zip = ZipFile.Read(inputStream))
			{
				int nCount = zip.FileCount;
				int i = 0;
				foreach (ZipEntry e in zip)
				{
					e.Extract(destination);
					i++;
					if (this.ProgressChanged != null)
					{
						ProgressChanged(this, new ProgressChangedEventArgs(i * 100 / nCount, null));
					}
				}
			}
		}

		#endregion

		public void Install(DirectoryInfo source, string destination)
		{
			DirectoryXCopyArgs args = new DirectoryXCopyArgs();
			args.Source = source;
			args.Target = new DirectoryInfo(destination);
			args.DirectoryFilters.Add("*");
			args.FileFilters.Add("*");
			args.Overwrite = true;
			args.Rollback = true;
			args.RollbackTempExtension = ".temp";
			DirectoryX.Copy(args);
		}
	}
}
