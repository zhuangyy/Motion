using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZForge.Controls.Update;
using System.Security.Cryptography;
using System.IO;

namespace ZForge.Update
{
	public partial class UpdateMainForm : Form
	{
		public UpdateMainForm()
		{
			InitializeComponent();
		}

		private void updateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateForm fm = new UpdateForm();
			try
			{
				fm.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
			}
		}

		private void generateKeyPairToolStripMenuItem_Click(object sender, EventArgs e)
		{
			KeyGenerationForm fm = new KeyGenerationForm();
			fm.ShowDialog();
		}

		private void Sign(string filename)
		{
			try
			{
				string p = @"<RSAKeyValue><Modulus>wJqUfZ3Iry4fV6p1bjO817u2/HE1zCmsnguE0Of+1Dzzcc+L3psx1PsDmXlxcLU9E4+ndbIacC2XMWlrIaLSikIJgfMwuvBej18HrrNATpKHwprUpRMU3P9ug5iemz0pyHA3Nr+keCU/b/HsFmido6R1cuBSDd6RYtlK1Xx+KlU=</Modulus><Exponent>AQAB</Exponent><P>+37HaPakQZN5GKh7Jf8a4b/3kqHIynsd0CYVNN0ax3qqRneEdyhfC2CzJGjv6UPOyAXZHn/T8kWpcSfLbqMlqw==</P><Q>xA3Byhq3RTP4YJYBdri/AZMBpRTiV+xSKi1XLz9m0QsNE5ctuwhbD3wY3YlMdIAbOAVewrxjTJg336z2JHPv/w==</Q><DP>VPgKa14ZNMacfUY/BSFhdbAj9viOHEroUbDsLUYejBLXgKNUr+WF5xQusjh6BfeQ32eKaZGKjCoZC1AEnUalrQ==</DP><DQ>BDAnC6I2eAv8KlQKA/c+XVI+nsArdaVeu/fr/N5l2+FYjiqUl4I+L75+6XydXX+/FRtIQvCzTleSGf0f5Pd1EQ==</DQ><InverseQ>xwGNcideNnj6XrDwLFSv3y7CMq2vMzuYxaObaNTU9sh1PTKVMRpiwdKWKpwnstXmDaSduBVw4EvfNlaz+SzUuw==</InverseQ><D>AJc4x13ZhLgGfpVWQN1Fwf+gYwvR12t1TRLJ+H4NqQb61CmHy0n8kCOo8iqOL4NOyaWSJOlD7X4mTY9+NZ8zOBn2Wij0r606Omw+/rlU986lwcxdBiw3y/LND3gowf1gR3Ei9K0eYsHTZZ9Ry9pmqowXi1DG916MBWSuwAbiOw0=</D></RSAKeyValue>";
				RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
				rsa.FromXmlString(p);

				FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
				
				SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
				byte[] hash = sha1.ComputeHash(fs);

				byte[] b = rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
				fs.Close();

				FileInfo fi = new FileInfo(filename);
				fi.CopyTo(filename + ".p1s", true);
				FileStream fo = new FileStream(filename + ".p1s", FileMode.Append, FileAccess.Write, FileShare.None);
				fo.Write(b, 0, b.Length);
				fo.Close();

				MessageBox.Show(filename + ".p1s is successfully created.");
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}

		private void signPackToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.openFileDialog.DefaultExt = "zip";
			this.openFileDialog.Filter = "更新包文件 (*.zip)|*.zip";
			this.openFileDialog.Title = "选择更新包文件";
			if (DialogResult.OK == openFileDialog.ShowDialog())
			{
				this.Sign(openFileDialog.FileName);
			}
		}
	}
}