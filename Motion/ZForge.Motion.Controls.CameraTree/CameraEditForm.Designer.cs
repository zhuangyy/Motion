namespace ZForge.Motion.Controls
{
	partial class CameraEditForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraEditForm));
			this.panelButton = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.pGridCamera = new ZForge.Controls.PropertyGridEx.PropertyGridEx();
			this.toolStripEdit = new System.Windows.Forms.ToolStrip();
			this.cameraViewTest = new Motion.Controls.CameraView();
			this.panelButton.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelButton
			// 
			this.panelButton.Controls.Add(this.btnCancel);
			this.panelButton.Controls.Add(this.btnOk);
			this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelButton.Location = new System.Drawing.Point(0, 395);
			this.panelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelButton.Name = "panelButton";
			this.panelButton.Size = new System.Drawing.Size(670, 75);
			this.panelButton.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.AutoEllipsis = true;
			this.btnCancel.Image = global::ZForge.Motion.Controls.Properties.Resources.gtk_cancel;
			this.btnCancel.Location = new System.Drawing.Point(393, 7);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(87, 49);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "取消";
			this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Image = global::ZForge.Motion.Controls.Properties.Resources.gtk_ok;
			this.btnOk.Location = new System.Drawing.Point(159, 7);
			this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(87, 49);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "确定";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Controls.Add(this.cameraViewTest);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(670, 395);
			this.panel2.TabIndex = 2;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.pGridCamera);
			this.panel3.Controls.Add(this.toolStripEdit);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(348, 395);
			this.panel3.TabIndex = 10;
			// 
			// pGridCamera
			// 
			// 
			// 
			// 
			this.pGridCamera.DocCommentDescription.AccessibleName = "";
			this.pGridCamera.DocCommentDescription.AutoEllipsis = true;
			this.pGridCamera.DocCommentDescription.Cursor = System.Windows.Forms.Cursors.Default;
			this.pGridCamera.DocCommentDescription.Location = new System.Drawing.Point(3, 21);
			this.pGridCamera.DocCommentDescription.Name = "";
			this.pGridCamera.DocCommentDescription.Size = new System.Drawing.Size(342, 34);
			this.pGridCamera.DocCommentDescription.TabIndex = 1;
			this.pGridCamera.DocCommentImage = null;
			// 
			// 
			// 
			this.pGridCamera.DocCommentTitle.Cursor = System.Windows.Forms.Cursors.Default;
			this.pGridCamera.DocCommentTitle.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold);
			this.pGridCamera.DocCommentTitle.Location = new System.Drawing.Point(3, 3);
			this.pGridCamera.DocCommentTitle.Name = "";
			this.pGridCamera.DocCommentTitle.Size = new System.Drawing.Size(342, 18);
			this.pGridCamera.DocCommentTitle.TabIndex = 0;
			this.pGridCamera.DocCommentTitle.UseMnemonic = false;
			this.pGridCamera.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pGridCamera.Location = new System.Drawing.Point(0, 0);
			this.pGridCamera.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
			this.pGridCamera.Name = "pGridCamera";
			this.pGridCamera.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.pGridCamera.SelectedObject = ((object)(resources.GetObject("pGridCamera.SelectedObject")));
			this.pGridCamera.ShowCustomProperties = true;
			this.pGridCamera.Size = new System.Drawing.Size(348, 370);
			this.pGridCamera.TabIndex = 12;
			// 
			// 
			// 
			this.pGridCamera.ToolStrip.AccessibleName = "ToolBar";
			this.pGridCamera.ToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
			this.pGridCamera.ToolStrip.AllowMerge = false;
			this.pGridCamera.ToolStrip.AutoSize = false;
			this.pGridCamera.ToolStrip.CanOverflow = false;
			this.pGridCamera.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.pGridCamera.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.pGridCamera.ToolStrip.Location = new System.Drawing.Point(0, 1);
			this.pGridCamera.ToolStrip.Name = "";
			this.pGridCamera.ToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
			this.pGridCamera.ToolStrip.Size = new System.Drawing.Size(348, 25);
			this.pGridCamera.ToolStrip.TabIndex = 1;
			this.pGridCamera.ToolStrip.TabStop = true;
			this.pGridCamera.ToolStrip.Text = "PropertyGridToolBar";
			this.pGridCamera.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pGridCamera_PropertyValueChanged);
			// 
			// toolStripEdit
			// 
			this.toolStripEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStripEdit.Location = new System.Drawing.Point(0, 370);
			this.toolStripEdit.Name = "toolStripEdit";
			this.toolStripEdit.Size = new System.Drawing.Size(348, 25);
			this.toolStripEdit.TabIndex = 13;
			this.toolStripEdit.Text = "toolStrip1";
			// 
			// cameraViewTest
			// 
			this.cameraViewTest.AlarmCount = 0;
			this.cameraViewTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cameraViewTest.CameraClass = null;
			this.cameraViewTest.Dock = System.Windows.Forms.DockStyle.Right;
			this.cameraViewTest.EditMode = false;
			this.cameraViewTest.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cameraViewTest.HeightFixed = false;
			this.cameraViewTest.Location = new System.Drawing.Point(348, 0);
			this.cameraViewTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cameraViewTest.Name = "cameraViewTest";
			this.cameraViewTest.ShowBanner = false;
			this.cameraViewTest.ShowEdit = false;
			this.cameraViewTest.ShowMotionRect = false;
			this.cameraViewTest.Size = new System.Drawing.Size(322, 395);
			this.cameraViewTest.Status = 2;
			this.cameraViewTest.TabIndex = 9;
			this.cameraViewTest.ViewRatio = 1F;
			// 
			// CameraEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(670, 470);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panelButton);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "CameraEditForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "配置摄像头";
			this.panelButton.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelButton;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Panel panel2;
		private CameraView cameraViewTest;
		private System.Windows.Forms.Panel panel3;
		private ZForge.Controls.PropertyGridEx.PropertyGridEx pGridCamera;
		private System.Windows.Forms.ToolStrip toolStripEdit;
	}
}