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

namespace ZForge.SA.Komponent
{
	public partial class SALicenseControl : UserControl
	{
		public SALicenseControl()
		{
			InitializeComponent();
			ListInitialize();

			SALicense sa = new SALicense();
			this.textBoxHostID.Text = sa.HostID;

			this.openFileDialog.Filter = Translator.Instance.T("许可证文件") + " (*.xml)|*.xml";
			this.openFileDialog.Title = Translator.Instance.T("导入许可证");
		}

		protected virtual ColumnModel ColumnInitialize()
		{
			TextColumn col1 = new TextColumn(Translator.Instance.T("模块"));
			TextColumn col2 = new TextColumn(Translator.Instance.T("许可证版本"));
			TextColumn col3 = new TextColumn(Translator.Instance.T("授权用户"));
			TextColumn col4 = new TextColumn(Translator.Instance.T("备注"));

			ColumnModel r = new ColumnModel(new Column[] { col1, col2, col3, col4 });
			foreach (Column col in r.Columns)
			{
				col.Resizable = true;
				col.Editable = false;
			}
			return r;
		}

		private void ListInitialize()
		{
			Table table = this.tableModules;       // The Table control on a form - already initialised

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

		private void ListSetup(List<SALicense> lics)
		{
			Table table = this.tableModules;       // The Table control on a form - already initialised
			table.TableModel.Rows.Clear();
			if (lics != null)
			{
				foreach (SALicense i in lics)
				{
					this.ListAdd(i);
				}
				table.ColumnModel.ResizeColumnWidth();
			}
		}

		private void ListAdd(SALicense lic)
		{
			Table table = this.tableModules;       // The Table control on a form - already initialised
			lic.Load();
			Row row = new Row(
				new Cell[] {
					new Cell(lic.Product),
					new Cell(lic.Version),
					new Cell(lic.Username),
					new Cell(lic.Message)
				}
			);
			row.Tag = lic;
			table.TableModel.Rows.Add(row);
		}

		private void ListUpdate()
		{
			Table table = this.tableModules;       // The Table control on a form - already initialised
			foreach (Row row in table.TableModel.Rows)
			{
				SALicense sa = row.Tag as SALicense;
				if (sa != null)
				{
					row.Cells[0].Text = sa.Product;
					row.Cells[1].Text = sa.Version;
					row.Cells[2].Text = sa.Username;
					row.Cells[3].Text = sa.Message;
				}
			}
			table.ColumnModel.ResizeColumnWidth();
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<SALicense> LicenseInstances
		{
			get
			{
				List<SALicense> r = new List<SALicense>();
				Table table = this.tableModules;       // The Table control on a form - already initialised
				foreach (Row row in table.TableModel.Rows)
				{
					SALicense sa = row.Tag as SALicense;
					if (sa != null)
					{
						r.Add(sa);
					}
				}
				return r;
			}
			set
			{
				this.ListSetup(value);
			}
		}

		public bool Import(string filename)
		{
			List<SALicense> lics = this.LicenseInstances;
			bool r = false;
			foreach (SALicense lic in lics)
			{
				if (lic.Import(filename))
				{
					r = true;
					break;
				}
				lic.Load();
			}
			if (r)
			{
				this.ListUpdate();
			}
			return r;
		}

		public bool Import()
		{
			DialogResult r = this.openFileDialog.ShowDialog();
			if (r == DialogResult.OK && string.IsNullOrEmpty(this.openFileDialog.FileName) == false)
			{
				if (false == this.Import(this.openFileDialog.FileName))
				{
					MessageBox.Show(Translator.Instance.T("导入许可证失败."), Translator.Instance.T("导入许可证"), MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				return true;
			}
			return false;
		}

		private void toolStripButtonImport_Click(object sender, EventArgs e)
		{
			this.Import();
		}
	}
}
