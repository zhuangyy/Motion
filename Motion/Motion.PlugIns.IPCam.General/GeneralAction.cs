using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Motion.PlugIns;
using ZForge.Configuration;
using ZForge.Controls.PropertyGridEx;
using System.Windows.Forms;
using AForge.Video;
using ZForge.Globalization;
using ZForge.Motion.Util;

namespace Motion.PlugIns.IPCam.General
{
	public class GeneralAction : IPlugInIPCam, IPlugInUI
	{
		protected string mUsername = "";
		protected string mPassword = "";
		protected string mURL = "";
		protected IPCAM mStream = IPCAM.JPEG;

		protected List<ZForge.Controls.PropertyGridEx.CustomProperty> mItemList = null;
		protected ZForge.Controls.PropertyGridEx.CustomProperty mItemUsername;
		protected ZForge.Controls.PropertyGridEx.CustomProperty mItemPassword;
		protected ZForge.Controls.PropertyGridEx.CustomProperty mItemURL;
		protected ZForge.Controls.PropertyGridEx.CustomProperty mItemStream;

		protected virtual string CatText
		{
			get { return Translator.Instance.T("摄像头设置"); }
		}

		#region IPlugInIPCam Members

		public virtual string URL
		{
			get
			{
				if (null == mURL)
				{
					mURL = "";
				}
				return mURL;
			}
			set
			{
				mURL = value;
			}
		}

		public string Username
		{
			get
			{
				if (null == mUsername)
				{
					mUsername = "";
				}
				return mUsername;
			}
			set
			{
				mUsername = value;
			}
		}

		public string Password
		{
			get
			{
				if (null == mPassword)
				{
					mPassword = "";
				}
				return mPassword;
			}
			set
			{
				mPassword = value;
			}
		}

		public IPCAM Stream
		{
			get { return mStream; }
			set { mStream = value; }
		}
		#endregion

		#region IPlugInUI Members

		public virtual List<ZForge.Controls.PropertyGridEx.CustomProperty> UIPropertyItems
		{
			get
			{
				if (mItemList == null)
				{
					mItemList = new List<ZForge.Controls.PropertyGridEx.CustomProperty>();

					mItemStream = new CustomProperty(Translator.Instance.T("网络摄像头数据类型"), this.Stream, false, this.CatText, Translator.Instance.T("设置网络摄像头的数据类型"), true);
					mItemList.Add(mItemStream);

					mItemURL = new CustomProperty(Translator.Instance.T("网络摄像头URL"), this.URL, false, this.CatText, Translator.Instance.T("设置网络摄像头的访问地址(URL)"), true);
					mItemList.Add(mItemURL);

					mItemUsername = new CustomProperty(Translator.Instance.T("网络摄像头登录用户"), this.Username, false, this.CatText, Translator.Instance.T("设置登录网络摄像头的用户名"), true);
					mItemList.Add(mItemUsername);

					mItemPassword = new CustomProperty(Translator.Instance.T("网络摄像头登录口令"), this.Password, false, this.CatText, Translator.Instance.T("设置登录网络摄像头的用户口令"), true);
					mItemPassword.IsPassword = true;
					mItemList.Add(mItemPassword);

					foreach (ZForge.Controls.PropertyGridEx.CustomProperty i in mItemList)
					{
						i.Tag = this.ID;
					}
				}
				return mItemList;
			}
		}

		public List<System.Windows.Forms.ToolStripItem> UIEditFormToolStripItems
		{
			get { return null; }
		}

		public List<System.Windows.Forms.ToolStripItem> UICameraViewToolStripItems
		{
			get { return null; }
		}

		public virtual bool UISetValue(System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			List<string> msgs = new List<string>();
			bool r = true;
			if (e.ChangedItem.Label.Equals(mItemStream.Name))
			{
				this.Stream = (IPCAM)e.ChangedItem.Value;
			}
			else 
			{
				string v = (string)e.ChangedItem.Value;
				if (e.ChangedItem.Label.Equals(mItemURL.Name) && mItemURL.IsReadOnly == false)
				{
					r = Validator.ValidateURL(msgs, this.mItemURL.Name, v);
					if (r)
					{
						this.URL = v;
					}
				}
				else if (e.ChangedItem.Label.Equals(mItemUsername.Name))
				{
					this.Username = v;
				}
				else if (e.ChangedItem.Label.Equals(mItemPassword.Name))
				{
					this.Password = v;
				}
			}
			if (r == false)
			{
				MessageBox.Show(Validator.MergeMessages(msgs), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return r;
		}

		#endregion

		#region IPlugIn Members

		public virtual string ID
		{
			get { return "p1f25f05340a17e6e6acdf656d3360ea0"; }
		}

		public virtual string Name
		{
			get { return Translator.Instance.T("网络摄像头通用插件"); }
		}

		public virtual string Description
		{
			get { return Translator.Instance.T("网络摄像头插件, 用于配置用户自定义的网络摄像头"); }
		}

		public virtual string Author
		{
			get { return "Alexx Joe"; }
		}

		public virtual string Version
		{
			get { return "2.0.0"; }
		}

		public bool Enabled
		{
			get { return true; }
		}

		public void Release()
		{
			return;
		}

		public void Dispose()
		{
			return;
		}

		public virtual bool ValidCheck(List<string> msgs)
		{
			return Validator.ValidateURL(msgs, Translator.Instance.T("网络摄像头URL"), this.URL);
		}

		public virtual List<ZForge.Controls.Logs.ChangeLogItem> ChangeLogList
		{
			get
			{
				List<ZForge.Controls.Logs.ChangeLogItem> r = new List<ZForge.Controls.Logs.ChangeLogItem>();
				r.Add(new ZForge.Controls.Logs.ChangeLogItem("2.0.0", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("升级到插件框架2.0.")));
				r.Add(new ZForge.Controls.Logs.ChangeLogItem("1.0.1", ZForge.Controls.Logs.ChangeLogLevel.ADD, string.Format(Translator.Instance.T("[{0}]公开发布."), this.Name)));
				return r;
			}
		}

		#endregion

		#region IPlugInCam Members

		public virtual string LabelText
		{
			get { return Translator.Instance.T("网络摄像头 (通用)"); }
		}

		public AForge.Video.IVideoSource VideoSource
		{
			get
			{
				IVideoSource r = null;
				switch (this.Stream)
				{
					case IPCAM.JPEG:
						JPEGStream jpegSource = new JPEGStream();
						jpegSource.Source = this.URL;
						jpegSource.Login = this.Username;
						jpegSource.Password = this.Password;
						r = jpegSource;
						break;
					case IPCAM.MJPEG:
						MJPEGStream mjpegSource = new MJPEGStream();
						mjpegSource.Source = this.URL;
						mjpegSource.Login = this.Username;
						mjpegSource.Password = this.Password;
						//mjpegSource.SeparateConnectionGroup = true;
						r = mjpegSource;
						break;
				}
				return r;
			}
		}

		#endregion

		#region IConfigurable Members

		public virtual void SaveConfig(IConfigSetting section)
		{
			IConfigSetting i = section[this.ID];
			i.RemoveChildren();

			i["username"].Value = this.Username;
			i["password"].Value = this.Password;
			i["url"].Value = this.URL;
			i["stream"].intValue = (int)this.Stream;
		}

		public virtual void LoadConfig(IConfigSetting section)
		{
			IConfigSetting i = section[this.ID];

			this.Username = i["username"].Value;
			this.Password = i["password"].Value;
			this.URL = i["url"].Value;
			this.Stream = (IPCAM)i["stream"].intValue;
		}

		#endregion
	}
}
