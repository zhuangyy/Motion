using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace ZForge.Controls.Update
{
	public interface IInstaller
	{
		event ProgressChangedEventHandler ProgressChanged;

		void Install(Stream inputStream, string destinaton);
	}
}
