// ZipEntry.cs
//
// Copyright (c) 2006, 2007 Microsoft Corporation.  All rights reserved.
//
// Part of an implementation of a zipfile class library. 
// See the file ZipFile.cs for further information.
//
// Tue, 27 Mar 2007  15:30


using System;

namespace ZForge.Zip
{
	/// <summary>
	/// Represents a single entry in a ZipFile. Typically, applications
	/// get a ZipEntry by enumerating the entries within a ZipFile. 
	/// </summary>
	public class ZipEntry
	{

		/// <summary>
		/// The time and date at which the file indicated by the ZipEntry was last modified. 
		/// </summary>
		public DateTime LastModified
		{
			get { return _LastModified; }
		}

		/// <summary>
		/// When this is set, this class trims the volume (eg C:\) from any 
		/// fully-qualified pathname on the ZipEntry, 
		/// before writing the ZipEntry into the ZipFile. 
		/// </summary>
		public bool TrimVolumeFromFullyQualifiedPaths
		{
			get { return _TrimVolumeFromFullyQualifiedPaths; }
			set { _TrimVolumeFromFullyQualifiedPaths = value; }
		}

		/// <summary>
		/// The name of the file contained in the ZipEntry. 
		/// </summary>
		public string LocalFileName
		{
			get { return _LocalFileName; }
		}

		/// <summary>
		/// The name of the file contained in the ZipEntry. 
		/// </summary>
		public string FileName
		{
			get { return _FileNameInArchive; }
		}
		/// <summary>
		/// The version of the zip engine needed to read the ZipEntry. 
		/// </summary>
		public Int16 VersionNeeded
		{
			get { return _VersionNeeded; }
		}

		/// <summary>
		/// a bitfield as defined in the zip spec. 
		/// </summary>
		public Int16 BitField
		{
			get { return _BitField; }
		}

		/// <summary>
		/// The compression method employed for this ZipEntry.
		/// </summary>
		public Int16 CompressionMethod
		{
			get { return _CompressionMethod; }
		}

		/// <summary>
		/// The compressed size of the file, within the zip archive.
		/// </summary>
		public Int32 CompressedSize
		{
			get { return _CompressedSize; }
		}

		/// <summary>
		/// The size of the file, before compression, or after extraction. 
		/// </summary>
		public Int32 UncompressedSize
		{
			get { return _UncompressedSize; }
		}

		/// <summary>
		/// The ratio of compressed size to uncompressed size. 
		/// </summary>
		public Double CompressionRatio
		{
			get
			{
				return 100 * (1.0 - (1.0 * CompressedSize) / (1.0 * UncompressedSize));
			}
		}

		private byte[] _FileData
		{
			get
			{
				if (__filedata == null)
				{
				}
				return __filedata;
			}
		}

		private System.IO.Compression.DeflateStream CompressedStream
		{
			get
			{
				if (_CompressedStream == null)
				{
					// we read from the underlying memory stream after data is written to the compressed stream
					_UnderlyingMemoryStream = new System.IO.MemoryStream();
					bool LeaveUnderlyingStreamOpen = true;

					// we write to the compressed stream, and compression happens as we write.
					_CompressedStream = new System.IO.Compression.DeflateStream(_UnderlyingMemoryStream,
																					System.IO.Compression.CompressionMode.Compress,
																					LeaveUnderlyingStreamOpen);
				}
				return _CompressedStream;
			}
		}

		internal byte[] Header
		{
			get
			{
				return _header;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool IsDirectory()
		{
			return FileName.EndsWith("/");
		}

		private static bool ReadHeader(System.IO.Stream s, ZipEntry ze)
		{
			int signature = ZForge.Zip.Shared.ReadSignature(s);

			// Return false if this is not a local file header signature.
			if (ZipEntry.IsNotValidSig(signature))
			{
				s.Seek(-4, System.IO.SeekOrigin.Current); // unread the signature
				// Getting "not a ZipEntry signature" is not always wrong or an error. 
				// This can happen when walking through a zipfile.  After the last compressed entry, 
				// we expect to read a ZipDirEntry signature.  When we get this is how we 
				// know we've reached the end of the compressed entries. 
				if (ZipDirEntry.IsNotValidSig(signature))
				{
					throw new Exception(String.Format("  ZipEntry::Read(): Bad signature ({0:X8}) at position  0x{1:X8}", signature, s.Position));
				}
				return false;
			}

			byte[] block = new byte[26];
			int n = s.Read(block, 0, block.Length);
			if (n != block.Length) return false;

			int i = 0;
			ze._VersionNeeded = (short)(block[i++] + block[i++] * 256);
			ze._BitField = (short)(block[i++] + block[i++] * 256);
			ze._CompressionMethod = (short)(block[i++] + block[i++] * 256);
			ze._LastModDateTime = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;

			// the PKZIP spec says that if bit 3 is set (0x0008), then the CRC, Compressed size, and uncompressed size
			// come directly after the file data.  The only way to find it is to scan the zip archive for the signature of 
			// the Data Descriptor, and presume that that signature does not appear in the (compressed) data of the compressed file.  

			if ((ze._BitField & 0x0008) != 0x0008)
			{
				ze._Crc32 = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;
				ze._CompressedSize = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;
				ze._UncompressedSize = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;
			}
			else
			{
				// the CRC, compressed size, and uncompressed size are stored later in the stream.
				// here, we advance the pointer.
				i += 12;
			}

			Int16 filenameLength = (short)(block[i++] + block[i++] * 256);
			Int16 extraFieldLength = (short)(block[i++] + block[i++] * 256);

			block = new byte[filenameLength];
			n = s.Read(block, 0, block.Length);
			ze._FileNameInArchive = ZForge.Zip.Shared.StringFromBuffer(block, 0, block.Length);

			// when creating an entry by reading, the LocalFileName is the same as the FileNameInArchivre
			ze._LocalFileName = ze._FileNameInArchive;

			ze._Extra = new byte[extraFieldLength];
			n = s.Read(ze._Extra, 0, ze._Extra.Length);

			// transform the time data into something usable
			ze._LastModified = ZForge.Zip.Shared.PackedToDateTime(ze._LastModDateTime);

			// actually get the compressed size and CRC if necessary
			if ((ze._BitField & 0x0008) == 0x0008)
			{
				long posn = s.Position;
				long SizeOfDataRead = ZForge.Zip.Shared.FindSignature(s, ZipConstants.ZipEntryDataDescriptorSignature);
				if (SizeOfDataRead == -1) return false;

				// read 3x 4-byte fields (CRC, Compressed Size, Uncompressed Size)
				block = new byte[12];
				n = s.Read(block, 0, block.Length);
				if (n != 12) return false;
				i = 0;
				ze._Crc32 = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;
				ze._CompressedSize = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;
				ze._UncompressedSize = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;

				if (SizeOfDataRead != ze._CompressedSize)
					throw new Exception("Data format error (bit 3 is set)");

				// seek back to previous position, to read file data
				s.Seek(posn, System.IO.SeekOrigin.Begin);
			}

			return true;
		}


		private static bool IsNotValidSig(int signature)
		{
			return (signature != ZipConstants.ZipEntrySignature);
		}

		/// <summary>
		/// Reads one ZipEntry from the given stream.  
		/// </summary>
		/// <param name="s">the stream to read from.</param>
		/// <returns>the ZipEntry read from the stream.</returns>
		public static ZipEntry Read(System.IO.Stream s)
		{
			ZipEntry entry = new ZipEntry();

			if (!ReadHeader(s, entry)) return null;

			entry.__filedata = new byte[entry.CompressedSize];
			int n = s.Read(entry._FileData, 0, entry._FileData.Length);
			if (n != entry._FileData.Length)
			{
				throw new Exception("badly formatted zip file.");
			}
			// finally, seek past the (already read) Data descriptor if necessary
			if ((entry._BitField & 0x0008) == 0x0008)
			{
				s.Seek(16, System.IO.SeekOrigin.Current);
			}
			return entry;
		}



		internal static ZipEntry Create(String filename)
		{
			return ZipEntry.Create(filename, null);
		}

		internal static ZipEntry Create(String filename, string DirectoryPathInArchive)
		{
			ZipEntry entry = new ZipEntry();
			entry._LocalFileName = filename;
			if (DirectoryPathInArchive == null)
				entry._FileNameInArchive = filename;
			else
			{
				// explicitly specify a pathname for this file  
				entry._FileNameInArchive =
						System.IO.Path.Combine(DirectoryPathInArchive, System.IO.Path.GetFileName(filename));
			}


			entry._LastModified = System.IO.File.GetLastWriteTime(filename);

			// adjust the time if the .NET BCL thinks it is in DST.  
			// see the note elsewhere in this file for more info. 
			if (entry._LastModified.IsDaylightSavingTime())
			{
				System.DateTime AdjustedTime = entry._LastModified - new System.TimeSpan(1, 0, 0);
				entry._LastModDateTime = ZForge.Zip.Shared.DateTimeToPacked(AdjustedTime);
			}
			else
				entry._LastModDateTime = ZForge.Zip.Shared.DateTimeToPacked(entry._LastModified);

			// we don't actually slurp in the file until the caller invokes Write on this entry.

			return entry;
		}


		/// <summary>
		/// Extract the current entry to the current working directory. 
		/// </summary>
		/// <remarks>
		/// <para>
		/// The last modified time of the created file may be adjusted 
		/// during extraction to compensate
		/// for differences in how the .NET Base Class Library deals
		/// with daylight saving time (DST) versus how the Windows
		/// filesystem deals with daylight saving time. 
		/// See http://blogs.msdn.com/oldnewthing/archive/2003/10/24/55413.aspx for more context.
		///</para>
		/// <para>
		/// In a nutshell: Daylight savings time rules change regularly.  In
		/// 2007, for example, the inception week of DST changed.  In 1977,
		/// DST was in place all year round. in 1945, likewise.  And so on.
		/// Win32 does not attempt to guess which time zone rules were in
		/// effect at the time in question.  It will render a time as
		/// "standard time" and allow the app to change to DST as necessary.
		///  .NET makes a different choice.
		///</para>
		/// <para>
		/// Compare the output of FileInfo.LastWriteTime.ToString("f") with
		/// what you see in the property sheet for a file that was last
		/// written to on the other side of the DST transition. For example,
		/// suppose the file was last modified on October 17, during DST but
		/// DST is not currently in effect. Explorer's file properties
		/// reports Thursday, October 17, 2003, 8:45:38 AM, but .NETs
		/// FileInfo reports Thursday, October 17, 2003, 9:45 AM.
		///</para>
		/// <para>
		/// Win32 says, "Thursday, October 17, 2002 8:45:38 AM PST". Note:
		/// Pacific STANDARD Time. Even though October 17 of that year
		/// occurred during Pacific Daylight Time, Win32 displays the time as
		/// standard time because that's what time it is NOW.
		///</para>
		/// <para>
		/// .NET BCL assumes that the current DST rules were in place at the
		/// time in question.  So, .NET says, "Well, if the rules in effect
		/// now were also in effect on October 17, 2003, then that would be
		/// daylight time" so it displays "Thursday, October 17, 2003, 9:45
		/// AM PDT" - daylight time.
		///</para>
		/// <para>
		/// So .NET gives a value which is more intuitively correct, but is
		/// also potentially incorrect, and which is not invertible. Win32
		/// gives a value which is intuitively incorrect, but is strictly
		/// correct.
		///</para>
		/// <para>
		/// With this adjustment, I add one hour to the tweaked .NET time, if
		/// necessary.  That is to say, if the time in question had occurred
		/// in what the .NET BCL assumed to be DST (an assumption that may be
		/// wrong given the constantly changing DST rules).
		/// </para>
		/// <overloads>This method has three overloads.</overloads>
		/// </remarks>
		public void Extract()
		{
			Extract(".");
		}

		/// <summary>
		/// Extracts the entry to the specified stream. For example, the caller could specify Console.Out.
		/// </summary>
		/// <param name="s">the stream to which the entry should be extracted.  </param>
		public void Extract(System.IO.Stream s)
		{
			Extract(null, s);
		}

		/// <summary>
		/// Extract the entry to the filesystem, starting at the specified base directory. 
		/// </summary>
		/// <param name="BaseDirectory">the pathname of the base directory</param>
		public void Extract(string BaseDirectory)
		{
			Extract(BaseDirectory, null);
		}


		// pass in either basedir or s, but not both. 
		// In other words, you can extract to a stream or to a directory, but not both!
		private void Extract(string basedir, System.IO.Stream s)
		{
			string TargetFile = null;
			if (basedir != null)
			{
				TargetFile = System.IO.Path.Combine(basedir, FileName);

				// check if a directory
				if (this.IsDirectory())
				{
					if (!System.IO.Directory.Exists(TargetFile))
						System.IO.Directory.CreateDirectory(TargetFile);
					return;
				}
			}
			else if (s != null)
			{
				if (this.IsDirectory())
					// extract a directory to streamwriter?  nothing to do!
					return;
			}
			else throw new Exception("Invalid input.");

			using (System.IO.MemoryStream memstream = new System.IO.MemoryStream(_FileData))
			{
				System.IO.Stream input = null;
				try
				{
					if (CompressedSize == UncompressedSize)
					{
						// the System.IO.Compression.DeflateStream class does not handle uncompressed data.
						// so if an entry is not compressed, then we just translate the bytes directly.
						input = memstream;
					}
					else
					{
						input = new System.IO.Compression.DeflateStream(memstream, System.IO.Compression.CompressionMode.Decompress);
					}

					if (TargetFile != null)
					{
						// ensure the target path exists
						if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(TargetFile)))
						{
							System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(TargetFile));
						}
					}

					System.IO.Stream output = null;
					try
					{
						if (TargetFile != null)
							output = new System.IO.FileStream(TargetFile, System.IO.FileMode.OpenOrCreate);
						else
							output = s;

						byte[] bytes = new byte[4096];
						int n;

						if (_Debug)
						{
							Console.WriteLine("{0}: _FileData.Length= {1}", TargetFile, _FileData.Length);
							Console.WriteLine("{0}: memstream.Position: {1}", TargetFile, memstream.Position);
							n = _FileData.Length;
							if (n > 1000)
							{
								n = 500;
								Console.WriteLine("{0}: truncating dump from {1} to {2} bytes...", TargetFile, _FileData.Length, n);
							}
							for (int j = 0; j < n; j += 2)
							{
								if ((j > 0) && (j % 40 == 0))
									System.Console.WriteLine();
								System.Console.Write(" {0:X2}", _FileData[j]);
								if (j + 1 < n)
									System.Console.Write("{0:X2}", _FileData[j + 1]);
							}
							System.Console.WriteLine("\n");
						}

						n = 1; // anything non-zero
						while (n != 0)
						{
							if (_Debug) Console.WriteLine("{0}: about to read...", TargetFile);
							n = input.Read(bytes, 0, bytes.Length);
							if (_Debug) Console.WriteLine("{0}: got {1} bytes", TargetFile, n);
							if (n > 0)
							{
								if (_Debug) Console.WriteLine("{0}: about to write...", TargetFile);
								output.Write(bytes, 0, n);
							}
						}
					}
					finally
					{
						// we only close the output stream if we opened it. 
						if ((output != null) && (TargetFile != null))
						{
							output.Close();
							output.Dispose();
						}
					}

					if (TargetFile != null)
					{
						// We may have to adjust the last modified time to compensate
						// for differences in how the .NET Base Class Library deals
						// with daylight saving time (DST) versus how the Windows
						// filesystem deals with daylight saving time. See 
						// http://blogs.msdn.com/oldnewthing/archive/2003/10/24/55413.aspx for some context. 

						// in a nutshell: Daylight savings time rules change regularly.  In
						// 2007, for example, the inception week of DST changed.  In 1977,
						// DST was in place all year round. in 1945, likewise.  And so on.
						// Win32 does not attempt to guess which time zone rules were in
						// effect at the time in question.  It will render a time as
						// "standard time" and allow the app to change to DST as necessary.
						//  .NET makes a different choice.

						// -------------------------------------------------------
						// Compare the output of FileInfo.LastWriteTime.ToString("f") with
						// what you see in the property sheet for a file that was last
						// written to on the other side of the DST transition. For example,
						// suppose the file was last modified on October 17, during DST but
						// DST is not currently in effect. Explorer's file properties
						// reports Thursday, October 17, 2003, 8:45:38 AM, but .NETs
						// FileInfo reports Thursday, October 17, 2003, 9:45 AM.

						// Win32 says, "Thursday, October 17, 2002 8:45:38 AM PST". Note:
						// Pacific STANDARD Time. Even though October 17 of that year
						// occurred during Pacific Daylight Time, Win32 displays the time as
						// standard time because that's what time it is NOW.

						// .NET BCL assumes that the current DST rules were in place at the
						// time in question.  So, .NET says, "Well, if the rules in effect
						// now were also in effect on October 17, 2003, then that would be
						// daylight time" so it displays "Thursday, October 17, 2003, 9:45
						// AM PDT" - daylight time.

						// So .NET gives a value which is more intuitively correct, but is
						// also potentially incorrect, and which is not invertible. Win32
						// gives a value which is intuitively incorrect, but is strictly
						// correct.
						// -------------------------------------------------------

						// With this adjustment, I add one hour to the tweaked .NET time, if
						// necessary.  That is to say, if the time in question had occurred
						// in what the .NET BCL assumed to be DST (an assumption that may be
						// wrong given the constantly changing DST rules).

						if (LastModified.IsDaylightSavingTime())
						{
							DateTime AdjustedLastModified = LastModified + new System.TimeSpan(1, 0, 0);
							System.IO.File.SetLastWriteTime(TargetFile, AdjustedLastModified);
						}
						else
							System.IO.File.SetLastWriteTime(TargetFile, LastModified);
					}

				}
				finally
				{
					// we only close the output stream if we opened it. 
					// we cannot use using() here because in some cases we do not want to Dispose the stream!
					if ((input != null) && (input != memstream))
					{
						input.Close();
						input.Dispose();
					}
				}
			}
		}


		internal void WriteCentralDirectoryEntry(System.IO.Stream s)
		{
			byte[] bytes = new byte[4096];
			int i = 0;
			// signature
			bytes[i++] = (byte)(ZipConstants.ZipDirEntrySignature & 0x000000FF);
			bytes[i++] = (byte)((ZipConstants.ZipDirEntrySignature & 0x0000FF00) >> 8);
			bytes[i++] = (byte)((ZipConstants.ZipDirEntrySignature & 0x00FF0000) >> 16);
			bytes[i++] = (byte)((ZipConstants.ZipDirEntrySignature & 0xFF000000) >> 24);

			// Version Made By
			bytes[i++] = Header[4];
			bytes[i++] = Header[5];

			// Version Needed, Bitfield, compression method, lastmod,
			// crc, sizes, filename length and extra field length -
			// are all the same as the local file header. So just copy them
			int j = 0;
			for (j = 0; j < 26; j++)
				bytes[i + j] = Header[4 + j];

			i += j;  // positioned at next available byte

			// File Comment Length
			bytes[i++] = 0;
			bytes[i++] = 0;

			// Disk number start
			bytes[i++] = 0;
			bytes[i++] = 0;

			// internal file attrs
			// TODO: figure out what is required here. 
			bytes[i++] = 1;
			bytes[i++] = 0;

			// external file attrs
			// TODO: figure out what is required here. 
			bytes[i++] = 0x20;
			bytes[i++] = 0;
			bytes[i++] = 0xb6;
			bytes[i++] = 0x81;

			// relative offset of local header (I think this can be zero)
			bytes[i++] = (byte)(_RelativeOffsetOfHeader & 0x000000FF);
			bytes[i++] = (byte)((_RelativeOffsetOfHeader & 0x0000FF00) >> 8);
			bytes[i++] = (byte)((_RelativeOffsetOfHeader & 0x00FF0000) >> 16);
			bytes[i++] = (byte)((_RelativeOffsetOfHeader & 0xFF000000) >> 24);

			if (_Debug) System.Console.WriteLine("\ninserting filename into CDS: (length= {0})", Header.Length - 30);
			// actual filename (starts at offset 34 in header) 
			for (j = 0; j < Header.Length - 30; j++)
			{
				bytes[i + j] = Header[30 + j];
				if (_Debug) System.Console.Write(" {0:X2}", bytes[i + j]);
			}
			if (_Debug) System.Console.WriteLine();
			i += j;

			s.Write(bytes, 0, i);
		}


		private void WriteHeader(System.IO.Stream s, byte[] bytes)
		{
			// write the header info

			int i = 0;
			// signature
			bytes[i++] = (byte)(ZipConstants.ZipEntrySignature & 0x000000FF);
			bytes[i++] = (byte)((ZipConstants.ZipEntrySignature & 0x0000FF00) >> 8);
			bytes[i++] = (byte)((ZipConstants.ZipEntrySignature & 0x00FF0000) >> 16);
			bytes[i++] = (byte)((ZipConstants.ZipEntrySignature & 0xFF000000) >> 24);

			// version needed
			Int16 FixedVersionNeeded = 0x14; // from examining existing zip files
			bytes[i++] = (byte)(FixedVersionNeeded & 0x00FF);
			bytes[i++] = (byte)((FixedVersionNeeded & 0xFF00) >> 8);

			// bitfield
			Int16 BitField = 0x00; // from examining existing zip files
			bytes[i++] = (byte)(BitField & 0x00FF);
			bytes[i++] = (byte)((BitField & 0xFF00) >> 8);

			Int16 CompressionMethod = 0x08; // 0x08 = Deflate, 0x00 == No Compression

			// CRC32 (Int32)
			if (_FileData != null)
			{
				// if we have FileData, that means we've read this entry from an
				// existing zip archive. We must just copy the existing file data, 
				// CRC, compressed size, and uncompressed size 
				// over to the new (updated) archive.  
			}
			else
			{
				// Read in the data from the file in the filesystem, comress it, and 
				// calculate a CRC on it as we read. 

				CRC32 crc32 = new CRC32();
				using (System.IO.Stream input = System.IO.File.OpenRead(LocalFileName))
				{
					UInt32 crc = crc32.GetCrc32AndCopy(input, CompressedStream);
					_Crc32 = (Int32)crc;
				}
				CompressedStream.Close();  // to get the footer bytes written to the underlying stream

				_UncompressedSize = crc32.TotalBytesRead;
				_CompressedSize = (Int32)_UnderlyingMemoryStream.Length;

				// It is possible that applying this stream compression on a previously compressed
				// file will actually increase the size of the data.  In that case, we back-off
				// and just store the uncompressed (really, already compressed) data.
				// We need to recompute the CRC, and point to the right data.
				if (_CompressedSize > _UncompressedSize)
				{
					using (System.IO.Stream input = System.IO.File.OpenRead(LocalFileName))
					{
						_UnderlyingMemoryStream = new System.IO.MemoryStream();
						UInt32 crc = crc32.GetCrc32AndCopy(input, _UnderlyingMemoryStream);
						_Crc32 = (Int32)crc;
					}
					_UncompressedSize = crc32.TotalBytesRead;
					_CompressedSize = (Int32)_UnderlyingMemoryStream.Length;
					if (_CompressedSize != _UncompressedSize) throw new Exception("No compression but unequal stream lengths!");
					CompressionMethod = 0x00;
				}
			}

			// compression method         
			bytes[i++] = (byte)(CompressionMethod & 0x00FF);
			bytes[i++] = (byte)((CompressionMethod & 0xFF00) >> 8);

			// LastMod
			bytes[i++] = (byte)(_LastModDateTime & 0x000000FF);
			bytes[i++] = (byte)((_LastModDateTime & 0x0000FF00) >> 8);
			bytes[i++] = (byte)((_LastModDateTime & 0x00FF0000) >> 16);
			bytes[i++] = (byte)((_LastModDateTime & 0xFF000000) >> 24);

			// calculated above
			bytes[i++] = (byte)(_Crc32 & 0x000000FF);
			bytes[i++] = (byte)((_Crc32 & 0x0000FF00) >> 8);
			bytes[i++] = (byte)((_Crc32 & 0x00FF0000) >> 16);
			bytes[i++] = (byte)((_Crc32 & 0xFF000000) >> 24);

			// CompressedSize (Int32)
			bytes[i++] = (byte)(_CompressedSize & 0x000000FF);
			bytes[i++] = (byte)((_CompressedSize & 0x0000FF00) >> 8);
			bytes[i++] = (byte)((_CompressedSize & 0x00FF0000) >> 16);
			bytes[i++] = (byte)((_CompressedSize & 0xFF000000) >> 24);

			// UncompressedSize (Int32)
			if (_Debug) System.Console.WriteLine("Uncompressed Size: {0}", _UncompressedSize);
			bytes[i++] = (byte)(_UncompressedSize & 0x000000FF);
			bytes[i++] = (byte)((_UncompressedSize & 0x0000FF00) >> 8);
			bytes[i++] = (byte)((_UncompressedSize & 0x00FF0000) >> 16);
			bytes[i++] = (byte)((_UncompressedSize & 0xFF000000) >> 24);

			// filename length (Int16)
			Int16 length = (Int16)FileName.Length;
			// see note below about TrimVolumeFromFullyQualifiedPaths.
			if ((TrimVolumeFromFullyQualifiedPaths) && (FileName[1] == ':') && (FileName[2] == '\\')) length -= 3;
			bytes[i++] = (byte)(length & 0x00FF);
			bytes[i++] = (byte)((length & 0xFF00) >> 8);

			// extra field length (short)
			Int16 ExtraFieldLength = 0x00;
			bytes[i++] = (byte)(ExtraFieldLength & 0x00FF);
			bytes[i++] = (byte)((ExtraFieldLength & 0xFF00) >> 8);

			// Tue, 27 Mar 2007  16:35

			// Creating a zip that contains entries with "fully qualified" pathnames
			// can result in a zip archive that is unreadable by Windows Explorer.
			// Such archives are valid according to other tools but not to explorer.
			// To avoid this, we can trim off the leading volume name and slash (eg
			// c:\) when creating (writing) a zip file.  We do this by default and we
			// leave the old behavior available with the
			// TrimVolumeFromFullyQualifiedPaths flag - set it to false to get the old
			// behavior.  It only affects zip creation.

			// actual filename
			char[] c = ((TrimVolumeFromFullyQualifiedPaths) && (FileName[1] == ':') && (FileName[2] == '\\')) ?
				FileName.Substring(3).ToCharArray() :  // trim off volume letter, colon, and slash
				FileName.ToCharArray();
			int j = 0;

			if (_Debug)
			{
				System.Console.WriteLine("local header: writing filename, {0} chars", c.Length);
				System.Console.WriteLine("starting offset={0}", i);
			}
			for (j = 0; (j < c.Length) && (i + j < bytes.Length); j++)
			{
				bytes[i + j] = System.BitConverter.GetBytes(c[j])[0];
				if (_Debug) System.Console.Write(" {0:X2}", bytes[i + j]);
			}
			if (_Debug) System.Console.WriteLine();

			i += j;

			// extra field (we always write nothing in this implementation)
			// ;;

			// remember the file offset of this header
			_RelativeOffsetOfHeader = (int)s.Length;

			if (_Debug)
			{
				System.Console.WriteLine("\nAll header data:");
				for (j = 0; j < i; j++)
					System.Console.Write(" {0:X2}", bytes[j]);
				System.Console.WriteLine();
			}
			// finally, write the header to the stream
			s.Write(bytes, 0, i);

			// preserve this header data for use with the central directory structure.
			_header = new byte[i];
			if (_Debug) System.Console.WriteLine("preserving header of {0} bytes", _header.Length);
			for (j = 0; j < i; j++)
				_header[j] = bytes[j];

		}


		internal void Write(System.IO.Stream s)
		{
			byte[] bytes = new byte[4096];
			int n;

			// write the header:
			WriteHeader(s, bytes);

			if (_Debug)
			{
				Console.WriteLine("{0}: writing compressed data to zipfile...", FileName);
				Console.WriteLine("{0}: total data length: {1}", FileName, _CompressedSize);
			}

			// write the actual file data: 
			if (_FileData != null)
			{
				// use the existing compressed data we read from the extant zip archive
				s.Write(_FileData, 0, _FileData.Length);
			}
			else
			{
				// rely on the compressed data we created in WriteHeader
				_UnderlyingMemoryStream.Position = 0;

				while ((n = _UnderlyingMemoryStream.Read(bytes, 0, bytes.Length)) != 0)
				{
					if (_Debug)
					{
						Console.WriteLine("{0}: transferring {1} bytes...", FileName, n);

						for (int j = 0; j < n; j += 2)
						{
							if ((j > 0) && (j % 40 == 0))
								System.Console.WriteLine();
							System.Console.Write(" {0:X2}", bytes[j]);
							if (j + 1 < n)
								System.Console.Write("{0:X2}", bytes[j + 1]);
						}
						System.Console.WriteLine("\n");
					}

					s.Write(bytes, 0, n);
				}

				//_CompressedStream.Close();
				//_CompressedStream= null;
				_UnderlyingMemoryStream.Close();
				_UnderlyingMemoryStream = null;
			}
		}



		private bool _Debug = false;

		private DateTime _LastModified;
		private bool _TrimVolumeFromFullyQualifiedPaths = true;  // by default, trim them.
		private string _LocalFileName;
		private string _FileNameInArchive;
		private Int16 _VersionNeeded;
		private Int16 _BitField;
		private Int16 _CompressionMethod;
		private Int32 _CompressedSize;
		private Int32 _UncompressedSize;
		private Int32 _LastModDateTime;
		private Int32 _Crc32;
		private byte[] _Extra;


		private byte[] __filedata;
		private System.IO.MemoryStream _UnderlyingMemoryStream;
		private System.IO.Compression.DeflateStream _CompressedStream;
		private byte[] _header;
		private int _RelativeOffsetOfHeader;

	}

}
