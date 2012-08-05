using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace ZForge.Controls.RSS
{
  public partial class ToolStripRSSLabel : ToolStripLabel
  {
    private List<string> mUrlList = new List<string>();
    private List<RSSItem> mItemCollection = new List<RSSItem>();
    private int mFeedLoadInterval = 3600;
    private int mUpdateInterval = 5;
    private bool mIsRunning = false;
    private int mCurrentItemIndex = 0;

    private RSSItem mDefaultItem = new RSSItem();

    public ToolStripRSSLabel()
    {
      this.Construct("江苏先安科技有限公司", "http://www.syan.com.cn");
    }

    public ToolStripRSSLabel(string txt, string link)
    {
      this.Construct(txt, link);
    }

    private void Construct(string txt, string link)
    {
      InitializeComponent();
      this.IsLink = true;
      mDefaultItem.Title = txt;
      mDefaultItem.Link = link;
      DirectUpdateUI(mDefaultItem);
    }

    public List<string> URLs 
    {
      get { return mUrlList; }
    }

    public RSSItem Default
    {
      get { return mDefaultItem; }
    }

    public int FeedLoadInterval
    {
      get { return mFeedLoadInterval; }
      set
      {
        if (value < 5) value = 5;
        mFeedLoadInterval = value;
      }
    }

    public int UpdateInterval
    {
      get { return mUpdateInterval; }
      set
      {
        if (value < 5) value = 5;
        if (value > 60) value = 60;
        mUpdateInterval = value;
      }
    }

    public void Run()
    {
      if (mIsRunning == false)
      {
        mIsRunning = true;
        Thread t = new Thread(new ThreadStart(Loop));
        t.Start();
      }
    }

    private bool UpdateItemCollection()
    {
      bool ret = false;
      foreach (string u in mUrlList)
      {
        RSSFeed f = RSSReader.GetFeed(u);
        if (string.IsNullOrEmpty(f.ErrorMessage))
        {
          mItemCollection.RemoveAll(delegate(RSSItem o) { return (o.Feed.URL == u); });
          foreach (RSSItem i in f.Items)
          {
            mItemCollection.Add(i);
            ret = true;
          }
        }
      }
      return ret;
    }

    delegate void UpdateUIDelegate(RSSItem item);

    private void DirectUpdateUI(RSSItem item)
    {
      string txt = item.Title;
      if (string.IsNullOrEmpty(item.Pubdate) == false)
      {
        DateTime dt = DateTime.Parse(item.Pubdate);
        if (dt != null)
        {
          txt += " (" + dt.ToShortDateString() + ")";
        }
      }
      this.Text = txt;
      this.Tag = item.Link;
      this.LinkVisited = false;
    }

    private void UpdateUI(RSSItem item)
    {
      ToolStrip parent = this.Parent as ToolStrip;
      if (parent != null && parent.InvokeRequired)
      {
        parent.Invoke(new UpdateUIDelegate(DirectUpdateUI), new object[] { item });
      }
      else
      {
        this.DirectUpdateUI(item);
      }
    }

    private void Loop()
    {
      DateTime next = DateTime.Now;
      while (true)
      {
        if (next.CompareTo(DateTime.Now) <= 0)
        {
          bool b = this.UpdateItemCollection();
          next = DateTime.Now.Add(new TimeSpan(0, 0, (b) ? this.FeedLoadInterval : Math.Min(this.FeedLoadInterval, 600)));
        }
        mCurrentItemIndex++;
        if (mCurrentItemIndex >= mItemCollection.Count)
        {
          mCurrentItemIndex = 0;
        }
        if (mItemCollection.Count > 0)
        {
          RSSItem ri = mItemCollection[mCurrentItemIndex];
          this.UpdateUI(ri);
        }
        else
        {
          this.UpdateUI(this.Default);
        }
        Thread.Sleep(this.UpdateInterval * 1000);
      }
    }

    private void ToolStripRSSLabel_Click(object sender, EventArgs e)
    {
      try
      {
        Process.Start(new ProcessStartInfo(this.Tag.ToString()));
      }
      catch (Exception)
      {
      }
    }
  }
}