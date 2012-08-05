using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZForge.Zip;
using ZForge.Globalization;

namespace ZForge.Controls.Update
{
	public partial class UpdateInstaller : UserControl
	{
		private IInstaller mInstaller;
		private string mDestination;

		public UpdateInstaller()
		{
			InitializeComponent();
		}

		[Browsable(false)]
		public string Destination
		{
			get
			{
				return this.mDestination;
			}
			set
			{
				this.mDestination = value;
			}
		}

		[Browsable(false)]
		public IInstaller Installer
		{
			get
			{
				if (mInstaller == null)
				{
					mInstaller = new ZipDefaultInstaller();
					mInstaller.ProgressChanged += new ProgressChangedEventHandler(installer_ProgressChanged);
				}
				return mInstaller;
			}
			set
			{
				mInstaller = value;
				if (mInstaller != null)
				{
					mInstaller.ProgressChanged += new ProgressChangedEventHandler(installer_ProgressChanged);
				}
			}
		}

		public void Install(Stream inputStream)
		{
			this.Reset();
			this.backgroundWorker.RunWorkerAsync(inputStream);
		}

		public virtual void Reset()
		{
			this.progressBar.Minimum = 0;
			this.progressBar.Maximum = 100;
			this.progressBar.Value = 0;
		}

		private void installer_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.backgroundWorker.ReportProgress(e.ProgressPercentage);
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Stream inputStream = e.Argument as Stream;
			this.Installer.Install(inputStream, this.Destination);
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.progressBar.Value = Math.Min(progressBar.Maximum, e.ProgressPercentage);
		}

		public event RunWorkerCompletedEventHandler RunWorkerCompleted;

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.progressBar.Value = (e.Error == null) ? this.progressBar.Maximum : this.progressBar.Minimum;
			if (this.RunWorkerCompleted != null)
			{
				this.RunWorkerCompleted(this, e);
			}
		}
	}
}
