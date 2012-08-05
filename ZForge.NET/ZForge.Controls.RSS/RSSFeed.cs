using System;
using System.Collections.Generic;
using System.Text;

namespace ZForge.Controls.RSS
{
  /// <summary>
  /// A data type to represent all properties of single RSS feed.
  /// (one XML document). The descriptions for
  /// the properties of RssItem are para-phrased from the 
  /// <see href="http://blogs.law.harvard.edu/tech/rss">RSS 2 specification</see>.
  /// See <see cref="RssReader">RssReader</see> for properties which 
  /// have not yet been implemented in this version of the
  /// the RssReader class.
  /// </summary>
  /// <remarks>
  /// The following elements of the RSS &lt;channel&gt; node aren't
  /// supported by this version of RssReader:
  /// <list type="bullet">
  /// <item>image (has subelements: image,url,title,link)</item>
  /// <item>cloud (has attributes: domain,port,path,registerProcedure,protocol)</item>
  /// <item>textInput (has subelements: title,description,name,link)</item>
  /// </list>
  /// </remarks>
  [Serializable()]
  public struct RSSFeed
  {
    /// <summary>
    /// The URL of the channel.
    /// </summary>
    public string URL;
    /// <summary>
    /// The name of the channel.
    /// </summary>
    public string Title;
    /// <summary>
    /// Phrase or sentence describing the channel.
    /// </summary>
    public string Description;
    /// <summary>
    /// The URL to the HTML website corresponding to the channel.
    /// </summary>
    public string Link;

    // Optional items

    /// <summary>
    /// The language the channel is written in. This allows 
    /// aggregators to group all Italian language sites, for example, on a single page. 
    /// </summary>
    public string Language;
    /// <summary>
    /// Copyright notice for content in the channel.
    /// </summary>
    public string Copyright;
    /// <summary>
    /// Email address for person responsible for technical issues relating to channel.
    /// </summary>
    public string Webmaster;
    /// <summary>
    /// The publication date for the content in the channel. 
    /// </summary>
    public string PubDate;
    /// <summary>
    /// The last time the content of the channel changed.
    /// </summary>
    public string LastBuildDate;
    /// <summary>
    /// Specify one or more categories that the channel belongs to.
    /// </summary>
    public string Category;
    /// <summary>
    /// A string indicating the program used to generate the channel.
    /// </summary>
    public string Generator;
    /// <summary>
    /// ttl stands for time to live. It's a number of minutes 
    /// that indicates how long a channel can be cached before 
    /// refreshing from the source
    /// </summary>
    public string Ttl;
    /// <summary>
    /// The <see href="http://www.w3.org/PICS/">PICS</see> rating for the channel.
    /// </summary>
    public string Rating;
    /// <summary>
    /// A hint for aggregators telling them which hours they can skip. 
    /// </summary>
    public string Skiphours;
    /// <summary>
    /// A hint for aggregators telling them which days they can skip. 
    /// </summary>
    public string Skipdays;
    /// <summary>
    /// Email address for person responsible for editorial content.
    /// </summary>
    public string ManagingEditor;
    /// <summary>
    /// A collection of RssItem datatypes, representing each
    /// item for the RSS feed.
    /// </summary>
    public RSSItemCollection Items;
    /// <summary>
    /// Contains any errors that occured during the loading or
    /// parsing of the XML document. Compare this to a blank string
    /// to see if any errors occured.
    /// </summary>
    public string ErrorMessage;
  }
}
