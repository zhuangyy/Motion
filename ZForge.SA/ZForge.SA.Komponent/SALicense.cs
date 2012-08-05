using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Configuration;
using System.Collections;
using ZForge.Win32;
using ZForge.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;

#if SAFE_ANYWHERE_WITHUK
using ZForge.UK.Util;
#endif

namespace ZForge.SA.Komponent
{
	public class SALicense
	{
		protected string mUsername = "DEMO";
		protected string mVersion = "";
		private string mProduct = "";

		public SALicense()
		{
		}

		public SALicense(string productName)
		{
			this.mProduct = productName;
		}

		public virtual bool Load()
		{
			return this.Load(this.LicenseFile);
		}

		public virtual bool Load(string f)
		{
			XMLConfig config = null;
			try
			{
#if SAFE_ANYWHERE_WITHUK
				FileInfo fi = new FileInfo(f);
				string x = UKInterOp.Instance.Hidden.LoadConfig(@"\" + fi.Name);
				config = new XMLConfig(x);
#else
				config = new XMLConfig(f, true);
#endif
				return this.Validate(config.Settings);
			}
			catch (Exception)
			{
				return false;
			}
		}

		public virtual bool Import(string f)
		{
			try
			{
				XMLConfig config = null;
				FileInfo fi = new FileInfo(f);
				f = fi.FullName;
				config = new XMLConfig(f, true);
				if (this.Validate(config.Settings))
				{
#if SAFE_ANYWHERE_WITHUK
					FileInfo fl = new FileInfo(this.LicenseFile);
					MemoryStream ms = new MemoryStream();
					config.Save(ms);
					UKInterOp.Instance.Hidden.SaveConfig(@"\" + fl.Name, ms.ToArray()); 
#else
					if (string.Compare(this.LicenseFile, f, true) != 0)
					{
						System.IO.File.Copy(f, this.LicenseFile, true);
					}
#endif
					return true;
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		#region Properties

		public virtual string Username
		{
			get { return this.mUsername; }
		}

		public virtual string Version
		{
			get
			{
				if (string.IsNullOrEmpty(this.mVersion))
				{
					this.mVersion = "N/A";
				}
				return this.mVersion;
			}
		}

		public virtual string Message
		{
			get
			{
				return "";
			}
		}

		public virtual string HostID
		{
			get
			{
        /*
				string v = "ZYY";
#if SAFE_ANYWHERE_WITHUK
				v = UKInterOp.Instance.UK.GetID();
				string r = this.MD5(v);
				return "UK" + r.Substring(2);
#else
				DriveInfoCollection hd = new DriveInfoCollection();
				DriveListEx list = new DriveListEx();
				list.Load();
				foreach (DriveInfoEx i in list)
				{
					if (hd.Count > 0)
					{
						int idx = hd.IndexOf(i.SerialNumber);
						if (idx < 0)
						{
							continue;
						}
					}
					v = i.SerialNumber + "ZYY" + i.ModelNumber;
					break;
				}
#endif
        */
        string v = "ZYY";
        return this.MD5(v);
      }
		}

		public string Product
		{
			get { return this.mProduct; }
			set { this.mProduct = value; }
		}

		#endregion

		protected bool IsUK()
		{
#if SAFE_ANYWHERE_WITHUK
			return true;
#else
			return false;
#endif
		}

		private string MD5(string p)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] bs = System.Text.Encoding.UTF8.GetBytes(p);
			bs = x.ComputeHash(bs);
			System.Text.StringBuilder s = new System.Text.StringBuilder();

			int n = 1;
			foreach (byte b in bs)
			{
				s.Append(b.ToString("x2").ToUpper());
				if (n % 2 == 0 && n != 16)
				{
					s.Append("-");
				}
				n++;
			}
			return s.ToString();
		}

		public virtual string LicenseFile
		{
			get
			{
				FileInfo fi = new FileInfo(Application.ExecutablePath);
				return Path.Combine(fi.DirectoryName, "ZForge.SA.License.XML");
			}
		}

		protected virtual bool Validate(IConfigSetting x)
		{
			IConfigSetting i = x["license"];
			this.mUsername = "DEMO";
			this.mVersion = null;
			string k = "<RSAKeyValue><Modulus>plCFGoZo+FV6WC0Za3DhlWJgx4Ifs8P2iMNcQip2zwLvwnWozsxZZ9/SbzA1FPXxX5rW+Wttec2iGohLiQiQDBYvPRlccBQgAk7GeTicyFqYZ8KuON0XgNfe1TSf5a17YBgfjI/zNaUypchUoMqMZHTho8UEaEa6pomtfOY3grs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(k);

#if SAFE_ANYWHERE_WITHUK
			string p = this.HostID;
#else
			string p = i["username"].Value;
#endif
			string s = i["signature"].Value;
			bool b = rsa.VerifyData(System.Text.UTF8Encoding.ASCII.GetBytes(p), new SHA1CryptoServiceProvider(), Convert.FromBase64String(s));
			if (b) 
			{
				this.mUsername = i["username"].Value;
			  this.mVersion = x["version"].Value;
			}
			return b;
		}
	}
}
