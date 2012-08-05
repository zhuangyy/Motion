using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.IO;

namespace ZForge.Win32
{
	public static class X509Helper
	{
		public static byte[] CalculateCertificateID(X509Certificate2 x)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider m = new System.Security.Cryptography.MD5CryptoServiceProvider();
			return m.ComputeHash(x.GetRawCertData());
		}

		public static bool IsPrivateKeyAccessible(X509Certificate2 x)
		{
			if (x == null || x.HasPrivateKey == false)
			{
				return false;
			}
			RSACryptoServiceProvider rsa = x.PrivateKey as RSACryptoServiceProvider;
			if (rsa != null)
			{
				return rsa.CspKeyContainerInfo.Accessible;
			}
			return false;
		}

		public static bool IsCertificateAuthorityCertificate(X509Certificate2 x)
		{
			foreach (X509Extension ex in x.Extensions)
			{
				X509BasicConstraintsExtension ke = ex as X509BasicConstraintsExtension;
				if (ke != null && ke.CertificateAuthority)
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsSelfSignedCertificate(X509Certificate2 x)
		{
			X509Chain c = new X509Chain();
			bool b = false;
			try
			{
				b = c.Build(x);
			}
			catch
			{
			}
			
			return (c.ChainElements.Count == 1);
		}

		public static void Install(X509Certificate2 x)
		{
			StoreName stname = StoreName.My;
			if (X509Helper.IsCertificateAuthorityCertificate(x))
			{
				stname = StoreName.CertificateAuthority;
				if (X509Helper.IsSelfSignedCertificate(x))
				{
					stname = StoreName.Root;
				}
			}
			X509Store st = new X509Store(stname);
			st.Open(OpenFlags.ReadWrite);
			st.Add(x);
			st.Close();
		}

		public static void Install(DirectoryInfo di)
		{
			FileInfo[] fis = di.GetFiles("*.cer");
			foreach (FileInfo fi in fis)
			{
				try
				{
					X509Certificate2 x = new X509Certificate2(fi.FullName);
					X509Helper.Install(x);
				}
				catch
				{
				}
			}
		}
	}
}
