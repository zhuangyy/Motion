using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.Controls
{
	public class CameraStatistics
	{
		private List<int> fps;

		public CameraStatistics() {
			fps = new List<int>();
		}

		public void Reset()
		{
			fps.Clear();
		}

		public void Push(int frames)
		{
			fps.Add(frames);
			if (fps.Count > 15)
			{
				fps.RemoveAt(0);
			}
		}

		public float FPS
		{
			get
			{
				if (fps.Count == 0)
				{
					return 0.0F;
				}
				int[] v = fps.ToArray();
				int c = 0;
				for (int i = 0; i < v.Length; i++)
				{
					c += v[i];
				}
				return ((float)c) / fps.Count;
			}
		}
	}
}
