using Helpers;
using RssReader.Models;
using RssReader.Resources.Lang;
using RssReader.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RssReader.ViewModels
{
    /// <summary>Вью-модель для просмотра Rss-ленты</summary>
    class RssVM : BaseViewModel
    {
        IXmlFeedParser Parser { get; }
        INetworkWorker NetworkWorker { get; }

        /// <summary>Rss-канал</summary>
        Rss Rss { get; }

        /// <summary>Сообщения в ленте</summary>
        public IEnumerable<RssMessage> Messages
        {
            get => Rss.Messages;
            set
            {
                Rss.Messages = value;
                OnPropertyChanged(nameof(Messages));
                MessagingCenter.Send(this, "RssFeedUpdated", Rss); // Отправляем сообщение о том, что лента обновилась
            }
        }

        public RssVM(Rss rss, IXmlFeedParser parser, INetworkWorker networkWorker)
        {
            Rss = rss;
            Parser = parser;
            NetworkWorker = networkWorker;
            Title = Rss.Name;

            // Обновляем ленту при открытии
            Device.BeginInvokeOnMainThread(async () =>
            {
                var messages = await GetRssFeed(rss.Link);
                if (messages != null)
                    Messages = messages;
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
                    async error => await DisplayAlert(Common.Error, error, "", Common.Ok));
                if (messages != null)
                    Messages = messages;
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
        {
            var feed = await NetworkWorker.GetFeedStringAsync(rssLink, errorhandler);
            var messages = new List<RssMessage>(Parser.ParseXml(feed));

            return messages;
        }
    }
}
