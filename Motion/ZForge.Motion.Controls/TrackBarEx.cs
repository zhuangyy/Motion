using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ZForge.Motion.Controls
{
	public partial class TrackBarEx : UserControl
	{
		private string title;

		public TrackBarEx()
		{
			InitializeComponent();
		}

		public decimal Maximum {
			get
			{
				return this.NumericUpDown.Maximum;
			}
			set
			{
				this.NumericUpDown.Maximum = value;
			}
		}

		public decimal Minimum
		{
			get
			{
				return this.NumericUpDown.Minimum;
			}
			set
			{
				this.NumericUpDown.Minimum = value;
			}
		}

		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
				this.groupBox.Text = this.title + " (" + this.Value + ")";
			}
		}

		public decimal Value
		{
			get
			{
				return this.numericUpDown.Value;
			}
			set
			{
				this.numericUpDown.Value = value;
				this.groupBox.Text = this.title + " (" + value.ToString() + ")";
			}
		}

		public NumericUpDown NumericUpDown
		{
			get
			{
				return this.numericUpDown;
			}
		}

		private void numericUpDown_ValueChanged(object sender, EventArgs e)
		{
			this.Value = this.numericUpDown.Value;
		}
	}
}
