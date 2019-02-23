using RssReader.Models;
using System;
using System.Collections.Generic;

namespace RssReader.Services.Abstract
{
    public interface IXmlFeedParser
    {
        IEnumerable<RssMessage> ParseXml(string feed, Action<string> errorHandler = null);
    }
}
