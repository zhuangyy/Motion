using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZForge.Controls.Update;

namespace ZForge.Update
{
	public partial class UpdateForm : Form
	{
		public UpdateForm()
		{
			InitializeComponent();

			//string p = "<RSAKeyValue><Modulus>wJqUfZ3Iry4fV6p1bjO817u2/HE1zCmsnguE0Of+1Dzzcc+L3psx1PsDmXlxcLU9E4+ndbIacC2XMWlrIaLSikIJgfMwuvBej18HrrNATpKHwprUpRMU3P9ug5iemz0pyHA3Nr+keCU/b/HsFmido6R1cuBSDd6RYtlK1Xx+KlU=</Modulus><Exponent>AQAB</Exponent><P>+37HaPakQZN5GKh7Jf8a4b/3kqHIynsd0CYVNN0ax3qqRneEdyhfC2CzJGjv6UPOyAXZHn/T8kWpcSfLbqMlqw==</P><Q>xA3Byhq3RTP4YJYBdri/AZMBpRTiV+xSKi1XLz9m0QsNE5ctuwhbD3wY3YlMdIAbOAVewrxjTJg336z2JHPv/w==</Q><DP>VPgKa14ZNMacfUY/BSFhdbAj9viOHEroUbDsLUYejBLXgKNUr+WF5xQusjh6BfeQ32eKaZGKjCoZC1AEnUalrQ==</DP><DQ>BDAnC6I2eAv8KlQKA/c+XVI+nsArdaVeu/fr/N5l2+FYjiqUl4I+L75+6XydXX+/FRtIQvCzTleSGf0f5Pd1EQ==</DQ><InverseQ>xwGNcideNnj6XrDwLFSv3y7CMq2vMzuYxaObaNTU9sh1PTKVMRpiwdKWKpwnstXmDaSduBVw4EvfNlaz+SzUuw==</InverseQ><D>AJc4x13ZhLgGfpVWQN1Fwf+gYwvR12t1TRLJ+H4NqQb61CmHy0n8kCOo8iqOL4NOyaWSJOlD7X4mTY9+NZ8zOBn2Wij0r606Omw+/rlU986lwcxdBiw3y/LND3gowf1gR3Ei9K0eYsHTZZ9Ry9pmqowXi1DG916MBWSuwAbiOw0=</D></RSAKeyValue>";
			string k = @"<RSAKeyValue><Modulus>wJqUfZ3Iry4fV6p1bjO817u2/HE1zCmsnguE0Of+1Dzzcc+L3psx1PsDmXlxcLU9E4+ndbIacC2XMWlrIaLSikIJgfMwuvBej18HrrNATpKHwprUpRMU3P9ug5iemz0pyHA3Nr+keCU/b/HsFmido6R1cuBSDd6RYtlK1Xx+KlU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
			this.updater.Key = k;
			UpdateSiteCollection uc = new UpdateSiteCollection();
			uc.Add(new UpdateSite("http://192.168.20.254/release", "test"));
			this.updater.UpdateSiteCollection = uc;
		}

		private void buttonCheck_Click(object sender, EventArgs e)
		{
			this.updater.LocalVersion = "1.0.0";
			this.updater.Destination = @"d:\update";
			this.updater.Upgrade();
		}
	}
}