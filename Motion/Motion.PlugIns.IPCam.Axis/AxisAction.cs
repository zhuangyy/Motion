using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Controls.PropertyGridEx;
using ZForge.Configuration;
using System.Windows.Forms;
using ZForge.Globalization;
using ZForge.Motion.Util;
using ZForge.Motion.PlugIns;

namespace Motion.PlugIns.IPCam.Axis
{
	public class AxisAction : Motion.PlugIns.IPCam.General.GeneralAction
	{
		protected string mHost;
		protected int mPort;
		protected string mResolution;
		protected int mCamID;

		protected ZForge.Controls.PropertyGridEx.CustomProperty mItemHost;
		protected ZForge.Controls.PropertyGridEx.CustomProperty mItemPort;
		protected ZForge.Controls.PropertyGridEx.CustomProperty mItemCamID;
		protected ZForge.Controls.PropertyGridEx.CustomProperty mItemResolution;

		#region Properties

		public string Host
		{
			get
			{
				if (mHost == null)
				{
					mHost = "";
				}
				return mHost;
			}
			set
			{
				mHost = value;
			}
		}

		public int Port
		{
			get
			{
				if (mPort < 1 || mPort >65535)
				{
					mPort = 80;
				}
				return mPort;
			}
			set
			{
				mPort = value;
			}
		}

		public int CamID
		{
			get
			{
				if (mCamID > 4 || mCamID < 0)
				{
					mCamID = 0;
				}
				return mCamID;
			}
			set
			{
				mCamID = value;
			}
		}

		public string Resolution
		{
			get
			{
				if (mResolution == null || mResolution.Length == 0)
				{
					mResolution = "320x240";
				}
				return mResolution;
			}
			set
			{
				mResolution = value;
			}
		}
		#endregion

		#region IPlugIn Members

		public override string ID
		{
			get { return "p532c301e3373bcd1b7a0b63e9367e5d1"; }
		}

		public override string Name
		{
			get { return Translator.Instance.T("Axis网络摄像头插件"); }
		}

		public override string Description
		{
			get { return Translator.Instance.T("Axis网络摄像头插件, 支持Axis Communication的摄像头"); }
		}

		public override string Author
		{
			get { return "Alexx Joe"; }
		}

		public override string Version
		{
			get { return "2.0.0"; }
		}

		public override List<ZForge.Controls.Logs.ChangeLogItem> ChangeLogList
		{
			get
			{
				List<ZForge.Controls.Logs.ChangeLogItem> r = new List<ZForge.Controls.Logs.ChangeLogItem>();
				r.Add(new ZForge.Controls.Logs.ChangeLogItem("2.0.0", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("升级到插件框架2.0.")));
				r.Add(new ZForge.Controls.Logs.ChangeLogItem("1.0.1", ZForge.Controls.Logs.ChangeLogLevel.ADD, string.Format(Translator.Instance.T("[{0}]公开发布."), this.Name)));
				return r;
			}
		}

		public override bool ValidCheck(List<string> msgs)
		{
			bool r = true;
			r = r && Validator.ValidateString(msgs, Translator.Instance.T("Axis摄像头地址"), this.Host);
			r = r && Validator.ValidateInt(msgs, Translator.Instance.T("Axis摄像头端口"), this.Port.ToString(), 1, 65535);
			r = r && Validator.ValidateInt(msgs, Translator.Instance.T("Axis摄像头ID"), this.CamID.ToString(), 0, 4);
			r = r && base.ValidCheck(msgs);
			return r;
		}
		#endregion

		#region IPlugInCam Members

		public override string LabelText
		{
			get { return Translator.Instance.T("网络摄像头 (Axis)"); }
		}

		#endregion

		#region IPlugInIPCam Members

		public override string URL
		{
			get
			{
				string cgi = (this.Stream == IPCAM.JPEG) ? "image" : "video";
				string path = (this.Stream == IPCAM.JPEG) ? "jpg" : "mjpg";
				mURL = string.Format("http://{0}:{1}/axis-cgi/{2}/{3}.cgi?resolution={4}",
					this.Host, this.Port, path, cgi, this.Resolution);
				if (this.CamID != 0)
				{
					mURL += "&camera=" + this.CamID;
				}
				return mURL;
			}
			set
			{
				mURL = value;
			}
		}

		#endregion

		#region IPlugInUI Members

		public override List<ZForge.Controls.PropertyGridEx.CustomProperty> UIPropertyItems
		{
			get
			{
				if (this.mItemList == null)
				{
					this.mItemList = base.UIPropertyItems;
					this.mItemURL.IsReadOnly = true;

					mItemHost = new CustomProperty(Translator.Instance.T("Axis摄像头地址"), this.Host, false, this.CatText, Translator.Instance.T("设置Axis网络摄像头的网络地址, 可以是主机名或者IP地址."), true);
					mItemList.Add(mItemHost);
					
					mItemPort = new CustomProperty(Translator.Instance.T("Axis摄像头端口"), this.Port.ToString(), false, this.CatText, Translator.Instance.T("设置Axis网络摄像头的网络端口, 通常是80."), true);
					mItemList.Add(mItemPort);
					
					mItemCamID = new CustomProperty(Translator.Instance.T("Axis摄像头ID"), this.CamID.ToString(), false, this.CatText, Translator.Instance.T("设置Axis网络摄像头的ID, 用于带有多个摄像头的Axis服务器, 否则请选择0."), true);
					string[] ids = new string[] { "0", "1", "2", "3", "4" };
					mItemCamID.Choices = new CustomChoices(ids);
					mItemList.Add(mItemCamID);
					
					mItemResolution = new CustomProperty(Translator.Instance.T("Axis摄像头分辨率"), this.Resolution, false, this.CatText, Translator.Instance.T("设置Axis网络摄像头的图像分辨率. 分辨率越大, 图像越清晰, 但是传输速度也越慢."), true);
					string[] rls = new string[] { "320x240", "480x360", "640x480", "VGA", "768x576" };
					//"768x576", "4CIF", "704x576", "704x480", "VGA", "640x480", "2CIFEXP", "2CIF", "704x288", "704x240", "480x360", "CIF", "384x288", "352x288", "352x240", ,
					mItemResolution.Choices = new CustomChoices(rls);
					mItemList.Add(mItemResolution);

					foreach (ZForge.Controls.PropertyGridEx.CustomProperty i in mItemList)
					{
						i.Tag = this.ID;
					}
				}
				return this.mItemList;
			}
		}

		public override bool UISetValue(System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			bool r = true;
			List<string> msgs = new List<string>();
			string v = "";
			if (e.ChangedItem.Label.Equals(mItemHost.Name))
			{
				v = (string)e.ChangedItem.Value;
				r = Validator.ValidateString(msgs, this.mItemHost.Name, v);
				if (r)
				{
					this.Host = v;
					this.mItemURL.Value = this.URL;
				}
			}
			else if (e.ChangedItem.Label.Equals(mItemPort.Name))
			{
				v = (string)e.ChangedItem.Value;
				r = Validator.ValidateInt(msgs, this.mItemPort.Name, v, 1, 65535);
				if (r)
				{
					this.Port = Convert.ToInt32(v);
					if (this.Host.Length != 0)
					{
						this.mItemURL.Value = this.URL;
					}
				}
			}
			else if (e.ChangedItem.Label.Equals(mItemCamID.Name))
			{
				v = (string)e.ChangedItem.Value;
				r = Validator.ValidateInt(msgs, this.mItemCamID.Name, v, 0, 4);
				if (r)
				{
					this.CamID = Convert.ToInt32(v);
					if (this.Host.Length != 0)
					{
						this.mItemURL.Value = this.URL;
					}
				}
			}
			else if (e.ChangedItem.Label.Equals(mItemResolution.Name))
			{
				v = (string)e.ChangedItem.Value;
				this.Resolution = v;
				if (this.Host.Length != 0)
				{
					this.mItemURL.Value = this.URL;
				}
			}
			else
			{
				r = base.UISetValue(e);
				if (r && e.ChangedItem.Label.Equals(mItemStream.Name))
				{
					if (this.Host.Length != 0)
					{
						this.mItemURL.Value = this.URL;
					}
				}
				return r;
			}
			if (r == false)
			{
				MessageBox.Show(Validator.MergeMessages(msgs), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return r;
		}
		#endregion

		#region IConfigurable Members

		public override void LoadConfig(IConfigSetting section)
		{
			base.LoadConfig(section);

			IConfigSetting i = section[this.ID];
			this.Host = i["host"].Value;
			this.Port = i["port"].intValue;
			this.CamID = i["cameraid"].intValue;
			this.Resolution = i["resolution"].Value;
		}

		public override void SaveConfig(IConfigSetting section)
		{
			base.SaveConfig(section);

			IConfigSetting i = section[this.ID];
			i["host"].Value = this.Host;
			i["port"].intValue = this.Port;
			i["cameraid"].intValue = this.CamID;
			i["resolution"].Value = this.Resolution;
		}

		#endregion
	}
}
