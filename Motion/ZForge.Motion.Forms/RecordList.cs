using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZForge.Motion.Core;
using System.Collections;
using System.IO;
using System.Threading;
using ZForge.Motion.Util;
using ZForge.Globalization;
using ZForge.Motion.Controls;

namespace ZForge.Motion.Forms
{
	internal partial class RecordList : UserControl, IGlobalization
	{
		private Motion.Controls.CameraBoard mBoard;
		private int mCount;
		private CameraBoardStyle mStyle = CameraBoardStyle.AVI;
		private CodecCollection mCC = new CodecCollection();

		public RecordList()
		{
			InitializeComponent();

			this.Count = 0;
		}

		#region Properties
		
		public Motion.Controls.CameraBoard BoardControl
		{
			get { return this.mBoard; }
			set { this.mBoard = value; }
		}

		protected int Count
		{
			get { return this.mCount; }
			set
			{
				this.mCount = value;
				this.toolStripLabelCount.Text = string.Format(Translator.Instance.T("{0}总数({1})"), this.StyleText, this.mCount);
			}
		}

		private string StyleText
		{
			get
			{
				switch (this.Style)
				{
					case CameraBoardStyle.AVI:
						return Translator.Instance.T("录像");
					case CameraBoardStyle.PIC:
						return Translator.Instance.T("截图");
				}
				return "";
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
				this.FileSystemWatcherEnabled = false;
				this.ClearAll();

				switch (this.mStyle)
				{
					case CameraBoardStyle.AVI:
						this.InitGridAVI();
						break;
					case CameraBoardStyle.PIC:
						this.InitGridPIC();
						break;
				}
				this.FileSystemWatcherEnabled = true;

				this.toolStripButtonAdd.ToolTipText = string.Format(Translator.Instance.T("添加选中的{0}到{0}浏览面板"), this.StyleText);
				this.toolStripButtonDelete.ToolTipText = string.Format(Translator.Instance.T("删除选中的{0}"), this.StyleText);
				this.toolStripButtonExport.ToolTipText = string.Format(Translator.Instance.T("导出选中的{0}"), this.StyleText);
				this.toolStripMenuItemView.Text = string.Format(Translator.Instance.T("添加选中的{0}到{0}浏览面板"), this.StyleText);
				this.toolStripMenuItemDelete.Text = string.Format(Translator.Instance.T("删除选中的{0}"), this.StyleText);
				this.toolStripMenuItemExport.Text = string.Format(Translator.Instance.T("导出选中的{0}"), this.StyleText);
				this.toolStripMenuItemMark.Text = string.Format(Translator.Instance.T("标记选中的{0}"), this.StyleText);
			}
		}

		public bool FileSystemWatcherEnabled
		{
			get
			{
				return this.fileSystemWatcher.EnableRaisingEvents;
			}
			set
			{
				if (value == true)
				{
					switch (this.Style)
					{
						case CameraBoardStyle.AVI:
							this.fileSystemWatcher.Path = MotionConfiguration.Instance.StorageAVI;
							this.fileSystemWatcher.Filter = "*.avi";
							break;
						case CameraBoardStyle.PIC:
							this.fileSystemWatcher.Path = MotionConfiguration.Instance.StoragePIC;
							this.fileSystemWatcher.Filter = "*.*";
							break;
					}
				}
				this.fileSystemWatcher.EnableRaisingEvents = value;
			}
		}

		#endregion

		#region BackgroundWorker

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			this.FillGrid();
		}

		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.toolStripProgressBar.Value++;
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.toolStripProgressBar.Value = this.toolStripProgressBar.Maximum;
		}

		#endregion

		public void ClearAll()
		{
			this.toolStripProgressBar.Maximum = this.GetItemCount();
			if (this.toolStripProgressBar.Maximum == 0)
			{
				this.toolStripProgressBar.Maximum = 1;
			}
			this.toolStripProgressBar.Value = 0;
			this.ClearGrid();
			if (this.BoardControl != null)
			{
				this.BoardControl.StopAll();
				this.BoardControl.ClearAll();
			}
		}

		#region Grid Operation

		private void InitGridAVI()
		{
			this.gridList.Rows.Clear();

			SourceGrid.Cells.ColumnHeader header;

			this.gridList.ColumnsCount = 6;
			this.gridList.FixedRows = 1;
			this.gridList.RowsCount = 1;

			//Font font = new Font(this.Font, FontStyle.Bold);
			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("状态"));
			//header.View.Font = font;
			this.gridList[0, 0] = header;

			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("摄像头"));
			this.gridList[0, 1] = header;
			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("录像开始时间"));
			this.gridList[0, 2] = header;
			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("帧数"));
			this.gridList[0, 3] = header;
			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("文件大小"));
			this.gridList[0, 4] = header;
			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("编码格式"));
			this.gridList[0, 5] = header;

			this.gridList.AutoSizeCells();
			this.gridList.AutoStretchColumnsToFitWidth = true;
			this.gridList.SelectionMode = SourceGrid.GridSelectionMode.Row;
		}

		private void InitGridPIC()
		{
			this.gridList.Rows.Clear();

			SourceGrid.Cells.ColumnHeader header;

			this.gridList.ColumnsCount = 4;
			this.gridList.FixedRows = 1;
			this.gridList.RowsCount = 1;

			//Font font = new Font(this.Font, FontStyle.Bold);
			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("状态"));
			//header.View.Font = font;
			this.gridList[0, 0] = header;

			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("摄像头"));
			this.gridList[0, 1] = header;
			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("截图时间"));
			this.gridList[0, 2] = header;
			header = new SourceGrid.Cells.ColumnHeader(Translator.Instance.T("文件大小"));
			this.gridList[0, 3] = header;

			this.gridList.AutoSizeCells();
			this.gridList.AutoStretchColumnsToFitWidth = true;
			this.gridList.SelectionMode = SourceGrid.GridSelectionMode.Row;
		}

		private int GetItemCount()
		{
			string[] files;
			switch (this.Style)
			{
				case CameraBoardStyle.AVI:
					files = Directory.GetFiles(MotionConfiguration.Instance.StorageAVI, "*.avi");
					return files.Length;
				case CameraBoardStyle.PIC:
					files = Directory.GetFiles(MotionConfiguration.Instance.StoragePIC, "*.*");
					return files.Length;
			}
			return 0;
		}
		
		delegate void FillGridDelegate();

		private void FillGrid()
		{
			if (this.InvokeRequired)
			{
				Invoke(new FillGridDelegate(FillGrid), null);
			}
			else
			{
				this.ClearGrid();
				this.FillGrid(RootClass.Instance.Children);
			}
		}

		private void FillGrid(ItemClassCollection its)
		{
			if (its == null)
			{
				return;
			}
			foreach (ItemClass i in its.Values)
			{
				CameraClass c = i as CameraClass;
				if (c != null)
				{
					this.FillGrid(c);
				}
				else
				{
					GroupClass g = i as GroupClass;
					if (g != null)
					{
						this.FillGrid(g.Children);
					}
				}
			}
		}

		private void FillGrid(CameraClass c)
		{
			switch (this.Style)
			{
				case CameraBoardStyle.AVI:
					foreach (AVIClass v in c.AVIs)
					{
						this.FillGrid(v);
						this.backgroundWorker.ReportProgress(1);
					}
					break;
				case CameraBoardStyle.PIC:
					foreach (PICClass v in c.PICs)
					{
						this.FillGrid(v);
						this.backgroundWorker.ReportProgress(1);
					}
					break;
			}
			this.gridList.AutoSizeCells();
		}

		private void FillRowMark(IFileClass v, int pos)
		{
			string text = Translator.Instance.T("未读");
			int ii = 0;

			switch (v.Mark)
			{
				case RecordMark.READ:
					text = Translator.Instance.T("已读");
					ii = 1;
					break;
				case RecordMark.IMPORTANT:
					text = Translator.Instance.T("重要");
					ii = 2;
					break;
				case RecordMark.UNREAD:
				default:
					text = Translator.Instance.T("未读");
					ii = 0;
					break;
			}
			this.gridList[pos, 0] = new SourceGrid.Cells.Cell(text);
			this.gridList[pos, 0].Image = this.imageList.Images[ii];
		}

		private void FillRow(AVIClass v, int pos)
		{
			this.FillRowMark(v, pos);
			if (v.Owner == null)
			{
				this.gridList[pos, 1] = new SourceGrid.Cells.Cell(Translator.Instance.T("**未知摄像头**"));
			}
			else
			{
				this.gridList[pos, 1] = new SourceGrid.Cells.Cell(v.Owner.FullName);
			}
			this.gridList[pos, 2] = new SourceGrid.Cells.Cell(v.TimeStamp);
			this.gridList[pos, 3] = new SourceGrid.Cells.Cell(v.Length);
			this.gridList[pos, 4] = new SourceGrid.Cells.Cell(v.FileSize);

			string codec = mCC.GetValue(v.Codec, string.Format(Translator.Instance.T("录像格式 ({0})"), v.Codec));
			this.gridList[pos, 5] = new SourceGrid.Cells.Cell(codec);
			this.gridList.Rows[pos].Tag = v;
		}

		private void FillRow(PICClass v, int pos)
		{
			this.FillRowMark(v, pos);
			if (v.Owner == null)
			{
				this.gridList[pos, 1] = new SourceGrid.Cells.Cell(Translator.Instance.T("**未知摄像头**"));
			}
			else
			{
				this.gridList[pos, 1] = new SourceGrid.Cells.Cell(v.Owner.FullName);
			}
			this.gridList[pos, 2] = new SourceGrid.Cells.Cell(v.TimeStamp);
			this.gridList[pos, 3] = new SourceGrid.Cells.Cell(v.FileSize);
			this.gridList.Rows[pos].Tag = v;
		}

		private void FillGrid(AVIClass v)
		{
			lock (this.gridList)
			{
				int pos = this.gridList.RowsCount;
				this.gridList.RowsCount++;

				this.FillRow(v, pos);

				this.Count = this.gridList.RowsCount - 1;
			}
		}

		private void FillGrid(PICClass v)
		{
			lock (this.gridList)
			{
				int pos = this.gridList.RowsCount;
				this.gridList.RowsCount++;

				this.FillRow(v, pos);

				this.Count = this.gridList.RowsCount - 1;
			}
		}

		private void InsertGrid(AVIClass v)
		{
			lock (this.gridList)
			{
				this.gridList.Rows.Insert(1);

				this.FillRow(v, 1);

				this.Count = this.gridList.RowsCount - 1;
				this.gridList.AutoSizeCells();
			}
		}

		private void InsertGrid(PICClass v)
		{
			lock (this.gridList)
			{
				this.gridList.Rows.Insert(1);

				this.FillRow(v, 1);

				this.Count = this.gridList.RowsCount - 1;
				this.gridList.AutoSizeCells();
			}
		}

		public void ClearGrid()
		{
			lock (this.gridList)
			{
				if (this.gridList.Rows.Count > 1)
				{
					this.gridList.Rows.RemoveRange(1, this.gridList.Rows.Count - 1);
				}
			}
			this.Count = 0;
		}

		private List<SourceGrid.Grid.GridRow> SelectedRows
		{
			get
			{
				List<SourceGrid.Grid.GridRow> r = new List<SourceGrid.Grid.GridRow>();
				int m = this.gridList.RowsCount;
				for (int n = 1; n < m; n++)
				{
					if (this.gridList.Selection.IsSelectedRow(n))
					{
						r.Add(this.gridList.Rows[n]);
					}
				}
				return r;
			}
		}

		public void RefreshList()
		{
			if (backgroundWorker.IsBusy)
			{
				return;
			}
			this.ClearAll();
			backgroundWorker.RunWorkerAsync();
		}

		private SourceGrid.Grid.GridRow Find(IFileClass c)
		{
			return this.Find(c.FileName);
		}

		private SourceGrid.Grid.GridRow Find(string fileName)
		{
			foreach (SourceGrid.Grid.GridRow row in gridList.Rows)
			{
				IFileClass f = row.Tag as IFileClass;
				if (f != null)
				{
					if (f.FileName.ToLower().Equals(fileName.ToLower()))
					{
						return row;
					}
				}
			}
			return null;
		}
		
		#endregion

		#region Grid & Board Operations

		private void Add()
		{
			if (this.BoardControl != null)
			{
				foreach (SourceGrid.Grid.GridRow row in this.SelectedRows)
				{
					switch (this.Style)
					{
						case CameraBoardStyle.AVI:
							AVIClass v = row.Tag as AVIClass;
							if (v.Mark == RecordMark.UNREAD || v.Mark == RecordMark.INVALID)
							{
								v.Mark = RecordMark.READ;
							}
							this.BoardControl.Add(v);
							break;
						case CameraBoardStyle.PIC:
							PICClass p = row.Tag as PICClass;
							if (p.Mark == RecordMark.UNREAD || p.Mark == RecordMark.INVALID)
							{
								p.Mark = RecordMark.READ;
							}
							this.BoardControl.Add(row.Tag as PICClass);
							break;
					}
				}
			}
		}

		private void Delete()
		{
			foreach (SourceGrid.Grid.GridRow row in this.SelectedRows)
			{
				IFileClass v = row.Tag as IFileClass;
				if (v != null)
				{
					if (this.BoardControl != null)
					{
						this.BoardControl.Remove(v);
					}
					v.Remove();
				}
			}
			this.Count = this.gridList.RowsCount - 1;
		}

		private void Export()
		{
			folderBrowserDialog.Description = string.Format(Translator.Instance.T("请选择{0}导出目录"), this.StyleText);
			DialogResult r = folderBrowserDialog.ShowDialog();
			if (DialogResult.OK == r && this.folderBrowserDialog.SelectedPath != null && this.folderBrowserDialog.SelectedPath.Length > 0)
			{
				foreach (SourceGrid.Grid.GridRow row in this.SelectedRows)
				{
					IFileClass i = row.Tag as IFileClass;
					if (i != null)
					{
						if (false == i.ExportToPath(this.folderBrowserDialog.SelectedPath))
						{
							return;
						}
					}
				}
			}
			string msg = string.Format(Translator.Instance.T("数据导出执行完毕!\n导出目录: ({0})"), this.folderBrowserDialog.SelectedPath);
			MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void SetMark(RecordMark mark)
		{
			if (this.SelectedRows.Count == 0)
			{
				string msg = string.Format(Translator.Instance.T("没有被选中的{0}. 您必须在下面的列表中先选择要标记的{0}."), this.StyleText);
				MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			foreach (SourceGrid.Grid.GridRow row in this.SelectedRows)
			{
				IFileClass f = row.Tag as IFileClass;
				if (f != null)
				{
					try
					{
						f.Mark = mark;
					}
					catch (System.IO.IOException ioe)
					{
						string msg = "";
						switch (this.Style)
						{
							case CameraBoardStyle.AVI:
								msg = string.Format(Translator.Instance.T("设置标记失败. (录像正在播放中, 您必须先停止录像的播放, 再对录像设置标记)\n详细信息:\n{0}"), ioe.Message);
								break;
							case CameraBoardStyle.PIC:
								msg = string.Format(Translator.Instance.T("设置标记失败.\n详细信息:\n{0}"), ioe.Message);
								break;
						}
						MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					catch (Exception ex)
					{
						string msg = string.Format(Translator.Instance.T("设置标记失败.\n详细信息:\n{0}"), ex.Message);
						MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		#endregion

		#region Toolbar & Context Menu

		private void gridList_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Point p = new Point(e.X, e.Y);
				int c = this.SelectedRows.Count;
				foreach (ToolStripItem i in this.contextMenuStrip.Items)
				{
					i.Enabled = (c > 0);
				}
				this.contextMenuStrip.Show(this.gridList, p);
			}
		}

		private void toolStripButtonRefresh_Click(object sender, EventArgs e)
		{
			this.RefreshList();
		}

		private void toolStripMenuItemView_Click(object sender, EventArgs e)
		{
			this.Add();
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			if (this.SelectedRows.Count == 0)
			{
				string msg = string.Format(Translator.Instance.T("没有被选中的{0}. 您必须在下面的列表中先选择要查看的{0}."), this.StyleText);
				MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			this.Add();
		}

		private void toolStripButtonDelete_Click(object sender, EventArgs e)
		{
			string msg;
			if (this.SelectedRows.Count == 0)
			{
				msg = string.Format(Translator.Instance.T("没有被选中的{0}. 您必须在下面的列表中先选择要删除的{0}."), this.StyleText);
				MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			msg = string.Format(Translator.Instance.T("您确定要删除这些选中的{0}吗?"), this.StyleText);
			if (DialogResult.Yes == MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				this.Delete();
			}
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			string msg = string.Format(Translator.Instance.T("您确定要删除这些选中的{0}吗?"), this.StyleText);
			if (DialogResult.Yes == MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				this.Delete();
			}
		}

		private void toolStripButtonExport_Click(object sender, EventArgs e)
		{
			if (this.SelectedRows.Count == 0)
			{
				string msg = string.Format(Translator.Instance.T("没有被选中的{0}. 您必须在下面的列表中先选择要导出的{0}."), this.StyleText);
				MessageBox.Show(msg, MotionPreference.Instance.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			this.Export();
		}

		private void toolStripMenuItemExport_Click(object sender, EventArgs e)
		{
			this.Export();
		}

		private void toolStripButtonMarkUnread_Click(object sender, EventArgs e)
		{
			this.SetMark(RecordMark.UNREAD);
		}

		private void toolStripButtonMarkRead_Click(object sender, EventArgs e)
		{
			this.SetMark(RecordMark.READ);
		}

		private void toolStripButtonMarkImportant_Click(object sender, EventArgs e)
		{
			this.SetMark(RecordMark.IMPORTANT);
		}

		private void toolStripMenuItemMarkUnread_Click(object sender, EventArgs e)
		{
			this.SetMark(RecordMark.UNREAD);
		}

		private void toolStripMenuItemMarkRead_Click(object sender, EventArgs e)
		{
			this.SetMark(RecordMark.READ);
		}

		private void toolStripMenuItemMarkImportant_Click(object sender, EventArgs e)
		{
			this.SetMark(RecordMark.IMPORTANT);
		}

		#endregion

		#region FileSystemWatcher

		private void fileSystemWatcher_Deleted(object sender, System.IO.FileSystemEventArgs e)
		{
			foreach (SourceGrid.Grid.GridRow row in gridList.Rows)
			{
				IFileClass c = row.Tag as IFileClass;
				if (c != null)
				{
					if (e.FullPath.ToLower().Equals(c.FileName.ToLower()))
					{
						if (this.BoardControl != null)
						{
							this.BoardControl.Remove(c);
						}
						gridList.Rows.Remove(row.Index);
						this.Count = this.gridList.RowsCount - 1;
						break;
					}
				}
			}
		}

		private void fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			IFileClass f = null;
			switch (this.Style)
			{
				case CameraBoardStyle.AVI:
					AVIClass v = new AVIClass(e.FullPath, null);
					f = v as IFileClass;
					break;
				case CameraBoardStyle.PIC:
					PICClass p = new PICClass(e.FullPath, null);
					f = p as IFileClass;
					break;
			}
			if (f != null && f.IsValid() && f.Owner != null)
			{
				SourceGrid.Grid.GridRow row = this.Find(e.OldFullPath);
				if (row == null)
				{
					row = this.Find(e.FullPath);
					if (row == null)
					{
						switch (this.Style)
						{
							case CameraBoardStyle.AVI:
								this.InsertGrid(f as AVIClass);
								break;
							case CameraBoardStyle.PIC:
								this.InsertGrid(f as PICClass);
								break;
						}
						return;
					}
				}
				IFileClass c = row.Tag as IFileClass;
				c.FileName = f.FileName;
				this.FillRowMark(c, row.Index);
				this.gridList.InvalidateCell(this.gridList[row.Index, 0]);
			}
		}

		private void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
		{
			switch (this.Style)
			{
				case CameraBoardStyle.PIC:
					PICClass c = new PICClass(e.FullPath, null);
					if (c.IsValid() && c.Owner != null)
					{
						this.InsertGrid(c);
					}
					break;
				case CameraBoardStyle.AVI:
					break;
			}
		}
		#endregion

		#region IGlobalization Members

		private void UpdateCultureGrid()
		{
			foreach (SourceGrid.Grid.GridRow row in this.gridList.Rows)
			{
				IFileClass t = row.Tag as IFileClass;
				if (t != null)
				{
					switch (t.Mark)
					{
						case RecordMark.READ:
							this.gridList[row.Index, 0].Value = Translator.Instance.T("已读");
							break;
						case RecordMark.IMPORTANT:
							this.gridList[row.Index, 0].Value = Translator.Instance.T("重要");
							break;
						case RecordMark.UNREAD:
						default:
							this.gridList[row.Index, 0].Value = Translator.Instance.T("未读");
							break;
					}
				}
			}
		}

		public void UpdateCulture()
		{
			this.toolStripButtonRefresh.Text = Translator.Instance.T("刷新");
			this.toolStripButtonRefresh.ToolTipText = Translator.Instance.T("刷新列表内容");
			this.toolStripButtonAdd.Text = Translator.Instance.T("查看");
			this.toolStripButtonAdd.ToolTipText = string.Format(Translator.Instance.T("添加选中的{0}到{0}浏览面板"), this.StyleText);
			this.toolStripButtonDelete.Text = Translator.Instance.T("删除");
			this.toolStripButtonDelete.ToolTipText = string.Format(Translator.Instance.T("删除选中的{0}"), this.StyleText);
			this.toolStripButtonExport.Text = Translator.Instance.T("导出");
			this.toolStripButtonExport.ToolTipText = string.Format(Translator.Instance.T("导出选中的{0}"), this.StyleText);
			this.toolStripButtonMarkUnread.Text = Translator.Instance.T("标记为未读");
			this.toolStripButtonMarkRead.Text = Translator.Instance.T("标记为已读");
			this.toolStripButtonMarkImportant.Text = Translator.Instance.T("标记为重要");

			this.toolStripMenuItemView.Text = string.Format(Translator.Instance.T("添加选中的{0}到{0}浏览面板"), this.StyleText);
			this.toolStripMenuItemDelete.Text = string.Format(Translator.Instance.T("删除选中的{0}"), this.StyleText);
			this.toolStripMenuItemExport.Text = string.Format(Translator.Instance.T("导出选中的{0}"), this.StyleText);
			this.toolStripMenuItemMark.Text = string.Format(Translator.Instance.T("标记选中的{0}"), this.StyleText);
			this.toolStripMenuItemMarkUnread.Text = Translator.Instance.T("未读");
			this.toolStripMenuItemMarkRead.Text = Translator.Instance.T("已读");
			this.toolStripMenuItemMarkImportant.Text = Translator.Instance.T("重要");

			lock (this.gridList)
			{
				switch (this.Style)
				{
					case CameraBoardStyle.AVI:
						if (this.gridList.ColumnsCount == 6)
						{
							this.gridList[0, 0].Value = Translator.Instance.T("状态");
							this.gridList[0, 1].Value = Translator.Instance.T("摄像头");
							this.gridList[0, 2].Value = Translator.Instance.T("录像开始时间");
							this.gridList[0, 3].Value = Translator.Instance.T("帧数");
							this.gridList[0, 4].Value = Translator.Instance.T("文件大小");
							this.gridList[0, 5].Value = Translator.Instance.T("编码格式");
						}
						break;
					case CameraBoardStyle.PIC:
						if (this.gridList.ColumnsCount == 4)
						{
							this.gridList[0, 0].Value = Translator.Instance.T("状态");
							this.gridList[0, 1].Value = Translator.Instance.T("摄像头");
							this.gridList[0, 2].Value = Translator.Instance.T("截图时间");
							this.gridList[0, 3].Value = Translator.Instance.T("文件大小");
						}
						break;
				}
				this.UpdateCultureGrid();
			}
			
			this.Count = this.Count;
		}

		#endregion

	}
}
