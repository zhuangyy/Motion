using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZForge.Motion.Controls;
using ZForge.Globalization;
using ZForge.Motion.Util;

namespace ZForge.Motion.Forms
{
	public partial class RecordBrowseForm : Form, IGlobalization
	{
		private CameraBoardStyle mStyle = CameraBoardStyle.AVI;

		public RecordBrowseForm(CameraBoardStyle style)
		{
			InitializeComponent();
			MotionPreference.Instance.UpdateUI(this);

			this.Style = style;
			this.recordList.BoardControl = this.WhiteBoard;
			this.FileSystemWatcherEnabled = true;
		}

		public bool FileSystemWatcherEnabled
		{
			get
			{
				return this.recordList.FileSystemWatcherEnabled;
			}
			set
			{
				this.recordList.FileSystemWatcherEnabled = value;
			}
		}

		public void ClearAll()
		{
			this.recordList.ClearAll();
		}

		private const int CP_NOCLOSE_BUTTON = 0x200;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams myCp = base.CreateParams;
				myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
				return myCp;
			}
		} 

		private CameraBoardStyle Style
		{
			get
			{
				return this.mStyle;
			}
			set
			{
				this.mStyle = value;
				switch (value)
				{
					case CameraBoardStyle.AVI:
						this.Text = string.Format(Translator.Instance.T("{0} 录像管理"), MotionPreference.Instance.ProductFullName);
						toolStripMenuItemMain.Text = Translator.Instance.T("录像管理");
						this.panelBoard.CaptionText = Translator.Instance.T("录像浏览");
						this.panelList.CaptionText = Translator.Instance.T("录像列表");
						this.Icon = this.MakeIcon("AAABAAEAEBAAAAEAIABoBAAAFgAAACgAAAAQAAAAIAAAAAEAIAAAAAAAQAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGRkZDp5eXmudHR00GVlZc5bW1utb29vRQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACBgYHIoKCgzISEhMxlZWXMREREzGtra24AAAAAAAAAAHR0dCl1dXVrfn5+mXZ2drRpaWmvbm5uSAAAAAAAAAAAfX190ISEhMJ1dXWuZWVlyExMTNhhYWGdfHx8iIqKisOSkpLXoqKizImJicxsbGzMUVFRzFJSUscAAAAAAAAAAF9fX8ZMTEyrWVlZmmFhYbVvb2/cfn5+3aSkpMyjo6PMk5OT1oWFhch1dXWYZ2dneWJiYoJcXFzYAAAAAAAAAABPT0+qJycnzD09PcxZWVnMc3Nzz4CAgN10dHTqXFxc9k9PT7xFRUVwODg4LGZmZh5sbGx5d3d30wAAAAAAAAAAcXFxMU1NTaJXV1fKaWlp8mZmZv5YWVr/U1dX/1BWVv9MUVH/UlVV/2FhYf5zc3P6o6Oj1H19fbAAAAAAAAAAAAAAAACZmZlEg4OD8Xl5ev95eXr/lpGP/7mamf+9iov/t4uL/5qCgv9nZGX/a2xs/1NTU/1AQEBAAAAAAAAAAACjo6MTjo6O7oaGhv+5sK//aGhj/2JlZP95gHz//66q/8eTkf94dnL/g4J9/5SPif9KS0v/NTU1jwAAAAAAAAAAnZ2dVJmZmf/8+fn/9/Dv/01PT/9gYWH/k5CN//+wrv9MT1D/UlZY/2JlZ//Twr7/18HC/0lLS6kAAAAAAAAAAKmqqk7l3d3/083K/66sqP/b2dP//////4yKhf+copz//uvg/+ra1//l2ND///z4//////9mZma7AAAAAAAAAACur69dsKin/zM4OP9WWVr/XGNl///k3/+khob/uqWg//Dm4P9HS0n/U1VX/15hYf/89/L/X2BgrAAAAAAAAAAA0tPTF9TOzu/Hrq3/vael//HNyP+Of3z/cXFt/5SJhf/20Mz/aWdm/2htav+mop7/mJaU+mlpaToAAAAAAAAAAAAAAADo6ek60czM4PTNzv/wtbT/QkhH/2FjZP91enj//8zJ///s7f/r09T/jIaH6HJzc0sAAAAAAAAAAAAAAAAAAAAAAAAAANTW1gixtrZfrbCxq7CoptuenpvxsJyb8pyQkd91d3exaGtrZ3BxcQwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");
						break;
					case CameraBoardStyle.PIC:
						this.Text = string.Format(Translator.Instance.T("{0} 截图管理"), MotionPreference.Instance.ProductFullName);
						toolStripMenuItemMain.Text = Translator.Instance.T("截图管理");
						this.panelBoard.CaptionText = Translator.Instance.T("截图浏览");
						this.panelList.CaptionText = Translator.Instance.T("截图列表");
						this.Icon = this.MakeIcon("AAABAAEAEBAAAAEAIABoBAAAFgAAACgAAAAQAAAAIAAAAAEAIAAAAAAAQAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADZqoz437GS/9+vkf/erpH/3K2R/9urkP/aqpD/2aiP/9injv/WpY7/1aSN/9SijP/ToIz/xJOH/wAAAAAAAAAA4rST+/rx3//q6c//6+nP/+vozf/r6Mv/7OfJ/+vnx//s58X/7OXB/+zjv//r4L3//OnJ/9Sjjf8AAAAAAAAAAOO1lPvr6tT/Dbo4/xW5O/8RtTP/DLAp/wirIP8Alwb/AIIA/wBzAP8AbAD/AF8A/+jfuf/VpI7/AAAAAAAAAADlt5X77OzY/x7BTf8nwE7/Ir1G/xexMP8AjwD/AH0A/wWBC/8AewD/AHYA/wBrAP/q4L7/16aP/wAAAAAAAAAA5riV++zt3P8lyFr/KMZT/wycJf8AcwD/GoRg/0Cj/P9Krv//L5y7/wB7AP8AcQD/6uLB/9inj/8AAAAAAAAAAOe7l/vr7Nz/BqIt/0uiW/+turX/1tLe/2Km/P8lj///KpD//yuR//8ZirH/AHUA/+fjwf/aqY//AAAAAAAAAADovJf78Ozi/6azmf/VyMr/0szJ/9bRy//FyM7/GH39/wVz//8Ccv//g6vp/8/Nz//46tX/2quP/wAAAAAAAAAA6b2X+/bx6f/ax7X/wbKj/8W3qv+elov/zr+y/9/Pv/+otc7/x8XH/97Qwf/UzMP/+OvV/9uskP8AAAAAAAAAAOq/mPv28er/4cSm/9W8o/+LfXD/ybOe/+fPtv/kzbT/5c61/+PNtv/gzLf/38u3//js1//crZH/AAAAAAAAAADrwJj79vHq//THm//An33/+9Cm//TNpf/xzKX/78um/+7Lp//ty6j/7Mup/+vKqP/569f/3a6R/wAAAAAAAAAA7MGY+/r59v/28ej/9/Dp//fv5//37+b/9/Dl//fv4//37uD/+O7f//jt3f/57Nr/+vHj/9+wkf8AAAAAAAAAAO7AkPrswZj/67+X/+q+l//pvZb/57uW/+a7lv/luZX/5LeU/+O2k//js5P/4bKR/+Cxkf/ZqYr/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");
						break;
				}
				this.recordList.Style = this.mStyle;
				this.WhiteBoard.Style = this.mStyle;
			}
		}

		private System.Drawing.Icon MakeIcon(string v)
		{
			Byte[] d = Convert.FromBase64String(v);
			System.IO.MemoryStream s = new System.IO.MemoryStream(d);
			return new System.Drawing.Icon(s);
		}

		private void toolStripMenuItemClose_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		private void MovieForm_Load(object sender, EventArgs e)
		{
			this.recordList.RefreshList();
		}

		#region IGlobalization Members

		public void UpdateCulture()
		{
			this.toolStripMenuItemClose.Text = Translator.Instance.T("关闭");
			switch (this.Style)
			{
				case CameraBoardStyle.AVI:
					this.Text = string.Format(Translator.Instance.T("{0} 录像管理"), MotionPreference.Instance.ProductFullName);
					toolStripMenuItemMain.Text = Translator.Instance.T("录像管理");
					this.panelBoard.CaptionText = Translator.Instance.T("录像浏览");
					this.panelList.CaptionText = Translator.Instance.T("录像列表");
					this.Icon = this.MakeIcon("AAABAAEAEBAAAAEAIABoBAAAFgAAACgAAAAQAAAAIAAAAAEAIAAAAAAAQAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGRkZDp5eXmudHR00GVlZc5bW1utb29vRQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACBgYHIoKCgzISEhMxlZWXMREREzGtra24AAAAAAAAAAHR0dCl1dXVrfn5+mXZ2drRpaWmvbm5uSAAAAAAAAAAAfX190ISEhMJ1dXWuZWVlyExMTNhhYWGdfHx8iIqKisOSkpLXoqKizImJicxsbGzMUVFRzFJSUscAAAAAAAAAAF9fX8ZMTEyrWVlZmmFhYbVvb2/cfn5+3aSkpMyjo6PMk5OT1oWFhch1dXWYZ2dneWJiYoJcXFzYAAAAAAAAAABPT0+qJycnzD09PcxZWVnMc3Nzz4CAgN10dHTqXFxc9k9PT7xFRUVwODg4LGZmZh5sbGx5d3d30wAAAAAAAAAAcXFxMU1NTaJXV1fKaWlp8mZmZv5YWVr/U1dX/1BWVv9MUVH/UlVV/2FhYf5zc3P6o6Oj1H19fbAAAAAAAAAAAAAAAACZmZlEg4OD8Xl5ev95eXr/lpGP/7mamf+9iov/t4uL/5qCgv9nZGX/a2xs/1NTU/1AQEBAAAAAAAAAAACjo6MTjo6O7oaGhv+5sK//aGhj/2JlZP95gHz//66q/8eTkf94dnL/g4J9/5SPif9KS0v/NTU1jwAAAAAAAAAAnZ2dVJmZmf/8+fn/9/Dv/01PT/9gYWH/k5CN//+wrv9MT1D/UlZY/2JlZ//Twr7/18HC/0lLS6kAAAAAAAAAAKmqqk7l3d3/083K/66sqP/b2dP//////4yKhf+copz//uvg/+ra1//l2ND///z4//////9mZma7AAAAAAAAAACur69dsKin/zM4OP9WWVr/XGNl///k3/+khob/uqWg//Dm4P9HS0n/U1VX/15hYf/89/L/X2BgrAAAAAAAAAAA0tPTF9TOzu/Hrq3/vael//HNyP+Of3z/cXFt/5SJhf/20Mz/aWdm/2htav+mop7/mJaU+mlpaToAAAAAAAAAAAAAAADo6ek60czM4PTNzv/wtbT/QkhH/2FjZP91enj//8zJ///s7f/r09T/jIaH6HJzc0sAAAAAAAAAAAAAAAAAAAAAAAAAANTW1gixtrZfrbCxq7CoptuenpvxsJyb8pyQkd91d3exaGtrZ3BxcQwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");
					break;
				case CameraBoardStyle.PIC:
					this.Text = string.Format(Translator.Instance.T("{0} 截图管理"), MotionPreference.Instance.ProductFullName);
					toolStripMenuItemMain.Text = Translator.Instance.T("截图管理");
					this.panelBoard.CaptionText = Translator.Instance.T("截图浏览");
					this.panelList.CaptionText = Translator.Instance.T("截图列表");
					this.Icon = this.MakeIcon("AAABAAEAEBAAAAEAIABoBAAAFgAAACgAAAAQAAAAIAAAAAEAIAAAAAAAQAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADZqoz437GS/9+vkf/erpH/3K2R/9urkP/aqpD/2aiP/9injv/WpY7/1aSN/9SijP/ToIz/xJOH/wAAAAAAAAAA4rST+/rx3//q6c//6+nP/+vozf/r6Mv/7OfJ/+vnx//s58X/7OXB/+zjv//r4L3//OnJ/9Sjjf8AAAAAAAAAAOO1lPvr6tT/Dbo4/xW5O/8RtTP/DLAp/wirIP8Alwb/AIIA/wBzAP8AbAD/AF8A/+jfuf/VpI7/AAAAAAAAAADlt5X77OzY/x7BTf8nwE7/Ir1G/xexMP8AjwD/AH0A/wWBC/8AewD/AHYA/wBrAP/q4L7/16aP/wAAAAAAAAAA5riV++zt3P8lyFr/KMZT/wycJf8AcwD/GoRg/0Cj/P9Krv//L5y7/wB7AP8AcQD/6uLB/9inj/8AAAAAAAAAAOe7l/vr7Nz/BqIt/0uiW/+turX/1tLe/2Km/P8lj///KpD//yuR//8ZirH/AHUA/+fjwf/aqY//AAAAAAAAAADovJf78Ozi/6azmf/VyMr/0szJ/9bRy//FyM7/GH39/wVz//8Ccv//g6vp/8/Nz//46tX/2quP/wAAAAAAAAAA6b2X+/bx6f/ax7X/wbKj/8W3qv+elov/zr+y/9/Pv/+otc7/x8XH/97Qwf/UzMP/+OvV/9uskP8AAAAAAAAAAOq/mPv28er/4cSm/9W8o/+LfXD/ybOe/+fPtv/kzbT/5c61/+PNtv/gzLf/38u3//js1//crZH/AAAAAAAAAADrwJj79vHq//THm//An33/+9Cm//TNpf/xzKX/78um/+7Lp//ty6j/7Mup/+vKqP/569f/3a6R/wAAAAAAAAAA7MGY+/r59v/28ej/9/Dp//fv5//37+b/9/Dl//fv4//37uD/+O7f//jt3f/57Nr/+vHj/9+wkf8AAAAAAAAAAO7AkPrswZj/67+X/+q+l//pvZb/57uW/+a7lv/luZX/5LeU/+O2k//js5P/4bKR/+Cxkf/ZqYr/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");
					break;
			}
		}

		#endregion
	}
}