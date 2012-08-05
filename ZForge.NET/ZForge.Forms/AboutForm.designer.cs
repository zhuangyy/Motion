namespace ZForge.Forms
{
	partial class AboutForm
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
			this.linkLabelURL = new System.Windows.Forms.LinkLabel();
			this.labelVersion = new System.Windows.Forms.Label();
			this.labelOrg = new System.Windows.Forms.Label();
			this.labelProduct = new System.Windows.Forms.Label();
			this.panelButton = new System.Windows.Forms.Panel();
			this.buttonClose = new System.Windows.Forms.Button();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.panelTopic = new System.Windows.Forms.Panel();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.changelogViewer = new ZForge.Controls.Logs.ChangeLogViewer();
			this.panelButton.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			this.panelTopic.SuspendLayout();
			this.groupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// linkLabelURL
			// 
			this.linkLabelURL.AutoSize = true;
			this.linkLabelURL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkLabelURL.Location = new System.Drawing.Point(3, 75);
			this.linkLabelURL.Name = "linkLabelURL";
			this.linkLabelURL.Size = new System.Drawing.Size(368, 25);
			this.linkLabelURL.TabIndex = 1;
			this.linkLabelURL.TabStop = true;
			this.linkLabelURL.Text = "http://www.syan.com.cn";
			this.linkLabelURL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.linkLabelURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelURL_LinkClicked);
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelVersion.Location = new System.Drawing.Point(3, 28);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(368, 17);
			this.labelVersion.TabIndex = 2;
			this.labelVersion.Text = "Version 1.1.1";
			this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelOrg
			// 
			this.labelOrg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelOrg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelOrg.Location = new System.Drawing.Point(3, 45);
			this.labelOrg.Name = "labelOrg";
			this.labelOrg.Size = new System.Drawing.Size(368, 30);
			this.labelOrg.TabIndex = 3;
			this.labelOrg.Text = "江苏先安科技有限责任公司";
			this.labelOrg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelProduct
			// 
			this.labelProduct.AutoSize = true;
			this.labelProduct.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelProduct.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelProduct.Location = new System.Drawing.Point(3, 0);
			this.labelProduct.Name = "labelProduct";
			this.labelProduct.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
			this.labelProduct.Size = new System.Drawing.Size(368, 28);
			this.labelProduct.TabIndex = 4;
			this.labelProduct.Text = "TLS VPN Client";
			this.labelProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelButton
			// 
			this.panelButton.Controls.Add(this.buttonClose);
			this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelButton.Location = new System.Drawing.Point(0, 287);
			this.panelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelButton.Name = "panelButton";
			this.panelButton.Size = new System.Drawing.Size(461, 75);
			this.panelButton.TabIndex = 6;
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Image = global::ZForge.Forms.Properties.Resources.gtk_about;
			this.buttonClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonClose.Location = new System.Drawing.Point(187, 7);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(84, 45);
			this.buttonClose.TabIndex = 5;
			this.buttonClose.Text = "关闭";
			this.buttonClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.labelProduct, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.labelVersion, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.labelOrg, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.linkLabelURL, 0, 3);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(87, 0);
			this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 4;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(374, 100);
			this.tableLayoutPanel.TabIndex = 7;
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(87, 100);
			this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxLogo.TabIndex = 0;
			this.pictureBoxLogo.TabStop = false;
			// 
			// panelTopic
			// 
			this.panelTopic.Controls.Add(this.tableLayoutPanel);
			this.panelTopic.Controls.Add(this.pictureBoxLogo);
			this.panelTopic.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTopic.Location = new System.Drawing.Point(0, 0);
			this.panelTopic.Name = "panelTopic";
			this.panelTopic.Size = new System.Drawing.Size(461, 100);
			this.panelTopic.TabIndex = 6;
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.changelogViewer);
			this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox.Location = new System.Drawing.Point(0, 100);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(461, 187);
			this.groupBox.TabIndex = 7;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "更新历史纪录";
			// 
			// changelogViewer
			// 
			this.changelogViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.changelogViewer.Location = new System.Drawing.Point(3, 19);
			this.changelogViewer.Name = "changelogViewer";
			this.changelogViewer.Size = new System.Drawing.Size(455, 165);
			this.changelogViewer.TabIndex = 0;
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(461, 362);
			this.Controls.Add(this.groupBox);
			this.Controls.Add(this.panelTopic);
			this.Controls.Add(this.panelButton);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.panelButton.ResumeLayout(false);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.panelTopic.ResumeLayout(false);
			this.groupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private System.Windows.Forms.LinkLabel linkLabelURL;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelOrg;
		private System.Windows.Forms.Label labelProduct;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Panel panelButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Panel panelTopic;
		private System.Windows.Forms.GroupBox groupBox;
		private ZForge.Controls.Logs.ChangeLogViewer changelogViewer;
	}
}