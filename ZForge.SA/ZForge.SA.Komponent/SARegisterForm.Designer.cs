namespace ZForge.SA.Komponent
{
	partial class SARegisterForm
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
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.licenseControl = new ZForge.SA.Komponent.SALicenseControl();
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.panelTopic = new System.Windows.Forms.Panel();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.labelProduct = new System.Windows.Forms.Label();
			this.labelVersion = new System.Windows.Forms.Label();
			this.labelOrg = new System.Windows.Forms.Label();
			this.linkLabelURL = new System.Windows.Forms.LinkLabel();
			this.panelButton = new System.Windows.Forms.Panel();
			this.buttonImport = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.groupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			this.panelTopic.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.panelButton.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.licenseControl);
			this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox.Location = new System.Drawing.Point(0, 100);
			this.groupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox.Name = "groupBox";
			this.groupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.groupBox.Size = new System.Drawing.Size(429, 193);
			this.groupBox.TabIndex = 10;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "注册信息";
			// 
			// licenseControl
			// 
			this.licenseControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.licenseControl.Location = new System.Drawing.Point(3, 20);
			this.licenseControl.Name = "licenseControl";
			this.licenseControl.Size = new System.Drawing.Size(423, 169);
			this.licenseControl.TabIndex = 0;
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(101, 100);
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
			this.panelTopic.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelTopic.Name = "panelTopic";
			this.panelTopic.Size = new System.Drawing.Size(429, 100);
			this.panelTopic.TabIndex = 9;
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
			this.tableLayoutPanel.Location = new System.Drawing.Point(101, 0);
			this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 4;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(328, 100);
			this.tableLayoutPanel.TabIndex = 7;
			// 
			// labelProduct
			// 
			this.labelProduct.AutoSize = true;
			this.labelProduct.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelProduct.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelProduct.Location = new System.Drawing.Point(3, 0);
			this.labelProduct.Name = "labelProduct";
			this.labelProduct.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
			this.labelProduct.Size = new System.Drawing.Size(322, 32);
			this.labelProduct.TabIndex = 4;
			this.labelProduct.Text = "TLS VPN Client";
			this.labelProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelVersion.Location = new System.Drawing.Point(3, 32);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(322, 17);
			this.labelVersion.TabIndex = 2;
			this.labelVersion.Text = "Version 1.1.1";
			this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelOrg
			// 
			this.labelOrg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelOrg.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelOrg.Location = new System.Drawing.Point(3, 49);
			this.labelOrg.Name = "labelOrg";
			this.labelOrg.Size = new System.Drawing.Size(322, 25);
			this.labelOrg.TabIndex = 3;
			this.labelOrg.Text = "江苏先安科技有限责任公司";
			this.labelOrg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// linkLabelURL
			// 
			this.linkLabelURL.AutoSize = true;
			this.linkLabelURL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkLabelURL.Location = new System.Drawing.Point(3, 74);
			this.linkLabelURL.Name = "linkLabelURL";
			this.linkLabelURL.Size = new System.Drawing.Size(322, 26);
			this.linkLabelURL.TabIndex = 1;
			this.linkLabelURL.TabStop = true;
			this.linkLabelURL.Text = "http://www.syan.com.cn";
			this.linkLabelURL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// panelButton
			// 
			this.panelButton.Controls.Add(this.buttonImport);
			this.panelButton.Controls.Add(this.buttonClose);
			this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelButton.Location = new System.Drawing.Point(0, 293);
			this.panelButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.panelButton.Name = "panelButton";
			this.panelButton.Size = new System.Drawing.Size(429, 69);
			this.panelButton.TabIndex = 8;
			// 
			// buttonImport
			// 
			this.buttonImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonImport.Location = new System.Drawing.Point(237, 5);
			this.buttonImport.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonImport.Name = "buttonImport";
			this.buttonImport.Size = new System.Drawing.Size(84, 45);
			this.buttonImport.TabIndex = 6;
			this.buttonImport.Text = "导入许可证";
			this.buttonImport.UseVisualStyleBackColor = true;
			this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Image = global::ZForge.SA.Komponent.Properties.Resources.exit_24;
			this.buttonClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonClose.Location = new System.Drawing.Point(101, 5);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(84, 45);
			this.buttonClose.TabIndex = 5;
			this.buttonClose.Text = "关闭";
			this.buttonClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// SARegisterForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(429, 362);
			this.Controls.Add(this.groupBox);
			this.Controls.Add(this.panelTopic);
			this.Controls.Add(this.panelButton);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SARegisterForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "RegisterForm";
			this.groupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.panelTopic.ResumeLayout(false);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.panelButton.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private System.Windows.Forms.Panel panelTopic;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label labelProduct;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelOrg;
		private System.Windows.Forms.LinkLabel linkLabelURL;
		private System.Windows.Forms.Panel panelButton;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonImport;
		private SALicenseControl licenseControl;
	}
}