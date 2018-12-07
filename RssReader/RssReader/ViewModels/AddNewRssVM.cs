using Helpers;
using RssReader.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RssReader.ViewModels
{
    class AddNewRssVM : BaseViewModel
    {
        Rss _Current;
        public Rss Current
        {
            get { return _Current; }
            set { SetProperty(ref _Current, value); }
        }

        Rss _Original;
        public Rss Original
        {
            get { return _Original; }
            set { SetProperty(ref _Original, value); }
        }

        INavigation navigation;
        bool IsNew;

        public AddNewRssVM(INavigation navigation)
        {
            this.navigation = navigation;
            Title = "Создание";
            IsNew = true;
            Original = new Rss("1", "2");
            Current = (Rss)Original.Clone();

            cmdSave = new RelayCommand(() =>
            {
                Original = (Rss)Current.Clone();
                MessagingCenter.Send(this, "AddRss", Original);
                navigation.PopAsync();
            });
        }

        public AddNewRssVM(INavigation navigation, Rss rss)
        {
            this.navigation = navigation;
            Title = "Редактирование";
            IsNew = false;

            Original = rss;
            Current = (Rss)Original.Clone();

            cmdSave = new RelayCommand(() =>
            {
                Original = (Rss)Current.Clone();
                MessagingCenter.Send(this, "AddRss", Original);
                navigation.PopAsync();
            });
        }

        public RelayCommand cmdSave { get; }
    }
}
