namespace ZForge.Controls.Logs
{
	partial class LogViewer
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
			ZForge.Controls.XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder1 = new ZForge.Controls.XPTable.Models.DataSourceColumnBinder();
			this.toolStripMaster = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonClear = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonError = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonWarning = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonInfo = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelError = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelWarning = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableList = new ZForge.Controls.XPTable.Models.Table();
			this.toolStripMaster.SuspendLayout();
			this.statusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tableList)).BeginInit();
			this.SuspendLayout();
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
            this.toolStripButtonCopy});
			this.toolStripMaster.Location = new System.Drawing.Point(0, 0);
			this.toolStripMaster.Name = "toolStripMaster";
			this.toolStripMaster.Size = new System.Drawing.Size(381, 31);
			this.toolStripMaster.TabIndex = 3;
			this.toolStripMaster.Text = "toolStrip1";
			// 
			// toolStripButtonClear
			// 
			this.toolStripButtonClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonClear.Image = global::ZForge.Controls.Logs.Properties.Resources.scroll_delete_24;
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
			this.toolStripButtonError.Image = global::ZForge.Controls.Logs.Properties.Resources.scroll_error_24;
			this.toolStripButtonError.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonError.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonError.Name = "toolStripButtonError";
			this.toolStripButtonError.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonError.Text = "显示报警日志";
			this.toolStripButtonError.CheckedChanged += new System.EventHandler(this.toolStripButtonError_CheckedChanged);
			// 
			// toolStripButtonWarning
			// 
			this.toolStripButtonWarning.Checked = true;
			this.toolStripButtonWarning.CheckOnClick = true;
			this.toolStripButtonWarning.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripButtonWarning.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonWarning.Image = global::ZForge.Controls.Logs.Properties.Resources.scroll_warning_24;
			this.toolStripButtonWarning.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonWarning.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonWarning.Name = "toolStripButtonWarning";
			this.toolStripButtonWarning.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonWarning.Text = "显示警告日志";
			this.toolStripButtonWarning.CheckedChanged += new System.EventHandler(this.toolStripButtonWarning_CheckedChanged);
			// 
			// toolStripButtonInfo
			// 
			this.toolStripButtonInfo.Checked = true;
			this.toolStripButtonInfo.CheckOnClick = true;
			this.toolStripButtonInfo.CheckState = System.Windows.Forms.CheckState.Checked;
			this.toolStripButtonInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonInfo.Image = global::ZForge.Controls.Logs.Properties.Resources.scroll_information_24;
			this.toolStripButtonInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonInfo.Name = "toolStripButtonInfo";
			this.toolStripButtonInfo.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonInfo.Text = "显示普通信息";
			this.toolStripButtonInfo.CheckedChanged += new System.EventHandler(this.toolStripButtonInfo_CheckedChanged);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			// 
			// toolStripButtonCopy
			// 
			this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonCopy.Image = global::ZForge.Controls.Logs.Properties.Resources.copy_24;
			this.toolStripButtonCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonCopy.Name = "toolStripButtonCopy";
			this.toolStripButtonCopy.Size = new System.Drawing.Size(28, 28);
			this.toolStripButtonCopy.Text = "拷贝当前日志到剪贴板";
			this.toolStripButtonCopy.Click += new System.EventHandler(this.toolStripButtonCopy_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelError,
            this.toolStripStatusLabelWarning,
            this.toolStripStatusLabelInfo});
			this.statusStrip.Location = new System.Drawing.Point(0, 202);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.statusStrip.Size = new System.Drawing.Size(381, 22);
			this.statusStrip.TabIndex = 4;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabelError
			// 
			this.toolStripStatusLabelError.Name = "toolStripStatusLabelError";
			this.toolStripStatusLabelError.Size = new System.Drawing.Size(70, 17);
			this.toolStripStatusLabelError.Text = "错误日志: 0";
			// 
			// toolStripStatusLabelWarning
			// 
			this.toolStripStatusLabelWarning.Name = "toolStripStatusLabelWarning";
			this.toolStripStatusLabelWarning.Size = new System.Drawing.Size(70, 17);
			this.toolStripStatusLabelWarning.Text = "警告日志: 0";
			// 
			// toolStripStatusLabelInfo
			// 
			this.toolStripStatusLabelInfo.Name = "toolStripStatusLabelInfo";
			this.toolStripStatusLabelInfo.Size = new System.Drawing.Size(70, 17);
			this.toolStripStatusLabelInfo.Text = "普通日志: 0";
			// 
			// tableList
			// 
			this.tableList.BorderColor = System.Drawing.Color.Black;
			this.tableList.DataMember = null;
			this.tableList.DataSourceColumnBinder = dataSourceColumnBinder1;
			this.tableList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableList.Location = new System.Drawing.Point(0, 31);
			this.tableList.Name = "tableList";
			this.tableList.Size = new System.Drawing.Size(381, 171);
			this.tableList.TabIndex = 5;
			this.tableList.TopIndex = -1;
			this.tableList.TopItem = null;
			this.tableList.UnfocusedBorderColor = System.Drawing.Color.Black;
			this.tableList.FontChanged += new System.EventHandler(this.tableList_FontChanged);
			// 
			// LogViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableList);
			this.Controls.Add(this.toolStripMaster);
			this.Controls.Add(this.statusStrip);
			this.Name = "LogViewer";
			this.Size = new System.Drawing.Size(381, 224);
			this.toolStripMaster.ResumeLayout(false);
			this.toolStripMaster.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tableList)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStripMaster;
		private System.Windows.Forms.ToolStripButton toolStripButtonClear;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButtonError;
		private System.Windows.Forms.ToolStripButton toolStripButtonWarning;
		private System.Windows.Forms.ToolStripButton toolStripButtonInfo;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelError;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelWarning;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelInfo;
		private ZForge.Controls.XPTable.Models.Table tableList;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
	}
}
