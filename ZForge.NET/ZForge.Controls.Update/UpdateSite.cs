using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.Update
{
	public class UpdateSite
	{
		private string mDescription;
		private string mURL;
		private bool mEditable = false;
		private bool mDefault = false;

		public UpdateSite(string url, string descr)
		{
			this.mDescription = descr;
			this.mURL = url;
		}

		public UpdateSite(string url, string descr, bool uneditable)
		{
			this.mDescription = descr;
			this.mURL = url;
			this.mEditable = uneditable;
		}

		public bool IsUneditable
		{
			get { return this.mEditable; }
		}

		public bool IsDefault
		{
			get { return this.mDefault; }
			set { this.mDefault = value; }
		}

		public string Description
		{
			get { return mDescription; }
			set { mDescription = value; }
		}

		public string URL
		{
			get { return mURL.TrimEnd(new char[] { '/' }); }
			set { mURL = value; }
		}

		public override string ToString()
		{
			return string.Format("{0} ({1})", this.Description, this.URL);
		}
	}
}
