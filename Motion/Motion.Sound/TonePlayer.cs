using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Motion.Sound
{
	/// <summary>
	/// Summary description for CannedWCAtones.
	/// </summary>
	public class TonePlayer : IAlarmPlayer
	{

		[DllImport("kernel32.dll")]
		private static extern bool Beep(int frequency, int duration);

		private bool mStop;
		private ToneEnum mTone;
		private Thread mThread = null;

		public TonePlayer()
		{
			mStop = false;
			mTone = ToneEnum.NONE;
		}

		public ToneEnum Tone
		{
			get { return this.mTone; }
			set { this.mTone = value; }
		}

		#region IAlarmPlayer
		public bool IsRunning
		{
			get
			{
				return (mThread != null);
			}
		}

		public void AlarmStart()
		{
			if (mThread == null && this.Check())
			{
				// create and start new thread
				mThread = new Thread(new ThreadStart(WorkerThread));
				mThread.Start();
			}
		}

		public void AlarmEnd()
		{
			if (mThread != null)
			{
				this.Stop();
				mThread.Abort();
				mThread = null;
			}
		}

		public bool Check()
		{
			return true;
		}
		#endregion

		private void WorkerThread()
		{
			this.Play();
		}

		public void Play()
		{
			this.mStop = false;
			switch (this.Tone)
			{
				case ToneEnum.MASTERWARNING1:
					this.MasterWarning1();
					break;
				case ToneEnum.MASTERWARNING2:
					this.MasterWarning2();
					break;
				case ToneEnum.RADIATION:
					this.Radiation();
					break;
				case ToneEnum.WHEELS:
					this.Wheels();
					break;
			}
		}

		public void Stop()
		{
			this.mStop = true;
		}

		public void MasterWarning1()
		{
			int startFreq = 700;
			int endFreq = 1700;
			int duration = 85;
			int dwell = 12;
			int steps = 20;

			int diff = Math.Abs(startFreq - endFreq);
			diff = Convert.ToInt32(diff / duration);

			while (true)
			{
				if (mStop)
				{
					break;
				}
				// tone
				int CurrentFreq = startFreq;

				for (int i = 0; i < steps - 1; i++)
				{
					if (mStop)
					{
						break;
					}
					Beep(CurrentFreq, Convert.ToInt32(duration / steps));
					CurrentFreq = CurrentFreq + diff;
				}

				// dwell
				Thread.Sleep(dwell);
			}
		}

		public void MasterWarning2()
		{

			int startFreq = 700;
			int endFreq = 1700;
			int duration = 255;
			int dwell = 15;
			int steps = 20;

			int diff = Math.Abs(startFreq - endFreq);
			diff = Convert.ToInt32(diff / duration);

			while (true)
			{
				if (mStop)
				{
					break;
				}
				// tone
				int CurrentFreq = startFreq;

				for (int i = 0; i < steps - 1; i++)
				{
					if (mStop)
					{
						break;
					}
					Beep(CurrentFreq, Convert.ToInt32(duration / steps));
					CurrentFreq = CurrentFreq + diff;
				}

				// dwell
				Thread.Sleep(dwell);
			}
		}

		public void Wheels()
		{
			int freq1 = 250;
			int duration1 = 200;
			int dwell1 = 1;
			int freq2 = 1;
			int duration2 = 200;
			int dwell2 = 1;

			while (true)
			{
				if (mStop)
				{
					break;
				}
				Beep(freq1, duration1);
				Thread.Sleep(dwell1);
				Beep(freq2, duration2);
				Thread.Sleep(dwell2);
			}
		}

		public void Radiation()
		{
			int freq1 = 500;
			int duration1 = 32;
			int dwell1 = 2;
			int freq2 = 400;
			int duration2 = 30;
			int dwell2 = 2;

			while (true)
			{
				if (mStop)
				{
					break;
				}
				Beep(freq1, duration1);
				Thread.Sleep(dwell1);
				Beep(freq2, duration2);
				Thread.Sleep(dwell2);
			}
		}

	}
}
