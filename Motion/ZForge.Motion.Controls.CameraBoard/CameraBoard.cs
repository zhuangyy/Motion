using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZForge.Motion.Core;
using System.Collections;
using ZForge.Globalization;
using ZForge.Motion.Util;

namespace ZForge.Motion.Controls
{
	public partial class CameraBoard : UserControl, IGlobalization
	{
		private float m_viewratio = 1.0F;
		private CameraBoardStyle mStyle;
		private int mCount = 0;

		public CameraBoard()
		{
			InitializeComponent();
			this.toolStripComboBoxRatio.SelectedIndex = 3;
			this.ViewCount = 0;
		}

		#region My Event
		public event CameraBoardAddNewHandler CameraBoardAddNew;
		protected virtual void OnCameraBoardAddNewEvent(CameraBoardAddNewEventArgs e)
		{
			if (this.CameraBoardAddNew != null)
			{
				CameraBoardAddNew(this, e);
			}
		}
		#endregion

		#region Properties
		public float ViewRatio
		{
			get
			{
				return m_viewratio;
			}
			set
			{
				if (value < 0.5F)
				{
					value = 0.5F;
				}
				m_viewratio = value;
				foreach (Control c in this.CameraPanel.Controls)
				{
					if (c.Visible = true && c is IView)
					{
						((IView)c).ViewRatio = m_viewratio;
					}
				}
			}
		}

		public CameraClass Camera
		{
			set
			{
				switch (this.Style)
				{
					case CameraBoardStyle.AVI:
						this.ClearAll();
						if (value != null)
						{
							foreach (Motion.Core.AVIClass f in value.AVIs)
							{
								this.Add(f);
							}
						}
						break;
					case CameraBoardStyle.PIC:
						this.ClearAll();
						if (value != null)
						{
							foreach (PICClass p in value.PICs)
							{
								this.Add(p);
							}
						}
						break;
				}
			}
		}

		private string ShortCutText
		{
			get
			{
				switch (this.Style)
				{
					case CameraBoardStyle.CAMERA:
						return Translator.Instance.T("摄像头");
					case CameraBoardStyle.AVI:
						return Translator.Instance.T("录像");
					case CameraBoardStyle.PIC:
						return Translator.Instance.T("图片");
					default:
						return "";
				}
			}
		}

		public CameraBoardStyle Style
		{
			get
			{
				return this.mStyle;
			}
			set
			{
				this.mStyle = value;
				switch (this.mStyle)
				{
					case CameraBoardStyle.CAMERA:
						this.toolStripButtonStartAll.Visible = true;
						this.toolStripButtonStopAll.Visible = true;
						this.toolStripButtonPauseAll.Visible = true;
						this.toolStripButtonAlarmCleanAll.Visible = true;
						this.toolStripSeparator1.Visible = true;
						this.toolStripSeparator2.Visible = true;
						this.toolStripButtonExportAll.Visible = false;
						this.toolStripButtonDeleteAll.Visible = false;
						break;
					case CameraBoardStyle.AVI:
						this.toolStripButtonStartAll.Visible = true;
						this.toolStripButtonStopAll.Visible = true;
						this.toolStripButtonPauseAll.Visible = true;
						this.toolStripButtonAlarmCleanAll.Visible = false;
						this.toolStripSeparator1.Visible = false;
						//this.toolStripSeparator2.Visible = false;
						this.toolStripButtonExportAll.Visible = true;
						this.toolStripButtonDeleteAll.Visible = true;
						break;
					case CameraBoardStyle.PIC:
						this.toolStripButtonStartAll.Visible = false;
						this.toolStripButtonStopAll.Visible = false;
						this.toolStripButtonPauseAll.Visible = false;
						this.toolStripButtonAlarmCleanAll.Visible = false;
						this.toolStripSeparator1.Visible = false;
						this.toolStripSeparator2.Visible = false;
						this.toolStripButtonExportAll.Visible = true;
						this.toolStripButtonDeleteAll.Visible = true;
						break;
				}
				this.toolStripLabelShortCut.Text = this.ShortCutText + ":";
			}
		}

		private int ViewCount
		{
			get { return this.mCount; }
			set
			{
				this.mCount = value;
				this.toolStripButtonDeleteAll.Enabled = (this.mCount != 0);
				this.toolStripButtonExportAll.Enabled = (this.mCount != 0);
				this.toolStripLabelShortCut.Text = this.ShortCutText + "(" + this.mCount + "):";
			}
		}
		#endregion

		public IVideoView Find(CameraClass c)
		{
			foreach (Control t in this.CameraPanel.Controls)
			{
				IVideoView v = t as IVideoView;
				if (v != null && v.CameraClass.ID == c.ID)
				{
					return v;
				}
			}
			return null;
		}

		public bool Remove(CameraClass c)
		{
			foreach (Control t in this.CameraPanel.Controls)
			{
				IView v = t as IView;
				if (v != null)
				{
					if (false == v.Owner.ID.Equals(c.ID))
					{
						continue;
					}
					if (v is IVideoView)
					{
						IVideoView x = (IVideoView)v;
						x.Stop();
					}
					this.CameraPanel.Controls.Remove(v.Me);
					this.ShortcutRemove(v);
					break;
				}
			}
			return true;
		}

		public bool Remove(AVIClass c)
		{
			foreach (Control t in this.CameraPanel.Controls)
			{
				AVIView v = t as AVIView;
				if (v != null && v.AVIClass.FileName.Equals(c.FileName))
				{
					v.Stop();
					this.CameraPanel.Controls.Remove(v.Me);
					this.ShortcutRemove(v);
					break;
				}
			}
			return true;
		}

		public bool Remove(PICClass c)
		{
			foreach (Control t in this.CameraPanel.Controls)
			{
				PICView v = t as PICView;
				if (v != null && v.PICClass.FileName.Equals(c.FileName))
				{
					this.CameraPanel.Controls.Remove(v.Me);
					this.ShortcutRemove(v);
					break;
				}
			}
			return true;
		}

		public bool Remove(IFileClass c)
		{
			foreach (Control t in this.CameraPanel.Controls)
			{
				IFileView v = t as IFileView;
				if (v != null && v.FileClass.ID.Equals(c.ID))
				{
					if (t is IVideoView)
					{
						IVideoView x = t as IVideoView;
						x.Stop();
					}
					this.CameraPanel.Controls.Remove(t);
					this.ShortcutRemove(t as IView);
					break;
				}
			}
			return true;
		}

		public IVideoView Add(AVIClass c)
		{
			IVideoView v;

			if (c == null)
			{
				return null;
			}
			v = this.Find(c.Camera);
			if (v == null)
			{
				AVIView x = new AVIView();
				x.AVIClass = c;
				x.VisibleChanged += new EventHandler(this.IView_VisibleChanged);
				this.CameraPanel.Controls.Add(x);
				x.ViewRatio = this.ViewRatio;

				CameraBoardAddNewEventArgs e = new CameraBoardAddNewEventArgs(x);
				this.OnCameraBoardAddNewEvent(e);
				e = null;
				v = x;
				this.ShortcutAdd(v);
			}
			else
			{
				v.ViewRatio = this.ViewRatio;
				v.Me.Visible = true;
			}
			return v;
		}

		public PICView Add(PICClass c)
		{
			if (c == null)
			{
				return null;
			}
			PICView v = new PICView(c);
			v.ViewRatio = this.ViewRatio;
			v.VisibleChanged += new EventHandler(this.IView_VisibleChanged);
			this.CameraPanel.Controls.Add(v);
			this.ShortcutAdd(v);
			return v;
		}

		public IVideoView Add(CameraClass c)
		{
			IVideoView v;

			if (c == null)
			{
				return null;
			}
			v = this.Find(c);
			if (v == null)
			{
				if (this.AtTheEdge() == true)
				{
					return null;
				}
				CameraView x = new CameraView();
				x.CameraClass = c;
				this.CameraPanel.Controls.Add(x);
				x.ViewRatio = this.ViewRatio;
				x.VisibleChanged += new EventHandler(this.IView_VisibleChanged);

				CameraBoardAddNewEventArgs e = new CameraBoardAddNewEventArgs(x);
				this.OnCameraBoardAddNewEvent(e);
				e = null;
				v = x;
				this.ShortcutAdd(v);
			}
			else
			{
				v.ViewRatio = this.ViewRatio;
				v.Me.Visible = true;
			}
			this.ScrollToView(v.CameraClass.ID);
			return v;
		}

		private void IView_VisibleChanged(object sender, EventArgs e)
		{
			IView v = sender as IView;
			if (v != null)
			{
				if (v.Me.Visible == false)
				{
					this.ShortcutRemove(v);
					this.CameraPanel.Controls.Remove(v.Me);
				}
			}
		}

		#region Shortcut functions
		private void toolStripComboBoxShortCut_SelectedIndexChanged(object sender, EventArgs e)
		{
			VideoViewTag tag = (VideoViewTag)this.toolStripComboBoxShortCut.SelectedItem;
			if (tag != null)
			{
				this.ScrollToView(tag.ID);
			}
		}

		private void ShortcutAdd(IView v)
		{
			VideoViewTag tag = new VideoViewTag(v.ID, v.Title);
			foreach (VideoViewTag t in this.toolStripComboBoxShortCut.Items)
			{
				if (t.ID.Equals(tag.ID))
				{
					return;
				}
			}
			this.toolStripComboBoxShortCut.Items.Add(tag);
			this.ViewCount++;
			try
			{
				object o = this.toolStripComboBoxShortCut.SelectedItem;
				ArrayList list = ArrayList.Adapter(this.toolStripComboBoxShortCut.Items);
				list.Sort();
				this.toolStripComboBoxShortCut.SelectedItem = o;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void ShortcutClear()
		{
			this.toolStripComboBoxShortCut.Items.Clear();
			this.ViewCount = 0;
		}

		public void ShortcutRemove(IView v)
		{
			foreach (VideoViewTag t in this.toolStripComboBoxShortCut.Items)
			{
				if (t.ID.Equals(v.ID))
				{
					this.toolStripComboBoxShortCut.Items.Remove(t);
					this.ViewCount--;
					break;
				}
			}
		}
		#endregion

		public void StartAll()
		{
			this.Cursor = Cursors.WaitCursor;
			foreach (Control t in this.CameraPanel.Controls)
			{
				IVideoView v = t as IVideoView;
				if (v != null && (v.IsRunning() == false) && v.Me.Visible)
				{
					try
					{
						v.Start();
					}
					catch (ApplicationException ax)
					{
						MessageBox.Show("[" + v.CameraClass.Name + "]: " + ax.Message);
					}
				}
			}
			this.Cursor = Cursors.Default;
		}

		public void StopAll()
		{
			this.Cursor = Cursors.WaitCursor;
			foreach (Control t in this.CameraPanel.Controls)
			{
				IVideoView v = t as IVideoView;
				if ((v != null) && (v.IsRunning() == true))
				{
					try
					{
						v.Stop();
					}
					catch (ApplicationException ex)
					{
						MessageBox.Show("[" + v.CameraClass.Name + "]: " + ex.Message);
					}
				}
			}
			this.Cursor = Cursors.Default;
		}

		public void PauseAll(bool on)
		{
			foreach (Control t in this.CameraPanel.Controls)
			{
				IVideoView v = t as IVideoView;
				if (v != null)
				{
					v.Pause(on);
				}
			}
		}

		public void ClearAll()
		{
			this.CameraPanel.Controls.Clear();
			this.ShortcutClear();
		}

		public void AlarmResetAll()
		{
			this.Cursor = Cursors.WaitCursor;
			foreach (Control t in this.CameraPanel.Controls)
			{
				if (t is CameraView)
				{
					CameraView v = (CameraView)t;
					v.AlarmCount = 0;
				}
			}
			this.Cursor = Cursors.Default;
		}

		public void ScrollToView(string id)
		{
			foreach (Control t in this.CameraPanel.Controls)
			{
				if (t is IView)
				{
					IView v = (IView)t;
					if (v.ID.Equals(id))
					{
						t.Visible = true;
						this.CameraPanel.ScrollControlIntoView(t);
					}
				}
			}
		}

		private void toolStripComboBoxRatio_SelectedIndexChanged(object sender, EventArgs e)
		{
			//int n = this.toolStripComboBoxRatio.SelectedIndex;
			float f = 1.0F;
			string s = this.toolStripComboBoxRatio.SelectedItem.ToString();
			string v = s.Replace("%", "");
			try
			{
				int n = Convert.ToInt32(v);
				f = n / 100.0F;
			}
			catch (Exception)
			{
			}
			this.ViewRatio = f;
		}

		private void toolStripButtonStartAll_Click(object sender, EventArgs e)
		{
			this.StartAll();
		}

		private void toolStripButtonStopAll_Click(object sender, EventArgs e)
		{
			this.StopAll();
		}

		private void toolStripButtonPauseAll_Click(object sender, EventArgs e)
		{
			this.PauseAll(this.toolStripButtonPauseAll.Checked);
		}

		private void toolStripButtonAlarmCleanAll_Click(object sender, EventArgs e)
		{
			this.AlarmResetAll();
		}

		private void toolStripButtonDeleteAll_Click(object sender, EventArgs e)
		{
			string msg = string.Format(Translator.Instance.T("确定要永久删除这些{0}吗?"), this.ShortCutText);
			if (DialogResult.Yes == MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
			{
				foreach (Control t in this.CameraPanel.Controls)
				{
					if (t is IView)
					{
						IView v = (IView)t;
						if (false == v.Remove())
						{
							break;
						}
					}
				}
			}
		}

		private void toolStripButtonCloseAll_Click(object sender, EventArgs e)
		{
			this.StopAll();
			this.ClearAll();
		}

		private bool AtTheEdge()
		{
      return false;
		}

		private void toolStripButtonExportAll_Click(object sender, EventArgs e)
		{
			bool b = true;
			folderBrowserDialog.Description = string.Format(Translator.Instance.T("请选择{0}导出目录"), this.ShortCutText);
			DialogResult r = folderBrowserDialog.ShowDialog();
			if (DialogResult.OK == r && this.folderBrowserDialog.SelectedPath != null && this.folderBrowserDialog.SelectedPath.Length > 0)
			{
				foreach (Control t in this.CameraPanel.Controls)
				{
					IFileView fv = t as IFileView;
					if (fv != null)
					{
						if (false == fv.FileClass.ExportToPath(this.folderBrowserDialog.SelectedPath))
						{
							b = false;
							break;
						}
					}
				}
				if (b == true)
				{
					string msg = string.Format(Translator.Instance.T("执行完毕!\n导出目录: {0}"), folderBrowserDialog.SelectedPath);
					MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}


		#region IGlobalization Members

		public void UpdateCulture()
		{
			this.toolStripLabelZoom.Text = Translator.Instance.T("缩放");
			this.toolStripLabelShortCut.Text = Translator.Instance.T("摄像头:");
			this.toolStripButtonStartAll.Text = Translator.Instance.T("全部启动");
			this.toolStripButtonPauseAll.Text = Translator.Instance.T("全部暂停");
			this.toolStripButtonStopAll.Text = Translator.Instance.T("全部停止");
			this.toolStripButtonAlarmCleanAll.Text = Translator.Instance.T("重置全部报警记数");
			this.toolStripButtonCloseAll.Text = Translator.Instance.T("全部关闭");
			this.toolStripButtonExportAll.Text = Translator.Instance.T("全部导出");
			this.toolStripButtonDeleteAll.Text = Translator.Instance.T("全部删除");
		}

		#endregion
	}
}
