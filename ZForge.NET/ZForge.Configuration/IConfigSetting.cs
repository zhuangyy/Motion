using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Configuration
{
	public interface IConfigSetting
	{
		IConfigSetting this[string path] { get; }

		string Name { get; }
		bool Exists { get; }

		string Value { get; set; }
		int intValue { get; set; }
		float floatValue { get; set; }
		bool boolValue { get; set; }

		void RemoveChildren();
		int GetChildCount(bool unique);
		IList<String> GetChildrenNames(bool unique);
	}
}
