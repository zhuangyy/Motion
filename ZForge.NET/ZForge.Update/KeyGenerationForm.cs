using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ZForge.Update
{
	public partial class KeyGenerationForm : Form
	{
		public KeyGenerationForm()
		{
			InitializeComponent();
		}

		private void buttonGenerate_Click(object sender, EventArgs e)
		{
			try
			{
				RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
				rsa.KeySize = 2048;
				textBoxPri.Text = rsa.ToXmlString(true);
				textBoxPub.Text = rsa.ToXmlString(false);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}