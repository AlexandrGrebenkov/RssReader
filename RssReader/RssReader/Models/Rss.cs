using System.Collections.Generic;

namespace RssReader.Models
{
    public class Rss
    {
        /// <summary>Имя</summary>
        public string Name { get; }

        /// <summary>Путь</summary>
        public string Link { get; }

        /// <summary>Список сообщений</summary>
        IEnumerable<RssMessage> RssMessages { get; set; }

        public Rss(string name, string link)
        {
            Name = name;
            Link = link;
        }
    }
}
