using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ZForge.Motion.Util;
using ZForge.SA.Komponent;

namespace Motion
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			SAApplication app = new SAApplication(new MotionMainKomponent());
			try
			{
				app.Run(args);
			}
			catch (Microsoft.VisualBasic.ApplicationServices.NoStartupFormException)
			{
			}
		}
	}
}