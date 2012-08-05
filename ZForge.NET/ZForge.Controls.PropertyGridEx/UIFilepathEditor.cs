using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;

namespace ZForge.Controls.PropertyGridEx
{
	public class UIFilepathEditor : System.Drawing.Design.UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (context != null && context.Instance != null)
			{
				if (!context.PropertyDescriptor.IsReadOnly)
				{
					return UITypeEditorEditStyle.Modal;
				}
			}
			return UITypeEditorEditStyle.None;
		}

		[RefreshProperties(RefreshProperties.All)]
		public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			if (context == null || provider == null || context.Instance == null)
			{
				return base.EditValue(provider, value);
			}

			FolderBrowserDialog pathDlg = new FolderBrowserDialog();
			pathDlg.Description =  "Select " + context.PropertyDescriptor.DisplayName;
			pathDlg.ShowNewFolderButton = true;
			pathDlg.SelectedPath = (string)value;

			if (pathDlg.ShowDialog() == DialogResult.OK)
			{
				value = pathDlg.SelectedPath;
			}
			pathDlg.Dispose();
			return value;
		}
	}
}
