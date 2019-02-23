using RssReader.Models;
using RssReader.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace RssReader.Services
{
    public class XmlFeedParser : IXmlFeedParser
    {
        //Sun, 17 Feb 2019 00:00:00 +0500
        public IEnumerable<RssMessage> ParseXml(string feed, Action<string> errorHandler = null)
        {
            var rssFeed = new List<RssMessage>();

            try
            {// Разбор полученной XML ленты
                if (string.IsNullOrEmpty(feed)) return null;

                var parsedFeed = XElement.Parse(feed);
                rssFeed = new List<RssMessage>();

                foreach (var item in parsedFeed?.Element("channel")?.Elements("item"))
                {
                    var title = item.Element("title");
                    var link = item.Element("link");
                    var text = item.Element("description");
                    var date = item.Element("pubDate");

                    if (date != null && !DateTime.TryParse(date.Value, out var dt))
                        dt = DateTime.MinValue;
                    else
                        dt = DateTime.MinValue;

                    rssFeed.Add(new RssMessage(title?.Value, text?.Value, dt, link?.Value));
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                errorHandler?.Invoke(ex.Message);
#else
                errorHandler?.Invoke(Strings.ParsingXmlProblems);
#endif
                return null;
            }
            return rssFeed;
        }
    }
}
