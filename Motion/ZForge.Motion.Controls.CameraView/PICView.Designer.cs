namespace ZForge.Motion.Controls
{
	partial class PICView
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
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.panelMaster = new ZForge.Controls.HeaderPanel();
			this.panelMain = new System.Windows.Forms.Panel();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.panelMaster.SuspendLayout();
			this.panelMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "jpg";
			this.saveFileDialog.Filter = "图像文件 (*.jpg)|*.jpg";
			this.saveFileDialog.Title = "存储图像文件";
			// 
			// panelMaster
			// 
			this.panelMaster.BorderColor = System.Drawing.SystemColors.ActiveCaption;
			this.panelMaster.BorderStyle = ZForge.Controls.HeaderPanelBorderStyles.Single;
			this.panelMaster.CaptionBeginColor = System.Drawing.SystemColors.InactiveCaption;
			this.panelMaster.CaptionEndColor = System.Drawing.SystemColors.ActiveCaption;
			this.panelMaster.CaptionGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			this.panelMaster.CaptionHeight = 25;
			this.panelMaster.CaptionPosition = ZForge.Controls.HeaderPanelCaptionPositions.Top;
			this.panelMaster.CaptionText = "图像";
			this.panelMaster.CaptionVisible = true;
			this.panelMaster.Controls.Add(this.panelMain);
			this.panelMaster.Controls.Add(this.toolStrip);
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
			this.panelMaster.Size = new System.Drawing.Size(175, 196);
			this.panelMaster.TabIndex = 1;
			this.panelMaster.TextAntialias = true;
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.pictureBox);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 23);
			this.panelMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(173, 146);
			this.panelMain.TabIndex = 3;
			// 
			// pictureBox
			// 
			this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(173, 146);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonClose,
            this.toolStripSeparator1,
            this.toolStripButtonZoomIn,
            this.toolStripButtonZoomOut,
            this.toolStripSeparator2,
            this.toolStripButtonSaveAs,
            this.toolStripButtonDelete});
			this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(173, 23);
			this.toolStrip.TabIndex = 2;
			this.toolStrip.Text = "toolStrip1";
			// 
			// toolStripButtonClose
			// 
			this.toolStripButtonClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonClose.Image = global::ZForge.Motion.Controls.Properties.Resources.window_delete_16;
			this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonClose.Name = "toolStripButtonClose";
			this.toolStripButtonClose.Size = new System.Drawing.Size(23, 20);
			this.toolStripButtonClose.Text = "关闭";
			this.toolStripButtonClose.ToolTipText = "关闭当前窗口";
			this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonClose_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
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
			this.toolStripButtonDelete.ToolTipText = "删除截图";
			this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
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
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
			// 
			// PICView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelMaster);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "PICView";
			this.Size = new System.Drawing.Size(175, 196);
			this.panelMaster.ResumeLayout(false);
			this.panelMaster.PerformLayout();
			this.panelMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private ZForge.Controls.HeaderPanel panelMaster;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton toolStripButtonSaveAs;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripButton toolStripButtonClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
		private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
		private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}
