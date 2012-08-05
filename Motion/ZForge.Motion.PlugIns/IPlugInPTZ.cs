using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Motion.PlugIns
{
	public abstract class PTZAction
	{
		//public abstract int Position { get; }

		public abstract bool Step(int v);
		public abstract bool Next(int v);
		public abstract bool Auto(bool on);
		public abstract bool CanAutomate
		{
			get;
		}
	}

	public interface IPlugInPTZ : IPlugIn
	{
		PTZAction P { get; }
		PTZAction T { get; }
		PTZAction Z { get; }
	}
}
