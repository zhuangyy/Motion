using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ZForge.Win32.DirectShow;
using ZForge.Win32.DirectShow.Core;
using System.Collections;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using ZForge.Motion.PlugIns;
using ZForge.Configuration;
using System.Threading;
using ZForge.Globalization;
using ZForge.Motion.Util;
using ZForge.PlugIn;

namespace ZForge.Motion.Core
{
	public class CameraClass : ItemClass, IConfigurable, IGlobalization, ICloneable
	{
		private string mStream;

		private CAPTUREFLAG mCapture = CAPTUREFLAG.NOCAPTURE;
		private bool mCapturing = false;
		private int mCaptureElapse = 0;

		private decimal mSensi = 15;
		private decimal mAlarmElapse = 5;
		private decimal mDifferenceThreshold = 15;
		private CameraRegions mRegions;
		private string mCodec;
		private bool mBanner = false;
		private bool mShowMotionRect = true;
		private bool mShowRegion = false;
		private Motion.PlugIns.MotionPlugIns mPlugIns = null;
		private DETECTMODE mDetectMode = DETECTMODE.MOTION;

		private bool mMirror = false;
		private bool mFlip = false;

		public CameraClass()
		{
		}

		public CameraClass(IConfigSetting section)
		{
			this.LoadConfig(section);
		}

		public CameraClass(IConfigSetting section, GroupClass parent)
		{
			this.LoadConfig(section);
			this.Group = parent;
		}

		#region Override

		protected override string IDPrefix
		{
			get
			{
				return "c.";
			}
		}

		public override void Clean()
		{
			foreach (AVIClass c in this.AVIs)
			{
				if (false == c.Remove())
				{
					return;
				}
			}
			foreach (PICClass p in this.AVIs)
			{
				if (false == p.Remove())
				{
					return;
				}
			}
		}

		#endregion

		#region Event

		public event ItemValueChangedEventHandler ItemValueChanged;

		public void FireItemValueChangedEvent()
		{
			if (this.ItemValueChanged != null)
			{
				this.ItemValueChanged(this, null);
			}
		}

		#endregion

		#region Properties

		public Motion.PlugIns.MotionPlugIns PlugIns
		{
			get
			{
				if (this.mPlugIns == null)
				{
					this.mPlugIns = new Motion.PlugIns.MotionPlugIns();
				}
				return this.mPlugIns;
			}
		}

		public IPlugInVideoSource PlugInVideoSource
		{
			get
			{
				foreach (AvailablePlugIn<IPlugIn> p in this.PlugIns.AvailablePlugInCollection)
				{
					IPlugInVideoSource i = p.Instance as IPlugInVideoSource;
					if (i != null)
					{
						if (p.Instance.ID.Equals(this.Stream))
						{
							return i;
						}
					}
				}
				return null;
			}
		}

		public IPlugInPTZ PlugInPTZ
		{
			get
			{
				IPlugInVideoSource i = this.PlugInVideoSource;
				if (i == null)
				{
					return null;
				}
				return (i as IPlugInPTZ);
			}
		}

		public AForge.Video.IVideoSource VideoSource
		{
			get
			{
				IPlugInVideoSource i = this.PlugInVideoSource;
				if (i != null)
				{
					return i.VideoSource;
				}
				return null;
			}
		}

		public ArrayList VideoSourceCollection
		{
			get
			{
				ArrayList r = new ArrayList();
				foreach (AvailablePlugIn<IPlugIn> p in this.PlugIns.AvailablePlugInCollection)
				{
					IPlugInVideoSource i = p.Instance as IPlugInVideoSource;
					if (i != null)
					{
						KeyValuePair<string, string> v = new KeyValuePair<string, string>(p.Instance.ID, i.LabelText);
						r.Add(v);
					}
				}
				return r;
			}
		}

		public string Stream
		{
			get
			{
				if (string.IsNullOrEmpty(mStream))
				{
					mStream = "pe5087ee4b071dafd9744d6201dbe65b8";
				}
				return mStream;
			}
			set
			{
				mStream = value;
			}
		}

		public string Codec
		{
			get
			{
				if (this.mCodec == null)
				{
					CodecCollection cc = new CodecCollection();
					string[] prefers = new string[] { "divx", "mp43", "mp42", "mp41", "wmv3" };
					foreach (string s in prefers)
					{
						if (cc.ContainsKey(s))
						{
							this.mCodec = s;
							break;
						}
					}
				}
				return this.mCodec;
			}
			set
			{
				CodecCollection cc = new CodecCollection();
				if (value != null && cc.ContainsKey(value))
				{
					this.mCodec = value;
				}
			}
		}

		public CAPTUREFLAG Capture
		{
			get
			{
				return this.mCapture;
			}
			set
			{
				this.mCapture = value;
			}
		}

		public int CaptureElapse
		{
			get
			{
				return this.mCaptureElapse;
			}
			set
			{
				this.mCaptureElapse = value;
				if (this.mCaptureElapse < 0)
				{
					this.mCaptureElapse = 0;
				}
			}
		}

		public bool Capturing
		{
			get
			{
				return this.mCapturing;
			}
			set
			{
				this.mCapturing = value;
			}
		}

		public decimal Sensibility
		{
			get
			{
				return this.mSensi;
			}
			set
			{
				if (value < ValueRange.SensibilityMin || value > ValueRange.SensibilityMax)
				{
					value = ValueRange.SensibilityDefault;
				}
				this.mSensi = value;
			}
		}

		public decimal AlarmElapse
		{
			get
			{
				return this.mAlarmElapse;
			}
			set
			{
				if (value < ValueRange.AlarmElapseMin || value > ValueRange.AlarmElapseMax)
				{
					value = ValueRange.AlarmElapseDefault;
				}
				this.mAlarmElapse = value;
			}
		}

		public decimal DifferenceThreshold
		{
			get { return this.mDifferenceThreshold; }
			set
			{
				if (value < ValueRange.DifferenceThresholdMin || value > ValueRange.DifferenceThresholdMax)
				{
					value = ValueRange.DifferenceThresholdDefault;
				}
				mDifferenceThreshold = value;
			}
		}

		public CameraRegions CameraRegions
		{
			get
			{
				if (this.mRegions == null)
				{
					this.mRegions = new CameraRegions();
				}
				return this.mRegions;
			}
		}

		public ArrayList AVIs
		{
			get
			{
				ArrayList r = new ArrayList();
				string[] files = System.IO.Directory.GetFiles(MotionConfiguration.Instance.StorageAVI, this.ID + ".*.avi");
				foreach (string file in files)
				{
					AVIClass n = new AVIClass(file, this);
					if (n.IsValid())
					{
						r.Add(n);
					}
				}
				return r;
			}
		}

		public ArrayList PICs
		{
			get
			{
				ArrayList r = new ArrayList();
				string[] files = System.IO.Directory.GetFiles(MotionConfiguration.Instance.StoragePIC, this.ID + ".*.png");
				foreach (string file in files)
				{
					PICClass n = new PICClass(file, this);
					if (n.IsValid())
					{
						r.Add(n);
					}
				}
				files = System.IO.Directory.GetFiles(MotionConfiguration.Instance.StoragePIC, this.ID + ".*.jpg");
				foreach (string file in files)
				{
					PICClass n = new PICClass(file, this);
					if (n.IsValid())
					{
						r.Add(n);
					}
				}
				return r;
			}
		}

		public bool Banner
		{
			get { return this.mBanner; }
			set { this.mBanner = value; }
		}

		public bool ShowMotionRect
		{
			get { return this.mShowMotionRect; }
			set { this.mShowMotionRect = value; }
		}

		public bool ShowRegion
		{
			get { return this.mShowRegion; }
			set { this.mShowRegion = value; }
		}

		public DETECTMODE DetectMode
		{
			get { return mDetectMode; }
			set { mDetectMode = value; }
		}

		public bool Flip
		{
			get
			{
				return this.mFlip;
			}
			set
			{
				this.mFlip = value;
			}
		}

		public bool Mirror
		{
			get
			{
				return this.mMirror;
			}
			set
			{
				this.mMirror = value;
			}
		}
		#endregion

		public bool ValidCheck(bool showMsgBox)
		{
			List<string> msgs = new List<string>();
			if (this.Name == null || this.Name.Equals(""))
			{
				msgs.Add(Translator.Instance.T("请指定摄像头的名称."));
			}
			if (this.Capture != CAPTUREFLAG.NOCAPTURE && (this.Codec == null || this.Codec.Equals("")))
			{
				msgs.Add(Translator.Instance.T("请指定录像编码格式."));
			}
			foreach (AvailablePlugIn<IPlugIn> p in this.PlugIns.AvailablePlugInCollection)
			{
				IPlugInVideoSource i = p.Instance as IPlugInVideoSource;
				if (i != null && i != this.PlugInVideoSource)
				{
					continue;
				}
				p.Instance.ValidCheck(msgs);
			}
			if (msgs.Count > 0 && showMsgBox)
			{
				MessageBox.Show(Validator.MergeMessages(msgs, string.Format(Translator.Instance.T("摄像头({0})配置中存在下列错误:"), this.FullName)), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			return (msgs.Count == 0);
		}

		public void ReloadGlobalSettings()
		{
			this.PlugIns.LoadGlobalConfig();
		}

		#region IGlobalization Members

		public void UpdateCulture()
		{
			this.PlugIns.UpdateCultureAll();
		}

		#endregion

		#region IConfigurable Members

		public void SaveConfig(IConfigSetting i)
		{
			i.RemoveChildren();

			i["version"].floatValue = 2.0F;
			i["id"].Value = this.ID;
			i["name"].Value = this.Name;
			i["stream"].Value = this.Stream;
			i["capture"].intValue = (int)this.Capture;
			i["sensi"].intValue = (int)this.Sensibility;
			i["alarmelapse"].intValue = (int)this.AlarmElapse;
			i["differencethreshold"].intValue = (int)this.DifferenceThreshold;
			i["codec"].Value = this.Codec;
			i["banner"].boolValue = this.Banner;
			i["showmotionrect"].boolValue = this.ShowMotionRect;
			i["showregion"].boolValue = this.ShowRegion;
			i["captureelapse"].intValue = this.CaptureElapse;
			i["detectmode"].intValue = (int)this.DetectMode;
			i["mirror"].boolValue = this.Mirror;
			i["flip"].boolValue = this.Flip;

			try
			{
				byte[] bs = this.CameraRegions.Serialize();
				i["regions"].Value = Convert.ToBase64String(bs);
			}
			catch (Exception) { }

			IConfigSetting s = i["plugins"];
			this.PlugIns.SaveConfigAll(s);
		}

		public void LoadConfig(IConfigSetting i)
		{
			this.PlugIns.LoadConfigAll(i["plugins"]);

			float version = i["version"].floatValue;
			if (version < 1.1F)
			{
				this.Capture = (i["capture"].boolValue == true) ? CAPTUREFLAG.ONALARM : CAPTUREFLAG.NOCAPTURE;
				this.ShowMotionRect = true;
			}
			else
			{
				this.Capture = (CAPTUREFLAG)(i["capture"].intValue);
				this.ShowMotionRect = i["showmotionrect"].boolValue;
			}
			this.Stream = i["stream"].Value;
			if (version < 1.2F)
			{
				this.Stream = (i["stream"].intValue == 0) ? null : "Motion.PlugIns.IPCam.General";
				foreach (AvailablePlugIn<IPlugIn> pi in this.PlugIns.AvailablePlugInCollection)
				{
					IPlugInIPCam c = pi.Instance as IPlugInIPCam;
					if (c != null)
					{
						c.URL = i["url"].Value;
						c.Username = i["login"].Value;
						c.Password = i["password"].Value;
						c.Stream = (i["stream"].intValue == 1) ? IPCAM.JPEG : IPCAM.MJPEG;
					}
				}
			}
			if (version < 2.0F)
			{
				if (this.Stream != null)
				{
					Dictionary<string, string> gs = new Dictionary<string, string>();
					gs.Add("motion.plugins.alarm.email", "p17a988de71b3fb81d5cc2d6612dfe072");
					gs.Add("motion.plugins.alarm.sound", "p4f2679987e0e6e85cf70ca40feaac595");
					gs.Add("motion.plugins.ipcam.general", "p1f25f05340a17e6e6acdf656d3360ea0");
					gs.Add("motion.plugins.ipcam.axis", "p532c301e3373bcd1b7a0b63e9367e5d1");
					gs.Add("motion.plugins.usbcam.general", "pe5087ee4b071dafd9744d6201dbe65b8");
					string k = this.Stream.ToLower();
					this.Stream = gs.ContainsKey(k) ? gs[k] : null;
				}
			}

			this.ID = i["id"].Value;
			this.Name = i["name"].Value;
			this.Sensibility = (decimal)i["sensi"].intValue;
			this.AlarmElapse = (decimal)i["alarmelapse"].intValue;
			this.DifferenceThreshold = i["differencethreshold"].intValue;
			this.Banner = i["banner"].boolValue;
			this.CaptureElapse = i["captureelapse"].intValue;
			this.ShowRegion = i["showregion"].boolValue;
			this.DetectMode = (DETECTMODE)i["detectmode"].intValue;
			this.Codec = i["codec"].Value;
			this.Mirror = i["mirror"].boolValue;
			this.Flip = i["flip"].boolValue;

			try
			{
				byte[] bs = Convert.FromBase64String(i["regions"].Value);
				this.CameraRegions.Unserialize(bs);
			}
			catch (Exception) { }
		}

		#endregion

		public void Copy(CameraClass c)
		{
			XMLConfig x = new XMLConfig();
			this.SaveConfig(x.Settings["temp"]);
			c.LoadConfig(x.Settings["temp"]);
		}

		#region ICloneable Members

		public object Clone()
		{
			CameraClass c = new CameraClass();
			this.Copy(c);
			return c;
		}

		#endregion
	}
}
