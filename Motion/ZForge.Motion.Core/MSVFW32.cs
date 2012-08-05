using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Core
{
  internal enum ICMODE
  {
    COMPRESS = 1,
    DECOMPRESS,
    FASTDECOMPRESS,
    QUERY,
    FASTCOMPRESS,
    DRAW = 8
  }

  internal enum VIDCF
  {
    QUALITY = 0x0001,
    CRUNCH = 0x0002,
    TEMPORAL = 0x0004,
    COMPRESSFRAMES = 0x0008,
    DRAW = 0x0010,
    FASTTEMPORALC = 0x0020,
    FASTTEMPORALD = 0x0080
  }

  [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
  internal struct ICINFO
  {
    /// DWORD->unsigned int
    public uint dwSize;
    /// DWORD->unsigned int
    public uint fccType;
    /// DWORD->unsigned int
    public uint fccHandler;
    /// DWORD->unsigned int
    public uint dwFlags;
    /// DWORD->unsigned int
    public uint dwVersion;
    /// DWORD->unsigned int
    public uint dwVersionICM;
    /// WCHAR[16]
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 16)]
    public string szName;
    /// WCHAR[128]
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 128)]
    public string szDescription;
    /// WCHAR[128]
    [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 128)]
    public string szDriver;
  }

  internal class MSVFW32
  {
    /// Return Type: BOOL->int
    ///fccType: DWORD->unsigned int
    ///fccHandler: DWORD->unsigned int
    ///lpicinfo: ICINFO*
    [System.Runtime.InteropServices.DllImportAttribute("MSVFW32.dll", EntryPoint = "ICInfo")]
    [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
    public static extern bool ICInfo(uint fccType, uint fccHandler, ref ICINFO lpicinfo);


    /// Return Type: LRESULT->LONG_PTR->int
    ///hic: int
    ///lpicinfo: ICINFO*
    ///cb: DWORD->unsigned int
    [System.Runtime.InteropServices.DllImportAttribute("MSVFW32.dll", EntryPoint = "ICGetInfo")]
    public static extern int ICGetInfo(System.IntPtr hic, ref ICINFO lpicinfo, uint cb);


    /// Return Type: int
    ///fccType: DWORD->unsigned int
    ///fccHandler: DWORD->unsigned int
    ///wMode: UINT->unsigned int
    [System.Runtime.InteropServices.DllImportAttribute("MSVFW32.dll", EntryPoint = "ICOpen")]
    public static extern System.IntPtr ICOpen(uint fccType, uint fccHandler, ICMODE wMode);

    /// Return Type: LRESULT->LONG_PTR->int
    ///hic: int
    [System.Runtime.InteropServices.DllImportAttribute("MSVFW32.dll", EntryPoint = "ICClose")]
    public static extern int ICClose(System.IntPtr hic);
  }
}
