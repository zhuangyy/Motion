using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZForge.Motion.Controls
{
	internal enum PTZControlItemGroup
	{
		P = 1, T, Z
	}

	internal enum PTZControlItemMoveAction
	{
		STEP = 1,
		NEXT,
		AUTO
	}

	internal enum PTZControlItemDirection
	{
		P = 1,
		N = -1
	}

	internal class PTZControlItem
	{
		private Image mImage;
		private string mLabel;
		private PTZControlItemGroup mGroup;
		private PTZControlItemMoveAction mMove;
		private PTZControlItemDirection mDirection;

		public PTZControlItem(Image i, string l, PTZControlItemGroup g, PTZControlItemMoveAction m, PTZControlItemDirection d)
		{
			this.mImage = i;
			this.mLabel = l;
			this.mGroup = g;
			this.mMove = m;
			this.mDirection = d;
		}

		public Image Image
		{
			get { return this.mImage; }
		}

		public string Caption
		{
			get { return this.mLabel; }
		}

		public string ToolTip
		{
			get { return this.mLabel; }
		}

		public PTZControlItemGroup Group
		{
			get { return this.mGroup; }
		}

		public PTZControlItemMoveAction Move
		{
			get { return this.mMove; }
		}

		public PTZControlItemDirection Direction
		{
			get { return this.mDirection; }
		}
	}
}
