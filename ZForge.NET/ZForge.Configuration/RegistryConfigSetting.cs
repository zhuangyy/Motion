using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace ZForge.Configuration
{
	public class RegistryConfigSetting : IConfigSetting, IDisposable
	{
		private RegistryKey mNode;
		private string mName;

		public RegistryConfigSetting(RegistryKey node, string name)
		{
			mNode = node;
			mName = name;
		}

		public RegistryConfigSetting(string path)
		{
			mNode = Registry.CurrentUser.OpenSubKey(path, true);
			if (mNode == null)
			{
				mNode = Registry.CurrentUser.CreateSubKey(path);
			}
		}

		#region IConfigSetting Members

		public string Name
		{
			get { return this.mName; }
		}

		public bool Exists
		{
			get { return (mNode.GetValue(this.Name) != null); }
		}

		public string Value
		{
			get
			{
				return mNode.GetValue(this.mName, "").ToString();
			}
			set
			{
				mNode.SetValue(this.mName, value);
			}
		}

		public int intValue
		{
			get { int i; int.TryParse(this.Value, out i); return i; }
			set { this.Value = value.ToString(); }
		}

		public bool boolValue
		{
			get { bool b; bool.TryParse(this.Value, out b); return b; }
			set { this.Value = value.ToString(); }
		}

		public float floatValue
		{
			get { float f; float.TryParse(this.Value, out f); return f; }
			set { this.Value = value.ToString(); }
		}

		public IConfigSetting this[string path]
		{
			get 
			{
				return new RegistryConfigSetting(this.mNode, path); 
			}
		}

		public void RemoveChildren()
		{
			IList<string> list = this.GetChildrenNames(false);
			foreach (string s in list)
			{
				mNode.DeleteValue(s);
			}
		}

		public int GetChildCount(bool unique)
		{
			return mNode.ValueCount;
		}

		public IList<string> GetChildrenNames(bool unique)
		{
			List<string> list = new List<string>();
			string[] ss = mNode.GetValueNames();
			foreach (string s in ss)
			{
				list.Add(s);
			}
			return list;
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			mNode.Close();
		}

		#endregion

	}
}
