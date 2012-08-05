using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Text.RegularExpressions;
using ZForge.Globalization;

namespace ZForge.Configuration
{
	public static class Validator
	{
		public static bool ValidateInt(List<string> msgs, string name, string value, int min, int max)
		{
			try
			{
				int v = Convert.ToInt32(value);
				if (v < min || v > max)
				{
					throw new ArgumentException();
				}
				return true;
			}
			catch (Exception)
			{
				string s = Translator.Instance.T("[{0}]设置错误! [{0}]必须是一个{1}~{2}之间的整数.");
				msgs.Add(string.Format(s, name, min, max));
				return false;
			}
		}

		public static bool ValidateString(List<string> msgs, string name, string value)
		{
			if (value == null || value.Length == 0)
			{
				string s = Translator.Instance.T("[{0}]设置错误! [{0}]必须填写.");
				msgs.Add(string.Format(s, name));
				return false;
			}
			return true;
		}

		public static bool ValidateEmailAddress(List<string> msgs, string name, string value)
		{
			try
			{
				MailAddress m = new MailAddress(value, "test", System.Text.Encoding.UTF8);
				return true;
			}
			catch (Exception)
			{
				string s = Translator.Instance.T("[{0}]设置错误, [{0}]必须为一个合法的EMail地址.");
				msgs.Add(string.Format(s, name));
				return false;
			}
		}

		public static bool ValidateURL(List<string> msgs, string name, string value)
		{
			try
			{
				Uri u = new Uri(value);
				if (u.Scheme == null || (!u.Scheme.ToLower().Equals("http") && !u.Scheme.ToLower().Equals("https")))
				{
					throw new System.ArgumentException();
				}
				if (u.Host == null || u.Host.Length == 0)
				{
					throw new System.ArgumentException();
				}
				return true;
			}
			catch (Exception)
			{
				string s = Translator.Instance.T("[{0}]设置错误, [{0}]必须为一个合法的URL.");
				msgs.Add(string.Format(s, name));
				return false;
			}
		}

		public static string MergeMessages(List<string> msgs, string title)
		{
			string r = title + "\n";
			foreach (string s in msgs)
			{
				r += s + "\n";
			}
			return r.Trim();
		}

		public static string MergeMessages(List<string> msgs)
		{
			string r = "";
			foreach (string s in msgs)
			{
				r += s + "\n";
			}
			return r.Trim();
		}
	}
}
