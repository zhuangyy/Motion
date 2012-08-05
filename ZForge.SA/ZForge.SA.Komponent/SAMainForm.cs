using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ZForge.Win32;
using System.ComponentModel;
using ZForge.Globalization;
using System.IO;
using ZForge.Configuration;
using System.Drawing;

namespace ZForge.SA.Komponent
{
	public class SAMainForm : Form, IConfigurable
	{
#if SAFE_ANYWHERE_WITHUK
		private RemovableDriveDetector mRemovableDriveDetector;
#endif

		public virtual System.Windows.Forms.MenuStrip MainMenu { get { return null; } }
		public virtual System.Windows.Forms.ToolStrip MainTool { get { return null; } }

		protected virtual void InitializeKomponentMenu(SAPreference pref)
		{
			System.Windows.Forms.MenuStrip ms = this.MainMenu;
			if (ms != null && KomponentCollection.Instance.Count > 0)
			{
				System.Windows.Forms.ToolStripMenuItem sa = new ToolStripMenuItem();
				sa.Text = "Safe.Anywhere";
				sa.Name = "KomponentMenuItem";

				int n = 0;
				foreach (IKomponent k in KomponentCollection.Instance)
				{
					System.Windows.Forms.ToolStripMenuItem mi = new ToolStripMenuItem();
					mi.Name = string.Format("KomponentMenuItem{0}", n);
					mi.Text = k.FullName;
					mi.Tag = k;
					mi.Image = k.Image16;
					mi.Click += new System.EventHandler(this.KomponentMenuItem_Click);
					n++;
					sa.DropDownItems.Add(mi);
				}
				ms.Items.Add(sa);
			}
			System.Windows.Forms.ToolStrip ts = this.MainTool;
			if (ts != null && KomponentCollection.Instance.Count > 0)
			{
				System.Windows.Forms.ToolStripSeparator sp = new System.Windows.Forms.ToolStripSeparator();
				ts.Items.Add(sp);

				int n = 0;
				foreach (IKomponent k in KomponentCollection.Instance)
				{
					System.Windows.Forms.ToolStripButton ti = new System.Windows.Forms.ToolStripButton();
					ti.Name = string.Format("KomponentToolItem{0}", n);
					ti.Text = k.FullName;
					ti.ToolTipText = k.FullName;
					ti.Tag = k;
					ti.Image = k.Image24;
					ti.DisplayStyle = ToolStripItemDisplayStyle.Image;
					ti.ImageScaling = ToolStripItemImageScaling.None;
					ti.Click += new System.EventHandler(this.KomponentMenuItem_Click);
					n++;
					ts.Items.Add(ti);
				}
			}
      if (ts != null)
      {
        ZForge.Controls.RSS.ToolStripRSSLabel rl = new ZForge.Controls.RSS.ToolStripRSSLabel(pref.Company, pref.URL);
        rl.Alignment = ToolStripItemAlignment.Right;
        rl.URLs.AddRange(pref.RSSFeeds);
        ts.Items.Add(rl);
        rl.Run();
      }
    }

		protected virtual void KomponentMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripItem ti = sender as ToolStripItem;
			if (ti != null)
			{
				IKomponent k = ti.Tag as IKomponent;
				if (k != null)
				{
					k.Run(null);
				}
			}
		}

		protected virtual void ApplicationInitialize()
		{
			if (this.IsStandalone)
			{
#if SAFE_ANYWHERE_WITHUK
				mRemovableDriveDetector = new RemovableDriveDetector();
				mRemovableDriveDetector.DeviceRemoved += new RemovableDriveDetectorEventHandler(RemovableDriveDetector_DeviceRemoved);
#else
				this.ClearUselessFiles();
#endif
			}
		}

		private void ClearUselessFiles()
		{
			FileInfo fi = new FileInfo(Application.ExecutablePath);
			try
			{
				DirectoryX.Delete(fi.Directory, "*.temp", "*.temp", true);
			}
			catch
			{
			}
			string[] files = new string[] { "ZForge.NDIS.dll", "ZForge.NDIS.Interop.dll", "ZForge.DiskSafe.dll", "ZForge.DiskSafe.Interop.dll" };
			foreach (string s in files)
			{
				string f = Path.Combine(fi.DirectoryName, s);
				try
				{
					if (File.Exists(f))
					{
						File.Delete(f);
					}
				}
				catch
				{
				}
			}
			string[] dirs = new string[] { "Driver", "Tools" };
			foreach (string s in dirs)
			{
				string f = Path.Combine(fi.DirectoryName, s);
				try
				{
					if (Directory.Exists(f))
					{
						Directory.Delete(f, true);
					}
				}
				catch
				{
				}
			}
		}

#if SAFE_ANYWHERE_WITHUK
		private void RemovableDriveDetector_DeviceRemoved(object sender, RemovableDriveDetectorEventArgs e)
		{
			FileInfo fi = new FileInfo(Application.ExecutablePath);
			if (false == fi.Exists)
			{
				this.Quit(false);
			}
		}
#endif

		protected virtual void ApplicationReset()
		{
		}

		protected virtual bool ClosingCheckReason { get { return true; } }

		protected virtual bool IsStandalone { get { return true; } }

		public virtual void Quit(bool quiet)
		{
			this.ApplicationReset();
			if (this.IsStandalone)
			{
				Application.Exit();
        Environment.Exit(0);
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			FormClosingEventArgs ce = e as FormClosingEventArgs;
      if (this.ClosingCheckReason && ce != null)
			{
				switch (ce.CloseReason)
				{
					case CloseReason.UserClosing:
						e.Cancel = true;
						this.Visible = false;
						break;
					default:
						e.Cancel = false;
						break;
				}
			}
			base.OnClosing(e);
		}

		protected void Register(SAPreference pref, Image ti)
		{
			List<SALicense> lics = new List<SALicense>();
			lics.Add(new SALicense(pref.ProductName));
			SARegisterForm fm = new SARegisterForm(pref, lics);
			fm.TopicImage = ti;
			fm.ShowDialog();
		}

		#region IConfigurable Members

		public virtual void SaveConfig(IConfigSetting s)
		{
			ConfigHelper.Save(s, this);
		}

		public virtual void LoadConfig(IConfigSetting s)
		{
			ConfigHelper.Load(s, this);
		}

		#endregion
	}
}
