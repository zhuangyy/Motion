namespace ZForge.Motion.Forms
{
	partial class PlugInForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			ZForge.Controls.XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder1 = new ZForge.Controls.XPTable.Models.DataSourceColumnBinder();
			this.panelTop = new System.Windows.Forms.Panel();
			this.labelTop = new System.Windows.Forms.Label();
			this.panelBottom = new System.Windows.Forms.Panel();
			this.buttonOk = new System.Windows.Forms.Button();
			this.panelMain = new System.Windows.Forms.Panel();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPagePlugInList = new System.Windows.Forms.TabPage();
			this.tablePlugIns = new ZForge.Controls.XPTable.Models.Table();
			this.panelTop.SuspendLayout();
			this.panelBottom.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPagePlugInList.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tablePlugIns)).BeginInit();
			this.SuspendLayout();
			// 
			// panelTop
			// 
			this.panelTop.Controls.Add(this.labelTop);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(527, 35);
			this.panelTop.TabIndex = 0;
			// 
			// labelTop
			// 
			this.labelTop.AutoSize = true;
			this.labelTop.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelTop.Location = new System.Drawing.Point(3, 10);
			this.labelTop.Name = "labelTop";
			this.labelTop.Size = new System.Drawing.Size(92, 17);
			this.labelTop.TabIndex = 0;
			this.labelTop.Text = "已安装的插件：";
			// 
			// panelBottom
			// 
			this.panelBottom.Controls.Add(this.buttonOk);
			this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBottom.Location = new System.Drawing.Point(0, 297);
			this.panelBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(527, 73);
			this.panelBottom.TabIndex = 1;
			// 
			// buttonOk
			// 
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Image = global::ZForge.Motion.Forms.Properties.Resources.ok;
			this.buttonOk.Location = new System.Drawing.Point(214, 7);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(86, 47);
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "关闭";
			this.buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.buttonOk.UseVisualStyleBackColor = true;
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.tabControl);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 35);
			this.panelMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(527, 262);
			this.panelMain.TabIndex = 2;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPagePlugInList);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(527, 262);
			this.tabControl.TabIndex = 0;
			// 
			// tabPagePlugInList
			// 
			this.tabPagePlugInList.Controls.Add(this.tablePlugIns);
			this.tabPagePlugInList.Location = new System.Drawing.Point(4, 26);
			this.tabPagePlugInList.Name = "tabPagePlugInList";
			this.tabPagePlugInList.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePlugInList.Size = new System.Drawing.Size(519, 232);
			this.tabPagePlugInList.TabIndex = 0;
			this.tabPagePlugInList.Text = "插件列表";
			this.tabPagePlugInList.UseVisualStyleBackColor = true;
			// 
			// tablePlugIns
			// 
			this.tablePlugIns.BorderColor = System.Drawing.Color.Black;
			this.tablePlugIns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tablePlugIns.DataMember = null;
			this.tablePlugIns.DataSourceColumnBinder = dataSourceColumnBinder1;
			this.tablePlugIns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tablePlugIns.FullRowSelect = true;
			this.tablePlugIns.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tablePlugIns.Location = new System.Drawing.Point(3, 3);
			this.tablePlugIns.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tablePlugIns.Name = "tablePlugIns";
			this.tablePlugIns.Size = new System.Drawing.Size(513, 226);
			this.tablePlugIns.TabIndex = 1;
			this.tablePlugIns.TopIndex = -1;
			this.tablePlugIns.TopItem = null;
			this.tablePlugIns.UnfocusedBorderColor = System.Drawing.Color.Black;
			// 
			// PlugInForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(527, 370);
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.panelBottom);
			this.Controls.Add(this.panelTop);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "PlugInForm";
			this.ShowInTaskbar = false;
			this.Text = "插件浏览";
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			this.panelBottom.ResumeLayout(false);
			this.panelMain.ResumeLayout(false);
			this.tabControl.ResumeLayout(false);
			this.tabPagePlugInList.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tablePlugIns)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Label labelTop;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPagePlugInList;
		private ZForge.Controls.XPTable.Models.Table tablePlugIns;
	}
}