using Helpers;
using RssReader.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Xamarin.Forms;

namespace RssReader.ViewModels
{
    class RssVM : BaseViewModel
    {
        HttpClient client;

        Rss _Rss;
        /// <summary>Rss-канал</summary>
        public Rss Rss
        {
            get { return _Rss; }
            set { SetProperty(ref _Rss, value); }
        }

        public RssVM(Rss rss)
        {
            Rss = rss;
            Title = Rss.Name;

            Task.Run(async () =>
            {
                Rss.Messages = await GetRssFeed(rss.Link);
            });

            cmdSelect = new Command<RssMessage>(message =>
            {
                if (!string.IsNullOrWhiteSpace(message.Link))
                    Device.OpenUri(new Uri(message.Link));
            });

            cmdRefresh = new RelayCommand(async () =>
            {
                IsBusy = true;
                Rss.Messages = await GetRssFeed(rss.Link);
                IsBusy = false;
            });
        }

        public ICommand cmdSelect { get; }
        public RelayCommand cmdRefresh { get; }

        private async Task<IEnumerable<RssMessage>> GetRssFeed(string rssLink)
        {
            if (client == null)
                client = new HttpClient();
            var feed = await client.GetStringAsync(rssLink);

            if (string.IsNullOrEmpty(feed)) return null;

            var parsedFeed = XElement.Parse(feed);

            var messages = new List<RssMessage>();

            foreach (var item in parsedFeed.Element("channel").Elements("item"))
            {
                var title = item.Element("title");
                var link = item.Element("link");

                messages.Add(new RssMessage(title.Value, "", DateTime.Now, link.Value));
            }

            return messages;
        }
    }
}
