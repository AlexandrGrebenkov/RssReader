using RssReader.Models;
using System;
using System.Collections.Generic;

namespace RssReader.Services.Abstract
{
    public interface IRssData
    {
        IEnumerable<Rss> GetRssList(Action<string> errorHandler = null);

        void CreateRss(Rss rss, Action<string> errorHandler = null);
        void UpdateRss(Rss rss, Action<string> errorHandler = null);
        void DeleteRss(Rss rss, Action<string> errorHandler = null);
    }
}
