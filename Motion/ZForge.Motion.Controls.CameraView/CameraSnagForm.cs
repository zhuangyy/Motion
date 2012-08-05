using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZForge.Motion.Core;
using ZForge.Globalization;
using ZForge.Motion.Util;

namespace ZForge.Motion.Controls
{
	public partial class CameraSnagForm : Form
	{
		private CameraClass mCamera;
		private DateTimeEx mDateTime;

		public CameraSnagForm()
		{
			InitializeComponent();
			MotionPreference.Instance.UpdateUI(this);
		}

		public Bitmap Bitmap
		{
			set
			{
				if (value != null)
				{
					this.SuspendLayout();
					pictureBox.ClientSize = new Size(value.Width + 20, value.Height + 20);
					this.pictureBox.Image = (Image)value;
					this.ClientSize = new Size(pictureBox.Width + 20, pictureBox.Height + 20 + toolStrip1.Height);
					this.ResumeLayout(false);
					this.PerformLayout();
				}
			}
		}

		public CameraClass Camera
		{
			get
			{
				return this.mCamera;
			}
			set
			{
				this.mCamera = value;
				this.mDateTime = new DateTimeEx();
				this.Text = this.Camera.Name + " " + this.mDateTime.DateTime;
			}
		}

		private void toolStripButtonSaveAs_Click(object sender, EventArgs e)
		{
			this.saveFileDialog.FileName = this.Text.Replace(":", "-");
			DialogResult r = this.saveFileDialog.ShowDialog();
			if (r == DialogResult.OK && this.saveFileDialog.FileName != null && this.saveFileDialog.FileName.Length > 0)
			{
				try
				{
					pictureBox.Image.Save(this.saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
				}
				catch (Exception ex)
				{
					MessageBox.Show(Translator.Instance.T("保存图像失败!") + "\n" + ex.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

		}

		private void toolStripButtonSave_Click(object sender, EventArgs e)
		{
			if (MotionConfiguration.Instance.StorageIsValid == false)
			{
				string m = Translator.Instance.T("数据存储目录[{0}]不存在, 您的截图无法存储到该目录.");
				m += "\n";
				m += Translator.Instance.T("如果您需要存放截图, 请通过[主菜单]->[工具]->[使用偏好...], 设置您的数据存储目录.");
				MessageBox.Show(string.Format(m, MotionConfiguration.Instance.Storage), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			String fileName = string.Format("{0}.{1}.{2}.jpg", this.Camera.ID, (int)RecordMark.UNREAD, this.mDateTime.TimeStamp);
			try
			{
				pictureBox.Image.Save(MotionConfiguration.Instance.StoragePIC + @"\" + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(Translator.Instance.T("保存图像失败!") + "\n" + ex.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}