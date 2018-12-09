using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RssReader.Models;

namespace RssReader.Services.Mock
{
    /// <summary>Мок сервиса работы с файловой системой</summary>
    class FileWorker_Mock : IFileWorker
    {
        public async Task<IEnumerable<Rss>> LoadRssListAsync(Action<string> errorhandler = null)
        {
            await Task.Delay(100); // Эмуляция длительного чтения
            return new List<Rss>()
                    {
                        new Rss("Meteoinfo.ru", "https://meteoinfo.ru/rss/forecasts/index.php?s=28440"),
                        new Rss("Acomics.ru", "https://acomics.ru/~depth-of-delusion/rss"),
                        new Rss("Calend.ru", "http://www.calend.ru/img/export/calend.rss"),
                        new Rss("Old-Hard.ru", "http://www.old-hard.ru/rss"),
                    };
        }

        public async Task<bool> SaveRssListAsync(IEnumerable<Rss> list, Action<string> errorhandler = null)
        {
            await Task.Delay(100); // Эмуляция длительной записи
            return true;
        }
    }
}
