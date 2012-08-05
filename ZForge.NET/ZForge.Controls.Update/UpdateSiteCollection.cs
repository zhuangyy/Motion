using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.Update
{
	public class UpdateSiteCollection : List<UpdateSite>
	{
		public UpdateSite Default
		{
			get
			{
				foreach (UpdateSite s in this)
				{
					if (s.IsDefault)
					{
						return s;
					}
				}
				return null;
			}
			set
			{
				foreach (UpdateSite s in this)
				{
					s.IsDefault = (s == value);
				}
			}
		}
	}
}
