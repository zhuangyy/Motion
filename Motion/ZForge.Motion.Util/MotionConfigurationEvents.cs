using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Util
{
	public delegate void StorageChangedEventHandler(object sender, StorageChangedEventArgs e);

	public class StorageChangedEventArgs : EventArgs
	{
		private string mOldStorage;
		private string mNewStorage;

		public StorageChangedEventArgs(string oldStorage, string newStorage)
		{
			this.mOldStorage = oldStorage;
			this.mNewStorage = newStorage;
		}

		public string OldStorage
		{
			get
			{
				return this.mOldStorage;
			}
		}

		public string NewStorage
		{
			get
			{
				return this.mNewStorage;
			}
		}
	}
}
