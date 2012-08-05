using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ZForge.SA.Komponent
{
	public interface IKomponent : ISAApplication
	{
		Image Image16 { get; }
		Image Image24 { get; }

		string Description { get; }
		string Version { get; }
		string ID { get; }

		void Run(string[] args);
		void Off();

		System.Windows.Forms.ToolStripMenuItem ContextMenuItem { get; }

		SALicense License { get; }
	}
}
