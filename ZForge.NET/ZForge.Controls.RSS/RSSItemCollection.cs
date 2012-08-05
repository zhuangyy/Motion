using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ZForge.Controls.RSS
{
  /// <summary>
  /// Represents a collection of RSS items for
  /// the RSS feed.
  /// </summary>
  [Serializable()]
  public class RSSItemCollection : CollectionBase
  {
    public RSSItem this[int item]
    {
      get
      {
        return this.getItem(item);
      }
    }

    public void Add(RSSItem rssItem)
    {
      List.Add(rssItem);
    }

    public bool Remove(int index)
    {
      if (index > Count - 1 || index < 0)
      {
        return false;
      }
      else
      {
        List.RemoveAt(index);
        return true;
      }
    }

    private RSSItem getItem(int Index)
    {
      return (RSSItem)List[Index];
    }

  }
}
