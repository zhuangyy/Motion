using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZForge.Controls.Logs;
using ZForge.Globalization;
using System.Threading;

namespace ZForge.Controls.Update
{
	public partial class Updater : UserControl
	{
		public event RunWorkerCompletedEventHandler RunWorkerCompleted;
		public event UpdateStepChangedEventHandler UpdateStepChanged;

		private UpdateSiteCollection mUpdateSiteCollection;
		private UpdateSteps mCurrentStep = UpdateSteps.NONE;
		private string mURLSuffix = "";

		public Updater()
		{
			InitializeComponent();
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			Size s = TextRenderer.MeasureText("ABpb", SystemFonts.MessageBoxFont);
			if (s.Height < 25)
			{
				s.Height = 25;
			}
			this.tableLayoutPanel.Height = (s.Height + this.tableLayoutPanel.Margin.Top + this.tableLayoutPanel.Margin.Bottom) * this.tableLayoutPanel.RowCount;
			base.OnLayout(e);
		}

		protected virtual void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (this.RunWorkerCompleted != null)
			{
				RunWorkerCompleted(this, e);
			}
		}

		protected virtual void OnUpdateStepChanged(UpdateStepChangedEventArgs e)
		{
			if (this.UpdateStepChanged != null)
			{
				UpdateStepChanged(this, e);
			}
		}

		protected virtual void OnUpdateStepChanged()
		{
			if (this.UpdateStepChanged != null)
			{
				UpdateStepChanged(this, new UpdateStepChangedEventArgs(this.CurrentStep));
			}
		}

		#region Properties

		public UpdateSteps CurrentStep
		{
			get { return mCurrentStep; }
		}

		public UpdateSiteCollection UpdateSiteCollection
		{
			get
			{
				if (null == this.mUpdateSiteCollection)
				{
					this.mUpdateSiteCollection = new UpdateSiteCollection();
				}
				return this.mUpdateSiteCollection;
			}
			set
			{
				this.mUpdateSiteCollection = value;
				this.comboBoxURL.Items.Clear();
				if (this.mUpdateSiteCollection != null && this.mUpdateSiteCollection.Count > 0)
				{
					this.comboBoxURL.Items.AddRange(mUpdateSiteCollection.ToArray());

					UpdateSite def = this.mUpdateSiteCollection.Default;
					if (def != null)
					{
						this.comboBoxURL.SelectedItem = def;
					}
					else
					{
						this.comboBoxURL.SelectedIndex = 0;
					}
				}
			}
		}

		public string URL
		{
			set
			{
				if (value != null)
				{
					updateChecker.URL = string.Format(@"{0}/{1}/checkupdate.php", value, this.ProductID);
					updateDownloader.URL = string.Format(@"{0}/{1}/setup.ipk", value, this.ProductID);
				}
				else
				{
					updateChecker.URL = null;
					updateDownloader.URL = null;
				}
			}
		}

		public string Key
		{
			get { return updateVerifier.Key; }
			set {	updateVerifier.Key = value; }
		}

		public string LocalVersion
		{
			get {	return updateChecker.LocalVersion; }
			set	{	updateChecker.LocalVersion = value; }
		}

		public string ProductID
		{
			get { return updateChecker.ProductID; }
			set	{ updateChecker.ProductID = value; }
		}

		public string UserID
		{
			set { updateChecker.UserID = value; }
		}

		public string Destination
		{
			get { return updateInstaller.Destination; }
			set { updateInstaller.Destination = value; }
		}

		public IInstaller Installer
		{
			set { updateInstaller.Installer = value; }
		}

		public string URLSuffix
		{
			get { return this.mURLSuffix; }
			set
			{
				this.mURLSuffix = value;
			}
		}

		public List<string> AdditionalKomponentIDCollection
		{
			get
			{
				return this.updateChecker.AdditionalKomponentIDCollection;
			}
		}

		public LogViewer LogViewer
		{
			get { return this.logViewer; }
		}

		#endregion

		public bool Upgrade()
		{
			UpdateSite us = this.comboBoxURL.SelectedItem as UpdateSite;
			if (us != null)
			{
				this.URL = us.URL + "/" + this.URLSuffix;
				StepCheck();
				return true;
			}
			else
			{
				this.logViewer.LogAdd(new LogItem(LogLevel.LOG_ERROR, Translator.Instance.T("未选择更新服务器.")));
				return false;
			}
		}

		public void Reset(bool bClearLog)
		{
			mCurrentStep = UpdateSteps.NONE;
			this.OnUpdateStepChanged();

			this.pictureBoxCheck.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			this.pictureBoxDownload.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			this.pictureBoxVerify.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			this.pictureBoxInstall.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			
			this.updateChecker.Reset();
			this.updateDownloader.Reset();
			this.updateVerifier.Reset();
			this.updateInstaller.Reset();

			if (bClearLog)
			{
				this.logViewer.Clear();
			}
		}

		private void StepCheck()
		{
			mCurrentStep = UpdateSteps.CHECK;
			this.OnUpdateStepChanged();

			string m = string.Format(Translator.Instance.T("开始检查更新. ({0})"), this.updateChecker.URL);
			this.logViewer.LogAdd(new LogItem(LogLevel.LOG_INFO, m));
			this.pictureBoxCheck.Image = global::ZForge.Controls.Update.Properties.Resources.gear_run_24;
			this.pictureBoxDownload.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			this.pictureBoxVerify.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			this.pictureBoxInstall.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;

			this.updateChecker.Download();
		}

		private void StepDownload()
		{
			mCurrentStep = UpdateSteps.DOWNLOAD;
			this.OnUpdateStepChanged();

			string m = string.Format(Translator.Instance.T("开始下载更新. ({0})"), this.updateDownloader.URL);
			this.logViewer.LogAdd(new LogItem(LogLevel.LOG_INFO, m));
			this.pictureBoxDownload.Image = global::ZForge.Controls.Update.Properties.Resources.gear_run_24;
			this.pictureBoxVerify.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			this.pictureBoxInstall.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			this.updateDownloader.Output = new MemoryStream();

			this.updateDownloader.Download();
		}

		private void StepVerify()
		{
			mCurrentStep = UpdateSteps.VERIFY;
			this.OnUpdateStepChanged();

			this.logViewer.LogAdd(new LogItem(LogLevel.LOG_INFO, Translator.Instance.T("开始校验更新包...")));
			MemoryStream ms = this.updateDownloader.Output as MemoryStream;
			this.pictureBoxInstall.Image = global::ZForge.Controls.Update.Properties.Resources.gear_stop_24;
			this.pictureBoxVerify.Image = global::ZForge.Controls.Update.Properties.Resources.gear_run_24;
			
			this.updateVerifier.Verify(ms.ToArray());
		}

		private void StepInstall()
		{
			mCurrentStep = UpdateSteps.INSTALL;
			this.OnUpdateStepChanged();

			this.logViewer.LogAdd(new LogItem(LogLevel.LOG_INFO, Translator.Instance.T("开始安装更新包...")));
			MemoryStream ms = this.updateDownloader.Output as MemoryStream;
			ms.Seek(0, SeekOrigin.Begin);
			this.updateInstaller.Install(ms);
		}

		private void updateDownloader_GotContentLength(object sender, ZForge.Controls.Net.GotContentLengthEventArgs e)
		{
			if (e.Length != 0)
			{
				string m = string.Format(Translator.Instance.T("更新文件大小: ({0}字节)"), e.Length);
				this.logViewer.LogAdd(new LogItem(LogLevel.LOG_INFO, m));
			}
			else
			{
				this.logViewer.LogAdd(new LogItem(LogLevel.LOG_WARNING, Translator.Instance.T("更新文件大小不可知.")));
			}
		}

		private void updateDownloader_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (updateDownloader.ContentLength > 0)
			{
				int p = (int)((long)updateDownloader.Output.Length * 100 / updateDownloader.ContentLength);
				this.labelDownload.Text = string.Format(Translator.Instance.T("下载更新 ({0}%):"), p);
			}
			else
			{
				this.labelDownload.Text = string.Format(Translator.Instance.T("下载更新 ({0}):"), updateDownloader.Output.Length);
			}
		}

		private void updateDownloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				this.logViewer.LogAdd(new LogItem(LogLevel.LOG_ERROR, Translator.Instance.T("更新文件下载失败.") + e.Error.Message));
				this.pictureBoxDownload.Image = global::ZForge.Controls.Update.Properties.Resources.gear_error_24;
				OnRunWorkerCompleted(sender, e);
			}
			else
			{
				this.logViewer.LogAdd(new LogItem(LogLevel.LOG_INFO, Translator.Instance.T("更新文件下载完毕.")));
				this.pictureBoxDownload.Image = global::ZForge.Controls.Update.Properties.Resources.gear_ok_24;
				this.StepVerify();
			}
		}

		private void updateChecker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				this.logViewer.LogAdd(new LogItem(LogLevel.LOG_ERROR, Translator.Instance.T("检查更新失败.") + e.Error.Message));
				this.pictureBoxCheck.Image = global::ZForge.Controls.Update.Properties.Resources.gear_error_24;
				OnRunWorkerCompleted(sender, e);
			}
			else
			{
				string m = string.Format(Translator.Instance.T("当前最新的版本为{0}."), this.updateChecker.CurrentVersion);
				this.logViewer.LogAdd(new LogItem(LogLevel.LOG_INFO, m));
				if (this.updateChecker.Message != null)
				{
					this.logViewer.LogAdd(new LogItem(LogLevel.LOG_INFO, this.updateChecker.Message));
				}
				if (this.updateChecker.AdditionalKomponentIDCollection != null)
				{
					foreach (string s in this.updateChecker.AdditionalKomponentIDCollection)
					{
						this.logViewer.LogAdd(new LogItem(LogLevel.LOG_INFO, string.Format(Translator.Instance.T("新模块提示: [{0}]"), s)));
					}
				}
				this.pictureBoxCheck.Image = global::ZForge.Controls.Update.Properties.Resources.gear_ok_24;
				if (this.updateChecker.CompareVersion() <= 0)
				{
					this.logViewer.LogAdd(new LogItem(LogLevel.LOG_INFO, Translator.Instance.T("当前版本已经是最新版本, 无需更新.")));
					OnRunWorkerCompleted(sender, e);
				}
				else
				{
					this.StepDownload();
				}
			}
		}

		private void updateVerifier_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				this.logViewer.LogAdd(new LogItem(LogLevel.LOG_ERROR, Translator.Instance.T("校验更新文件失败.") + e.Error.Message));
				this.pictureBoxVerify.Image = global::ZForge.Controls.Update.Properties.Resources.gear_error_24;
				OnRunWorkerCompleted(sender, e);
			}
			else
			{
				this.logViewer.LogAdd(new LogItem(LogLevel.LOG_INFO, Translator.Instance.T("校验更新文件通过.")));
				this.pictureBoxVerify.Image = global::ZForge.Controls.Update.Properties.Resources.gear_ok_24;
				this.StepInstall();
			}
		}

		private void updateInstaller_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				this.logViewer.LogAdd(new LogItem(LogLevel.LOG_ERROR, Translator.Instance.T("安装更新文件失败.") + e.Error.Message));
				this.pictureBoxInstall.Image = global::ZForge.Controls.Update.Properties.Resources.gear_error_24;
			}
			else
			{
				this.logViewer.LogAdd(new LogItem(LogLevel.LOG_INFO, Translator.Instance.T("安装更新文件成功.")));
				this.pictureBoxInstall.Image = global::ZForge.Controls.Update.Properties.Resources.gear_ok_24;
			}
			OnRunWorkerCompleted(sender, e);
		}

		private void buttonURL_Click(object sender, EventArgs e)
		{
			UpdateSiteManagerForm fm = new UpdateSiteManagerForm();
			fm.UpdateSiteCollection = this.UpdateSiteCollection;
			fm.ShowDialog();
			this.UpdateSiteCollection = fm.UpdateSiteCollection;
		}

		private void comboBoxURL_SelectionChangeCommitted(object sender, EventArgs e)
		{
			this.UpdateSiteCollection.Default = this.comboBoxURL.SelectedItem as UpdateSite;
		}

	}
}