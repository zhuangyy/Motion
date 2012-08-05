using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using ZForge.Globalization;
using ZForge.Motion.Util;

namespace ZForge.Motion.Core
{
	public class PICClass : IFileClass
	{
		private CameraClass mOwner;
		private string mFileName;

		public PICClass(string fileName, CameraClass owner)
		{
			this.FileName = fileName;
			mOwner = owner;
		}

		#region IFileClass Members

		public bool Remove()
		{
			try
			{
				System.IO.File.Delete(this.FileName);
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format(Translator.Instance.T("删除图片({0})失败."), this.Title) + "\n" + ex.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		public bool IsValid()
		{
			MotionFileInfo fi = new MotionFileInfo(this.FileName);
			if (false == fi.IsValid() || 0 == this.FileSize)
			{
				return false;
			}
			try
			{
				Bitmap b = new Bitmap(this.FileName);
				b.Dispose();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public string FileName
		{
			get { return this.mFileName; }
			set
			{
				FileInfo fi = new FileInfo(value);
				mFileName = fi.FullName;
			}
		}

		public long FileSize
		{
			get
			{
				FileInfo i = new FileInfo(this.FileName);
				return i.Length;
			}
		}

		public CameraClass Owner
		{
			get
			{
				if (this.mOwner == null)
				{
					MotionFileInfo fi = new MotionFileInfo(this.FileName);
					if (fi.OwnerID != null)
					{
						ItemClass v = RootClass.Instance.Find(fi.OwnerID);
						this.mOwner = v as CameraClass;
					}
				}
				return this.mOwner;
			}
		}

		public DateTime TimeStamp
		{
			get
			{
				MotionFileInfo fi = new MotionFileInfo(this.FileName);
				return fi.TimeStamp.DateTime;
			}
		}

		public string Title
		{
			get { return this.Owner.FullName + " " + this.TimeStamp; }
		}

		public string ID
		{
			get { return this.FileName.ToLower(); }
		}

		public bool ExportToFile(string fileName)
		{
			try
			{
				System.IO.File.Copy(this.FileName, fileName, true);
				return true;
			}
			catch (System.IO.IOException ex)
			{
				MessageBox.Show(string.Format(Translator.Instance.T("导出图片({0})失败."), this.Title) + "\n" + ex.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		public bool ExportToPath(string path)
		{
			FileInfo fi = new FileInfo(this.FileName);
			string fname = this.Title.Replace(":", "-") + fi.Extension;
			return this.ExportToFile(path + @"\" + fname);
		}

		public RecordMark Mark
		{
			get
			{
				MotionFileInfo i = new MotionFileInfo(this.FileName);
				return i.Mark;
			}
			set
			{
				MotionFileInfo i = new MotionFileInfo(this.FileName);
				i.Mark = value;
				this.FileName = i.FileName;
			}
		}

		#endregion
	}
}
