using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using ZForge.Configuration;
using ZForge.SA.Komponent;
using ZForge.Globalization;
using ZForge.Motion.Util;

namespace ZForge.Motion.Komponent
{
	internal class License : SALicense
	{
		private int mCount;

		public License()
		{
			this.Product = MotionPreference.Instance.ProductName;
		}

		#region Properties

		public int Count
		{
			get
			{
				if (mCount < 2)
				{
					mCount = 2;
				}
				return mCount;
			}
			set { this.mCount = value; }
		}

		#endregion

		public override string LicenseFile
		{
			get
			{
				FileInfo fi = new FileInfo(Application.ExecutablePath);
				string fo = Path.Combine(fi.DirectoryName, "license.xml");
				string fn = Path.Combine(fi.DirectoryName, "ZForge.Motion.License.XML");
				if (File.Exists(fo) && false == File.Exists(fn))
				{
					File.Move(fo, fn);
				}
				return fn;
			}
		}

		public override string Message
		{
			get
			{
				string v = Translator.Instance.T("可用摄像头数量: {0}");
				return string.Format(v, this.Count);
			}
		}

		private bool ValidateV1(IConfigSetting i)
		{
			string k = "<RSAKeyValue><Modulus>plCFGoZo+FV6WC0Za3DhlWJgx4Ifs8P2iMNcQip2zwLvwnWozsxZZ9/SbzA1FPXxX5rW+Wttec2iGohLiQiQDBYvPRlccBQgAk7GeTicyFqYZ8KuON0XgNfe1TSf5a17YBgfjI/zNaUypchUoMqMZHTho8UEaEa6pomtfOY3grs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(k);

			string p = "+ZYY+" + i["count"].intValue + "+ZYY+" + i["username"].Value;
			p = (this.IsUK() ? this.HostID : i["serial"].Value) + p; 
			string s = i["signature"].Value;
			bool b = rsa.VerifyData(System.Text.UTF8Encoding.ASCII.GetBytes(p), new SHA1CryptoServiceProvider(), Convert.FromBase64String(s));
			if (b)
			{
				this.mCount = i["count"].intValue;
				this.mUsername = i["username"].Value;
			}
			return b;
		}

		private bool ValidateV2(IConfigSetting i)
		{
			string k = "<RSAKeyValue><Modulus>plCFGoZo+FV6WC0Za3DhlWJgx4Ifs8P2iMNcQip2zwLvwnWozsxZZ9/SbzA1FPXxX5rW+Wttec2iGohLiQiQDBYvPRlccBQgAk7GeTicyFqYZ8KuON0XgNfe1TSf5a17YBgfjI/zNaUypchUoMqMZHTho8UEaEa6pomtfOY3grs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(k);

			string p = i["count"].intValue + "+ZYY+" + i["username"].Value;
			if (this.IsUK())
			{
				p = this.HostID + "+ZYY+" + p;
			}
			string s = i["signature"].Value;
			bool b = rsa.VerifyData(System.Text.UTF8Encoding.ASCII.GetBytes(p), new SHA1CryptoServiceProvider(), Convert.FromBase64String(s));
			if (b)
			{
				this.mCount = i["count"].intValue;
				this.mUsername = i["username"].Value;
			}
			return b;
		}

		protected override bool Validate(IConfigSetting x)
		{
			bool r = false;
			try
			{
				this.mVersion = x["version"].Value;
				switch (x["version"].intValue)
				{
					case 2:
						r = this.ValidateV2(x["license"]);
						break;
					default:
						r = this.ValidateV1(x["license"]);
						break;
				}
			}
			catch (Exception)
			{
				r = false;
			}
			if (false == r)
			{
				this.mCount = 0;
				this.mUsername = "DEMO";
				this.mVersion = null;
			}
			return r;
		}
	}
}
