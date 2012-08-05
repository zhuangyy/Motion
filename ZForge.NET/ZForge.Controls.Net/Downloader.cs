using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Web;

namespace ZForge.Controls.Net
{
	public partial class Downloader : UserControl
	{
		private string mURL;
		private Stream mOutput;
		private long mContentLength;
		private bool mResult;
		private Dictionary<string, object> mPostParameters;

		public Downloader()
		{
			InitializeComponent();
		}

		#region Properties

		public string URL
		{
			get { return this.mURL; }
			set { this.mURL = value; }
		}

		public Stream Output
		{
			get { return this.mOutput; }
			set { this.mOutput = value; }
		}

		public long ContentLength
		{
			get { return mContentLength; }
		}

		public bool Result
		{
			get
			{
				return mResult;
			}
		}

		public Dictionary<string, object> Post
		{
			get
			{
				if (mPostParameters == null)
				{
					mPostParameters = new Dictionary<string, object>();
				}
				return mPostParameters;
			}
			set
			{
				mPostParameters = value;
			}
		}

		#endregion

		#region Events
		public event GotContentLengthEventHandler GotContentLength;
		public event RunWorkerCompletedEventHandler RunWorkerCompleted;
		public event ProgressChangedEventHandler ProgressChanged;
		#endregion

		public virtual void Download()
		{
			this.Reset();
			backgroundWorker.RunWorkerAsync();
		}

		public virtual void Reset()
		{
			this.progressBar.Minimum = 0;
			this.progressBar.Maximum = 100;
			this.progressBar.Value = 0;
			this.mResult = false;
		}

		protected virtual byte[] ConstructPostQueries()
		{
			if (this.Post == null || this.Post.Count == 0)
				return null;
			string r = "";
			foreach (KeyValuePair<string, object> kvp in this.Post)
			{
				if (r.Length > 0)
				{
					r += "&";
				}
				r += kvp.Key + "=" + HttpUtility.UrlEncode(kvp.Value.ToString());
			}
			return Encoding.UTF8.GetBytes(r);
		}

		protected virtual void OnDoWork()
		{
			using (WebClient wc = new WebClient())
			{
				Stream input = null;
				HttpWebResponse rsp = null;

				try
				{
					// Create a request to the file we are downloading
					HttpWebRequest req = WebRequest.Create(this.URL) as HttpWebRequest;
					// Set default authentication for retrieving the file
					req.Credentials = CredentialCache.DefaultCredentials;
					byte[] q = this.ConstructPostQueries();
					if (q != null && q.Length > 0)
					{
						req.Method = "POST";
						req.ContentType = "application/x-www-form-urlencoded";
						req.ContentLength = q.Length;
						Stream stpost = req.GetRequestStream();
						stpost.Write(q, 0, q.Length);
						stpost.Close();
					}

					// Retrieve the response from the server
					rsp = req.GetResponse() as HttpWebResponse;
					// Ask the server for the file size and store it
					this.mContentLength = rsp.ContentLength;
					if (this.GotContentLength != null)
					{
						this.GotContentLength(this, new GotContentLengthEventArgs(this.mContentLength));
					}
					// Open the URL for download 
					input = wc.OpenRead(this.URL);

					// It will store the current number of bytes we retrieved from the server
					int bytesSize = 0;
					// A buffer for storing and writing the data retrieved from the server
					byte[] downBuffer = new byte[20480];

					// Loop through the buffer until the buffer is empty
					while ((bytesSize = input.Read(downBuffer, 0, downBuffer.Length)) > 0)
					{
						// Write the data from the buffer to the local hard drive
						this.Output.Write(downBuffer, 0, bytesSize);
						// Invoke the method that updates the form's label and progress bar
						backgroundWorker.ReportProgress(0);
					}
					//this.progressBar.Value = this.progressBar.Maximum;
					this.mResult = true;
				}
				finally
				{
					// When the above code has ended, close the streams
					if (rsp != null)
					{
						rsp.Close();
					}
					if (input != null)
					{
						input.Close();
					}
				}
			}
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			this.OnDoWork();
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (this.ContentLength > 0)
			{
				Int64 p = this.Output.Length * 100 / this.ContentLength;
				this.progressBar.Value = (int) p;
			}
			if (this.ProgressChanged != null)
			{
				this.ProgressChanged(this, e);
			}
		}

		protected virtual void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.progressBar.Value = (e.Error == null) ? this.progressBar.Maximum : this.progressBar.Minimum;
			if (this.RunWorkerCompleted != null)
			{
				this.RunWorkerCompleted(this, e);
			}
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.OnRunWorkerCompleted(sender, e);
		}
	}
}