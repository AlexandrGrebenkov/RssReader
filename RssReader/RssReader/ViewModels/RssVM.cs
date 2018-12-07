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

            Device.BeginInvokeOnMainThread(async () =>
            {
                var messages = await GetRssFeed(rss.Link,
                    async error => await DisplayAlert("Ошибка", error, "", "Ok"));
                if (messages != null)
                    Rss.Messages = messages;
            });

            cmdSelect = new Command<RssMessage>(message =>
            {
                if (!string.IsNullOrWhiteSpace(message.Link))
                    Device.OpenUri(new Uri(message.Link));
            });

            cmdRefresh = new RelayCommand(async () =>
            {
                IsBusy = true;
                var messages = await GetRssFeed(rss.Link,
                    async error => await DisplayAlert("Ошибка", error, "", "Ok"));
                if (messages != null)
                    Rss.Messages = messages;
                IsBusy = false;
            });
        }

        public ICommand cmdSelect { get; }
        public RelayCommand cmdRefresh { get; }

        private async Task<IEnumerable<RssMessage>> GetRssFeed(string rssLink, Action<string> errorhandler = null)
        {
            string feed = string.Empty;
            List<RssMessage> messages = null;
            try
            {
                if (client == null)
                    client = new HttpClient();
                feed = await client.GetStringAsync(rssLink);
            }
            catch (Exception ex)
            {
#if DEBUG
                errorhandler?.Invoke(ex.Message);
#else
                errorhandler?.Invoke("Что-то не так со связью :(");
#endif
                return null;
            }

            try
            {
                if (string.IsNullOrEmpty(feed)) return null;

                var parsedFeed = XElement.Parse(feed);
                messages = new List<RssMessage>();

                foreach (var item in parsedFeed.Element("channel").Elements("item"))
                {
                    var title = item.Element("title");
                    var link = item.Element("link");

                    messages.Add(new RssMessage(title.Value, "", DateTime.Now, link.Value));
                }

                MessagingCenter.Send(this, "RssFeedUpdated", new object());
            }
            catch (Exception ex)
            {
#if DEBUG
                errorhandler?.Invoke(ex.Message);
#else
                errorhandler?.Invoke("Не могу разобрать что сервер прислал :(");
#endif
                return null;
            }

            return messages;
        }
    }
}
