// Motion Detector
//
// Copyright ?Andrew Kirillov, 2005
// andrew.kirillov@gmail.com
//
namespace ZForge.Motion.Controls
{
	using System;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.Reflection;

	using AForge.Imaging;
	using AForge.Imaging.Filters;
	using AForge.Vision.Motion;

	/// <summary>
	/// ObjectMotionDetector
	/// </summary>
	public class ObjectMotionDetector : IMotionDetector, ICountingMotionDetector
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

		private int width;	// image width
		private int height;	// image height
		private int pixelsChanged;
		private int objectsCount = 0;
		private Motion.Core.CameraRegions motionZone;
		private bool highlightMotionRegions = true;

		public int MinObjectDimension
		{
			get { return Math.Min(this.MinObjectsHeight, this.MinObjectsWidth); }
			set
			{
				lock (blobCounter)
				{
					blobCounter.MinHeight = value;
					blobCounter.MinWidth = value;
				}
			}
		}

		public int DifferenceThreshold
		{
			get { return (int) thresholdFilter.ThresholdValue; }
			set
			{
				lock (thresholdFilter)
				{
					thresholdFilter.ThresholdValue = (byte) Math.Max(1, Math.Min(255, value));
				}
			}
		}

		public Motion.Core.CameraRegions MotionZone
		{
			set { this.motionZone = value; }
		}

		// Constructor
		public ObjectMotionDetector(bool highlight)
		{
			processingFilter1.Add(grayscaleFilter);
			processingFilter1.Add(pixellateFilter);
			this.highlightMotionRegions = highlight;
		}

		public ObjectMotionDetector() : this(true)
		{
		}

		#region ICountingMotionDetector Members

		public int MaxObjectsHeight
		{
			get { return blobCounter.MaxHeight; }
			set
			{
				lock (blobCounter)
				{
					blobCounter.MaxHeight = value;
				}
			}
		}

		public int MaxObjectsWidth
		{
			get { return blobCounter.MaxWidth; }
			set
			{
				lock (blobCounter)
				{
					blobCounter.MaxWidth = value;
				}
			}
		}

		public int MinObjectsHeight
		{
			get { return blobCounter.MinHeight; }
			set
			{
				lock (blobCounter)
				{
					blobCounter.MinHeight = value;
				}
			}
		}

		public int MinObjectsWidth
		{
			get { return blobCounter.MinWidth; }
			set
			{
				lock (blobCounter)
				{
					blobCounter.MinWidth = value;
				}
			}
		}

		public bool ObjectCoupledSize
		{
			get { return blobCounter.CoupledSizeFiltering; }
			set
			{
				lock (blobCounter)
				{
					blobCounter.CoupledSizeFiltering = value;
				}
			}
		}

		public Rectangle[] ObjectRectangles
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public int ObjectsCount
		{
			get { return objectsCount; }
		}

		#endregion

		#region IMotionDetector Members

		public bool HighlightMotionRegions
		{
			get { return highlightMotionRegions; }
			set { highlightMotionRegions = value; }
		}

		// Motion level - amount of changes in percents
		public double MotionLevel
		{
			get { return (double)pixelsChanged / (width * height); }
		}

		public void ProcessFrame(Bitmap image)
		{
			pixelsChanged = 0;
			objectsCount = 0;

			if (backgroundFrame == null)
			{
				// create initial backgroung image
				backgroundFrame = processingFilter1.Apply(image);

				// get image dimension
				width = image.Width;
				height = image.Height;

				// just return for the first time
				return;
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

			lock (thresholdFilter)
			{
				// apply threshold filter
				thresholdFilter.ApplyInPlace(bitmapData);
			}

			lock (blobCounter)
			{
				// get object rectangles
				blobCounter.ProcessImage(bitmapData);
			}

			// unlock temporary image
			tmpImage.UnlockBits(bitmapData);
			tmpImage.Dispose();

			Rectangle[] rects = blobCounter.GetObjectRectangles();
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
						if (rc.Width < this.MinObjectDimension && rc.Height < this.MinObjectDimension)
						{
							continue;
						}
						if (this.motionZone != null && !this.motionZone.RegionOverlapped(rc))
						{
							continue;
						}
						objectsCount ++;
						if (this.HighlightMotionRegions)
						{
							g.DrawRectangle(pen, rc);

							if ((n < 9) && (rc.Width > 15) && (rc.Height > 15))
							{
								n ++;
								g.DrawString(n.ToString(), font, Brushes.Red, new Point(rc.Left, rc.Top));
							}
						}

						// a little bit inaccurate, but fast
						pixelsChanged += rc.Width * rc.Height;
					}
				}
				g.Dispose();
			}
		}

		public void Reset()
		{
			if (backgroundFrame != null)
			{
				backgroundFrame.Dispose();
				backgroundFrame = null;
			}
			counter = 0;
		}

		#endregion
	}
}
