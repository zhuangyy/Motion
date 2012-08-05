using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZForge.Globalization;

namespace ZForge.SA.Komponent
{
	public partial class SARegisterForm : Form
	{
		public SARegisterForm(SAPreference pref, List<SALicense> lics)
		{
			InitializeComponent();
			this.Text = Translator.Instance.T("зЂВс...");

			this.Preference = pref;
			this.LicenseInstances = lics;

			pref.UpdateUI(this);
		}

		#region Properties

		public string Product
		{
			set
			{
				this.labelProduct.Text = value;
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

		private SAPreference Preference
		{
			set
			{
				this.Product = value.ProductName;
				this.Version = value.Version;
				this.Company = value.Company;
				this.URL = value.URL;
			}
		}

		private List<SALicense> LicenseInstances
		{
			set
			{
				this.licenseControl.LicenseInstances = value;
			}
		}

		#endregion

		private void buttonImport_Click(object sender, EventArgs e)
		{
			this.licenseControl.Import();
		}
	}
}