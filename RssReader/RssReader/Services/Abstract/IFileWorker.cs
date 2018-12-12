using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RssReader.Models;

namespace RssReader.Services.Abstract
{
    /// <summary>Сервис работы с файловой системой</summary>
    public interface IFileWorker
    {
        /// <summary>Чтение списка Rss-каналов из памяти</summary>
        /// <param name="errorhandler">Обработчик ошибок</param>
        /// <returns>null - неудачное чтение</returns>
        Task<IEnumerable<Rss>> LoadRssListAsync(Action<string> errorhandler = null);

        /// <summary>Сохранение списка Rss-каналов в память</summary>
        /// <param name="list">Список каналов</param>
        /// <param name="errorhandler">Обработчик ошибок</param>
        /// <returns>true - успешная запись</returns>
        Task<bool> SaveRssListAsync(IEnumerable<Rss> list, Action<string> errorhandler = null);
    }
}
