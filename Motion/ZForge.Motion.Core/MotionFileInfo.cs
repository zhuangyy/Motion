using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ZForge.Motion.Core
{
	public class MotionFileInfo
	{
		private string mFileName;
		private DateTimeEx mTimeStamp;
		private string mOwnerID;

		public MotionFileInfo(string filename)
		{
			this.mFileName = filename;
			this.Parse();
		}

		public string FileName
		{
			get
			{
				FileInfo i = new FileInfo(this.mFileName);
				return i.FullName;
			}
		}

		public DateTimeEx TimeStamp
		{
			get
			{
				return mTimeStamp;
			}
		}

		public string OwnerID
		{
			get
			{
				return mOwnerID;
			}
		}

		public RecordMark Mark
		{
			get
			{
				FileInfo i = new FileInfo(this.FileName);
				string[] secs = i.Name.Split(new char[] { '.' });
				if (secs.Length != 5)
				{
					return RecordMark.INVALID;
				}
				try
				{
					int v = Convert.ToInt32(secs[secs.Length - 3]);
					return (RecordMark)v;
				}
				catch (Exception)
				{
					return RecordMark.INVALID;
				}
			}
			set
			{
				if (this.Mark == value)
				{
					return;
				}
				FileInfo i = new FileInfo(this.FileName);
				string[] secs = i.Name.Split(new char[] { '.' });
				if (secs.Length != 5)
				{
					return;
				}
				string s = string.Format("{0}.{1}.{2}.{3}.{4}", secs[0], secs[1], (int)value, secs[3], secs[4]);
				i.MoveTo(i.DirectoryName + @"\" + s);
				this.mFileName = i.FullName;
			}
		}

		public bool Parse()
		{
			FileInfo i = new FileInfo(this.FileName);
			string[] secs = i.Name.Split(new char[] { '.' });
			if (secs.Length != 5)
			{
				return false;
			}
			try
			{
				string s = secs[secs.Length - 2];
				long n = Convert.ToInt32(s);
				this.mTimeStamp = new DateTimeEx(n);

				this.mOwnerID = string.Format("{0}.{1}", secs[0], secs[1]);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public bool IsValid()
		{
			return (this.OwnerID != null);
		}
	}
}
