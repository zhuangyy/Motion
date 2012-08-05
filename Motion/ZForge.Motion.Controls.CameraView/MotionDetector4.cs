// Motion Detector
//
// Copyright ?Andrew Kirillov, 2005
// andrew.kirillov@gmail.com
//
namespace Motion.Controls
{
	using System;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.Reflection;

	using AForge.Imaging;
	using AForge.Imaging.Filters;

	/// <summary>
	/// MotionDetector4
	/// </summary>
	public class MotionDetector4 : IMotionDetector
	{
		private IFilter grayscaleFilter = new GrayscaleBT709();
		private IFilter pixellateFilter = new Pixellate();
		private Difference differenceFilter = new Difference();
		private Threshold thresholdFilter = new Threshold(15);
		private MoveTowards moveTowardsFilter = new MoveTowards();

		private FiltersSequence processingFilter1 = new FiltersSequence();
		private BlobCounter blobCounter = new BlobCounter();

		private Bitmap backgroundFrame;
		private BitmapData bitmapData;
		private int counter = 0;

		//private Bitmap[] numbersBitmaps = new Bitmap[9];

		private bool calculateMotionLevel = false;
		private int width;	// image width
		private int height;	// image height
		private int pixelsChanged;
		private int alarmDimension = 0;
		private double alarmLevel = 0.005;
		private Motion.Core.CameraRegions regions;
		private bool mHighlight = true;

		// Motion level calculation - calculate or not motion level
		public bool MotionLevelCalculation
		{
			get { return calculateMotionLevel; }
			set { calculateMotionLevel = value; }
		}

		// Motion level - amount of changes in percents
		public double MotionLevel
		{
			get { return (double)pixelsChanged / (width * height); }
		}

		public int AlarmDimension
		{
			get { return this.alarmDimension; }
			set { this.alarmDimension = value; }
		}

		public double AlarmLevel
		{
			get { return this.alarmLevel; }
			set { this.alarmLevel = value; }
		}

		public bool Highlight
		{
			get { return this.mHighlight; }
			set { this.mHighlight = value; }
		}

		public Motion.Core.CameraRegions Regions
		{
			set { this.regions = value; }
		}

		// Constructor
		public MotionDetector4()
		{
			processingFilter1.Add(grayscaleFilter);
			processingFilter1.Add(pixellateFilter);
		}

		// Reset detector to initial state
		public void Reset()
		{
			if (backgroundFrame != null)
			{
				backgroundFrame.Dispose();
				backgroundFrame = null;
			}
			counter = 0;
		}

		// Process new frame
		public bool ProcessFrame(ref Bitmap image)
		{
			if (backgroundFrame == null)
			{
				// create initial backgroung image
				backgroundFrame = processingFilter1.Apply(image);

				// get image dimension
				width = image.Width;
				height = image.Height;

				// just return for the first time
				return false;
			}

			Bitmap tmpImage;

			// apply the the first filters sequence
			tmpImage = processingFilter1.Apply(image);

			if (++counter == 2)
			{
				counter = 0;

				// move background towards current frame
				moveTowardsFilter.OverlayImage = tmpImage;
				moveTowardsFilter.ApplyInPlace(backgroundFrame);
			}

			// set backgroud frame as an overlay for difference filter
			differenceFilter.OverlayImage = backgroundFrame;

			// lock temporary image to apply several filters
			bitmapData = tmpImage.LockBits(new Rectangle(0, 0, width, height),
					ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

			// apply difference filter
			differenceFilter.ApplyInPlace(bitmapData);
			// apply threshold filter
			thresholdFilter.ApplyInPlace(bitmapData);

			// get object rectangles
			blobCounter.ProcessImage(bitmapData);
			Rectangle[] rects = blobCounter.GetObjectRectangles();

			// unlock temporary image
			tmpImage.UnlockBits(bitmapData);
			tmpImage.Dispose();

			pixelsChanged = 0;
			int c = 0;

			if (rects.Length != 0)
			{
				// create graphics object from initial image
				Graphics g = Graphics.FromImage(image);
				Font font = SystemFonts.MessageBoxFont;

				using (Pen pen = new Pen(Color.Red, 1))
				{
					int n = 0;

					// draw each rectangle
					foreach (Rectangle rc in rects)
					{
						if (rc.Width < this.AlarmDimension && rc.Height < this.AlarmDimension)
						{
							continue;
						}
						if (this.regions != null && !this.regions.RegionOverlapped(rc))
						{
							continue;
						}
						c++;
						if (this.Highlight)
						{
							g.DrawRectangle(pen, rc);

							if ((n < 9) && (rc.Width > 15) && (rc.Height > 15))
							{
								n++;
								g.DrawString(n.ToString(), font, Brushes.Red, new Point(rc.Left, rc.Top));
							}
						}

						// a little bit inaccurate, but fast
						if (calculateMotionLevel)
							pixelsChanged += rc.Width * rc.Height;
					}
				}
				g.Dispose();
			}
			if (this.AlarmDimension > 0)
			{
				return (c > 0);
			}
			return (this.MotionLevel > this.AlarmLevel);
		}
	}
}
