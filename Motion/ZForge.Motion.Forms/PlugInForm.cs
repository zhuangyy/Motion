using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZForge.Motion.PlugIns;
using ZForge.Globalization;
using ZForge.Motion.Util;
using ZForge.PlugIn;
using ZForge.Controls.XPTable.Models;
using ZForge.Controls.XPTable.Renderers;

namespace ZForge.Motion.Forms
{
	public partial class PlugInForm : Form, IGlobalization
	{
		public PlugInForm()
		{
			InitializeComponent();
			MotionPreference.Instance.UpdateUI(this);

			InitPlugInGrid();
			FillGrid();
			InitPlugInTabs();
		}

		private void InitPlugInGrid()
		{
			Table table = this.tablePlugIns;       // The Table control on a form - already initialised

			table.BeginUpdate();
			table.SelectionStyle = SelectionStyle.ListView;
			table.GridLines = GridLines.None;
			table.FullRowSelect = true;
			table.NoItemsText = "";
			//table.EnableWordWrap = false;

			TextColumn col1 = new TextColumn(Translator.Instance.T("类别"), 100);
			TextColumn col2 = new TextColumn(Translator.Instance.T("名称"), 100);
			TextColumn col3 = new TextColumn(Translator.Instance.T("作者"), 100);
			TextColumn col4 = new TextColumn(Translator.Instance.T("版本"), 100);
			TextColumn col5 = new TextColumn(Translator.Instance.T("描述"), 100);
			TextColumn col6 = new TextColumn(Translator.Instance.T("文件"), 100);

			table.ColumnModel = new ColumnModel(new Column[] { col1, col2, col3, col4, col5, col6 });
			foreach (Column col in table.ColumnModel.Columns)
			{
				col.Resizable = true;
				col.Editable = false;
			}

			Font ft = SystemFonts.MessageBoxFont;
			table.HeaderFont = new Font(ft.FontFamily.Name, ft.Size, FontStyle.Bold);

			table.TableModel = new TableModel();
			table.TableModel.RowHeight = TextRenderer.MeasureText("ABCbp", table.Font).Height + 2;

			GradientHeaderRenderer gradientRenderer = new GradientHeaderRenderer();
			table.HeaderRenderer = gradientRenderer;

			table.EndUpdate();
		}

		private void FillGrid()
		{
			Table table = this.tablePlugIns;       // The Table control on a form - already initialised

			MotionPlugIns ps = new MotionPlugIns();
			foreach (AvailablePlugIn<IPlugIn> p in ps.AvailablePlugInCollection)
			{
				Row row = new Row();
				row.Cells.Add(new Cell(ps.GetPlugInCategory(p.Instance)));
				row.Cells.Add(new Cell(p.Instance.Name));
				row.Cells.Add(new Cell(p.Instance.Author));
				row.Cells.Add(new Cell(p.Instance.Version));
				row.Cells.Add(new Cell(p.Instance.Description));
				row.Cells.Add(new Cell(p.AssemblyPath));

				table.TableModel.Rows.Add(row);
			}
			table.ColumnModel.ResizeColumnWidth();
		}

		private void InitPlugInTabs()
		{
			MotionPlugIns ps = new MotionPlugIns();
			foreach (AvailablePlugIn<IPlugIn> p in ps.AvailablePlugInCollection)
			{
				this.InitPlugInTab(p);
			}
		}

		private void InitPlugInTab(AvailablePlugIn<IPlugIn> p)
		{
			ZForge.Controls.Logs.ChangeLogViewer v = new ZForge.Controls.Logs.ChangeLogViewer();
			TabPage page = new TabPage(p.Instance.Name);

			v.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			v.Dock = System.Windows.Forms.DockStyle.Fill;
			List<ZForge.Controls.Logs.ChangeLogItem> list = p.Instance.ChangeLogList;
			v.ChangeLogCollection = list.ToArray();

			page.Controls.Add(v);
			page.UseVisualStyleBackColor = true;
			this.tabControl.Controls.Add(page);
		}

		#region IGlobalization Members

		public void UpdateCulture()
		{
			this.labelTop.Text = Translator.Instance.T("已安装的插件:");
			this.buttonOk.Text = Translator.Instance.T("关闭");
			this.tabPagePlugInList.Text = Translator.Instance.T("插件列表");
			this.Text = string.Format(Translator.Instance.T("{0} 插件浏览"), MotionPreference.Instance.ProductFullName);
		}

		#endregion
	}
}