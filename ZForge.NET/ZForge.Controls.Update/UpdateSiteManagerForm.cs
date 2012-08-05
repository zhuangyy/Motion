using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZForge.Controls.XPTable.Models;
using ZForge.Controls.XPTable.Renderers;
using ZForge.Globalization;
using ZForge.Controls.XPTable.Editors;

namespace ZForge.Controls.Update
{
	public partial class UpdateSiteManagerForm : Form
	{
		private UpdateSiteCollection mUpdateSiteCollection;

		public UpdateSiteManagerForm()
		{
			InitializeComponent();
			ListInitialize();
		}

		protected virtual ColumnModel ColumnInitialize()
		{
			TextColumn col1 = new TextColumn(Translator.Instance.T("名称"), 100);
			TextColumn col2 = new TextColumn(Translator.Instance.T("地址(URL)"), 100);

			ColumnModel r = new ColumnModel(new Column[] { col1, col2 });
			foreach (Column col in r.Columns)
			{
				col.Resizable = true;
				col.Editable = true;
			}
			return r;
		}

		protected virtual void ListInitialize()
		{
			Table table = this.tableList;       // The Table control on a form - already initialised

			table.BeginUpdate();
			table.SelectionStyle = SelectionStyle.Grid;
			table.GridLines = GridLines.None;
			table.FullRowSelect = false;
			table.NoItemsText = "";
			table.EnableWordWrap = false;

			table.ColumnModel = this.ColumnInitialize();

			Font ft = SystemFonts.MessageBoxFont;
			table.HeaderFont = new Font(ft.FontFamily.Name, ft.Size, FontStyle.Bold);

			table.TableModel = new TableModel();
			table.TableModel.RowHeight = TextRenderer.MeasureText("ABCbp", table.Font).Height + 4;

			GradientHeaderRenderer gradientRenderer = new GradientHeaderRenderer();
			table.HeaderRenderer = gradientRenderer;

			table.EndUpdate();
		}

		protected virtual Row ListAdd(UpdateSite us)
		{
			Table table = this.tableList;       // The Table control on a form - already initialised
			Row row = new Row(
				new Cell[] {
					new Cell(us.Description),
					new Cell(us.URL)
				}
			);
			row.Tag = us;
			table.TableModel.Rows.Add(row);
			this.AutosizeColumnWidth();
			return row;
		}

		protected virtual void AutosizeColumnWidth()
		{
			Table table = this.tableList;       // The Table control on a form - already initialised
			foreach (Column c in table.ColumnModel.Columns)
			{
				int w1 = c.GetMaximumContentWidth();
				int w2 = c.GetMinimumHeaderWidth();
				c.Width = Math.Max(w1, w2);
			}
		}

		#region Properties

		public UpdateSiteCollection UpdateSiteCollection
		{
			get
			{
				if (mUpdateSiteCollection == null)
				{
					mUpdateSiteCollection = new UpdateSiteCollection();
				}
				return mUpdateSiteCollection;
			}
			set
			{
				mUpdateSiteCollection = value;
				if (mUpdateSiteCollection != null)
				{
					foreach (UpdateSite us in this.mUpdateSiteCollection)
					{
						if (false == us.IsUneditable)
						{
							this.ListAdd(us);
						}
					}
				}
				this.SwapToolStripButtonStates();
			}
		}

		private Cell SelectedCell
		{
			get
			{
				Row row = this.SelectedRow;
				if (row == null)
				{
					return null;
				}
				Table table = this.tableList;       // The Table control on a form - already initialised
				for (int n = 0; n < table.ColumnCount; n++)
				{
					if (table.TableModel.Selections.IsCellSelected(row.Index, n))
					{
						return row.Cells[n];
					}
				}
				return null;
			}
		}

		private Row SelectedRow
		{
			get
			{
				Table table = this.tableList;       // The Table control on a form - already initialised
				Row[] rows = table.TableModel.Selections.SelectedItems;
				if (rows.Length == 0)
				{
					return null;
				}
				return rows[0];
			}
		}

		private UpdateSite SelectedUpdateSite
		{
			get
			{
				Row row = this.SelectedRow;
				if (row == null)
				{
					return null;
				}
				return row.Tag as UpdateSite;
			}
		}

		#endregion

		#region Toolstrip Actions

		private void SwapToolStripButtonStates()
		{
			Cell v = this.SelectedCell;
			this.toolStripButtonDelete.Enabled = (v != null);
			this.toolStripButtonEdit.Enabled = (v != null);
		}

		private void toolStripButtonAdd_Click(object sender, EventArgs e)
		{
			UpdateSite us = new UpdateSite("http://127.0.0.1", Translator.Instance.T("新增更新服务器"));
			this.ListAdd(us);
			this.UpdateSiteCollection.Add(us);
		}

		private void toolStripButtonDelete_Click(object sender, EventArgs e)
		{
			Row row = this.SelectedRow;
			if (row != null)
			{
				this.UpdateSiteCollection.Remove(row.Tag as UpdateSite);

				Table table = this.tableList;       // The Table control on a form - already initialised
				table.TableModel.Rows.Remove(row);
				SwapToolStripButtonStates();
			}
		}

		private void toolStripButtonEdit_Click(object sender, EventArgs e)
		{
			Cell cell = this.SelectedCell;
			if (cell != null)
			{
				Table table = this.tableList;       // The Table control on a form - already initialised
				table.EditCell(cell.Row.Index, cell.Index);
			}
		}

		private void tableList_EditingStopped(object sender, ZForge.Controls.XPTable.Events.CellEditEventArgs e)
		{
			Table table = this.tableList;
			Row row = table.TableModel.Rows[e.Row];
			UpdateSite us = row.Tag as UpdateSite;
			TextCellEditor editor = e.Editor as TextCellEditor;
			string v = (editor.TextBox.Text != null) ? editor.TextBox.Text.Trim() : null;
			if (v == null || v.Length == 0)
			{
				e.Cancel = true;
				return;
			}
			switch (e.Column)
			{
				case 0:
					us.Description = v;
					break;
				case 1:
					List<string> msgs = new List<string>();
					if (false == ZForge.Configuration.Validator.ValidateURL(msgs, Translator.Instance.T("地址(URL)"), v))
					{
						MessageBox.Show(ZForge.Configuration.Validator.MergeMessages(msgs), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
						e.Cancel = true;
					}
					else
					{
						us.URL = v;
					}
					break;
			}
			this.AutosizeColumnWidth();
		}

		private void tableList_SelectionChanged(object sender, ZForge.Controls.XPTable.Events.SelectionEventArgs e)
		{
			this.SwapToolStripButtonStates();
		}

		#endregion
	}
}