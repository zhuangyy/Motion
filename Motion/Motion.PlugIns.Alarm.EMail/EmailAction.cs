using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ZForge.Motion.PlugIns;
using System.Net.Mail;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.IO;
using ZForge.Globalization;
using ZForge.Configuration;
using ZForge.Controls.PropertyGridEx;
using ZForge.Controls.Logs;
using ZForge.Motion.Util;

namespace Motion.PlugIns.Alarm.EMail
{
	public class EmailAction : IPlugInAlarm, IPlugInUIWithGlobal, IPlugInFeed
	{
		private string mToAddress = "";
		private string mSMTPAddress = "";
		private int mSMTPPort = 25;
		private int mTimeout = 10;
		private string mUsername = "";
		private string mPassword = "";
		private bool mSSL = false;
		private SmtpMailSender mMailSender = null;
		private bool mIsRunning = false;
		private EMailActions mAction = EMailActions.NONE;
		private bool mAttachImage = false;
		private Bitmap mBitmap = null;
		private bool mGlobalOperation = false;
		private bool mUseGlobal = true;

		private List<ZForge.Controls.PropertyGridEx.CustomProperty> mItemList = null;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemUseGlobal;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemAction;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemSSL;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemSmtpAddress;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemSmtpPort;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemToAddress;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemTimeout;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemUsername;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemPassword;
		private ZForge.Controls.PropertyGridEx.CustomProperty mItemAttachImage;

		public EmailAction()
		{
			mMailSender = new SmtpMailSender();
			mMailSender.Client.SendCompleted += new SendCompletedEventHandler(MailSender_SendCompleted);
		}

		#region Properties

		private EMailActions Action
		{
			get { return this.mAction; }
			set
			{
				this.mAction = value;
				if (this.mItemList != null)
				{
					this.mItemAction.SelectedValue = this.Action;
					
					EMailArgs args = new EMailArgs();
					this.mItemAction.Value = args.GetValue(this.Action);

					this.SyncUI();
				}
			}
		}

		private bool SSL
		{
			get { return this.mSSL; }
			set { 
				this.mSSL = value;
				if (this.mItemList != null)
				{
					this.mItemSSL.Value = this.mSSL;
				}
			}
		}

		private string SMTPAddress
		{
			get { return this.mSMTPAddress; }
			set
			{
				this.mSMTPAddress = value;
				if (this.mItemList != null)
				{
					this.mItemSmtpAddress.Value = this.mSMTPAddress;
				}
			}
		}

		private int SMTPPort
		{
			get { return this.mSMTPPort; }
			set
			{
				this.mSMTPPort = value;
				if (this.mSMTPPort < 1 || this.mSMTPPort > 65535)
				{
					this.mSMTPPort = 25;
				}
				if (this.mItemList != null)
				{
					this.mItemSmtpPort.Value = this.mSMTPPort.ToString();
				}
			}
		}

		private int Timeout
		{
			get { return this.mTimeout; }
			set
			{
				if (value < 10)
				{
					this.mTimeout = 10;
				}
				else if (value > 60)
				{
					this.mTimeout = 60;
				}
				else
				{
					this.mTimeout = value;
				}
				if (this.mItemList != null)
				{
					this.mItemTimeout.Value = this.mTimeout.ToString();
				}
			}
		}

		private string Username
		{
			get { return this.mUsername; }
			set
			{
				this.mUsername = value;
				if (this.mItemList != null)
				{
					this.mItemUsername.Value = this.mUsername;
				}
			}
		}

		private string Password
		{
			get { return this.mPassword; }
			set
			{
				this.mPassword = value; 
				if (this.mItemList != null)
				{
					this.mItemPassword.Value = this.mPassword;
				}
			}
		}

		private string ToAddress
		{
			get { return this.mToAddress; }
			set
			{
				this.mToAddress = value; 
				if (this.mItemList != null)
				{
					this.mItemToAddress.Value = this.mToAddress;
				}
			}
		}

		private bool AttachImage
		{
			get { return mAttachImage; }
			set
			{
				this.mAttachImage = value;
				if (this.mItemList != null)
				{
					this.mItemAttachImage.Value = this.mAttachImage;
				}
			}
		}

		private string CategoryText
		{
			get
			{
				return (this.GlobalOperation) ? Translator.Instance.T("EMail报警全局设置") : Translator.Instance.T("EMail报警");
			}
		}

		#endregion

		private void MailSender_SendCompleted(object sender, AsyncCompletedEventArgs e)
		{
			if (this.Log != null)
			{
				MailMessage mail = (MailMessage)e.UserState;
				string subject = mail.Subject;
				if (e.Cancelled)
				{
					string s = string.Format(Translator.Instance.T("报警邮件发送被取消. [{0}]."), subject);
					Log(this, new LogEventArgs(LogLevel.LOG_WARNING, s));
				}
				if (e.Error != null)
				{
					string s = String.Format(Translator.Instance.T("报警邮件发送失败. [{0}] {1}."), subject, e.Error.Message);
					Log(this, new LogEventArgs(LogLevel.LOG_ERROR, s));
				}
				else
				{
					string s = string.Format(Translator.Instance.T("报警邮件发送成功. [{0}]."), subject);
					Log(this, new LogEventArgs(LogLevel.LOG_INFO, s));
				}
			}
			this.mIsRunning = false;
		}

		#region IPlugInAlarm Members

		public bool IsRunning
		{
			get { return this.mIsRunning; }
		}

		public bool Alarm()
		{
			// create the mail message
			if (this.IsRunning == false)
			{
				this.mIsRunning = true;

				System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
				try
				{
					if (this.mAttachImage == true && this.FeedImage != null)
					{
						FeedImage(this, null);
					}
					msg.SubjectEncoding = System.Text.Encoding.UTF8;
					msg.BodyEncoding = System.Text.Encoding.UTF8;
					msg.IsBodyHtml = false;
					msg.Priority = MailPriority.High;

					msg.To.Add(this.ToAddress);
					msg.From = new MailAddress("noreply@motion.com", this.Username, System.Text.Encoding.UTF8);
					msg.Subject = string.Format(Translator.Instance.T("{0} 报警信息 ({1})"), MotionPreference.Instance.ProductFullName, DateTime.Now);
					msg.Body = string.Format(Translator.Instance.T("{0}报警邮件."), MotionPreference.Instance.ProductFullName);

					if (this.mAttachImage == true)
					{
						lock (this)
						{
							if (mBitmap != null)
							{
								MemoryStream stm = new MemoryStream();
								mBitmap.Save(stm, System.Drawing.Imaging.ImageFormat.Jpeg);
								stm.Flush();
								stm.Seek(0L, SeekOrigin.Begin);
								Attachment img = new Attachment(stm, "alarm.jpg", "image/jpg");
								msg.Attachments.Add(img);
								//stm.Dispose();
							}
						}
					}

					mMailSender.Client.Host = this.SMTPAddress;
					mMailSender.Client.Port = this.SMTPPort;
					if (this.Username != null && this.Username.Length > 0)
					{
						mMailSender.Client.Credentials = new System.Net.NetworkCredential(this.Username, this.Password);
					}
					mMailSender.Client.Timeout = this.Timeout * 1000;
					mMailSender.Client.EnableSsl = this.SSL;

					if (this.Log != null)
					{
						string s = string.Format(Translator.Instance.T("发送报警邮件. [{0}]."), msg.Subject);
						Log(this, new LogEventArgs(LogLevel.LOG_INFO, s));
					}
					mMailSender.Send(msg);
				}
				catch (Exception ex)
				{
					this.mIsRunning = false;
					if (this.Log != null)
					{
						string s = string.Format(Translator.Instance.T("报警邮件发送失败. [{0}], {1}."), msg.Subject, ex.Message);
						Log(this, new LogEventArgs(LogLevel.LOG_ERROR, s));
					}
				}
			}
			return true;
		}

		public bool Stop()
		{
			return true;
		}

		#endregion

		#region IPlugin Members

		public string ID
		{
			get { return "p17a988de71b3fb81d5cc2d6612dfe072"; }
		}

		public string Name
		{
			get { return Translator.Instance.T("E-Mail报警"); }
		}

		public string Description
		{
			get { return Translator.Instance.T("通过E-Mail报警"); }
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
				List<string> msgs = new List<string>();
				if (this.mAction == EMailActions.NONE) {
					return false;
				}
				return this.ValidCheck(msgs);
			}
		}

		public void Release()
		{
			this.Stop();
		}

		public void Dispose()
		{
			this.Stop();
			mMailSender = null;
		}

		public bool ValidCheck(List<string> msgs)
		{
			bool r = true;
			if (this.mAction != EMailActions.NONE && this.UseGlobal != true)
			{
				r = (r && Validator.ValidateEmailAddress(msgs, Translator.Instance.T("接收者EMail地址"), this.ToAddress));
				r = (r && Validator.ValidateString(msgs, Translator.Instance.T("SMTP服务器地址"), this.SMTPAddress));
				r = (r && Validator.ValidateInt(msgs, Translator.Instance.T("SMTP服务器端口"), this.SMTPPort.ToString(), 1, 65535));
				r = (r && Validator.ValidateInt(msgs, Translator.Instance.T("邮件发送超时设置"), this.Timeout.ToString(), 10, 60));
			}
			return r;
		}

		public List<ZForge.Controls.Logs.ChangeLogItem> ChangeLogList
		{
			get
			{
				List<ZForge.Controls.Logs.ChangeLogItem> r = new List<ZForge.Controls.Logs.ChangeLogItem>();
				r.Add(new ZForge.Controls.Logs.ChangeLogItem("2.0.0", ZForge.Controls.Logs.ChangeLogLevel.CHANGE, Translator.Instance.T("升级到插件框架2.0.")));
				r.Add(new ZForge.Controls.Logs.ChangeLogItem("1.0.3", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("支持全局设置.")));
				r.Add(new ZForge.Controls.Logs.ChangeLogItem("1.0.2", ZForge.Controls.Logs.ChangeLogLevel.ADD, Translator.Instance.T("界面微调.")));
				r.Add(new ZForge.Controls.Logs.ChangeLogItem("1.0.1", ZForge.Controls.Logs.ChangeLogLevel.ADD, string.Format(Translator.Instance.T("[{0}]插件公开发布."), this.Name)));
				return r;
			}
		}

		#endregion

		#region IPluginLog Members

		public event ZForge.Controls.Logs.LogEventHandler Log;

		#endregion

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

		private void InitGridItems(List<ZForge.Controls.PropertyGridEx.CustomProperty> list)
		{
			EMailArgs Args = new EMailArgs();

			if (this.GlobalOperation == false)
			{
				mItemUseGlobal = new CustomProperty(Translator.Instance.T("是否使用Email报警全局设置"), this.UseGlobal, false, this.CategoryText,Translator.Instance.T("确定是否使用[使用偏好]中的全局设置"), true);
				list.Add(mItemUseGlobal);
			}

			mItemAction = new CustomProperty(Translator.Instance.T("EMail报警方式"), Args.GetValue(this.Action), false, this.CategoryText, Translator.Instance.T("设置启用或者不启用EMail报警"), true);
			mItemAction.ValueMember = "Key";
			mItemAction.DisplayMember = "Value";
			mItemAction.Datasource = Args.ACTIONS;
			list.Add(mItemAction);

			mItemToAddress = new CustomProperty(Translator.Instance.T("接收者EMail地址"), this.ToAddress, false, this.CategoryText, Translator.Instance.T("设置报警接收者的邮箱地址"), true);
			list.Add(mItemToAddress);

			mItemSmtpAddress = new CustomProperty(Translator.Instance.T("SMTP服务器地址"), this.SMTPAddress, false, this.CategoryText, Translator.Instance.T("设置发送邮件的SMTP服务器地址"), true);
			list.Add(mItemSmtpAddress);

			mItemSmtpPort = new CustomProperty(Translator.Instance.T("SMTP服务器端口"), this.SMTPPort.ToString(), false, this.CategoryText, Translator.Instance.T("设置发送邮件的SMTP服务器端口, 通常为25; 如果是SSL连接, 通常是465; 如果您使用的是Gmail账号, 这里应该设置成25."), true);
			list.Add(mItemSmtpPort);

			mItemUsername = new CustomProperty(Translator.Instance.T("SMTP用户名"), this.Username, false, this.CategoryText, Translator.Instance.T("设置邮件发送的用户名, 如果您使用的是Gmail账号, 这里需要填入完整的Email地址."), true);
			list.Add(mItemUsername);

			mItemPassword = new CustomProperty(Translator.Instance.T("SMTP用户密码"), this.Password, false, this.CategoryText, Translator.Instance.T("设置邮件发送的密码"), true);
			mItemPassword.IsPassword = true;
			list.Add(mItemPassword);

			mItemSSL = new CustomProperty(Translator.Instance.T("是否是SSL连接"), this.SSL, false, this.CategoryText, Translator.Instance.T("设置是否是SSL连接, 如果您使用的是Gmail账号, 请设置为True"), true);
			list.Add(mItemSSL);

			mItemTimeout = new CustomProperty(Translator.Instance.T("邮件发送超时设置"), this.Timeout.ToString(), false, this.CategoryText, Translator.Instance.T("设置邮件发送的超时, 缺省为10秒"), true);
			list.Add(mItemTimeout);

			mItemAttachImage = new CustomProperty(Translator.Instance.T("邮件包含图片"), this.mAttachImage, false, this.CategoryText, Translator.Instance.T("设置在邮件中是否包含图片"), true);
			list.Add(mItemAttachImage);

			foreach (ZForge.Controls.PropertyGridEx.CustomProperty i in list)
			{
				i.Tag = this.ID;
			}
			this.SyncUI();
		}

		public List<ZForge.Controls.PropertyGridEx.CustomProperty> UIPropertyItems
		{
			get
			{
				if (mItemList == null)
				{
					mItemList = new List<ZForge.Controls.PropertyGridEx.CustomProperty>();
					InitGridItems(mItemList);
				}
				return mItemList;
			}
		}

		public List<System.Windows.Forms.ToolStripItem> UIEditFormToolStripItems
		{
			get
			{
				List<System.Windows.Forms.ToolStripItem> r = new List<System.Windows.Forms.ToolStripItem>();
				return r;
			}
		}

		public List<System.Windows.Forms.ToolStripItem> UICameraViewToolStripItems
		{
			get
			{
				List<System.Windows.Forms.ToolStripItem> r = new List<System.Windows.Forms.ToolStripItem>();
				return r;
			}
		}

		private void SyncUI()
		{
			if (this.mItemList == null)
			{
				return;
			}

			bool b;
			b = (this.Action != EMailActions.NONE);
			mItemToAddress.Visible = b;
			mItemSmtpAddress.Visible = b;
			mItemSmtpPort.Visible = b;
			mItemTimeout.Visible = b;
			mItemUsername.Visible = b;
			mItemPassword.Visible = b;
			mItemSSL.Visible = b;
			mItemAttachImage.Visible = b;

			b = this.UseGlobal;
			mItemAction.IsReadOnly = b;
			mItemToAddress.IsReadOnly = b;
			mItemSmtpAddress.IsReadOnly = b;
			mItemSmtpPort.IsReadOnly = b;
			mItemTimeout.IsReadOnly = b;
			mItemUsername.IsReadOnly = b;
			mItemPassword.IsReadOnly = b;
			mItemSSL.IsReadOnly = b;
			mItemAttachImage.IsReadOnly = b;
		}

		public bool UISetValue(System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			List<string> msgs = new List<string>();
			bool r = true;
			string v = e.ChangedItem.Value as string;

			if (this.mItemUseGlobal != null && e.ChangedItem.Label.Equals(this.mItemUseGlobal.Name))
			{
				this.UseGlobal = (bool)e.ChangedItem.Value;
			}
			else if (e.ChangedItem.Label.Equals(this.mItemAction.Name))
			{
				this.Action = (EMailActions)this.mItemAction.SelectedValue;
			}
			else if (e.ChangedItem.Label.Equals(this.mItemToAddress.Name))
			{
				r = Validator.ValidateEmailAddress(msgs, this.mItemToAddress.Name, v);
				if (r)
				{
					this.ToAddress = v;
				}
			}
			else if (e.ChangedItem.Label.Equals(this.mItemSmtpAddress.Name))
			{
				r = Validator.ValidateString(msgs, this.mItemSmtpAddress.Name, v);
				if (r)
				{
					this.SMTPAddress = v;
				}
			}
			else if (e.ChangedItem.Label.Equals(this.mItemSmtpPort.Name))
			{
				r = Validator.ValidateInt(msgs, this.mItemSmtpPort.Name, v, 1, 65535);
				if (r)
				{
					this.SMTPPort = Convert.ToInt32(v);
				}
			}
			else if (e.ChangedItem.Label.Equals(this.mItemTimeout.Name))
			{
				r = Validator.ValidateInt(msgs, this.mItemTimeout.Name, v, 10, 60);
				if (r)
				{
					this.Timeout = Convert.ToInt32(v);
				}
			}
			else if (e.ChangedItem.Label.Equals(this.mItemUsername.Name))
			{
				this.Username = v;
			}
			else if (e.ChangedItem.Label.Equals(this.mItemPassword.Name))
			{
				this.Password = v;
			}
			else if (e.ChangedItem.Label.Equals(this.mItemSSL.Name))
			{
				this.SSL = (bool)e.ChangedItem.Value;
			}
			else if (e.ChangedItem.Label.Equals(this.mItemAttachImage.Name))
			{
				this.mAttachImage = (bool)e.ChangedItem.Value;
			}
			if (r == false)
			{
				MessageBox.Show(Validator.MergeMessages(msgs), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return r;
		}

		#endregion

		#region IPlugInFeed Members

		public event PlugInFeedEventHandler FeedImage;

		public System.Drawing.Bitmap Image
		{
			set
			{
				lock (this)
				{
					if (mBitmap != null)
					{
						mBitmap.Dispose();
						mBitmap = null;
					}
					mBitmap = (value == null) ? null : new Bitmap(value);
				}
			}
		}

		#endregion

		#region IConfigurable Members

		public void LoadConfig(IConfigSetting section)
		{
			IConfigSetting i = section[this.ID];

			this.UseGlobal = i["useglobal"].boolValue;
			if (this.UseGlobal == true)
			{
				return;
			}

			IConfigSetting s = i["smtp"];
			this.SMTPAddress = s["address"].Value;
			this.SMTPPort = s["port"].intValue;

			this.SSL = i["ssl"].boolValue;
			this.Timeout = i["timeout"].intValue;
			this.ToAddress = i["to"].Value;
			this.Username = i["username"].Value;
			this.Password = i["password"].Value;
			this.AttachImage = i["attachimage"].boolValue;

			this.Action = (EMailActions) i["action"].intValue;
		}

		public void SaveConfig(IConfigSetting section)
		{
			IConfigSetting i = section[this.ID];
			i.RemoveChildren();

			IConfigSetting s = i["smtp"];
			s["address"].Value = this.SMTPAddress;
			s["port"].intValue = this.SMTPPort;

			i["action"].intValue = (int)this.Action;
			i["timeout"].intValue = this.Timeout;
			i["to"].Value = this.ToAddress;
			i["username"].Value = this.Username;
			i["password"].Value = this.Password;
			i["ssl"].boolValue = this.SSL;
			i["attachimage"].boolValue = this.mAttachImage;

			if (this.GlobalOperation == false)
			{
				i["useglobal"].boolValue = this.UseGlobal;
			}
		}

		#endregion
	}
}
