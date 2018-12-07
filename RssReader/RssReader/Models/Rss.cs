using Helpers;
using System.Collections.Generic;

namespace RssReader.Models
{
    public class Rss : ObservableObject, ICreatable
    {
        string _Name;
        /// <summary>Имя</summary>
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }

        string _Link;
        /// <summary>Путь</summary>
        public string Link
        {
            get { return _Link; }
            set { SetProperty(ref _Link, value); }
        }

        /// <summary>Список сообщений</summary>
        IEnumerable<RssMessage> RssMessages { get; set; }

        public bool IsValid { get; }

        public Rss(string name, string link)
        {
            Name = name;
            Link = link;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Rss source)) return -1;

            if ((Name == source.Name) &
                (Link == source.Link))
                return 0;
            return 1;
        }

        public object Clone() => new Rss(Name, Link);
    }
}
