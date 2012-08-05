/*
 * Copyright ?2004-2005, Mathew Hall
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 *
 *    - Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 * 
 *    - Redistributions in binary form must reproduce the above copyright notice, 
 *      this list of conditions and the following disclaimer in the documentation 
 *      and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
 * OF SUCH DAMAGE.
 */


using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;


namespace ZForge.Controls.ExplorerBar
{
	#region NativeMethods

	/// <summary>
	/// A class that provides access to the Win32 API
	/// </summary>
	public sealed class NativeMethods
	{
		/// <summary>
		/// The LoadLibrary function maps the specified executable module into the 
		/// address space of the calling process
		/// </summary>
		/// <param name="lpFileName">Pointer to a null-terminated string that names 
		/// the executable module (either a .dll or .exe file). The name specified 
		/// is the file name of the module and is not related to the name stored in 
		/// the library module itself, as specified by the LIBRARY keyword in the 
		/// module-definition (.def) file.  
		/// If the string specifies a path but the file does not exist in the specified 
		/// directory, the function fails. When specifying a path, be sure to use 
		/// backslashes (\), not forward slashes (/).  
		/// If the string does not specify a path, the function uses a standard search 
		/// strategy to find the file.</param>
		/// <returns>If the function succeeds, the return value is a handle to the module. 
		/// If the function fails, the return value is NULL</returns>
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr LoadLibrary(string lpFileName);


		/// <summary>
		/// The LoadLibraryEx function maps the specified executable module into the 
		/// address space of the calling process. The executable module can be a .dll 
		/// or an .exe file. The specified module may cause other modules to be mapped 
		/// into the address space
		/// </summary>
		/// <param name="lpfFileName">Pointer to a null-terminated string that names 
		/// the executable module (either a .dll or an .exe file). The name specified 
		/// is the file name of the executable module. This name is not related to the 
		/// name stored in a library module itself, as specified by the LIBRARY keyword 
		/// in the module-definition (.def) file. If the string specifies a path, but 
		/// the file does not exist in the specified directory, the function fails. When 
		/// specifying a path, be sure to use backslashes (\), not forward slashes (/). 
		/// If the string does not specify a path, and the file name extension is omitted, 
		/// the function appends the default library extension .dll to the file name. 
		/// However, the file name string can include a trailing point character (.) to 
		/// indicate that the module name has no extension. If the string does not specify 
		/// a path, the function uses a standard search strategy to find the file. If 
		/// mapping the specified module into the address space causes the system to map 
		/// in other, associated executable modules, the function can use either the 
		/// standard search strategy or an alternate search strategy to find those modules.</param>
		/// <param name="flags">Action to take when loading the module. If no flags are 
		/// specified, the behavior of this function is identical to that of the LoadLibrary 
		/// function. This parameter can be one of the LoadLibraryExFlags values</param>
		/// <returns>If the function succeeds, the return value is a handle to the mapped 
		/// executable module. If the function fails, the return value is NULL.</returns>
		public static IntPtr LoadLibraryEx(string lpfFileName, LoadLibraryExFlags flags)
		{
			return NativeMethods.InternalLoadLibraryEx(lpfFileName, IntPtr.Zero, (int)flags);
		}

		[DllImport("Kernel32.dll", EntryPoint = "LoadLibraryEx")]
		private static extern IntPtr InternalLoadLibraryEx(string lpfFileName, IntPtr hFile, int dwFlags);


		/// <summary>
		/// The FreeLibrary function decrements the reference count of the loaded 
		/// dynamic-link library (DLL). When the reference count reaches zero, the 
		/// module is unmapped from the address space of the calling process and the 
		/// handle is no longer valid
		/// </summary>
		/// <param name="hModule">Handle to the loaded DLL module. The LoadLibrary 
		/// function returns this handle</param>
		/// <returns>If the function succeeds, the return value is nonzero. If the 
		/// function fails, the return value is zero</returns>
		[DllImport("Kernel32.dll")]
		public static extern bool FreeLibrary(IntPtr hModule);


		/// <summary>
		/// The FindResource function determines the location of a resource with the 
		/// specified type and name in the specified module
		/// </summary>
		/// <param name="hModule">Handle to the module whose executable file contains 
		/// the resource. A value of NULL specifies the module handle associated with 
		/// the image file that the operating system used to create the current process</param>
		/// <param name="lpName">Specifies the name of the resource</param>
		/// <param name="lpType">Specifies the resource type</param>
		/// <returns>If the function succeeds, the return value is a handle to the 
		/// specified resource's information block. To obtain a handle to the resource, 
		/// pass this handle to the LoadResource function. If the function fails, the 
		/// return value is NULL</returns>
		[DllImport("Kernel32.dll")]
		public static extern IntPtr FindResource(IntPtr hModule, string lpName, int lpType);

		/// <summary>
		/// The FindResource function determines the location of a resource with the 
		/// specified type and name in the specified module
		/// </summary>
		/// <param name="hModule">Handle to the module whose executable file contains 
		/// the resource. A value of NULL specifies the module handle associated with 
		/// the image file that the operating system used to create the current process</param>
		/// <param name="lpName">Specifies the name of the resource</param>
		/// <param name="lpType">Specifies the resource type</param>
		/// <returns>If the function succeeds, the return value is a handle to the 
		/// specified resource's information block. To obtain a handle to the resource, 
		/// pass this handle to the LoadResource function. If the function fails, the 
		/// return value is NULL</returns>
		[DllImport("Kernel32.dll")]
		public static extern IntPtr FindResource(IntPtr hModule, string lpName, string lpType);


		/// <summary>
		/// The SizeofResource function returns the size, in bytes, of the specified 
		/// resource
		/// </summary>
		/// <param name="hModule">Handle to the module whose executable file contains 
		/// the resource</param>
		/// <param name="hResInfo">Handle to the resource. This handle must be created 
		/// by using the FindResource or FindResourceEx function</param>
		/// <returns>If the function succeeds, the return value is the number of bytes 
		/// in the resource. If the function fails, the return value is zero</returns>
		[DllImport("Kernel32.dll")]
		public static extern int SizeofResource(IntPtr hModule, IntPtr hResInfo);


		/// <summary>
		/// The LoadResource function loads the specified resource into global memory
		/// </summary>
		/// <param name="hModule">Handle to the module whose executable file contains 
		/// the resource. If hModule is NULL, the system loads the resource from the 
		/// module that was used to create the current process</param>
		/// <param name="hResInfo">Handle to the resource to be loaded. This handle is 
		/// returned by the FindResource or FindResourceEx function</param>
		/// <returns>If the function succeeds, the return value is a handle to the data 
		/// associated with the resource. If the function fails, the return value is NULL</returns>
		[DllImport("Kernel32.dll")]
		public static extern System.IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);


		/// <summary>
		/// The FreeResource function decrements (decreases by one) the reference count 
		/// of a loaded resource. When the reference count reaches zero, the memory occupied 
		/// by the resource is freed
		/// </summary>
		/// <param name="hglbResource">Handle of the resource. It is assumed that hglbResource 
		/// was created by LoadResource</param>
		/// <returns>If the function succeeds, the return value is zero. If the function fails, 
		/// the return value is non-zero, which indicates that the resource has not been freed</returns>
		[DllImport("Kernel32.dll")]
		public static extern int FreeResource(IntPtr hglbResource);


		/// <summary>
		/// The CopyMemory function copies a block of memory from one location to another
		/// </summary>
		/// <param name="Destination">Pointer to the starting address of the copied 
		/// block's destination</param>
		/// <param name="Source">Pointer to the starting address of the block of memory 
		/// to copy</param>
		/// <param name="Length">Size of the block of memory to copy, in bytes</param>
		[DllImport("Kernel32.dll")]
		public static extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);


		/// <summary>
		/// The LoadBitmap function loads the specified bitmap resource from a module's 
		/// executable file
		/// </summary>
		/// <param name="hInstance">Handle to the instance of the module whose executable 
		/// file contains the bitmap to be loaded</param>
		/// <param name="lpBitmapName">Pointer to a null-terminated string that contains 
		/// the name of the bitmap resource to be loaded. Alternatively, this parameter 
		/// can consist of the resource identifier in the low-order word and zero in the 
		/// high-order word</param>
		/// <returns>If the function succeeds, the return value is the handle to the specified 
		/// bitmap. If the function fails, the return value is NULL</returns>
		[DllImport("User32.dll")]
		public static extern IntPtr LoadBitmap(IntPtr hInstance, uint lpBitmapName);


		/// <summary>
		/// The LoadBitmap function loads the specified bitmap resource from a module's 
		/// executable file
		/// </summary>
		/// <param name="hInstance">Handle to the instance of the module whose executable 
		/// file contains the bitmap to be loaded</param>
		/// <param name="lpBitmapName">Pointer to a null-terminated string that contains 
		/// the name of the bitmap resource to be loaded. Alternatively, this parameter 
		/// can consist of the resource identifier in the low-order word and zero in the 
		/// high-order word</param>
		/// <returns>If the function succeeds, the return value is the handle to the specified 
		/// bitmap. If the function fails, the return value is NULL</returns>
		[DllImport("User32.dll")]
		public static extern IntPtr LoadBitmap(IntPtr hInstance, string lpBitmapName);


		/// <summary>
		/// The GdiFlush function flushes the calling thread's current batch
		/// </summary>
		/// <returns>If all functions in the current batch succeed, the return value is 
		/// nonzero. If not all functions in the current batch succeed, the return value 
		/// is zero, indicating that at least one function returned an error</returns>
		[DllImport("Gdi32.dll")]
		public static extern int GdiFlush();


		/// <summary>
		/// The LoadString function loads a string resource from the executable file 
		/// associated with a specified module, copies the string into a buffer, and 
		/// appends a terminating null character
		/// </summary>
		/// <param name="hInstance">Handle to an instance of the module whose executable 
		/// file contains the string resource</param>
		/// <param name="uID">Specifies the integer identifier of the string to be loaded</param>
		/// <param name="lpBuffer">Pointer to the buffer to receive the string</param>
		/// <param name="nBufferMax">Specifies the size of the buffer, in TCHARs. This 
		/// refers to bytes for versions of the function or WCHARs for Unicode versions. 
		/// The string is truncated and null terminated if it is longer than the number 
		/// of characters specified</param>
		/// <returns>If the function succeeds, the return value is the number of TCHARs 
		/// copied into the buffer, not including the null-terminating character, or 
		/// zero if the string resource does not exist</returns>
		[DllImport("User32.dll")]
		public static extern int LoadString(IntPtr hInstance, int uID, StringBuilder lpBuffer, int nBufferMax);


		/// <summary>
		/// The SendMessage function sends the specified message to a 
		/// window or windows. It calls the window procedure for the 
		/// specified window and does not return until the window 
		/// procedure has processed the message
		/// </summary>
		/// <param name="hwnd">Handle to the window whose window procedure will 
		/// receive the message</param>
		/// <param name="msg">Specifies the message to be sent</param>
		/// <param name="wParam">Specifies additional message-specific information</param>
		/// <param name="lParam">Specifies additional message-specific information</param>
		/// <returns>The return value specifies the result of the message processing; 
		/// it depends on the message sent</returns>
		public static int SendMessage(IntPtr hwnd, WindowMessageFlags msg, IntPtr wParam, IntPtr lParam)
		{
			return NativeMethods.InternalSendMessage(hwnd, (int)msg, wParam, lParam);
		}

		[DllImport("User32.dll", EntryPoint = "SendMessage")]
		private static extern int InternalSendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);


		/// <summary>
		/// Implemented by many of the Microsoft?Windows?Shell dynamic-link libraries 
		/// (DLLs) to allow applications to obtain DLL-specific version information
		/// </summary>
		/// <param name="pdvi">Pointer to a DLLVERSIONINFO structure that receives the 
		/// version information. The cbSize member must be filled in before calling 
		/// the function</param>
		/// <returns>Returns NOERROR if successful, or an OLE-defined error value otherwise</returns>
		[DllImport("Comctl32.dll")]
		public static extern int DllGetVersion(ref DLLVERSIONINFO pdvi);


		/// <summary>
		/// The GetProcAddress function retrieves the address of an exported function 
		/// or variable from the specified dynamic-link library (DLL)
		/// </summary>
		/// <param name="hModule">Handle to the DLL module that contains the function 
		/// or variable. The LoadLibrary or GetModuleHandle function returns this handle</param>
		/// <param name="procName">Pointer to a null-terminated string that specifies 
		/// the function or variable name, or the function's ordinal value. If this 
		/// parameter is an ordinal value, it must be in the low-order word; the 
		/// high-order word must be zero</param>
		/// <returns>If the function succeeds, the return value is the address of the 
		/// exported function or variable. If the function fails, the return value is NULL</returns>
		[DllImport("Kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);


		/// <summary>
		/// The SetErrorMode function controls whether the system will handle the 
		/// specified types of serious errors, or whether the process will handle them
		/// </summary>
		/// <param name="uMode">Process error mode. This parameter can be one or more of 
		/// the SetErrorModeFlags values</param>
		/// <returns>The return value is the previous state of the error-mode bit flags</returns>
		public static int SetErrorMode(SetErrorModeFlags uMode)
		{
			return NativeMethods.InternalSetErrorMode((int)uMode);
		}

		[DllImport("Kernel32.dll", EntryPoint = "SetErrorMode")]
		private static extern int InternalSetErrorMode(int uMode);


		/// <summary>
		/// The GetSystemMetrics function retrieves various system metrics (widths and 
		/// heights of display elements) and system configuration settings. All dimensions 
		/// retrieved by GetSystemMetrics are in pixels
		/// </summary>
		/// <param name="nIndex">System metric or configuration setting to retrieve. This 
		/// parameter can be one of the SysMetricsFlags values. Note that all SM_CX* values 
		/// are widths and all SM_CY* values are heights</param>
		/// <returns>If the function succeeds, the return value is the requested system 
		/// metric or configuration setting. If the function fails, the return value is zero</returns>
		[DllImport("User32.dll")]
		internal static extern int GetSystemMetrics(int nIndex);


		/// <summary>
		/// The GetDC function retrieves a handle to a display device context (DC) for 
		/// the client area of a specified window or for the entire screen. You can use 
		/// the returned handle in subsequent GDI functions to draw in the DC
		/// </summary>
		/// <param name="hWnd">Handle to the window whose DC is to be retrieved. If this 
		/// value is IntPtr.Zero, GetDC retrieves the DC for the entire screen</param>
		/// <returns>If the function succeeds, the return value is an IntPtr that points 
		/// to the handle to the DC for the specified window's client area. If the function 
		/// fails, the return value is IntPtr.Zero</returns>
		[DllImport("User32.dll")]
		internal static extern IntPtr GetDC(IntPtr hWnd);


		/// <summary>
		/// The ReleaseDC function releases a device context (DC), freeing it for use by 
		/// other applications. The effect of the ReleaseDC function depends on the type 
		/// of DC. It frees only common and window DCs. It has no effect on class or 
		/// private DCs
		/// </summary>
		/// <param name="hWnd">Handle to the window whose DC is to be released</param>
		/// <param name="hDC">Handle to the DC to be released</param>
		/// <returns>If the DC was released, the return value is 1, otherwise the return 
		/// value is zero</returns>
		[DllImport("User32.dll")]
		internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);


		/// <summary>
		/// The GetDeviceCaps function retrieves device-specific information for the 
		/// specified device
		/// </summary>
		/// <param name="hDC">Handle to the DC</param>
		/// <param name="nIndex">Specifies the item to return. This parameter can be one of 
		/// the DeviceCapsFlags values</param>
		/// <returns>The return value specifies the value of the desired item</returns>
		[DllImport("Gdi32.dll")]
		internal static extern int GetDeviceCaps(IntPtr hDC, int nIndex);


		/// <summary>
		/// The CreateIconFromResourceEx function creates an icon or cursor from resource 
		/// bits describing the icon
		/// </summary>
		/// <param name="pbIconBits">Pointer to a buffer containing the icon or cursor 
		/// resource bits. These bits are typically loaded by calls to the 
		/// LookupIconIdFromDirectoryEx and LoadResource functions</param>
		/// <param name="cbIconBits">Specifies the size, in bytes, of the set of bits 
		/// pointed to by the pbIconBits parameter</param>
		/// <param name="fIcon">Specifies whether an icon or a cursor is to be created. 
		/// If this parameter is TRUE, an icon is to be created. If it is FALSE, a cursor 
		/// is to be created</param>
		/// <param name="dwVersion">Specifies the version number of the icon or cursor 
		/// format for the resource bits pointed to by the pbIconBits parameter. This 
		/// parameter can be 0x00030000</param>
		/// <param name="csDesired">Specifies the desired width, in pixels, of the icon 
		/// or cursor. If this parameter is zero, the function uses the SM_CXICON or 
		/// SM_CXCURSOR system metric value to set the width</param>
		/// <param name="cyDesired">Specifies the desired height, in pixels, of the icon 
		/// or cursor. If this parameter is zero, the function uses the SM_CYICON or 
		/// SM_CYCURSOR system metric value to set the height</param>
		/// <param name="flags"></param>
		/// <returns>If the function succeeds, the return value is a handle to the icon 
		/// or cursor. If the function fails, the return value is NULL</returns>
		[DllImport("User32.dll")]
		internal static extern unsafe IntPtr CreateIconFromResourceEx(byte* pbIconBits, int cbIconBits, bool fIcon, int dwVersion, int csDesired, int cyDesired, int flags);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="hdc"></param>
		/// <param name="hgdiobj"></param>
		/// <returns></returns>
		[DllImport("Gdi32.dll")]
		internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="hObject"></param>
		/// <returns></returns>
		[DllImport("Gdi32.dll")]
		internal static extern bool DeleteObject(IntPtr hObject);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="hdc"></param>
		/// <param name="lpString"></param>
		/// <param name="nCount"></param>
		/// <param name="lpRect"></param>
		/// <param name="uFormat"></param>
		/// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern int DrawText(IntPtr hdc, string lpString, int nCount, ref RECT lpRect, DrawTextFlags uFormat);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="hdc"></param>
		/// <param name="iBkMode"></param>
		/// <returns></returns>
		[DllImport("Gdi32.dll")]
		internal static extern int SetBkMode(IntPtr hdc, int iBkMode);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="hdc"></param>
		/// <param name="crColor"></param>
		/// <returns></returns>
		[DllImport("Gdi32.dll")]
		internal static extern int SetTextColor(IntPtr hdc, int crColor);
	}

	#endregion



	#region Structs

	/// <summary>
	/// The POINT structure defines the x- and y- coordinates of a point
	/// </summary>
	[Serializable(),
	StructLayout(LayoutKind.Sequential)]
	public struct POINT
	{
		/// <summary>
		/// Specifies the x-coordinate of the point
		/// </summary>
		public int x;
			
		/// <summary>
		/// Specifies the y-coordinate of the point
		/// </summary>
		public int y;


		/// <summary>
		/// Creates a new RECT struct with the specified x and y coordinates
		/// </summary>
		/// <param name="x">The x-coordinate of the point</param>
		/// <param name="y">The y-coordinate of the point</param>
		public POINT(int x, int y)
		{
			this.x = x;
			this.y = y;
		}


		/// <summary>
		/// Creates a new POINT struct from the specified Point
		/// </summary>
		/// <param name="p">The Point to create the POINT from</param>
		/// <returns>A POINT struct with the same x and y coordinates as 
		/// the specified Point</returns>
		public static POINT FromPoint(Point p)
		{
			return new POINT(p.X, p.Y);
		}


		/// <summary>
		/// Returns a Point with the same x and y coordinates as the POINT
		/// </summary>
		/// <returns>A Point with the same x and y coordinates as the POINT</returns>
		public Point ToPoint()
		{
			return new Point(this.x, this.y);
		}
	}


	/// <summary>
	/// The RECT structure defines the coordinates of the upper-left 
	/// and lower-right corners of a rectangle
	/// </summary>
	[Serializable(),
	StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		/// <summary>
		/// Specifies the x-coordinate of the upper-left corner of the RECT
		/// </summary>
		public int left;
			
		/// <summary>
		/// Specifies the y-coordinate of the upper-left corner of the RECT
		/// </summary>
		public int top;
			
		/// <summary>
		/// Specifies the x-coordinate of the lower-right corner of the RECT
		/// </summary>
		public int right;
			
		/// <summary>
		/// Specifies the y-coordinate of the lower-right corner of the RECT
		/// </summary>
		public int bottom;


		/// <summary>
		/// Creates a new RECT struct with the specified location and size
		/// </summary>
		/// <param name="left">The x-coordinate of the upper-left corner of the RECT</param>
		/// <param name="top">The y-coordinate of the upper-left corner of the RECT</param>
		/// <param name="right">The x-coordinate of the lower-right corner of the RECT</param>
		/// <param name="bottom">The y-coordinate of the lower-right corner of the RECT</param>
		public RECT(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}


		/// <summary>
		/// Creates a new RECT struct from the specified Rectangle
		/// </summary>
		/// <param name="rect">The Rectangle to create the RECT from</param>
		/// <returns>A RECT struct with the same location and size as 
		/// the specified Rectangle</returns>
		public static RECT FromRectangle(Rectangle rect)
		{
			return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
		}


		/// <summary>
		/// Creates a new RECT struct with the specified location and size
		/// </summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the RECT</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the RECT</param>
		/// <param name="width">The width of the RECT</param>
		/// <param name="height">The height of the RECT</param>
		/// <returns>A RECT struct with the specified location and size</returns>
		public static RECT FromXYWH(int x, int y, int width, int height)
		{
			return new RECT(x, y, x + width, y + height);
		}


		/// <summary>
		/// Returns a Rectangle with the same location and size as the RECT
		/// </summary>
		/// <returns>A Rectangle with the same location and size as the RECT</returns>
		public Rectangle ToRectangle()
		{
			return new Rectangle(this.left, this.top, this.right - this.left, this.bottom - this.top);
		}
	}


	/// <summary>
	/// Receives dynamic-link library (DLL)-specific version information. 
	/// It is used with the DllGetVersion function
	/// </summary>
	[Serializable(),
	StructLayout(LayoutKind.Sequential)]
	public struct DLLVERSIONINFO
	{
		/// <summary>
		/// Size of the structure, in bytes. This member must be filled 
		/// in before calling the function
		/// </summary>
		public int cbSize;

		/// <summary>
		/// Major version of the DLL. If the DLL's version is 4.0.950, 
		/// this value will be 4
		/// </summary>
		public int dwMajorVersion;

		/// <summary>
		/// Minor version of the DLL. If the DLL's version is 4.0.950, 
		/// this value will be 0
		/// </summary>
		public int dwMinorVersion;

		/// <summary>
		/// Build number of the DLL. If the DLL's version is 4.0.950, 
		/// this value will be 950
		/// </summary>
		public int dwBuildNumber;

		/// <summary>
		/// Identifies the platform for which the DLL was built
		/// </summary>
		public int dwPlatformID;
	}


	/// <summary>
	/// 
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	internal struct ICONFILE
	{
		/// <summary>
		/// 
		/// </summary>
		public short reserved;

		/// <summary>
		/// 
		/// </summary>
		public short resourceType;

		/// <summary>
		/// 
		/// </summary>
		public short iconCount;

		/// <summary>
		/// 
		/// </summary>
		public ICONENTRY entries;
	}


	/// <summary>
	/// 
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct ICONENTRY
	{
		/// <summary>
		/// 
		/// </summary>
		public byte width;

		/// <summary>
		/// 
		/// </summary>
		public byte height;

		/// <summary>
		/// 
		/// </summary>
		public byte numColors;

		/// <summary>
		/// 
		/// </summary>
		public byte reserved;

		/// <summary>
		/// 
		/// </summary>
		public short numPlanes;

		/// <summary>
		/// 
		/// </summary>
		public short bitsPerPixel;

		/// <summary>
		/// 
		/// </summary>
		public int dataSize;

		/// <summary>
		/// 
		/// </summary>
		public int dataOffset;
	}

	#endregion



	#region Flags

	#region Window Messages

	/// <summary>
	/// The WindowMessageFlags enemeration contains Windows messages that the 
	/// XPExplorerBar may be interested in listening for
	/// </summary>
	public enum WindowMessageFlags
	{
		/// <summary>
		/// The WM_PRINT message is sent to a window to request that it draw 
		/// itself in the specified device context, most commonly in a printer 
		/// device context
		/// </summary>
		WM_PRINT = 791,

		/// <summary>
		/// The WM_PRINTCLIENT message is sent to a window to request that it draw 
		/// its client area in the specified device context, most commonly in a 
		/// printer device context
		/// </summary>
		WM_PRINTCLIENT = 792,
	}

	#endregion

	#region WmPrint

	/// <summary>
	/// The WmPrintFlags enemeration contains flags that may be sent 
	/// when a WM_PRINT or WM_PRINTCLIENT message is recieved
	/// </summary>
	public enum WmPrintFlags
	{
		/// <summary>
		/// Draws the window only if it is visible
		/// </summary>
		PRF_CHECKVISIBLE = 1,

		/// <summary>
		/// Draws the nonclient area of the window
		/// </summary>
		PRF_NONCLIENT = 2,

		/// <summary>
		/// Draws the client area of the window
		/// </summary>
		PRF_CLIENT = 4,

		/// <summary>
		/// Erases the background before drawing the window
		/// </summary>
		PRF_ERASEBKGND = 8,

		/// <summary>
		/// Draws all visible children windows
		/// </summary>
		PRF_CHILDREN = 16,

		/// <summary>
		/// Draws all owned windows
		/// </summary>
		PRF_OWNED = 32
	}

	#endregion

	#region LoadLibraryEx

	/// <summary>
	/// The LoadLibraryExFlags enemeration contains flags that control 
	/// how a .dll file is loaded with the NativeMethods.LoadLibraryEx 
	/// function
	/// </summary>
	public enum LoadLibraryExFlags
	{
		/// <summary>
		/// If this value is used, and the executable module is a DLL, 
		/// the system does not call DllMain for process and thread 
		/// initialization and termination. Also, the system does not 
		/// load additional executable modules that are referenced by 
		/// the specified module. If this value is not used, and the 
		/// executable module is a DLL, the system calls DllMain for 
		/// process and thread initialization and termination. The system 
		/// loads additional executable modules that are referenced by 
		/// the specified module
		/// </summary>
		DONT_RESOLVE_DLL_REFERENCES = 1,

		/// <summary>
		/// If this value is used, the system maps the file into the calling 
		/// process's virtual address space as if it were a data file. Nothing 
		/// is done to execute or prepare to execute the mapped file. Use 
		/// this flag when you want to load a DLL only to extract messages 
		/// or resources from it
		/// </summary>
		LOAD_LIBRARY_AS_DATAFILE = 2,

		/// <summary>
		/// If this value is used, and lpFileName specifies a path, the 
		/// system uses the alternate file search strategy to find associated 
		/// executable modules that the specified module causes to be loaded. 
		/// If this value is not used, or if lpFileName does not specify a 
		/// path, the system uses the standard search strategy to find 
		/// associated executable modules that the specified module causes 
		/// to be loaded
		/// </summary>
		LOAD_WITH_ALTERED_SEARCH_PATH = 8,

		/// <summary>
		/// If this value is used, the system does not perform automatic 
		/// trust comparisons on the DLL or its dependents when they are 
		/// loaded
		/// </summary>
		LOAD_IGNORE_CODE_AUTHZ_LEVEL = 16
	}

	#endregion

	#region SetErrorMode

	/// <summary>
	/// The SetErrorModeFlags enemeration contains flags that control 
	/// whether the system will handle the specified types of serious errors, 
	/// or whether the process will handle them
	/// </summary>
	public enum SetErrorModeFlags
	{
		/// <summary>
		/// Use the system default, which is to display all error dialog boxes
		/// </summary>
		SEM_DEFAULT = 0,

		/// <summary>
		/// The system does not display the critical-error-handler message box. 
		/// Instead, the system sends the error to the calling process
		/// </summary>
		SEM_FAILCRITICALERRORS = 1,

		/// <summary>
		/// The system does not display the general-protection-fault message box. 
		/// This flag should only be set by debugging applications that handle 
		/// general protection (GP) faults themselves with an exception handler
		/// </summary>
		SEM_NOGPFAULTERRORBOX = 2,

		/// <summary>
		/// After this value is set for a process, subsequent attempts to clear 
		/// the value are ignored. 64-bit Windows:  The system automatically fixes 
		/// memory alignment faults and makes them invisible to the application. 
		/// It does this for the calling process and any descendant processes
		/// </summary>
		SEM_NOALIGNMENTFAULTEXCEPT = 4,

		/// <summary>
		/// The system does not display a message box when it fails to find a 
		/// file. Instead, the error is returned to the calling process
		/// </summary>
		SEM_NOOPENFILEERRORBOX = 32768
	}

	#endregion

	#region DrawTextFlags

	/// <summary>
	/// 
	/// </summary>
	public enum DrawTextFlags
	{
		/// <summary>
		/// Justifies the text to the top of the rectangle.
		/// </summary>
		DT_TOP = 0x00000000,

		/// <summary>
		/// Aligns text to the left.
		/// </summary>
		DT_LEFT = 0x00000000,

		/// <summary>
		/// Centers text horizontally in the rectangle
		/// </summary>
		DT_CENTER = 0x00000001,

		/// <summary>
		/// Aligns text to the right
		/// </summary>
		DT_RIGHT = 0x00000002,

		/// <summary>
		/// Centers text vertically. This value is used only with the DT_SINGLELINE value
		/// </summary>
		DT_VCENTER = 0x00000004,

		/// <summary>
		/// Justifies the text to the bottom of the rectangle. This value is used 
		/// only with the DT_SINGLELINE value
		/// </summary>
		DT_BOTTOM = 0x00000008,

		/// <summary>
		/// Breaks words. Lines are automatically broken between words if a word would 
		/// extend past the edge of the rectangle specified by the lpRect parameter. A 
		/// carriage return-line feed sequence also breaks the line. If this is not 
		/// specified, output is on one line
		/// </summary>
		DT_WORDBREAK = 0x00000010,

		/// <summary>
		/// Displays text on a single line only. Carriage returns and line feeds do not 
		/// break the line
		/// </summary>
		DT_SINGLELINE = 0x00000020,

		/// <summary>
		/// Expands tab characters. The default number of characters per tab is eight. 
		/// The DT_WORD_ELLIPSIS, DT_PATH_ELLIPSIS, and DT_END_ELLIPSIS values cannot be 
		/// used with the DT_EXPANDTABS value
		/// </summary>
		DT_EXPANDTABS = 0x00000040,

		/// <summary>
		/// Sets tab stops. Bits 15? (high-order byte of the low-order word) of the uFormat 
		/// parameter specify the number of characters for each tab. The default number of 
		/// characters per tab is eight. The DT_CALCRECT, DT_EXTERNALLEADING, DT_INTERNAL, 
		/// DT_NOCLIP, and DT_NOPREFIX values cannot be used with the DT_TABSTOP value
		/// </summary>
		DT_TABSTOP = 0x00000080,

		/// <summary>
		/// Draws without clipping. DrawText is somewhat faster when DT_NOCLIP is used
		/// </summary>
		DT_NOCLIP = 0x00000100,

		/// <summary>
		/// Includes the font external leading in line height. Normally, external leading 
		/// is not included in the height of a line of text
		/// </summary>
		DT_EXTERNALLEADING = 0x00000200,

		/// <summary>
		/// Determines the width and height of the rectangle. If there are multiple lines 
		/// of text, DrawText uses the width of the rectangle pointed to by the lpRect 
		/// parameter and extends the base of the rectangle to bound the last line of text. 
		/// If the largest word is wider than the rectangle, the width is expanded. If the 
		/// text is less than the width of the rectangle, the width is reduced. If there is 
		/// only one line of text, DrawText modifies the right side of the rectangle so that 
		/// it bounds the last character in the line. In either case, DrawText returns the 
		/// height of the formatted text but does not draw the text
		/// </summary>
		DT_CALCRECT = 0x00000400,

		/// <summary>
		/// Turns off processing of prefix characters. Normally, DrawText interprets the 
		/// mnemonic-prefix character &amp; as a directive to underscore the character that 
		/// follows, and the mnemonic-prefix characters &amp;&amp; as a directive to print a 
		/// single &amp;. By specifying DT_NOPREFIX, this processing is turned off
		/// </summary>
		DT_NOPREFIX = 0x00000800,

		/// <summary>
		/// Uses the system font to calculate text metrics
		/// </summary>
		DT_INTERNAL = 0x00001000,

		/// <summary>
		/// Duplicates the text-displaying characteristics of a multiline edit control. 
		/// Specifically, the average character width is calculated in the same manner as 
		/// for an edit control, and the function does not display a partially visible last 
		/// line
		/// </summary>
		DT_EDITCONTROL = 0x00002000,

		/// <summary>
		/// For displayed text, replaces characters in the middle of the string with ellipses 
		/// so that the result fits in the specified rectangle. If the string contains backslash 
		/// (\) characters, DT_PATH_ELLIPSIS preserves as much as possible of the text after 
		/// the last backslash. The string is not modified unless the DT_MODIFYSTRING flag is 
		/// specified
		/// </summary>
		DT_PATH_ELLIPSIS = 0x00004000,

		/// <summary>
		/// For displayed text, if the end of a string does not fit in the rectangle, it is 
		/// truncated and ellipses are added. If a word that is not at the end of the string 
		/// goes beyond the limits of the rectangle, it is truncated without ellipses. The 
		/// string is not modified unless the DT_MODIFYSTRING flag is specified
		/// </summary>
		DT_END_ELLIPSIS = 0x00008000,

		/// <summary>
		/// Modifies the specified string to match the displayed text. This value has no effect 
		/// unless DT_END_ELLIPSIS or DT_PATH_ELLIPSIS is specified
		/// </summary>
		DT_MODIFYSTRING = 0x00010000,

		/// <summary>
		/// Layout in right-to-left reading order for bi-directional text when the font selected 
		/// into the hdc is a Hebrew or Arabic font. The default reading order for all text is 
		/// left-to-right
		/// </summary>
		DT_RTLREADING = 0x00020000,

		/// <summary>
		/// Truncates any word that does not fit in the rectangle and adds ellipses
		/// </summary>
		DT_WORD_ELLIPSIS = 0x00040000
	}

	#endregion

	#endregion
}
