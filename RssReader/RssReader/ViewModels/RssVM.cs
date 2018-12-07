using Helpers;
using RssReader.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RssReader.ViewModels
{
    class RssVM : BaseViewModel
    {
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

            cmdSelect = new Command<RssMessage>(message =>
            {

            });

            cmdRefresh = new RelayCommand(async () =>
            {
                IsBusy = true;
                await Task.Delay(1000);
                Rss.Messages = new List<RssMessage>() { new RssMessage("Title 1", "Text...", DateTime.Now, "http://google.ru") };
                IsBusy = false;
            });
        }

        public ICommand cmdSelect { get; }
        public RelayCommand cmdRefresh { get; }
    }
}
