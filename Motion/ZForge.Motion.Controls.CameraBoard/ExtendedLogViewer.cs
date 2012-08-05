using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZForge.Motion.Core;
using System.Threading;
using System.Collections;
using ZForge.Globalization;
using ZForge.Controls.Logs;

namespace ZForge.Motion.Controls
{
	public partial class ExtendedLogViewer : UserControl, IGlobalization
	{
		public delegate void LogAddCallBack(ZForge.Controls.Logs.LogLevel level, CameraView view, string msg);

		private int mShowLogs;
		private int mCountError;
		private int mCountWarn;
		private int mCountInfo;
		private ArrayList mLogs;
		private Mutex mMutex;

		public ExtendedLogViewer()
		{
			this.mMutex = new Mutex();
			InitializeComponent();
			this.InitGrid();

			this.mShowLogs = (int)ZForge.Controls.Logs.LogLevel.LOG_ERROR | (int)ZForge.Controls.Logs.LogLevel.LOG_WARNING | (int)ZForge.Controls.Logs.LogLevel.LOG_INFO;
			this.toolStripComboBoxView.Items.Add(new ExtendedLogViewerTag(ZForge.Controls.Logs.LogLevel.LOG_INFO, "0", Translator.Instance.T("显示所有摄像头的日志信息")));
			this.toolStripComboBoxView.SelectedIndex = 0;
			this.CountError = 0;
			this.CountInfo = 0;
			this.CountWarn = 0;

			this.mLogs = new ArrayList();
		}

		private void InitGrid()
		{
			SourceGrid.Cells.ColumnHeader header;

			this.logGrid.ColumnsCount = 5;
			this.logGrid.FixedRows = 1;
			this.logGrid.RowsCount = 1;

			//Font font = new Font(this.Font, FontStyle.Bold);
			header = new SourceGrid.Cells.ColumnHeader("#");
			//header.View.Font = font;

			//header.View = viewColumnHeader;
			this.logGrid[0, 0] = header;
			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("日志等级"));
			//header.View = viewColumnHeader;
			this.logGrid[0, 1] = header;
			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("时间"));
			//header.View = viewColumnHeader;
			this.logGrid[0, 2] = header;
			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("摄像头"));
			//header.View = viewColumnHeader;
			this.logGrid[0, 3] = header;
			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("内容"));
			//header.View = viewColumnHeader;
			this.logGrid[0, 4] = header;

			this.logGrid.AutoSizeCells();
			this.logGrid.AutoStretchColumnsToFitWidth = true;
			this.logGrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
		}

		#region My Event
		public event LogViewerCountEventHandler CountChanged;
		protected virtual void OnCountChanged(LogViewerCountChangedEventArgs e)
		{
			if (this.CountChanged != null)
			{
				CountChanged(this, e);
			}
		}
		#endregion

		#region properties
		private void SetShowFlags(ZForge.Controls.Logs.LogLevel f, bool set)
		{
			lock (this)
			{
				if (set == true)
				{
					this.mShowLogs = this.mShowLogs | (int)f;
				}
				else
				{
					this.mShowLogs = this.mShowLogs & (~(int)f);
				}
				foreach (SourceGrid.Grid.GridRow row in this.logGrid.Rows)
				{
					if (row.Tag != null)
					{
						ExtendedLogViewerTag t = (ExtendedLogViewerTag)row.Tag;
						row.Visible = (((int)t.LogLevel & this.mShowLogs) != 0);
					}
				}
			}
		}

		public bool ShowError
		{
			get
			{
				return (this.mShowLogs & (int)ZForge.Controls.Logs.LogLevel.LOG_ERROR) != 0;
			}
			set
			{
				this.SetShowFlags(ZForge.Controls.Logs.LogLevel.LOG_ERROR, value);
				toolStripButtonError.Checked = this.ShowError;
			}
		}

		public bool ShowWarning
		{
			get
			{
				return (this.mShowLogs & (int)ZForge.Controls.Logs.LogLevel.LOG_WARNING) != 0;
			}
			set
			{
				this.SetShowFlags(ZForge.Controls.Logs.LogLevel.LOG_WARNING, value);
				toolStripButtonWarning.Checked = this.ShowWarning;
			}
		}

		public bool ShowInfo
		{
			get
			{
				return (this.mShowLogs & (int)ZForge.Controls.Logs.LogLevel.LOG_INFO) != 0;
			}
			set
			{
				this.SetShowFlags(ZForge.Controls.Logs.LogLevel.LOG_INFO, value);
				toolStripButtonInfo.Checked = this.ShowInfo;
			}
		}

		public int CountError
		{
			get { return this.mCountError; }
			set
			{
				this.mCountError = value;
				this.toolStripStatusLabelError.Text = Translator.Instance.T("错误日志:") + " " + this.mCountError;
				OnCountChanged(new LogViewerCountChangedEventArgs(this.CountError, this.CountWarn, this.CountInfo));
			}
		}

		public int CountWarn
		{
			get { return this.mCountWarn; }
			set
			{
				this.mCountWarn = value;
				this.toolStripStatusLabelWarning.Text = Translator.Instance.T("警告日志:") + " " + this.mCountWarn;
				OnCountChanged(new LogViewerCountChangedEventArgs(this.CountError, this.CountWarn, this.CountInfo));
			}
		}

		public int CountInfo
		{
			get { return this.mCountInfo; }
			set
			{
				this.mCountInfo = value;
				this.toolStripStatusLabelInfo.Text = Translator.Instance.T("普通日志:") + " " + this.mCountInfo;
				OnCountChanged(new LogViewerCountChangedEventArgs(this.CountError, this.CountWarn, this.CountInfo));
			}
		}
		#endregion

		public void Add(ZForge.Controls.Logs.LogLevel level, CameraView view, string msg)
		{
			ExtendedLogViewerItem i = new ExtendedLogViewerItem(level, view, msg);
			this.mMutex.WaitOne();
			this.mLogs.Add(i);
			this.mMutex.ReleaseMutex();
		}

		private void FillRowMark(ZForge.Controls.Logs.LogLevel level, int pos)
		{
			switch (level)
			{
				case ZForge.Controls.Logs.LogLevel.LOG_ERROR:
					this.logGrid[pos, 1] = new SourceGrid.Cells.Cell(Translator.Instance.T("错误"));
					this.logGrid[pos, 1].Image = this.imageListLog.Images[0];
					this.logGrid.Rows[pos].Visible = this.ShowError;
					this.CountError++;
					break;
				case ZForge.Controls.Logs.LogLevel.LOG_WARNING:
					this.logGrid[pos, 1] = new SourceGrid.Cells.Cell(Translator.Instance.T("警告"));
					this.logGrid[pos, 1].Image = this.imageListLog.Images[1];
					this.logGrid.Rows[pos].Visible = this.ShowWarning;
					this.CountWarn++;
					break;
				default:
					this.logGrid[pos, 1] = new SourceGrid.Cells.Cell(Translator.Instance.T("普通"));
					this.logGrid[pos, 1].Image = this.imageListLog.Images[2];
					this.CountInfo++;
					this.logGrid.Rows[pos].Visible = this.ShowInfo;
					break;
			}
		}

		private void UpdateUIAdd(ExtendedLogViewerItem item)
		{
			int pos = this.logGrid.RowsCount;
			this.logGrid.RowsCount++;

			this.logGrid[pos, 0] = new SourceGrid.Cells.Cell(pos);
			this.FillRowMark(item.LogLevel, pos);
			this.logGrid[pos, 2] = new SourceGrid.Cells.Cell(item.TimeStamp.ToString());
			this.logGrid[pos, 3] = new SourceGrid.Cells.Cell(item.Name);

			this.logGrid[pos, 4] = new SourceGrid.Cells.Cell(item.Message);
			this.logGrid[pos, 4].View.WordWrap = true;

			ExtendedLogViewerTag tag = new ExtendedLogViewerTag(item.LogLevel, item.ID, item.Name);
			this.logGrid.Rows[pos].Tag = tag;

			this.ShortcutAdd(tag);
		}

		private void ShortcutAdd(ExtendedLogViewerTag tag)
		{
			foreach (ExtendedLogViewerTag t in this.toolStripComboBoxView.Items)
			{
				if (t.ID.Equals(tag.ID))
				{
					return;
				}
			}
			this.toolStripComboBoxView.Items.Add(tag);
			try
			{
				object o = this.toolStripComboBoxView.SelectedItem;
				ArrayList list = ArrayList.Adapter(this.toolStripComboBoxView.Items);
				list.Sort();
				this.toolStripComboBoxView.SelectedItem = o;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		public void Clear()
		{
			this.mMutex.WaitOne();
			if (this.logGrid.Rows.Count > 1)
			{
				this.logGrid.Rows.RemoveRange(1, this.logGrid.Rows.Count - 1);
			}
			this.CountError = 0;
			this.CountWarn = 0;
			this.CountInfo = 0;
			this.toolStripComboBoxView.Items.Clear();
			this.toolStripComboBoxView.Items.Add(new ExtendedLogViewerTag(ZForge.Controls.Logs.LogLevel.LOG_INFO, "0", Translator.Instance.T("显示所有摄像头的日志信息")));
			this.mMutex.ReleaseMutex();
			this.toolStripComboBoxView.SelectedIndex = 0;
		}

		#region Toolbar Events
		private void toolStripButtonClear_Click(object sender, EventArgs e)
		{
			this.Clear();
		}

		private void toolStripButtonError_CheckStateChanged(object sender, EventArgs e)
		{
			this.ShowError = toolStripButtonError.Checked;
		}

		private void toolStripButtonWarning_CheckStateChanged(object sender, EventArgs e)
		{
			this.ShowWarning = toolStripButtonWarning.Checked;
		}

		private void toolStripButtonInfo_CheckStateChanged(object sender, EventArgs e)
		{
			this.ShowInfo = toolStripButtonInfo.Checked;
		}

		private void toolStripComboBoxView_SelectedIndexChanged(object sender, EventArgs e)
		{
			ExtendedLogViewerTag s = (ExtendedLogViewerTag)toolStripComboBoxView.SelectedItem;
			this.mMutex.WaitOne();
			foreach (SourceGrid.Grid.GridRow row in this.logGrid.Rows)
			{
				if (row.Tag != null)
				{
					ExtendedLogViewerTag t = (ExtendedLogViewerTag)row.Tag;
					row.Visible = (s.ID.Equals("0") || s.ID.Equals(t.ID));
				}
			}
			this.mMutex.ReleaseMutex();
		}
		#endregion

		private void timer_Tick(object sender, EventArgs e)
		{
			this.mMutex.WaitOne();
			if (this.mLogs.Count != 0)
			{
				foreach (ExtendedLogViewerItem i in this.mLogs)
				{
					this.UpdateUIAdd(i);
				}
				this.mLogs.Clear();
				this.logGrid.AutoSizeCells();
				this.logGrid.Columns[4].Width = this.logGrid.DisplayRectangle.Width;
				this.logGrid.Rows.AutoSize(true);
			}
			this.mMutex.ReleaseMutex();
		}


		#region IGlobalization Members

		private void UpdateCultureGrid()
		{
			foreach (SourceGrid.Grid.GridRow row in this.logGrid.Rows)
			{
				ExtendedLogViewerTag t = row.Tag as ExtendedLogViewerTag;
				if (t != null)
				{
					switch (t.LogLevel)
					{
						case ZForge.Controls.Logs.LogLevel.LOG_ERROR:
							this.logGrid[row.Index, 1].Value = Translator.Instance.T("错误");
							break;
						case ZForge.Controls.Logs.LogLevel.LOG_WARNING:
							this.logGrid[row.Index, 1].Value = Translator.Instance.T("警告");
							break;
						default:
							this.logGrid[row.Index, 1].Value = Translator.Instance.T("普通");
							break;
					}
				}
			}
		}

		public void UpdateCulture()
		{
			this.toolStripButtonClear.Text = Translator.Instance.T("清空日志");
			this.toolStripButtonError.Text = Translator.Instance.T("显示报警日志");
			this.toolStripButtonWarning.Text = Translator.Instance.T("显示警告日志");
			this.toolStripButtonInfo.Text = Translator.Instance.T("显示普通信息");

			this.logGrid[0, 1].Value = Translator.Instance.T("日志等级");
			this.logGrid[0, 2].Value = Translator.Instance.T("时间");
			this.logGrid[0, 3].Value = Translator.Instance.T("摄像头");
			this.logGrid[0, 4].Value = Translator.Instance.T("内容");

			this.mMutex.WaitOne();
			this.CountError = this.CountError;
			this.CountWarn = this.CountWarn;
			this.CountInfo = this.CountInfo;
			if (this.toolStripComboBoxView.Items.Count > 0)
			{
				this.toolStripComboBoxView.Items[0] = new ExtendedLogViewerTag(ZForge.Controls.Logs.LogLevel.LOG_INFO, "0", Translator.Instance.T("显示所有摄像头的日志信息"));
			}
			this.UpdateCultureGrid();
			this.mMutex.ReleaseMutex();
		}

		#endregion
	}
}
