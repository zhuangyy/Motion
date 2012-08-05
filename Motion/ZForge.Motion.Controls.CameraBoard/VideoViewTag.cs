using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Controls
{
	internal class VideoViewTag : IComparable
	{
		private string mId;
		private string mName;

		public VideoViewTag(string id, string name)
		{
			this.mId = id;
			this.mName = name;
		}

		public string Name
		{
			get { return this.mName; }
		}

		public string ID
		{
			get { return this.mId; }
		}

		public override string ToString()
		{
			return this.Name;
		}

		#region IComparable Members

		public int CompareTo(object obj)
		{
			/*
			if (obj is VideoViewTag)
			{
				VideoViewTag t = (VideoViewTag)obj;
				if (t.ID.Equals("0"))
				{
					return 1;
				}
				if (this.ID.Equals("0"))
				{
					return -1;
				}
			}
			*/
			return this.Name.CompareTo(obj.ToString());
		}

		#endregion
	}
}
