using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZForge.Controls.PropertyGridEx;
using System.Collections;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.IO;
using ZForge.Motion.PlugIns;
using ZForge.Globalization;
using ZForge.Motion.Util;
using ZForge.Win32;

namespace ZForge.Motion.Forms
{
	public partial class PerferenceForm : Form, IGlobalization
	{
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemStore;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemFontName;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemFontSize;
		private ZForge.Motion.PlugIns.MotionPlugIns mPlugIns = new ZForge.Motion.PlugIns.MotionPlugIns();
		private bool mGlobalSettingsChanged = false;

		public PerferenceForm()
		{
			InitializeComponent();
			InitPropGrid();

			this.DialogResult = DialogResult.Cancel;
			MotionPreference.Instance.UpdateUI(this);
		}

		public bool GlobalSettingsChanged
		{
			get { return mGlobalSettingsChanged; }
		}

		private void InitPropGrid()
		{
			this.pGridPerf.Items.Clear();

			this.mItemStore = new CustomProperty(Translator.Instance.T("存储目录"), MotionConfiguration.Instance.Storage, false, Translator.Instance.T("配置"), Translator.Instance.T("设置存储目录。所有的录像、截图都将存放在该目录下面"), true);
			this.mItemStore.UseFilePathEditor = true;
			this.pGridPerf.Items.Add(this.mItemStore);

			this.mItemFontName = new CustomProperty(Translator.Instance.T("字体"), FontHelper.GetFontFaceName(MotionConfiguration.Instance.FontName), false, Translator.Instance.T("外观"), Translator.Instance.T("设置窗口字体"), true);
			this.mItemFontName.ValueMember = "Key";
			this.mItemFontName.DisplayMember = "Value";
			this.mItemFontName.Datasource = FontHelper.FontList;
			this.pGridPerf.Items.Add(this.mItemFontName);

			float[] slist = new float[] { 4F, 5F, 6F, 7F, 8F, 8.5F, 9F, 9.5F, 10F, 10.5F, 11F, 12F, 13F };
			ArrayList ls = new ArrayList();
			for (int i = 0; i < slist.Length; i++)
			{
				ls.Add(slist[i]);
			}
			this.mItemFontSize = new CustomProperty(Translator.Instance.T("字体大小"), MotionConfiguration.Instance.FontSize, false, Translator.Instance.T("外观"), Translator.Instance.T("设置窗口字体大小"), true);
			this.mItemFontSize.Choices = new CustomChoices(ls);
			this.pGridPerf.Items.Add(this.mItemFontSize);
			this.mPlugIns.PropertyGridItemsInitialization(this.pGridPerf.Items, true);

			this.pGridPerf.Refresh();
			this.pGridPerf.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pGridPerf_PropertyValueChanged);
		}

		private bool ValidCheck(bool showMsgBox)
		{
			List<string> msgs = new List<string>();
			mPlugIns.ValidateAll(msgs, true);
			if (msgs.Count > 0 && showMsgBox)
			{
				MessageBox.Show(ZForge.Configuration.Validator.MergeMessages(msgs, Translator.Instance.T("使用偏好设置中存在下列错误:")), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			return (msgs.Count == 0);
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (this.ValidCheck(true) == false)
			{
				return;
			}

			this.mPlugIns.SaveGlobalConfig();
			MotionConfiguration.Instance.Storage = this.mItemStore.Value as string;
			MotionConfiguration.Instance.FontName = this.mItemFontName.SelectedValue as string;
			MotionConfiguration.Instance.FontSize = Convert.ToSingle(this.mItemFontSize.Value.ToString());
			MotionConfiguration.Instance.Save();

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void pGridPerf_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (e.ChangedItem.Label.Equals(this.mItemStore.Name))
			{
				string v = (string)e.ChangedItem.Value;
				if (v == null || v.Length == 0) 
				{
					MessageBox.Show(Translator.Instance.T("非法的存储目录, 请选择合适的存储目录."), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.mItemStore.Value = MotionConfiguration.Instance.Storage;
					return;
				}
				try
				{
					DirectoryInfo di = new DirectoryInfo(v);
					if (false == di.Exists)
					{
						string msg = string.Format(Translator.Instance.T("您选择的存储目录({0})不存在! 现在创建它吗?"), di.FullName);
						if (DialogResult.Yes == MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
						{
							di.Create();
							this.mItemStore.Value = di.FullName;
						}
						else
						{
							this.mItemStore.Value = MotionConfiguration.Instance.Storage;
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(Translator.Instance.T("设置存储目录失败.") + "\n" + ex.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.mItemStore.Value = MotionConfiguration.Instance.Storage;
				}
			}
			else if (e.ChangedItem.Label.Equals(this.mItemFontName.Name))
			{
			}
			else if (e.ChangedItem.Label.Equals(this.mItemFontSize.Name))
			{
			}
			else
			{
				if (this.mPlugIns.PropertyGridItemsSetValue(e))
				{
					this.mGlobalSettingsChanged = true;
				}
			}
			this.pGridPerf.Refresh();
		}

		#region IGlobalization Members

		public void UpdateCulture()
		{
			this.buttonCancel.Text = Translator.Instance.T("取消");
			this.buttonOk.Text = Translator.Instance.T("确认");
			this.Text = string.Format(Translator.Instance.T("{0} 使用偏好"), MotionPreference.Instance.ProductFullName);
		}

		#endregion
	}
}