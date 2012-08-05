using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZForge.Globalization;
using ZForge.Motion.Util;

namespace ZForge.Motion.Core
{
	public class AVIClass : AForge.Video.VFW.AVIReader, IFileClass
	{
		private string mFileName;
		private CameraClass mOwner;

		public AVIClass(string file, CameraClass o)
			: base()
		{
			this.FileName = file;
			this.mOwner = o;
		}

		#region Properties

		public CameraClass Camera
		{
			get 
			{
				CameraClass c = new CameraClass();
				c.Name = this.Owner.Name;
				c.ID = c.GenID(this.FileName);
				return c;
			}
		}

		#endregion

		public void Open()
		{
			base.Open(this.FileName);
		}

		#region IFileClass Members

		public bool ExportToFile(string fileName)
		{
			try
			{
				System.IO.File.Copy(this.FileName, fileName, true);
				return true;
			}
			catch (System.IO.IOException ex)
			{
				string s = string.Format(Translator.Instance.T("导出录像[{0}]失败."), this.Title);
				MessageBox.Show(s + "\n" + ex.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		public bool ExportToPath(string path)
		{
			string fname = this.Title.Replace(":", "-") + ".avi";
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

		public string FileName
		{
			get { return this.mFileName; }
			set
			{
				FileInfo fi = new FileInfo(value);
				this.mFileName = fi.FullName;
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

		public DateTime TimeStamp
		{
			get
			{
				MotionFileInfo fi = new MotionFileInfo(this.FileName);
				return fi.TimeStamp.DateTime;
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

		public string Title
		{
			get { return this.Owner.FullName + " " + this.TimeStamp; }
		}

		public string ID
		{
			get { return this.FileName.ToLower(); }
		}

		public bool IsValid()
		{
			MotionFileInfo fi = new MotionFileInfo(this.FileName);
			if (false == fi.IsValid())
			{
				return false;
			}
			try
			{
				this.Open();
				this.Close();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public bool Remove()
		{
			try
			{
				System.IO.File.Delete(this.FileName);
				//System.IO.File.Delete(this.FileName + @".xml");
				return true;
			}
			catch (Exception ex)
			{
				string s = string.Format(Translator.Instance.T("删除录像[{0}]失败."), this.Title);
				MessageBox.Show(s + "\n" + ex.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		#endregion
	}
}
