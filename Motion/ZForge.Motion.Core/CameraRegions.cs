using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AForge.Imaging.Filters;
using System.Collections;
using System.Runtime.Serialization;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ZForge.Motion.Core
{
	[Serializable()]
	public class CameraRegions : ISerializable
	{
		private bool mShowGrid = true;
		private int mGridSize = 10;
		private Bitmap mBmp;
		private ArrayList mRegions;
		private ArrayList mOutline;

		public CameraRegions()
		{
			this.mRegions = new ArrayList();
		}

		public CameraRegions(SerializationInfo info, StreamingContext ctxt)
		{
			//Get the values from info and assign them to the appropriate properties
			this.mRegions = (ArrayList)info.GetValue("regions", typeof(ArrayList));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
		{
			info.AddValue("regions", this.mRegions);
		}

		public void Copy(CameraRegions c)
		{
			if (this != c)
			{
				lock (this)
				{
					this.mRegions = (ArrayList)c.Regions.Clone();
					this.mOutline = null;
				}
			}
		}

		#region Properties
		private ArrayList Outline
		{
			get
			{
				if (this.mOutline == null)
				{
					try
					{
						this.mOutline = this.GetOutline();
					}
					catch (Exception)
					{
						this.mOutline = null;
					}
				}
				return (this.mOutline == null) ? new ArrayList() : this.mOutline;
			}
		}

		public ArrayList Regions
		{
			get { return this.mRegions; }
		}

		public System.Drawing.Rectangle[] Rectangles
		{
			get
			{
				List<Rectangle> list = new List<Rectangle>();
				foreach (Point p in this.mRegions)
				{
					list.Add(new Rectangle(p.X, p.Y, 10, 10));
				}
				return list.ToArray();
			}
		}

		public bool ShowGrid
		{
			get { return this.mShowGrid; }
			set { this.mShowGrid = value; }
		}

		public Bitmap Bitmap
		{
			get 
			{
				return this.Render(); 
			}
			set
			{
				lock (this)
				{
					this.mBmp = value;
				}
			}
		}

		public bool IsReady
		{
			get
			{
				if (this.mBmp == null)
				{
					return false;
				}
				try
				{
					int w = mBmp.Width;
					int h = mBmp.Height;
					return true;
				}
				catch (Exception)
				{
					return false;
				}
			}
		}

		public int GridSize
		{
			get { return this.mGridSize; }
			set 
			{
				if (value / 10 == 0)
				{
					value = 10;
				}
				this.mGridSize = (value / 10) * 10; 
			}
		}
		#endregion

		public void ClearRegion()
		{
			lock (this)
			{
				this.mRegions.Clear();
				this.mOutline = null;
			}
		}

		public ArrayList ReverseRegion()
		{
			int x, y;

			if (this.IsReady)
			{
				this.mOutline.Clear();
				ArrayList r = new ArrayList();
				for (x = 0; x < this.mBmp.Width; x += 10)
				{
					for (y = 0; y < this.mBmp.Height; y += 10)
					{
						if (!this.mRegions.Contains(new Point(x, y)))
						{
							r.Add(new Point(x, y));
						}
					}
				}
				this.mRegions.Clear();
				this.mRegions = r;
				this.mOutline = null;
			}
			return this.mRegions;
		}

		public bool RegionOverlapped(Rectangle rc)
		{
			if (this.mRegions.Count == 0) {
				return true;
			}
			foreach (Point n in this.mRegions)
			{
				Rectangle r = new Rectangle(n.X, n.Y, 10, 10);
				if (rc.Top > r.Bottom || rc.Bottom < r.Top || rc.Left > r.Right || rc.Right < r.Left) {
					continue;
				}
				return true;
			}
			return false;
		}

		public ArrayList SwapRegion(int x, int y, int rw, int rh)
		{
			if (this.IsReady)
			{
				ArrayList region;

				x = (x * 10000 * this.mBmp.Width / rw) / 10000;
				y = (y * 10000 * this.mBmp.Height / rh) / 10000;

				region = this.SnapRegion(x, y);
				if (this.RegionFilled(region))
				{
					foreach (Point n in region)
					{
						this.mRegions.Remove(n);
					}
				}
				else
				{
					foreach (Point n in region)
					{
						if (this.mRegions.Contains(n))
						{
							continue;
						}
						this.mRegions.Add(n);
					}
				}
				this.mOutline = null;
			}
			return this.mRegions;
		}

		private bool RegionFilled(ArrayList rs)
		{
			foreach (Point n in rs)
			{
				if (!this.mRegions.Contains(n))
				{
					return false;
				}
			}
			return true;
		}

		private ArrayList SnapRegion(int x, int y)
		{
			int n = this.GridSize / 10;
			int i, j, xs, ys;
			ArrayList r = new ArrayList();

			x = (x / this.GridSize) * this.GridSize;
			y = (y / this.GridSize) * this.GridSize;
			for (i = 0; i < n; i++)
			{
				xs = x + i * 10;
				if (xs >= this.mBmp.Width)
				{
					break;
				}
				for (j = 0; j < n; j++)
				{
					ys = y + j * 10;
					if (ys >= this.mBmp.Height)
					{
						break;
					}
					r.Add(new Point(xs, ys));
				}
			}
			return r;
		}

		private bool InRegion(int x, int y)
		{
			int l, t, r, b;

			if (this.mRegions.Count == 0)
			{
				return true;
			}
			if (x < 0 || y < 0)
			{
				return false;
			}
			if (this.mBmp != null)
			{
				if (x > this.mBmp.Width || y > this.mBmp.Height)
				{
					return false;
				}
			}
			foreach (Point n in this.mRegions)
			{
				l = n.X;
				t = n.Y;
				r = l + 10;
				b = t + 10;
				if ((x >= l && x <= r) && (y >= t && y <= b))
				{
					return true;
				}
			}
			return false;
		}

		private ArrayList GetOutline()
		{
			ArrayList list = new ArrayList();
			foreach (Point n in this.mRegions)
			{
				if (!this.InRegion(n.X - 1, n.Y + 1)) //left
				{
					list.Add(new CameraRegionOutline(n.X, n.Y, n.X, n.Y + 10));
				}
				if (!this.InRegion(n.X + 1, n.Y - 1)) //top
				{
					list.Add(new CameraRegionOutline(n.X, n.Y, n.X + 10, n.Y));
				}
				if (!this.InRegion(n.X + 11, n.Y + 1)) //right
				{
					list.Add(new CameraRegionOutline(n.X + 10, n.Y, n.X + 10, n.Y + 10));
				}
				if (!this.InRegion(n.X + 1, n.Y + 11)) //bottom
				{
					list.Add(new CameraRegionOutline(n.X, n.Y + 10, n.X + 10, n.Y + 10));
				}
			}
			return list;
		}

		private void DrawGrid(Graphics g)
		{
			Pen pen = new Pen(Color.Gray, 1);
			for (int x = this.GridSize; x < this.mBmp.Width; x += this.GridSize)
			{
				g.DrawLine(pen, x, 0, x, this.mBmp.Height);
			}
			for (int y = this.GridSize; y < this.mBmp.Height; y += this.GridSize)
			{
				g.DrawLine(pen, 0, y, this.mBmp.Width, y);
			}
			pen.Dispose();
		}

		private void DrawOutline(Graphics g)
		{
			Pen pen = new Pen(Color.Blue, 1);
			foreach (CameraRegionOutline o in this.Outline)
			{
				o.Adjust(this.mBmp.Width, this.mBmp.Height);
				g.DrawLine(pen, o.P1, o.P2);
			}
			pen.Dispose();
		}

		private void HighlightRegion(Graphics g, Bitmap bmp)
		{
			foreach (Point n in this.mRegions)
			{
				int rcw = Math.Min(10, bmp.Width - n.X);
				int rch = Math.Min(10, bmp.Height - n.Y);
				Bitmap c = bmp.Clone(new Rectangle(n.X, n.Y, rcw, rch), System.Drawing.Imaging.PixelFormat.DontCare);
				g.DrawImage(c, n.X, n.Y, rcw, rch);
				c.Dispose();
			}
		}

		public Bitmap Render()
		{
			if (this.IsReady == false)
				return null;

			Bitmap r = null;
			lock (this)
			{
				IFilter grayscaleFilter = new GrayscaleBT709();
				Bitmap b = grayscaleFilter.Apply(this.mBmp);
				r = new Bitmap(b.Width, b.Height);
				
				Graphics g = Graphics.FromImage(r);
				g.DrawImage(b, 0, 0, r.Width, r.Height);
				b.Dispose();

				if (this.ShowGrid)
				{
					this.DrawGrid(g);
				}

				this.HighlightRegion(g, this.mBmp);
				
				this.DrawOutline(g);
				
				g.Dispose();
			}

			return r;
		}

		public byte[] Serialize()
		{
			lock (this)
			{
				MemoryStream ms = new MemoryStream();
				BinaryFormatter bformatter = new BinaryFormatter();
				bformatter.Serialize(ms, this);
				ms.Close();
				return ms.ToArray();
			}
		}

		public bool Unserialize(byte[] data)
		{
			CameraRegions t = null;
			try
			{
				MemoryStream ms = new MemoryStream(data);
				BinaryFormatter bformatter = new BinaryFormatter();
				t = (CameraRegions) bformatter.Deserialize(ms);
				ms.Close();
			}
			catch (Exception)
			{
				t = null;
			}
			if (t != null)
			{
				this.Copy(t);
			}
			else
			{
				this.ClearRegion();
			}
			return true;
		}
	}
}
