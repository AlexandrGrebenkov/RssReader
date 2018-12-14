using RssReader.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RssReader.Services.Abstract
{
    public interface IRssData
    {
        IEnumerable<Rss> GetRssList();

        void CreateRss(Rss rss);
        void UpdateRss(Rss rss);
        void DeleteRss(Rss rss);
    }
}
