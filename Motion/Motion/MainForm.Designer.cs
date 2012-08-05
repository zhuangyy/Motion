using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Motion.Controls;
using System.Windows.Forms;
using ZForge.Controls.RSS;

namespace Motion
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.panel = new System.Windows.Forms.Panel();
      this.panelBoard = new ZForge.Controls.HeaderPanel();
      this.boardCamera = new ZForge.Motion.Controls.CameraBoard();
      this.splitterSub = new ZForge.Controls.CollapsibleSplitter.CollapsibleSplitter();
      this.tabControlSub = new System.Windows.Forms.TabControl();
      this.tabPageLog = new System.Windows.Forms.TabPage();
      this.panelLogMaster = new System.Windows.Forms.Panel();
      this.logViewer = new ZForge.Motion.Controls.ExtendedLogViewer();
      this.imageListMain = new System.Windows.Forms.ImageList(this.components);
      this.splitterMain = new ZForge.Controls.CollapsibleSplitter.CollapsibleSplitter();
      this.panelTree = new ZForge.Controls.HeaderPanel();
      this.treeCamera = new ZForge.Motion.Controls.CameraTree();
      this.menuStripMain = new System.Windows.Forms.MenuStrip();
      this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemSaveOnExit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemTools = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemViewAVI = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemViewPIC = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItemImport = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItemPref = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemSelectLang = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemCheckUpdate = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemRegister = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItemPlugIns = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.notifyIconTray = new System.Windows.Forms.NotifyIcon(this.components);
      this.contextMenuStripTray = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.toolStripMenuItemTrayRestore = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemTrayViewAVI = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemTrayViewPIC = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparatorTray1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMenuItemTrayQuit = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripBottom = new System.Windows.Forms.ToolStrip();
      this.toolStripMainButtonViewAVI = new System.Windows.Forms.ToolStripButton();
      this.toolStripMainButtonViewPIC = new System.Windows.Forms.ToolStripButton();
      this.toolStripMainButtonPref = new System.Windows.Forms.ToolStripButton();
      this.toolStripMainButtonPlugIns = new System.Windows.Forms.ToolStripButton();
      this.toolStripMainButtonAbout = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripMainLabelStorage = new System.Windows.Forms.ToolStripLabel();
      this.toolStripMain = new System.Windows.Forms.ToolStrip();
      this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
      this.panel.SuspendLayout();
      this.panelBoard.SuspendLayout();
      this.tabControlSub.SuspendLayout();
      this.tabPageLog.SuspendLayout();
      this.panelLogMaster.SuspendLayout();
      this.panelTree.SuspendLayout();
      this.menuStripMain.SuspendLayout();
      this.contextMenuStripTray.SuspendLayout();
      this.toolStripBottom.SuspendLayout();
      this.toolStripMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel
      // 
      this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel.Controls.Add(this.panelBoard);
      this.panel.Controls.Add(this.splitterSub);
      this.panel.Controls.Add(this.tabControlSub);
      this.panel.Controls.Add(this.splitterMain);
      this.panel.Controls.Add(this.panelTree);
      this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel.Location = new System.Drawing.Point(0, 53);
      this.panel.Name = "panel";
      this.panel.Size = new System.Drawing.Size(821, 518);
      this.panel.TabIndex = 1;
      // 
      // panelBoard
      // 
      this.panelBoard.BorderColor = System.Drawing.SystemColors.ActiveCaption;
      this.panelBoard.BorderStyle = ZForge.Controls.HeaderPanelBorderStyles.None;
      this.panelBoard.CaptionBeginColor = System.Drawing.SystemColors.InactiveCaption;
      this.panelBoard.CaptionEndColor = System.Drawing.SystemColors.ActiveCaption;
      this.panelBoard.CaptionFont = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
      this.panelBoard.CaptionGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
      this.panelBoard.CaptionHeight = 25;
      this.panelBoard.CaptionPosition = ZForge.Controls.HeaderPanelCaptionPositions.Top;
      this.panelBoard.CaptionText = "监控面板";
      this.panelBoard.CaptionVisible = true;
      this.panelBoard.Controls.Add(this.boardCamera);
      this.panelBoard.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelBoard.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
      this.panelBoard.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
      this.panelBoard.GradientEnd = System.Drawing.SystemColors.Window;
      this.panelBoard.GradientStart = System.Drawing.SystemColors.Window;
      this.panelBoard.Location = new System.Drawing.Point(244, 0);
      this.panelBoard.Name = "panelBoard";
      this.panelBoard.PanelIcon = null;
      this.panelBoard.PanelIconVisible = false;
      this.panelBoard.Size = new System.Drawing.Size(573, 212);
      this.panelBoard.TabIndex = 9;
      this.panelBoard.TextAntialias = true;
      // 
      // boardCamera
      // 
      this.boardCamera.Dock = System.Windows.Forms.DockStyle.Fill;
      this.boardCamera.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.boardCamera.Location = new System.Drawing.Point(0, 0);
      this.boardCamera.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.boardCamera.Name = "boardCamera";
      this.boardCamera.Size = new System.Drawing.Size(573, 187);
      this.boardCamera.Style = ZForge.Motion.Controls.CameraBoardStyle.CAMERA;
      this.boardCamera.TabIndex = 3;
      this.boardCamera.ViewRatio = 1F;
      this.boardCamera.CameraBoardAddNew += new ZForge.Motion.Controls.CameraBoardAddNewHandler(this.cameraBoardMain_CameraBoardAddNew);
      // 
      // splitterSub
      // 
      this.splitterSub.AnimationDelay = 20;
      this.splitterSub.AnimationStep = 20;
      this.splitterSub.BorderStyle3D = System.Windows.Forms.Border3DStyle.Bump;
      this.splitterSub.ControlToHide = this.tabControlSub;
      this.splitterSub.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitterSub.ExpandParentForm = false;
      this.splitterSub.Location = new System.Drawing.Point(244, 212);
      this.splitterSub.Name = "splitterSub";
      this.splitterSub.TabIndex = 8;
      this.splitterSub.TabStop = false;
      this.splitterSub.UseAnimations = false;
      this.splitterSub.VisualStyle = ZForge.Controls.CollapsibleSplitter.CollapsibleSplitter.VisualStyles.Mozilla;
      // 
      // tabControlSub
      // 
      this.tabControlSub.Controls.Add(this.tabPageLog);
      this.tabControlSub.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.tabControlSub.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.tabControlSub.HotTrack = true;
      this.tabControlSub.ImageList = this.imageListMain;
      this.tabControlSub.Location = new System.Drawing.Point(244, 220);
      this.tabControlSub.Name = "tabControlSub";
      this.tabControlSub.SelectedIndex = 0;
      this.tabControlSub.Size = new System.Drawing.Size(573, 294);
      this.tabControlSub.TabIndex = 1;
      // 
      // tabPageLog
      // 
      this.tabPageLog.Controls.Add(this.panelLogMaster);
      this.tabPageLog.ImageIndex = 1;
      this.tabPageLog.Location = new System.Drawing.Point(4, 26);
      this.tabPageLog.Name = "tabPageLog";
      this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageLog.Size = new System.Drawing.Size(565, 264);
      this.tabPageLog.TabIndex = 1;
      this.tabPageLog.Text = "运行日志";
      this.tabPageLog.UseVisualStyleBackColor = true;
      // 
      // panelLogMaster
      // 
      this.panelLogMaster.Controls.Add(this.logViewer);
      this.panelLogMaster.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelLogMaster.Location = new System.Drawing.Point(3, 3);
      this.panelLogMaster.Name = "panelLogMaster";
      this.panelLogMaster.Size = new System.Drawing.Size(559, 258);
      this.panelLogMaster.TabIndex = 0;
      // 
      // logViewer
      // 
      this.logViewer.CountError = 0;
      this.logViewer.CountInfo = 0;
      this.logViewer.CountWarn = 0;
      this.logViewer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.logViewer.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.logViewer.Location = new System.Drawing.Point(0, 0);
      this.logViewer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.logViewer.Name = "logViewer";
      this.logViewer.ShowError = true;
      this.logViewer.ShowInfo = true;
      this.logViewer.ShowWarning = true;
      this.logViewer.Size = new System.Drawing.Size(559, 258);
      this.logViewer.TabIndex = 0;
      this.logViewer.CountChanged += new ZForge.Controls.Logs.LogViewerCountEventHandler(this.logViewer_CountChanged);
      // 
      // imageListMain
      // 
      this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
      this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
      this.imageListMain.Images.SetKeyName(0, "movie.png");
      this.imageListMain.Images.SetKeyName(1, "scroll.png");
      // 
      // splitterMain
      // 
      this.splitterMain.AnimationDelay = 20;
      this.splitterMain.AnimationStep = 20;
      this.splitterMain.BorderStyle3D = System.Windows.Forms.Border3DStyle.Bump;
      this.splitterMain.ControlToHide = this.panelTree;
      this.splitterMain.ExpandParentForm = false;
      this.splitterMain.Location = new System.Drawing.Point(236, 0);
      this.splitterMain.Name = "splitter1";
      this.splitterMain.TabIndex = 2;
      this.splitterMain.TabStop = false;
      this.splitterMain.UseAnimations = false;
      this.splitterMain.VisualStyle = ZForge.Controls.CollapsibleSplitter.CollapsibleSplitter.VisualStyles.XP;
      // 
      // panelTree
      // 
      this.panelTree.BorderColor = System.Drawing.SystemColors.ActiveCaption;
      this.panelTree.BorderStyle = ZForge.Controls.HeaderPanelBorderStyles.None;
      this.panelTree.CaptionBeginColor = System.Drawing.SystemColors.InactiveCaption;
      this.panelTree.CaptionEndColor = System.Drawing.SystemColors.ActiveCaption;
      this.panelTree.CaptionFont = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
      this.panelTree.CaptionGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
      this.panelTree.CaptionHeight = 25;
      this.panelTree.CaptionPosition = ZForge.Controls.HeaderPanelCaptionPositions.Top;
      this.panelTree.CaptionText = "摄像头列表";
      this.panelTree.CaptionVisible = true;
      this.panelTree.Controls.Add(this.treeCamera);
      this.panelTree.Dock = System.Windows.Forms.DockStyle.Left;
      this.panelTree.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
      this.panelTree.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
      this.panelTree.GradientEnd = System.Drawing.SystemColors.Window;
      this.panelTree.GradientStart = System.Drawing.SystemColors.Window;
      this.panelTree.Location = new System.Drawing.Point(0, 0);
      this.panelTree.Name = "panelTree";
      this.panelTree.PanelIcon = null;
      this.panelTree.PanelIconVisible = false;
      this.panelTree.Size = new System.Drawing.Size(236, 514);
      this.panelTree.TabIndex = 4;
      this.panelTree.TextAntialias = true;
      // 
      // treeCamera
      // 
      this.treeCamera.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeCamera.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.treeCamera.Location = new System.Drawing.Point(0, 0);
      this.treeCamera.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.treeCamera.Name = "treeCamera";
      this.treeCamera.Size = new System.Drawing.Size(236, 489);
      this.treeCamera.TabIndex = 0;
      this.treeCamera.CameraOnBoard += new ZForge.Motion.Controls.CameraTreeOnBoardEventHandler(this.treeCamera_CameraOnBoard);
      this.treeCamera.CameraBeforeDelete += new ZForge.Motion.Controls.CameraTreeBeforeDeleteEventHandler(this.treeCamera_CameraBeforeDelete);
      // 
      // menuStripMain
      // 
      this.menuStripMain.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemTools,
            this.ToolStripMenuItemHelp});
      this.menuStripMain.Location = new System.Drawing.Point(0, 0);
      this.menuStripMain.Name = "menuStripMain";
      this.menuStripMain.Size = new System.Drawing.Size(821, 24);
      this.menuStripMain.TabIndex = 4;
      // 
      // toolStripMenuItemFile
      // 
      this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSaveOnExit,
            this.toolStripSeparator1,
            this.toolStripMenuItemExit});
      this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
      this.toolStripMenuItemFile.Size = new System.Drawing.Size(62, 20);
      this.toolStripMenuItemFile.Text = "监控 (&F)";
      // 
      // toolStripMenuItemSaveOnExit
      // 
      this.toolStripMenuItemSaveOnExit.Checked = true;
      this.toolStripMenuItemSaveOnExit.CheckOnClick = true;
      this.toolStripMenuItemSaveOnExit.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolStripMenuItemSaveOnExit.Name = "toolStripMenuItemSaveOnExit";
      this.toolStripMenuItemSaveOnExit.Size = new System.Drawing.Size(196, 22);
      this.toolStripMenuItemSaveOnExit.Text = "在退出时自动保存配置";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
      // 
      // toolStripMenuItemExit
      // 
      this.toolStripMenuItemExit.Image = global::Motion.Properties.Resources.cancel;
      this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
      this.toolStripMenuItemExit.Size = new System.Drawing.Size(196, 22);
      this.toolStripMenuItemExit.Text = "退出 (&e)";
      this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
      // 
      // toolStripMenuItemTools
      // 
      this.toolStripMenuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemViewAVI,
            this.toolStripMenuItemViewPIC,
            this.toolStripSeparator5,
            this.toolStripMenuItemImport,
            this.toolStripSeparator3,
            this.toolStripMenuItemPref,
            this.toolStripMenuItemSelectLang});
      this.toolStripMenuItemTools.Name = "toolStripMenuItemTools";
      this.toolStripMenuItemTools.Size = new System.Drawing.Size(44, 20);
      this.toolStripMenuItemTools.Text = "工具";
      // 
      // toolStripMenuItemViewAVI
      // 
      this.toolStripMenuItemViewAVI.Image = global::Motion.Properties.Resources.movie;
      this.toolStripMenuItemViewAVI.Name = "toolStripMenuItemViewAVI";
      this.toolStripMenuItemViewAVI.Size = new System.Drawing.Size(214, 22);
      this.toolStripMenuItemViewAVI.Text = "录像管理 ...";
      this.toolStripMenuItemViewAVI.Click += new System.EventHandler(this.toolStripMenuItemViewAVI_Click);
      // 
      // toolStripMenuItemViewPIC
      // 
      this.toolStripMenuItemViewPIC.Image = global::Motion.Properties.Resources.photo_scenery;
      this.toolStripMenuItemViewPIC.Name = "toolStripMenuItemViewPIC";
      this.toolStripMenuItemViewPIC.Size = new System.Drawing.Size(214, 22);
      this.toolStripMenuItemViewPIC.Text = "截图管理 ...";
      this.toolStripMenuItemViewPIC.Click += new System.EventHandler(this.toolStripMenuItemViewPIC_Click);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(211, 6);
      // 
      // toolStripMenuItemImport
      // 
      this.toolStripMenuItemImport.Name = "toolStripMenuItemImport";
      this.toolStripMenuItemImport.Size = new System.Drawing.Size(214, 22);
      this.toolStripMenuItemImport.Text = "导入旧版本(1.x.x)的配置...";
      this.toolStripMenuItemImport.Click += new System.EventHandler(this.toolStripMenuItemImport_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(211, 6);
      // 
      // toolStripMenuItemPref
      // 
      this.toolStripMenuItemPref.Image = global::Motion.Properties.Resources.heart_preferences;
      this.toolStripMenuItemPref.Name = "toolStripMenuItemPref";
      this.toolStripMenuItemPref.Size = new System.Drawing.Size(214, 22);
      this.toolStripMenuItemPref.Text = "使用偏好 ...";
      this.toolStripMenuItemPref.Click += new System.EventHandler(this.toolStripMenuItemPref_Click);
      // 
      // toolStripMenuItemSelectLang
      // 
      this.toolStripMenuItemSelectLang.Name = "toolStripMenuItemSelectLang";
      this.toolStripMenuItemSelectLang.Size = new System.Drawing.Size(214, 22);
      this.toolStripMenuItemSelectLang.Text = "界面语言";
      // 
      // ToolStripMenuItemHelp
      // 
      this.ToolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCheckUpdate,
            this.toolStripMenuItemRegister,
            this.toolStripSeparator2,
            this.toolStripMenuItemPlugIns,
            this.toolStripMenuItemAbout});
      this.ToolStripMenuItemHelp.Image = global::Motion.Properties.Resources.help;
      this.ToolStripMenuItemHelp.Name = "ToolStripMenuItemHelp";
      this.ToolStripMenuItemHelp.Size = new System.Drawing.Size(78, 20);
      this.ToolStripMenuItemHelp.Text = "帮助 (&F)";
      // 
      // toolStripMenuItemCheckUpdate
      // 
      this.toolStripMenuItemCheckUpdate.Name = "toolStripMenuItemCheckUpdate";
      this.toolStripMenuItemCheckUpdate.Size = new System.Drawing.Size(137, 22);
      this.toolStripMenuItemCheckUpdate.Text = "检查更新...";
      this.toolStripMenuItemCheckUpdate.Click += new System.EventHandler(this.toolStripMenuItemCheckUpdate_Click);
      // 
      // toolStripMenuItemRegister
      // 
      this.toolStripMenuItemRegister.Name = "toolStripMenuItemRegister";
      this.toolStripMenuItemRegister.Size = new System.Drawing.Size(137, 22);
      this.toolStripMenuItemRegister.Text = "注册 ...";
      this.toolStripMenuItemRegister.Enabled = false;
      this.toolStripMenuItemRegister.Visible = false;
      this.toolStripMenuItemRegister.Click += new System.EventHandler(this.toolStripMenuItemRegister_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(134, 6);
      // 
      // toolStripMenuItemPlugIns
      // 
      this.toolStripMenuItemPlugIns.Image = global::Motion.Properties.Resources.component_view_16;
      this.toolStripMenuItemPlugIns.Name = "toolStripMenuItemPlugIns";
      this.toolStripMenuItemPlugIns.Size = new System.Drawing.Size(137, 22);
      this.toolStripMenuItemPlugIns.Text = "浏览插件 ...";
      this.toolStripMenuItemPlugIns.Click += new System.EventHandler(this.toolStripMenuItemPlugIns_Click);
      // 
      // toolStripMenuItemAbout
      // 
      this.toolStripMenuItemAbout.Image = global::Motion.Properties.Resources.house_16;
      this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
      this.toolStripMenuItemAbout.Size = new System.Drawing.Size(137, 22);
      this.toolStripMenuItemAbout.Text = "关于 ... (&A)";
      this.toolStripMenuItemAbout.Click += new System.EventHandler(this.ToolStripMenuItemAbout_Click);
      // 
      // notifyIconTray
      // 
      this.notifyIconTray.ContextMenuStrip = this.contextMenuStripTray;
      this.notifyIconTray.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconTray.Icon")));
      this.notifyIconTray.Text = "Motion Detector";
      this.notifyIconTray.Visible = true;
      this.notifyIconTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconTray_MouseDoubleClick);
      // 
      // contextMenuStripTray
      // 
      this.contextMenuStripTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemTrayRestore,
            this.toolStripMenuItemTrayViewAVI,
            this.toolStripMenuItemTrayViewPIC,
            this.toolStripSeparatorTray1,
            this.toolStripMenuItemTrayQuit});
      this.contextMenuStripTray.Name = "contextMenuStripTray";
      this.contextMenuStripTray.Size = new System.Drawing.Size(138, 98);
      // 
      // toolStripMenuItemTrayRestore
      // 
      this.toolStripMenuItemTrayRestore.Image = global::Motion.Properties.Resources.window_new;
      this.toolStripMenuItemTrayRestore.Name = "toolStripMenuItemTrayRestore";
      this.toolStripMenuItemTrayRestore.Size = new System.Drawing.Size(137, 22);
      this.toolStripMenuItemTrayRestore.Text = "显示窗口";
      this.toolStripMenuItemTrayRestore.Click += new System.EventHandler(this.tripMenuItemTrayRestore_Click);
      // 
      // toolStripMenuItemTrayViewAVI
      // 
      this.toolStripMenuItemTrayViewAVI.Image = global::Motion.Properties.Resources.movie;
      this.toolStripMenuItemTrayViewAVI.Name = "toolStripMenuItemTrayViewAVI";
      this.toolStripMenuItemTrayViewAVI.Size = new System.Drawing.Size(137, 22);
      this.toolStripMenuItemTrayViewAVI.Text = "录像管理 ...";
      this.toolStripMenuItemTrayViewAVI.Click += new System.EventHandler(this.toolStripMenuItemTrayViewAVI_Click);
      // 
      // toolStripMenuItemTrayViewPIC
      // 
      this.toolStripMenuItemTrayViewPIC.Image = global::Motion.Properties.Resources.photo_scenery;
      this.toolStripMenuItemTrayViewPIC.Name = "toolStripMenuItemTrayViewPIC";
      this.toolStripMenuItemTrayViewPIC.Size = new System.Drawing.Size(137, 22);
      this.toolStripMenuItemTrayViewPIC.Text = "截图管理 ...";
      this.toolStripMenuItemTrayViewPIC.Click += new System.EventHandler(this.toolStripMenuItemTrayViewPIC_Click);
      // 
      // toolStripSeparatorTray1
      // 
      this.toolStripSeparatorTray1.Name = "toolStripSeparatorTray1";
      this.toolStripSeparatorTray1.Size = new System.Drawing.Size(134, 6);
      // 
      // toolStripMenuItemTrayQuit
      // 
      this.toolStripMenuItemTrayQuit.Image = global::Motion.Properties.Resources.delete2;
      this.toolStripMenuItemTrayQuit.Name = "toolStripMenuItemTrayQuit";
      this.toolStripMenuItemTrayQuit.Size = new System.Drawing.Size(137, 22);
      this.toolStripMenuItemTrayQuit.Text = "退出";
      this.toolStripMenuItemTrayQuit.Click += new System.EventHandler(this.toolStripMenuItemTrayQuit_Click);
      // 
      // toolStripBottom
      // 
      this.toolStripBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.toolStripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMainButtonViewAVI,
            this.toolStripMainButtonViewPIC,
            this.toolStripMainButtonPref,
            this.toolStripMainButtonPlugIns,
            this.toolStripMainButtonAbout,
            this.toolStripSeparator4,
            this.toolStripMainLabelStorage});
      this.toolStripBottom.Location = new System.Drawing.Point(0, 571);
      this.toolStripBottom.Name = "toolStripBottom";
      this.toolStripBottom.Size = new System.Drawing.Size(821, 25);
      this.toolStripBottom.TabIndex = 4;
      this.toolStripBottom.Text = "toolStrip1";
      // 
      // toolStripMainButtonViewAVI
      // 
      this.toolStripMainButtonViewAVI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripMainButtonViewAVI.Image = global::Motion.Properties.Resources.movie;
      this.toolStripMainButtonViewAVI.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripMainButtonViewAVI.Name = "toolStripMainButtonViewAVI";
      this.toolStripMainButtonViewAVI.Size = new System.Drawing.Size(23, 22);
      this.toolStripMainButtonViewAVI.Text = "录像管理 ...";
      this.toolStripMainButtonViewAVI.Click += new System.EventHandler(this.toolStripMainButtonViewAVI_Click);
      // 
      // toolStripMainButtonViewPIC
      // 
      this.toolStripMainButtonViewPIC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripMainButtonViewPIC.Image = global::Motion.Properties.Resources.photo_scenery;
      this.toolStripMainButtonViewPIC.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripMainButtonViewPIC.Name = "toolStripMainButtonViewPIC";
      this.toolStripMainButtonViewPIC.Size = new System.Drawing.Size(23, 22);
      this.toolStripMainButtonViewPIC.Text = "截图管理 ...";
      this.toolStripMainButtonViewPIC.Click += new System.EventHandler(this.toolStripMainButtonViewPIC_Click);
      // 
      // toolStripMainButtonPref
      // 
      this.toolStripMainButtonPref.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripMainButtonPref.Image = global::Motion.Properties.Resources.heart_preferences;
      this.toolStripMainButtonPref.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripMainButtonPref.Name = "toolStripMainButtonPref";
      this.toolStripMainButtonPref.Size = new System.Drawing.Size(23, 22);
      this.toolStripMainButtonPref.Text = "使用偏好 ...";
      this.toolStripMainButtonPref.Click += new System.EventHandler(this.toolStripMainButtonPref_Click);
      // 
      // toolStripMainButtonPlugIns
      // 
      this.toolStripMainButtonPlugIns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripMainButtonPlugIns.Image = global::Motion.Properties.Resources.component_view_16;
      this.toolStripMainButtonPlugIns.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripMainButtonPlugIns.Name = "toolStripMainButtonPlugIns";
      this.toolStripMainButtonPlugIns.Size = new System.Drawing.Size(23, 22);
      this.toolStripMainButtonPlugIns.Text = "浏览插件 ...";
      this.toolStripMainButtonPlugIns.Click += new System.EventHandler(this.toolStripMainButtonPlugIns_Click);
      // 
      // toolStripMainButtonAbout
      // 
      this.toolStripMainButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripMainButtonAbout.Image = global::Motion.Properties.Resources.house_16;
      this.toolStripMainButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripMainButtonAbout.Name = "toolStripMainButtonAbout";
      this.toolStripMainButtonAbout.Size = new System.Drawing.Size(23, 22);
      this.toolStripMainButtonAbout.Text = "关于 ...";
      this.toolStripMainButtonAbout.Click += new System.EventHandler(this.toolStripMainButtonAbout_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripMainLabelStorage
      // 
      this.toolStripMainLabelStorage.Name = "toolStripMainLabelStorage";
      this.toolStripMainLabelStorage.Size = new System.Drawing.Size(0, 22);
      // 
      // toolStripMain
      // 
      this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExit});
      this.toolStripMain.Location = new System.Drawing.Point(0, 24);
      this.toolStripMain.Name = "toolStripMain";
      this.toolStripMain.Size = new System.Drawing.Size(821, 29);
      this.toolStripMain.TabIndex = 5;
      this.toolStripMain.Text = "toolStrip1";
      // 
      // toolStripButtonExit
      // 
      this.toolStripButtonExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonExit.Image = global::Motion.Properties.Resources.cancel;
      this.toolStripButtonExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonExit.Name = "toolStripButtonExit";
      this.toolStripButtonExit.Size = new System.Drawing.Size(26, 26);
      this.toolStripButtonExit.Text = "退出";
      this.toolStripButtonExit.Click += new System.EventHandler(this.toolStripButtonExit_Click);
      // 
      // MainForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
      this.ClientSize = new System.Drawing.Size(821, 596);
      this.Controls.Add(this.panel);
      this.Controls.Add(this.toolStripBottom);
      this.Controls.Add(this.toolStripMain);
      this.Controls.Add(this.menuStripMain);
      this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStripMain;
      this.Name = "MainForm";
      this.Text = "Motion Detector";
      this.VisibleChanged += new System.EventHandler(this.MainForm_VisibleChanged);
      this.panel.ResumeLayout(false);
      this.panelBoard.ResumeLayout(false);
      this.tabControlSub.ResumeLayout(false);
      this.tabPageLog.ResumeLayout(false);
      this.panelLogMaster.ResumeLayout(false);
      this.panelTree.ResumeLayout(false);
      this.menuStripMain.ResumeLayout(false);
      this.menuStripMain.PerformLayout();
      this.contextMenuStripTray.ResumeLayout(false);
      this.toolStripBottom.ResumeLayout(false);
      this.toolStripBottom.PerformLayout();
      this.toolStripMain.ResumeLayout(false);
      this.toolStripMain.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion

		private ZForge.Motion.Controls.CameraTree treeCamera;
		private Panel panel;
		private ZForge.Controls.CollapsibleSplitter.CollapsibleSplitter splitterMain;
		private ZForge.Controls.HeaderPanel panelTree;
		private MenuStrip menuStripMain;
		private ToolStripMenuItem toolStripMenuItemFile;
		private ToolStripMenuItem toolStripMenuItemExit;
		private ToolStripMenuItem ToolStripMenuItemHelp;
		private ToolStripMenuItem toolStripMenuItemAbout;
		private ZForge.Controls.HeaderPanel panelBoard;
		private ZForge.Motion.Controls.CameraBoard boardCamera;
		private ZForge.Controls.CollapsibleSplitter.CollapsibleSplitter splitterSub;
		private ToolStripMenuItem toolStripMenuItemSaveOnExit;
		private ToolStripSeparator toolStripSeparator1;
		private TabControl tabControlSub;
		private TabPage tabPageLog;
		private Panel panelLogMaster;
		private ZForge.Motion.Controls.ExtendedLogViewer logViewer;
		private ImageList imageListMain;
		private ToolStripMenuItem toolStripMenuItemRegister;
		private ToolStripSeparator toolStripSeparator2;
		private NotifyIcon notifyIconTray;
		private ToolStripMenuItem toolStripMenuItemCheckUpdate;
		private ToolStripMenuItem toolStripMenuItemTools;
		private ToolStripMenuItem toolStripMenuItemPref;
		private ToolStripMenuItem toolStripMenuItemPlugIns;
		private ContextMenuStrip contextMenuStripTray;
		private ToolStripMenuItem toolStripMenuItemTrayRestore;
		private ToolStripSeparator toolStripSeparatorTray1;
		private ToolStripMenuItem toolStripMenuItemTrayQuit;
		private ToolStripMenuItem toolStripMenuItemViewAVI;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripMenuItem toolStripMenuItemViewPIC;
		private ToolStripMenuItem toolStripMenuItemTrayViewAVI;
		private ToolStripMenuItem toolStripMenuItemTrayViewPIC;
		private ToolStrip toolStripBottom;
		private ToolStripButton toolStripMainButtonViewAVI;
		private ToolStripButton toolStripMainButtonViewPIC;
		private ToolStripButton toolStripMainButtonPref;
		private ToolStripButton toolStripMainButtonPlugIns;
		private ToolStripButton toolStripMainButtonAbout;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripLabel toolStripMainLabelStorage;
		private ToolStripMenuItem toolStripMenuItemSelectLang;
		private ToolStripMenuItem toolStripMenuItemImport;
		private ToolStripSeparator toolStripSeparator5;
		private ToolStrip toolStripMain;
    private ToolStripButton toolStripButtonExit;
	}
}
