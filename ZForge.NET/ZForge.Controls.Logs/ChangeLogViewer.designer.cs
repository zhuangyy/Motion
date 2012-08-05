namespace ZForge.Controls.Logs
{
	partial class ChangeLogViewer
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
			ZForge.Controls.XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder1 = new ZForge.Controls.XPTable.Models.DataSourceColumnBinder();
			this.tableList = new ZForge.Controls.XPTable.Models.Table();
			((System.ComponentModel.ISupportInitialize)(this.tableList)).BeginInit();
			this.SuspendLayout();
			// 
			// tableList
			// 
			this.tableList.BorderColor = System.Drawing.Color.Black;
			this.tableList.DataMember = null;
			this.tableList.DataSourceColumnBinder = dataSourceColumnBinder1;
			this.tableList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableList.FullRowSelect = true;
			this.tableList.GridLines = ZForge.Controls.XPTable.Models.GridLines.Both;
			this.tableList.Location = new System.Drawing.Point(0, 0);
			this.tableList.Name = "tableList";
			this.tableList.Size = new System.Drawing.Size(418, 190);
			this.tableList.TabIndex = 0;
			this.tableList.TopIndex = -1;
			this.tableList.TopItem = null;
			this.tableList.UnfocusedBorderColor = System.Drawing.Color.Black;
			this.tableList.FontChanged += new System.EventHandler(this.tableList_FontChanged);
			// 
			// ChangeLogViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableList);
			this.Name = "ChangeLogViewer";
			this.Size = new System.Drawing.Size(418, 190);
			((System.ComponentModel.ISupportInitialize)(this.tableList)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private ZForge.Controls.XPTable.Models.Table tableList;
	}
}
