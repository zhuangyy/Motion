namespace ZForge.Win32.DirectShow.Core
{
	using System;
	using System.Runtime.InteropServices;
	using System.Runtime.InteropServices.ComTypes;

	// ICreateDevEnum interface
	//
	// Creates an enumerator for a list of filters and
	// hardware devices in a specified filter category
	//
	[ComImport,
	Guid("29840822-5B84-11D0-BD3B-00A0C911CE86"),
	InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ICreateDevEnum
	{
		// Creates a class enumerator for the specified category
		[PreserveSig]
		int CreateClassEnumerator(
			[In] ref Guid pType,
			[Out] out IEnumMoniker ppEnumMoniker,
			[In] int dwFlags);
	}
}
