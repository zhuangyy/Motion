using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using ZForge.Globalization;

namespace ZForge.SA.Komponent
{
	public class SAApplication : WindowsFormsApplicationBase
	{
		private delegate void ArgumentCallback(string[] args);
		private ISAApplication mSAapplication;

		/// <summary>
		/// We inherit from the VB.NET WindowsFormApplicationBase class, which has the 
		/// single-instance functionality.
		/// </summary>
		public SAApplication(ISAApplication sa)
		{
			this.mSAapplication = sa;

			// Make this a single-instance application
			this.IsSingleInstance = true;
			this.EnableVisualStyles = true;
			
			// There are some other things available in the VB application model, for
			// instance the shutdown style:
			//this.ShutdownStyle = Microsoft.VisualBasic.ApplicationServices.ShutdownMode.AfterMainFormCloses;

			// Add StartupNextInstance handler
			this.StartupNextInstance += new StartupNextInstanceEventHandler(this.SAApplication_StartupNextInstance);
		}

		/// <summary>
		/// We are responsible for creating the application's main form in this override.
		/// </summary>
		protected override void OnCreateMainForm()
		{
			// Create an instance of the main form and set it in the application; 
			// but don't try to run it.
			try
			{
				this.MainForm = this.mSAapplication.MainForm;

				// We want to pass along the command-line arguments to this first instance
				// Allocate room in our string array
				IAcceptArgument ia = this.MainForm as IAcceptArgument;
				if (ia != null && this.CommandLineArgs.Count > 0)
				{
					string[] args = new string[this.CommandLineArgs.Count];
					this.CommandLineArgs.CopyTo(args, 0);
					ia.Argument(args);
				}
			}
			catch (Exception e)
			{
				string ms = Translator.Instance.T("应用程序初始化失败. 无法继续!");
				ms += "\n\n";
				ms += e.Message;
				//ms += "\n";
				//ms += e.StackTrace;
				MessageBox.Show(ms, this.mSAapplication.FullName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// This is called for additional instances. The application model will call this 
		/// function, and terminate the additional instance when this returns.
		/// </summary>
		/// <param name="eventArgs"></param>
		protected void SAApplication_StartupNextInstance(object sender,
				StartupNextInstanceEventArgs eventArgs)
		{
			if (this.MainForm != null)
			{
				this.MainForm.Visible = true;
				this.MainForm.Activate();

				IAcceptArgument ia = this.MainForm as IAcceptArgument;
				if (ia != null && eventArgs.CommandLine.Count > 0)
				{
					// Copy the arguments to a string array
					string[] args = new string[eventArgs.CommandLine.Count];
					eventArgs.CommandLine.CopyTo(args, 0);

					// Create an argument array for the Invoke method
					object[] parameters = new object[] { args };

					// Need to use invoke to b/c this is being called from another thread.
					this.MainForm.Invoke(new ArgumentCallback(ia.Argument), parameters);
				}
			}
		}
	}
}
