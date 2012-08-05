using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Controls.Logs;
using ZForge.Configuration;

namespace ZForge.Motion.PlugIns
{
	public interface IPlugIn : IConfigurable
	{
		string Name { get;}
		string Description { get;}
		string Author { get;}
		string Version { get;}
		string ID { get; }

		bool Enabled { get; }

		void Release();
		void Dispose();

		bool ValidCheck(List<string> msgs);

		List<ZForge.Controls.Logs.ChangeLogItem> ChangeLogList { get; }
	}

}
