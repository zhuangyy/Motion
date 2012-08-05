using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ZForge.Globalization;

namespace Motion.PlugIns
{
	public class AvailablePlugins : System.Collections.CollectionBase
	{
		//A Simple Home-brew class to hold some info about our Available Plugins

		/// <summary>
		/// Add a Plugin to the collection of Available plugins
		/// </summary>
		/// <param name="pluginToAdd">The Plugin to Add</param>
		public void Add(AvailablePlugin p)
		{
			this.List.Add(p);
		}

		/// <summary>
		/// Remove a Plugin to the collection of Available plugins
		/// </summary>
		/// <param name="pluginToRemove">The Plugin to Remove</param>
		public void Remove(AvailablePlugin p)
		{
			this.List.Remove(p);
		}

		/// <summary>
		/// Finds a plugin in the available Plugins
		/// </summary>
		/// <param name="pluginNameOrPath">The name or File path of the plugin to find</param>
		/// <returns>Available Plugin, or null if the plugin is not found</returns>
		public AvailablePlugin Find(string pluginNameOrPath)
		{
			AvailablePlugin r = null;

			//Loop through all the plugins
			foreach (AvailablePlugin pluginOn in this.List)
			{
				//Find the one with the matching name or filename
				if ((pluginOn.Instance.Name.Equals(pluginNameOrPath)) || pluginOn.AssemblyPath.Equals(pluginNameOrPath))
				{
					r = pluginOn;
					break;
				}
			}
			return r;
		}
	}

	/// <summary>
	/// Data Class for Available Plugin.  Holds and instance of the loaded Plugin, as well as the Plugin's Assembly Path
	/// </summary>
	public class AvailablePlugin
	{
		//This is the actual AvailablePlugin object.. 
		//Holds an instance of the plugin to access
		//ALso holds assembly path... not really necessary
		private IPlugIn mInstance = null;
		private string mAssemblyPath = "";

		public IPlugIn Instance
		{
			get { return mInstance; }
			set { mInstance = value; }
		}

		public string AssemblyPath
		{
			get { return mAssemblyPath; }
			set { mAssemblyPath = value; }
		}

		public string Category
		{
			get
			{
				string r = "";
				if (this.Instance is IPlugInAlarm)
				{
					r += Translator.Instance.T("[报警]"); 
				}
				if (this.Instance is IPlugInVideoSource)
				{
					r += Translator.Instance.T("[视频来源]");
				}
				if (r.Length == 0) {
					r = Translator.Instance.T("[未知]");
				}
				return r.Trim();
			}
		}
	}
}
