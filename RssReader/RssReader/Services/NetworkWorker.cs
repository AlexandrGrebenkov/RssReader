using RssReader.Services.Abstract;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RssReader.Services
{
    public class NetworkWorker : INetworkWorker
    {
        /// <summary></summary>
        HttpClient client;

        public async Task<string> GetFeedStringAsync(string link, Action<string> errorHandler = null)
        {
            string feed = null;
            try
            {// Запрос XML ленты
                if (client == null)
                    client = new HttpClient();
                feed = await client.GetStringAsync(link);
            }
            catch (Exception ex)
            {
#if DEBUG
                errorHandler?.Invoke(ex.Message);
#else
                errorHandler?.Invoke(Strings.NetworkProblems);
#endif
            }
            return feed;
        }
    }
}
