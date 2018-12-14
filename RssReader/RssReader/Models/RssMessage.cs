using System;

namespace RssReader.Models
{
    /// <summary>Rss-Сообщение</summary>
    public class RssMessage
    {
        public int Id { get; set; }

        /// <summary>Заголовок</summary>
        public string Title { get; private set; }

        /// <summary>Текст сообщения</summary>
        public string Text { get; private set; }

        /// <summary>Дата публикации</summary>
        public DateTime Date { get; private set; }

        /// <summary>Ссылка на сообщение</summary>
        public string Link { get; private set; }

        public int RssId { get; set; }
        public Rss Rss { get; set; }

        public RssMessage(string title, string text, DateTime date, string link)
        {
            Title = title;
            Text = text;
            Date = date;
            Link = link;
        }

        private RssMessage() { }
    }
}
