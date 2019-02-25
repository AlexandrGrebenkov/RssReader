using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RssReader.Models;
using RssReader.Services;

namespace RssReader.UnitTests.Services
{
    public class XmlParserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ParseXml()
        {
            var feedString =
            @"<rss version=""2.0"">
                <channel>
                    <title>Channel title</title>
                    <link>http://www.foo.com</link>
                    <description>Channel description</description>
                    <ttl>60</ttl>

                    <item>
                        <title>Title 1</title>
                        <link>http://foo.com/1</link>
                        <description>description 1</description>
                        <category>Cat 1</category>
                        <pubDate>Sun, 17 Feb 2019 00:00:00 +0500</pubDate>
                        <guid>1428142975</guid>
                    </item>

                    <item>
                        <title>Title 2</title>
                        <link>http://foo.com/2</link>
                        <description>description 2</description>
                        <category>Cat 2</category>
                        <guid>2428142975</guid>
                    </item>
                </channel>
            </rss>";

            XmlFeedParser parser = new XmlFeedParser();

            var feed = parser.ParseXml(feedString);

            Assert.NotNull(feed);
            Assert.AreEqual(2, new List<RssMessage>(feed).Count);
            CollectionAssert.AllItemsAreNotNull(feed);
        }
    }
}
