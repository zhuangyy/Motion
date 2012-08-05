using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace ZForge.PlugIn
{
	public class PlugIns<T>
	{
		private AvailablePlugInCollection<T> mAvailablePlugins = new AvailablePlugInCollection<T>();

		protected PlugIns()
		{
		}

		public PlugIns(string Path, string pattern)
		{
			this.Load(Path, pattern);
		}

		/// <summary>
		/// A Collection of all Plugins Found and Loaded by the FindPlugins() Method
		/// </summary>
		public AvailablePlugInCollection<T> AvailablePlugInCollection
		{
			get { return mAvailablePlugins; }
		}

		/// <summary>
		/// Searches the passed Path for Plugins
		/// </summary>
		/// <param name="Path">Directory to search for Plugins in</param>
		public void Load(string Path, string pattern)
		{
			//First empty the collection, we're reloading them all
			mAvailablePlugins.Clear();

			try
			{
				//Go through all the files in the plugin directory
				foreach (string fileOn in Directory.GetFiles(Path, pattern))
				{
					FileInfo file = new FileInfo(fileOn);

					//Preliminary check, must be .dll
					if (file.Extension.Equals(".dll"))
					{
						//Add the 'plugin'
						this.AddPlugin(fileOn);
					}
				}
			}
			catch (Exception ex)
			{
				System.Reflection.ReflectionTypeLoadException e = ex as System.Reflection.ReflectionTypeLoadException;
				if (e != null)
				{
					foreach (Exception ee in e.LoaderExceptions)
					{
						Console.WriteLine(ee.Message);
					}
				}
			}
		}

		/// <summary>
		/// Unloads and Closes all AvailablePlugins
		/// </summary>
		public void Clear()
		{
			foreach (AvailablePlugIn<T> pluginOn in mAvailablePlugins)
			{
				//Close all plugin instances
				//We call the plugins Dispose sub first incase it has to do 
				//Its own cleanup stuff
				IDisposable d = pluginOn.Instance as IDisposable;
				if (d != null)
				{
					d.Dispose();
				}

				//After we give the plugin a chance to tidy up, get rid of it
				pluginOn.Instance = default(T);
			}

			//Finally, clear our collection of available plugins
			mAvailablePlugins.Clear();
		}

		private void AddPlugin(string FileName)
		{
			//Create a new assembly from the plugin file we're adding..
			Assembly pluginAssembly = Assembly.LoadFrom(FileName);
			string tn = typeof(T).FullName;

			//Next we'll loop through all the Types found in the assembly
			foreach (Type pluginType in pluginAssembly.GetTypes())
			{
				if (pluginType.IsPublic) //Only look at public types
				{
					if (!pluginType.IsAbstract)  //Only look at non-abstract types
					{
						//Gets a type object of the interface we need the plugins to match
						Type typeInterface = pluginType.GetInterface(tn, true);

						//Make sure the interface we want to use actually exists
						if (typeInterface != null)
						{
							//Create a new available plugin since the type implements the IPlugin interface
							AvailablePlugIn<T> newPlugin = new AvailablePlugIn<T>();

							//Set the filename where we found it
							newPlugin.AssemblyPath = FileName;

							//Create a new instance and store the instance in the collection for later use
							//We could change this later on to not load an instance.. we have 2 options
							//1- Make one instance, and use it whenever we need it.. it's always there
							//2- Don't make an instance, and instead make an instance whenever we use it, then close it
							//For now we'll just make an instance of all the plugins
							newPlugin.Instance = (T)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));

							//Add the new plugin to our collection here
							this.mAvailablePlugins.Add(newPlugin);

							//cleanup a bit
							newPlugin = null;
						}

						typeInterface = null; //Mr. Clean			
					}
				}
			}

			pluginAssembly = null; //more cleanup
		}
	}
}
