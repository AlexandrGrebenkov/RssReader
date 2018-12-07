using Helpers;
using RssReader.Models;
using RssReader.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RssReader.ViewModels
{
    class RssListVM : BaseViewModel
    {
        ObservableCollection<Rss> _RssList;
        /// <summary>Список Rss-каналов</summary>
        public ObservableCollection<Rss> RssList
        {
            get { return _RssList; }
            set { SetProperty(ref _RssList, value); }
        }

        public RssListVM(INavigation navigation)
        {
            Title = "Список Rss";
            RssList = new ObservableCollection<Rss>
                {
                    new Rss("Calend", "http://www.calend.ru/img/export/calend.rss"),
                    new Rss("Old Hard", "http://www.old-hard.ru/rss"),
                };

            MessagingCenter.Subscribe<AddNewRssVM, Rss>(this, "AddRss", (obj, rss) =>
            {
                RssList?.Add(rss);
            });

            cmdAdd = new RelayCommand(() => navigation.PushAsync(new AddNewRssPage()));

            cmdSelect = new Command<Rss>(rss => navigation.PushAsync(new RssPage(rss)));

            cmdContextAction = new Command<Rss>(async rss =>
            {
                var answer = await DisplayActionSheet($"Действия с рассылкой \"{rss.Name}\"",
                    "Отмена", "",
                    "Изменить", "Удалить");
                switch (answer)
                {
                    case "Изменить":
                    {
                        await navigation.PushAsync(new AddNewRssPage(rss));
                        break;
                    }
                    case "Удалить":
                    {
                        if (await DisplayAlert("Внимание!",
                            $"Вы действительно хотите удалить рассылку \"{rss.Name}\"",
                            "Удалить", "Отмена"))
                            RssList.Remove(rss);
                        break;
                    }
                }
            });
        }

        public RelayCommand cmdAdd { get; }
        public ICommand cmdSelect { get; }
        public ICommand cmdContextAction { get; }
    }
}
