using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Globalization;

namespace ZForge.Motion.Controls
{
	internal class PTZControlItemCollection : List<PTZControlItem>
	{
		public PTZControlItemCollection()
			: base()
		{
			this.Add(new PTZControlItem(global::ZForge.Motion.Controls.Properties.Resources.navigate_left2_16, Translator.Instance.T("左"), PTZControlItemGroup.P, PTZControlItemMoveAction.NEXT, PTZControlItemDirection.N));
			this.Add(new PTZControlItem(global::ZForge.Motion.Controls.Properties.Resources.navigate_left_16, Translator.Instance.T("左"), PTZControlItemGroup.P, PTZControlItemMoveAction.STEP, PTZControlItemDirection.N));
			this.Add(new PTZControlItem(global::ZForge.Motion.Controls.Properties.Resources.navigate_right_16, Translator.Instance.T("右"), PTZControlItemGroup.P, PTZControlItemMoveAction.STEP, PTZControlItemDirection.P));
			this.Add(new PTZControlItem(global::ZForge.Motion.Controls.Properties.Resources.navigate_right2_16, Translator.Instance.T("右"), PTZControlItemGroup.P, PTZControlItemMoveAction.NEXT, PTZControlItemDirection.P));
			this.Add(new PTZControlItem(global::ZForge.Motion.Controls.Properties.Resources.navigate_up2_16, Translator.Instance.T("上"), PTZControlItemGroup.T, PTZControlItemMoveAction.NEXT, PTZControlItemDirection.P));
			this.Add(new PTZControlItem(global::ZForge.Motion.Controls.Properties.Resources.navigate_up_16, Translator.Instance.T("上"), PTZControlItemGroup.T, PTZControlItemMoveAction.STEP, PTZControlItemDirection.P));
			this.Add(new PTZControlItem(global::ZForge.Motion.Controls.Properties.Resources.navigate_down_16, Translator.Instance.T("下"), PTZControlItemGroup.T, PTZControlItemMoveAction.STEP, PTZControlItemDirection.N));
			this.Add(new PTZControlItem(global::ZForge.Motion.Controls.Properties.Resources.navigate_down2_16, Translator.Instance.T("下"), PTZControlItemGroup.T, PTZControlItemMoveAction.NEXT, PTZControlItemDirection.N));
		}
	}
}
