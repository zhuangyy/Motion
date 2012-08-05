namespace ZForge.Motion.Controls
{
	partial class ExtendedLogViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtendedLogViewer));
			this.panelMaster = new System.Windows.Forms.Panel();
			this.logGrid = new SourceGrid.Grid();
			this.toolStripMaster = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonClear = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonError = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonWarning = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonInfo = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripComboBoxView = new System.Windows.Forms.ToolStripComboBox();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelError = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelWarning = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.imageListLog = new System.Windows.Forms.ImageList(this.components);
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.panelMaster.SuspendLayout();
			this.toolStripMaster.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMaster
			// 
			this.panelMaster.Controls.Add(this.logGrid);
			this.panelMaster.Controls.Add(this.toolStripMaster);
			this.panelMaster.Controls.Add(this.statusStrip);
			this.panelMaster.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMaster.Location = new System.Drawing.Point(0, 0);
			this.panelMaster.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelMaster.Name = "panelMaster";
			this.panelMaster.Size = new System.Drawing.Size(493, 433);
			this.panelMaster.TabIndex = 0;
			// 
			// logGrid
			// 
			this.logGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logGrid.Location = new System.Drawing.Point(0, 31);
			this.logGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.logGrid.Name = "logGrid";
			this.logGrid.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
			this.logGrid.SelectionMode = SourceGrid.GridSelectionMode.Cell;
			this.logGrid.Size = new System.Drawing.Size(493, 380);
			this.logGrid.TabIndex = 3;
			this.logGrid.TabStop = true;
			this.logGrid.ToolTipText = "";
			// 
			// toolStripMaster
			// 
			this.toolStripMaster.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonClear,
            this.toolStripSeparator1,
            this.toolStripButtonError,
            this.toolStripButtonWarning,
            this.toolStripButtonInfo,
            this.toolStripSeparator2,
            this.toolStripComboBoxView});
			this.toolStripMaster.Location = new System.Drawing.Point(0, 0);
			this.toolStripMaster.Name = "toolStripMaster";
			this.toolStripMaster.Size = new System.Drawing.Size(493, 31);
			this.toolStripMaster.TabIndex = 0;
			this.toolStripMaster.Text = "toolStrip1";
			// 
			// toolStripButtonClear
			// 
			this.toolStripButtonClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonClear.Image = global::ZForge.Motion.Controls.Properties.Resources.scroll_delete_24;
			this.toolStripButtonClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonClear.Name = "toolStripButtonClear";
			this.toolStripButtonClear.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonClear.Text = "清空日志";
			this.toolStripButtonClear.Click += new System.EventHandler(this.toolStripButtonClear_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// toolStripButtonError
			// 
			this.toolStripButtonError.Checked = true;
			this.toolStripButtonError.CheckOnClick = true;
			this.toolStripButtonError.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripButtonError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonError.Image = global::ZForge.Motion.Controls.Properties.Resources.scroll_error_24;
			this.toolStripButtonError.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonError.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonError.Name = "toolStripButtonError";
			this.toolStripButtonError.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonError.Text = "显示报警日志";
			this.toolStripButtonError.CheckStateChanged += new System.EventHandler(this.toolStripButtonError_CheckStateChanged);
			// 
			// toolStripButtonWarning
			// 
			this.toolStripButtonWarning.Checked = true;
			this.toolStripButtonWarning.CheckOnClick = true;
			this.toolStripButtonWarning.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripButtonWarning.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonWarning.Image = global::ZForge.Motion.Controls.Properties.Resources.scroll_warning_24;
			this.toolStripButtonWarning.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonWarning.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonWarning.Name = "toolStripButtonWarning";
			this.toolStripButtonWarning.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonWarning.Text = "显示警告日志";
			this.toolStripButtonWarning.CheckStateChanged += new System.EventHandler(this.toolStripButtonWarning_CheckStateChanged);
			// 
			// toolStripButtonInfo
			// 
			this.toolStripButtonInfo.Checked = true;
			this.toolStripButtonInfo.CheckOnClick = true;
			this.toolStripButtonInfo.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripButtonInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonInfo.Image = global::ZForge.Motion.Controls.Properties.Resources.scroll_information_24;
			this.toolStripButtonInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonInfo.Name = "toolStripButtonInfo";
			this.toolStripButtonInfo.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonInfo.Text = "显示普通信息";
			this.toolStripButtonInfo.CheckStateChanged += new System.EventHandler(this.toolStripButtonInfo_CheckStateChanged);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			// 
			// toolStripComboBoxView
			// 
			this.toolStripComboBoxView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolStripComboBoxView.Name = "toolStripComboBoxView";
			this.toolStripComboBoxView.Size = new System.Drawing.Size(200, 31);
			this.toolStripComboBoxView.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxView_SelectedIndexChanged);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelError,
            this.toolStripStatusLabelWarning,
            this.toolStripStatusLabelInfo});
			this.statusStrip.Location = new System.Drawing.Point(0, 411);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.statusStrip.Size = new System.Drawing.Size(493, 22);
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabelError
			// 
			this.toolStripStatusLabelError.Name = "toolStripStatusLabelError";
			this.toolStripStatusLabelError.Size = new System.Drawing.Size(94, 17);
			this.toolStripStatusLabelError.Text = "错误日志数量: 0";
			// 
			// toolStripStatusLabelWarning
			// 
			this.toolStripStatusLabelWarning.Name = "toolStripStatusLabelWarning";
			this.toolStripStatusLabelWarning.Size = new System.Drawing.Size(94, 17);
			this.toolStripStatusLabelWarning.Text = "警告日志数量: 0";
			// 
			// toolStripStatusLabelInfo
			// 
			this.toolStripStatusLabelInfo.Name = "toolStripStatusLabelInfo";
			this.toolStripStatusLabelInfo.Size = new System.Drawing.Size(94, 17);
			this.toolStripStatusLabelInfo.Text = "普通日志数量: 0";
			// 
			// imageListLog
			// 
			this.imageListLog.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLog.ImageStream")));
			this.imageListLog.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListLog.Images.SetKeyName(0, "error.png");
			this.imageListLog.Images.SetKeyName(1, "warning.png");
			this.imageListLog.Images.SetKeyName(2, "information.png");
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Interval = 250;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// LogViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelMaster);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "LogViewer";
			this.Size = new System.Drawing.Size(493, 433);
			this.panelMaster.ResumeLayout(false);
			this.panelMaster.PerformLayout();
			this.toolStripMaster.ResumeLayout(false);
			this.toolStripMaster.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelMaster;
		private System.Windows.Forms.ToolStrip toolStripMaster;
		private System.Windows.Forms.ToolStripButton toolStripButtonClear;
		private System.Windows.Forms.StatusStrip statusStrip;
		private SourceGrid.Grid logGrid;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButtonError;
		private System.Windows.Forms.ToolStripButton toolStripButtonWarning;
		private System.Windows.Forms.ToolStripButton toolStripButtonInfo;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBoxView;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelError;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelWarning;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelInfo;
		private System.Windows.Forms.ImageList imageListLog;
		private System.Windows.Forms.Timer timer;
	}
}
