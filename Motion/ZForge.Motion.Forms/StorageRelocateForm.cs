using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZForge.Motion.Core;
using System.Collections;
using System.IO;
using ZForge.Motion.Util;
using ZForge.Globalization;
using ZForge.Controls.Logs;

namespace ZForge.Motion.Forms
{
	public partial class StorageRelocateForm : Form, IGlobalization
	{
		private bool mUpdating = false;
		private string mFrom = null;

		public StorageRelocateForm()
		{
			InitializeComponent();
			MotionPreference.Instance.UpdateUI(this);
		}

		private const int CP_NOCLOSE_BUTTON = 0x200;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams myCp = base.CreateParams;
				myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
				return myCp;
			}
		} 

		#region Properties

		public string Topic
		{
			set
			{
				this.labelTopic.Text = value;
			}
		}

		public string StorageSource
		{
			get
			{
				return this.mFrom;
			}
			set
			{
				this.mFrom = value;
			}
		}

		#endregion

		public bool StorageRelocate()
		{
			mUpdating = true;

			bool b = false;
			try
			{
				this.MoveFiles(Path.Combine(this.StorageSource, "Motion.AVI"), MotionConfiguration.Instance.StorageAVI);
				this.MoveFiles(Path.Combine(this.StorageSource, "Motion.PIC"), MotionConfiguration.Instance.StoragePIC);
				b = true;
			}
			catch (Exception ex)
			{
				string msg = string.Format(Translator.Instance.T("{0}数据更新失败!"), MotionPreference.Instance.ProductFullName);
				MessageBox.Show(msg + "\n" + ex.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			mUpdating = false;
			return b;
		}

		private void ProgressBarInitialization()
		{
			this.logViewer.LogAdd(LogLevel.LOG_INFO, Translator.Instance.T("开始更新..."));
			this.logViewer.LogAdd(LogLevel.LOG_INFO, string.Format(Translator.Instance.T("源目录: {0}"), this.StorageSource));
			this.logViewer.LogAdd(LogLevel.LOG_INFO, string.Format(Translator.Instance.T("新目录: {0}"), MotionConfiguration.Instance.Storage));

			List<string> dirs = new List<string>();
			dirs.Add(Path.Combine(this.StorageSource, "Motion.AVI"));
			dirs.Add(Path.Combine(this.StorageSource, "Motion.PIC"));

			int count = 0;
			foreach (string dir in dirs)
			{
				DirectoryInfo di = new DirectoryInfo(dir);
				if (di.Exists)
				{
					FileInfo[] fis = di.GetFiles();
					count += fis.Length;
					this.logViewer.LogAdd(LogLevel.LOG_INFO, string.Format(Translator.Instance.T("{0}个文件 ({1})"), fis.Length, dir));
				}
			}
			this.progressBar.Maximum = count + 1;
			this.progressBar.Minimum = 0;
			this.progressBar.Value = 0;

			this.buttonClose.Enabled = false;
		}

		private void MoveFiles(string from, string path)
		{
			DirectoryInfo d = new DirectoryInfo(path);
			if (false == d.Exists)
			{
				d.Create();
			}
			DirectoryInfo s = new DirectoryInfo(from);
			if (false == s.Exists)
			{
				return;
			}
			FileInfo[] fis = s.GetFiles();
			foreach (FileInfo fi in fis)
			{
				try
				{
					fi.CopyTo(Path.Combine(d.FullName, fi.Name), true);
					fi.Delete();
				}
				catch (Exception ex)
				{
					this.logViewer.LogAdd(LogLevel.LOG_ERROR, string.Format(Translator.Instance.T("迁移文件[{0}]失败, 详细信息: {1}"), fi.Name, ex.Message));
				}
				this.backgroundWorker.ReportProgress(1);
			}
			try
			{
				s.Delete();
			}
			catch (Exception) { }
		}

		#region Form events

		private void StorageRelocateForm_Load(object sender, EventArgs e)
		{
			this.ProgressBarInitialization();
			this.backgroundWorker.RunWorkerAsync();
		}

		private void StorageRelocateForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (mUpdating)
			{
				e.Cancel = true;
			}
		}

		#endregion

		#region background worker

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			if (mUpdating == false)
			{
				this.StorageRelocate();
			}
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.progressBar.Value++;
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			mUpdating = false;
			this.buttonClose.Enabled = true;
			this.progressBar.Value = this.progressBar.Maximum;
			this.logViewer.LogAdd(LogLevel.LOG_INFO, string.Format(Translator.Instance.T("数据迁移结束")));
		}

		#endregion

		#region IGlobalization Members

		public void UpdateCulture()
		{
			string m = Translator.Instance.T("{0}正在更新数据存储位置, 请稍候...");
			this.Topic = string.Format(m, MotionPreference.Instance.ProductName);
			this.Text = string.Format(Translator.Instance.T("{0} 数据更新"), MotionPreference.Instance.ProductFullName);
		}

		#endregion

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}