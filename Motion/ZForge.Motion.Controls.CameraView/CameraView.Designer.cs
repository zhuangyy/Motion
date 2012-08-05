namespace ZForge.Motion.Controls
{
	partial class CameraView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraView));
			this.timer = new System.Timers.Timer();
			this.panelMaster = new ZForge.Controls.HeaderPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelCameraViewEdit = new System.Windows.Forms.Panel();
			this.panelTrackBars = new System.Windows.Forms.TableLayoutPanel();
			this.trackBarDifferenceThreshold = new ZForge.Motion.Controls.TrackBarEx();
			this.trackBarSensi = new ZForge.Motion.Controls.TrackBarEx();
			this.trackBarElapse = new ZForge.Motion.Controls.TrackBarEx();
			this.toolStripRegion = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabelGridSize = new System.Windows.Forms.ToolStripLabel();
			this.toolStripComboBoxResolution = new System.Windows.Forms.ToolStripComboBox();
			this.cameraWindow = new ZForge.Motion.Controls.CameraWindow();
			this.cwStatusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelFps = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelAlarm = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelCapture = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripMain = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.toolStripButtonRegionEdit = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonRegionClear = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonRegionReverse = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
			this.toolStripDropDownButtonDetectMode = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripMenuItemDetectModeMotion = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDetectModeStillness = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripButtonKit = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonSnag = new System.Windows.Forms.ToolStripButton();
			this.toolStripDropDownButtonCapture = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripMenuItemCaptureNone = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCaptureOnAlarm = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCaptureAlways = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripButtonAlarmClean = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonRegion = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonShowMotionRect = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonBanner = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonMirror = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonFlip = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.timer)).BeginInit();
			this.panelMaster.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panelCameraViewEdit.SuspendLayout();
			this.panelTrackBars.SuspendLayout();
			this.toolStripRegion.SuspendLayout();
			this.cwStatusStrip.SuspendLayout();
			this.toolStripMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Interval = 1000;
			this.timer.SynchronizingObject = this;
			this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
			// 
			// panelMaster
			// 
			this.panelMaster.BorderColor = System.Drawing.SystemColors.ActiveCaption;
			this.panelMaster.BorderStyle = ZForge.Controls.HeaderPanelBorderStyles.None;
			this.panelMaster.CaptionBeginColor = System.Drawing.SystemColors.InactiveCaption;
			this.panelMaster.CaptionEndColor = System.Drawing.SystemColors.ActiveCaption;
			this.panelMaster.CaptionFont = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
			this.panelMaster.CaptionGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.panelMaster.CaptionHeight = 25;
			this.panelMaster.CaptionPosition = ZForge.Controls.HeaderPanelCaptionPositions.Top;
			this.panelMaster.CaptionText = "Camera";
			this.panelMaster.CaptionVisible = true;
			this.panelMaster.Controls.Add(this.panel1);
			this.panelMaster.Controls.Add(this.toolStripMain);
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
			this.panelMaster.Size = new System.Drawing.Size(340, 528);
			this.panelMaster.TabIndex = 0;
			this.panelMaster.TextAntialias = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panelCameraViewEdit);
			this.panel1.Controls.Add(this.cameraWindow);
			this.panel1.Controls.Add(this.cwStatusStrip);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 46);
			this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(340, 457);
			this.panel1.TabIndex = 1;
			// 
			// panelCameraViewEdit
			// 
			this.panelCameraViewEdit.Controls.Add(this.panelTrackBars);
			this.panelCameraViewEdit.Controls.Add(this.toolStripRegion);
			this.panelCameraViewEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelCameraViewEdit.Location = new System.Drawing.Point(0, 242);
			this.panelCameraViewEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelCameraViewEdit.Name = "panelCameraViewEdit";
			this.panelCameraViewEdit.Size = new System.Drawing.Size(340, 193);
			this.panelCameraViewEdit.TabIndex = 4;
			// 
			// panelTrackBars
			// 
			this.panelTrackBars.ColumnCount = 3;
			this.panelTrackBars.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
			this.panelTrackBars.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
			this.panelTrackBars.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
			this.panelTrackBars.Controls.Add(this.trackBarDifferenceThreshold, 0, 0);
			this.panelTrackBars.Controls.Add(this.trackBarSensi, 0, 0);
			this.panelTrackBars.Controls.Add(this.trackBarElapse, 0, 0);
			this.panelTrackBars.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTrackBars.Location = new System.Drawing.Point(0, 25);
			this.panelTrackBars.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelTrackBars.Name = "panelTrackBars";
			this.panelTrackBars.RowCount = 1;
			this.panelTrackBars.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
			this.panelTrackBars.Size = new System.Drawing.Size(340, 63);
			this.panelTrackBars.TabIndex = 2;
			// 
			// trackBarDifferenceThreshold
			// 
			this.trackBarDifferenceThreshold.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trackBarDifferenceThreshold.Location = new System.Drawing.Point(231, 5);
			this.trackBarDifferenceThreshold.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.trackBarDifferenceThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.trackBarDifferenceThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.trackBarDifferenceThreshold.Name = "trackBarDifferenceThreshold";
			this.trackBarDifferenceThreshold.Size = new System.Drawing.Size(105, 53);
			this.trackBarDifferenceThreshold.TabIndex = 2;
			this.trackBarDifferenceThreshold.TabStop = false;
			this.trackBarDifferenceThreshold.Title = "差异阀值";
			this.trackBarDifferenceThreshold.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// trackBarSensi
			// 
			this.trackBarSensi.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trackBarSensi.Location = new System.Drawing.Point(115, 4);
			this.trackBarSensi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.trackBarSensi.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.trackBarSensi.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.trackBarSensi.Name = "trackBarSensi";
			this.trackBarSensi.Size = new System.Drawing.Size(109, 55);
			this.trackBarSensi.TabIndex = 1;
			this.trackBarSensi.TabStop = false;
			this.trackBarSensi.Title = "灵敏度";
			this.trackBarSensi.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// trackBarElapse
			// 
			this.trackBarElapse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trackBarElapse.Location = new System.Drawing.Point(3, 4);
			this.trackBarElapse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.trackBarElapse.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.trackBarElapse.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.trackBarElapse.Name = "trackBarElapse";
			this.trackBarElapse.Size = new System.Drawing.Size(106, 55);
			this.trackBarElapse.TabIndex = 0;
			this.trackBarElapse.TabStop = false;
			this.trackBarElapse.Title = "侦测延时";
			this.trackBarElapse.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// toolStripRegion
			// 
			this.toolStripRegion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRegionEdit,
            this.toolStripButtonRegionClear,
            this.toolStripButtonRegionReverse,
            this.toolStripSeparator2,
            this.toolStripLabelGridSize,
            this.toolStripComboBoxResolution});
			this.toolStripRegion.Location = new System.Drawing.Point(0, 0);
			this.toolStripRegion.Name = "toolStripRegion";
			this.toolStripRegion.Size = new System.Drawing.Size(340, 25);
			this.toolStripRegion.TabIndex = 1;
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabelGridSize
			// 
			this.toolStripLabelGridSize.Name = "toolStripLabelGridSize";
			this.toolStripLabelGridSize.Size = new System.Drawing.Size(59, 22);
			this.toolStripLabelGridSize.Text = "网格大小:";
			// 
			// toolStripComboBoxResolution
			// 
			this.toolStripComboBoxResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolStripComboBoxResolution.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50"});
			this.toolStripComboBoxResolution.Name = "toolStripComboBoxResolution";
			this.toolStripComboBoxResolution.Size = new System.Drawing.Size(75, 25);
			this.toolStripComboBoxResolution.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxResolution_SelectedIndexChanged);
			// 
			// cameraWindow
			// 
			this.cameraWindow.BackColor = System.Drawing.SystemColors.AppWorkspace;
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
			this.cameraWindow.Size = new System.Drawing.Size(340, 242);
			this.cameraWindow.TabIndex = 3;
			// 
			// cwStatusStrip
			// 
			this.cwStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelFps,
            this.toolStripStatusLabelAlarm,
            this.toolStripStatusLabelCapture});
			this.cwStatusStrip.Location = new System.Drawing.Point(0, 435);
			this.cwStatusStrip.Name = "cwStatusStrip";
			this.cwStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.cwStatusStrip.Size = new System.Drawing.Size(340, 22);
			this.cwStatusStrip.TabIndex = 0;
			// 
			// toolStripStatusLabelStatus
			// 
			this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
			this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(32, 17);
			this.toolStripStatusLabelStatus.Text = "停止";
			// 
			// toolStripStatusLabelFps
			// 
			this.toolStripStatusLabelFps.Name = "toolStripStatusLabelFps";
			this.toolStripStatusLabelFps.Size = new System.Drawing.Size(37, 17);
			this.toolStripStatusLabelFps.Text = "0 fps";
			// 
			// toolStripStatusLabelAlarm
			// 
			this.toolStripStatusLabelAlarm.Name = "toolStripStatusLabelAlarm";
			this.toolStripStatusLabelAlarm.Size = new System.Drawing.Size(46, 17);
			this.toolStripStatusLabelAlarm.Text = "报警: 0";
			// 
			// toolStripStatusLabelCapture
			// 
			this.toolStripStatusLabelCapture.Name = "toolStripStatusLabelCapture";
			this.toolStripStatusLabelCapture.Size = new System.Drawing.Size(0, 17);
			// 
			// toolStripMain
			// 
			this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonStart,
            this.toolStripButtonPause,
            this.toolStripButtonStop,
            this.toolStripButtonClose,
            this.toolStripSeparator1,
            this.toolStripDropDownButtonDetectMode,
            this.toolStripButtonKit,
            this.toolStripButtonSnag,
            this.toolStripDropDownButtonCapture,
            this.toolStripButtonAlarmClean,
            this.toolStripSeparator3,
            this.toolStripButtonZoomIn,
            this.toolStripButtonZoomOut,
            this.toolStripButtonMirror,
            this.toolStripButtonFlip,
            this.toolStripSeparator4,
            this.toolStripButtonRegion,
            this.toolStripButtonShowMotionRect,
            this.toolStripButtonBanner});
			this.toolStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStripMain.Location = new System.Drawing.Point(0, 0);
			this.toolStripMain.Name = "toolStripMain";
			this.toolStripMain.Size = new System.Drawing.Size(340, 46);
			this.toolStripMain.TabIndex = 0;
			this.toolStripMain.Text = "toolStrip1";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// toolStripButtonRegionEdit
			// 
			this.toolStripButtonRegionEdit.CheckOnClick = true;
			this.toolStripButtonRegionEdit.Image = global::ZForge.Motion.Controls.Properties.Resources.column;
			this.toolStripButtonRegionEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRegionEdit.Name = "toolStripButtonRegionEdit";
			this.toolStripButtonRegionEdit.Size = new System.Drawing.Size(76, 22);
			this.toolStripButtonRegionEdit.Text = "区域编辑";
			this.toolStripButtonRegionEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.toolStripButtonRegionEdit.ToolTipText = "区域编辑在摄像头启动后生效";
			this.toolStripButtonRegionEdit.CheckedChanged += new System.EventHandler(this.toolStripButtonRegionEdit_CheckedChanged);
			// 
			// toolStripButtonRegionClear
			// 
			this.toolStripButtonRegionClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRegionClear.Enabled = false;
			this.toolStripButtonRegionClear.Image = global::ZForge.Motion.Controls.Properties.Resources.column_delete;
			this.toolStripButtonRegionClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRegionClear.Name = "toolStripButtonRegionClear";
			this.toolStripButtonRegionClear.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonRegionClear.Text = "取消区域设定";
			this.toolStripButtonRegionClear.Click += new System.EventHandler(this.toolStripButtonRegionClear_Click);
			// 
			// toolStripButtonRegionReverse
			// 
			this.toolStripButtonRegionReverse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRegionReverse.Enabled = false;
			this.toolStripButtonRegionReverse.Image = global::ZForge.Motion.Controls.Properties.Resources.table_replace;
			this.toolStripButtonRegionReverse.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRegionReverse.Name = "toolStripButtonRegionReverse";
			this.toolStripButtonRegionReverse.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonRegionReverse.Text = "区域反选";
			this.toolStripButtonRegionReverse.Click += new System.EventHandler(this.toolStripButtonRegionReverse_Click);
			// 
			// toolStripButtonStart
			// 
			this.toolStripButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonStart.Image = global::ZForge.Motion.Controls.Properties.Resources.StartCamera;
			this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonStart.Name = "toolStripButtonStart";
			this.toolStripButtonStart.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonStart.Text = "启动";
			this.toolStripButtonStart.Click += new System.EventHandler(this.toolStripButtonStart_Click);
			// 
			// toolStripButtonPause
			// 
			this.toolStripButtonPause.CheckOnClick = true;
			this.toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonPause.Image = global::ZForge.Motion.Controls.Properties.Resources.PauseCamera;
			this.toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonPause.Name = "toolStripButtonPause";
			this.toolStripButtonPause.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonPause.Text = "暂停";
			this.toolStripButtonPause.CheckedChanged += new System.EventHandler(this.toolStripButtonPause_CheckedChanged);
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
			this.toolStripButtonClose.ToolTipText = "关闭监控窗口";
			this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonClose_Click);
			// 
			// toolStripDropDownButtonDetectMode
			// 
			this.toolStripDropDownButtonDetectMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButtonDetectMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDetectModeMotion,
            this.toolStripMenuItemDetectModeStillness});
			this.toolStripDropDownButtonDetectMode.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonDetectMode.Image")));
			this.toolStripDropDownButtonDetectMode.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonDetectMode.Name = "toolStripDropDownButtonDetectMode";
			this.toolStripDropDownButtonDetectMode.Size = new System.Drawing.Size(29, 20);
			this.toolStripDropDownButtonDetectMode.Text = "toolStripDropDownButton1";
			// 
			// toolStripMenuItemDetectModeMotion
			// 
			this.toolStripMenuItemDetectModeMotion.Image = global::ZForge.Motion.Controls.Properties.Resources.lightbulb_on;
			this.toolStripMenuItemDetectModeMotion.Name = "toolStripMenuItemDetectModeMotion";
			this.toolStripMenuItemDetectModeMotion.Size = new System.Drawing.Size(124, 22);
			this.toolStripMenuItemDetectModeMotion.Text = "动作监测";
			this.toolStripMenuItemDetectModeMotion.Click += new System.EventHandler(this.toolStripMenuItemDetectModeMotion_Click);
			// 
			// toolStripMenuItemDetectModeStillness
			// 
			this.toolStripMenuItemDetectModeStillness.Image = global::ZForge.Motion.Controls.Properties.Resources.lightbulb;
			this.toolStripMenuItemDetectModeStillness.Name = "toolStripMenuItemDetectModeStillness";
			this.toolStripMenuItemDetectModeStillness.Size = new System.Drawing.Size(124, 22);
			this.toolStripMenuItemDetectModeStillness.Text = "静止监测";
			this.toolStripMenuItemDetectModeStillness.Click += new System.EventHandler(this.toolStripMenuItemDetectModeStillness_Click);
			// 
			// toolStripButtonKit
			// 
			this.toolStripButtonKit.CheckOnClick = true;
			this.toolStripButtonKit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonKit.Image = global::ZForge.Motion.Controls.Properties.Resources.preferences;
			this.toolStripButtonKit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonKit.Name = "toolStripButtonKit";
			this.toolStripButtonKit.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonKit.Text = "配置";
			this.toolStripButtonKit.Click += new System.EventHandler(this.toolStripButtonKit_Click);
			// 
			// toolStripButtonSnag
			// 
			this.toolStripButtonSnag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonSnag.Image = global::ZForge.Motion.Controls.Properties.Resources.camera2;
			this.toolStripButtonSnag.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSnag.Name = "toolStripButtonSnag";
			this.toolStripButtonSnag.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonSnag.Text = "截屏";
			this.toolStripButtonSnag.ToolTipText = "截屏 (截屏在摄像头启动后生效)";
			this.toolStripButtonSnag.Click += new System.EventHandler(this.toolStripButtonSnag_Click);
			// 
			// toolStripDropDownButtonCapture
			// 
			this.toolStripDropDownButtonCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButtonCapture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCaptureNone,
            this.toolStripMenuItemCaptureOnAlarm,
            this.toolStripMenuItemCaptureAlways});
			this.toolStripDropDownButtonCapture.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonCapture.Image")));
			this.toolStripDropDownButtonCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonCapture.Name = "toolStripDropDownButtonCapture";
			this.toolStripDropDownButtonCapture.Size = new System.Drawing.Size(29, 20);
			this.toolStripDropDownButtonCapture.Text = "toolStripDropDownButton1";
			// 
			// toolStripMenuItemCaptureNone
			// 
			this.toolStripMenuItemCaptureNone.Image = global::ZForge.Motion.Controls.Properties.Resources.capture_none;
			this.toolStripMenuItemCaptureNone.Name = "toolStripMenuItemCaptureNone";
			this.toolStripMenuItemCaptureNone.Size = new System.Drawing.Size(136, 22);
			this.toolStripMenuItemCaptureNone.Text = "不录像";
			this.toolStripMenuItemCaptureNone.Click += new System.EventHandler(this.toolStripMenuItemCaptureNone_Click);
			// 
			// toolStripMenuItemCaptureOnAlarm
			// 
			this.toolStripMenuItemCaptureOnAlarm.Image = global::ZForge.Motion.Controls.Properties.Resources.capture_alarm;
			this.toolStripMenuItemCaptureOnAlarm.Name = "toolStripMenuItemCaptureOnAlarm";
			this.toolStripMenuItemCaptureOnAlarm.Size = new System.Drawing.Size(136, 22);
			this.toolStripMenuItemCaptureOnAlarm.Text = "报警时录像";
			this.toolStripMenuItemCaptureOnAlarm.Click += new System.EventHandler(this.toolStripMenuItemCaptureOnAlarm_Click);
			// 
			// toolStripMenuItemCaptureAlways
			// 
			this.toolStripMenuItemCaptureAlways.Image = global::ZForge.Motion.Controls.Properties.Resources.capture;
			this.toolStripMenuItemCaptureAlways.Name = "toolStripMenuItemCaptureAlways";
			this.toolStripMenuItemCaptureAlways.Size = new System.Drawing.Size(136, 22);
			this.toolStripMenuItemCaptureAlways.Text = "持续录像";
			this.toolStripMenuItemCaptureAlways.Click += new System.EventHandler(this.toolStripMenuItemCaptureAlways_Click);
			// 
			// toolStripButtonAlarmClean
			// 
			this.toolStripButtonAlarmClean.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonAlarmClean.Image = global::ZForge.Motion.Controls.Properties.Resources.refresh;
			this.toolStripButtonAlarmClean.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAlarmClean.Name = "toolStripButtonAlarmClean";
			this.toolStripButtonAlarmClean.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonAlarmClean.Text = "重置报警记数";
			this.toolStripButtonAlarmClean.Click += new System.EventHandler(this.toolStripButtonAlarmClean_Click);
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
			// toolStripButtonRegion
			// 
			this.toolStripButtonRegion.CheckOnClick = true;
			this.toolStripButtonRegion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRegion.Image = global::ZForge.Motion.Controls.Properties.Resources.EditArea;
			this.toolStripButtonRegion.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRegion.Name = "toolStripButtonRegion";
			this.toolStripButtonRegion.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonRegion.Text = "显示监控区域";
			this.toolStripButtonRegion.CheckedChanged += new System.EventHandler(this.toolStripButtonRegion_CheckedChanged);
			// 
			// toolStripButtonShowMotionRect
			// 
			this.toolStripButtonShowMotionRect.CheckOnClick = true;
			this.toolStripButtonShowMotionRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonShowMotionRect.Image = global::ZForge.Motion.Controls.Properties.Resources.breakpoints;
			this.toolStripButtonShowMotionRect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonShowMotionRect.Name = "toolStripButtonShowMotionRect";
			this.toolStripButtonShowMotionRect.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonShowMotionRect.Text = "显示动作识别框";
			this.toolStripButtonShowMotionRect.CheckStateChanged += new System.EventHandler(this.toolStripButtonShowMotionRect_CheckStateChanged);
			// 
			// toolStripButtonBanner
			// 
			this.toolStripButtonBanner.CheckOnClick = true;
			this.toolStripButtonBanner.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonBanner.Image = global::ZForge.Motion.Controls.Properties.Resources.message_add;
			this.toolStripButtonBanner.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonBanner.Name = "toolStripButtonBanner";
			this.toolStripButtonBanner.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonBanner.Text = "添加图像标签";
			this.toolStripButtonBanner.CheckStateChanged += new System.EventHandler(this.toolStripButtonBanner_CheckStateChanged);
			// 
			// toolStripButtonMirror
			// 
			this.toolStripButtonMirror.CheckOnClick = true;
			this.toolStripButtonMirror.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonMirror.Image = global::ZForge.Motion.Controls.Properties.Resources.window_split_hor_16;
			this.toolStripButtonMirror.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonMirror.Name = "toolStripButtonMirror";
			this.toolStripButtonMirror.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonMirror.Text = "水平翻转";
			this.toolStripButtonMirror.CheckedChanged += new System.EventHandler(this.toolStripButtonMirror_CheckedChanged);
			// 
			// toolStripButtonFlip
			// 
			this.toolStripButtonFlip.CheckOnClick = true;
			this.toolStripButtonFlip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonFlip.Image = global::ZForge.Motion.Controls.Properties.Resources.window_split_ver_16;
			this.toolStripButtonFlip.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonFlip.Name = "toolStripButtonFlip";
			this.toolStripButtonFlip.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonFlip.Text = "垂直翻转";
			this.toolStripButtonFlip.CheckedChanged += new System.EventHandler(this.toolStripButtonFlip_CheckedChanged);
			// 
			// CameraView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.panelMaster);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "CameraView";
			this.Size = new System.Drawing.Size(340, 528);
			((System.ComponentModel.ISupportInitialize)(this.timer)).EndInit();
			this.panelMaster.ResumeLayout(false);
			this.panelMaster.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panelCameraViewEdit.ResumeLayout(false);
			this.panelCameraViewEdit.PerformLayout();
			this.panelTrackBars.ResumeLayout(false);
			this.toolStripRegion.ResumeLayout(false);
			this.toolStripRegion.PerformLayout();
			this.cwStatusStrip.ResumeLayout(false);
			this.cwStatusStrip.PerformLayout();
			this.toolStripMain.ResumeLayout(false);
			this.toolStripMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private ZForge.Controls.HeaderPanel panelMaster;
		private System.Windows.Forms.ToolStrip toolStripMain;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.StatusStrip cwStatusStrip;
		private System.Timers.Timer timer;
		private CameraWindow cameraWindow;
		private System.Windows.Forms.ToolStripButton toolStripButtonStart;
		private System.Windows.Forms.ToolStripButton toolStripButtonStop;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFps;
		private System.Windows.Forms.ToolStripButton toolStripButtonClose;
		private System.Windows.Forms.ToolStripButton toolStripButtonPause;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAlarm;
		private System.Windows.Forms.Panel panelCameraViewEdit;
		private System.Windows.Forms.ToolStripButton toolStripButtonKit;
		private System.Windows.Forms.ToolStripButton toolStripButtonRegion;
		private System.Windows.Forms.TableLayoutPanel panelTrackBars;
		private Motion.Controls.TrackBarEx trackBarSensi;
		private Motion.Controls.TrackBarEx trackBarElapse;
		private System.Windows.Forms.ToolStrip toolStripRegion;
		private System.Windows.Forms.ToolStripButton toolStripButtonRegionEdit;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBoxResolution;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButtonRegionClear;
		private System.Windows.Forms.ToolStripLabel toolStripLabelGridSize;
		private System.Windows.Forms.ToolStripButton toolStripButtonAlarmClean;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCapture;
		private System.Windows.Forms.ToolStripButton toolStripButtonRegionReverse;
		private System.Windows.Forms.ToolStripButton toolStripButtonBanner;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton toolStripButtonSnag;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonCapture;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCaptureNone;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCaptureOnAlarm;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCaptureAlways;
		private System.Windows.Forms.ToolStripButton toolStripButtonShowMotionRect;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonDetectMode;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDetectModeMotion;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDetectModeStillness;
		private System.Windows.Forms.ImageList imageList1;
		private TrackBarEx trackBarDifferenceThreshold;
		private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
		private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton toolStripButtonMirror;
		private System.Windows.Forms.ToolStripButton toolStripButtonFlip;
	}
}
