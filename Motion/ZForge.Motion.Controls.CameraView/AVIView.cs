using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZForge.Motion.Core;
using AForge.Video;
using AForge.Video.VFW;
using ZForge.Motion.VideoSource;
using System.IO;
using ZForge.Globalization;
using ZForge.Motion.Util;

namespace ZForge.Motion.Controls
{
	public partial class AVIView : UserControl, IVideoView, IFileView, IGlobalization
	{
		private float mViewRatio = 1.0F;
		private AVIClass mAVIClass;
		private int mStatus;
		private int mSpeed;
		private int mPosition;

		public AVIView()
		{
			InitializeComponent();
			this.Status = CameraStatus.STOPPED;
			this.Speed = 1;
			MotionPreference.Instance.UpdateUI(this);
		}

		#region IFileView members
		public IFileClass FileClass
		{
			get
			{
				return this.AVIClass;
			}
		}
		#endregion

		#region Properties
		public AVIClass AVIClass
		{
			get { return this.mAVIClass; }
			set
			{
				this.mAVIClass = value;

				if (this.mAVIClass != null)
				{
					CameraClass c = this.CameraClass;
					if (this.AVIClass.IsValid())
					{
						this.panelMaster.CaptionText = this.AVIClass.Title;

						AVIClass v = this.mAVIClass;
						string i = string.Format(Translator.Instance.T("编码格式: {0} 帧数: {1} {2}x{3}"), v.Codec, (v.Length - v.Start), v.Width, v.Height);
						this.toolStripStatusLabelInfo.Text = i;
						this.trackBarTime.Maximum = v.Length;
						this.trackBarTime.LargeChange = 1;
					}
					else
					{
						this.panelMaster.CaptionText = c.Name + " " + Translator.Instance.T("文件错误");
					}
				}
			}
		}

		public int Status
		{
			get
			{
				return this.mStatus;
			}
			set
			{
				this.mStatus = value;
				switch (this.mStatus)
				{
					case CameraStatus.PAUSED:
						this.toolStripStatusLabelStatus.Text = Translator.Instance.T("暂停");
						break;
					case CameraStatus.STOPPED:
						this.toolStripStatusLabelStatus.Text = Translator.Instance.T("停止");
						this.trackBarTime.Value = 0;
						break;
					case CameraStatus.STARTED:
						this.toolStripStatusLabelStatus.Text = Translator.Instance.T("播放");
						//this.trackBarTime.Value = 0;
						break;
				}
				if (this.IsRunning() && this.cameraWindow.Camera.VideoSource is AVIFileExVideoSource)
				{
					AVIFileExVideoSource s = (AVIFileExVideoSource)this.cameraWindow.Camera.VideoSource;
					s.Pause = (this.mStatus == CameraStatus.PAUSED);
				}
			}
		}

		public int Speed
		{
			get { return this.mSpeed; }
			set
			{
				if (value > 16)
				{
					value = 16;
				}
				if (value < -16)
				{
					value = -16;
				}
				this.mSpeed = value;
				if (this.IsRunning() && this.cameraWindow.Camera.VideoSource is AVIFileExVideoSource)
				{
					AVIFileExVideoSource s = (AVIFileExVideoSource)this.cameraWindow.Camera.VideoSource;
					s.Speed = this.mSpeed;
				}
				string t = "";
				if (this.mSpeed >= 0)
				{
					t = this.mSpeed + "x";
				}
				else
				{
					t = (0 - this.mSpeed) + "x";
					if (this.mSpeed != -1)
					{
						t = "1/" + t;
					}
				}
				this.toolStripLabelSpeed.Text = t;
			}
		}

		public int Position
		{
			get {
				if (this.IsRunning())
				{
					if (this.cameraWindow.Camera.VideoSource is AVIFileExVideoSource)
					{
						AVIFileExVideoSource s = (AVIFileExVideoSource)this.cameraWindow.Camera.VideoSource;
						this.mPosition = s.Position;
						return s.Position;
					}
				}
				return this.mPosition;
			}
			set
			{
				this.mPosition = value;
				if (this.IsRunning())
				{
					if (this.cameraWindow.Camera.VideoSource is AVIFileExVideoSource)
					{
						AVIFileExVideoSource s = (AVIFileExVideoSource)this.cameraWindow.Camera.VideoSource;
						s.Position = value;
					}
				}
			}
		}
		#endregion

		public void SetDimension(float ratio)
		{
			int ww = ((int)(322 * 100 * ratio)) / 100;
			int wh = ((int)(242 * 100 * ratio)) / 100;
			int h = wh + 60;

			this.Width = ww;
			this.cameraWindow.Height = wh;
			this.Height = h + this.toolStripControl.Height + this.statusStrip.Height + this.panelMaster.CaptionHeight;
		}

		// Open video source
		private void OpenVideoSource(IVideoSource source)
		{
			// set busy cursor
			this.Cursor = Cursors.WaitCursor;

			// close previous file
			CloseVideoSource();

			//source.VideoSourceError += new VideoSourceErrorEventHandler(source_VideoSourceError);
			// create camera
			Camera camera = new Camera(source);

			// attach camera to camera window
			cameraWindow.Camera = camera;

			// start camera
			camera.Start();
			//timer.Start();

			this.Cursor = Cursors.Default;
		}

		// Close current file
		private void CloseVideoSource()
		{
			Camera camera = cameraWindow.Camera;

			if (camera != null)
			{
				// detach camera from camera window
				cameraWindow.Camera = null;

				// signal camera to stop
				camera.SignalToStop();
				// wait for the camera
				camera.WaitForStop();

				//timer.Stop();
				camera = null;
			}
		}

		#region IVideoView Members

		public bool IsRunning()
		{
			if (this.cameraWindow.Camera != null)
			{
				return this.cameraWindow.Camera.IsRunning;
			}
			return false;
		}

		public void Start()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				AVIFileExVideoSource fileSource = new AVIFileExVideoSource();
				fileSource.Source = this.AVIClass.FileName;
				fileSource.FrameIntervalFromSource = true;
				fileSource.Speed = this.Speed;
				OpenVideoSource(fileSource);
				this.Status = CameraStatus.STARTED;
			}
			catch (Exception)
			{
				MessageBox.Show(string.Format(Translator.Instance.T("播放录像[{0}]失败!"), this.Title), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Status = CameraStatus.STOPPED;
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		public void Stop()
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				CloseVideoSource();
				this.cameraWindow.Invalidate();
			}
			catch (Exception)
			{
			}
			this.Status = CameraStatus.STOPPED;
			this.Cursor = Cursors.Default;
		}

		public void Pause(bool on)
		{
			//throw new Exception("The method or operation is not implemented.");
			this.Status = CameraStatus.PAUSED;
		}

		public Motion.Core.CameraClass CameraClass
		{
			get
			{
				return this.AVIClass.Camera;
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public float ViewRatio
		{
			get
			{
				return mViewRatio;
			}
			set
			{
				if (value < 0.5F)
				{
					value = 0.5F;
				}
				mViewRatio = value;
				this.SetDimension(value);
			}
		}

		public UserControl Me
		{
			get
			{
				return this;
			}
		}

		public string Title
		{
			get { return this.AVIClass.Title; }
		}

		public string ID
		{
			get { return this.CameraClass.ID; }
		}

		public CameraClass Owner
		{
			get { return this.AVIClass.Owner; }
		}

		public bool Remove()
		{
			this.Stop();
			if (this.AVIClass.Remove())
			{
				this.Visible = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

		#region Top Toolbar

		private void toolStripButtonStart_Click(object sender, EventArgs e)
		{
			if (this.IsRunning() && this.Status == CameraStatus.PAUSED)
			{
				this.Status = CameraStatus.STARTED;
			}
			else 
			{
				this.Start();
			}
		}

		private void toolStripButtonPause_Click(object sender, EventArgs e)
		{
			this.Pause(true);
		}

		private void toolStripButtonStop_Click(object sender, EventArgs e)
		{
			this.Stop();
		}

		private void toolStripButtonDelete_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == MessageBox.Show(Translator.Instance.T("确定要永久删除这个监控录像吗?"), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
			{
				this.Remove();
			}
		}

		private void toolStripButtonSpeedInc_Click(object sender, EventArgs e)
		{
			this.Speed++;
			if (this.Speed == 0)
			{
				this.Speed = 1;
			}
		}

		private void toolStripButtonSpeedDec_Click(object sender, EventArgs e)
		{
			this.Speed--;
			if (this.Speed == 0)
			{
				this.Speed = -1;
			}
		}

		private void toolStripButtonSaveAs_Click(object sender, EventArgs e)
		{
			this.saveFileDialog.FileName = this.panelMaster.CaptionText.Replace(":", "-");
			DialogResult r = this.saveFileDialog.ShowDialog();
			if (r == DialogResult.OK && this.saveFileDialog.FileName != null && this.saveFileDialog.FileName.Length > 0)
			{
				try
				{
					File.Copy(this.AVIClass.FileName, this.saveFileDialog.FileName, true);
					MessageBox.Show(Translator.Instance.T("录像文件导出成功.") + "\n" + this.saveFileDialog.FileName, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show(Translator.Instance.T("录像文件导出失败!") + "\n" + ex.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void toolStripButtonSnag_Click(object sender, EventArgs e)
		{
			Bitmap b = this.cameraWindow.CurrentFrame;
			if (b != null)
			{
				CameraSnagForm f = new CameraSnagForm();
				f.Camera = this.CameraClass;
				f.Bitmap = b;
				f.Show();
			}
			else
			{
				MessageBox.Show(Translator.Instance.T("当前窗口中尚无图像, 无法完成截图操作."), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void toolStripButtonClose_Click(object sender, EventArgs e)
		{
			this.Stop();
			this.Visible = false;
		}

		private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
		{
			float[] ratios = new float[] { 0.5F, 0.6F, 0.7F, 0.8F, 0.9F, 1.0F, 1.2F, 1.5F, 2.0F };
			int n = 0;
			foreach (float f in ratios)
			{
				n++;
				if (this.ViewRatio == f && n < ratios.Length)
				{
					this.ViewRatio = ratios[n];
					break;
				}
			}
		}

		private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
		{
			float[] ratios = new float[] { 0.5F, 0.6F, 0.7F, 0.8F, 0.9F, 1.0F, 1.2F, 1.5F, 2.0F };
			int n = 0;
			foreach (float f in ratios)
			{
				if (this.ViewRatio == f && n > 0)
				{
					this.ViewRatio = ratios[n - 1];
					break;
				}
				n++;
			}
		}

		#endregion

		private void trackBarTime_ValueChanged(object sender, EventArgs e)
		{
			this.groupBoxTrack.Text = Translator.Instance.T("当前位置:") + " " + this.trackBarTime.Value;
			if (this.Position != this.trackBarTime.Value)
			{
				this.Position = this.trackBarTime.Value;
			}
		}

		private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			this.trackBarTime.Value = this.Position;
		}

		#region IGlobalization Members

		public void UpdateCulture()
		{
			this.saveFileDialog.Title = Translator.Instance.T("导出录像");
			this.saveFileDialog.Filter = Translator.Instance.T("录像文件") + "(*.avi)|*.avi";

			this.toolStripButtonStart.Text = Translator.Instance.T("播放");
			this.toolStripButtonPause.Text = Translator.Instance.T("暂停");
			this.toolStripButtonStop.Text = Translator.Instance.T("停止");
			this.toolStripButtonClose.Text = Translator.Instance.T("关闭");
			this.toolStripButtonSnag.Text = Translator.Instance.T("截屏");
			this.toolStripButtonSpeedInc.Text = Translator.Instance.T("加快播放速度");
			this.toolStripButtonSpeedDec.Text = Translator.Instance.T("减慢播放速度");
			this.toolStripButtonSaveAs.Text = Translator.Instance.T("另存为...");
			this.toolStripButtonDelete.Text = Translator.Instance.T("删除");
			this.toolStripButtonZoomIn.Text = Translator.Instance.T("放大");
			this.toolStripButtonZoomIn.ToolTipText = Translator.Instance.T("放大");
			this.toolStripButtonZoomOut.Text = Translator.Instance.T("缩小");
			this.toolStripButtonZoomOut.ToolTipText = Translator.Instance.T("缩小");

			this.AVIClass = this.AVIClass;
			this.Status = this.Status;
		}

		#endregion
	}
}
