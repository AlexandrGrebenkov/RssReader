using Helpers;
using System;

namespace RssReader.Models
{
    /// <summary>Rss-Сообщение</summary>
    public class RssMessage
    {
        /// <summary>Заголовок</summary>
        public string Title { get; }

        /// <summary>Текст сообщения</summary>
        public string Text { get; }

        /// <summary>Дата публикации</summary>
        public DateTime Date { get; }

        /// <summary>Ссылка на сообщение</summary>
        public string Link { get; }
    }
}
