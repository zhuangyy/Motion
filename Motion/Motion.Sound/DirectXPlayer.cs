using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.AudioVideoPlayback;

namespace Motion.Sound
{
	public class DirectXPlayer : IAlarmPlayer, IDisposable
	{
		private string mFileName;
		private Audio mAudio = null;
		private bool mLoop = true;

		public DirectXPlayer()
		{
		}

		public string FileName
		{
			get { return mFileName; }
			set
			{
				mFileName = value;
			}
		}

		public bool Looping
		{
			get { return mLoop; }
			set
			{
				mLoop = value;
			}
		}

		private void mAudio_Ending(object sender, EventArgs e)
		{
			if (this.Looping)
			{
				mAudio.CurrentPosition = 0;
			}
		}

		#region IAlarmPlayer Members

		public bool Check()
		{
			try
			{
				Audio a = new Audio(this.FileName);
				a.Dispose();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool IsRunning
		{
			get
			{
				if (mAudio != null)
				{
					return mAudio.Playing;
				}
				return false;
			}
		}

		public void AlarmStart()
		{
			if (mAudio != null)
			{
				mAudio.Dispose();
			}
			mAudio = new Audio(mFileName);
			mAudio.Ending += new EventHandler(mAudio_Ending);
			mAudio.Play();
		}

		public void AlarmEnd()
		{
			if (mAudio != null)
			{
				if (mAudio.Playing)
				{
					mAudio.Stop();
				}
				mAudio.Dispose();
				mAudio = null;
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (mAudio != null)
			{
				mAudio.Dispose();
			}
			mAudio = null;
		}

		#endregion
	}
}
