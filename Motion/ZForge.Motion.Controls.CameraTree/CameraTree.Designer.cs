namespace ZForge.Motion.Controls
{
	partial class CameraTree
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraTree));
			this.toolStripTree = new System.Windows.Forms.ToolStrip();
			this.treeViewCamera = new System.Windows.Forms.TreeView();
			this.imageListTree = new System.Windows.Forms.ImageList(this.components);
			this.contextMenuStripGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextMenuStripCamera = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonNewGroup = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonDel = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSort = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonTest = new System.Windows.Forms.ToolStripButton();
			this.toolStripMenuItemAddMember = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemGroupDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemGroupEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCemeraAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCameraEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTree.SuspendLayout();
			this.contextMenuStripGroup.SuspendLayout();
			this.contextMenuStripCamera.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripTree
			// 
			this.toolStripTree.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStripTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNew,
            this.toolStripButtonNewGroup,
            this.toolStripButtonDel,
            this.toolStripButtonEdit,
            this.toolStripButtonSort,
            this.toolStripButtonTest});
			this.toolStripTree.Location = new System.Drawing.Point(0, 0);
			this.toolStripTree.Name = "toolStripTree";
			this.toolStripTree.Size = new System.Drawing.Size(358, 31);
			this.toolStripTree.TabIndex = 0;
			// 
			// treeViewCamera
			// 
			this.treeViewCamera.AllowDrop = true;
			this.treeViewCamera.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeViewCamera.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewCamera.ImageIndex = 0;
			this.treeViewCamera.ImageList = this.imageListTree;
			this.treeViewCamera.LabelEdit = true;
			this.treeViewCamera.Location = new System.Drawing.Point(0, 31);
			this.treeViewCamera.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.treeViewCamera.Name = "treeViewCamera";
			this.treeViewCamera.SelectedImageIndex = 0;
			this.treeViewCamera.ShowNodeToolTips = true;
			this.treeViewCamera.Size = new System.Drawing.Size(358, 501);
			this.treeViewCamera.TabIndex = 1;
			this.treeViewCamera.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewCamera_NodeMouseDoubleClick);
			this.treeViewCamera.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewCamera_DragDrop);
			this.treeViewCamera.DragOver += new System.Windows.Forms.DragEventHandler(this.treeViewCamera_DragOver);
			this.treeViewCamera.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewCamera_AfterLabelEdit);
			this.treeViewCamera.DragLeave += new System.EventHandler(this.treeViewCamera_DragLeave);
			this.treeViewCamera.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCamera_AfterSelect);
			this.treeViewCamera.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeViewCamera_MouseUp);
			this.treeViewCamera.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewCamera_DragEnter);
			this.treeViewCamera.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewCamera_ItemDrag);
			// 
			// imageListTree
			// 
			this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
			this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListTree.Images.SetKeyName(0, "");
			this.imageListTree.Images.SetKeyName(1, "");
			this.imageListTree.Images.SetKeyName(2, "");
			this.imageListTree.Images.SetKeyName(3, "");
			this.imageListTree.Images.SetKeyName(4, "");
			// 
			// contextMenuStripGroup
			// 
			this.contextMenuStripGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAddMember,
            this.toolStripMenuItemGroupDelete,
            this.toolStripMenuItemGroupEdit});
			this.contextMenuStripGroup.Name = "contextMenuStripGroup";
			this.contextMenuStripGroup.Size = new System.Drawing.Size(221, 70);
			// 
			// contextMenuStripCamera
			// 
			this.contextMenuStripCamera.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCemeraAdd,
            this.toolStripMenuItemCameraEdit,
            this.toolStripMenuItemDelete});
			this.contextMenuStripCamera.Name = "contextMenuStripCamera";
			this.contextMenuStripCamera.Size = new System.Drawing.Size(161, 70);
			// 
			// toolStripButtonNew
			// 
			this.toolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonNew.Image = global::ZForge.Motion.Controls.Properties.Resources.videocamera_24;
			this.toolStripButtonNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonNew.Name = "toolStripButtonNew";
			this.toolStripButtonNew.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonNew.Text = "新增摄像头";
			this.toolStripButtonNew.ToolTipText = "新增摄像头";
			this.toolStripButtonNew.Click += new System.EventHandler(this.toolStripButtonNew_Click);
			// 
			// toolStripButtonNewGroup
			// 
			this.toolStripButtonNewGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonNewGroup.Image = global::ZForge.Motion.Controls.Properties.Resources.folder_add_24;
			this.toolStripButtonNewGroup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonNewGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonNewGroup.Name = "toolStripButtonNewGroup";
			this.toolStripButtonNewGroup.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonNewGroup.Text = "新建组";
			this.toolStripButtonNewGroup.Click += new System.EventHandler(this.toolStripButtonNewGroup_Click);
			// 
			// toolStripButtonDel
			// 
			this.toolStripButtonDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonDel.Image = global::ZForge.Motion.Controls.Properties.Resources.delete_24;
			this.toolStripButtonDel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonDel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDel.Name = "toolStripButtonDel";
			this.toolStripButtonDel.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonDel.Text = "删除";
			this.toolStripButtonDel.Click += new System.EventHandler(this.toolStripButtonDel_Click);
			// 
			// toolStripButtonEdit
			// 
			this.toolStripButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonEdit.Image = global::ZForge.Motion.Controls.Properties.Resources.text_rich_colored_24;
			this.toolStripButtonEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonEdit.Name = "toolStripButtonEdit";
			this.toolStripButtonEdit.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonEdit.Text = "编辑";
			this.toolStripButtonEdit.Click += new System.EventHandler(this.toolStripButtonEdit_Click);
			// 
			// toolStripButtonSort
			// 
			this.toolStripButtonSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSort.Image = global::ZForge.Motion.Controls.Properties.Resources.sort_ascending_24;
			this.toolStripButtonSort.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonSort.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSort.Name = "toolStripButtonSort";
			this.toolStripButtonSort.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonSort.Text = "排序";
			this.toolStripButtonSort.Click += new System.EventHandler(this.toolStripButtonSort_Click);
			// 
			// toolStripButtonTest
			// 
			this.toolStripButtonTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonTest.Image = global::ZForge.Motion.Controls.Properties.Resources.link_add_24;
			this.toolStripButtonTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonTest.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonTest.Name = "toolStripButtonTest";
			this.toolStripButtonTest.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonTest.Text = "测试摄像头";
			this.toolStripButtonTest.Click += new System.EventHandler(this.toolStripButtonTest_Click);
			// 
			// toolStripMenuItemAddMember
			// 
			this.toolStripMenuItemAddMember.Image = global::ZForge.Motion.Controls.Properties.Resources.add2_16;
			this.toolStripMenuItemAddMember.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripMenuItemAddMember.Name = "toolStripMenuItemAddMember";
			this.toolStripMenuItemAddMember.Size = new System.Drawing.Size(220, 22);
			this.toolStripMenuItemAddMember.Text = "添加所有组成员到监控面板";
			this.toolStripMenuItemAddMember.Click += new System.EventHandler(this.toolStripMenuItemAddMember_Click);
			// 
			// toolStripMenuItemGroupDelete
			// 
			this.toolStripMenuItemGroupDelete.Image = global::ZForge.Motion.Controls.Properties.Resources.delete_16;
			this.toolStripMenuItemGroupDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripMenuItemGroupDelete.Name = "toolStripMenuItemGroupDelete";
			this.toolStripMenuItemGroupDelete.Size = new System.Drawing.Size(220, 22);
			this.toolStripMenuItemGroupDelete.Text = "删除组";
			this.toolStripMenuItemGroupDelete.Click += new System.EventHandler(this.toolStripMenuItemGroupDelete_Click);
			// 
			// toolStripMenuItemGroupEdit
			// 
			this.toolStripMenuItemGroupEdit.Image = global::ZForge.Motion.Controls.Properties.Resources.text_rich_colored_16;
			this.toolStripMenuItemGroupEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripMenuItemGroupEdit.Name = "toolStripMenuItemGroupEdit";
			this.toolStripMenuItemGroupEdit.Size = new System.Drawing.Size(220, 22);
			this.toolStripMenuItemGroupEdit.Text = "重命名";
			this.toolStripMenuItemGroupEdit.Click += new System.EventHandler(this.toolStripMenuItemGroupEdit_Click);
			// 
			// toolStripMenuItemCemeraAdd
			// 
			this.toolStripMenuItemCemeraAdd.Image = global::ZForge.Motion.Controls.Properties.Resources.add2_16;
			this.toolStripMenuItemCemeraAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripMenuItemCemeraAdd.Name = "toolStripMenuItemCemeraAdd";
			this.toolStripMenuItemCemeraAdd.Size = new System.Drawing.Size(160, 22);
			this.toolStripMenuItemCemeraAdd.Text = "添加到监控面板";
			this.toolStripMenuItemCemeraAdd.Click += new System.EventHandler(this.toolStripMenuItemCemeraAdd_Click);
			// 
			// toolStripMenuItemCameraEdit
			// 
			this.toolStripMenuItemCameraEdit.Image = global::ZForge.Motion.Controls.Properties.Resources.text_rich_colored_16;
			this.toolStripMenuItemCameraEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripMenuItemCameraEdit.Name = "toolStripMenuItemCameraEdit";
			this.toolStripMenuItemCameraEdit.Size = new System.Drawing.Size(160, 22);
			this.toolStripMenuItemCameraEdit.Text = "编辑";
			this.toolStripMenuItemCameraEdit.Click += new System.EventHandler(this.toolStripMenuItemCameraEdit_Click);
			// 
			// toolStripMenuItemDelete
			// 
			this.toolStripMenuItemDelete.Image = global::ZForge.Motion.Controls.Properties.Resources.delete_16;
			this.toolStripMenuItemDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			this.toolStripMenuItemDelete.Size = new System.Drawing.Size(160, 22);
			this.toolStripMenuItemDelete.Text = "删除";
			this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
			// 
			// CameraTree
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.treeViewCamera);
			this.Controls.Add(this.toolStripTree);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "CameraTree";
			this.Size = new System.Drawing.Size(358, 532);
			this.toolStripTree.ResumeLayout(false);
			this.toolStripTree.PerformLayout();
			this.contextMenuStripGroup.ResumeLayout(false);
			this.contextMenuStripCamera.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStripTree;
		private System.Windows.Forms.TreeView treeViewCamera;
		private System.Windows.Forms.ToolStripButton toolStripButtonNew;
		private System.Windows.Forms.ToolStripButton toolStripButtonDel;
		private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
		private System.Windows.Forms.ImageList imageListTree;
		private System.Windows.Forms.ToolStripButton toolStripButtonTest;
		private System.Windows.Forms.ToolStripButton toolStripButtonSort;
		private System.Windows.Forms.ToolStripButton toolStripButtonNewGroup;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripGroup;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddMember;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGroupDelete;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGroupEdit;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripCamera;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCemeraAdd;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCameraEdit;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
	}
}
