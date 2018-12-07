using RssReader.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RssReader.Services
{
    public interface IFileWorker
    {
        Task<IEnumerable<Rss>> LoadRssListAsync(Action<string> errorhandler = null);
        Task<bool> SaveRssListAsync(IEnumerable<Rss> list, Action<string> errorhandler = null);
    }
}
