using Helpers;
using RssReader.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
