using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using ZForge.Globalization;

namespace ZForge.Controls.Update
{
	public partial class UpdateVerifier : UserControl
	{
		private bool mResult = false;
		private string mKey;

		public UpdateVerifier()
		{
			InitializeComponent();
		}

		public bool Result
		{
			get { return mResult; }
		}

		public string Key
		{
			get { return this.mKey; }
			set
			{
				if (null != value)
				{
					RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
					rsa.FromXmlString(value);
					this.mKey = value;
				}
			}
		}

		public virtual void Reset()
		{
			this.progressBar.Minimum = 0;
			this.progressBar.Maximum = 100;
			this.progressBar.Value = 0;
			this.mResult = false;
		}

		public void Verify(byte[] input)
		{
			this.Verify(input, this.Key);
		}

		public void Verify(byte[] input, string k)
		{
			this.Reset();

			UpdateVerifierArgs args = new UpdateVerifierArgs(input, k);
			this.backgroundWorker.RunWorkerAsync(args);
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			UpdateVerifierArgs args = e.Argument as UpdateVerifierArgs;
			mResult = args.Verify();
			if (mResult == false)
			{
				throw new Exception("");
			}
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
