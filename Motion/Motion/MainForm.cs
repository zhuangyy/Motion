using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using ZForge.Motion.Controls;
using ZForge.Motion.Core;
using ZForge.Motion.PlugIns;
using ZForge.Motion.Forms;
using ZForge.Motion.Util;
using ZForge.Globalization;
using ZForge.Controls.Logs;
using ZForge.Win32;
using System.Collections.Generic;
using System.IO;
using ZForge.SA.Komponent;

namespace Motion
{
    /// <summary>
    /// Summary description for MainForm
    /// </summary>
	internal partial class MainForm : SAMainForm, IGlobalization
	{
		private ZForge.Motion.Forms.RecordBrowseForm mfPIC;
		private ZForge.Motion.Forms.RecordBrowseForm mfAVI;

		public MainForm()
		{
			this.ApplicationInitialize();

			InitializeComponent();
      this.InitializeKomponentMenu(MotionPreference.Instance);

			this.Text = MotionPreference.Instance.ProductFullName;
			this.toolStripMainLabelStorage.Text = Translator.Instance.T("存储目录:") + " " + MotionConfiguration.Instance.Storage;
			this.InitLanguageMenu();

			MotionPreference.Instance.UpdateUI(this);
			MotionConfiguration.Instance.StorageChanged += new StorageChangedEventHandler(MotionConfiguration_StorageChanged);
			this.toolStripMenuItemSelectLang.Enabled = false;
			
			this.CheckStorage();
			MotionPreference.Instance.CheckUpdate(true);
		}

		private void InitLanguageMenu()
		{
			LanguageFileInfoCollection col = new LanguageFileInfoCollection();
			foreach (LanguageFileInfo fi in col.Values)
			{
				ToolStripMenuItem i = new ToolStripMenuItem(fi.Language);
				i.Tag = fi;
				i.Click += new System.EventHandler(this.toolStripMenuItemLang_Click);
				if (fi.ID == col.CurrentLanguage.ID)
				{
					i.Checked = true;
				}
				this.toolStripMenuItemSelectLang.DropDownItems.Add(i);
			}
		}

		#region SAMainForm overrides

		protected override void ApplicationReset()
		{
			if (this.boardCamera != null)
			{
				this.boardCamera.StopAll();
			}
			if (this.toolStripMenuItemSaveOnExit != null && this.toolStripMenuItemSaveOnExit.Checked)
			{
				RootClass.Instance.SaveConfig();
				MotionConfiguration.Instance.Save();
			}

			if (this.notifyIconTray != null)
			{
				this.notifyIconTray.Visible = false;
				this.notifyIconTray.Icon.Dispose();
				this.notifyIconTray.Dispose();
			}
			base.ApplicationReset();
		}

		public override void Quit(bool quiet)
		{
			if (quiet == false)
			{
				if (DialogResult.Yes != MessageBox.Show(Translator.Instance.T("确定要退出吗? 退出后所有的摄像头将停止监控."), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					return;
				}
			}
			base.Quit(quiet);
		}

		#endregion

		#region CameraView Events
		private void CameraView_CameraViewStatusChanged(object sender, CameraViewStatusChangedEventArgs e)
		{
			this.treeCamera.SetTreeNodeStatus(e.CameraView.CameraClass, e.Status);
		}

		private void CameraView_CameraViewAlarmCountChanged(object sender, CameraViewAlarmCountChangedEventArgs e)
		{
			this.treeCamera.SetTreeNodeAlarmCount(e.CameraView.CameraClass, e.AlarmCount);
		}

		private void CameraView_CameraViewLog(object sender, CameraViewLogEventArgs e)
		{
			this.logViewer.Add(e.LogLevel, e.CameraView, e.Message);
		}
		#endregion

		#region Menu Events

		#region Actions

		private void ActionAbout()
		{
			SAAboutForm fm = new SAAboutForm(MotionPreference.Instance);
			fm.TopicImage = global::Motion.Properties.Resources.videocamera_48;

			List<ChangeLogItem> list = new List<ChangeLogItem>();
      list.Add(new ChangeLogItem("2.0.3", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("彻底取消了许可证限制.")));
      list.Add(new ChangeLogItem("2.0.2", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("修改了获取系统中视频编码器的方式.")));
      list.Add(new ChangeLogItem("2.0.1", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("主模块更新.")));
			list.Add(new ChangeLogItem("2.0.0", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("增加IP Camera UCI (Universal Camera Interface)插件, 支持PTZ.")));
			list.Add(new ChangeLogItem("2.0.0", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("监控窗口增加图像水平和垂直翻转的功能.")));
			list.Add(new ChangeLogItem("2.0.0", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("配置文件的存储方式变更为单个文件存放.")));
			list.Add(new ChangeLogItem("2.0.0", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("增加[主菜单]->[工具]->[导入旧版本(1.x.x)配置文件], 用于导入以往版本的配置信息.")));
			list.Add(new ChangeLogItem("2.0.0", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("内部代码结构大规模的改进, 并纳入到Safe.Anywhere框架中.")));
			list.Add(new ChangeLogItem("1.7.8", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("单个监控窗口/录像播放窗口/截图查看窗口都可以独立地放大和缩小.")));
			list.Add(new ChangeLogItem("1.7.8", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("在监控窗口配置中, 增加了[差异阀值]的选项, 该阀值可以用于设置比较图像差异时的阀值, 这个值越小, 对图像差异的敏感度越高.")));
			list.Add(new ChangeLogItem("1.7.7", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("修复了一个与日志分类显示有关的Bug. 该Bug有可能造成程序死锁.")));
			list.Add(new ChangeLogItem("1.7.7", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("修复了另一个与监控区域的设置和显示有关的Bug.")));
			list.Add(new ChangeLogItem("1.7.7", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("修复由于用户系统缺少视频编码驱动引起异常的Bug.")));
			list.Add(new ChangeLogItem("1.7.6", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("内部字符串全部采用UTF-8编码.")));
			list.Add(new ChangeLogItem("1.7.6", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("界面多语言支持")));
			list.Add(new ChangeLogItem("1.7.5", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("在启用EMail报警, 并且在EMail里包含图片的情况下, Motion Detector有时会死锁(Deadlock), 在这个版本里面修复了这个Bug.")));
			list.Add(new ChangeLogItem("1.7.5", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("使用偏好中增加全局设置.")));
			list.Add(new ChangeLogItem("1.7.5", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("EMail报警插件支持全局设置.")));
			list.Add(new ChangeLogItem("1.7.5", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("声音报警插件支持全局设置.")));
			list.Add(new ChangeLogItem("1.7.4", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("主窗口下方新增工具栏, 用于主菜单的快捷操作.")));
			list.Add(new ChangeLogItem("1.7.4", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("新的录像浏览方式. 录像浏览窗口中的列表数据会自动与系统同步, 录像的新增, 删除后的结果会自动体现在录像浏览窗口的的列表数据中, 也就是说, 您不需要不断地点击[刷新]按钮. 但是需要注意的是, 正在进行中的录像在录像结束后才会自动显示在列表中.")));
			list.Add(new ChangeLogItem("1.7.4", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("新的截图浏览方式. 截图浏览窗口中的列表数据会自动与系统同步, 截图的新增, 删除后的结果会自动体现在截图浏览窗口的的列表数据中, 也就是说, 您不需要不断地点击[刷新]按钮")));
			list.Add(new ChangeLogItem("1.7.4", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("对录像和截图记录支持设置标签, 您可以通过标签来组织录像和截图记录. 目前支持3类标签: 未读, 已读, 重要.")));
			list.Add(new ChangeLogItem("1.7.4", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("录像播放窗口中原先的[删除]按钮变成[关闭]按钮, 用于关闭当前的录像播放窗口. 删除当前录像请使用新增的[删除]按钮.")));
			list.Add(new ChangeLogItem("1.7.4", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("截图查看窗口中原先的[删除]按钮变成[关闭]按钮, 用于关闭当前的截图查看窗口. 删除当前截图请使用新增的[删除]按钮.")));
			list.Add(new ChangeLogItem("1.7.4", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("录像和截图的存储目录与摄像头配置文件存储的目录分开, 在Motion Detector启动时, 会自动检测并更新存储方式. 感谢Naco@CCF提出的建议.")));
			list.Add(new ChangeLogItem("1.7.3", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("Motion Detector的视频来源现在也是插件的形式了, 这样可以方便地支持更多的摄像头, 同时可以充分利用摄像头自身的特性.")));
			list.Add(new ChangeLogItem("1.7.3", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("新增Axis网络摄像头插件. (支持Axis Communication公司的网络摄像头)")));
			list.Add(new ChangeLogItem("1.7.3", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("网络摄像头通用插件发布.")));
			list.Add(new ChangeLogItem("1.7.3", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("本地USB摄像头通用插件发布.")));
			list.Add(new ChangeLogItem("1.7.2", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("监控窗口的[隐藏]按钮变更为[关闭]按钮, 相应的操作也从隐藏当前窗口变更为关闭当前窗口, 这样应该更符合使用习惯.")));
			list.Add(new ChangeLogItem("1.7.2", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("监控窗口的用户界面做了一些微调, 对一些按钮的使用条件做了一些限制, (例如:[暂停], [截图], [区域编辑]必须在摄像头启动, 并接收到图像后才能启用).")));
			list.Add(new ChangeLogItem("1.7.2", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("[配置摄像头]对话框的用户界面做了一些微调.")));
			list.Add(new ChangeLogItem("1.7.2", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("对网络摄像头进行区域编辑有的时候会发生Out of Memory的错误, 在这个版本里面修复了这个Bug.")));
			list.Add(new ChangeLogItem("1.7.2", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("在v1.7.1里面引入了一个新的BUG, 在监控窗口里面把录像按钮从其它方式改变为[不录像]时, 录像没有停止. 在这个版本里面修复了这个问题.")));
			list.Add(new ChangeLogItem("1.7.2", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("在监控窗口没有图像的情况下, 进行区域编辑, 会造成异常. 在这个版本里面修复了这个问题.")));
			list.Add(new ChangeLogItem("1.7.2", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("更可靠的线程同步.")));
			list.Add(new ChangeLogItem("1.7.2", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("用户通过[配置摄像头]对话框配置完毕后, 如果此时该摄像头已经存在于监控面板中, 监控窗口应该反映出用户的修改. 在这个版本里面完成了这一功能.")));
			list.Add(new ChangeLogItem("1.7.2", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("[配置摄像头]对话框的[取消]按钮没有实现预期的功能, 在这个版本里面修复了这个BUG.")));
			list.Add(new ChangeLogItem("1.7.1", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("增加[静止报警]功能, 与动作报警相反, 这是在图像静止的情况下报警.")));
			list.Add(new ChangeLogItem("1.7.1", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("截图改为使用JPEG格式存放, 可以大幅度减少截图存储的文件大小.")));
			list.Add(new ChangeLogItem("1.7.1", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("添加系统托盘右键菜单.")));
			list.Add(new ChangeLogItem("1.7.1", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("修复了在运行日志为空的情况下, 清除日志会造成异常的BUG (感谢最高危机@CCF发现了这个问题).")));
			list.Add(new ChangeLogItem("1.7.1", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("修复了程序偶尔会锁死的Bug, (感谢chinaman@CCF最早报告了这个问题).")));
			list.Add(new ChangeLogItem("1.7.0", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("Motion Detector架构进行了大规模的调整, 支持插件.")));
			list.Add(new ChangeLogItem("1.7.0", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("新增E-Mail报警插件.")));
			list.Add(new ChangeLogItem("1.7.0", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("声音报警调整为插件形式.")));
			list.Add(new ChangeLogItem("1.6.9", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("在主窗口增加[工具]->[使用偏好]菜单,你可以在这里设置数据存储目录,改变窗口的字体.")));
			list.Add(new ChangeLogItem("1.6.9", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("在主窗口下方的状态栏里面显示当前的数据存储目录.")));
			list.Add(new ChangeLogItem("1.6.8", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("在监控窗口的工具栏里面,添加[显示动作识别框]按钮,你可以通过它来控制在监控窗口中显示或者隐藏红色的动作识别框.")));
			list.Add(new ChangeLogItem("1.6.8", ZForge.Controls.Logs.ChangeLogLevel.BUGFIX, Translator.Instance.T("在多个(8个以上)网络摄像头同时运行时,全部关闭这些摄像头在某些情况下会出现死锁现象,在本版本里面修复这个Bug.")));
			list.Add(new ChangeLogItem("1.6.8", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("增加检查版本更新的功能.[关于]->[检查新版本].在Motion Detector启动时,会自动检查新版本.")));
			list.Add(new ChangeLogItem("1.6.8", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("录像方式可以设置为以下三种中的一种: (不录像,报警时录像,持续录像).")));
			list.Add(new ChangeLogItem("1.6.8", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("录像存储方式可以设置为连续存储或者分时存储.")));
			list.Add(new ChangeLogItem("1.6.8", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("增加了对新版本配置文件的检查,避免旧版本的Motion Detector打开新版本配置文件,可能产生的不可预测的错误.")));
			list.Add(new ChangeLogItem("1.6.7", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("新增截图管理,在截图后,您可以选择是存放在图片库里面,还是另行存放.")));
			list.Add(new ChangeLogItem("1.6.7", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("录像浏览和截图浏览中,支持批量导出.")));
			list.Add(new ChangeLogItem("1.6.7", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("录像浏览和截图浏览中,支持批量删除.")));
			list.Add(new ChangeLogItem("1.6.7", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("当用户点击[x]关闭窗口时,提示用户Motion Detector其实依然在运行.")));
			list.Add(new ChangeLogItem("1.6.6", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("使用DirectX来播放报警声音.")));
			list.Add(new ChangeLogItem("1.6.5", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("在录像窗口中,增加[截屏]按钮,点击该按钮将截取当前录像窗口图像.")));
			list.Add(new ChangeLogItem("1.6.5", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("在监控窗口中,增加[截屏]按钮,点击该按钮将截取当前监控窗口图像.")));
			list.Add(new ChangeLogItem("1.6.5", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("在监控窗口中,增加[静音]按钮,如果该按钮被选中,则在报警时，不播放报警音.")));
			list.Add(new ChangeLogItem("1.6.5", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("增加声音报警,包括11种预设的报警音,用户也可以自定义报警音.")));
			list.Add(new ChangeLogItem("1.6.4", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("在录像窗口中,增加[导出录像]按钮,可以将录像导出到其它目录下.")));
			list.Add(new ChangeLogItem("1.6.3", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("在监控窗口中,增加[添加图像标签]按钮,如果该按钮被选中，将自动添加图像标签.")));
			list.Add(new ChangeLogItem("1.6.3", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("在监控面板中,监控窗口可以放大,最大可以放大到原先大小的2倍.")));
			list.Add(new ChangeLogItem("1.6.3", ZForge.Controls.Logs.ChangeLogLevel.REMOVE, Translator.Instance.T("在检测到移动时,监视窗口不再闪动.")));
			list.Add(new ChangeLogItem("1.6.3", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("在[区域编辑]里面增加反向选择功能.")));
			list.Add(new ChangeLogItem("1.6.3", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("在[关于]里面显示版本变更的信息.")));
			fm.ChangeLogCollection = list.ToArray();
			fm.ShowDialog();
		}

		private void ActionShowPlugIn()
		{
			PlugInForm fm = new PlugInForm();
			fm.ShowDialog();
		}

		private void ActionShowAVI()
		{
			if (mfAVI == null)
			{
				mfAVI = new ZForge.Motion.Forms.RecordBrowseForm(CameraBoardStyle.AVI);
				mfAVI.Show();
			}
			else
			{
				mfAVI.Visible = true;
				mfAVI.Activate();
			}
		}

		private void ActionShowPIC()
		{
			if (mfPIC == null)
			{
				mfPIC = new ZForge.Motion.Forms.RecordBrowseForm(CameraBoardStyle.PIC);
				mfPIC.Show();
			}
			else
			{
				mfPIC.Visible = true;
				mfPIC.Activate();
			}
		}

		private void ActionPrefBeforeUpdate()
		{
			this.boardCamera.StopAll();
			this.boardCamera.ClearAll();

			if (mfAVI != null)
			{
				mfAVI.FileSystemWatcherEnabled = false;
				mfAVI.ClearAll();
			}
			if (mfPIC != null)
			{
				mfPIC.FileSystemWatcherEnabled = false;
				mfPIC.ClearAll();
			}
		}

		private void ActionPrefAfterUpdate()
		{
			if (mfAVI != null)
			{
				mfAVI.FileSystemWatcherEnabled = true;
			}
			if (mfPIC != null)
			{
				mfPIC.FileSystemWatcherEnabled = true;
			}
			this.treeCamera.ReloadTreeView();
		}

		private void ActionPref()
		{
			string oldStorage = MotionConfiguration.Instance.Storage;
			PerferenceForm fm = new PerferenceForm();
			if (DialogResult.OK == fm.ShowDialog())
			{
				MotionPreference.Instance.UpdateUI(this);
				if (mfAVI != null)
				{
					MotionPreference.Instance.UpdateUI(mfAVI);
				}
				if (mfPIC != null)
				{
					MotionPreference.Instance.UpdateUI(mfPIC);
				}

				if (DirectoryX.Compare(MotionConfiguration.Instance.Storage, oldStorage) != 0)
				{
					string msg = string.Format(Translator.Instance.T("您的数据存储目录发生了变化, 从[{0}]变更为[{1}], 您是否需要把原先存储目录下的数据迁移到新的存储目录下?"), oldStorage, MotionConfiguration.Instance.Storage);
					if (DialogResult.Yes == MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						this.Cursor = Cursors.WaitCursor;

						this.ActionPrefBeforeUpdate();
						StorageRelocateForm sfm = new StorageRelocateForm();
						sfm.StorageSource = oldStorage;
						sfm.ShowDialog();
						this.ActionPrefAfterUpdate();

						this.Cursor = Cursors.Default;
					}
				}
				if (fm.GlobalSettingsChanged)
				{
					this.treeCamera.NodeReloadGlobalSettings();
				}
				//this.toolStripMainLabelStorage.Text = Translator.Instance.T("存储目录:") + " " + MotionConfiguration.Instance.Storage;
			}
		}

		#endregion

		private void toolStripMenuItemExit_Click(object sender, EventArgs e)
		{
			this.Quit(false);
		}

		private void toolStripButtonExit_Click(object sender, EventArgs e)
		{
			this.Quit(false);
		}

		private void toolStripMenuItemTrayQuit_Click(object sender, EventArgs e)
		{
			this.Visible = true;
			this.Activate();
			this.Quit(false);
		}

		private void tripMenuItemTrayRestore_Click(object sender, EventArgs e)
		{
			this.Visible = true;
			this.Activate();
		}

		private void toolStripMenuItemRegister_Click(object sender, EventArgs e)
		{
      /*
			List<SALicense> lics = new List<SALicense>();
			lics.Add(new ZForge.Motion.License.License());
			SARegisterForm fm = new SARegisterForm(MotionPreference.Instance, lics);
			fm.TopicImage = global::Motion.Properties.Resources.videocamera_48;
			fm.ShowDialog();
      */
		}

		private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
		{
			this.ActionAbout();
		}

		private void toolStripMenuItemPlugIns_Click(object sender, EventArgs e)
		{
			this.ActionShowPlugIn();
		}

		private void toolStripMenuItemCheckUpdate_Click(object sender, EventArgs e)
		{
			MotionPreference.Instance.CheckUpdate(false);
		}

		private void toolStripMainButtonPlugIns_Click(object sender, EventArgs e)
		{
			this.ActionShowPlugIn();
		}

		private void toolStripMainButtonAbout_Click(object sender, EventArgs e)
		{
			this.ActionAbout();
		}

		private void toolStripMenuItemViewAVI_Click(object sender, EventArgs e)
		{
			this.ActionShowAVI();
		}

		private void toolStripMenuItemViewPIC_Click(object sender, EventArgs e)
		{
			this.ActionShowPIC();
		}

		private void toolStripMenuItemTrayViewAVI_Click(object sender, EventArgs e)
		{
			this.ActionShowAVI();
		}

		private void toolStripMenuItemTrayViewPIC_Click(object sender, EventArgs e)
		{
			this.ActionShowPIC();
		}

		private void toolStripMainButtonViewAVI_Click(object sender, EventArgs e)
		{
			this.ActionShowAVI();
		}

		private void toolStripMainButtonViewPIC_Click(object sender, EventArgs e)
		{
			this.ActionShowPIC();
		}
		
		private void toolStripMenuItemPref_Click(object sender, EventArgs e)
		{
			this.ActionPref();
		}

		private void toolStripMainButtonPref_Click(object sender, EventArgs e)
		{
			this.ActionPref();
		}

		private void toolStripMenuItemLang_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem i = sender as ToolStripMenuItem;
			if (i != null && i.Tag != null)
			{
				LanguageFileInfoCollection col = new LanguageFileInfoCollection();
				LanguageFileInfo n = i.Tag as LanguageFileInfo;
				if (n != null)
				{
					if (col.CurrentLanguage.ID == n.ID)
					{
						i.Checked = true;
						return;
					}
					col.CurrentLanguage = n;
					Translator.Instance.Update(this);
					foreach (ToolStripMenuItem m in this.toolStripMenuItemSelectLang.DropDownItems)
					{
						if (m.Checked == true)
						{
							m.Checked = false;
						}
					}
					i.Checked = true;
				}
			}
		}

		private void toolStripMenuItemImport_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.ShowNewFolderButton = false;
			dlg.Description = Translator.Instance.T("请选择旧版本(Motion Detector 1.x.x)的安装目录");
			if (DialogResult.OK == dlg.ShowDialog())
			{
				try
				{
					DirectoryInfo di = new DirectoryInfo(dlg.SelectedPath);
					if (MotionConfiguration.Instance.Import(di.FullName, false))
					{
						MessageBox.Show(Translator.Instance.T("成功导入旧版本的配置信息."), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

						RootClass.Instance.Reload();
						RootClass.Instance.SaveConfig();
						MotionConfiguration.Instance.Save();

						this.treeCamera.ReloadTreeView();
					}
					else
					{
						MessageBox.Show(Translator.Instance.T("没有发现旧版本的配置信息. 您确认选择的目录没有错误吗?"), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(Translator.Instance.T("导入过程中发生错误. 您确认选择的目录没有错误吗?") + "\n" + ex.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		#endregion

		#region Board Events
		private void cameraBoardMain_CameraBoardAddNew(object sender, CameraBoardAddNewEventArgs e)
		{
			IVideoView v = e.VideoView;
			if (v is CameraView)
			{
				CameraView n = (CameraView)v;
				n.EventStatusChanged += new CameraViewStatusChangedEventHandler(this.CameraView_CameraViewStatusChanged);
				n.EventAlarmCountChanged += new CameraViewAlarmCountChangedEventHandler(this.CameraView_CameraViewAlarmCountChanged);
				n.EventLog += new CameraViewLogEventHandler(this.CameraView_CameraViewLog);
			}
		}
		#endregion

		#region Tree Events
		private void treeCamera_CameraOnBoard(object sender, CameraTreeEventArgs e)
		{
			this.boardCamera.Add(e.Camera);
		}

		private void treeCamera_CameraBeforeDelete(object sender, CameraTreeNodeDeleteEventArgs e)
		{
			this.boardCamera.Remove(e.Camera);
		}
		#endregion

		private void logViewer_CountChanged(object sender, LogViewerCountChangedEventArgs e)
		{
			this.tabPageLog.Text = string.Format(Translator.Instance.T("运行日志 ({0}/{1}/{2})"), e.E, e.W, e.I);
		}

		private void CheckStorage()
		{
			bool b = MotionConfiguration.Instance.StorageIsValid;
			if (b)
			{
				this.toolStripMainLabelStorage.Text = Translator.Instance.T("存储目录:") + " " + MotionConfiguration.Instance.Storage;
				if (this.mfAVI != null)
				{
					this.mfAVI.FileSystemWatcherEnabled = true;
				}
				if (this.mfPIC != null)
				{
					this.mfPIC.FileSystemWatcherEnabled = true;
				}
			}
			else
			{
				this.toolStripMainLabelStorage.Text = Translator.Instance.T("存储目录:") + "N/A";
				if (this.mfAVI != null)
				{
					this.mfAVI.Close();
					this.mfAVI.Dispose();
					this.mfAVI = null;
				}
				if (this.mfPIC != null)
				{
					this.mfPIC.Close();
					this.mfPIC.Dispose();
					this.mfPIC = null;
				}
			}
			this.toolStripMainButtonViewAVI.Enabled = b;
			this.toolStripMainButtonViewPIC.Enabled = b;
			this.toolStripMenuItemTrayViewAVI.Enabled = b;
			this.toolStripMenuItemTrayViewPIC.Enabled = b;
			this.toolStripMenuItemViewAVI.Enabled = b;
			this.toolStripMenuItemViewPIC.Enabled = b;
		}

		private void MotionConfiguration_StorageChanged(object sender, StorageChangedEventArgs e)
		{
			this.CheckStorage();
		}

		private void notifyIconTray_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Visible = true;
			this.Activate();
		}

		private void MainForm_VisibleChanged(object sender, EventArgs e)
		{
			if (this.Visible == false)
			{
				this.notifyIconTray.ShowBalloonTip(5, MotionPreference.Instance.MessageBoxCaption, string.Format(Translator.Instance.T("{0}依然在运行"), MotionPreference.Instance.MessageBoxCaption), ToolTipIcon.Info);
			}
		}

		#region IGlobalization Members

		public void UpdateCulture()
		{
			this.toolStripMainLabelStorage.Text = Translator.Instance.T("存储目录:") + " " + MotionConfiguration.Instance.Storage; 
			
			this.panelBoard.CaptionText = Translator.Instance.T("监控面板");
			this.tabPageLog.Text = Translator.Instance.T("运行日志");
			this.panelTree.CaptionText = Translator.Instance.T("摄像头列表");
			this.toolStripMenuItemFile.Text = Translator.Instance.T("监控 (&F)");
			this.toolStripMenuItemSaveOnExit.Text = Translator.Instance.T("在退出时自动保存配置");
			this.toolStripMenuItemExit.Text = Translator.Instance.T("退出 (&e)");
			this.toolStripMenuItemTools.Text = Translator.Instance.T("工具");
			this.toolStripMenuItemViewAVI.Text = Translator.Instance.T("录像管理 ...");
			this.toolStripMenuItemViewPIC.Text = Translator.Instance.T("截图管理 ...");
			this.toolStripMenuItemPref.Text = Translator.Instance.T("使用偏好 ...");
			this.toolStripMenuItemSelectLang.Text = Translator.Instance.T("界面语言");
			this.ToolStripMenuItemHelp.Text = Translator.Instance.T("帮助 (&F)");
			this.toolStripMenuItemCheckUpdate.Text = Translator.Instance.T("检查更新 ...");
			this.toolStripMenuItemRegister.Text = Translator.Instance.T("注册 ...");
			this.toolStripMenuItemPlugIns.Text = Translator.Instance.T("浏览插件 ...");
			this.toolStripMenuItemAbout.Text = Translator.Instance.T("关于... (&A)");
			this.toolStripMenuItemTrayRestore.Text = Translator.Instance.T("显示窗口");
			this.toolStripMenuItemTrayViewAVI.Text = Translator.Instance.T("录像管理 ...");
			this.toolStripMenuItemTrayViewPIC.Text = Translator.Instance.T("截图管理 ...");
			this.toolStripMenuItemTrayQuit.Text = Translator.Instance.T("退出");
			this.toolStripMainButtonViewAVI.Text = Translator.Instance.T("录像管理 ...");
			this.toolStripMainButtonViewPIC.Text = Translator.Instance.T("截图管理 ...");
			this.toolStripMainButtonPref.Text = Translator.Instance.T("使用偏好 ...");
			this.toolStripMainButtonPlugIns.Text = Translator.Instance.T("浏览插件 ...");
			this.toolStripMainButtonAbout.Text = Translator.Instance.T("关于 ...");

			if (mfAVI != null)
			{
				Translator.Instance.Update(mfAVI);
			}
			if (mfPIC != null)
			{
				Translator.Instance.Update(mfPIC);
			}
		}

		#endregion

		public override MenuStrip MainMenu
		{
			get { return this.menuStripMain; }
		}

		public override ToolStrip MainTool
		{
			get { return this.toolStripMain; }
		}

	}
}
