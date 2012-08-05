using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ZForge.SA.Tools.Sig
{
	internal class Secure
	{
		public static RSACryptoServiceProvider RSA
		{
			get
			{
				string p = @"<RSAKeyValue><Modulus>wJqUfZ3Iry4fV6p1bjO817u2/HE1zCmsnguE0Of+1Dzzcc+L3psx1PsDmXlxcLU9E4+ndbIacC2XMWlrIaLSikIJgfMwuvBej18HrrNATpKHwprUpRMU3P9ug5iemz0pyHA3Nr+keCU/b/HsFmido6R1cuBSDd6RYtlK1Xx+KlU=</Modulus><Exponent>AQAB</Exponent><P>+37HaPakQZN5GKh7Jf8a4b/3kqHIynsd0CYVNN0ax3qqRneEdyhfC2CzJGjv6UPOyAXZHn/T8kWpcSfLbqMlqw==</P><Q>xA3Byhq3RTP4YJYBdri/AZMBpRTiV+xSKi1XLz9m0QsNE5ctuwhbD3wY3YlMdIAbOAVewrxjTJg336z2JHPv/w==</Q><DP>VPgKa14ZNMacfUY/BSFhdbAj9viOHEroUbDsLUYejBLXgKNUr+WF5xQusjh6BfeQ32eKaZGKjCoZC1AEnUalrQ==</DP><DQ>BDAnC6I2eAv8KlQKA/c+XVI+nsArdaVeu/fr/N5l2+FYjiqUl4I+L75+6XydXX+/FRtIQvCzTleSGf0f5Pd1EQ==</DQ><InverseQ>xwGNcideNnj6XrDwLFSv3y7CMq2vMzuYxaObaNTU9sh1PTKVMRpiwdKWKpwnstXmDaSduBVw4EvfNlaz+SzUuw==</InverseQ><D>AJc4x13ZhLgGfpVWQN1Fwf+gYwvR12t1TRLJ+H4NqQb61CmHy0n8kCOo8iqOL4NOyaWSJOlD7X4mTY9+NZ8zOBn2Wij0r606Omw+/rlU986lwcxdBiw3y/LND3gowf1gR3Ei9K0eYsHTZZ9Ry9pmqowXi1DG916MBWSuwAbiOw0=</D></RSAKeyValue>";
				RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
				rsa.FromXmlString(p);
				return rsa;
			}
		}

		public static byte[] Sign(byte[] sha1)
		{
			byte[] b = Secure.RSA.SignHash(sha1, CryptoConfig.MapNameToOID("SHA1"));
			return b;
		}

		public static void Sign(string filename)
		{
			FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
			SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
			byte[] hash = sha1.ComputeHash(fs);
			fs.Close();

			byte[] b = Secure.Sign(hash);

			FileInfo fi = new FileInfo(filename);
			string dstfile = fi.DirectoryName + @"\setup.ipk";

			fi.CopyTo(dstfile, true);
			FileStream fo = new FileStream(dstfile, FileMode.Append, FileAccess.Write, FileShare.None);
			fo.Write(b, 0, b.Length);
			fo.Close();
		}

	}
}
