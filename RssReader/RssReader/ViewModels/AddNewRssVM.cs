using Helpers;
using RssReader.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace RssReader.ViewModels
{
    class AddNewRssVM : BaseViewModel
    {
        Rss Current;
        Rss Original;

        string _Name;
        /// <summary>Имя</summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value);
                NameError = string.Empty;
                if (string.IsNullOrWhiteSpace(Name))
                    NameError = "Имя не может быть пустым";
                cmdSave?.RaiseCanExecuteChanged();
            }
        }

        string _NameError;
        /// <summary>Ошибка ввода имени</summary>
        public string NameError
        {
            get { return _NameError; }
            set { SetProperty(ref _NameError, value); }
        }

        string _Link;
        /// <summary>Ссылка</summary>
        public string Link
        {
            get { return _Link; }
            set
            {
                SetProperty(ref _Link, value);
                LinkError = string.Empty;
                if (string.IsNullOrWhiteSpace(Link))
                    LinkError = "Ссылка не может быть пустой";
                if (!linkRegex.IsMatch(Link))
                    LinkError = "Ошибка формата ссылки";
                cmdSave?.RaiseCanExecuteChanged();
            }
        }

        string _LinkError;
        /// <summary>Ошибка ввода ссылки</summary>
        public string LinkError
        {
            get { return _LinkError; }
            set { SetProperty(ref _LinkError, value); }
        }

        Regex linkRegex = new Regex(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$");

        INavigation navigation;
        bool IsNew;

        public AddNewRssVM(INavigation navigation)
        {
            this.navigation = navigation;
            Title = "Создание";
            IsNew = true;
            Original = new Rss("", "");
            Current = (Rss)Original.Clone();

            cmdSave = new RelayCommand(() =>
            {
                Original = (Rss)Current.Clone();
                MessagingCenter.Send(this, "AddRss", Original);
                navigation.PopAsync();
            }, () => string.IsNullOrWhiteSpace(NameError) &&
                    string.IsNullOrWhiteSpace(LinkError));
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
            }, () => string.IsNullOrWhiteSpace(NameError) &&
                    string.IsNullOrWhiteSpace(LinkError));
        }

        public RelayCommand cmdSave { get; }
    }
}
