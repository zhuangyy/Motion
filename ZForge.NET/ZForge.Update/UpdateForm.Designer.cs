namespace ZForge.Update
{
	partial class UpdateForm
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
			this.panelBottom = new System.Windows.Forms.Panel();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonCheck = new System.Windows.Forms.Button();
			this.updater = new ZForge.Controls.Update.Updater();
			this.panelBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelBottom
			// 
			this.panelBottom.Controls.Add(this.buttonClose);
			this.panelBottom.Controls.Add(this.buttonCheck);
			this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBottom.Location = new System.Drawing.Point(0, 362);
			this.panelBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(552, 63);
			this.panelBottom.TabIndex = 0;
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(318, 8);
			this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(87, 46);
			this.buttonClose.TabIndex = 1;
			this.buttonClose.Text = "关闭";
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// buttonCheck
			// 
			this.buttonCheck.Location = new System.Drawing.Point(144, 8);
			this.buttonCheck.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.buttonCheck.Name = "buttonCheck";
			this.buttonCheck.Size = new System.Drawing.Size(87, 44);
			this.buttonCheck.TabIndex = 0;
			this.buttonCheck.Text = "自动更新";
			this.buttonCheck.UseVisualStyleBackColor = true;
			this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
			// 
			// updater
			// 
			this.updater.Dock = System.Windows.Forms.DockStyle.Fill;
			this.updater.Key = null;
			this.updater.Location = new System.Drawing.Point(0, 0);
			this.updater.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.updater.Name = "updater";
			this.updater.Size = new System.Drawing.Size(552, 362);
			this.updater.TabIndex = 1;
			// 
			// UpdateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(552, 425);
			this.Controls.Add(this.updater);
			this.Controls.Add(this.panelBottom);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "UpdateForm";
			this.Text = "UpdateForm";
			this.panelBottom.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonCheck;
		private ZForge.Controls.Update.Updater updater;
	}
}