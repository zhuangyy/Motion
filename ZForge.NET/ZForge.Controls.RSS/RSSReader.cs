using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Net;

/// Version 1.0
namespace ZForge.Controls.RSS
{
	#region Event datatype/delegate
	/// <summary>
	/// Holds details about any errors that occured
	/// during the loading or parsing of the RSS feed.
	/// </summary>
	public class RSSReaderErrorEventArgs : EventArgs
	{
		/// <summary>
		/// The details of the error.
		/// </summary>
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		private string message;
	}

	/// <summary>
	/// Represents the method that will handle the RssReader error event.
	/// </summary>
	public delegate void RssReaderErrorEventHandler(object sender, RSSReaderErrorEventArgs e);
	#endregion

	#region RSSReader class
	/// <summary>
	/// The RssReader class provides a number of static methods for easy
	/// 1 or 2 step retrieval of RSS feeds. RSS feeds can be downloaded from any
	/// URL, and are then parsed into an <see cref="RssFeed">RssFeed</see> data type,
	/// which contains properties representing most aspects of an RSS Feed. A number
	/// of events are available for the calling application to register at the various
	/// stages of the feed request and parsing.
	/// <example>
	/// The following example retrieves the RSS news feed for the BBC news website,
	/// and creates a HTML document from the feed's details. It saves the HTML document
	/// to disk, and launches the default browser with the document. The number of items
	/// displayed is limited to 5. If there is any error, a messagebox is displayed with
	/// the details of the error.
	/// <code>
	/// RssFeed feed = RssReader.GetFeed("http://www.bbc.co.uk/syndication/feeds/news/ukfs_news/front_page/rss091.xml");
	/// 
	/// if ( feed.ErrorMessage == null || feed.ErrorMessage == "" )
	/// {
	///		string template = "&lt;a href=\"%Link%&gt;%Title%&lt;/a&gt;&lt;br/&gt;%Description%&lt;br/&gt;&lt;br/&gt;&lt;ul&gt;%Items%&lt;/ul&gt;";
	///		string itemTemplate = "&lt;li&gt;&lt;a href=\"%Link%&gt;%Title%&lt;/a&gt;&lt;br/&gt;%Description%&lt;/li&gt;";
	/// 	string html = RssReader.CreateHtml(feed,template,itemTemplate,"",5);
	/// 
	/// 	StreamWriter streamWriter = File.CreateText("c:\\rss.html");
	/// 	streamWriter.Write(html);
	/// 	streamWriter.Close();
	/// 
	/// 	System.Diagnostics.Process.Start("c:\\rss.html");
	/// }
	/// else
	/// {
	/// 	MessageBox.Show("Error getting feed:\r\n" +feed.ErrorMessage,"Rss Demo App",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
	/// }
	/// </code>
	/// </example>
	/// </summary>
	public class RSSReader
	{
		// Events: XML document loaded, rss element found,
		// channel node found, item parsed, error

		/// <summary>
		/// This event is fired when the feed has finished loading from the URL
		/// provided, into the XML parser.
		/// </summary>
		public event EventHandler FeedLoaded;

		/// <summary>
		/// This event is fired when the root node (typically 'rss') has
		/// been found in the feed.
		/// </summary>
		public event EventHandler RssNodeFound;

		/// <summary>
		/// This event is fired when the channel/child node of the rss node
		/// (typically 'channel') has been found in the feed.
		/// </summary>
		public event EventHandler ChannelNodeFound;

		/// <summary>
		/// This event is fired when an item is added to the <see cref="RssFeed">RssFeed</see>'s
		/// collection of items.
		/// </summary>
		public event EventHandler ItemAdded;

		/// <summary>
		/// This event is fired when an error occurs in the loading or parsing
		/// of the feed. The same error message is also available in the ErrorMessage
		/// property of the <see cref="RssFeed">RssFeed</see> object that is returned
		/// by the <see cref="Retrieve">Retrieve</see> method.
		/// </summary>
		public event RssReaderErrorEventHandler Error;


		/// <summary>
		/// The node name for the channel element
		/// in the RSS feed. This will rarely ever to be
		/// changed. Default is 'channel'.
		/// </summary>
		public string RootNodeName
		{
			get 
			{
				return this.rootNodeName;
			}
			set
			{
				this.rootNodeName = value;
			}
		}

		/// <summary>
		/// The node name for the root rss element
		/// in the RSS feed. This is altered automatically to 'rdf:RDF'
		/// when RdfMode is set to true. Default is 'rss'.
		/// </summary>
		public string ChannelNodeName
		{
			get 
			{
				return this.channelNodeName;
			}
			set
			{
				this.channelNodeName = value;
			}
		}


		/// <summary>
		/// If this is set to true, then the XML document
		/// is parsed slightly different, to cater sites with RDF feeds (such as
		/// slashdot.org and register.com). The whole RDF format is not supported,
		/// but those items in RSS which have a corresponding RDF property, such
		/// as description,title for the channel, and title,description for each
		/// item, are matched.
		/// </summary>
		public bool RdfMode
		{
			get 
			{
				return this.rdfMode;
			}
			set
			{
				if ( value )
				{
					this.rootNodeName = "rdf:RDF";
				}
				else
				{
					this.rootNodeName = "rss";
				}
				this.rdfMode = value;
			}
		}

		/// <summary>
		/// Member for the public property.
		/// </summary>
		private string rootNodeName = "rss";

		/// <summary>
		/// Member for the public property.
		/// </summary>
		private string channelNodeName = "channel";

		/// <summary>
		/// Member for the public property.
		/// </summary>
		private bool rdfMode = false;

		/// <summary>
		/// Retrieves a <see cref="RssFeed">RssFeed</see> object using
		/// the url provided as the source of the Feed.
		/// </summary>
		/// <param name="Url">The url to retrieve the RSS feed from, this can
		/// be in the format of http:// and also file://.. (ftp?)</param>
		/// <param name="RdfFormat">If this is set to true, then the XML document
		/// is parsed slightly different, to cater sites with RDF feeds (such as
		/// slashdot.org and register.com). The whole RDF format is not supported,
		/// but those items in RSS which have a corresponding RDF property, such
		/// as description,title for the channel, and title,description for each
		/// item, are matched.</param>
		/// <returns>A <see cref="RssFeed">RssFeed</see> object from the
		/// RSS feed's details.</returns>
		public static RSSFeed GetFeed(string Url,bool RdfFormat)
		{
			RSSReader rssReader = new RSSReader();
			rssReader.RdfMode = RdfFormat;
			return rssReader.Retrieve(Url);
		}

		/// <summary>
		/// Retrieves a <see cref="RssFeed">RssFeed</see> object using
		/// the url provided as the source of the Feed.
		/// </summary>
		/// <param name="Url">The url to retrieve the RSS feed from, this can
		/// be in the format of http:// and also file://.. (ftp?)</param>
		/// <returns>A <see cref="RssFeed">RssFeed</see> object from the
		/// RSS feed's details.</returns>
		public static RSSFeed GetFeed(string Url)
		{
			RSSReader rssReader = new RSSReader();
			return rssReader.Retrieve(Url);
		}

		/// <summary>
		/// Retrieves an RSS feed using the given Url, parses it and
		/// creates and new <see cref="RssFeed">RssFeed</see> object with the information.
		/// If an error occurs in the XML loading of the document, or parsing of
		/// the RSS feed, the error is trapped and stored inside the RssFeed's
		/// ErrorMessage property.
		/// </summary>
		/// <param name="Url">The url to retrieve the RSS feed from, this can
		/// be in the format of http:// and also file://.. (ftp?)</param>
		/// <returns>An <see cref="RssFeed">RssFeed</see> object with information
		/// retrieved from the feed.</returns>
		public RSSFeed Retrieve(string Url)
		{
			
			RSSFeed rssFeed = new RSSFeed();
      rssFeed.URL = Url;
			rssFeed.Items = new RSSItemCollection();

      XmlTextReader xmlTextReader = new XmlTextReader(Url);
			XmlDocument xmlDoc= new XmlDocument();

      try
      {
        xmlDoc.Load(xmlTextReader);

        // Fire the load event
        if (this.FeedLoaded != null)
        {
          this.FeedLoaded(this, new EventArgs());
        }

        XmlNode rssXmlNode = null;

        // Loop child nodes till we find the rss one
        for (int i = 0; i < xmlDoc.ChildNodes.Count; i++)
        {
          System.Diagnostics.Debug.Write("Child: " + xmlDoc.ChildNodes[i].Name);
          System.Diagnostics.Debug.WriteLine(" has " + xmlDoc.ChildNodes[i].ChildNodes.Count + " children");

          if (xmlDoc.ChildNodes[i].Name == this.rootNodeName && xmlDoc.ChildNodes[i].ChildNodes.Count > 0)
          {
            rssXmlNode = xmlDoc.ChildNodes[i];

            // Fire the found event
            if (this.RssNodeFound != null)
            {
              this.RssNodeFound(this, new EventArgs());
            }

            break;
          }
        }

        if (rssXmlNode != null)
        {
          XmlNode channelXmlNode = null;

          // Loop through the rss node till we find the channel
          for (int i = 0; i < rssXmlNode.ChildNodes.Count; i++)
          {
            System.Diagnostics.Debug.WriteLine("Rss child: " + rssXmlNode.ChildNodes[i].Name);
            if (rssXmlNode.ChildNodes[i].Name == this.channelNodeName && rssXmlNode.ChildNodes[i].ChildNodes.Count > 0)
            {
              channelXmlNode = rssXmlNode.ChildNodes[i];

              // Fire the found event
              if (this.ChannelNodeFound != null)
              {
                this.ChannelNodeFound(this, new EventArgs());
              }

              break;
            }
          }

          // Found the channel node
          if (channelXmlNode != null)
          {
            // Loop through its children, copying details to the
            // RssFeed struct, and parsing the items
            for (int i = 0; i < channelXmlNode.ChildNodes.Count; i++)
            {
              System.Diagnostics.Debug.WriteLine(channelXmlNode.ChildNodes[i].Name);
              switch (channelXmlNode.ChildNodes[i].Name.ToLower())
              {
                case "title":
                  {
                    rssFeed.Title = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "description":
                  {
                    rssFeed.Description = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "language":
                  {
                    rssFeed.Language = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "copyright":
                  {
                    rssFeed.Copyright = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "webmaster":
                  {
                    rssFeed.Webmaster = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "pubDate":
                  {
                    rssFeed.PubDate = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "lastBuildDate":
                  {
                    rssFeed.LastBuildDate = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "category":
                  {
                    rssFeed.Category = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "generator":
                  {
                    rssFeed.Generator = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "ttl":
                  {
                    rssFeed.Ttl = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "rating":
                  {
                    rssFeed.Rating = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "skipHours":
                  {
                    rssFeed.Skiphours = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "skipDays":
                  {
                    rssFeed.Skipdays = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "managingEditor":
                  {
                    rssFeed.ManagingEditor = channelXmlNode.ChildNodes[i].InnerText;
                    break;
                  }
                case "item":
                  {
                    RSSItem t = this.getRssItem(channelXmlNode.ChildNodes[i]);
                    t.Feed = rssFeed;
                    rssFeed.Items.Add(t);

                    // Fire the found event
                    if (this.ItemAdded != null)
                    {
                      this.ItemAdded(this, new EventArgs());
                    }

                    break;
                  }
              }

            }

            // If rdf mode is set, then the channel node only contains
            // information about the channel, it doesn't hold the item
            // nodes. The item nodes are children of the root node in
            // an RDF document, so we use this instead.
            if (this.RdfMode)
            {
              for (int i = 0; i < rssXmlNode.ChildNodes.Count; i++)
              {
                switch (rssXmlNode.ChildNodes[i].Name)
                {
                  case "item":
                    {
                      RSSItem t = this.getRssItem(rssXmlNode.ChildNodes[i]);
                      t.Feed = rssFeed;
                      rssFeed.Items.Add(t);

                      // Fire the found event
                      if (this.ItemAdded != null)
                      {
                        this.ItemAdded(this, new EventArgs());
                      }

                      break;
                    }
                }
              }
            }
          }
          else
          {
            rssFeed.ErrorMessage = "Unable to find rss <seehannel> node";

            // Fire the error event
            if (this.Error != null)
            {
              RSSReaderErrorEventArgs args = new RSSReaderErrorEventArgs();
              args.Message = rssFeed.ErrorMessage;
              this.Error(this, args);
            }
          }

        }
        else
        {
          rssFeed.ErrorMessage = "Unable to find root <rss> node";

          // Fire the error event
          if (this.Error != null)
          {
            RSSReaderErrorEventArgs args = new RSSReaderErrorEventArgs();
            args.Message = rssFeed.ErrorMessage;
            this.Error(this, args);
          }
        }

      }
      catch (XmlException err)
      {
        //
        rssFeed.ErrorMessage = "Xml error: " + err.Message;

        // Fire the error event
        if (this.Error != null)
        {
          RSSReaderErrorEventArgs args = new RSSReaderErrorEventArgs();
          args.Message = rssFeed.ErrorMessage;
          this.Error(this, args);
        }
        return rssFeed;
      }
      catch (WebException we)
      {
        rssFeed.ErrorMessage = "Web error: " + we.Message;
      }

			return rssFeed;
		}

		/// <summary>
		/// Creates an RSS item from an XML node with the 
		/// corresponding child nodes (title,description etc.)
		/// </summary>
		/// <param name="xmlNode">The node to extract the details from</param>
		/// <returns>An RssItem object with details taken from the item node.</returns>
		private RSSItem getRssItem(XmlNode xmlNode)
		{
			RSSItem rssItem = new RSSItem();

			for (int i=0;i < xmlNode.ChildNodes.Count;i++)
			{					
				switch ( xmlNode.ChildNodes[i].Name.ToLower() )
				{
					case "title":
					{
						rssItem.Title = xmlNode.ChildNodes[i].InnerText;
						break;
					}
					case "description":
					{
						rssItem.Description = xmlNode.ChildNodes[i].InnerText;
						break;
					}
					case "link":
					{
						rssItem.Link = xmlNode.ChildNodes[i].InnerText;
						break;
					}
					case "author":
					{
						rssItem.Author = xmlNode.ChildNodes[i].InnerText;
						break;
					}
					case "comments":
					{
						rssItem.Comments = xmlNode.ChildNodes[i].InnerText;
						break;
					}
					case "pubdate":
					{
						rssItem.Pubdate = xmlNode.ChildNodes[i].InnerText;
						break;
					}
					case "guid":
					{
						rssItem.Guid = xmlNode.ChildNodes[i].InnerText;
						break;
					}
				}
			}

			return rssItem;
		}
	}
	#endregion

}
