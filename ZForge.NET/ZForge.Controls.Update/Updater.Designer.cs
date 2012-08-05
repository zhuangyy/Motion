namespace ZForge.Controls.Update
{
	partial class Updater
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
			ZForge.Controls.Update.ZipDefaultInstaller zipDefaultInstaller1 = new ZForge.Controls.Update.ZipDefaultInstaller();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.labelURL = new System.Windows.Forms.Label();
			this.comboBoxURL = new System.Windows.Forms.ComboBox();
			this.buttonURL = new System.Windows.Forms.Button();
			this.labelCheck = new System.Windows.Forms.Label();
			this.updateChecker = new ZForge.Controls.Update.UpdateChecker();
			this.pictureBoxCheck = new System.Windows.Forms.PictureBox();
			this.labelDownload = new System.Windows.Forms.Label();
			this.updateDownloader = new ZForge.Controls.Net.Downloader();
			this.pictureBoxDownload = new System.Windows.Forms.PictureBox();
			this.labelVerify = new System.Windows.Forms.Label();
			this.updateVerifier = new ZForge.Controls.Update.UpdateVerifier();
			this.pictureBoxVerify = new System.Windows.Forms.PictureBox();
			this.labelInstall = new System.Windows.Forms.Label();
			this.updateInstaller = new ZForge.Controls.Update.UpdateInstaller();
			this.pictureBoxInstall = new System.Windows.Forms.PictureBox();
			this.logViewer = new ZForge.Controls.Logs.LogViewer();
			this.tableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheck)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxDownload)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxVerify)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxInstall)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.Controls.Add(this.labelURL, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.comboBoxURL, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.buttonURL, 2, 0);
			this.tableLayoutPanel.Controls.Add(this.labelCheck, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.updateChecker, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.pictureBoxCheck, 2, 1);
			this.tableLayoutPanel.Controls.Add(this.labelDownload, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.updateDownloader, 1, 2);
			this.tableLayoutPanel.Controls.Add(this.pictureBoxDownload, 2, 2);
			this.tableLayoutPanel.Controls.Add(this.labelVerify, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.updateVerifier, 1, 3);
			this.tableLayoutPanel.Controls.Add(this.pictureBoxVerify, 2, 3);
			this.tableLayoutPanel.Controls.Add(this.labelInstall, 0, 4);
			this.tableLayoutPanel.Controls.Add(this.updateInstaller, 1, 4);
			this.tableLayoutPanel.Controls.Add(this.pictureBoxInstall, 2, 4);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 5;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(417, 119);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// labelURL
			// 
			this.labelURL.AutoSize = true;
			this.labelURL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelURL.Location = new System.Drawing.Point(3, 0);
			this.labelURL.Name = "labelURL";
			this.labelURL.Size = new System.Drawing.Size(94, 23);
			this.labelURL.TabIndex = 11;
			this.labelURL.Text = "选择更新服务器:";
			this.labelURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxURL
			// 
			this.comboBoxURL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxURL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxURL.FormattingEnabled = true;
			this.comboBoxURL.Location = new System.Drawing.Point(103, 3);
			this.comboBoxURL.Name = "comboBoxURL";
			this.comboBoxURL.Size = new System.Drawing.Size(280, 21);
			this.comboBoxURL.TabIndex = 12;
			this.comboBoxURL.SelectionChangeCommitted += new System.EventHandler(this.comboBoxURL_SelectionChangeCommitted);
			// 
			// buttonURL
			// 
			this.buttonURL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonURL.Image = global::ZForge.Controls.Update.Properties.Resources.edit_16;
			this.buttonURL.Location = new System.Drawing.Point(389, 3);
			this.buttonURL.Name = "buttonURL";
			this.buttonURL.Size = new System.Drawing.Size(25, 17);
			this.buttonURL.TabIndex = 13;
			this.buttonURL.UseVisualStyleBackColor = true;
			this.buttonURL.Click += new System.EventHandler(this.buttonURL_Click);
			// 
			// labelCheck
			// 
			this.labelCheck.AutoSize = true;
			this.labelCheck.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelCheck.Location = new System.Drawing.Point(3, 23);
			this.labelCheck.Name = "labelCheck";
			this.labelCheck.Size = new System.Drawing.Size(94, 23);
			this.labelCheck.TabIndex = 8;
			this.labelCheck.Text = "检查更新:";
			this.labelCheck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// updateChecker
			// 
			this.updateChecker.Dock = System.Windows.Forms.DockStyle.Fill;
			this.updateChecker.LocalVersion = null;
			this.updateChecker.Location = new System.Drawing.Point(103, 26);
			this.updateChecker.Name = "updateChecker";
			this.updateChecker.Output = ((System.IO.Stream)(resources.GetObject("updateChecker.Output")));
			this.updateChecker.Post = ((System.Collections.Generic.Dictionary<string, object>)(resources.GetObject("updateChecker.Post")));
			this.updateChecker.ProductID = null;
			this.updateChecker.Size = new System.Drawing.Size(280, 17);
			this.updateChecker.TabIndex = 9;
			this.updateChecker.URL = null;
			this.updateChecker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.updateChecker_RunWorkerCompleted);
			// 
			// pictureBoxCheck
			// 
			this.pictureBoxCheck.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBoxCheck.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			this.pictureBoxCheck.Location = new System.Drawing.Point(389, 26);
			this.pictureBoxCheck.Name = "pictureBoxCheck";
			this.pictureBoxCheck.Size = new System.Drawing.Size(25, 17);
			this.pictureBoxCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxCheck.TabIndex = 10;
			this.pictureBoxCheck.TabStop = false;
			// 
			// labelDownload
			// 
			this.labelDownload.AutoSize = true;
			this.labelDownload.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelDownload.Location = new System.Drawing.Point(3, 46);
			this.labelDownload.Name = "labelDownload";
			this.labelDownload.Size = new System.Drawing.Size(94, 23);
			this.labelDownload.TabIndex = 0;
			this.labelDownload.Text = "下载更新:";
			this.labelDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// updateDownloader
			// 
			this.updateDownloader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.updateDownloader.Location = new System.Drawing.Point(103, 49);
			this.updateDownloader.Name = "updateDownloader";
			this.updateDownloader.Output = null;
			this.updateDownloader.Post = ((System.Collections.Generic.Dictionary<string, object>)(resources.GetObject("updateDownloader.Post")));
			this.updateDownloader.Size = new System.Drawing.Size(280, 17);
			this.updateDownloader.TabIndex = 3;
			this.updateDownloader.URL = null;
			this.updateDownloader.GotContentLength += new ZForge.Controls.Net.GotContentLengthEventHandler(this.updateDownloader_GotContentLength);
			this.updateDownloader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.updateDownloader_RunWorkerCompleted);
			this.updateDownloader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.updateDownloader_ProgressChanged);
			// 
			// pictureBoxDownload
			// 
			this.pictureBoxDownload.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBoxDownload.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			this.pictureBoxDownload.Location = new System.Drawing.Point(389, 49);
			this.pictureBoxDownload.Name = "pictureBoxDownload";
			this.pictureBoxDownload.Size = new System.Drawing.Size(25, 17);
			this.pictureBoxDownload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxDownload.TabIndex = 2;
			this.pictureBoxDownload.TabStop = false;
			// 
			// labelVerify
			// 
			this.labelVerify.AutoSize = true;
			this.labelVerify.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelVerify.Location = new System.Drawing.Point(3, 69);
			this.labelVerify.Name = "labelVerify";
			this.labelVerify.Size = new System.Drawing.Size(94, 23);
			this.labelVerify.TabIndex = 1;
			this.labelVerify.Text = "校验更新包:";
			this.labelVerify.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// updateVerifier
			// 
			this.updateVerifier.AutoSize = true;
			this.updateVerifier.Dock = System.Windows.Forms.DockStyle.Fill;
			this.updateVerifier.Key = null;
			this.updateVerifier.Location = new System.Drawing.Point(103, 72);
			this.updateVerifier.Name = "updateVerifier";
			this.updateVerifier.Size = new System.Drawing.Size(280, 17);
			this.updateVerifier.TabIndex = 4;
			this.updateVerifier.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.updateVerifier_RunWorkerCompleted);
			// 
			// pictureBoxVerify
			// 
			this.pictureBoxVerify.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBoxVerify.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			this.pictureBoxVerify.Location = new System.Drawing.Point(389, 72);
			this.pictureBoxVerify.Name = "pictureBoxVerify";
			this.pictureBoxVerify.Size = new System.Drawing.Size(25, 17);
			this.pictureBoxVerify.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxVerify.TabIndex = 6;
			this.pictureBoxVerify.TabStop = false;
			// 
			// labelInstall
			// 
			this.labelInstall.AutoSize = true;
			this.labelInstall.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelInstall.Location = new System.Drawing.Point(3, 92);
			this.labelInstall.Name = "labelInstall";
			this.labelInstall.Size = new System.Drawing.Size(94, 27);
			this.labelInstall.TabIndex = 2;
			this.labelInstall.Text = "安装更新:";
			this.labelInstall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// updateInstaller
			// 
			this.updateInstaller.AutoSize = true;
			this.updateInstaller.Destination = null;
			this.updateInstaller.Dock = System.Windows.Forms.DockStyle.Fill;
			this.updateInstaller.Installer = zipDefaultInstaller1;
			this.updateInstaller.Location = new System.Drawing.Point(103, 95);
			this.updateInstaller.Name = "updateInstaller";
			this.updateInstaller.Size = new System.Drawing.Size(280, 21);
			this.updateInstaller.TabIndex = 5;
			this.updateInstaller.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.updateInstaller_RunWorkerCompleted);
			// 
			// pictureBoxInstall
			// 
			this.pictureBoxInstall.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBoxInstall.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			this.pictureBoxInstall.Location = new System.Drawing.Point(389, 95);
			this.pictureBoxInstall.Name = "pictureBoxInstall";
			this.pictureBoxInstall.Size = new System.Drawing.Size(25, 21);
			this.pictureBoxInstall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxInstall.TabIndex = 7;
			this.pictureBoxInstall.TabStop = false;
			// 
			// logViewer
			// 
			this.logViewer.AutoScrollToLast = true;
			this.logViewer.CountError = 0;
			this.logViewer.CountInfo = 0;
			this.logViewer.CountWarn = 0;
			this.logViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logViewer.Location = new System.Drawing.Point(0, 119);
			this.logViewer.LogFilter = ZForge.Controls.Logs.LogFilter.LOG_ALL;
			this.logViewer.Name = "logViewer";
			this.logViewer.Size = new System.Drawing.Size(417, 212);
			this.logViewer.TabIndex = 1;
			// 
			// Updater
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.logViewer);
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "Updater";
			this.Size = new System.Drawing.Size(417, 331);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxCheck)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxDownload)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxVerify)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxInstall)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label labelDownload;
		private System.Windows.Forms.Label labelVerify;
		private System.Windows.Forms.Label labelInstall;
		private ZForge.Controls.Net.Downloader updateDownloader;
		private ZForge.Controls.Logs.LogViewer logViewer;
		private ZForge.Controls.Update.UpdateVerifier updateVerifier;
		private ZForge.Controls.Update.UpdateInstaller updateInstaller;
		private System.Windows.Forms.PictureBox pictureBoxDownload;
		private System.Windows.Forms.PictureBox pictureBoxVerify;
		private System.Windows.Forms.PictureBox pictureBoxInstall;
		private System.Windows.Forms.PictureBox pictureBoxCheck;
		private ZForge.Controls.Update.UpdateChecker updateChecker;
		private System.Windows.Forms.Label labelCheck;
		private System.Windows.Forms.Label labelURL;
		private System.Windows.Forms.ComboBox comboBoxURL;
		private System.Windows.Forms.Button buttonURL;
	}
}
