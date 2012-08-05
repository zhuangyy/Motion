using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Collections;

//#////////////////////////////////////////#//
//# Iterative Version of xDirectory.Copy() #//
//# Coder: John D. Storer II               #//
//# Date:  Thursday, May 18th, 2006        #//
//# Tool:  Visual C# 2005 Express Edition  #//
//#////////////////////////////////////////#//

namespace ZForge.Win32
{
	public class DirectoryXArgs
	{
		private int mLimit = 0;
		private List<string> mFileFilters = new List<string>();
		private List<string> mDirFilters = new List<string>();

		public int Limit
		{
			get { return mLimit; }
			set { mLimit = value; }
		}

		public List<string> FileFilters
		{
			get { return mFileFilters; }
		}

		public List<string> DirectoryFilters
		{
			get { return mDirFilters; }
		}
	}

	public class DirectoryXCopyArgs : DirectoryXArgs
	{
		private DirectoryInfo mSrc;
		private DirectoryInfo mDst;
		private bool mOverwrite = true;
		private bool mRollback = false;
		private string mRollBackTempExtension = ".temp";

		public DirectoryInfo Source
		{
			get { return this.mSrc; }
			set { this.mSrc = value; }
		}

		public DirectoryInfo Target
		{
			get { return this.mDst; }
			set { this.mDst = value; }
		}

		public bool Overwrite
		{
			get { return mOverwrite; }
			set { mOverwrite = value; }
		}

		public bool Rollback
		{
			get { return mRollback; }
			set { mRollback = value; }
		}

		public string RollbackTempExtension
		{
			get { return mRollBackTempExtension; }
			set { mRollBackTempExtension = value; }
		}
	}

	public class DirectoryXDeleteArgs : DirectoryXArgs
	{
		private DirectoryInfo mTarget;
		private bool mForce = true;

		public DirectoryInfo Target
		{
			get { return this.mTarget; }
			set { this.mTarget = value; }
		}

		public bool Force
		{
			get { return mForce; }
			set { mForce = value; }
		}
	}

	/// <summary>
	/// xDirectory v2.0 - Copy a Source Directory and it's SubDirectories/Files.
	/// Coder: John Storer II
	/// Date: Thursday, May 18, 2006
	/// </summary>
	public class DirectoryX
	{
		///////////////////////////////////////////////////////////
		/////////////////// String Copy Methods ///////////////////
		///////////////////////////////////////////////////////////

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="sSource">The Source Directory</param>
		/// <param name="sDestination">The Destination Directory</param>
		public static void Copy(string sSource, string sDestination)
		{
			Copy(new DirectoryInfo(sSource), new DirectoryInfo(sDestination), null, null, true, 0);
		}

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="sSource">The Source Directory</param>
		/// <param name="sDestination">The Destination Directory</param>
		/// <param name="Overwrite">Whether or not to Overwrite a Destination File if it Exists.</param>
		public static void Copy(string sSource, string sDestination, bool Overwrite)
		{
			Copy(new DirectoryInfo(sSource), new DirectoryInfo(sDestination), null, null, Overwrite, 0);
		}

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="sSource">The Source Directory</param>
		/// <param name="sDestination">The Destination Directory</param>
		/// <param name="FileFilter">The File Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		public static void Copy(string sSource, string sDestination, string FileFilter)
		{
			Copy(new DirectoryInfo(sSource), new DirectoryInfo(sDestination), FileFilter, null, true, 0);
		}

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="sSource">The Source Directory</param>
		/// <param name="sDestination">The Destination Directory</param>
		/// <param name="FileFilter">The File Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		/// <param name="Overwrite">Whether or not to Overwrite a Destination File if it Exists.</param>
		public static void Copy(string sSource, string sDestination, string FileFilter, bool Overwrite)
		{
			Copy(new DirectoryInfo(sSource), new DirectoryInfo(sDestination), FileFilter, null, Overwrite, 0);
		}

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="sSource">The Source Directory</param>
		/// <param name="sDestination">The Destination Directory</param>
		/// <param name="FileFilter">The File Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		/// <param name="DirectoryFilter">The Directory Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		public static void Copy(string sSource, string sDestination, string FileFilter, string DirectoryFilter)
		{
			Copy(new DirectoryInfo(sSource), new DirectoryInfo(sDestination), FileFilter, DirectoryFilter, true, 0);
		}

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="sSource">The Source Directory</param>
		/// <param name="sDestination">The Destination Directory</param>
		/// <param name="FileFilter">The File Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		/// <param name="DirectoryFilter">The Directory Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		/// <param name="Overwrite">Whether or not to Overwrite a Destination File if it Exists.</param>
		public static void Copy(string sSource, string sDestination, string FileFilter, string DirectoryFilter, bool Overwrite)
		{
			Copy(new DirectoryInfo(sSource), new DirectoryInfo(sDestination), FileFilter, DirectoryFilter, Overwrite, 0);
		}

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="sSource">The Source Directory</param>
		/// <param name="sDestination">The Destination Directory</param>
		/// <param name="FileFilter">The File Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		/// <param name="DirectoryFilter">The Directory Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		/// <param name="Overwrite">Whether or not to Overwrite a Destination File if it Exists.</param>
		/// <param name="FolderLimit">Iteration Limit - Total Number of Folders/SubFolders to Copy</param>
		public static void Copy(string sSource, string sDestination, string FileFilter, string DirectoryFilter, bool Overwrite, int FolderLimit)
		{
			Copy(new DirectoryInfo(sSource), new DirectoryInfo(sDestination), FileFilter, DirectoryFilter, Overwrite, FolderLimit);
		}

		//////////////////////////////////////////////////////////////////
		/////////////////// DirectoryInfo Copy Methods ///////////////////
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="diSource">The Source Directory</param>
		/// <param name="diDestination">The Destination Directory</param>
		public static void Copy(DirectoryInfo diSource, DirectoryInfo diDestination)
		{
			Copy(diSource, diDestination, null, null, true, 0);
		}

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="diSource">The Source Directory</param>
		/// <param name="diDestination">The Destination Directory</param>
		/// <param name="Overwrite">Whether or not to Overwrite a Destination File if it Exists.</param>
		public static void Copy(DirectoryInfo diSource, DirectoryInfo diDestination, bool Overwrite)
		{
			Copy(diSource, diDestination, null, null, Overwrite, 0);
		}

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="diSource">The Source Directory</param>
		/// <param name="diDestination">The Destination Directory</param>
		/// <param name="FileFilter">The File Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		public static void Copy(DirectoryInfo diSource, DirectoryInfo diDestination, string FileFilter)
		{
			Copy(diSource, diDestination, FileFilter, null, true, 0);
		}

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="diSource">The Source Directory</param>
		/// <param name="diDestination">The Destination Directory</param>
		/// <param name="FileFilter">The File Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		/// <param name="Overwrite">Whether or not to Overwrite a Destination File if it Exists.</param>
		public static void Copy(DirectoryInfo diSource, DirectoryInfo diDestination, string FileFilter, bool Overwrite)
		{
			Copy(diSource, diDestination, FileFilter, null, Overwrite, 0);
		}

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="diSource">The Source Directory</param>
		/// <param name="diDestination">The Destination Directory</param>
		/// <param name="FileFilter">The File Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		/// <param name="DirectoryFilter">The Directory Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		public static void Copy(DirectoryInfo diSource, DirectoryInfo diDestination, string FileFilter, string DirectoryFilter)
		{
			Copy(diSource, diDestination, FileFilter, DirectoryFilter, true, 0);
		}

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="diSource">The Source Directory</param>
		/// <param name="diDestination">The Destination Directory</param>
		/// <param name="FileFilter">The File Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		/// <param name="DirectoryFilter">The Directory Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		/// <param name="Overwrite">Whether or not to Overwrite a Destination File if it Exists.</param>
		public static void Copy(DirectoryInfo diSource, DirectoryInfo diDestination, string FileFilter, string DirectoryFilter, bool Overwrite)
		{
			Copy(diSource, diDestination, FileFilter, DirectoryFilter, Overwrite, 0);
		}

		/// <summary>
		/// xDirectory.Copy() - Copy a Source Directory and it's SubDirectories/Files
		/// </summary>
		/// <param name="diSource">The Source Directory</param>
		/// <param name="diDestination">The Destination Directory</param>
		/// <param name="FileFilter">The File Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		/// <param name="DirectoryFilter">The Directory Filter (Standard Windows Filter Parameter, Wildcards: "*" and "?")</param>
		/// <param name="Overwrite">Whether or not to Overwrite a Destination File if it Exists.</param>
		/// <param name="FolderLimit">Iteration Limit - Total Number of Folders/SubFolders to Copy</param>
		public static void Copy(DirectoryInfo diSource, DirectoryInfo diDestination, string FileFilter, string DirectoryFilter, bool Overwrite, int FolderLimit)
		{
			DirectoryXCopyArgs args = new DirectoryXCopyArgs();
			args.Source = diSource;
			args.Target = diDestination;
			args.DirectoryFilters.Add((DirectoryFilter != null && DirectoryFilter != string.Empty) ? DirectoryFilter : "*");
			args.FileFilters.Add((FileFilter != null && FileFilter != string.Empty) ? FileFilter : "*");
			args.Overwrite = Overwrite;
			args.Limit = FolderLimit;

			Copy(args);
		}

		/////////////////////////////////////////////////////////////////////
		/////////////////// The xDirectory.Copy() Method! ///////////////////
		/////////////////////////////////////////////////////////////////////

		public static void Copy(DirectoryXCopyArgs args)
		{
			List<DirectoryInfo> diSourceList = new List<DirectoryInfo>();
			List<FileInfo> fiSourceList = new List<FileInfo>();

			if (args.Source == null)
				throw new ArgumentException("Source Directory: NULL");
			if (args.Target == null)
				throw new ArgumentException("Target Directory: NULL");
			if (!args.Source.Exists)
				throw new IOException("Source Directory: Does Not Exist");
			if (args.Limit < 0)
				throw new ArgumentException("Folder Limit: Less Than 0");

			try
			{
				if (args.DirectoryFilters.Count == 0)
				{
					args.DirectoryFilters.Add("*");
				}
				if (args.FileFilters.Count == 0)
				{
					args.FileFilters.Add("*");
				}
				///// Add Source Directory to List /////
				diSourceList.Add(args.Source);
				Load(args, diSourceList, fiSourceList);

				///// Second Section: Create Folders from Listing /////
				foreach (DirectoryInfo di in diSourceList)
				{
					if (di.Exists)
					{
						string sFolderPath = args.Target.FullName + @"\" + di.FullName.Remove(0, args.Source.FullName.Length);

						///// Prevent Silly IOException /////
						if (!Directory.Exists(sFolderPath))
							Directory.CreateDirectory(sFolderPath);
					}
				}

				///// Third Section: Copy Files from Listing /////
				foreach (FileInfo fi in fiSourceList)
				{
					if (fi.Exists)
					{
						string sFilePath = args.Target.FullName + @"\" + fi.FullName.Remove(0, args.Source.FullName.Length);

						///// Better Overwrite Test W/O IOException from CopyTo() /////
						if (args.Overwrite)
						{
							if (args.Rollback)
							{
								string tmp = sFilePath + args.RollbackTempExtension;
								if (File.Exists(sFilePath))
								{
									if (File.Exists(tmp))
									{
										File.Delete(tmp);
									}
									File.Move(sFilePath, tmp);
								}
								try
								{
									fi.CopyTo(sFilePath, true);
									if (File.Exists(tmp))
									{
										try
										{
											File.Delete(tmp);
										}
										catch
										{
										}
									}
								}
								catch (Exception ex)
								{
									if (File.Exists(tmp) && false == File.Exists(sFilePath))
									{
										File.Move(tmp, sFilePath);
									}
									throw ex;
								}
							}
							else
							{
								fi.CopyTo(sFilePath, true);
							}
						}
						else
						{
							///// Prevent Silly IOException /////
							if (!File.Exists(sFilePath))
								fi.CopyTo(sFilePath, true);
						}
					}
				}
			}
			catch
			{ throw; }
		}

		public static void Delete(DirectoryInfo diTarget, bool force)
		{
			Delete(diTarget, null, null, force);
		}

		public static void Delete(DirectoryInfo diTarget, string fileFilter, string directoryFilter, bool force)
		{
			DirectoryXDeleteArgs args = new DirectoryXDeleteArgs();
			args.Target = diTarget;
			args.Force = force;
			if (fileFilter != null && fileFilter != string.Empty)
			{
				args.FileFilters.Add(fileFilter);
			}
			args.DirectoryFilters.Add((directoryFilter != null && directoryFilter != string.Empty) ? directoryFilter : "*");
						
			Delete(args);
		}

		public static void Delete(DirectoryXDeleteArgs args)
		{
			List<DirectoryInfo> diList = new List<DirectoryInfo>();
			List<FileInfo> fiList = new List<FileInfo>();
			try
			{
				if (args.DirectoryFilters.Count == 0)
				{
					args.DirectoryFilters.Add("*");
				}
				///// Add Target Directory to List /////
				diList.Add(args.Target);
				///// First Section: Get Folder/File Listing /////
				Load(args, diList, fiList);

				///// Second Section: Delete Files /////
				if (fiList.Count > 0)
				{
					foreach (FileInfo fi in fiList)
					{
						if (fi.IsReadOnly && args.Force)
						{
							fi.Attributes = fi.Attributes & (~FileAttributes.ReadOnly);
						}
						fi.Delete();
					}
				}
				else
				{
					diList.Remove(args.Target);
					foreach (DirectoryInfo di in diList)
					{
						if (di.Exists)
						{
							di.Delete(true);
						}
					}
				}
			}
			catch
			{ throw; }
		}

		public static void ClearFileAttributes(string path, FileAttributes attrib)
		{
			string[] SubDirectories = Directory.GetDirectories(path);
			if (SubDirectories.Length == 0)
			{
				DirectoryInfo di = new DirectoryInfo(path);
				FileInfo[] fis = di.GetFiles();
				foreach (FileInfo fi in fis)
				{
					fi.Attributes = fi.Attributes & (~attrib);
				}
			}
			else
			{
				for (int i = 0; i < SubDirectories.Length; i++)
				{
					ClearFileAttributes(SubDirectories[i], attrib);
				}
			}
		}

		public static int Compare(string d1, string d2)
		{
			DirectoryInfo a = new DirectoryInfo(d1);
			DirectoryInfo b = new DirectoryInfo(d2);
			string s1 = a.FullName.TrimEnd(@"\".ToCharArray());
			string s2 = b.FullName.TrimEnd(@"\".ToCharArray());
			s1 = s1.ToLower();
			s2 = s2.ToLower();
			return s1.CompareTo(s2);
		}

		private static int Load(DirectoryXArgs args, List<DirectoryInfo> diList, List<FileInfo> fiList)
		{
			int iterator = 0;

			///// First Section: Get Folder/File Listing /////
			while (iterator < diList.Count && (args.Limit == 0 || iterator < args.Limit))
			{
				foreach (string dfilter in args.DirectoryFilters)
				{
					foreach (DirectoryInfo di in diList[iterator].GetDirectories(dfilter))
						diList.Add(di);

					foreach (string ffilter in args.FileFilters)
					{
						foreach (FileInfo fi in diList[iterator].GetFiles(ffilter))
							fiList.Add(fi);
					}
				}
				iterator++;
			}
			return iterator;
		}
	}
}
