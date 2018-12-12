using Helpers;
using RssReader.Models;
using RssReader.Resources.Lang;
using RssReader.Services;
using RssReader.Views;
using System.Collections.ObjectModel;
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
            Title = Titles.RssList;
            Device.BeginInvokeOnMainThread(async () =>
            {
                var loaded = await fileWorker.LoadRssListAsync(async error => await DisplayAlert(Common.Error, error, "", Common.Ok));
                if (loaded == null)
                {
                    RssList = new ObservableCollection<Rss>
                    {
                        new Rss("Meteoinfo.ru", "https://meteoinfo.ru/rss/forecasts/index.php?s=28440"),
                        new Rss("Acomics.ru", "https://acomics.ru/~depth-of-delusion/rss"),
                        new Rss("Calend.ru", "http://www.calend.ru/img/export/calend.rss"),
                        new Rss("Old-Hard.ru", "http://www.old-hard.ru/rss"),
                    };
                    await fileWorker.SaveRssListAsync(RssList, async error => await DisplayAlert(Common.Error, error, "", Common.Ok));
                }
                else
                    RssList = new ObservableCollection<Rss>(loaded);
            });

            MessagingCenter.Subscribe<AddNewRssVM, Rss>(this, "AddRss", (obj, rss) =>
            {
                if (RssList == null) return;
                RssList.Add(rss);
                fileWorker.SaveRssListAsync(RssList, async error => await DisplayAlert(Common.Error, error, "", Common.Ok));
            });

            MessagingCenter.Subscribe<AddNewRssVM, Rss>(this, "EditRss", (obj, rss) =>
            {
                fileWorker.SaveRssListAsync(RssList, async error => await DisplayAlert(Common.Error, error, "", Common.Ok));
            });

            MessagingCenter.Subscribe<RssVM, object>(this, "RssFeedUpdated", (obj, arg) =>
             {
                 fileWorker.SaveRssListAsync(RssList, async error => await DisplayAlert(Common.Error, error, "", Common.Ok));
             });

            cmdAdd = new RelayCommand(() => navigation.PushAsync(new AddNewRssPage()));

            cmdSelect = new Command<Rss>(rss => navigation.PushAsync(new RssPage(rss)));

            cmdContextAction = new Command<Rss>(async rss =>
            {
                var answer = await DisplayActionSheet($"{Strings.FeedAction} \"{rss.Name}\"",
                    Common.Cancel, "",
                    Common.Edit, Common.Remove);

                if (answer == Common.Edit)
                {
                    await navigation.PushAsync(new AddNewRssPage(rss));
                }
                else if (answer == Common.Remove)
                {
                    if (await DisplayAlert(Common.Attention,
                        $"{Strings.AreUSureToRemoveFeed} \"{rss.Name}\"?",
                        Common.Remove, Common.Cancel))
                        RssList.Remove(rss);
                }

            });
        }

        public RelayCommand cmdAdd { get; }
        public ICommand cmdSelect { get; }
        public ICommand cmdContextAction { get; }
    }
}
