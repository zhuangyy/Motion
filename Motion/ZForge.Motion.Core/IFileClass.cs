using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Core
{
	public interface IFileClass
	{
		Motion.Core.RecordMark Mark { get; set; }
		string FileName { get; set; }
		DateTime TimeStamp { get; }
		long FileSize { get; }
		CameraClass Owner { get; }
		string Title { get; }
		string ID { get; }

		bool ExportToFile(string target);
		bool ExportToPath(string target);
		bool IsValid();
		bool Remove();
	}
}
