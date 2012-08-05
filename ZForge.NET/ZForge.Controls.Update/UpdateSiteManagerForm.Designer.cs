namespace ZForge.Controls.Update
{
	partial class UpdateSiteManagerForm
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
			ZForge.Controls.XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder3 = new ZForge.Controls.XPTable.Models.DataSourceColumnBinder();
			this.panelButton = new System.Windows.Forms.Panel();
			this.buttonOk = new System.Windows.Forms.Button();
			this.panelTopic = new System.Windows.Forms.Panel();
			this.labelTopic = new System.Windows.Forms.Label();
			this.panelMain = new System.Windows.Forms.Panel();
			this.tableList = new ZForge.Controls.XPTable.Models.Table();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
			this.pictureBoxTopic = new System.Windows.Forms.PictureBox();
			this.panelButton.SuspendLayout();
			this.panelTopic.SuspendLayout();
			this.panelMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tableList)).BeginInit();
			this.toolStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxTopic)).BeginInit();
			this.SuspendLayout();
			// 
			// panelButton
			// 
			this.panelButton.Controls.Add(this.buttonOk);
			this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelButton.Location = new System.Drawing.Point(0, 294);
			this.panelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelButton.Name = "panelButton";
			this.panelButton.Size = new System.Drawing.Size(502, 77);
			this.panelButton.TabIndex = 0;
			// 
			// buttonOk
			// 
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Location = new System.Drawing.Point(199, 8);
			this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(87, 44);
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "确认";
			this.buttonOk.UseVisualStyleBackColor = true;
			// 
			// panelTopic
			// 
			this.panelTopic.Controls.Add(this.labelTopic);
			this.panelTopic.Controls.Add(this.pictureBoxTopic);
			this.panelTopic.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTopic.Location = new System.Drawing.Point(0, 0);
			this.panelTopic.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelTopic.Name = "panelTopic";
			this.panelTopic.Size = new System.Drawing.Size(502, 50);
			this.panelTopic.TabIndex = 1;
			// 
			// labelTopic
			// 
			this.labelTopic.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelTopic.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelTopic.Location = new System.Drawing.Point(117, 0);
			this.labelTopic.Name = "labelTopic";
			this.labelTopic.Size = new System.Drawing.Size(385, 50);
			this.labelTopic.TabIndex = 1;
			this.labelTopic.Text = "管理更新服务器";
			this.labelTopic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.tableList);
			this.panelMain.Controls.Add(this.toolStrip);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 50);
			this.panelMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(502, 244);
			this.panelMain.TabIndex = 2;
			// 
			// tableList
			// 
			this.tableList.BorderColor = System.Drawing.Color.Black;
			this.tableList.DataMember = null;
			this.tableList.DataSourceColumnBinder = dataSourceColumnBinder3;
			this.tableList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableList.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tableList.Location = new System.Drawing.Point(0, 31);
			this.tableList.Name = "tableList";
			this.tableList.Size = new System.Drawing.Size(502, 213);
			this.tableList.TabIndex = 2;
			this.tableList.TopIndex = -1;
			this.tableList.TopItem = null;
			this.tableList.UnfocusedBorderColor = System.Drawing.Color.Black;
			this.tableList.EditingStopped += new ZForge.Controls.XPTable.Events.CellEditEventHandler(this.tableList_EditingStopped);
			this.tableList.SelectionChanged += new ZForge.Controls.XPTable.Events.SelectionEventHandler(this.tableList_SelectionChanged);
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripButtonDelete,
            this.toolStripButtonEdit});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(502, 31);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// toolStripButtonAdd
			// 
			this.toolStripButtonAdd.Image = global::ZForge.Controls.Update.Properties.Resources.add2_24;
			this.toolStripButtonAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonAdd.Name = "toolStripButtonAdd";
			this.toolStripButtonAdd.Size = new System.Drawing.Size(60, 28);
			this.toolStripButtonAdd.Text = "新增";
			this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
			// 
			// toolStripButtonDelete
			// 
			this.toolStripButtonDelete.Image = global::ZForge.Controls.Update.Properties.Resources.delete2_24;
			this.toolStripButtonDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonDelete.Name = "toolStripButtonDelete";
			this.toolStripButtonDelete.Size = new System.Drawing.Size(60, 28);
			this.toolStripButtonDelete.Text = "删除";
			this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
			// 
			// toolStripButtonEdit
			// 
			this.toolStripButtonEdit.Image = global::ZForge.Controls.Update.Properties.Resources.edit_24;
			this.toolStripButtonEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonEdit.Name = "toolStripButtonEdit";
			this.toolStripButtonEdit.Size = new System.Drawing.Size(60, 28);
			this.toolStripButtonEdit.Text = "修改";
			this.toolStripButtonEdit.Click += new System.EventHandler(this.toolStripButtonEdit_Click);
			// 
			// pictureBoxTopic
			// 
			this.pictureBoxTopic.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBoxTopic.Image = global::ZForge.Controls.Update.Properties.Resources.download_48;
			this.pictureBoxTopic.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxTopic.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.pictureBoxTopic.Name = "pictureBoxTopic";
			this.pictureBoxTopic.Size = new System.Drawing.Size(117, 50);
			this.pictureBoxTopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxTopic.TabIndex = 0;
			this.pictureBoxTopic.TabStop = false;
			// 
			// UpdateSiteManagerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(502, 371);
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.panelTopic);
			this.Controls.Add(this.panelButton);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateSiteManagerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "更新服务器管理";
			this.panelButton.ResumeLayout(false);
			this.panelTopic.ResumeLayout(false);
			this.panelMain.ResumeLayout(false);
			this.panelMain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tableList)).EndInit();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxTopic)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelButton;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Panel panelTopic;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Label labelTopic;
		private System.Windows.Forms.PictureBox pictureBoxTopic;
		private System.Windows.Forms.ToolStrip toolStrip;
		private ZForge.Controls.XPTable.Models.Table tableList;
		private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
		private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
		private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
	}
}