namespace ZForge.Motion.Forms
{
	partial class RecordList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordList));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonExport = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonMarkUnread = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonMarkRead = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonMarkImportant = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabelCount = new System.Windows.Forms.ToolStripLabel();
			this.gridList = new SourceGrid.Grid();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExport = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemMark = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemMarkUnread = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemMarkRead = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemMarkImportant = new System.Windows.Forms.ToolStripMenuItem();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.fileSystemWatcher = new System.IO.FileSystemWatcher();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRefresh,
            this.toolStripProgressBar,
            this.toolStripSeparator1,
            this.toolStripButtonAdd,
            this.toolStripButtonDelete,
            this.toolStripButtonExport,
            this.toolStripSeparator2,
            this.toolStripButtonMarkUnread,
            this.toolStripButtonMarkRead,
            this.toolStripButtonMarkImportant,
            this.toolStripSeparator3,
            this.toolStripLabelCount});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(554, 31);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButtonRefresh
			// 
			this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRefresh.Image = global::ZForge.Motion.Forms.Properties.Resources.replace2_24;
			this.toolStripButtonRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
			this.toolStripButtonRefresh.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonRefresh.Text = "刷新";
			this.toolStripButtonRefresh.ToolTipText = "刷新列表内容";
			this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
			// 
			// toolStripProgressBar
			// 
			this.toolStripProgressBar.Name = "toolStripProgressBar";
			this.toolStripProgressBar.Size = new System.Drawing.Size(100, 28);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// toolStripButtonAdd
			// 
			this.toolStripButtonAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAdd.Image = global::ZForge.Motion.Forms.Properties.Resources.add_24;
			this.toolStripButtonAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAdd.Name = "toolStripButtonAdd";
			this.toolStripButtonAdd.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonAdd.Text = "查看";
			this.toolStripButtonAdd.ToolTipText = "添加选中的录像到录像浏览面板";
			this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
			// 
			// toolStripButtonDelete
			// 
			this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonDelete.Image = global::ZForge.Motion.Forms.Properties.Resources.delete_24;
			this.toolStripButtonDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDelete.Name = "toolStripButtonDelete";
			this.toolStripButtonDelete.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonDelete.Text = "删除";
			this.toolStripButtonDelete.ToolTipText = "删除选中的录像";
			this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
			// 
			// toolStripButtonExport
			// 
			this.toolStripButtonExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonExport.Image = global::ZForge.Motion.Forms.Properties.Resources.save_as_24;
			this.toolStripButtonExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonExport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonExport.Name = "toolStripButtonExport";
			this.toolStripButtonExport.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonExport.Text = "导出";
			this.toolStripButtonExport.ToolTipText = "导出选中的录像";
			this.toolStripButtonExport.Click += new System.EventHandler(this.toolStripButtonExport_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			// 
			// toolStripButtonMarkUnread
			// 
			this.toolStripButtonMarkUnread.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonMarkUnread.Image = global::ZForge.Motion.Forms.Properties.Resources.box_closed_24;
			this.toolStripButtonMarkUnread.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonMarkUnread.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonMarkUnread.Name = "toolStripButtonMarkUnread";
			this.toolStripButtonMarkUnread.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonMarkUnread.Text = "标记为未读";
			this.toolStripButtonMarkUnread.Click += new System.EventHandler(this.toolStripButtonMarkUnread_Click);
			// 
			// toolStripButtonMarkRead
			// 
			this.toolStripButtonMarkRead.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonMarkRead.Image = global::ZForge.Motion.Forms.Properties.Resources.document_check_24;
			this.toolStripButtonMarkRead.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonMarkRead.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonMarkRead.Name = "toolStripButtonMarkRead";
			this.toolStripButtonMarkRead.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonMarkRead.Text = "标记为已读";
			this.toolStripButtonMarkRead.Click += new System.EventHandler(this.toolStripButtonMarkRead_Click);
			// 
			// toolStripButtonMarkImportant
			// 
			this.toolStripButtonMarkImportant.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonMarkImportant.Image = global::ZForge.Motion.Forms.Properties.Resources.star_yellow_new_24;
			this.toolStripButtonMarkImportant.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonMarkImportant.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonMarkImportant.Name = "toolStripButtonMarkImportant";
			this.toolStripButtonMarkImportant.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonMarkImportant.Text = "标记为重要";
			this.toolStripButtonMarkImportant.Click += new System.EventHandler(this.toolStripButtonMarkImportant_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			// 
			// toolStripLabelCount
			// 
			this.toolStripLabelCount.Name = "toolStripLabelCount";
			this.toolStripLabelCount.Size = new System.Drawing.Size(23, 28);
			this.toolStripLabelCount.Text = "(0)";
			// 
			// gridList
			// 
			this.gridList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridList.Location = new System.Drawing.Point(0, 31);
			this.gridList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.gridList.Name = "gridList";
			this.gridList.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
			this.gridList.SelectionMode = SourceGrid.GridSelectionMode.Cell;
			this.gridList.Size = new System.Drawing.Size(554, 314);
			this.gridList.TabIndex = 1;
			this.gridList.TabStop = true;
			this.gridList.ToolTipText = "";
			this.gridList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridList_MouseUp);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemView,
            this.toolStripMenuItemDelete,
            this.toolStripMenuItemExport,
            this.toolStripMenuItemMark});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(245, 92);
			// 
			// toolStripMenuItemView
			// 
			this.toolStripMenuItemView.Image = global::ZForge.Motion.Forms.Properties.Resources.add_16;
			this.toolStripMenuItemView.Name = "toolStripMenuItemView";
			this.toolStripMenuItemView.Size = new System.Drawing.Size(244, 22);
			this.toolStripMenuItemView.Text = "添加选中的录像到录像浏览面板";
			this.toolStripMenuItemView.Click += new System.EventHandler(this.toolStripMenuItemView_Click);
			// 
			// toolStripMenuItemDelete
			// 
			this.toolStripMenuItemDelete.Image = global::ZForge.Motion.Forms.Properties.Resources.delete_16;
			this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			this.toolStripMenuItemDelete.Size = new System.Drawing.Size(244, 22);
			this.toolStripMenuItemDelete.Text = "删除选中的录像";
			this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
			// 
			// toolStripMenuItemExport
			// 
			this.toolStripMenuItemExport.Image = global::ZForge.Motion.Forms.Properties.Resources.save_as;
			this.toolStripMenuItemExport.Name = "toolStripMenuItemExport";
			this.toolStripMenuItemExport.Size = new System.Drawing.Size(244, 22);
			this.toolStripMenuItemExport.Text = "导出选中的录像";
			this.toolStripMenuItemExport.Click += new System.EventHandler(this.toolStripMenuItemExport_Click);
			// 
			// toolStripMenuItemMark
			// 
			this.toolStripMenuItemMark.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMarkUnread,
            this.toolStripMenuItemMarkRead,
            this.toolStripMenuItemMarkImportant});
			this.toolStripMenuItemMark.Name = "toolStripMenuItemMark";
			this.toolStripMenuItemMark.Size = new System.Drawing.Size(244, 22);
			this.toolStripMenuItemMark.Text = "标记选中的录像";
			// 
			// toolStripMenuItemMarkUnread
			// 
			this.toolStripMenuItemMarkUnread.Image = global::ZForge.Motion.Forms.Properties.Resources.box_closed_16;
			this.toolStripMenuItemMarkUnread.Name = "toolStripMenuItemMarkUnread";
			this.toolStripMenuItemMarkUnread.Size = new System.Drawing.Size(100, 22);
			this.toolStripMenuItemMarkUnread.Text = "未读";
			this.toolStripMenuItemMarkUnread.Click += new System.EventHandler(this.toolStripMenuItemMarkUnread_Click);
			// 
			// toolStripMenuItemMarkRead
			// 
			this.toolStripMenuItemMarkRead.Image = global::ZForge.Motion.Forms.Properties.Resources.document_check_16;
			this.toolStripMenuItemMarkRead.Name = "toolStripMenuItemMarkRead";
			this.toolStripMenuItemMarkRead.Size = new System.Drawing.Size(100, 22);
			this.toolStripMenuItemMarkRead.Text = "已读";
			this.toolStripMenuItemMarkRead.Click += new System.EventHandler(this.toolStripMenuItemMarkRead_Click);
			// 
			// toolStripMenuItemMarkImportant
			// 
			this.toolStripMenuItemMarkImportant.Image = global::ZForge.Motion.Forms.Properties.Resources.star_yellow_new_16;
			this.toolStripMenuItemMarkImportant.Name = "toolStripMenuItemMarkImportant";
			this.toolStripMenuItemMarkImportant.Size = new System.Drawing.Size(100, 22);
			this.toolStripMenuItemMarkImportant.Text = "重要";
			this.toolStripMenuItemMarkImportant.Click += new System.EventHandler(this.toolStripMenuItemMarkImportant_Click);
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
			// 
			// fileSystemWatcher
			// 
			this.fileSystemWatcher.EnableRaisingEvents = true;
			this.fileSystemWatcher.SynchronizingObject = this;
			this.fileSystemWatcher.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher_Renamed);
			this.fileSystemWatcher.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Deleted);
			this.fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Created);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "box_closed_16.png");
			this.imageList.Images.SetKeyName(1, "document_check_16.png");
			this.imageList.Images.SetKeyName(2, "star_yellow_new_16.png");
			// 
			// RecordList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridList);
			this.Controls.Add(this.toolStrip1);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "RecordList";
			this.Size = new System.Drawing.Size(554, 345);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
		private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
		private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
		private SourceGrid.Grid gridList;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemView;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.ToolStripButton toolStripButtonExport;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripLabel toolStripLabelCount;
		private System.IO.FileSystemWatcher fileSystemWatcher;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMark;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMarkUnread;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMarkRead;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMarkImportant;
		private System.Windows.Forms.ToolStripButton toolStripButtonMarkUnread;
		private System.Windows.Forms.ToolStripButton toolStripButtonMarkRead;
		private System.Windows.Forms.ToolStripButton toolStripButtonMarkImportant;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ImageList imageList;
	}
}
