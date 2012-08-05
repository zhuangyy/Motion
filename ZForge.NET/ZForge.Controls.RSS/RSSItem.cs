using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.RSS
{
  /// <summary>
  /// A data type to represent a single
  /// RSS item in a RSS feed. See <see cref="RssReader">RssReader</see> for
  /// properties of a RSS item which have not yet been implemented 
  /// in this version of the the RssReader class. The descriptions for
  /// the properties of RssItem are para-phrased from the 
  /// <see href="http://blogs.law.harvard.edu/tech/rss">RSS 2 specification.</see>
  /// </summary>
  /// <remarks>
  /// The following elements of a RSS item aren't
  /// supported by this version of RssReader:
  /// <list type="bullet">
  /// <item>category (can have domain attribute)</item>
  /// <item>enclosure ( has attributes: url,length,type )</item>
  /// <item>source (has attributes: url)</item>
  /// </list>
  /// </remarks>
  [Serializable()]
  public struct RSSItem
  {
    /// <summary>
    /// The the feed of this item.
    /// </summary>
    public RSSFeed Feed;
    /// <summary>
    /// The title of the item.
    /// </summary>
    public string Title;
    /// <summary>
    /// The item synopsis.
    /// </summary>
    public string Description;
    /// <summary>
    /// The URL of the item.
    /// </summary>
    public string Link;
    /// <summary>
    /// Email address of the author of the item. 
    /// </summary>
    public string Author;
    /// <summary>
    /// URL of a page for comments relating to the item
    /// </summary>
    public string Comments;
    /// <summary>
    /// Indicates when the item was published. 
    /// </summary>
    public string Pubdate;
    /// <summary>
    /// A string that uniquely identifies the item.
    /// </summary>
    public string Guid;
  }
}
