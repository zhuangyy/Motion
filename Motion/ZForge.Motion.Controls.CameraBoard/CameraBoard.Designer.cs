namespace ZForge.Motion.Controls
{
	partial class CameraBoard
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
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabelZoom = new System.Windows.Forms.ToolStripLabel();
			this.toolStripComboBoxRatio = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabelShortCut = new System.Windows.Forms.ToolStripLabel();
			this.toolStripComboBoxShortCut = new System.Windows.Forms.ToolStripComboBox();
			this.CameraPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.toolStripButtonStartAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonPauseAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonStopAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonAlarmCleanAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonCloseAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonExportAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonDeleteAll = new System.Windows.Forms.ToolStripButton();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonStartAll,
            this.toolStripButtonPauseAll,
            this.toolStripButtonStopAll,
            this.toolStripSeparator2,
            this.toolStripButtonAlarmCleanAll,
            this.toolStripSeparator1,
            this.toolStripButtonCloseAll,
            this.toolStripButtonExportAll,
            this.toolStripButtonDeleteAll,
            this.toolStripLabelZoom,
            this.toolStripComboBoxRatio,
            this.toolStripSeparator3,
            this.toolStripLabelShortCut,
            this.toolStripComboBoxShortCut});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(576, 31);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// toolStripLabelZoom
			// 
			this.toolStripLabelZoom.Name = "toolStripLabelZoom";
			this.toolStripLabelZoom.Size = new System.Drawing.Size(32, 28);
			this.toolStripLabelZoom.Text = "缩放";
			// 
			// toolStripComboBoxRatio
			// 
			this.toolStripComboBoxRatio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolStripComboBoxRatio.DropDownWidth = 50;
			this.toolStripComboBoxRatio.Items.AddRange(new object[] {
            "200%",
            "150%",
            "120%",
            "100%",
            "90%",
            "80%",
            "70%",
            "60%",
            "50%"});
			this.toolStripComboBoxRatio.Margin = new System.Windows.Forms.Padding(1);
			this.toolStripComboBoxRatio.Name = "toolStripComboBoxRatio";
			this.toolStripComboBoxRatio.Size = new System.Drawing.Size(75, 29);
			this.toolStripComboBoxRatio.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxRatio_SelectedIndexChanged);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			// 
			// toolStripLabelShortCut
			// 
			this.toolStripLabelShortCut.Name = "toolStripLabelShortCut";
			this.toolStripLabelShortCut.Size = new System.Drawing.Size(47, 28);
			this.toolStripLabelShortCut.Text = "摄像头:";
			// 
			// toolStripComboBoxShortCut
			// 
			this.toolStripComboBoxShortCut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolStripComboBoxShortCut.Name = "toolStripComboBoxShortCut";
			this.toolStripComboBoxShortCut.Size = new System.Drawing.Size(200, 25);
			this.toolStripComboBoxShortCut.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxShortCut_SelectedIndexChanged);
			// 
			// CameraPanel
			// 
			this.CameraPanel.AutoScroll = true;
			this.CameraPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CameraPanel.Location = new System.Drawing.Point(0, 31);
			this.CameraPanel.Name = "CameraPanel";
			this.CameraPanel.Size = new System.Drawing.Size(576, 389);
			this.CameraPanel.TabIndex = 1;
			// 
			// toolStripButtonStartAll
			// 
			this.toolStripButtonStartAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonStartAll.Image = global::ZForge.Motion.Controls.Properties.Resources.gears_run_24;
			this.toolStripButtonStartAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonStartAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonStartAll.Name = "toolStripButtonStartAll";
			this.toolStripButtonStartAll.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonStartAll.Text = "全部启动";
			this.toolStripButtonStartAll.Click += new System.EventHandler(this.toolStripButtonStartAll_Click);
			// 
			// toolStripButtonPauseAll
			// 
			this.toolStripButtonPauseAll.CheckOnClick = true;
			this.toolStripButtonPauseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonPauseAll.Image = global::ZForge.Motion.Controls.Properties.Resources.gears_pause_24;
			this.toolStripButtonPauseAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonPauseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonPauseAll.Name = "toolStripButtonPauseAll";
			this.toolStripButtonPauseAll.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonPauseAll.Text = "全部暂停";
			this.toolStripButtonPauseAll.Click += new System.EventHandler(this.toolStripButtonPauseAll_Click);
			// 
			// toolStripButtonStopAll
			// 
			this.toolStripButtonStopAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonStopAll.Image = global::ZForge.Motion.Controls.Properties.Resources.gears_stop_24;
			this.toolStripButtonStopAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonStopAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonStopAll.Name = "toolStripButtonStopAll";
			this.toolStripButtonStopAll.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonStopAll.Text = "全部停止";
			this.toolStripButtonStopAll.Click += new System.EventHandler(this.toolStripButtonStopAll_Click);
			// 
			// toolStripButtonAlarmCleanAll
			// 
			this.toolStripButtonAlarmCleanAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAlarmCleanAll.Image = global::ZForge.Motion.Controls.Properties.Resources.replace2_24;
			this.toolStripButtonAlarmCleanAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonAlarmCleanAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAlarmCleanAll.Name = "toolStripButtonAlarmCleanAll";
			this.toolStripButtonAlarmCleanAll.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonAlarmCleanAll.Text = "重置全部报警记数";
			this.toolStripButtonAlarmCleanAll.Click += new System.EventHandler(this.toolStripButtonAlarmCleanAll_Click);
			// 
			// toolStripButtonCloseAll
			// 
			this.toolStripButtonCloseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonCloseAll.Image = global::ZForge.Motion.Controls.Properties.Resources.window_delete_24;
			this.toolStripButtonCloseAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonCloseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonCloseAll.Name = "toolStripButtonCloseAll";
			this.toolStripButtonCloseAll.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonCloseAll.Text = "全部关闭";
			this.toolStripButtonCloseAll.Click += new System.EventHandler(this.toolStripButtonCloseAll_Click);
			// 
			// toolStripButtonExportAll
			// 
			this.toolStripButtonExportAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonExportAll.Image = global::ZForge.Motion.Controls.Properties.Resources.disks_24;
			this.toolStripButtonExportAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonExportAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonExportAll.Name = "toolStripButtonExportAll";
			this.toolStripButtonExportAll.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonExportAll.Text = "全部导出";
			this.toolStripButtonExportAll.Click += new System.EventHandler(this.toolStripButtonExportAll_Click);
			// 
			// toolStripButtonDeleteAll
			// 
			this.toolStripButtonDeleteAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonDeleteAll.Image = global::ZForge.Motion.Controls.Properties.Resources.index_delete_24;
			this.toolStripButtonDeleteAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonDeleteAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDeleteAll.Name = "toolStripButtonDeleteAll";
			this.toolStripButtonDeleteAll.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonDeleteAll.Text = "全部删除";
			this.toolStripButtonDeleteAll.Click += new System.EventHandler(this.toolStripButtonDeleteAll_Click);
			// 
			// CameraBoard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CameraPanel);
			this.Controls.Add(this.toolStrip);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "CameraBoard";
			this.Size = new System.Drawing.Size(576, 420);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.FlowLayoutPanel CameraPanel;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBoxRatio;
		private System.Windows.Forms.ToolStripButton toolStripButtonStartAll;
		private System.Windows.Forms.ToolStripButton toolStripButtonPauseAll;
		private System.Windows.Forms.ToolStripButton toolStripButtonStopAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButtonAlarmCleanAll;
		private System.Windows.Forms.ToolStripLabel toolStripLabelZoom;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBoxShortCut;
		private System.Windows.Forms.ToolStripLabel toolStripLabelShortCut;
		private System.Windows.Forms.ToolStripButton toolStripButtonExportAll;
		private System.Windows.Forms.ToolStripButton toolStripButtonDeleteAll;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.ToolStripButton toolStripButtonCloseAll;
	}
}
