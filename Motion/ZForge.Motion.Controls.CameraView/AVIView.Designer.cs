using ZForge.Globalization;

namespace ZForge.Motion.Controls
{
	partial class AVIView
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
			this.timer = new System.Timers.Timer();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.panelMaster = new ZForge.Controls.HeaderPanel();
			this.panelView = new System.Windows.Forms.Panel();
			this.panelInfo = new System.Windows.Forms.Panel();
			this.panelControl = new System.Windows.Forms.Panel();
			this.groupBoxTrack = new System.Windows.Forms.GroupBox();
			this.trackBarTime = new System.Windows.Forms.TrackBar();
			this.cameraWindow = new Motion.Controls.CameraWindow();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripControl = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonSnag = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonSpeedInc = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSpeedDec = new System.Windows.Forms.ToolStripButton();
			this.toolStripLabelSpeed = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			((System.ComponentModel.ISupportInitialize)(this.timer)).BeginInit();
			this.panelMaster.SuspendLayout();
			this.panelView.SuspendLayout();
			this.panelInfo.SuspendLayout();
			this.panelControl.SuspendLayout();
			this.groupBoxTrack.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarTime)).BeginInit();
			this.statusStrip.SuspendLayout();
			this.toolStripControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.SynchronizingObject = this;
			this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "avi";
			this.saveFileDialog.Filter = "录像文件 (*.avi)|*.avi";
			this.saveFileDialog.Title = "导出录像";
			// 
			// panelMaster
			// 
			this.panelMaster.BorderColor = System.Drawing.SystemColors.ActiveCaption;
			this.panelMaster.BorderStyle = ZForge.Controls.HeaderPanelBorderStyles.None;
			this.panelMaster.CaptionBeginColor = System.Drawing.SystemColors.InactiveCaption;
			this.panelMaster.CaptionEndColor = System.Drawing.SystemColors.ActiveCaption;
			this.panelMaster.CaptionGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.panelMaster.CaptionHeight = 25;
			this.panelMaster.CaptionPosition = ZForge.Controls.HeaderPanelCaptionPositions.Top;
			this.panelMaster.CaptionText = "AVI";
			this.panelMaster.CaptionVisible = true;
			this.panelMaster.Controls.Add(this.panelView);
			this.panelMaster.Controls.Add(this.statusStrip);
			this.panelMaster.Controls.Add(this.toolStripControl);
			this.panelMaster.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMaster.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
			this.panelMaster.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.panelMaster.GradientEnd = System.Drawing.SystemColors.Window;
			this.panelMaster.GradientStart = System.Drawing.SystemColors.Window;
			this.panelMaster.Location = new System.Drawing.Point(0, 0);
			this.panelMaster.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelMaster.Name = "panelMaster";
			this.panelMaster.PanelIcon = null;
			this.panelMaster.PanelIconVisible = false;
			this.panelMaster.Size = new System.Drawing.Size(395, 429);
			this.panelMaster.TabIndex = 0;
			this.panelMaster.TextAntialias = true;
			// 
			// panelView
			// 
			this.panelView.Controls.Add(this.panelInfo);
			this.panelView.Controls.Add(this.cameraWindow);
			this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelView.Location = new System.Drawing.Point(0, 23);
			this.panelView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelView.Name = "panelView";
			this.panelView.Size = new System.Drawing.Size(395, 359);
			this.panelView.TabIndex = 2;
			// 
			// panelInfo
			// 
			this.panelInfo.Controls.Add(this.panelControl);
			this.panelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelInfo.Location = new System.Drawing.Point(0, 260);
			this.panelInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelInfo.Name = "panelInfo";
			this.panelInfo.Size = new System.Drawing.Size(395, 99);
			this.panelInfo.TabIndex = 2;
			// 
			// panelControl
			// 
			this.panelControl.Controls.Add(this.groupBoxTrack);
			this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl.Location = new System.Drawing.Point(0, 0);
			this.panelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelControl.Name = "panelControl";
			this.panelControl.Size = new System.Drawing.Size(395, 99);
			this.panelControl.TabIndex = 0;
			// 
			// groupBoxTrack
			// 
			this.groupBoxTrack.Controls.Add(this.trackBarTime);
			this.groupBoxTrack.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxTrack.Location = new System.Drawing.Point(0, 0);
			this.groupBoxTrack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBoxTrack.Name = "groupBoxTrack";
			this.groupBoxTrack.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBoxTrack.Size = new System.Drawing.Size(395, 99);
			this.groupBoxTrack.TabIndex = 0;
			this.groupBoxTrack.TabStop = false;
			this.groupBoxTrack.Text = "当前位置： 0";
			// 
			// trackBarTime
			// 
			this.trackBarTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trackBarTime.Location = new System.Drawing.Point(3, 20);
			this.trackBarTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.trackBarTime.Maximum = 100;
			this.trackBarTime.Name = "trackBarTime";
			this.trackBarTime.Size = new System.Drawing.Size(389, 75);
			this.trackBarTime.TabIndex = 1;
			this.trackBarTime.ValueChanged += new System.EventHandler(this.trackBarTime_ValueChanged);
			// 
			// cameraWindow
			// 
			this.cameraWindow.Banner = false;
			this.cameraWindow.Camera = null;
			this.cameraWindow.CameraClass = null;
			this.cameraWindow.Dock = System.Windows.Forms.DockStyle.Top;
			this.cameraWindow.GridSize = 10;
			this.cameraWindow.Location = new System.Drawing.Point(0, 0);
			this.cameraWindow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cameraWindow.Name = "cameraWindow";
			this.cameraWindow.Paused = false;
			this.cameraWindow.RegionEditing = false;
			this.cameraWindow.ShowRegion = false;
			this.cameraWindow.Size = new System.Drawing.Size(395, 260);
			this.cameraWindow.TabIndex = 1;
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelInfo});
			this.statusStrip.Location = new System.Drawing.Point(0, 382);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.statusStrip.Size = new System.Drawing.Size(395, 22);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabelStatus
			// 
			this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
			this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(32, 17);
			this.toolStripStatusLabelStatus.Text = "停止";
			// 
			// toolStripStatusLabelInfo
			// 
			this.toolStripStatusLabelInfo.Name = "toolStripStatusLabelInfo";
			this.toolStripStatusLabelInfo.Size = new System.Drawing.Size(0, 17);
			// 
			// toolStripControl
			// 
			this.toolStripControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonStart,
            this.toolStripButtonPause,
            this.toolStripButtonStop,
            this.toolStripButtonClose,
            this.toolStripSeparator1,
            this.toolStripButtonSnag,
            this.toolStripSeparator3,
            this.toolStripButtonSpeedInc,
            this.toolStripButtonSpeedDec,
            this.toolStripLabelSpeed,
            this.toolStripSeparator2,
            this.toolStripButtonZoomIn,
            this.toolStripButtonZoomOut,
            this.toolStripSeparator4,
            this.toolStripButtonSaveAs,
            this.toolStripButtonDelete});
			this.toolStripControl.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStripControl.Location = new System.Drawing.Point(0, 0);
			this.toolStripControl.Name = "toolStripControl";
			this.toolStripControl.Size = new System.Drawing.Size(395, 23);
			this.toolStripControl.TabIndex = 0;
			this.toolStripControl.Text = "toolStrip1";
			// 
			// toolStripButtonStart
			// 
			this.toolStripButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonStart.Image = global::ZForge.Motion.Controls.Properties.Resources.StartCamera;
			this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonStart.Name = "toolStripButtonStart";
			this.toolStripButtonStart.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonStart.Text = "播放";
			this.toolStripButtonStart.Click += new System.EventHandler(this.toolStripButtonStart_Click);
			// 
			// toolStripButtonPause
			// 
			this.toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonPause.Image = global::ZForge.Motion.Controls.Properties.Resources.PauseCamera;
			this.toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonPause.Name = "toolStripButtonPause";
			this.toolStripButtonPause.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonPause.Text = "暂停";
			this.toolStripButtonPause.Click += new System.EventHandler(this.toolStripButtonPause_Click);
			// 
			// toolStripButtonStop
			// 
			this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonStop.Image = global::ZForge.Motion.Controls.Properties.Resources.StopCamera;
			this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonStop.Name = "toolStripButtonStop";
			this.toolStripButtonStop.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonStop.Text = "停止";
			this.toolStripButtonStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
			// 
			// toolStripButtonClose
			// 
			this.toolStripButtonClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonClose.Image = global::ZForge.Motion.Controls.Properties.Resources.window_delete_16;
			this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonClose.Name = "toolStripButtonClose";
			this.toolStripButtonClose.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonClose.Text = "关闭";
			this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonClose_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
			// 
			// toolStripButtonSnag
			// 
			this.toolStripButtonSnag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSnag.Image = global::ZForge.Motion.Controls.Properties.Resources.camera2;
			this.toolStripButtonSnag.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSnag.Name = "toolStripButtonSnag";
			this.toolStripButtonSnag.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonSnag.Text = "截屏";
			this.toolStripButtonSnag.Click += new System.EventHandler(this.toolStripButtonSnag_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
			// 
			// toolStripButtonSpeedInc
			// 
			this.toolStripButtonSpeedInc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSpeedInc.Image = global::ZForge.Motion.Controls.Properties.Resources.arrow_up_blue;
			this.toolStripButtonSpeedInc.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSpeedInc.Name = "toolStripButtonSpeedInc";
			this.toolStripButtonSpeedInc.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonSpeedInc.Text = "加快播放速度";
			this.toolStripButtonSpeedInc.Click += new System.EventHandler(this.toolStripButtonSpeedInc_Click);
			// 
			// toolStripButtonSpeedDec
			// 
			this.toolStripButtonSpeedDec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSpeedDec.Image = global::ZForge.Motion.Controls.Properties.Resources.arrow_down_blue;
			this.toolStripButtonSpeedDec.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSpeedDec.Name = "toolStripButtonSpeedDec";
			this.toolStripButtonSpeedDec.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonSpeedDec.Text = "减慢播放速度";
			this.toolStripButtonSpeedDec.Click += new System.EventHandler(this.toolStripButtonSpeedDec_Click);
			// 
			// toolStripLabelSpeed
			// 
			this.toolStripLabelSpeed.Name = "toolStripLabelSpeed";
			this.toolStripLabelSpeed.Size = new System.Drawing.Size(21, 17);
			this.toolStripLabelSpeed.Text = "1x";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
			// 
			// toolStripButtonSaveAs
			// 
			this.toolStripButtonSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSaveAs.Image = global::ZForge.Motion.Controls.Properties.Resources.save_as;
			this.toolStripButtonSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
			this.toolStripButtonSaveAs.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonSaveAs.Text = "另存为...";
			this.toolStripButtonSaveAs.Click += new System.EventHandler(this.toolStripButtonSaveAs_Click);
			// 
			// toolStripButtonDelete
			// 
			this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonDelete.Image = global::ZForge.Motion.Controls.Properties.Resources.delete_16;
			this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDelete.Name = "toolStripButtonDelete";
			this.toolStripButtonDelete.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonDelete.Text = "删除";
			this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
			// 
			// toolStripButtonZoomOut
			// 
			this.toolStripButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonZoomOut.Image = global::ZForge.Motion.Controls.Properties.Resources.zoom_out_16;
			this.toolStripButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonZoomOut.Name = "toolStripButtonZoomOut";
			this.toolStripButtonZoomOut.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonZoomOut.Text = "缩小";
			this.toolStripButtonZoomOut.Click += new System.EventHandler(this.toolStripButtonZoomOut_Click);
			// 
			// toolStripButtonZoomIn
			// 
			this.toolStripButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonZoomIn.Image = global::ZForge.Motion.Controls.Properties.Resources.zoom_in_16;
			this.toolStripButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonZoomIn.Name = "toolStripButtonZoomIn";
			this.toolStripButtonZoomIn.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonZoomIn.Text = "放大";
			this.toolStripButtonZoomIn.Click += new System.EventHandler(this.toolStripButtonZoomIn_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
			// 
			// AVIView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.panelMaster);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "AVIView";
			this.Size = new System.Drawing.Size(395, 429);
			((System.ComponentModel.ISupportInitialize)(this.timer)).EndInit();
			this.panelMaster.ResumeLayout(false);
			this.panelMaster.PerformLayout();
			this.panelView.ResumeLayout(false);
			this.panelInfo.ResumeLayout(false);
			this.panelControl.ResumeLayout(false);
			this.groupBoxTrack.ResumeLayout(false);
			this.groupBoxTrack.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarTime)).EndInit();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.toolStripControl.ResumeLayout(false);
			this.toolStripControl.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private ZForge.Controls.HeaderPanel panelMaster;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStrip toolStripControl;
		private System.Windows.Forms.Panel panelView;
		private Motion.Controls.CameraWindow cameraWindow;
		private System.Windows.Forms.Panel panelInfo;
		private System.Windows.Forms.Panel panelControl;
		private System.Windows.Forms.ToolStripButton toolStripButtonStart;
		private System.Windows.Forms.ToolStripButton toolStripButtonPause;
		private System.Windows.Forms.ToolStripButton toolStripButtonStop;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
		private System.Windows.Forms.ToolStripButton toolStripButtonClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButtonSpeedInc;
		private System.Windows.Forms.ToolStripButton toolStripButtonSpeedDec;
		private System.Windows.Forms.ToolStripLabel toolStripLabelSpeed;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelInfo;
		private System.Windows.Forms.GroupBox groupBoxTrack;
		private System.Windows.Forms.TrackBar trackBarTime;
		private System.Timers.Timer timer;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButtonSaveAs;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripButton toolStripButtonSnag;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
		private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
		private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
	}
}
