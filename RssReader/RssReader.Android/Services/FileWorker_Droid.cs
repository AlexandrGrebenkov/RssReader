using System;
using System.Collections.Generic;
using Android.Content;
using RssReader.Services;
using System.Threading.Tasks;
using RssReader.Models;
using System.IO;
using System.Xml.Serialization;

[assembly: Xamarin.Forms.Dependency(typeof(RssReader.Droid.Services.FileWorker_Droid))]
namespace RssReader.Droid.Services
{
    class FileWorker_Droid : IFileWorker
    {
        public Context Context;
        string AppDirectory;

        public FileWorker_Droid()
        {
            Context = Android.App.Application.Context;
            AppDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public Task<IEnumerable<Rss>> LoadRssListAsync(Action<string> errorhandler = null)
        {
            IEnumerable<Rss> result = null;

            string filename = Path.Combine(AppDirectory, "RssFeeds.xml");

            if (!File.Exists(filename))
                return Task.FromResult(result);

            try
            {
                using (var fs = new FileStream(filename, FileMode.Open))
                using (var reader = new StreamReader(fs))
                {
                    var xs = new XmlSerializer(typeof(IEnumerable<Rss>));
                    try { result = (List<Rss>)xs.Deserialize(reader); }
                    catch (Exception ex)
                    {
#if DEBUG
                        errorhandler?.Invoke(ex.Message);
#else
                        errorhandler?.Invoke("Ошибка распознования файла!");
#endif
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                errorhandler?.Invoke(ex.Message);
#else
                errorhandler?.Invoke("Ошибка чтения файла!");
#endif
            }
            return Task.FromResult(result);
        }

        public Task<bool> SaveRssListAsync(IEnumerable<Rss> list, Action<string> errorhandler = null)
        {

            string filename = Path.Combine(AppDirectory, "RssFeeds.xml");

            try
            {
                using (var fs = new FileStream(filename, FileMode.Create))
                using (var writer = new StreamWriter(fs))
                {
                    var xs = new XmlSerializer(typeof(IEnumerable<Rss>));

                    xs.Serialize(writer, list);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                errorhandler?.Invoke(ex.Message);
#else
                errorhandler?.Invoke("Ошибка сохранения файла!");
#endif
            }
            return Task.FromResult(true);
        }
    }
}