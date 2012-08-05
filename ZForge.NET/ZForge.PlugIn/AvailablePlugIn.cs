using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ZForge.PlugIn
{
	/// <summary>
	/// Data Class for Available Plugin.  Holds and instance of the loaded Plugin, as well as the Plugin's Assembly Path
	/// </summary>
	public class AvailablePlugIn<T>
	{
		//This is the actual AvailablePlugin object.. 
		//Holds an instance of the plugin to access
		//ALso holds assembly path... not really necessary
		private T mInstance;
		private string mAssemblyPath = "";

		public T Instance
		{
			get { return mInstance; }
			set { mInstance = value; }
		}

		public string AssemblyPath
		{
			get { return mAssemblyPath; }
			set { mAssemblyPath = value; }
		}
	}

}
