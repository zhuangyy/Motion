using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Configuration
{
	public interface IConfigurable
	{
		void SaveConfig(IConfigSetting s);
		void LoadConfig(IConfigSetting s);
	}
}
