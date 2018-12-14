using Helpers;
using RssReader.Models;
using RssReader.Resources.Lang;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace RssReader.ViewModels
{
    /// <summary>Добавление/Редактирование Rss-каналов</summary>
    class AddNewRssVM : BaseViewModel
    {
        Rss Original; // Оригинальный объект (для проверки изменений)

        public bool IsChanged => string.Compare(Name.Trim(), Original.Name) != 0 ||
                                 string.Compare(Link.Trim(), Original.Link) != 0;

        public bool CanSave
        {
            get
            {
                NameError = string.Empty;
                if (string.IsNullOrWhiteSpace(Name))
                    NameError = Strings.NameCantBeEmpty;

                LinkError = string.Empty;
                if (string.IsNullOrWhiteSpace(Link))
                    LinkError = Strings.LinkCantBeEmpty;
                else if (!linkRegex.IsMatch(Link))
                    LinkError = Strings.LinkFormatError;

                if (string.IsNullOrWhiteSpace(NameError) &&
                    string.IsNullOrWhiteSpace(LinkError))
                    return true;
                else
                    return false;
            }

        }

        string _Name;
        /// <summary>Имя</summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value);

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
            Title = Titles.NewRss;
            IsNew = true;
            Original = new Rss("", "");

            Name = Original.Name;
            Link = Original.Link;

            cmdSave = new RelayCommand(Save);
        }

        public AddNewRssVM(INavigation navigation, Rss rss)
        {
            this.navigation = navigation;
            Title = Titles.EditRss;
            IsNew = false;

            Original = rss;

            Name = Original.Name;
            Link = Original.Link;

            cmdSave = new RelayCommand(Save);
        }
        
        /// <summary>Команда сохранения изменений</summary>
        public RelayCommand cmdSave { get; }

        public void Save()
        {
            if (!CanSave) return;

            Original.Name = Name.Trim();
            Original.Link = Link.Trim();
            if (IsNew)
                MessagingCenter.Send(this, "AddRss", Original);
            else
                MessagingCenter.Send(this, "EditRss", Original);
            navigation.PopAsync();
        }
    }
}
