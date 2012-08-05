using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Globalization;
using ZForge.Configuration;
using ZForge.Motion.PlugIns;
using ZForge.Controls.PropertyGridEx;
using System.Windows.Forms;
using ZForge.Motion.Util;

namespace Motion.PlugIns.IPCam.UCI
{
	public class UCIAction : Motion.PlugIns.IPCam.General.GeneralAction, IPlugInPTZ
	{
		protected string mHost;
		protected int mPort;
		protected int mCamID;

		protected ZForge.Controls.PropertyGridEx.CustomProperty mItemHost;
		protected ZForge.Controls.PropertyGridEx.CustomProperty mItemPort;
		protected ZForge.Controls.PropertyGridEx.CustomProperty mItemCamID;

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
				if (mPort < 1 || mPort > 65535)
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
				if (mCamID < 1)
				{
					mCamID = 1;
				}
				return mCamID;
			}
			set
			{
				mCamID = value;
			}
		}
		#endregion

		#region IPlugIn Members

		public override string ID
		{
			get { return "p24c610238c45177f55dbd012eef7cbb8"; }
		}

		public override string Name
		{
			get { return Translator.Instance.T("UCI网络摄像头插件"); }
		}

		public override string Description
		{
			get { return Translator.Instance.T("UCI网络摄像头插件, 支持NetONE UCI服务接口"); }
		}

		public override string Author
		{
			get { return "Alexx Joe"; }
		}

		public override string Version
		{
			get { return "1.0.1"; }
		}

		public override List<ZForge.Controls.Logs.ChangeLogItem> ChangeLogList
		{
			get
			{
				List<ZForge.Controls.Logs.ChangeLogItem> r = new List<ZForge.Controls.Logs.ChangeLogItem>();
				r.Add(new ZForge.Controls.Logs.ChangeLogItem("1.0.1", ZForge.Controls.Logs.ChangeLogLevel.ADD, string.Format(Translator.Instance.T("[{0}]公开发布."), this.Name)));
				return r;
			}
		}

		public override bool ValidCheck(List<string> msgs)
		{
			bool r = true;
			r = r && Validator.ValidateString(msgs, Translator.Instance.T("UCI服务地址"), this.Host);
			r = r && Validator.ValidateInt(msgs, Translator.Instance.T("UCI服务端口"), this.Port.ToString(), 1, 65535);
			r = r && Validator.ValidateInt(msgs, Translator.Instance.T("UCI摄像头ID"), this.CamID.ToString(), 1, 65535);
			r = r && base.ValidCheck(msgs);
			return r;
		}
		#endregion

		#region IPlugInCam Members

		public override string LabelText
		{
			get { return Translator.Instance.T("网络摄像头 (UCI)"); }
		}

		#endregion

		#region IPlugInIPCam Members

		public override string URL
		{
			get
			{
				string cgi = (this.Stream == IPCAM.JPEG) ? "image" : "stream";
				mURL = string.Format("http://{0}:{1}/{2}.cgi?", this.Host, this.Port, cgi);
				mURL += "cam=" + this.CamID;
				if (string.IsNullOrEmpty(this.Username) == false)
				{
					mURL += "&username=" + this.Username;
				}
				if (string.IsNullOrEmpty(this.Password) == false)
				{
					mURL += "&password=" + this.Password;
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

					mItemHost = new CustomProperty(Translator.Instance.T("UCI服务地址"), this.Host, false, this.CatText, Translator.Instance.T("设置UCI服务的网络地址, 可以是主机名或者IP地址."), true);
					mItemList.Add(mItemHost);

					mItemPort = new CustomProperty(Translator.Instance.T("UCI服务端口"), this.Port.ToString(), false, this.CatText, Translator.Instance.T("设置UCI服务的网络端口, 通常是80."), true);
					mItemList.Add(mItemPort);

					mItemCamID = new CustomProperty(Translator.Instance.T("UCI摄像头ID"), this.CamID.ToString(), false, this.CatText, Translator.Instance.T("设置UCI摄像头的ID."), true);
					string[] ids = new string[] { "1", "2", "3", "4", "5", "6", "7", "8" };
					mItemCamID.Choices = new CustomChoices(ids);
					mItemList.Add(mItemCamID);

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
				r = Validator.ValidateInt(msgs, this.mItemCamID.Name, v, 1, 65535);
				if (r)
				{
					this.CamID = Convert.ToInt32(v);
					if (this.Host.Length != 0)
					{
						this.mItemURL.Value = this.URL;
					}
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
		}

		public override void SaveConfig(IConfigSetting section)
		{
			base.SaveConfig(section);

			IConfigSetting i = section[this.ID];
			i["host"].Value = this.Host;
			i["port"].intValue = this.Port;
			i["cameraid"].intValue = this.CamID;
		}

		#endregion

		#region IPlugInPTZ Members

		public PTZAction P
		{
			get { return new UCIPTZActionP(this); }
		}

		public PTZAction T
		{
			get { return new UCIPTZActionT(this); }
		}

		public PTZAction Z
		{
			get { return null; }
		}

		#endregion
	}
}
