// Motion Detector
//
// Copyright ?Andrew Kirillov, 2005
// andrew.kirillov@gmail.com
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using AForge.Imaging.Filters;
using AForge.Video;
using ZForge.Motion.VideoSource;
using System.Drawing.Text;
using ZForge.Motion.Core;

namespace ZForge.Motion.Controls
{
	/// <summary>
	/// Summary description for CameraWindow.
	/// </summary>
	public class CameraWindow : System.Windows.Forms.Control
	{
		private Motion.Core.CameraClass mCameraClass = null;
		private Camera mCamera = null;
		private Bitmap mCurrentFrame = null;
		private bool mPaused = false;
		private bool mRegionEditing = false;
		private bool mShowRegion = false;
		private bool mBanner = false;
		private Color mRectColor = Color.Black;

		#region Properties
		public bool Paused
		{
			get
			{
				return mPaused;
			}
			set
			{
				mPaused = value;
				if (mPaused == true && mCamera != null)
				{
					lock (this)
					{
						mCurrentFrame = mCamera.LastFrame;
					}
				}
			}
		}

		public bool RegionEditing
		{
			get
			{
				return mRegionEditing;
			}
			set
			{
				this.mRegionEditing = value;
				this.Invalidate();
			}
		}

		public int GridSize
		{
			get
			{
				return (this.CameraClass != null) ? this.CameraClass.CameraRegions.GridSize : 10;
			}
			set
			{
				if (this.CameraClass != null)
				{
					this.CameraClass.CameraRegions.GridSize = value;
				}
				if (this.RegionEditing)
				{
					this.Invalidate();
				}
			}
		}

		public bool ShowRegion
		{
			get
			{
				return this.mShowRegion;
			}
			set
			{
				this.mShowRegion = value;
				this.Invalidate();
			}
		}

		public bool Banner
		{
			get { return this.mBanner; }
			set
			{
				this.mBanner = value;
				this.Invalidate();
			}
		}

		// Camera property
		[Browsable(false)]
		public Camera Camera
		{
			get { return mCamera; }
			set
			{
				lock (this)
				{
					// detach event
					if (mCamera != null)
					{
						mCamera.NewFrame -= new NewFrameEventHandler(camera_NewFrame);
					}

					mCamera = value;
					// attach event
					if (mCamera != null)
					{
						mCamera.NewFrame += new NewFrameEventHandler(camera_NewFrame);
					}
				}
			}
		}

		[Browsable(false)]
		public Motion.Core.CameraClass CameraClass
		{
			get { return this.mCameraClass; }
			set
			{
				this.mCameraClass = value;
				if (this.Camera != null)
				{
					this.Camera.CameraClass = value;
				}
			}
		}

		[Browsable(false)]
		public Bitmap CurrentFrame
		{
			get
			{
				Bitmap b = null;
				lock (this)
				{
					if (mCurrentFrame != null)
					{
						b = new Bitmap(mCurrentFrame);
					}
				}
				return b;
			}
		}

		#endregion

		// Constructor
		public CameraWindow()
		{
			InitializeComponent();

			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer |
				ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
		}

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
		}
		#endregion

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			if (this.mRegionEditing)
			{
				Rectangle rc = this.ClientRectangle;
				this.CameraClass.CameraRegions.SwapRegion(e.X, e.Y, rc.Width, rc.Height);
				this.Invalidate();
			}
		}

		private void ShowFrame(Graphics g, Bitmap bmp)
		{
			if (bmp == null)
			{
				return;
			}
			Rectangle rc = this.ClientRectangle;
			if (this.RegionEditing || this.ShowRegion)
			{
				this.CameraClass.CameraRegions.Bitmap = bmp;
				this.CameraClass.CameraRegions.ShowGrid = this.RegionEditing;
				bmp = this.CameraClass.CameraRegions.Render();
			}
			if (this.mBanner)
			{
				bmp = CameraBanner.Render(bmp, this.CameraClass.Name, true);
			}
			g.DrawImage(bmp, rc.X + 1, rc.Y + 1, rc.Width - 2, rc.Height - 2);
		}

		// Paint control
		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			Rectangle rc = this.ClientRectangle;
			Pen pen = new Pen(mRectColor, 1);

			// draw rectangle
			g.DrawRectangle(pen, rc.X, rc.Y, rc.Width - 1, rc.Height - 1);

			if (false == this.Paused && mCamera != null)
			{
				lock (this)
				{
					mCurrentFrame = mCamera.LastFrame;
				}
			}
			this.ShowFrame(g, this.CurrentFrame);

			pen.Dispose();

			base.OnPaint(pe);
		}

		// On new frame ready
		private void camera_NewFrame(object sender, System.EventArgs e)
		{
			Invalidate();
		}

		public void ClearRegion()
		{
			this.CameraClass.CameraRegions.ClearRegion();
			this.Invalidate();
		}

		public void ReverseRegion()
		{
			this.CameraClass.CameraRegions.ReverseRegion();
			this.Invalidate();
		}
	}
}
