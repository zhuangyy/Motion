using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZForge.Controls.XPTable.Models;
using ZForge.Globalization;
using ZForge.Controls.XPTable.Renderers;

namespace ZForge.Controls.Logs
{
	public partial class ChangeLogViewer : UserControl
	{
		public ChangeLogViewer()
		{
			InitializeComponent();
			ListInitialize();
		}

		public ChangeLogItem[] ChangeLogCollection
		{
			set
			{
				this.ListUpdate(value);
			}
		}

		protected virtual ColumnModel ColumnInitialize()
		{
			TextColumn col1 = new TextColumn(Translator.Instance.T("版本"));
			
			ImageColumn col2 = new ImageColumn(Translator.Instance.T("类别"));
			col2.DrawText = true;

			TextColumn col3 = new TextColumn(Translator.Instance.T("内容"));

			ColumnModel r = new ColumnModel(new Column[] { col1, col2, col3 });
			foreach (Column col in r.Columns)
			{
				col.Resizable = true;
				col.Editable = false;
			}
			return r;
		}

		private void ListInitialize()
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

		private void ListUpdate(ChangeLogItem[] logs)
		{
			Table table = this.tableList;       // The Table control on a form - already initialised
			table.TableModel.Rows.Clear();
			foreach (ChangeLogItem i in logs)
			{
				this.ListAdd(i);
			}
			table.ColumnModel.ResizeColumnWidth();
		}

		private void ListAdd(ChangeLogItem log)
		{
			Table table = this.tableList;       // The Table control on a form - already initialised
			Cell c = new Cell();
			switch (log.T)
			{
				case ChangeLogLevel.ADD:
					c = new Cell(Translator.Instance.T("新增"), global::ZForge.Controls.Logs.Properties.Resources.add_16);
					break;
				case ChangeLogLevel.BUGFIX:
					c = new Cell(Translator.Instance.T("BUGFIX"), global::ZForge.Controls.Logs.Properties.Resources.bug_yellow_16);
					break;
				case ChangeLogLevel.REMOVE:
					c = new Cell(Translator.Instance.T("移除"), global::ZForge.Controls.Logs.Properties.Resources.delete_16);
					break;
				default:
					c = new Cell(Translator.Instance.T("更改"), global::ZForge.Controls.Logs.Properties.Resources.document_into_16);
					break;
			}
			Row row = new Row(
				new Cell[] {
					new Cell(log.Version),
					c,
					new Cell(log.Message)
				}
			);
			row.Tag = log;
			table.TableModel.Rows.Add(row);
		}

		public void Add(ChangeLogItem i)
		{
			this.ListAdd(i);
		}

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
