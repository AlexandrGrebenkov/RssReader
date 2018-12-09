using Helpers;
using RssReader.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Xamarin.Forms;

namespace RssReader.ViewModels
{
    /// <summary>Вью-модель для просмотра Rss-ленты</summary>
    class RssVM : BaseViewModel
    {
        /// <summary></summary>
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

            // Обновляем ленту при открытии
            Device.BeginInvokeOnMainThread(async () =>
            {
                var messages = await GetRssFeed(rss.Link);
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

        /// <summary>Выбор сообщения из ленты (открывает браузер)</summary>
        public ICommand cmdSelect { get; }
        /// <summary>Команда обновления ленты</summary>
        public RelayCommand cmdRefresh { get; }

        /// <summary>Метод получения ленты из сети</summary>
        /// <param name="rssLink">Ссылка на Rss-канал</param>
        /// <param name="errorhandler">Обработчик ошибок</param>
        async Task<IEnumerable<RssMessage>> GetRssFeed(string rssLink, Action<string> errorhandler = null)
        {// TODO: Разбить на 2 метода
            string feed = string.Empty;
            List<RssMessage> messages = null;
            try
            {// Запрос XML ленты
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
            {// Разбор полученной XML ленты
                if (string.IsNullOrEmpty(feed)) return null;

                var parsedFeed = XElement.Parse(feed);
                messages = new List<RssMessage>();

                foreach (var item in parsedFeed.Element("channel").Elements("item"))
                {
                    var title = item.Element("title");
                    var link = item.Element("link");

                    messages.Add(new RssMessage(title.Value, "", DateTime.Now, link.Value));
                }

                MessagingCenter.Send(this, "RssFeedUpdated", new object()); // Отправляем сообщение о том, что лента обновилась
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
