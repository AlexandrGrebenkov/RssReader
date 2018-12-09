using Helpers;
using RssReader.Models;
using RssReader.Services;
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

        public RssListVM(INavigation navigation, IFileWorker fileWorker)
        {
            Title = "Список Rss";
            Device.BeginInvokeOnMainThread(async () =>
            {
                var loaded = await fileWorker.LoadRssListAsync(async error => await DisplayAlert("Error", error, "", "Ok"));
                if (loaded == null)
                {
                    RssList = new ObservableCollection<Rss>
                    {
                        new Rss("Meteoinfo.ru", "https://meteoinfo.ru/rss/forecasts/index.php?s=28440"),
                        new Rss("Acomics.ru", "https://acomics.ru/~depth-of-delusion/rss"),
                        new Rss("Calend.ru", "http://www.calend.ru/img/export/calend.rss"),
                        new Rss("Old-Hard.ru", "http://www.old-hard.ru/rss"),
                    };
                    await fileWorker.SaveRssListAsync(RssList, async error => await DisplayAlert("Error", error, "", "Ok"));
                }
                else
                    RssList = new ObservableCollection<Rss>(loaded);
            });

            MessagingCenter.Subscribe<AddNewRssVM, Rss>(this, "AddRss", (obj, rss) =>
            {
                if (RssList == null) return;
                RssList.Add(rss);
                fileWorker.SaveRssListAsync(RssList, async error => await DisplayAlert("Error", error, "", "Ok"));
            });

            MessagingCenter.Subscribe<AddNewRssVM, Rss>(this, "EditRss", (obj, rss) =>
            {
                fileWorker.SaveRssListAsync(RssList, async error => await DisplayAlert("Error", error, "", "Ok"));
            });

            MessagingCenter.Subscribe<RssVM,object>(this, "RssFeedUpdated", (obj, arg) =>
            {
                fileWorker.SaveRssListAsync(RssList, async error => await DisplayAlert("Error", error, "", "Ok"));
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
