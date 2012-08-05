using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ZForge.Controls.PropertyGridEx;
using System.IO;
using ZForge.Configuration;
using ZForge.Globalization;
using ZForge.Controls.Logs;
using ZForge.Motion.Util;
using ZForge.Motion.PlugIns;

namespace Motion.PlugIns.Alarm.Sound
{
	public class SoundAction : IPlugInAlarm, IPlugInUIWithGlobal, IGlobalization
	{
		private DirectXPlayer mDxPlayer;
		private ToneEnum mTone = ToneEnum.NONE;
		private string mMP3 = null;
		private bool mMute = false;
		private bool mStopping = false;
		private bool mGlobalOperation = false;
		private bool mUseGlobal = true;

		private List<ZForge.Controls.PropertyGridEx.CustomProperty> mItemList = null;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemUseGlobal;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemTone;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemMp3;

		private System.Windows.Forms.ToolStripButton mToolStripButtonMute = null;
		private System.Windows.Forms.ToolStripButton mToolStripButtonTestAudio = null;

		private ToneEnum Tone
		{
			get { return this.mTone; }
			set
			{
				this.mTone = value;
				if (mToolStripButtonTestAudio != null)
				{
					mToolStripButtonTestAudio.Enabled = (this.Tone != ToneEnum.NONE);
				}
				if (mItemList != null)
				{
					SoundArgs args = new SoundArgs();
					mItemTone.SelectedValue = this.Tone;
					mItemTone.Value = args.GetValue(this.Tone);
					this.SyncUI();
				}
			}
		}

		private DirectXPlayer Player
		{
			get
			{
				if (this.mDxPlayer == null)
				{
					this.mDxPlayer = new DirectXPlayer();
				}
				return this.mDxPlayer;
			}
		}

		private string MP3
		{
			get
			{
				if (this.mMP3 == null)
				{
					return "";
				}
				return this.mMP3;
			}
			set
			{
				this.mMP3 = value;
				if (this.mItemList != null)
				{
					this.mItemMp3.Value = this.MP3;
				}
			}
		}

		private string MP3FileName
		{
			get
			{
				string r = null;
				switch (this.Tone)
				{
					case ToneEnum.MP3:
						r = this.MP3;
						break;
					case ToneEnum.NONE:
						r = null;
						break;
					default:
						string e = Application.ExecutablePath;
						FileInfo ei = new FileInfo(e);
						r = ei.Directory + @"\PlugIns\Sound\" + (int)this.Tone + ".mp3";
						break;
				}
				return r;
			}
		}

		private bool Mute
		{
			get { return this.mMute; }
			set { 
				this.mMute = value;
				if (false == this.mMute)
				{
					this.Stop();
				}
				if (this.mToolStripButtonMute != null)
				{
					this.mToolStripButtonMute.Checked = this.mMute;
				}
			}
		}

		private string CategoryText
		{
			get
			{
				return (this.GlobalOperation) ? Translator.Instance.T("声音报警全局设置") : Translator.Instance.T("声音报警");
			}
		}

		#region IPlugInAlarm Members

		public bool IsRunning
		{
			get { return this.Player.IsRunning; }
		}

		public bool Alarm()
		{
			if (!this.IsRunning && this.Enabled)
			{
				this.Player.FileName = this.MP3FileName;
				this.Player.Play();
				if (this.Log != null)
				{
					Log(this, new LogEventArgs(LogLevel.LOG_INFO, Translator.Instance.T("播放报警声音.")));
				}
			}
			return true;
		}

		public bool Stop()
		{
			//Motion.Util.Tools.PrintStackTrace();

			mStopping = true;
			this.Player.Stop(); 
			if (mToolStripButtonTestAudio != null)
			{
				mToolStripButtonTestAudio.Checked = false;
			}
			mStopping = false;
			return true;
		}
		#endregion

		#region IPlugin Members

		public string ID
		{
			get { return "p4f2679987e0e6e85cf70ca40feaac595"; }
		}

		public string Name
		{
			get { return Translator.Instance.T("声音报警"); }
		}

		public string Description
		{
			get { return Translator.Instance.T("播放预先设置的报警音"); }
		}

		public string Author
		{
			get { return "Alexx Joe"; }
		}

		public string Version
		{
			get { return "2.0.0"; }
		}

		public bool Enabled
		{
			get
			{
				return (this.Tone != ToneEnum.NONE && this.Mute == false);
			}
		}

		public void Dispose()
		{
			if (this.mDxPlayer != null)
			{
				this.Stop();
				this.mDxPlayer.Dispose();
				this.mDxPlayer = null;
			}
		}

		public void Release()
		{
			this.Stop();
		}

		public bool ValidCheck(List<string> msgs)
		{
			if (this.Tone != ToneEnum.NONE)
			{
				this.Player.FileName = this.MP3FileName;
				if (false == this.Player.Check())
				{
					msgs.Add(Translator.Instance.T("报警音设置有错.(指定播放的MP3文件无法播放)."));
					return false;
				}
			}
			return true;
		}

		public List<ZForge.Controls.Logs.ChangeLogItem> ChangeLogList
		{
			get
			{
				List<ZForge.Controls.Logs.ChangeLogItem> r = new List<ZForge.Controls.Logs.ChangeLogItem>();
				r.Add(new ZForge.Controls.Logs.ChangeLogItem("2.0.0", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("升级到插件框架2.0.")));
				r.Add(new ZForge.Controls.Logs.ChangeLogItem("1.0.2", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("支持全局设置.")));
				r.Add(new ZForge.Controls.Logs.ChangeLogItem("1.0.1", ZForge.Controls.Logs.ChangeLogLevel.ADD, string.Format(Translator.Instance.T("[{0}]插件公开发布."), this.Name)));
				return r;
			}
		}

		#endregion

		#region IPluginLog Members

		public event ZForge.Controls.Logs.LogEventHandler Log;

		#endregion

		private void toolStripButtonMute_CheckStateChanged(object sender, EventArgs e)
		{
			this.Mute = mToolStripButtonMute.Checked;
		}

		private void toolStripButtonTestAudio_CheckedChanged(object sender, EventArgs e)
		{
			if (mStopping)
			{
				return;
			}
			if (false == mToolStripButtonTestAudio.Checked)
			{
				this.Stop();
			}
			else
			{
				List<string> msgs = new List<string>();
				if (false == this.ValidCheck(msgs))
				{
					string s = "";
					foreach (string i in msgs) {
						s += i + "\n";
					}
					MessageBox.Show(s, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
					mToolStripButtonTestAudio.Checked = false;
				}
				else
				{
					this.Alarm();
				}
			}
		}

		#region IPlugInUIWithGlobal Members

		public bool GlobalOperation
		{
			get { return this.mGlobalOperation; }
			set
			{
				this.mGlobalOperation = value;
				this.mItemList = null;
				if (this.mGlobalOperation)
				{
					this.UseGlobal = false;
					this.LoadConfig(MotionConfiguration.Instance.PlugInGlobalSettings);
				}
			}
		}

		public bool UseGlobal
		{
			get { return this.mUseGlobal; }
			set
			{
				this.mUseGlobal = value;
				if (this.mUseGlobal)
				{
					this.LoadConfig(MotionConfiguration.Instance.PlugInGlobalSettings);
				}
				if (this.mItemList != null)
				{
					this.mItemUseGlobal.Value = this.UseGlobal;
					this.SyncUI();
				}
			}
		}

		public List<ZForge.Controls.PropertyGridEx.CustomProperty> UIPropertyItems
		{
			get 
			{
				if (mItemList == null)
				{
					SoundArgs Args = new SoundArgs();
					mItemList = new List<ZForge.Controls.PropertyGridEx.CustomProperty>();

					if (this.GlobalOperation == false)
					{
						mItemUseGlobal = new CustomProperty(Translator.Instance.T("是否使用声音报警全局设置"), this.UseGlobal, false, this.CategoryText, Translator.Instance.T("确定是否使用[使用偏好]中的全局设置"), true);
						mItemList.Add(mItemUseGlobal);
					}

					mItemTone = new CustomProperty(Translator.Instance.T("报警音"), Args.GetValue(this.Tone), false, this.CategoryText, Translator.Instance.T("设置报警音"), true);
					mItemTone.ValueMember = "Key";
					mItemTone.DisplayMember = "Value";
					mItemTone.Datasource = Args.TONES;
					mItemList.Add(mItemTone);

					mItemMp3 = new CustomProperty(Translator.Instance.T("报警MP3文件"), this.MP3, false, this.CategoryText, Translator.Instance.T("设置报警时播放的MP3文件"), true);
					mItemMp3.Visible = (this.Tone == ToneEnum.MP3);
					mItemMp3.UseFileNameEditor = true;
					mItemMp3.FileNameDialogType = UIFilenameEditor.FileDialogType.LoadFileDialog;
					mItemMp3.FileNameFilter = Translator.Instance.T("MP3文件") + " (*.mp3)|*.mp3";
					mItemList.Add(mItemMp3);

					foreach (ZForge.Controls.PropertyGridEx.CustomProperty i in mItemList)
					{
						i.Tag = this.ID;
					}

					this.SyncUI();
				}
				return mItemList;
			}
		}

		private void SyncUI()
		{
			if (this.mItemList == null)
			{
				return;
			}

			bool b;
			b = (this.Tone == ToneEnum.MP3);
			mItemMp3.Visible = b;

			b = this.UseGlobal;
			mItemTone.IsReadOnly = b;
			mItemMp3.IsReadOnly = b;
		}

		public List<System.Windows.Forms.ToolStripItem> UIEditFormToolStripItems
		{
			get
			{
				if (null == mToolStripButtonTestAudio)
				{
					// 
					// toolStripButtonTestAudio
					// 
					System.Windows.Forms.ToolStripButton b = new System.Windows.Forms.ToolStripButton();
					b.CheckOnClick = true;
					b.Image = global::Motion.PlugIns.Alarm.Sound.Properties.Resources.loudspeaker;
					b.ImageTransparentColor = System.Drawing.Color.Magenta;
					b.Name = "toolStripButtonTestAudio";
					b.Size = new System.Drawing.Size(88, 22);
					b.Text = Translator.Instance.T("报警音试听");
					b.CheckedChanged += new System.EventHandler(this.toolStripButtonTestAudio_CheckedChanged);
					b.Enabled = (this.Tone != ToneEnum.NONE);

					mToolStripButtonTestAudio = b;
				}
				List<System.Windows.Forms.ToolStripItem> r = new List<ToolStripItem>();
				r.Add(mToolStripButtonTestAudio);
				return r;
			}
		}

		public List<System.Windows.Forms.ToolStripItem> UICameraViewToolStripItems
		{
			get
			{
				if (null == mToolStripButtonMute)
				{
					// 
					// toolStripButtonMute
					//
					mToolStripButtonMute = new System.Windows.Forms.ToolStripButton();
					mToolStripButtonMute.CheckOnClick = true;
					mToolStripButtonMute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
					mToolStripButtonMute.Image = global::Motion.PlugIns.Alarm.Sound.Properties.Resources.mute;
					mToolStripButtonMute.ImageTransparentColor = System.Drawing.Color.Magenta;
					mToolStripButtonMute.Name = "toolStripButtonMute";
					mToolStripButtonMute.Size = new System.Drawing.Size(23, 22);
					mToolStripButtonMute.Text = Translator.Instance.T("静音");
					mToolStripButtonMute.Checked = this.Mute;
					mToolStripButtonMute.CheckStateChanged += new System.EventHandler(this.toolStripButtonMute_CheckStateChanged);
				}
				List<System.Windows.Forms.ToolStripItem> r = new List<ToolStripItem>();
				System.Windows.Forms.ToolStripSeparator s = new System.Windows.Forms.ToolStripSeparator();
				r.Add(s);
				r.Add(mToolStripButtonMute);
				return r;
			}
		}

		public bool UISetValue(PropertyValueChangedEventArgs e)
		{
			bool r = false;
			if (this.mItemUseGlobal != null && e.ChangedItem.Label.Equals(this.mItemUseGlobal.Name))
			{
				this.UseGlobal = (bool)e.ChangedItem.Value;
			}
			else if (e.ChangedItem.Label.Equals(mItemTone.Name))
			{
				if (mToolStripButtonTestAudio != null)
				{
					mToolStripButtonTestAudio.Checked = false;
				}
				this.Tone = (ToneEnum)mItemTone.SelectedValue;
				r = true;
			}
			else if (e.ChangedItem.Label.Equals(mItemMp3.Name))
			{
				if (mToolStripButtonTestAudio != null)
				{
					mToolStripButtonTestAudio.Checked = false;
				}
				this.MP3 = (string)e.ChangedItem.Value;
				r = true;
			}
			return r;
		}

		#endregion

		#region IGlobalization Members

		public void UpdateCulture()
		{
			if (null != mToolStripButtonMute)
			{
				mToolStripButtonMute.Text = Translator.Instance.T("静音");
			}
			// other items do not need to update now
		}

		#endregion

		#region IConfigurable Members

		public void SaveConfig(IConfigSetting section)
		{
			IConfigSetting i = section[this.ID];
			i.RemoveChildren();

			i["tone"].intValue = (int)this.Tone;
			i["mp3"].Value = this.MP3;
			i["mute"].boolValue = this.Mute;

			if (this.GlobalOperation == false)
			{
				i["useglobal"].boolValue = this.UseGlobal;
			}
		}

		public void LoadConfig(IConfigSetting section)
		{
			IConfigSetting i = section[this.ID];
			this.UseGlobal = i["useglobal"].boolValue;
			if (this.UseGlobal == true)
			{
				return;
			}
			this.Tone = (ToneEnum)i["tone"].intValue;
			this.MP3 = i["mp3"].Value;
			this.mMute = i["mute"].boolValue;
		}

		#endregion
	}
}
