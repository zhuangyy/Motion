namespace ZForge.Motion.Forms
{
	partial class PerferenceForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerferenceForm));
			this.panelSub = new System.Windows.Forms.Panel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.panelMain = new System.Windows.Forms.Panel();
			this.pGridPerf = new ZForge.Controls.PropertyGridEx.PropertyGridEx();
			this.panelSub.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelSub
			// 
			this.panelSub.Controls.Add(this.buttonCancel);
			this.panelSub.Controls.Add(this.buttonOk);
			this.panelSub.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelSub.Location = new System.Drawing.Point(0, 343);
			this.panelSub.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelSub.Name = "panelSub";
			this.panelSub.Size = new System.Drawing.Size(541, 83);
			this.panelSub.TabIndex = 0;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Image = global::ZForge.Motion.Forms.Properties.Resources.cancel;
			this.buttonCancel.Location = new System.Drawing.Point(292, 13);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(90, 52);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "取消";
			this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.Image = global::ZForge.Motion.Forms.Properties.Resources.ok;
			this.buttonOk.Location = new System.Drawing.Point(148, 13);
			this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(89, 52);
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "确认";
			this.buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.pGridPerf);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(541, 343);
			this.panelMain.TabIndex = 1;
			// 
			// pGridPerf
			// 
			// 
			// 
			// 
			this.pGridPerf.DocCommentDescription.AccessibleName = "";
			this.pGridPerf.DocCommentDescription.AutoEllipsis = true;
			this.pGridPerf.DocCommentDescription.Cursor = System.Windows.Forms.Cursors.Default;
			this.pGridPerf.DocCommentDescription.Location = new System.Drawing.Point(3, 21);
			this.pGridPerf.DocCommentDescription.Name = "";
			this.pGridPerf.DocCommentDescription.Size = new System.Drawing.Size(535, 34);
			this.pGridPerf.DocCommentDescription.TabIndex = 1;
			this.pGridPerf.DocCommentImage = null;
			// 
			// 
			// 
			this.pGridPerf.DocCommentTitle.Cursor = System.Windows.Forms.Cursors.Default;
			this.pGridPerf.DocCommentTitle.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
			this.pGridPerf.DocCommentTitle.Location = new System.Drawing.Point(3, 3);
			this.pGridPerf.DocCommentTitle.Name = "";
			this.pGridPerf.DocCommentTitle.Size = new System.Drawing.Size(535, 18);
			this.pGridPerf.DocCommentTitle.TabIndex = 0;
			this.pGridPerf.DocCommentTitle.UseMnemonic = false;
			this.pGridPerf.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pGridPerf.Location = new System.Drawing.Point(0, 0);
			this.pGridPerf.Name = "pGridPerf";
			this.pGridPerf.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.pGridPerf.SelectedObject = ((object)(resources.GetObject("pGridPerf.SelectedObject")));
			this.pGridPerf.ShowCustomProperties = true;
			this.pGridPerf.Size = new System.Drawing.Size(541, 343);
			this.pGridPerf.TabIndex = 0;
			// 
			// 
			// 
			this.pGridPerf.ToolStrip.AccessibleName = "ToolBar";
			this.pGridPerf.ToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
			this.pGridPerf.ToolStrip.AllowMerge = false;
			this.pGridPerf.ToolStrip.AutoSize = false;
			this.pGridPerf.ToolStrip.CanOverflow = false;
			this.pGridPerf.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.pGridPerf.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.pGridPerf.ToolStrip.Location = new System.Drawing.Point(0, 1);
			this.pGridPerf.ToolStrip.Name = "";
			this.pGridPerf.ToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
			this.pGridPerf.ToolStrip.Size = new System.Drawing.Size(541, 25);
			this.pGridPerf.ToolStrip.TabIndex = 1;
			this.pGridPerf.ToolStrip.TabStop = true;
			this.pGridPerf.ToolStrip.Text = "PropertyGridToolBar";
			// 
			// PerferenceForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(541, 426);
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.panelSub);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "PerferenceForm";
			this.ShowInTaskbar = false;
			this.Text = "使用偏好";
			this.panelSub.ResumeLayout(false);
			this.panelMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelSub;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Panel panelMain;
		private ZForge.Controls.PropertyGridEx.PropertyGridEx pGridPerf;
	}
}