namespace ZForge.Win32.DirectShow.Core
{
	using System;
	using System.Runtime.InteropServices;
	using System.Runtime.InteropServices.ComTypes;

	/// <summary>
	/// Some Win32 API functions
	/// </summary>
	[ComVisible(false)]
	public class Win32
	{
		// memcpy - copy a block of memery
		[DllImport("ntdll.dll")]
		public static extern
			int memcpy(int dst, int src, int count);

		// Supplies a pointer to an implementation of IBindCtx
		[DllImport("ole32.dll")]
		public static extern
		int CreateBindCtx(
			int reserved,
			out IBindCtx ppbc);

		// Converts a string into a moniker that identifies
		// the object named by the string
		[DllImport("ole32.dll", CharSet=CharSet.Unicode)]
		public static extern
		int MkParseDisplayName(
			IBindCtx pbc,
			string szUserName,
			ref int pchEaten,
			out IMoniker ppmk);

		// window styles
		[Flags]
		public enum WS
		{
			CHILD	= 0x40000000,
			VISIBLE	= 0x10000000
		}
	}
}
