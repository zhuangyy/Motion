namespace ZForge.SA.Komponent
{
	partial class SALicenseControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			ZForge.Controls.XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder2 = new ZForge.Controls.XPTable.Models.DataSourceColumnBinder();
			this.tableModules = new ZForge.Controls.XPTable.Models.Table();
			this.tableLayoutPanelHostID = new System.Windows.Forms.TableLayoutPanel();
			this.labelHostID = new System.Windows.Forms.Label();
			this.textBoxHostID = new System.Windows.Forms.TextBox();
			this.panelMain = new System.Windows.Forms.Panel();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.toolStripButtonImport = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.tableModules)).BeginInit();
			this.tableLayoutPanelHostID.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableModules
			// 
			this.tableModules.BorderColor = System.Drawing.Color.Black;
			this.tableModules.DataMember = null;
			this.tableModules.DataSourceColumnBinder = dataSourceColumnBinder2;
			this.tableModules.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableModules.Location = new System.Drawing.Point(0, 29);
			this.tableModules.Name = "tableModules";
			this.tableModules.Size = new System.Drawing.Size(349, 90);
			this.tableModules.TabIndex = 1;
			this.tableModules.TopIndex = -1;
			this.tableModules.TopItem = null;
			this.tableModules.UnfocusedBorderColor = System.Drawing.Color.Black;
			// 
			// tableLayoutPanelHostID
			// 
			this.tableLayoutPanelHostID.ColumnCount = 2;
			this.tableLayoutPanelHostID.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelHostID.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelHostID.Controls.Add(this.labelHostID, 0, 0);
			this.tableLayoutPanelHostID.Controls.Add(this.textBoxHostID, 1, 0);
			this.tableLayoutPanelHostID.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanelHostID.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelHostID.Name = "tableLayoutPanelHostID";
			this.tableLayoutPanelHostID.RowCount = 1;
			this.tableLayoutPanelHostID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelHostID.Size = new System.Drawing.Size(349, 29);
			this.tableLayoutPanelHostID.TabIndex = 0;
			// 
			// labelHostID
			// 
			this.labelHostID.AutoSize = true;
			this.labelHostID.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelHostID.Location = new System.Drawing.Point(3, 0);
			this.labelHostID.Name = "labelHostID";
			this.labelHostID.Size = new System.Drawing.Size(46, 29);
			this.labelHostID.TabIndex = 0;
			this.labelHostID.Text = "序列码:";
			this.labelHostID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxHostID
			// 
			this.textBoxHostID.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxHostID.Location = new System.Drawing.Point(55, 3);
			this.textBoxHostID.Name = "textBoxHostID";
			this.textBoxHostID.ReadOnly = true;
			this.textBoxHostID.Size = new System.Drawing.Size(291, 20);
			this.textBoxHostID.TabIndex = 1;
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.tableModules);
			this.panelMain.Controls.Add(this.tableLayoutPanelHostID);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 31);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(349, 119);
			this.panelMain.TabIndex = 2;
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonImport});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(349, 31);
			this.toolStrip.TabIndex = 2;
			this.toolStrip.Text = "toolStrip1";
			// 
			// toolStripButtonImport
			// 
			this.toolStripButtonImport.Image = global::ZForge.SA.Komponent.Properties.Resources.lock_open_24;
			this.toolStripButtonImport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonImport.Name = "toolStripButtonImport";
			this.toolStripButtonImport.Size = new System.Drawing.Size(96, 28);
			this.toolStripButtonImport.Text = "导入许可证";
			this.toolStripButtonImport.Click += new System.EventHandler(this.toolStripButtonImport_Click);
			// 
			// SALicenseControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.toolStrip);
			this.Name = "SALicenseControl";
			this.Size = new System.Drawing.Size(349, 150);
			((System.ComponentModel.ISupportInitialize)(this.tableModules)).EndInit();
			this.tableLayoutPanelHostID.ResumeLayout(false);
			this.tableLayoutPanelHostID.PerformLayout();
			this.panelMain.ResumeLayout(false);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ZForge.Controls.XPTable.Models.Table tableModules;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHostID;
		private System.Windows.Forms.Label labelHostID;
		private System.Windows.Forms.TextBox textBoxHostID;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton toolStripButtonImport;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
	}
}
