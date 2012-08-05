namespace ZForge.Forms
{
	partial class QAForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QAForm));
			this.panelButton = new System.Windows.Forms.Panel();
			this.panelTopic = new System.Windows.Forms.Panel();
			this.labelTopic = new System.Windows.Forms.Label();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.textBoxInfo = new System.Windows.Forms.TextBox();
			this.panelMaster = new System.Windows.Forms.Panel();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.labelEmail = new System.Windows.Forms.Label();
			this.labelEmailDescription = new System.Windows.Forms.Label();
			this.textBoxEmail = new System.Windows.Forms.TextBox();
			this.reportUploader = new ZForge.Controls.Net.Downloader();
			this.pictureBoxTopic = new System.Windows.Forms.PictureBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.panelButton.SuspendLayout();
			this.panelTopic.SuspendLayout();
			this.groupBox.SuspendLayout();
			this.panelMaster.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxTopic)).BeginInit();
			this.SuspendLayout();
			// 
			// panelButton
			// 
			this.panelButton.Controls.Add(this.buttonCancel);
			this.panelButton.Controls.Add(this.buttonOk);
			this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelButton.Location = new System.Drawing.Point(0, 306);
			this.panelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelButton.Name = "panelButton";
			this.panelButton.Size = new System.Drawing.Size(463, 82);
			this.panelButton.TabIndex = 0;
			// 
			// panelTopic
			// 
			this.panelTopic.Controls.Add(this.labelTopic);
			this.panelTopic.Controls.Add(this.pictureBoxTopic);
			this.panelTopic.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTopic.Location = new System.Drawing.Point(0, 0);
			this.panelTopic.Name = "panelTopic";
			this.panelTopic.Size = new System.Drawing.Size(463, 59);
			this.panelTopic.TabIndex = 1;
			// 
			// labelTopic
			// 
			this.labelTopic.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelTopic.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelTopic.Location = new System.Drawing.Point(100, 0);
			this.labelTopic.Name = "labelTopic";
			this.labelTopic.Size = new System.Drawing.Size(363, 59);
			this.labelTopic.TabIndex = 1;
			this.labelTopic.Text = "发现运行错误";
			this.labelTopic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.textBoxInfo);
			this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox.Location = new System.Drawing.Point(0, 71);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(463, 151);
			this.groupBox.TabIndex = 2;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "错误信息";
			// 
			// textBoxInfo
			// 
			this.textBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxInfo.Location = new System.Drawing.Point(3, 19);
			this.textBoxInfo.Multiline = true;
			this.textBoxInfo.Name = "textBoxInfo";
			this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxInfo.Size = new System.Drawing.Size(457, 129);
			this.textBoxInfo.TabIndex = 0;
			this.textBoxInfo.WordWrap = false;
			// 
			// panelMaster
			// 
			this.panelMaster.Controls.Add(this.groupBox);
			this.panelMaster.Controls.Add(this.reportUploader);
			this.panelMaster.Controls.Add(this.tableLayoutPanel);
			this.panelMaster.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMaster.Location = new System.Drawing.Point(0, 59);
			this.panelMaster.Name = "panelMaster";
			this.panelMaster.Size = new System.Drawing.Size(463, 247);
			this.panelMaster.TabIndex = 1;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.labelEmail, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.labelEmailDescription, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.textBoxEmail, 1, 0);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 2;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(463, 71);
			this.tableLayoutPanel.TabIndex = 0;
			// 
			// labelEmail
			// 
			this.labelEmail.AutoSize = true;
			this.labelEmail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelEmail.Location = new System.Drawing.Point(3, 0);
			this.labelEmail.Name = "labelEmail";
			this.labelEmail.Size = new System.Drawing.Size(72, 29);
			this.labelEmail.TabIndex = 0;
			this.labelEmail.Text = "E-Mail地址:";
			this.labelEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelEmailDescription
			// 
			this.labelEmailDescription.AutoSize = true;
			this.labelEmailDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelEmailDescription.Location = new System.Drawing.Point(81, 29);
			this.labelEmailDescription.Name = "labelEmailDescription";
			this.labelEmailDescription.Size = new System.Drawing.Size(379, 42);
			this.labelEmailDescription.TabIndex = 1;
			this.labelEmailDescription.Text = "如果您输入了Email地址, 在我们修复了您发送的错误后, 会通过Email通知您.";
			// 
			// textBoxEmail
			// 
			this.textBoxEmail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxEmail.Location = new System.Drawing.Point(81, 3);
			this.textBoxEmail.Name = "textBoxEmail";
			this.textBoxEmail.Size = new System.Drawing.Size(379, 23);
			this.textBoxEmail.TabIndex = 2;
			// 
			// reportUploader
			// 
			this.reportUploader.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.reportUploader.Location = new System.Drawing.Point(0, 222);
			this.reportUploader.Name = "reportUploader";
			this.reportUploader.Output = null;
			this.reportUploader.Post = ((System.Collections.Generic.Dictionary<string, object>)(resources.GetObject("reportUploader.Post")));
			this.reportUploader.Size = new System.Drawing.Size(463, 25);
			this.reportUploader.TabIndex = 1;
			this.reportUploader.URL = null;
			this.reportUploader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.reportUploader_RunWorkerCompleted);
			// 
			// pictureBoxTopic
			// 
			this.pictureBoxTopic.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBoxTopic.Image = global::ZForge.Forms.Properties.Resources.cube_molecule_view_48;
			this.pictureBoxTopic.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxTopic.Name = "pictureBoxTopic";
			this.pictureBoxTopic.Size = new System.Drawing.Size(100, 59);
			this.pictureBoxTopic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxTopic.TabIndex = 0;
			this.pictureBoxTopic.TabStop = false;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Image = global::ZForge.Forms.Properties.Resources.delete_24;
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(262, 16);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(87, 41);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "关闭";
			this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonOk
			// 
			this.buttonOk.Image = global::ZForge.Forms.Properties.Resources.check_24;
			this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonOk.Location = new System.Drawing.Point(113, 16);
			this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(87, 41);
			this.buttonOk.TabIndex = 0;
			this.buttonOk.Text = "发送";
			this.buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// QAForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(463, 388);
			this.Controls.Add(this.panelMaster);
			this.Controls.Add(this.panelTopic);
			this.Controls.Add(this.panelButton);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "QAForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "发现运行错误";
			this.panelButton.ResumeLayout(false);
			this.panelTopic.ResumeLayout(false);
			this.groupBox.ResumeLayout(false);
			this.groupBox.PerformLayout();
			this.panelMaster.ResumeLayout(false);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxTopic)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelButton;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Panel panelTopic;
		private System.Windows.Forms.Label labelTopic;
		private System.Windows.Forms.PictureBox pictureBoxTopic;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.TextBox textBoxInfo;
		private System.Windows.Forms.Panel panelMaster;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label labelEmail;
		private System.Windows.Forms.Label labelEmailDescription;
		private System.Windows.Forms.TextBox textBoxEmail;
		private ZForge.Controls.Net.Downloader reportUploader;
	}
}