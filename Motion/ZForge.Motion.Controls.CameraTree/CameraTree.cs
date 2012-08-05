using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using ZForge.Motion.Core;
using ZForge.Motion.Util;
using ZForge.Globalization;

namespace ZForge.Motion.Controls
{
	public partial class CameraTree : UserControl, IGlobalization
	{
		private System.Windows.Forms.ImageList mImageListDrag;
		private CameraTreeNode mDragNode = null;

		public CameraTree()
		{
			InitializeComponent();
			
			this.mImageListDrag = new System.Windows.Forms.ImageList(this.components);
			ReloadTreeView();
			this.treeViewCamera.AllowDrop = true;
			this.treeViewCamera.Sort();
			SetToolStripButtonState();
		}

		#region Properties

		public TreeView TreeView
		{
			get { return this.treeViewCamera; }
		}

		#endregion

		#region Event
		public event CameraTreeOnBoardEventHandler CameraOnBoard;
		public event CameraTreeBeforeDeleteEventHandler CameraBeforeDelete;
		public event CameraTreeAfterDeleteEventHandler CameraAfterDelete;

		protected virtual void OnCameraTreeOnBoard(CameraTreeEventArgs e)
		{
			if (this.CameraOnBoard != null)
			{
				CameraOnBoard(this, e);
			}
		}

		protected virtual void OnCameraTreeBeforeDelete(CameraTreeNodeDeleteEventArgs e)
		{
			if (this.CameraBeforeDelete != null)
			{
				CameraBeforeDelete(this, e);
			}
		}

		protected virtual void OnCameraTreeAfterDelete(CameraTreeEventArgs e)
		{
			if (this.CameraAfterDelete != null)
			{
				CameraAfterDelete(this, e);
			}
		}
		#endregion

		private bool AtTheEdge()
		{
      return false;
		}
		
		public void ReloadTreeView()
		{
			this.treeViewCamera.Nodes.Clear();

			PopulateTreeView(RootClass.Instance.Children, null);
			this.treeViewCamera.ExpandAll();
		}

		private void PopulateTreeView(ItemClassCollection items, TreeNode parentNode)
		{
			foreach (ItemClass item in items.Values)
			{
				if (this.AtTheEdge())
				{
					break;
				}
				CameraTreeNode n = new CameraTreeNode(item);
				if (parentNode == null)
				{
					this.treeViewCamera.Nodes.Add(n);
				}
				else
				{
					parentNode.Nodes.Add(n);
				}
				GroupClass g = item as GroupClass;
				if (g != null)
				{
					PopulateTreeView(g.Children, n);
				}
			}
		}

		private CameraTreeNode GetSelectedTreeNode()
		{
			TreeNode n = this.treeViewCamera.SelectedNode;
			return (CameraTreeNode)n;
		}

		private CameraTreeNode GetSelectedGroupNode()
		{
			CameraTreeNode n = this.GetSelectedTreeNode();
			if (null != n && n.IsGroup() == false)
			{
				return (CameraTreeNode)(n.Parent);
			}
			return n;
		}

		public CameraTreeNode FindCameraTreeNode(ItemClass c, TreeNode node)
		{
			TreeNodeCollection nodes;
			CameraTreeNode r = null;

			nodes = (node == null) ? this.treeViewCamera.Nodes : node.Nodes;
			foreach (TreeNode n in nodes)
			{
				if (n is CameraTreeNode)
				{
					CameraTreeNode t = (CameraTreeNode)n;
					if (t.Item.ID == c.ID)
					{
						return t;
					}
					r = FindCameraTreeNode(c, n);
					if (r != null)
					{
						return r;
					}
				}
			}
			return null;
		}

		public void SetTreeNodeStatus(CameraClass c, int status) 
		{
			CameraTreeNode n = this.FindCameraTreeNode(c, null);
			if (n != null)
			{
				CameraStatus s = new CameraStatus(status);
				if (s.IsStatusSet(CameraStatus.PAUSED))
				{
					n.ImageIndex = 3;
				}
				else if (s.IsStatusSet(CameraStatus.STARTED))
				{
					n.ImageIndex = 2;
				}
				else
				{
					n.ImageIndex = 1;
				}
				n.SelectedImageIndex = n.ImageIndex;
				this.treeViewCamera.SelectedNode = n;
			}
		}

		public void SetTreeNodeAlarmCount(CameraClass c, int v)
		{
			CameraTreeNode n = this.FindCameraTreeNode(c, null);
			if (n != null)
			{
				try
				{
					n.Text = n.Item.Name + " (" + v.ToString() + ")";
				}
				catch (System.InvalidOperationException e)
				{
					MessageBox.Show(e.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void SetToolStripButtonState()
		{
			CameraTreeNode n = GetSelectedTreeNode();
			this.toolStripButtonEdit.Enabled = (n != null && !n.IsGroup());
			this.toolStripButtonDel.Enabled = (n != null);
		}

		public void Event_CameraViewAlarmCountChanged(object sender, CameraViewAlarmCountChangedEventArgs e)
		{
			this.SetTreeNodeAlarmCount(e.CameraView.CameraClass, e.AlarmCount);
		}

		#region Node operations

		private CameraTreeNode NodeAdd(ItemClass c, CameraTreeNode p)
		{
			if (c is CameraClass && this.AtTheEdge()) {
				return null;
			}
			CameraTreeNode n;
			n = new CameraTreeNode(c);
			if (p == null)
			{
				this.treeViewCamera.Nodes.Add(n);
				RootClass.Instance.Add(c);
			}
			else
			{
				GroupClass g = p.Item as GroupClass;
				g.Add(c);
				p.Nodes.Add(n);
			}

			this.treeViewCamera.SelectedNode = n;
			return n;
		}

		private CameraTreeNode NodeAdd(ItemClass c)
		{
			CameraTreeNode p = this.GetSelectedGroupNode();
			return this.NodeAdd(c, p);
		}

		private void NodeEdit()
		{
			CameraTreeNode n = this.GetSelectedTreeNode();
			if (n != null)
			{
				if (!n.IsGroup())
				{
					CameraClass m = n.Item as CameraClass;
					CameraClass e = m.Clone() as CameraClass;
					CameraEditForm fmEdit = new CameraEditForm(e);
					fmEdit.ShowDialog(this);
					if (DialogResult.OK == fmEdit.DialogResult)
					{
						fmEdit.Camera.Copy(m);
						RootClass.Instance.SaveConfig();
						MotionConfiguration.Instance.Save();

						m.FireItemValueChangedEvent();
					}
				}
				else
				{
					n.BeginEdit();
				}
			}
		}

		private bool NodeDelete(CameraTreeNode node)
		{
			if (node.IsGroup())
			{
				if (node.Nodes.Count > 0)
				{
					ArrayList list = new ArrayList();
					foreach (TreeNode n in node.Nodes)
					{
						list.Add(n);
					}
					foreach (TreeNode n in list)
					{
						if (false == this.NodeDelete(n as CameraTreeNode))
						{
							return false;
						}
					}
				}
				if (node.Nodes.Count == 0)
				{
					node.Item.Remove();
					node.Item.Clean();
					node.Remove();
					return true;
				}
			}
			else
			{
				CameraClass c = node.Item as CameraClass;
				if (c.Capturing == true)
				{
					MessageBox.Show(string.Format(Translator.Instance.T("删除摄像头[{0}]失败! 该摄像头正在录像中, 您必须首先停止录像, 然后再删除该摄像头."), c.Name), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				CameraTreeNodeDeleteEventArgs e = new CameraTreeNodeDeleteEventArgs(c);
				this.OnCameraTreeBeforeDelete(e);
				if (e.Cancel == false)
				{
					node.Item.Remove();
					node.Item.Clean();
					node.Remove();
					this.OnCameraTreeAfterDelete(e);
					return true;
				}
			}
			return false;
		}

		private bool NodeDelete()
		{
			CameraTreeNode n = this.GetSelectedTreeNode();
			if (n != null)
			{
				string msg = "";
				if (n.IsGroup())
				{
					msg = Translator.Instance.T("您确实要删除这个视频监控组吗? 如果该组被删除, 则该组内的所有摄像头及其监控信息都将被删除.");
				}
				else
				{
					msg = Translator.Instance.T("您确实要删除这个摄像头吗? 如果该摄像头被删除, 则该摄像头的所有监控信息也将被删除.");
				}
				if (DialogResult.Yes == MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
				{
					return this.NodeDelete(n);
				}
			}
			return false;
		}

		private void NodeOnBoard(CameraTreeNode node)
		{
			if (node == null)
			{
				return;
			}
			if (node.IsGroup())
			{
				foreach (TreeNode n in node.Nodes)
				{
					this.NodeOnBoard((CameraTreeNode)n);
				}
			}
			else
			{
				this.OnCameraTreeOnBoard(new CameraTreeEventArgs((CameraClass)node.Item));
			}
		}

		private void NodeOnBoard()
		{
			CameraTreeNode n = this.GetSelectedTreeNode();
			this.NodeOnBoard(n);
		}
		
		private int NodeCount(CameraTreeNode p) {
			int r = 0;
			if (p.IsGroup()) {
				foreach (CameraTreeNode n in p.Nodes) {
					r += this.NodeCount(n);
				}
			}
			else {
				r ++;
			}
			return r;
		}

		public void NodeReloadGlobalSettings(CameraTreeNode p)
		{
			if (p.IsGroup())
			{
				foreach (CameraTreeNode n in p.Nodes)
				{
					this.NodeReloadGlobalSettings(n);
				}
			}
			else
			{
				CameraClass c = p.Item as CameraClass;
				if (c != null)
				{
					c.ReloadGlobalSettings();
				}
			}
		}

		public void NodeReloadGlobalSettings()
		{
			foreach (CameraTreeNode p in this.treeViewCamera.Nodes)
			{
				this.NodeReloadGlobalSettings(p);
			}
		}

		#endregion

		#region Toolbar Events

		private void toolStripButtonNew_Click(object sender, EventArgs e)
		{
			CameraEditForm fmEdit;
			CameraClass m = new CameraClass();

			fmEdit = new CameraEditForm(m);
			fmEdit.ShowDialog(this);
			if (fmEdit.DialogResult == DialogResult.OK)
			{
				this.NodeAdd(fmEdit.Camera);
				RootClass.Instance.SaveConfig();
				MotionConfiguration.Instance.Save();
			}
		}

		private void toolStripButtonDel_Click(object sender, EventArgs e)
		{
			if (this.NodeDelete())
			{
				RootClass.Instance.SaveConfig();
				MotionConfiguration.Instance.Save();
			}
		}

		private void toolStripButtonEdit_Click(object sender, EventArgs e)
		{
			this.NodeEdit();
		}

		private void toolStripButtonTest_Click(object sender, EventArgs e)
		{
			string[] j = new string[] {
				"http://61.220.38.10/axis-cgi/jpg/image.cgi?camera=1",
				"http://212.98.46.120/axis-cgi/jpg/image.cgi?resolution=352x240",
				"http://webcam.mmhk.cz/axis-cgi/jpg/image.cgi?resolution=320x240",
				"http://195.243.185.195/axis-cgi/jpg/image.cgi?camera=1"
			};
			string[] m = new string[] {
				"http://129.186.47.239/axis-cgi/mjpg/video.cgi?resolution=352x240",
				"http://195.243.185.195/axis-cgi/mjpg/video.cgi?camera=3",
				"http://195.243.185.195/axis-cgi/mjpg/video.cgi?camera=4",
				"http://chipmunk.uvm.edu/cgi-bin/webcam/nph-update.cgi?dummy=garb"
			};
			int n;

			GroupClass gc = new GroupClass();
			gc.Name = Translator.Instance.T("测试摄像头");
			CameraTreeNode g = this.NodeAdd(gc, null);
			for (n = 0; n < m.Length; n++)
			{
				CameraClass c = new CameraClass();
				c.Name = "MJPEG " + (n + 1);
				c.Stream = "p1f25f05340a17e6e6acdf656d3360ea0";
				Motion.PlugIns.IPlugInVideoSource s = c.PlugInVideoSource;
				if (s != null)
				{
					Motion.PlugIns.IPlugInIPCam p = s as Motion.PlugIns.IPlugInIPCam;
					if (p != null)
					{
						p.Stream = Motion.PlugIns.IPCAM.MJPEG;
						p.URL = m[n];
						if (null == this.NodeAdd(c, g))
						{
							break;
						}
          }
				}
			}
			if (n == m.Length)
			{
				for (n = 0; n < j.Length; n++)
				{
					CameraClass c = new CameraClass();
					c.Name = "JPEG " + (n + 1);
					c.Stream = "p1f25f05340a17e6e6acdf656d3360ea0";
					Motion.PlugIns.IPlugInVideoSource s = c.PlugInVideoSource;
					if (s != null)
					{
						Motion.PlugIns.IPlugInIPCam p = s as Motion.PlugIns.IPlugInIPCam;
						if (p != null)
						{
							p.Stream = Motion.PlugIns.IPCAM.JPEG;
							p.URL = j[n];
							if (null == this.NodeAdd(c, g))
							{
								break;
							}
            }
					}
				}
			}
			RootClass.Instance.SaveConfig();
			MotionConfiguration.Instance.Save();
		}

		private void toolStripButtonSort_Click(object sender, EventArgs e)
		{
			this.treeViewCamera.Sort();
		}

		private void toolStripButtonNewGroup_Click(object sender, EventArgs e)
		{
			GroupClass c = new GroupClass();
			CameraTreeNode n = this.GetSelectedGroupNode();
			if (n != null)
			{
				c.Group = n.Item as GroupClass;
			}
			c.Name = Translator.Instance.T("新建组");
			this.NodeAdd(c);

			RootClass.Instance.SaveConfig();
			MotionConfiguration.Instance.Save();
		}

		#endregion

		#region TreeView Events

		private void treeViewCamera_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.SetToolStripButtonState();
		}

		private void treeViewCamera_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			try
			{
				if (e.Label == null)
				{
					e.CancelEdit = true;
					return;
				}
				CameraTreeNode n = (CameraTreeNode)e.Node;
				string l = e.Label.Trim();
				if (l.Length == 0)
				{
					MessageBox.Show(Translator.Instance.T("请输入名称."), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.CancelEdit = true;
					return;
				}
				n.Item.Name = e.Label;
				RootClass.Instance.SaveConfig();
				MotionConfiguration.Instance.Save();
			}
			catch (Exception)
			{
				e.CancelEdit = true;
			}
		}

		private void treeViewCamera_ItemDrag(object sender, ItemDragEventArgs e)
		{
			/*
			this.treeViewCamera.SelectedNode = (TreeNode)e.Item;
			CameraTreeNode n = (CameraTreeNode)e.Item;
			this.treeViewCamera.DoDragDrop(n.Item, DragDropEffects.Copy);
			 */

			// Get drag node and select it
			this.mDragNode = (CameraTreeNode)e.Item;
			this.treeViewCamera.SelectedNode = this.mDragNode;

			// Reset image list used for drag image
			this.mImageListDrag.Images.Clear();
			this.mImageListDrag.ImageSize = new Size(this.mDragNode.Bounds.Size.Width + this.treeViewCamera.Indent, this.mDragNode.Bounds.Height);

			// Create new bitmap
			// This bitmap will contain the tree node image to be dragged
			Bitmap bmp = new Bitmap(this.mDragNode.Bounds.Width + this.treeViewCamera.Indent, this.mDragNode.Bounds.Height);

			// Get graphics from bitmap
			Graphics gfx = Graphics.FromImage(bmp);

			// Draw node icon into the bitmap
			gfx.DrawImage(this.imageListTree.Images[this.mDragNode.SelectedImageIndex], 0, 0);

			// Draw node label into bitmap
			gfx.DrawString(this.mDragNode.Text,
				this.treeViewCamera.Font,
				new SolidBrush(this.treeViewCamera.ForeColor),
				(float)this.treeViewCamera.Indent, 1.5f);

			// Add bitmap to imagelist
			this.mImageListDrag.Images.Add(bmp);

			// Get mouse position in client coordinates
			Point p = this.treeViewCamera.PointToClient(Control.MousePosition);

			// Compute delta between mouse position and node bounds
			int dx = p.X + this.treeViewCamera.Indent - this.mDragNode.Bounds.Left;
			int dy = p.Y - this.mDragNode.Bounds.Top + this.treeViewCamera.Location.Y;

			// Begin dragging image
			if (ImageListDragHelper.ImageList_BeginDrag(this.mImageListDrag.Handle, 0, dx, dy))
			{
				// Begin dragging
				this.treeViewCamera.DoDragDrop(((CameraTreeNode)this.mDragNode).Item, DragDropEffects.Move);
				// End dragging image
				ImageListDragHelper.ImageList_EndDrag();
			}
		}

		private void treeViewCamera_DragOver(object sender, DragEventArgs e)
		{
			// Compute drag position and move image
			Point formP = this.PointToClient(new Point(e.X, e.Y));
			ImageListDragHelper.ImageList_DragMove(formP.X - this.treeViewCamera.Left, formP.Y - this.treeViewCamera.Top + this.treeViewCamera.Location.Y);

			// Get actual drop node
			CameraTreeNode dropNode = (CameraTreeNode)this.treeViewCamera.GetNodeAt(this.treeViewCamera.PointToClient(new Point(e.X, e.Y)));
			if (dropNode != null)
			{
				if (!dropNode.IsGroup() || dropNode == this.mDragNode)
				{
					e.Effect = DragDropEffects.None;
					return;
				}
			}
			// Avoid that drop node is child of drag node 
			TreeNode tmpNode = dropNode;
			while (tmpNode != null && tmpNode.Parent != null)
			{
				if (tmpNode.Parent == this.mDragNode)
				{
					e.Effect = DragDropEffects.None;
					return;
				}
				tmpNode = tmpNode.Parent;
			}

			e.Effect = DragDropEffects.Move;
			// if mouse is on a new node select it
			ImageListDragHelper.ImageList_DragShowNolock(false);
			this.treeViewCamera.SelectedNode = dropNode;
			ImageListDragHelper.ImageList_DragShowNolock(true);
		}

		private void treeViewCamera_DragEnter(object sender, DragEventArgs e)
		{
			ImageListDragHelper.ImageList_DragEnter(this.treeViewCamera.Handle, e.X, e.Y);
		}

		private void treeViewCamera_DragLeave(object sender, EventArgs e)
		{
			ImageListDragHelper.ImageList_DragLeave(this.treeViewCamera.Handle);
		}

		private void treeViewCamera_DragDrop(object sender, DragEventArgs e)
		{
			// Unlock updates
			ImageListDragHelper.ImageList_DragLeave(this.treeViewCamera.Handle);
			CameraTreeNode dropNode = this.GetSelectedGroupNode();

			// Remove drag node from parent
			if (this.mDragNode.Parent == null)
			{
				this.treeViewCamera.Nodes.Remove(this.mDragNode);
			}
			else
			{
				this.mDragNode.Parent.Nodes.Remove(this.mDragNode);
			}

			// Add drag node to drop node
			if (dropNode == null)
			{
				this.treeViewCamera.Nodes.Add(this.mDragNode);
				this.mDragNode.Item.MoveTo(null);
			}
			else
			{
				dropNode.Nodes.Add(this.mDragNode);
				dropNode.ExpandAll();
				GroupClass g = dropNode.Item as GroupClass;
				this.mDragNode.Item.MoveTo(g);
			}
			RootClass.Instance.SaveConfig();
			MotionConfiguration.Instance.Save();

			// Set drag node to null
			this.mDragNode = null;
		}

		private void treeViewCamera_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{

				// Point where the mouse is clicked.
				Point p = new Point(e.X, e.Y);

				// Get the node that the user has clicked.
				CameraTreeNode node = (CameraTreeNode)this.treeViewCamera.GetNodeAt(p);
				if (node == null)
				{
					return;
				}
				this.treeViewCamera.SelectedNode = node;
				if (node.IsGroup())
				{
					this.contextMenuStripGroup.Show(this.treeViewCamera, p);
				}
				else
				{
					this.contextMenuStripCamera.Show(this.treeViewCamera, p);
				}
			}
		}

		private void treeViewCamera_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			CameraTreeNode n = (CameraTreeNode)e.Node;
			if (n != null && !n.IsGroup())
			{
				this.OnCameraTreeOnBoard(new CameraTreeEventArgs((CameraClass)n.Item));
			}
		}
		#endregion

		#region TreeView Context Menu
		private void toolStripMenuItemAddMember_Click(object sender, EventArgs e)
		{
			this.NodeOnBoard();
		}

		private void toolStripMenuItemGroupDelete_Click(object sender, EventArgs e)
		{
			this.NodeDelete();
		}

		private void toolStripMenuItemGroupEdit_Click(object sender, EventArgs e)
		{
			this.NodeEdit();
		}

		private void toolStripMenuItemCemeraAdd_Click(object sender, EventArgs e)
		{
			CameraTreeNode n = this.GetSelectedTreeNode();
			if (n != null && !n.IsGroup())
			{
				this.OnCameraTreeOnBoard(new CameraTreeEventArgs((CameraClass)n.Item));
			}
		}

		private void toolStripMenuItemCameraEdit_Click(object sender, EventArgs e)
		{
			this.NodeEdit();
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			this.NodeDelete();
		}
		#endregion


		#region IGlobalization Members

		public void UpdateCulture()
		{
			this.toolStripButtonNew.Text = Translator.Instance.T("新增摄像头");
			this.toolStripButtonNew.ToolTipText = Translator.Instance.T("新增摄像头");
			this.toolStripButtonNewGroup.Text = Translator.Instance.T("新建组");
			this.toolStripButtonDel.Text = Translator.Instance.T("删除");
			this.toolStripButtonEdit.Text = Translator.Instance.T("编辑");
			this.toolStripButtonSort.Text = Translator.Instance.T("排序");
			this.toolStripButtonTest.Text = Translator.Instance.T("测试摄像头");
			this.toolStripMenuItemAddMember.Text = Translator.Instance.T("添加所有组成员到监控面板");
			this.toolStripMenuItemGroupDelete.Text = Translator.Instance.T("删除组");
			this.toolStripMenuItemGroupEdit.Text = Translator.Instance.T("重命名");
			this.toolStripMenuItemCemeraAdd.Text = Translator.Instance.T("添加到监控面板");
			this.toolStripMenuItemCameraEdit.Text = Translator.Instance.T("编辑");
			this.toolStripMenuItemDelete.Text = Translator.Instance.T("删除");
		}

		#endregion
	}
}
