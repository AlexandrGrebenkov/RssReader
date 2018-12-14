using System.Collections.Generic;

namespace RssReader.Models
{
    public class Rss
    {
        public int Id { get; set; }

        /// <summary>Имя</summary>
        public string Name { get; set; }

        /// <summary>Путь</summary>
        public string Link { get; set; }

        /// <summary>Список сообщений</summary>
        public IEnumerable<RssMessage> Messages { get; set; }

        public Rss(string name, string link)
        {
            Name = name;
            Link = link;
        }
    }
}
