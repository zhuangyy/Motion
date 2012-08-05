using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Controls.TreeViewAdv.Tree.NodeControls;

namespace ZForge.Controls.TreeViewAdv.Tree
{
	public interface IToolTipProvider
	{
		string GetToolTip(TreeNodeAdv node, NodeControl nodeControl);
	}
}
