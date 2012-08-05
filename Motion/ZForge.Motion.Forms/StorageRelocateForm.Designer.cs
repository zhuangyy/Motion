namespace ZForge.Motion.Forms
{
	partial class StorageRelocateForm
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
			this.labelTopic = new System.Windows.Forms.Label();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.buttonClose = new System.Windows.Forms.Button();
			this.panelTop = new System.Windows.Forms.Panel();
			this.panelBottom = new System.Windows.Forms.Panel();
			this.panelLog = new System.Windows.Forms.Panel();
			this.logViewer = new ZForge.Controls.Logs.LogViewer();
			this.panelTop.SuspendLayout();
			this.panelBottom.SuspendLayout();
			this.panelLog.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelTopic
			// 
			this.labelTopic.AutoSize = true;
			this.labelTopic.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTopic.Location = new System.Drawing.Point(12, 18);
			this.labelTopic.Name = "labelTopic";
			this.labelTopic.Size = new System.Drawing.Size(300, 17);
			this.labelTopic.TabIndex = 0;
			this.labelTopic.Text = "Motion Detector正在更新您的数据存储目录, 请稍候...";
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(15, 40);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(388, 23);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar.TabIndex = 1;
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(168, 8);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 44);
			this.buttonClose.TabIndex = 2;
			this.buttonClose.Text = "关闭";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// panelTop
			// 
			this.panelTop.Controls.Add(this.labelTopic);
			this.panelTop.Controls.Add(this.progressBar);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(418, 88);
			this.panelTop.TabIndex = 3;
			// 
			// panelBottom
			// 
			this.panelBottom.Controls.Add(this.buttonClose);
			this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBottom.Location = new System.Drawing.Point(0, 280);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(418, 64);
			this.panelBottom.TabIndex = 4;
			// 
			// panelLog
			// 
			this.panelLog.Controls.Add(this.logViewer);
			this.panelLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelLog.Location = new System.Drawing.Point(0, 88);
			this.panelLog.Name = "panelLog";
			this.panelLog.Size = new System.Drawing.Size(418, 192);
			this.panelLog.TabIndex = 5;
			// 
			// logViewer
			// 
			this.logViewer.AutoScrollToLast = true;
			this.logViewer.CountError = 0;
			this.logViewer.CountInfo = 0;
			this.logViewer.CountWarn = 0;
			this.logViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logViewer.Location = new System.Drawing.Point(0, 0);
			this.logViewer.LogFilter = ZForge.Controls.Logs.LogFilter.LOG_ALL;
			this.logViewer.Name = "logViewer";
			this.logViewer.Size = new System.Drawing.Size(418, 192);
			this.logViewer.TabIndex = 0;
			// 
			// StorageRelocateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(418, 344);
			this.Controls.Add(this.panelLog);
			this.Controls.Add(this.panelBottom);
			this.Controls.Add(this.panelTop);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StorageRelocateForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Motion Detector数据更新";
			this.Load += new System.EventHandler(this.StorageRelocateForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StorageRelocateForm_FormClosing);
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			this.panelBottom.ResumeLayout(false);
			this.panelLog.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelTopic;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.Panel panelLog;
		private ZForge.Controls.Logs.LogViewer logViewer;
	}
}