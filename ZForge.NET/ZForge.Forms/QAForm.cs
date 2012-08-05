using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZForge.Globalization;

namespace ZForge.Forms
{
	public partial class QAForm : Form
	{
		public QAForm()
		{
			InitializeComponent();
			this.reportUploader.Output = new MemoryStream();
		}

		public string URL
		{
			get { return reportUploader.URL; }
			set { reportUploader.URL = value; }
		}

		public string Information
		{
			get { return textBoxInfo.Text; }
			set
			{
				textBoxInfo.Text = value; 
				if (this.reportUploader.Post.ContainsKey("info"))
				{
					this.reportUploader.Post.Remove("info");
				}
				this.reportUploader.Post.Add("info", value);
			}
		}

		public string ProductID
		{
			set
			{
				if (this.reportUploader.Post.ContainsKey("pid"))
				{
					this.reportUploader.Post.Remove("pid");
				}
				this.reportUploader.Post.Add("pid", value);
			}
		}

		public string UserID
		{
			set
			{
				if (this.reportUploader.Post.ContainsKey("uid"))
				{
					this.reportUploader.Post.Remove("uid");
				}
				this.reportUploader.Post.Add("uid", value);
			}
		}

		private string Email
		{
			set
			{
				if (this.reportUploader.Post.ContainsKey("email"))
				{
					this.reportUploader.Post.Remove("email");
				}
				this.reportUploader.Post.Add("email", value);
			}
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			this.Email = textBoxEmail.Text;
			this.reportUploader.Download();
		}

		private void reportUploader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				string m = string.Format(Translator.Instance.T("发送运行错误报告失败! {0}"), e.Error.Message);
				MessageBox.Show(m, Translator.Instance.T("运行错误报告"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				this.DialogResult = DialogResult.OK;
			}
		}
	}
}