using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ZForge.Globalization;
using System.Windows.Forms;
using System.Drawing;

namespace ZForge.SA.Komponent
{
	public abstract class SAKomponent: IKomponent
	{
		public abstract SAPreference Preference { get; }
		public abstract string Executable { get; }
		public abstract Image Image16 { get; }
		public abstract Image Image24 { get; }
		public abstract Form MainForm { get; }

		#region IKomponent Members

		public virtual string Version
		{
			get { return this.Preference.Version; }
		}

		public virtual string ID
		{
			get { return this.Preference.ProductID; }
		}

		public virtual string Description
		{
			get { return this.Preference.Description; }
		}

		public virtual void Run(string[] args)
		{
			FileInfo fi = new FileInfo(Application.ExecutablePath);
			string run = Path.Combine(fi.Directory.FullName, this.Executable);
			try
			{
				System.Diagnostics.Process.Start(run);
			}
			catch (Exception e)
			{
				string m = string.Format(Translator.Instance.T("Æô¶¯{0}Ê§°Ü!"), this.FullName);
				m += "\n";
				m += e.Message;
				MessageBox.Show(m, this.Preference.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public virtual void Off()
		{
		}

		public virtual System.Windows.Forms.ToolStripMenuItem ContextMenuItem
		{
			get { return null; }
		}

		public virtual SALicense License
		{
			get
			{
				SALicense r = new SALicense(this.FullName);
				return r;
			}
		}

		#endregion

		#region ISAApplication Members

		public virtual string FullName
		{
			get { return this.Preference.ProductName; }
		}

		public virtual bool IsArgumentAcceptable(string[] args)
		{
			return false;
		}

		#endregion
	}
}
