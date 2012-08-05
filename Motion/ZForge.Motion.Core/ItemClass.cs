using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using ZForge.Motion.Util;
using ZForge.Globalization;

namespace ZForge.Motion.Core
{
	public abstract class ItemClass
	{
		private GroupClass mParent;
		protected string mID;
		protected String mName;

		protected abstract string IDPrefix { get; }

		public GroupClass Group
		{
			get
			{
				return this.mParent;
			}
			set
			{
				this.mParent = value;
			}
		}

		public string FullName
		{
			get
			{
				GroupClass g = this.Group;
				ArrayList r = new ArrayList();
				while (g != null && false == (g is RootClass))
				{
					r.Add(g.Name);
					g = g.Group;
				}
				string fn = "";
				foreach (string s in r)
				{
					if (fn.Length == 0)
					{
						fn = string.Format("[{0}]", s);
					}
					else
					{
						fn = string.Format("[{0}].{1}", s, fn);
					}
				}
				if (fn.Length == 0)
				{
					return "[" + this.Name + "]";
				}
				return fn + ".[" + this.Name + "]";
			}
		}

		private string GenRandomString()
		{
			ArrayList pool = new ArrayList();
			Random rdm = new Random();
			string s = "";

			for (int n = 33; n < 125; n++)
			{
				pool.Add(Convert.ToChar(n).ToString());
			}
			for (int i = 0; i < 32; i++)
			{
				s += pool[rdm.Next(pool.Count)].ToString();
			}
			return s + this.Name;
		}

		public string GenID(string p)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] bs = System.Text.Encoding.UTF8.GetBytes(p);
			bs = x.ComputeHash(bs);
			System.Text.StringBuilder s = new System.Text.StringBuilder();
			foreach (byte b in bs)
			{
				s.Append(b.ToString("x2").ToLower());
			}
			return s.ToString();
		}

		public string GenID()
		{
			string p = this.GenRandomString();
			return this.GenID(p);
		}

		public abstract void Clean();

		public void Remove()
		{
			if (this.Group != null)
			{
				this.Group.Children.Remove(this);
			}
		}

		public void MoveTo(GroupClass dst)
		{
			if (dst == null)
			{
				dst = RootClass.Instance as GroupClass;
			}
			if (this.Group == dst)
			{
				return;
			}
			this.Remove();
			dst.Children.Add(this, dst);
		}

		#region Properties

		public String Name
		{
			get
			{
				if (mName == null)
				{
					mName = Translator.Instance.T("未命名");
				}
				return mName;
			}
			set
			{
				mName = value.Trim();
			}
		}

		public string ID
		{
			get
			{
				if (mID == null)
				{
					mID = this.IDPrefix + this.GenID();
				}
				return mID;
			}
			set
			{
				mID = value.Trim();
			}
		}

		#endregion
	}
}
