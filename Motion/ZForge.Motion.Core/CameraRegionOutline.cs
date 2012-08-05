using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZForge.Motion.Core
{
	public class CameraRegionOutline
	{
		private Point p1;
		private Point p2;

		public CameraRegionOutline(int x1, int y1, int x2, int y2)
		{
			this.p1 = new Point(x1, y1);
			this.p2 = new Point(x2, y2);
		}

		public void Adjust(int w, int h)
		{
			if (this.p1.X > w)
			{
				this.p1.X = w;
			}
			if (this.p2.X > w)
			{
				this.p2.X = w;
			}
			if (this.p1.Y > h)
			{
				this.p1.Y = h;
			}
			if (this.p2.Y > h)
			{
				this.p2.Y = h;
			}
		}

		public Point P1
		{
			get { return this.p1; }
		}

		public Point P2
		{
			get { return this.p2; }
		}
	}
}
