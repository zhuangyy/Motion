using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Core
{
  public class Codec
  {
    private UInt32 mFourCC;
    private string mDescription;

    public Codec(UInt32 fourcc, string description)
    {
      this.mFourCC = fourcc;
      this.mDescription = description;
    }

    public static UInt32 mmioFOURCC(char ch0, char ch1, char ch2, char ch3)
    {
      return ((UInt32)(byte)(ch0) | ((UInt32)(byte)(ch1) << 8) |
      ((UInt32)(byte)(ch2) << 16) | ((UInt32)(byte)(ch3) << 24));
    }

    public static string FOURCCmmio(UInt32 fcc)
    {
      byte[] bs = new byte[4];
      for (int n = 0; n < 4; n++)
      {
        bs[n] = (byte)(fcc >> (n * 8));
      }
      return System.Text.Encoding.Default.GetString(bs);
    }

    public string FourCC
    {
      get { return Codec.FOURCCmmio(this.mFourCC);  }
    }

    public string Description
    {
      get { return this.mDescription; }
    }

    public override string ToString()
    {
      return this.Description;
    }
  }
}
