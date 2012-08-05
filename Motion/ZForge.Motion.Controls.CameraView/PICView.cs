using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZForge.Motion.Core;
using ZForge.Globalization;
using ZForge.Motion.Util;

namespace ZForge.Motion.Controls
{
	public partial class PICView : UserControl, IView, IFileView, IGlobalization
	{
		private PICClass mPhoto;
		private Bitmap mBitmap;
		private float mViewRatio = 1.0F;

		public PICView(PICClass c)
		{
			InitializeComponent();
			MotionPreference.Instance.UpdateUI(this);

			//this.pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			this.mPhoto = c;
			Bitmap b = new Bitmap(c.FileName);
			if (b != null)
			{
				this.mBitmap = new Bitmap(b.Width, b.Height);
				Graphics g = Graphics.FromImage(this.mBitmap);
				g.DrawImage(b, 0, 0, b.Width, b.Height);
				g.Dispose();
			}
			this.Title = this.mPhoto.Title;
		}

		public PICClass PICClass
		{
			get
			{
				return this.mPhoto;
			}
		}

		#region IFileView members
		public IFileClass FileClass
		{
			get
			{
				return this.PICClass;
			}
		}
		#endregion

		#region IView Members
		public CameraClass Owner
		{
			get
			{
				return this.PICClass.Owner;
			}
		}

		public float ViewRatio
		{
			get
			{
				return mViewRatio;
			}
			set
			{
				mViewRatio = value;
				int w = (int)(mBitmap.Width * mViewRatio + 0.5F);
				int h = (int)(mBitmap.Height * mViewRatio + 0.5F);

				this.pictureBox.ClientSize = new Size(w, h);
				if (this.pictureBox.Image != null)
				{
					this.pictureBox.Image.Dispose();
				}
				Bitmap b = new Bitmap(w, h);
				Graphics g = Graphics.FromImage(b);
				g.DrawImage(mBitmap, 0, 0, w, h);
				g.Dispose();

				this.pictureBox.Image = (Image)b;

				this.Width = w;
				this.Height = h + panelMaster.CaptionHeight + toolStrip.Height;
			}
		}

		public UserControl Me
		{
			get { return this; }
		}

		public string Title
		{
			get
			{
				return this.panelMaster.CaptionText;
			}
			set
			{
				this.panelMaster.CaptionText = value;
			}
		}

		public string ID
		{
			get { return this.PICClass.ID; }
		}

		public bool Remove()
		{
			this.mBitmap.Dispose();
			if (this.PICClass.Remove())
			{
				this.Visible = false;
				return true;
			}
			else
			{
				this.mBitmap = new Bitmap(this.PICClass.FileName);
				return false;
			}
		}
		#endregion

		#region Toolbar functions

		private void toolStripButtonSaveAs_Click(object sender, EventArgs e)
		{
			this.saveFileDialog.FileName = this.Title.Replace(":", "-");
			DialogResult r = this.saveFileDialog.ShowDialog();
			if (r == DialogResult.OK && this.saveFileDialog.FileName != null && this.saveFileDialog.FileName.Length > 0)
			{
				try
				{
					mBitmap.Save(this.saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
				}
				catch (Exception ex)
				{
					MessageBox.Show(Translator.Instance.T("保存图像失败!") + "\n" + ex.Message, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void toolStripButtonDelete_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == MessageBox.Show(Translator.Instance.T("确定要永久删除这张图像吗?"), MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
			{
				this.Remove();
			}
		}

		private void toolStripButtonClose_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

		private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
		{
			float[] ratios = new float[] { 0.5F, 0.6F, 0.7F, 0.8F, 0.9F, 1.0F, 1.2F, 1.5F, 2.0F };
			int n = 0;
			foreach (float f in ratios)
			{
				n++;
				if (this.ViewRatio == f && n < ratios.Length)
				{
					this.ViewRatio = ratios[n];
					break;
				}
			}
		}

		private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
		{
			float[] ratios = new float[] { 0.5F, 0.6F, 0.7F, 0.8F, 0.9F, 1.0F, 1.2F, 1.5F, 2.0F };
			int n = 0;
			foreach (float f in ratios)
			{
				if (this.ViewRatio == f && n > 0)
				{
					this.ViewRatio = ratios[n - 1];
					break;
				}
				n++;
			}
		}

	#endregion

		#region IGlobalization Members

		public void UpdateCulture()
		{
			this.saveFileDialog.Filter = Translator.Instance.T("截图文件") + " (*.jpg)|*.jpg";
			this.saveFileDialog.Title = Translator.Instance.T("存储截图文件");
			this.panelMaster.CaptionText = Translator.Instance.T("截图");
			this.toolStripButtonClose.Text = Translator.Instance.T("关闭");
			this.toolStripButtonClose.ToolTipText = Translator.Instance.T("关闭当前窗口");
			this.toolStripButtonSaveAs.Text = Translator.Instance.T("另存为...");
			this.toolStripButtonDelete.Text = Translator.Instance.T("删除");
			this.toolStripButtonDelete.ToolTipText = Translator.Instance.T("删除截图");

			this.toolStripButtonZoomIn.Text = Translator.Instance.T("放大");
			this.toolStripButtonZoomIn.ToolTipText = Translator.Instance.T("放大");
			this.toolStripButtonZoomOut.Text = Translator.Instance.T("缩小");
			this.toolStripButtonZoomOut.ToolTipText = Translator.Instance.T("缩小");
		}

		#endregion

	}
}
