using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Motion.Core;
using ZForge.Controls.Logs;

namespace ZForge.Motion.Controls
{
	public class ExtendedLogViewerTag : IComparable
	{
		private string mID;
		private ZForge.Controls.Logs.LogLevel mLevel;
		private string mViewName;

		public ExtendedLogViewerTag(ZForge.Controls.Logs.LogLevel l, string id, string n)
		{
			this.mID = id;
			this.mLevel = l;
			this.mViewName = n;
		}

		public string ID
		{
			get { return this.mID; }
		}

		public ZForge.Controls.Logs.LogLevel LogLevel
		{
			get { return this.mLevel; }
		}

		public string ViewName
		{
			get { return this.mViewName; }
		}

		public override string ToString()
		{
			if (this.ViewName != null)
			{
				return this.ViewName;
			}
			else
			{
				return this.ID;
			}
		}

		#region IComparable Members

		public int CompareTo(object obj)
		{
			if (obj is ExtendedLogViewerTag)
			{
				ExtendedLogViewerTag t = (ExtendedLogViewerTag)obj;
				if (t.ID.Equals("0"))
				{
					return 1;
				}
				if (this.ID.Equals("0"))
				{
					return -1;
				}
			}
			return this.ToString().CompareTo(obj.ToString());
		}

		#endregion
	}
}
