using System;
using System.Threading.Tasks;

namespace RssReader.Services.Abstract
{
    public interface INetworkWorker
    {
        Task<string> GetFeedStringAsync(string link, Action<string> errorHandler = null);
    }
}
