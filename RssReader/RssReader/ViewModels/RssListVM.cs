using Helpers;
using RssReader.Models;
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

        public RssListVM()
        {
            Title = "Список Rss";
            RssList = new ObservableCollection<Rss>
                { new Rss("Name 1", "http://123"), new Rss("Name 2", "222"), };

            cmdAdd = new RelayCommand(() =>
            {

            });

            cmdSelect = new Command<Rss>(rss =>
            {

            });

            cmdContextAction = new Command<Rss>(async rss =>
            {
                var answer = await DisplayActionSheet($"Действия с рассылкой \"{rss.Name}\"",
                    "Отмена", "",
                    "Изменить", "Удалить");
                switch (answer)
                {
                    case "Изменить":
                    {
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
