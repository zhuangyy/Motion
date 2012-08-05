using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Security.Principal;
using ZForge.Globalization;
using System.Windows.Forms;

namespace ZForge.Win32
{
	public static class UserEnviroment
	{
		public static bool IsVistaAbove()
		{
			return ((Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major >= 6));
		}

		public static bool IsWin2kAbove()
		{
			return ((Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major >= 5));
		}

		public static bool IsUACEnabled()
		{
			if (false == UserEnviroment.IsVistaAbove())
			{
				return false;
			}
			string sk = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
			RegistryKey rk = Registry.LocalMachine.OpenSubKey(sk);
			if (rk == null)
			{
				return false;
			}
			object o = rk.GetValue("EnableLUA");
			rk.Close();

			if (o == null)
			{
				return true;
			}
			return Convert.ToBoolean(o);
		}

		public static bool IsAdministrator()
		{
			WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
			return wp.IsInRole(WindowsBuiltInRole.Administrator);
		}

		public static void ValidateUserEnviroment(string name, bool needAdmin)
		{
			if (false == UserEnviroment.IsWin2kAbove())
			{
				string m = string.Format(Translator.Instance.T("{0} 仅能运行在Windows 2000之后的Windows操作系统上!"), name);
				throw new Exception(m);
			}
			if (needAdmin && false == UserEnviroment.IsAdministrator())
			{
				string m = string.Format(Translator.Instance.T("{0} 需要运行在管理员权限下! 您可以选择:"), name);
				m += "\n";
				m += Translator.Instance.T("1) 使用管理员账号重新登录系统.");
				m += "\n";
				m += Translator.Instance.T("2) 使用下面的方法提升应用程序的运行权限:");
				if (UserEnviroment.IsVistaAbove())
				{
					m += "\n";
					m += string.Format(Translator.Instance.T("在Windows 资源管理器中选择[{0}], 点击鼠标右键, 在弹出的菜单上选择[以管理员身份运行], 以提升本应用的运行权限."), Application.ExecutablePath);
				}
				else
				{
					m += "\n";
					m += string.Format(Translator.Instance.T("在Windows 资源管理器中选择[{0}], 点击鼠标右键, 在弹出的菜单上选择[运行方式...], 以提升本应用的运行权限."), Application.ExecutablePath);
				}
				throw new Exception(m);
			}
		}

	}
}
