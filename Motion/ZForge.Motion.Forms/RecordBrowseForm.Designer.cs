namespace ZForge.Motion.Forms
{
	partial class RecordBrowseForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordBrowseForm));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItemMain = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
			this.panelBoard = new ZForge.Controls.HeaderPanel();
			this.WhiteBoard = new ZForge.Motion.Controls.CameraBoard();
			this.panelList = new ZForge.Controls.HeaderPanel();
			this.recordList = new ZForge.Motion.Forms.RecordList();
			this.splitter1 = new ZForge.Controls.CollapsibleSplitter.CollapsibleSplitter();
			this.panelMaster = new System.Windows.Forms.Panel();
			this.menuStrip.SuspendLayout();
			this.panelBoard.SuspendLayout();
			this.panelList.SuspendLayout();
			this.panelMaster.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMain});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(624, 25);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// toolStripMenuItemMain
			// 
			this.toolStripMenuItemMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemClose});
			this.toolStripMenuItemMain.Name = "toolStripMenuItemMain";
			this.toolStripMenuItemMain.Size = new System.Drawing.Size(68, 21);
			this.toolStripMenuItemMain.Text = "录像管理";
			// 
			// toolStripMenuItemClose
			// 
			this.toolStripMenuItemClose.Image = global::ZForge.Motion.Forms.Properties.Resources.delete2_16;
			this.toolStripMenuItemClose.Name = "toolStripMenuItemClose";
			this.toolStripMenuItemClose.Size = new System.Drawing.Size(100, 22);
			this.toolStripMenuItemClose.Text = "关闭";
			this.toolStripMenuItemClose.Click += new System.EventHandler(this.toolStripMenuItemClose_Click);
			// 
			// panelBoard
			// 
			this.panelBoard.BorderColor = System.Drawing.SystemColors.ActiveCaption;
			this.panelBoard.BorderStyle = ZForge.Controls.HeaderPanelBorderStyles.Single;
			this.panelBoard.CaptionBeginColor = System.Drawing.SystemColors.InactiveCaption;
			this.panelBoard.CaptionEndColor = System.Drawing.SystemColors.ActiveCaption;
			this.panelBoard.CaptionGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.panelBoard.CaptionHeight = 25;
			this.panelBoard.CaptionPosition = ZForge.Controls.HeaderPanelCaptionPositions.Top;
			this.panelBoard.CaptionText = "录像浏览";
			this.panelBoard.CaptionVisible = true;
			this.panelBoard.Controls.Add(this.WhiteBoard);
			this.panelBoard.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelBoard.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
			this.panelBoard.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.panelBoard.GradientEnd = System.Drawing.SystemColors.Window;
			this.panelBoard.GradientStart = System.Drawing.SystemColors.Window;
			this.panelBoard.Location = new System.Drawing.Point(0, 0);
			this.panelBoard.Name = "panelBoard";
			this.panelBoard.PanelIcon = null;
			this.panelBoard.PanelIconVisible = false;
			this.panelBoard.Size = new System.Drawing.Size(624, 207);
			this.panelBoard.TabIndex = 1;
			this.panelBoard.TextAntialias = true;
			// 
			// WhiteBoard
			// 
			this.WhiteBoard.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WhiteBoard.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.WhiteBoard.Location = new System.Drawing.Point(0, 0);
			this.WhiteBoard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.WhiteBoard.Name = "WhiteBoard";
			this.WhiteBoard.Size = new System.Drawing.Size(622, 180);
			this.WhiteBoard.Style = ZForge.Motion.Controls.CameraBoardStyle.AVI;
			this.WhiteBoard.TabIndex = 0;
			this.WhiteBoard.ViewRatio = 1F;
			// 
			// panelList
			// 
			this.panelList.BorderColor = System.Drawing.SystemColors.ActiveCaption;
			this.panelList.BorderStyle = ZForge.Controls.HeaderPanelBorderStyles.None;
			this.panelList.CaptionBeginColor = System.Drawing.SystemColors.InactiveCaption;
			this.panelList.CaptionEndColor = System.Drawing.SystemColors.ActiveCaption;
			this.panelList.CaptionGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.panelList.CaptionHeight = 25;
			this.panelList.CaptionPosition = ZForge.Controls.HeaderPanelCaptionPositions.Top;
			this.panelList.CaptionText = "录像列表";
			this.panelList.CaptionVisible = true;
			this.panelList.Controls.Add(this.recordList);
			this.panelList.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelList.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
			this.panelList.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.panelList.GradientEnd = System.Drawing.SystemColors.Window;
			this.panelList.GradientStart = System.Drawing.SystemColors.Window;
			this.panelList.Location = new System.Drawing.Point(0, 232);
			this.panelList.Name = "panelList";
			this.panelList.PanelIcon = null;
			this.panelList.PanelIconVisible = false;
			this.panelList.Size = new System.Drawing.Size(624, 240);
			this.panelList.TabIndex = 2;
			this.panelList.TextAntialias = true;
			// 
			// recordList
			// 
			this.recordList.BoardControl = null;
			this.recordList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.recordList.FileSystemWatcherEnabled = false;
			this.recordList.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.recordList.Location = new System.Drawing.Point(0, 0);
			this.recordList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.recordList.Name = "recordList";
			this.recordList.Size = new System.Drawing.Size(624, 215);
			this.recordList.Style = ZForge.Motion.Controls.CameraBoardStyle.AVI;
			this.recordList.TabIndex = 0;
			// 
			// splitter1
			// 
			this.splitter1.AnimationDelay = 20;
			this.splitter1.AnimationStep = 20;
			this.splitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Bump;
			this.splitter1.ControlToHide = this.panelList;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.ExpandParentForm = false;
			this.splitter1.Location = new System.Drawing.Point(0, 224);
			this.splitter1.Name = "splitter1";
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			this.splitter1.UseAnimations = false;
			this.splitter1.VisualStyle = ZForge.Controls.CollapsibleSplitter.CollapsibleSplitter.VisualStyles.XP;
			// 
			// panelMaster
			// 
			this.panelMaster.Controls.Add(this.panelBoard);
			this.panelMaster.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMaster.Location = new System.Drawing.Point(0, 25);
			this.panelMaster.Name = "panelMaster";
			this.panelMaster.Size = new System.Drawing.Size(624, 207);
			this.panelMaster.TabIndex = 1;
			// 
			// BrowseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 472);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panelMaster);
			this.Controls.Add(this.panelList);
			this.Controls.Add(this.menuStrip);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "BrowseForm";
			this.Text = "Motion Detector 录像浏览";
			this.Load += new System.EventHandler(this.MovieForm_Load);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.panelBoard.ResumeLayout(false);
			this.panelList.ResumeLayout(false);
			this.panelMaster.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMain;
		private ZForge.Controls.HeaderPanel panelBoard;
		private ZForge.Controls.HeaderPanel panelList;
		private ZForge.Controls.CollapsibleSplitter.CollapsibleSplitter splitter1;
		private ZForge.Motion.Forms.RecordList recordList;
		private ZForge.Motion.Controls.CameraBoard WhiteBoard;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClose;
		private System.Windows.Forms.Panel panelMaster;
	}
}