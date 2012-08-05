using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZForge.Controls.XPTable.Models;
using ZForge.Controls.XPTable.Renderers;
using ZForge.Globalization;
using System.Threading;

namespace ZForge.Controls.Logs
{
	public partial class LogViewer : UserControl
	{
		private int mCountError;
		private int mCountWarn;
		private int mCountInfo;
		private bool mAutoScrollToLast = true;

		public LogViewer()
		{
			InitializeComponent();
			this.FilterEnabled = false;
			ListInitialize();
		}

		protected virtual ColumnModel ColumnInitialize()
		{
			ImageColumn col1 = new ImageColumn(Translator.Instance.T("日志类型"), 100);
			col1.DrawText = true;

			TextColumn col2 = new TextColumn(Translator.Instance.T("时间"), 100);
			TextColumn col3 = new TextColumn(Translator.Instance.T("内容"), 100);

			ColumnModel r = new ColumnModel(new Column[] { col1, col2, col3 });
			foreach (Column col in r.Columns)
			{
				col.Resizable = true;
				col.Editable = false;
			}
			return r;
		}

		protected virtual void ListInitialize()
		{
			Table table = this.tableList;       // The Table control on a form - already initialised

			table.BeginUpdate();
			table.SelectionStyle = SelectionStyle.ListView;
			table.GridLines = GridLines.None;
			table.FullRowSelect = true;
			table.NoItemsText = "";
			//table.EnableWordWrap = true;

			table.ColumnModel = this.ColumnInitialize();

			Font ft = SystemFonts.MessageBoxFont;
			table.HeaderFont = new Font(ft.FontFamily.Name, ft.Size, FontStyle.Bold);

			table.TableModel = new TableModel();
			table.TableModel.RowHeight = TextRenderer.MeasureText("ABCbp", table.Font).Height + 2;

			GradientHeaderRenderer gradientRenderer = new GradientHeaderRenderer();
			table.HeaderRenderer = gradientRenderer;

			table.EndUpdate();
		}

		protected virtual Row ListAdd(LogItem log)
		{
			Table table = this.tableList;       // The Table control on a form - already initialised
			Cell s = new Cell(log.Message);
			Cell c;
			switch (log.Level)
			{
				case LogLevel.LOG_ERROR:
					c = new Cell(Translator.Instance.T("错误"), global::ZForge.Controls.Logs.Properties.Resources.scroll_error_16);
					s.ForeColor = Color.Red;
					c.ForeColor = s.ForeColor;
					this.CountError++;
					break;
				case LogLevel.LOG_WARNING:
					c = new Cell(Translator.Instance.T("警告"), global::ZForge.Controls.Logs.Properties.Resources.scroll_warning_16);
					s.ForeColor = Color.SteelBlue;
					c.ForeColor = s.ForeColor;
					this.CountWarn++;
					break;
				default:
					c = new Cell(Translator.Instance.T("信息"), global::ZForge.Controls.Logs.Properties.Resources.scroll_information_16);
					s.ForeColor = Color.Green;
					c.ForeColor = s.ForeColor;
					this.CountInfo++;
					break;
			}
			Row row = new Row(
				new Cell[] {
					c,
					new Cell(log.Timestamp.ToString()),
					s
				}
			);
			row.Tag = log;
			table.TableModel.Rows.Add(row);
			return row;
		}

		delegate void LogAddCallback(LogItem log);

		public void LogAdd(LogLevel level, string msg)
		{
			this.LogAdd(new LogItem(level, msg));
		}

		public void LogAdd(LogItem log)
		{
			if (this.InvokeRequired)
			{
				LogAddCallback d = new LogAddCallback(LogAdd);
				this.Invoke(d, new object[] { log });
			}
			else
			{
				lock (this.tableList)
				{
					Row row = ListAdd(log);
					AutosizeColumnWidth();
					if (this.AutoScrollToLast)
					{
						this.tableList.TopIndex = this.CalcTableTopIndex();
					}
				}
			}
		}

		private int CalcTableTopIndex()
		{
			Table table = this.tableList;       // The Table control on a form - already initialised
			int c = table.TableModel.Rows.Count;
			if (c == 0) {
				return 0;
			}
			int ht = table.ClientRectangle.Height - table.HeaderHeight;
			if (table.HScroll)
			{
				ht -= SystemInformation.HorizontalScrollBarHeight;
			}
			if (table.EnableWordWrap == false)
			{
				return Math.Max(0, c - (ht / table.RowHeight));
			}
			else
			{
				int h = 0;
				int n;
				for (n = c; n > 0; n --) {
					h += table.TableModel.Rows[n - 1].Height;
					if (h >= ht)
					{
						break;
					}
				}
				return n;
			}
		}

		public void Clear()
		{
			Table table = this.tableList;       // The Table control on a form - already initialised
			lock (table)
			{
				if (table.TableModel.Rows.Count > 1)
				{
					table.TableModel.Rows.Clear();
				}
				this.CountError = 0;
				this.CountWarn = 0;
				this.CountInfo = 0;
			}
		}

		public virtual void AutosizeColumnWidth()
		{
			Table table = this.tableList;       // The Table control on a form - already initialised
			if (table.ColumnModel != null)
			{
				table.ColumnModel.ResizeColumnWidth();
			}
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

		#region Properties

		public LogFilter LogFilter
		{
			get
			{
				LogFilter r = LogFilter.LOG_NONE;
				r |= this.toolStripButtonError.Checked ? LogFilter.LOG_ERROR : LogFilter.LOG_NONE;
				r |= this.toolStripButtonInfo.Checked ? LogFilter.LOG_INFO : LogFilter.LOG_NONE;
				r |= this.toolStripButtonWarning.Checked ? LogFilter.LOG_WARNING : LogFilter.LOG_NONE;
				return r;
			}
			set
			{
				this.toolStripButtonError.Checked = ((value & LogFilter.LOG_ERROR) != LogFilter.LOG_NONE);
				this.toolStripButtonInfo.Checked = ((value & LogFilter.LOG_INFO) != LogFilter.LOG_NONE);
				this.toolStripButtonWarning.Checked = ((value & LogFilter.LOG_WARNING) != LogFilter.LOG_NONE);
				
				Table table = this.tableList;
				foreach (Row row in table.TableModel.Rows)
				{
					LogItem log = row.Tag as LogItem;
					if (log != null)
					{
						LogFilter f = (LogFilter)log.Level;
						row.Visible = ((value & f) == f);
					}
				}
			}
		}

		public bool FilterEnabled
		{
			set
			{
				this.toolStripButtonError.Visible = value;
				this.toolStripButtonInfo.Visible = value;
				this.toolStripButtonWarning.Visible = value;
				this.toolStripSeparator1.Visible = value;
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

		public string Logs
		{
			get
			{
				Table table = this.tableList;       // The Table control on a form - already initialised
				string r = "";
				foreach (Row row in table.TableModel.Rows)
				{
					r += row.Cells[2].Text.Trim() + "\n";
				}
				return r;
			}
		}

		public bool AutoScrollToLast
		{
			get { return mAutoScrollToLast; }
			set { mAutoScrollToLast = value; }
		}

		#endregion

		#region Toolstrip buttons

		private void toolStripButtonClear_Click(object sender, EventArgs e)
		{
			this.Clear();
		}

		private void toolStripButtonError_CheckedChanged(object sender, EventArgs e)
		{
			this.LogFilter = this.LogFilter;
		}

		private void toolStripButtonWarning_CheckedChanged(object sender, EventArgs e)
		{
			this.LogFilter = this.LogFilter;
		}

		private void toolStripButtonInfo_CheckedChanged(object sender, EventArgs e)
		{
			this.LogFilter = this.LogFilter;
		}

		private void toolStripButtonCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetDataObject(this.Logs, true);
		}

		#endregion

		private void tableList_FontChanged(object sender, EventArgs e)
		{
			Table table = this.tableList;       // The Table control on a form - already initialised
			if (table.TableModel != null)
			{
				table.TableModel.RowHeight = TextRenderer.MeasureText("ABCbp", table.Font).Height + 2;
				if (table.ColumnModel != null)
				{
					table.ColumnModel.ResizeColumnWidth();
				}
			}
		}
	}
}
