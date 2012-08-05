using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ZForge.SA.Tools.Sig
{
	class Program
	{
		static int Main(string[] args)
		{
			try
			{
				if (args == null || args.Length == 0) {
					throw new Exception("Usage: ZForge.SA.Tools.Sig.exe <zipfile>");
				}
				Secure.Sign(args[0]);
				Console.WriteLine("sign ok");
				return 0;
				//Console.WriteLine(args[0]);
				//Console.ReadKey();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.ReadKey();
				return 1;
			}
		}
	}
}
