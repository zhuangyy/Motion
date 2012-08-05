using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZForge.Motion.VideoSource;
using AForge.Video.VFW;
using AForge.Video;
using ZForge.Motion.Core;
using ZForge.Motion.PlugIns;
using System.Threading;
using AForge.Video.DirectShow;
using System.IO;
using ZForge.Globalization;
using ZForge.Motion.Util;
using AForge.Vision.Motion;
using ZForge.PlugIn;

namespace ZForge.Motion.Controls
{
	public partial class CameraView : UserControl, IVideoView, IGlobalization
	{
		private ObjectMotionDetector mMotionDetector = new ObjectMotionDetector(true);
		private AVIWriterEx mAVIWriter = null;
		private CameraStatistics mStat = null;
		private Motion.Core.CameraStatus mStatus;
		private Motion.Core.CameraClass mCameraClass;
		private Mutex mMutexCapture = new Mutex();
		private Mutex mMutexAlarm = new Mutex();
		private bool mEditMode = false;
		private PTZControl mPTZControl = null;

		private int mIntervalsToSave = 0;
		private int mAlarmCount = 0;
		private bool mFireAlarmEvent = false;
		private float mViewRatio = 1.0F;
		private bool mHeightFixed = false;
		private int mCaptureCount = 0;
		private int mSnagCount = 0;
		private long mCaptureElapse = 0;
		private bool mHasFrame = false;
		//private bool mAlarmIsRunning = false;

		public CameraView()
		{
			InitializeComponent();
			mPTZControl = new PTZControl(this);
			this.toolStripMain.Items.AddRange(mPTZControl.ToolStripItemCollection.ToArray());

			mStatus = new Motion.Core.CameraStatus((int)Motion.Core.CameraStatus.STOPPED);
			Translator.Instance.Update(this);

			MotionPreference.Instance.UpdateUI(this);

			mStat = new CameraStatistics();
			mMotionDetector.ObjectCoupledSize = false;

			this.toolStripComboBoxResolution.SelectedIndex = 0;

			this.trackBarElapse.Maximum = ValueRange.AlarmElapseMax;
			this.trackBarElapse.Minimum = ValueRange.AlarmElapseMin;
			this.trackBarElapse.Value = ValueRange.AlarmElapseDefault;
			this.trackBarElapse.NumericUpDown.ValueChanged += new EventHandler(trackBarElapse_ValueChanged);
			
			this.trackBarSensi.Maximum = ValueRange.SensibilityMax;
			this.trackBarSensi.Minimum = ValueRange.SensibilityMin;
			this.trackBarSensi.Value = ValueRange.SensibilityDefault;
			this.trackBarSensi.NumericUpDown.ValueChanged += new EventHandler(trackBarSensi_ValueChanged);

			this.trackBarDifferenceThreshold.Maximum = ValueRange.DifferenceThresholdMax;
			this.trackBarDifferenceThreshold.Minimum = ValueRange.DifferenceThresholdMin;
			this.trackBarDifferenceThreshold.Value = ValueRange.DifferenceThresholdDefault;
			this.trackBarDifferenceThreshold.NumericUpDown.ValueChanged += new EventHandler(trackBarDifferenceThreshold_ValueChanged);

			this.panelCameraViewEdit.Visible = false;
			this.UpdateUIWithFrame();
		}

		#region IVideoView Members
		public Motion.Core.CameraClass CameraClass
		{
			get
			{
				return mCameraClass;
			}
			set
			{
				mCameraClass = value;
				if (mCameraClass != null)
				{
					this.UpdateUI();
					foreach (AvailablePlugIn<IPlugIn> p in mCameraClass.PlugIns.AvailablePlugInCollection)
					{
						IPlugInUI u = p.Instance as IPlugInUI;
						if (u != null)
						{
							List<System.Windows.Forms.ToolStripItem> list = u.UICameraViewToolStripItems;
							if (list != null && list.Count > 0)
							{
								this.toolStripMain.SuspendLayout();
								foreach (System.Windows.Forms.ToolStripItem b in list)
								{
									this.toolStripMain.Items.Add(b);
								}
								this.toolStripMain.ResumeLayout(false);
								this.toolStripMain.PerformLayout();
							}
						}
						IPlugInLog l = p.Instance as IPlugInLog;
						if (l != null)
						{
							l.Log += new ZForge.Controls.Logs.LogEventHandler(PlugIn_Instance_Log);
						}
						IPlugInFeed f = p.Instance as IPlugInFeed;
						if (f != null)
						{
							f.FeedImage += new PlugInFeedEventHandler(PlugIn_Instance_FeedImage);
						}
					}
					mCameraClass.ItemValueChanged += new ItemValueChangedEventHandler(CameraClass_ItemValueChanged);
				}
				this.cameraWindow.CameraClass = mCameraClass;
			}
		}

		public float ViewRatio
		{
			get
			{
				return this.mViewRatio;
			}
			set
			{
				this.mViewRatio = value;
				this.SetDimension(this.mViewRatio);
			}
		}

		public string ID
		{
			get { return this.CameraClass.ID; }
		}

		public CameraClass Owner
		{
			get { return this.CameraClass; }
		}

		public UserControl Me
		{
			get { return this; }
		}

		public string Title
		{
			get { return this.CameraClass.Name; }
		}
		
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
			if (false == this.CameraClass.ValidCheck(true))
			{
				return;
			}
			this.Cursor = Cursors.WaitCursor;
			try
			{
				CameraViewBeforeStartEventArgs e = new CameraViewBeforeStartEventArgs(this.Status, this);
				OnCameraViewBeforeStartEvent(e);
				if (e.Cancel == false)
				{
					OnCameraViewLog(new CameraViewLogEventArgs(ZForge.Controls.Logs.LogLevel.LOG_INFO, string.Format(Translator.Instance.T("启动摄像头[{0}]."), this.CameraClass.Name), this));
					AForge.Video.IVideoSource source = this.CameraClass.VideoSource;
					if (source == null)
					{
						throw new System.NullReferenceException();
					}
					OpenVideoSource(source);
					this.SetStatus(Motion.Core.CameraStatus.STOPPED, false);
					this.SetStatus(Motion.Core.CameraStatus.STARTED, true);
				}
				e = null;
			}
			catch (Exception)
			{
				MessageBox.Show(string.Format(Translator.Instance.T("启动摄像头[{0}]失败!"), this.CameraClass.Name), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				OnCameraViewLog(new CameraViewLogEventArgs(ZForge.Controls.Logs.LogLevel.LOG_ERROR, string.Format(Translator.Instance.T("启动摄像头[{0}]失败!"), this.CameraClass.Name), this));
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		public void Stop()
		{
			this.SetStatus(Motion.Core.CameraStatus.STARTED, false);
			this.SetStatus(Motion.Core.CameraStatus.STOPPED, true);
			this.Cursor = Cursors.WaitCursor;
			try
			{
				mCameraClass.PlugIns.ReleaseAll();
				CloseVideoSource();
				this.cameraWindow.Invalidate();
				OnCameraViewLog(new CameraViewLogEventArgs(ZForge.Controls.Logs.LogLevel.LOG_INFO, string.Format(Translator.Instance.T("关闭摄像头[{0}]."), this.CameraClass.Name), this));
			}
			catch (Exception)
			{
			}
			this.mHasFrame = false;
			this.UpdateUIWithFrame();
			this.Cursor = Cursors.Default;
		}

		public void Pause(bool on)
		{
			this.SetStatus(Motion.Core.CameraStatus.PAUSED, on);
		}

		public bool Remove()
		{
			this.Stop();
			this.Visible = false;
			return true;
		}
		#endregion

		#region Properties
		private CAPTUREFLAG CaptureFlag
		{
			get
			{
				if (null == this.CameraClass)
				{
					return CAPTUREFLAG.NOCAPTURE;
				}
				return this.CameraClass.Capture;
			}
			set
			{
				if (value != CAPTUREFLAG.NOCAPTURE)
				{
					if (MotionConfiguration.Instance.StorageIsValid == false)
					{
						string m = Translator.Instance.T("数据存储目录[{0}]不存在, 您的录像将无法存储.");
						m += "\n";
						m += Translator.Instance.T("如果您需要录像, 请通过[主菜单]->[工具]->[使用偏好...], 设置您的数据存储目录.");
						MessageBox.Show(string.Format(m, MotionConfiguration.Instance.Storage), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
						value = CAPTUREFLAG.NOCAPTURE;
					}
				}

				this.CameraClass.Capture = value;
				switch (value)
				{
					case CAPTUREFLAG.ALWAYS:
						toolStripDropDownButtonCapture.Image = toolStripMenuItemCaptureAlways.Image;
						toolStripDropDownButtonCapture.Text = toolStripMenuItemCaptureAlways.Text;
						toolStripDropDownButtonCapture.ToolTipText = toolStripMenuItemCaptureAlways.ToolTipText;
						break;
					case CAPTUREFLAG.NOCAPTURE:
						toolStripDropDownButtonCapture.Image = toolStripMenuItemCaptureNone.Image;
						toolStripDropDownButtonCapture.Text = toolStripMenuItemCaptureNone.Text;
						toolStripDropDownButtonCapture.ToolTipText = toolStripMenuItemCaptureNone.ToolTipText;
						this.StopCapture();
						break;
					case CAPTUREFLAG.ONALARM:
						toolStripDropDownButtonCapture.Image = toolStripMenuItemCaptureOnAlarm.Image;
						toolStripDropDownButtonCapture.Text = toolStripMenuItemCaptureOnAlarm.Text;
						toolStripDropDownButtonCapture.ToolTipText = toolStripMenuItemCaptureOnAlarm.ToolTipText;
						break;
				}
			}
		}

		private int CaptureCount
		{
			get { return this.mCaptureCount; }
			set
			{
				this.mCaptureCount = value;
			}
		}

		private int SnagCount
		{
			get { return this.mSnagCount; }
			set
			{
				this.mSnagCount = value;
			}
		}

		public int AlarmCount
		{
			get
			{
				return this.mAlarmCount;
			}
			set
			{
				bool fireEvent = (this.mAlarmCount != value) ? true : false;
				this.mAlarmCount = value;
				this.toolStripStatusLabelAlarm.Text = Translator.Instance.T("报警:") + " " + value;
				if (fireEvent)
				{
					CameraViewAlarmCountChangedEventArgs e = new CameraViewAlarmCountChangedEventArgs(value, this);
					OnCameraViewAlarmCountChangedEvent(e);
					e = null;

					if (value != 0)
					{
						string msg = string.Format(Translator.Instance.T("报警! ({0})."), (this.DetectMode == DETECTMODE.MOTION) ? Translator.Instance.T("捕捉到物体移动") : Translator.Instance.T("捕捉到画面静止"));
						OnCameraViewLog(new CameraViewLogEventArgs(ZForge.Controls.Logs.LogLevel.LOG_WARNING, msg, this));
					}
				}
			}
		}

		public int Status
		{
			get
			{
				return mStatus.Value;
			}
			set
			{
				bool fireEvent = (mStatus.Value != value);
				mStatus.Value = value;
				if (mStatus.IsStatusSet(Motion.Core.CameraStatus.PAUSED))
				{
					toolStripStatusLabelStatus.Text = Translator.Instance.T("已暂停");
				}
				else if (mStatus.IsStatusSet(Motion.Core.CameraStatus.STARTED))
				{
					toolStripStatusLabelStatus.Text = Translator.Instance.T("已启动");
				}
				else
				{
					toolStripStatusLabelStatus.Text = Translator.Instance.T("已停止");
				}
				this.cameraWindow.Paused = mStatus.IsStatusSet(Motion.Core.CameraStatus.PAUSED);
				this.toolStripButtonPause.Checked = mStatus.IsStatusSet(Motion.Core.CameraStatus.PAUSED);
				if (fireEvent)
				{
					CameraViewStatusChangedEventArgs e = new CameraViewStatusChangedEventArgs(mStatus.Value, this);
					OnCameraViewStatusChangedEvent(e);
					e = null;
				}
			}
		}

		public bool EditMode
		{
			get
			{
				return this.mEditMode;
			}
			set
			{
				this.mEditMode = value;
				if (value == true)
				{
					this.BorderStyle = BorderStyle.None;
					this.HeightFixed = true;
					this.ShowEdit = true;

					this.toolStripButtonClose.Enabled = false;
					this.toolStripButtonClose.Visible = false;
					this.toolStripDropDownButtonCapture.Enabled = false;
					this.toolStripDropDownButtonCapture.Visible = false;
					this.toolStripButtonKit.Enabled = false;
					this.toolStripButtonKit.Visible = false;
					this.toolStripButtonAlarmClean.Enabled = false;
					this.toolStripButtonAlarmClean.Visible = false;
					this.toolStripButtonSnag.Enabled = false;
					this.toolStripButtonSnag.Visible = false;

					this.toolStripStatusLabelAlarm.Text = "";
				}
			}
		}

		public bool HeightFixed
		{
			get
			{
				return this.mHeightFixed;
			}
			set
			{
				this.mHeightFixed = value;
			}
		}

		public bool ShowEdit
		{
			get
			{
				return this.toolStripButtonKit.Checked;
			}
			set
			{
				this.toolStripButtonKit.Checked = value;
				this.panelCameraViewEdit.Visible = value;
			}
		}

		public bool ShowBanner
		{
			get { return (this.CameraClass != null) ? this.CameraClass.Banner : false; }
			set
			{
				if (this.CameraClass != null)
				{
					this.CameraClass.Banner = value;
				}
				this.toolStripButtonBanner.Checked = value;
				this.cameraWindow.Banner = value;
				this.SetDimension(this.ViewRatio);
			}
		}

		public bool ShowMotionRect
		{
			get { return (this.CameraClass != null) ? this.CameraClass.ShowMotionRect : false; }
			set
			{
				if (this.CameraClass != null)
				{
					this.CameraClass.ShowMotionRect = value;
				}
				this.toolStripButtonShowMotionRect.Checked = value;
				mMotionDetector.HighlightMotionRegions = value;
			}
		}

		public decimal Sensibility
		{
			get { return this.trackBarSensi.Value; }
			set
			{
				if (this.CameraClass != null)
				{
					this.CameraClass.Sensibility = value;
				}
				mMotionDetector.MinObjectDimension = (int)value;
				this.trackBarSensi.Value = value;
			}
		}

		public decimal DifferenceThreshold
		{
			get { return this.trackBarDifferenceThreshold.Value; }
			set
			{
				if (this.CameraClass != null)
				{
					this.CameraClass.DifferenceThreshold = value;
				}
				mMotionDetector.DifferenceThreshold = (int) value;
				this.trackBarDifferenceThreshold.Value = value;
			}
		}

		public DETECTMODE DetectMode
		{
			get { return this.CameraClass.DetectMode; }
			set
			{
				this.CameraClass.DetectMode = value;
				switch (value)
				{
					case DETECTMODE.STILLNESS:
						this.toolStripDropDownButtonDetectMode.Image = this.toolStripMenuItemDetectModeStillness.Image;
						this.toolStripDropDownButtonDetectMode.Text = this.toolStripMenuItemDetectModeStillness.Text;
						this.toolStripDropDownButtonDetectMode.ToolTipText = this.toolStripMenuItemDetectModeStillness.Text;
						break;
					default:
						this.toolStripDropDownButtonDetectMode.Image = this.toolStripMenuItemDetectModeMotion.Image;
						this.toolStripDropDownButtonDetectMode.Text = this.toolStripMenuItemDetectModeMotion.Text;
						this.toolStripDropDownButtonDetectMode.ToolTipText = this.toolStripMenuItemDetectModeMotion.Text;
						break;
				}
			}
		}
		#endregion

		#region Event
		public event CameraViewStatusChangedEventHandler EventStatusChanged;
		public event CameraViewBeforeStartEventHandler EventBeforeStart;
		public event CameraViewAlarmCountChangedEventHandler EventAlarmCountChanged;
		public event CameraViewCaptureStartedEventHandler EventRecordStarted;
		public event CameraViewCaptureFinishedEventHandler EventRecordFinished;
		public event CameraViewLogEventHandler EventLog;

		protected virtual void OnCameraViewStatusChangedEvent(CameraViewStatusChangedEventArgs e)
		{
			if (this.EventStatusChanged != null)
			{
				EventStatusChanged(this, e);
			}
		}

		protected virtual void OnCameraViewBeforeStartEvent(CameraViewBeforeStartEventArgs e)
		{
			if (this.EventBeforeStart != null)
			{
				EventBeforeStart(this, e);
			}
		}

		protected virtual void OnCameraViewAlarmCountChangedEvent(CameraViewAlarmCountChangedEventArgs e)
		{
			if (this.EventAlarmCountChanged != null)
			{
				EventAlarmCountChanged(this, e);
			}
		}

		protected virtual void OnCameraViewRecordStarted(CameraViewCaptureEventArgs e)
		{
			if (this.EventRecordStarted != null)
			{
				EventRecordStarted(this, e);
			}
		}

		protected virtual void OnCameraViewRecordFinished(CameraViewCaptureEventArgs e)
		{
			if (this.EventRecordFinished != null)
			{
				EventRecordFinished(this, e);
			}
		}

		protected virtual void OnCameraViewLog(CameraViewLogEventArgs e)
		{
			if (this.EventLog != null)
			{
				EventLog(this, e);
			}
		}

		private void CameraClass_ItemValueChanged(object sender, EventArgs e)
		{
			this.UpdateUI();
		}
		#endregion

		#region UI
		public void SetDimension(float ratio)
		{
			int ww = ((int)(322 * 100 * ratio)) / 100;
			int oh = 242;
			//if (this.Camera != null && this.Camera.Banner == true)
			//{
				//oh += 25;
			//}
			int wh = ((int)(oh * 100 * ratio)) / 100;

			this.Width = ww;
			this.cameraWindow.Height = wh;
			if (!this.HeightFixed)
			{
				int h;
				if (this.panelCameraViewEdit.Visible)
				{
					h = wh + this.toolStripRegion.Height + this.panelTrackBars.Height;
				}
				else
				{
					h = wh;
				}
				this.Height = h + toolStripMain.Height + cwStatusStrip.Height + panelMaster.CaptionHeight;
			}
		}

		public void UpdateUI()
		{
			this.panelMaster.CaptionText = mCameraClass.Name;
			this.trackBarSensi.Value = mCameraClass.Sensibility;
			this.trackBarElapse.Value = mCameraClass.AlarmElapse;
			this.trackBarDifferenceThreshold.Value = mCameraClass.DifferenceThreshold;
			this.ShowBanner = mCameraClass.Banner;
			this.CaptureFlag = mCameraClass.Capture;
			this.ShowMotionRect = mCameraClass.ShowMotionRect;
			this.DetectMode = mCameraClass.DetectMode;
			
			this.toolStripButtonRegion.Checked = mCameraClass.ShowRegion;
			this.toolStripButtonMirror.Checked = mCameraClass.Mirror;
			this.toolStripButtonFlip.Checked = mCameraClass.Flip;

			this.mPTZControl.PlugIn = mCameraClass.PlugInPTZ;
		}

		public void UpdateUIWithFrame()
		{
			if (this.mHasFrame != this.toolStripButtonSnag.Enabled)
			{
				this.toolStripButtonSnag.Enabled = this.mHasFrame;
			}
			if (this.mHasFrame != this.toolStripButtonRegionEdit.Enabled)
			{
				this.toolStripButtonRegionEdit.Enabled = this.mHasFrame;
				if (this.toolStripButtonRegionEdit.Enabled == false)
				{
					this.toolStripButtonRegionEdit.Checked = false;
				}
			}
			if (this.mHasFrame != this.toolStripButtonPause.Enabled)
			{
				if (this.toolStripButtonRegionEdit.Checked == false)
				{
					this.toolStripButtonPause.Enabled = this.mHasFrame;
					if (this.toolStripButtonPause.Enabled == false)
					{
						this.toolStripButtonPause.Checked = false;
					}
				}
			}
		}
		#endregion

		// Open video source
		private void OpenVideoSource(IVideoSource source)
		{
			// set busy cursor
			this.Cursor = Cursors.WaitCursor;

			// close previous file
			CloseVideoSource();

			source.VideoSourceError += new VideoSourceErrorEventHandler(source_VideoSourceError);
			// create camera
			Camera camera = new Camera(source, mMotionDetector);
			camera.CameraClass = this.CameraClass;
			if (camera.MotionDetector is ObjectMotionDetector)
			{
				ObjectMotionDetector m = camera.MotionDetector as ObjectMotionDetector;
				m.MinObjectDimension = (int)this.CameraClass.Sensibility;
				m.MotionZone = this.CameraClass.CameraRegions;
			}

			// start camera
			camera.Start();

			// attach camera to camera window
			cameraWindow.Camera = camera;

			// reset statistics
			mStat.Reset();

			// set event handlers
			camera.NewFrame += new AForge.Video.NewFrameEventHandler(camera_NewFrame);
			camera.Alarm += new EventHandler(camera_Alarm);

			// start timer
			timer.Start();

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
				Application.DoEvents();

				// signal camera to stop
				camera.SignalToStop();
				// wait for the camera
				// camera.WaitForStop();
				// wait 5 seconds until camera stops
				for (int i = 0; (i < 50) && (camera.IsRunning); i++)
				{
					Thread.Sleep(100);
				}
				if (camera.IsRunning)
				{
					try
					{
						camera.Stop();
					}
					catch (Exception)
					{
					}
				}

				camera = null;

				if (mMotionDetector != null)
					mMotionDetector.Reset();
			}

			this.StopCapture();
		}

		#region Camera Events
		// On alarm
		private void camera_Alarm(object sender, System.EventArgs e)
		{
			if (this.EditMode)
			{
				return;
			}
			//mAlarmIsRunning = true;
			//this.timer.Enabled = false;
			mMutexAlarm.WaitOne();
			if (false == this.mFireAlarmEvent)
			{
				this.mFireAlarmEvent = (0 >= this.mIntervalsToSave) ? true : false;
			}
			// save movie for 5 seconds after motion stops
			int elapse = (this.CameraClass != null) ? (int)(this.CameraClass.AlarmElapse) : 5;
			this.mIntervalsToSave = (int)(elapse * (1000 / timer.Interval));
			mMutexAlarm.ReleaseMutex();
			//mAlarmIsRunning = false;
		}

		// On new frame
		private void camera_NewFrame(object sender, System.EventArgs e)
		{
			this.mHasFrame = true;
			if (this.EditMode == true || this.CameraClass == null || (this.CameraClass.Capture == CAPTUREFLAG.NOCAPTURE))
			{
				return;
			}

			mMutexCapture.WaitOne();
			if ((this.mIntervalsToSave > 0) || (this.CameraClass.Capture == CAPTUREFLAG.ALWAYS))
			{
				// lets save the frame
				if (mAVIWriter == null && MotionConfiguration.Instance.StorageIsValid)
				{
					// create file name
					Motion.Core.DateTimeEx date = new Motion.Core.DateTimeEx();
					String fileName = String.Format("{0}.{1}.{2}.avi", this.CameraClass.ID, (int)RecordMark.INVALID, date.TimeStamp);
					try
					{
						// create AVI writer
						mAVIWriter = new AVIWriterEx(this.CameraClass.Codec);
						// open AVI file
						mAVIWriter.Open(MotionConfiguration.Instance.StorageAVI + @"\" + fileName, cameraWindow.Camera.Width, cameraWindow.Camera.Height);

						DateTime n = DateTime.Now;
						string s = "" + n.Hour + ":" + n.Minute + ":" + n.Second;
						this.toolStripStatusLabelCapture.Text = string.Format(Translator.Instance.T("录像开始 ({0})"), s);
						this.CameraClass.Capturing = true;
						this.OnCameraViewRecordStarted(new CameraViewCaptureEventArgs(this));

						mCaptureElapse = date.TimeStamp;
					}
					catch (Exception)
					{
						if (mAVIWriter != null)
						{
							mAVIWriter.Dispose();
							mAVIWriter = null;
						}
						this.toolStripStatusLabelCapture.Text = Translator.Instance.T("录像失败");
						this.CameraClass.Capturing = false;
						this.mCaptureElapse = 0;
						mMutexCapture.ReleaseMutex();
						return;
					}
				}

				Camera camera = cameraWindow.Camera;
				if (camera != null && mAVIWriter != null)
				{
					Bitmap b = camera.LastFrame;
					if (b != null)
					{
						try
						{
							if (this.ShowBanner)
							{
								Bitmap bmp = CameraBanner.Render(b, this.CameraClass.Name, true);
								mAVIWriter.AddFrame(bmp);
							}
							else
							{
								mAVIWriter.AddFrame(b);
							}
						}
						catch (Exception)
						{
							this.toolStripStatusLabelCapture.Text = Translator.Instance.T("录像失败");
							//OnCameraViewLog(new CameraViewLogEventArgs(Motion.Core.LogLevel.LOG_ERROR, ex.Message, this));
							//MessageBox.Show(ex.Message + "\n" + ex.StackTrace.ToString(), this.Camera.Name);
						}
					}
				}
			}
			mMutexCapture.ReleaseMutex();
		}

		private void source_VideoSourceError(object sender, VideoSourceErrorEventArgs e)
		{
			if (this.EditMode)
			{
				return;
			}
			OnCameraViewLog(new CameraViewLogEventArgs(ZForge.Controls.Logs.LogLevel.LOG_ERROR, e.Description, this));
		}
		#endregion

		#region PlugIn Events
		private void PlugIn_Instance_Log(object sender, ZForge.Controls.Logs.LogEventArgs e)
		{
			OnCameraViewLog(new CameraViewLogEventArgs(e.Level, e.Message, this));
		}

		private void PlugIn_Instance_FeedImage(object sender, EventArgs e)
		{
			IPlugIn p = sender as IPlugIn;
			if (p == null || p.Enabled == false)
			{
				return;
			}
			IPlugInFeed f = sender as IPlugInFeed;
			if (f != null)
			{
				f.Image = null;
				Camera camera = cameraWindow.Camera;
				if (camera != null)
				{
					Bitmap lastFrame = camera.LastFrame;
					if (lastFrame != null)
					{
						Bitmap bmp = CameraBanner.Render(lastFrame, this.CameraClass.Name, false);
						f.Image = bmp;
					}
				}
			}
		}
		#endregion

		// On timer event - gather statistic
		private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			Camera camera = cameraWindow.Camera;
			if (camera != null)
			{
				// get number of frames for the last second
				mStat.Push(camera.FramesReceived);
				this.toolStripStatusLabelFps.Text = mStat.FPS.ToString("F2") + " fps";
			}
			this.UpdateUIWithFrame();
			if (this.EditMode)
			{
				return;
			}
			mMutexAlarm.WaitOne();
			if (this.mFireAlarmEvent)
			{
				this.AlarmCount = this.AlarmCount + 1;
				this.CameraClass.PlugIns.AlarmAll(true);
				this.mFireAlarmEvent = false;
			}

			if (this.CameraClass != null && this.CameraClass.CaptureElapse > 0 && this.CameraClass.Capturing == true)
			{
				Motion.Core.DateTimeEx date = new Motion.Core.DateTimeEx();
				if ((date.TimeStamp - mCaptureElapse) / 3600 >= this.CameraClass.CaptureElapse)
				{
					this.StopAlarm();
					this.StopCapture();
					mMutexAlarm.ReleaseMutex();
					return;
				}
			}
			// descrease save counter
			if (mIntervalsToSave > 0)
			{
				mIntervalsToSave--;
				if (mIntervalsToSave <= 0)
				{
					this.StopAlarm();
					if (this.CaptureFlag != CAPTUREFLAG.ALWAYS)
					{
						this.StopCapture();
					}
				}
			}
			mMutexAlarm.ReleaseMutex();
		}

		private void StopAlarm()
		{
			this.CameraClass.PlugIns.AlarmAll(false);
		}

		private void StopCapture()
		{
			mMutexCapture.WaitOne();
			mCaptureElapse = 0;
			this.mIntervalsToSave = 0;
			if (mAVIWriter != null)
			{
				try
				{
					FileInfo fi = new FileInfo(mAVIWriter.FileName);
					mAVIWriter.Dispose();
					mAVIWriter = null;

					this.CaptureCount++;

					DateTime n = DateTime.Now;
					string s = "" + n.Hour + ":" + n.Minute + ":" + n.Second;
					this.toolStripStatusLabelCapture.Text = string.Format(Translator.Instance.T("录像结束({0})"), s);

					this.CameraClass.Capturing = false;

					AVIClass avi = new AVIClass(fi.FullName, this.CameraClass);
					avi.Mark = RecordMark.UNREAD;

					this.OnCameraViewRecordFinished(new CameraViewCaptureEventArgs(this));
				}
				catch (Exception)
				{
				}
			}
			mMutexCapture.ReleaseMutex();
		}

		#region Top Toolbar Events
		private void toolStripButtonStart_Click(object sender, EventArgs e)
		{
			Start();
		}

		private void toolStripButtonStop_Click(object sender, EventArgs e)
		{
			Stop();
		}

		private void toolStripButtonClose_Click(object sender, EventArgs e)
		{
			this.Remove();
		}

		private void toolStripButtonPause_CheckedChanged(object sender, EventArgs e)
		{
			this.SetStatus(Motion.Core.CameraStatus.PAUSED, toolStripButtonPause.Checked);
		}

		private void toolStripButtonKit_Click(object sender, EventArgs e)
		{
			this.ShowEdit = this.toolStripButtonKit.Checked;
			this.SetDimension(this.ViewRatio);
		}

		private void toolStripButtonRegion_CheckedChanged(object sender, EventArgs e)
		{
			this.cameraWindow.ShowRegion = toolStripButtonRegion.Checked;
			if (this.CameraClass != null)
			{
				this.CameraClass.ShowRegion = toolStripButtonRegion.Checked;
			}
		}

		private void toolStripButtonAlarmClean_Click(object sender, EventArgs e)
		{
			this.AlarmCount = 0;
		}

		private void toolStripButtonBanner_CheckStateChanged(object sender, EventArgs e)
		{
			this.ShowBanner = toolStripButtonBanner.Checked;
		}

		private void toolStripButtonSnag_Click(object sender, EventArgs e)
		{
			Bitmap b = this.cameraWindow.CurrentFrame;
			if (b != null)
			{
				CameraSnagForm f = new CameraSnagForm();
				f.Camera = this.CameraClass;
				if (this.ShowBanner)
				{
					b = CameraBanner.Render(b, this.CameraClass.Name, false);
				}
				f.Bitmap = b;
				this.SnagCount++;

				f.Show();
			}
			else
			{
				MessageBox.Show(Translator.Instance.T("当前窗口中尚无图像, 无法完成截图操作."), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void toolStripMenuItemCaptureNone_Click(object sender, EventArgs e)
		{
			this.CaptureFlag = CAPTUREFLAG.NOCAPTURE;
		}

		private void toolStripMenuItemCaptureOnAlarm_Click(object sender, EventArgs e)
		{
			this.CaptureFlag = CAPTUREFLAG.ONALARM;
		}

		private void toolStripMenuItemCaptureAlways_Click(object sender, EventArgs e)
		{
			this.CaptureFlag = CAPTUREFLAG.ALWAYS;
		}

		private void toolStripButtonShowMotionRect_CheckStateChanged(object sender, EventArgs e)
		{
			this.ShowMotionRect = toolStripButtonShowMotionRect.Checked;
		}

		private void toolStripMenuItemDetectModeMotion_Click(object sender, EventArgs e)
		{
			this.DetectMode = DETECTMODE.MOTION;
		}

		private void toolStripMenuItemDetectModeStillness_Click(object sender, EventArgs e)
		{
			this.DetectMode = DETECTMODE.STILLNESS;
		}

		private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
		{
			float[] ratios = new float[] { 0.5F, 0.6F, 0.7F, 0.8F, 0.9F, 1.0F, 1.2F, 1.5F, 2.0F };
			int n = 0;
			foreach (float f in ratios)
			{
				n ++;
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

		private void toolStripButtonMirror_CheckedChanged(object sender, EventArgs e)
		{
			if (this.CameraClass != null)
			{
				this.CameraClass.Mirror = toolStripButtonMirror.Checked;
			}
			this.mPTZControl.Mirror = toolStripButtonMirror.Checked;
		}

		private void toolStripButtonFlip_CheckedChanged(object sender, EventArgs e)
		{
			if (this.CameraClass != null)
			{
				this.CameraClass.Flip = toolStripButtonFlip.Checked;
			}
			this.mPTZControl.Flip = toolStripButtonFlip.Checked;
		}

		#endregion

		private void SetStatus(int s, bool set)
		{
			if (set)
			{
				this.Status = this.Status | s;
			}
			else
			{
				this.Status = this.Status & (~s);
			}
		}

		#region Region Toolbar Events
		private void toolStripButtonRegionEdit_CheckedChanged(object sender, EventArgs e)
		{
			this.Pause(toolStripButtonRegionEdit.Checked);
			this.toolStripButtonPause.Enabled = !toolStripButtonRegionEdit.Checked;
			this.toolStripButtonRegionClear.Enabled = toolStripButtonRegionEdit.Checked;
			this.toolStripButtonRegionReverse.Enabled = toolStripButtonRegionEdit.Checked;
			this.cameraWindow.RegionEditing = toolStripButtonRegionEdit.Checked;
			this.cameraWindow.Banner = (toolStripButtonRegionEdit.Checked) ? false : this.CameraClass.Banner;
			this.toolStripButtonBanner.Enabled = !toolStripButtonRegionEdit.Checked;
		}

		private void toolStripComboBoxResolution_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.cameraWindow.GridSize = (this.toolStripComboBoxResolution.SelectedIndex + 1) * 10;
		}

		private void toolStripButtonRegionClear_Click(object sender, EventArgs e)
		{
			string msg = Translator.Instance.T("您确实需要清除掉目前所有的区域设定吗?");
			if (DialogResult.Yes == MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
			{
				this.cameraWindow.ClearRegion();
			}
		}

		private void toolStripButtonRegionReverse_Click(object sender, EventArgs e)
		{
			this.cameraWindow.ReverseRegion();
		}
		#endregion

		#region Region Trackbar Events

		private void trackBarElapse_ValueChanged(object sender, EventArgs e)
		{
			if (this.CameraClass != null)
			{
				this.CameraClass.AlarmElapse = this.trackBarElapse.Value;
			}
		}

		private void trackBarSensi_ValueChanged(object sender, EventArgs e)
		{
			this.Sensibility = this.trackBarSensi.Value;
		}

		private void trackBarDifferenceThreshold_ValueChanged(object sender, EventArgs e)
		{
			this.DifferenceThreshold = this.trackBarDifferenceThreshold.Value;
		}

		#endregion

		#region IGlobalization Members

		public void UpdateCulture()
		{
			this.trackBarSensi.Title = Translator.Instance.T("灵敏度");
			this.trackBarElapse.Title = Translator.Instance.T("侦测延时");
			this.trackBarDifferenceThreshold.Title = Translator.Instance.T("差异阀值");
			this.toolStripButtonRegionEdit.Text = Translator.Instance.T("区域编辑");
			this.toolStripButtonRegionEdit.ToolTipText = Translator.Instance.T("区域编辑在摄像头启动后生效");
			this.toolStripButtonRegionClear.Text = Translator.Instance.T("取消区域设定");
			this.toolStripButtonRegionReverse.Text = Translator.Instance.T("区域反选");
			this.toolStripLabelGridSize.Text = Translator.Instance.T("网格大小:");
			this.toolStripButtonStart.Text = Translator.Instance.T("启动");
			this.toolStripButtonPause.Text = Translator.Instance.T("暂停");
			this.toolStripButtonStop.Text = Translator.Instance.T("停止");
			this.toolStripButtonClose.Text = Translator.Instance.T("关闭");
			this.toolStripButtonClose.ToolTipText = Translator.Instance.T("关闭监控窗口");
			this.toolStripMenuItemDetectModeMotion.Text = Translator.Instance.T("动作监测");
			this.toolStripMenuItemDetectModeStillness.Text = Translator.Instance.T("静止监测");
			this.toolStripButtonKit.Text = Translator.Instance.T("配置");
			this.toolStripButtonSnag.Text = Translator.Instance.T("截屏");
			this.toolStripButtonSnag.ToolTipText = Translator.Instance.T("截屏 (截屏在摄像头启动后生效)");
			this.toolStripMenuItemCaptureNone.Text = Translator.Instance.T("不录像");
			this.toolStripMenuItemCaptureOnAlarm.Text = Translator.Instance.T("报警时录像");
			this.toolStripMenuItemCaptureAlways.Text = Translator.Instance.T("持续录像");
			this.toolStripButtonAlarmClean.Text = Translator.Instance.T("重置报警记数");
			this.toolStripButtonRegion.Text = Translator.Instance.T("显示监控区域");
			this.toolStripButtonShowMotionRect.Text = Translator.Instance.T("显示动作识别框");
			this.toolStripButtonBanner.Text = Translator.Instance.T("添加图像标签");

			this.toolStripButtonZoomIn.Text = Translator.Instance.T("放大");
			this.toolStripButtonZoomIn.ToolTipText = Translator.Instance.T("放大");
			this.toolStripButtonZoomOut.Text = Translator.Instance.T("缩小");
			this.toolStripButtonZoomOut.ToolTipText = Translator.Instance.T("缩小");

			this.Status = this.Status;
			this.AlarmCount = this.AlarmCount;

			if (this.CameraClass != null)
			{
				Translator.Instance.Update(this.CameraClass);
			}
		}

		#endregion

	}
}
