using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Controls
{
	public class AVIWriterEx : AForge.Video.VFW.AVIWriter
	{
		private string mFileName;

		public AVIWriterEx()
			: base()
		{
		}

		public AVIWriterEx(string codec)
			: base(codec)
		{
		}

		public string FileName
		{
			get { return this.mFileName; }
		}

		public new void Open(string fileName, int width, int height)
		{
			base.Open(fileName, width, height);
			this.mFileName = fileName;
		}
	}
}
