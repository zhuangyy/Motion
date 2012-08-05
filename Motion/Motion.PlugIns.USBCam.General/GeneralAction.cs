using System;
using System.Collections.Generic;
using System.Text;
using ZForge.Win32.DirectShow;
using ZForge.Win32.DirectShow.Core;
using AForge.Video.DirectShow;
using ZForge.Configuration;
using System.Windows.Forms;
using ZForge.Controls.PropertyGridEx;
using ZForge.Globalization;
using ZForge.Controls.Logs;
using ZForge.Motion.Util;
using ZForge.Motion.PlugIns;

namespace Motion.PlugIns.USBCam.General
{
	public class GeneralAction : IPlugInUSBCam, IPlugInUI
	{
		protected List<ZForge.Controls.PropertyGridEx.CustomProperty> mItemList = null;
		protected ZForge.Controls.PropertyGridEx.CustomProperty mItemDevice;
		protected string mDevice;

		public string Device
		{
			get
			{
				if (mDevice == null)
				{
					return "";
				}
				return mDevice;
			}
			set
			{
				mDevice = value.Trim();
			}
		}

		public System.Collections.ArrayList DeviceCollection
		{
			get
			{
				System.Collections.ArrayList r = new System.Collections.ArrayList();
				FilterCollection filters;
				try
				{
					filters = new FilterCollection(ZForge.Win32.DirectShow.Core.FilterCategory.VideoInputDevice);
					foreach (Filter filter in filters)
					{
						r.Add(filter.Name);
					}
				}
				catch (ApplicationException)
				{
				}
				return r;
			}
		}

		#region IPlugInUSBCam Members

		public ZForge.Win32.DirectShow.Filter Filter
		{
			get
			{
				FilterCollection filters;
				try
				{
					filters = new FilterCollection(ZForge.Win32.DirectShow.Core.FilterCategory.VideoInputDevice);
					foreach (Filter filter in filters)
					{
						if (filter.Name.Equals(this.Device))
						{
							return filter;
						}
					}
				}
				catch (ApplicationException)
				{
				}
				return null;
			}
		}

		#endregion

		#region IPlugIn Members

		public string Name
		{
			get { return Translator.Instance.T("USB摄像头通用插件"); }
		}

		public string Description
		{
			get { return Translator.Instance.T("USB摄像头通用插件"); }
		}

		public string Author
		{
			get { return "Alexx Joe"; }
		}

		public string Version
		{
			get { return "2.0.0"; }
		}

		public virtual string ID
		{
			get { return "pe5087ee4b071dafd9744d6201dbe65b8"; }
		}

		public bool Enabled
		{
			get { return (this.Filter != null); }
		}

		public void Release()
		{
			return;
		}

		public void Dispose()
		{
			return;
		}

		public bool ValidCheck(List<string> msgs)
		{
			if (this.Filter == null)
			{
				string s = string.Format(Translator.Instance.T("[{0}]设置错误, 选择的本地摄像设备不存在."), this.LabelText);
				msgs.Add(s);
				return false;
			}
			return true;
		}

		public List<ZForge.Controls.Logs.ChangeLogItem> ChangeLogList
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

		public string LabelText
		{
			get { return Translator.Instance.T("本地摄像头 (通用)"); }
		}

		public AForge.Video.IVideoSource VideoSource
		{
			get
			{
				AForge.Video.IVideoSource r = null;
				if (this.Filter != null)
				{
					VideoCaptureDevice localSource = new VideoCaptureDevice();
					localSource.Source = this.Filter.MonikerString;
					r = localSource;
				}
				return r;
			}
		}

		#endregion

		#region IPlugInUI Members

		public List<ZForge.Controls.PropertyGridEx.CustomProperty> UIPropertyItems
		{
			get
			{
				if (this.mItemList == null)
				{
					mItemList = new List<ZForge.Controls.PropertyGridEx.CustomProperty>();

					this.mItemDevice = new CustomProperty(Translator.Instance.T("本地摄像头"), this.Device, false, Translator.Instance.T("摄像头设置"), Translator.Instance.T("选择本地摄像头"), true);
					mItemList.Add(mItemDevice);
					foreach (ZForge.Controls.PropertyGridEx.CustomProperty i in mItemList)
					{
						i.Tag = this.ID;
					}
				}
				this.mItemDevice.Choices = new CustomChoices(this.DeviceCollection);
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

		public bool UISetValue(System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			string v = (string)e.ChangedItem.Value;
			if (e.ChangedItem.Label.Equals(mItemDevice.Name))
			{
				this.Device = v;
			}
			return true;
		}

		#endregion

		#region IConfigurable Members

		public void SaveConfig(IConfigSetting section)
		{
			IConfigSetting i = section[this.ID];
			i.RemoveChildren();
			i["device"].Value = this.Device;
		}

		public void LoadConfig(IConfigSetting section)
		{
			IConfigSetting i = section[this.ID];
			this.Device = i["device"].Value;
		}

		#endregion
	}
}
