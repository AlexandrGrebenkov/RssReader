using Helpers;
using RssReader.Models;
using RssReader.Resources.Lang;
using RssReader.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RssReader.Services.Abstract;
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

        public RssListVM(INavigation navigation, IRssData rssData)
        {
            Title = Titles.RssList;
            RssList = new ObservableCollection<Rss>(rssData.GetRssList(async error => await DisplayAlert(Common.Error, error, "", Common.Ok)));

            MessagingCenter.Subscribe<AddNewRssVM, Rss>(this, "AddRss", (obj, rss) =>
            {
                if (RssList == null) return;
                RssList.Add(rss);
                rssData.CreateRss(rss, async error => await DisplayAlert(Common.Error, error, "", Common.Ok));
            });

            MessagingCenter.Subscribe<AddNewRssVM, Rss>(this, "EditRss", (obj, rss) =>
            {
                rssData.UpdateRss(rss, async error => await DisplayAlert(Common.Error, error, "", Common.Ok));
                RssList = new ObservableCollection<Rss>(RssList);
            });

            MessagingCenter.Subscribe<RssVM, Rss>(this, "RssFeedUpdated", (obj, rss) =>
            {
                rssData.UpdateRss(rss, async error => await DisplayAlert(Common.Error, error, "", Common.Ok));
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
                    {
                        RssList.Remove(rss);
                        rssData.DeleteRss(rss, async error => await DisplayAlert(Common.Error, error, "", Common.Ok));
                    }
                }
            });
        }

        public RelayCommand cmdAdd { get; }
        public ICommand cmdSelect { get; }
        public ICommand cmdContextAction { get; }
    }
}
