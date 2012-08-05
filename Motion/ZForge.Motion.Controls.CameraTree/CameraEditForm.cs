using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZForge.Controls.PropertyGridEx;
using ZForge.Win32.DirectShow;
using ZForge.Win32.DirectShow.Core;
using ZForge.Motion.Core;
using ZForge.Motion.PlugIns;
using ZForge.Motion.Util;
using ZForge.Globalization;

namespace ZForge.Motion.Controls
{
	public partial class CameraEditForm : Form, IGlobalization
	{
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemName;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemStream;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemCodec;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemCaptureFlag;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemCaptureElapse;

		CameraClass mCameraClass;

		public CameraEditForm(CameraClass m)
		{
			InitializeComponent();
			MotionPreference.Instance.UpdateUI(this);

			mCameraClass = m;
			this.cameraViewTest.CameraClass = m;
			this.cameraViewTest.EditMode = true;

			InitPropGrid();
			this.DialogResult = DialogResult.Cancel;
			this.Camera.PlugIns.ToolStripItemsInitialization(this.toolStripEdit.Items);
		}

		public CameraClass Camera
		{
			get
			{
				return mCameraClass;
			}
		}

		private void InitPropGrid()
		{
			MotionArgs Args = new MotionArgs();
			this.pGridCamera.Items.Clear();

			this.mItemName = new CustomProperty(Translator.Instance.T("摄像头名称"), this.Camera.Name, false, Translator.Instance.T("摄像头设置"), Translator.Instance.T("为本摄像头分配一个名称"), true);
			this.pGridCamera.Items.Add(this.mItemName);

			string sv = "";
			if (this.Camera.PlugInVideoSource != null)
			{
				sv = this.Camera.PlugInVideoSource.LabelText;
			}
			this.mItemStream = new CustomProperty(Translator.Instance.T("视频来源"), sv, false, Translator.Instance.T("摄像头设置"), Translator.Instance.T("设置视频来源"), true);
			this.mItemStream.ValueMember = "Key";
			this.mItemStream.DisplayMember = "Value";
			this.mItemStream.Datasource = this.Camera.VideoSourceCollection;
			//this.mItemStream.SelectedValue = this.Camera.Stream;
			this.pGridCamera.Items.Add(this.mItemStream);

			this.mItemCaptureFlag = new CustomProperty(Translator.Instance.T("录像方式"), Args.GetValue(this.Camera.Capture), false, Translator.Instance.T("录像设置"), Translator.Instance.T("设置录像方式"), true);
			this.mItemCaptureFlag.ValueMember = "Key";
			this.mItemCaptureFlag.DisplayMember = "Value";
			this.mItemCaptureFlag.Datasource = Args.CAPTUREFLAGS;
			//this.mItemCaptureFlag.SelectedValue = this.Camera.Capture;
			this.pGridCamera.Items.Add(this.mItemCaptureFlag);

			this.mItemCaptureElapse = new CustomProperty(Translator.Instance.T("存储方式"), Args.GetValue(this.Camera.CaptureElapse), false, Translator.Instance.T("录像设置"), Translator.Instance.T("设置录像存储的方式, 你可以选择将录像按照指定的时间段分割存储, 或者连续存储."), true);
			this.mItemCaptureElapse.ValueMember = "Key";
			this.mItemCaptureElapse.DisplayMember = "Value";
			this.mItemCaptureElapse.Datasource = Args.CAPTURE_ELAPSE;
			//this.mItemCaptureElapse.SelectedValue = this.Camera.CaptureElapse;
			this.pGridCamera.Items.Add(this.mItemCaptureElapse);

			CodecCollection cc = new CodecCollection();
			if (cc.KVs.Count > 0)
			{
				string codec = cc.GetValue(this.Camera.Codec, Translator.Instance.T("请指定录像编码格式."));
				this.mItemCodec = new CustomProperty(Translator.Instance.T("录像编码格式"), codec, false, Translator.Instance.T("录像设置"), Translator.Instance.T("设置录像采用的编码格式"), true);
				this.mItemCodec.ValueMember = "Key";
				this.mItemCodec.DisplayMember = "Value";
				this.mItemCodec.Datasource = cc.KVs;
				this.mItemCodec.IsDropdownResizable = true;
				//this.mItemCodec.SelectedValue = this.Camera.Codec;
			}
			else
			{
				this.mItemCodec = new CustomProperty(Translator.Instance.T("录像编码格式"), Translator.Instance.T("未找到合适的录像编码格式."), false, Translator.Instance.T("录像设置"), Translator.Instance.T("未找到合适的录像编码格式, 您需要安装视频编码驱动."), true);
				this.mItemCodec.IsReadOnly = true;
			}
			this.pGridCamera.Items.Add(this.mItemCodec);
			this.Camera.PlugIns.PropertyGridItemsInitialization(this.pGridCamera.Items);

			this.SetVideoSourceItemVisible(true);
			this.pGridCamera.Refresh();
		}

		private void pGridCamera_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (e.ChangedItem.Label.Equals(this.mItemStream.Name))
			{
				SetVideoSourceItemVisible(false);
				this.Camera.Stream = (string)mItemStream.SelectedValue;
				SetVideoSourceItemVisible(true);
				this.pGridCamera.Refresh();
			}
			else if (e.ChangedItem.Label.Equals(this.mItemName.Name))
			{
				this.Camera.Name = (string)e.ChangedItem.Value;
			}
			else if (e.ChangedItem.Label.Equals(this.mItemCodec.Name))
			{
				this.Camera.Codec = (string)this.mItemCodec.SelectedValue;
			}
			else if (e.ChangedItem.Label.Equals(this.mItemCaptureFlag.Name))
			{
				this.Camera.Capture = (CAPTUREFLAG)this.mItemCaptureFlag.SelectedValue;
			}
			else if (e.ChangedItem.Label.Equals(this.mItemCaptureElapse.Name))
			{
				this.Camera.CaptureElapse = (int)this.mItemCaptureElapse.SelectedValue;
			}
			else
			{
				this.Camera.PlugIns.PropertyGridItemsSetValue(e);
			}
			this.pGridCamera.Refresh();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (this.Camera.ValidCheck(true))
			{
				this.DialogResult = DialogResult.OK;
				if (this.cameraViewTest.IsRunning())
				{
					this.cameraViewTest.Stop();
				}
				this.Camera.PlugIns.ReleaseAll();
				this.Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			if (this.cameraViewTest.IsRunning())
			{
				this.cameraViewTest.Stop();
			}
			this.Camera.PlugIns.ReleaseAll();
			this.Close();
		}

		private void cameraViewTest_CameraViewBeforeStart(object sender, CameraViewBeforeStartEventArgs e)
		{
			e.Cancel = this.Camera.ValidCheck(true) ? false : true;
			if (!e.Cancel)
			{
				this.cameraViewTest.CameraClass = this.Camera;
			}
		}

		private void SetVideoSourceItemVisible(bool v)
		{
			IPlugInVideoSource i = this.Camera.PlugInVideoSource;
			if (i == null)
			{
				return;
			}
			IPlugInUI u = i as IPlugInUI;
			if (u == null)
			{
				return;
			}
			if (u.UIPropertyItems != null)
			{
				foreach (ZForge.Controls.PropertyGridEx.CustomProperty p in u.UIPropertyItems)
				{
					p.Visible = v;
				}
			}
			//this.pGridCamera.Refresh();
		}


		#region IGlobalization Members

		public void UpdateCulture()
		{
			this.btnCancel.Text = Translator.Instance.T("取消");
			this.btnOk.Text = Translator.Instance.T("确定");
			this.Text = string.Format(Translator.Instance.T("{0} 配置摄像头"), MotionPreference.Instance.MessageBoxCaption);
		}

		#endregion
	}
}