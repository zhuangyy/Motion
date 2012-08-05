// Motion Detector
//
// Copyright ?Andrew Kirillov, 2005
// andrew.kirillov@gmail.com
//
namespace ZForge.Motion.Controls
{
	using System;
	using System.Drawing;
	using System.Threading;
	using AForge.Video;
	using System.Windows.Forms;
	using ZForge.Motion.Core;
	using AForge.Vision.Motion;
	using System.ComponentModel;

	/// <summary>
	/// Camera class
	/// </summary>
	public class Camera
	{
		private IVideoSource mVideoSource = null;
		private IMotionDetector mMotionDetecotor = null;
		private Bitmap mLastFrame = null;

		// image width and height
		private int mImageWidth = -1, mImageHeight = -1;
		private CameraClass mCameraClass = null;
		//
		public event NewFrameEventHandler NewFrame;
		public event EventHandler Alarm;

		#region Properties
		// LastFrame property
		public Bitmap LastFrame
		{
			get
			{
				Bitmap b = null;
				lock (this)
				{
					if (mLastFrame != null)
					{
						b = new Bitmap(mLastFrame);
					}
				}
				return b;
			}
		}
		// Width property
		public int Width
		{
			get { return mImageWidth; }
		}
		// Height property
		public int Height
		{
			get { return mImageHeight; }
		}
		
		// FramesReceived property
		public int FramesReceived
		{
			get { return (mVideoSource == null) ? 0 : mVideoSource.FramesReceived; }
		}
		
		// BytesReceived property
		public int BytesReceived
		{
			get { return (mVideoSource == null) ? 0 : mVideoSource.BytesReceived; }
		}

		// Running property
		public bool IsRunning
		{
			get { return (mVideoSource == null) ? false : mVideoSource.IsRunning; }
		}

		// MotionDetector property
		public IMotionDetector MotionDetector
		{
			get { return mMotionDetecotor; }
			set { mMotionDetecotor = value; }
		}

		public IVideoSource VideoSource
		{
			get { return this.mVideoSource; }
		}

		[Browsable(false)]
		public Motion.Core.CameraClass CameraClass
		{
			get { return this.mCameraClass; }
			set { this.mCameraClass = value; }
		}

		public DETECTMODE DetectMode
		{
			get
			{
				if (this.CameraClass != null)
				{
					return this.CameraClass.DetectMode;
				}
				return DETECTMODE.MOTION;
			}
		}

		public bool Mirror
		{
			get
			{
				if (this.CameraClass != null)
				{
					return this.CameraClass.Mirror;
				}
				return false;
			}
		}

		public bool Flip
		{
			get
			{
				if (this.CameraClass != null)
				{
					return this.CameraClass.Flip;
				}
				return false;
			}
		}
		#endregion

		// Constructor
		public Camera(IVideoSource source)
			: this(source, null)
		{ }

		public Camera(IVideoSource source, IMotionDetector detector)
		{
			this.mVideoSource = source;
			this.mMotionDetecotor = detector;
			mVideoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
		}

		// Start video source
		public void Start()
		{
			if (mVideoSource != null)
			{
				mVideoSource.Start();
			}
		}

		// Siganl video source to stop
		public void SignalToStop()
		{
			if (mVideoSource != null)
			{
				mVideoSource.SignalToStop();
			}
		}

		// Wait video source for stop
		public void WaitForStop()
		{
			lock (this)
			{
				if (mVideoSource != null)
				{
					mVideoSource.WaitForStop();
				}
			}
		}

		// Abort camera
		public void Stop()
		{
			lock (this)
			{
				if (mVideoSource != null)
				{
					mVideoSource.Stop();
				}
			}
		}

		// On new frame
		private void video_NewFrame(object sender, NewFrameEventArgs e)
		{
			try
			{
				bool motion = false;
				lock (this)
				{
					// dispose old frame
					if (mLastFrame != null)
					{
						mLastFrame.Dispose();
					}
					mLastFrame = new Bitmap(e.Frame);
					if (this.Mirror)
					{
						mLastFrame.RotateFlip(RotateFlipType.RotateNoneFlipX);
					}
					if (this.Flip)
					{
						mLastFrame.RotateFlip(RotateFlipType.RotateNoneFlipY);
					}

					// apply motion detector
					if (mMotionDetecotor != null)
					{
						mMotionDetecotor.ProcessFrame(mLastFrame);
						if (mMotionDetecotor is ICountingMotionDetector)
						{
							ICountingMotionDetector m = mMotionDetecotor as ICountingMotionDetector;
							motion = (m.ObjectsCount > 0);
						}
						else
						{
							motion = (mMotionDetecotor.MotionLevel > 0.005);
						}
					}

					// image dimension
					mImageWidth = mLastFrame.Width;
					mImageHeight = mLastFrame.Height;
				}

				if (this.Alarm != null)
				{
					if ((motion && this.DetectMode == DETECTMODE.MOTION) || (motion == false && this.DetectMode == DETECTMODE.STILLNESS))
					{
						Alarm(this, new EventArgs());
					}
				}
			}
			catch (Exception)
			{
			}
			// notify client
			if (NewFrame != null)
				NewFrame(this, new NewFrameEventArgs(null));
		}
	}
}
