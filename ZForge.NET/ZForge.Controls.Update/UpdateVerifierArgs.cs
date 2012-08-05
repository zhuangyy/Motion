using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ZForge.Controls.Update
{
	internal class UpdateVerifierArgs
	{
		private MemoryStream mMem;
		private byte[] mSignature;
		private RSACryptoServiceProvider mRSA;
		internal const int SIGLEN = 128;

		internal UpdateVerifierArgs(byte[] bs, RSACryptoServiceProvider rsa)
		{
			this.Init(bs, rsa);
		}

		internal UpdateVerifierArgs(byte[] bs, string key)
		{
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(key);
			this.Init(bs, rsa);
		}

		internal void Init(byte[] bs, RSACryptoServiceProvider rsa)
		{
			if (bs.Length < SIGLEN)
			{
				throw new ArgumentOutOfRangeException();
			}
			mMem = new MemoryStream(bs, 0, bs.Length - SIGLEN);
			mSignature = new byte[SIGLEN];
			for (int n = 0; n < mSignature.Length; n++)
			{
				mSignature[n] = bs[bs.Length - SIGLEN + n];
			}
			mRSA = rsa;
		}

		internal MemoryStream Stream
		{
			get { return this.mMem; }
		}

		internal byte[] Signature
		{
			get { return this.mSignature; }
		}

		internal RSACryptoServiceProvider RSA
		{
			get { return this.mRSA; }
		}

		internal bool Verify()
		{
			RSACryptoServiceProvider rsa = this.RSA;
			SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
			byte[] hash = sha1.ComputeHash(this.Stream);
			return rsa.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1"), this.Signature); 
			//rsa.VerifyData(this.Stream.GetBuffer(), new SHA1CryptoServiceProvider(), this.Signature);
			//return true;
		}
	}
}
