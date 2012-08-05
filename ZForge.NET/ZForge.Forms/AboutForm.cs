using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.Threading;
using ZForge.Globalization;
using ZForge.Controls.Logs;

namespace ZForge.Forms
{
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			InitializeComponent();
		}

		public string Product
		{
			set
			{
				this.labelProduct.Text = value;
				this.Text = Translator.Instance.T("¹ØÓÚ") + value;
			}
		}

		public string Version
		{
			set
			{
				this.labelVersion.Text = value;
			}
		}

		public string Company
		{
			set
			{
				this.labelOrg.Text = value;
			}
		}

		public string URL
		{
			set
			{
				this.linkLabelURL.Text = value;
			}
		}

		public Image TopicImage
		{
			set
			{
				this.pictureBoxLogo.Image = value;
			}
		}

		public ChangeLogItem[] ChangeLogCollection
		{
			set
			{
				this.changelogViewer.ChangeLogCollection = value;
			}
		}

		private void linkLabelURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(this.linkLabelURL.Text);
		}
	}
}