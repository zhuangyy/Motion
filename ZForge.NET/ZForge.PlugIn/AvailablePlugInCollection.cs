using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.PlugIn
{
	public class AvailablePlugInCollection<T> : System.Collections.CollectionBase
	{
		//A Simple Home-brew class to hold some info about our Available Plugins

		/// <summary>
		/// Add a Plugin to the collection of Available plugins
		/// </summary>
		/// <param name="pluginToAdd">The Plugin to Add</param>
		public void Add(AvailablePlugIn<T> p)
		{
			this.List.Add(p);
		}

		/// <summary>
		/// Remove a Plugin to the collection of Available plugins
		/// </summary>
		/// <param name="pluginToRemove">The Plugin to Remove</param>
		public void Remove(AvailablePlugIn<T> p)
		{
			this.List.Remove(p);
		}

		/// <summary>
		/// Finds a plugin in the available Plugins
		/// </summary>
		/// <param name="pluginNameOrPath">The name or File path of the plugin to find</param>
		/// <returns>Available Plugin, or null if the plugin is not found</returns>
		public AvailablePlugIn<T> Find(string pluginNameOrPath)
		{
			AvailablePlugIn<T> r = null;

			//Loop through all the plugins
			foreach (AvailablePlugIn<T> pluginOn in this.List)
			{
				//Find the one with the matching name or filename
				if (pluginOn.AssemblyPath.Equals(pluginNameOrPath) || pluginOn.AssemblyPath.EndsWith(pluginNameOrPath))
				{
					r = pluginOn;
					break;
				}
			}
			return r;
		}
	}

}
