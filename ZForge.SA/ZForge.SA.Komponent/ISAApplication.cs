using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZForge.SA.Komponent
{
	public interface ISAApplication
	{
		string FullName { get; }
		Form MainForm { get; }

		bool IsArgumentAcceptable(string[] args);
	}
}
