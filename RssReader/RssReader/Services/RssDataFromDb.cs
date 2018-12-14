using System.Linq;
using RssReader.Models;
using RssReader.Services.Abstract;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RssReader.Services
{
    public class RssDataFromDb : IRssData
    {
        readonly string fileName;

        public RssDataFromDb(string fileName)
        {
            this.fileName = fileName;
            using (var db = new ApplicationContext(fileName))
            {
                db.Database.EnsureCreated();
                if (db.RssList.Count() == 0)
                {
                    db.Add(new Rss("Meteoinfo.ru", "https://meteoinfo.ru/rss/forecasts/index.php?s=28440"));
                    db.Add(new Rss("Acomics.ru", "https://acomics.ru/~depth-of-delusion/rss"));
                    db.Add(new Rss("Calend.ru", "http://www.calend.ru/img/export/calend.rss"));
                    db.Add(new Rss("Old-Hard.ru", "http://www.old-hard.ru/rss"));

                    db.SaveChanges();
                }
            }
        }

        public void CreateRss(Rss rss)
        {
            using (var db = new ApplicationContext(fileName))
            {
                if (rss.Id == 0)
                {
                    db.Add(rss);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteRss(Rss rss)
        {
            using (var db = new ApplicationContext(fileName))
            {
                if (rss.Id != 0)
                {
                    db.Remove(rss);
                    db.SaveChanges();
                }
            }
        }

        public IEnumerable<Rss> GetRssList()
        {
            IEnumerable<Rss> list;
            using (var db = new ApplicationContext(fileName))
            {
                list = db.RssList.Include(r => r.Messages).ToList();
            }
            return list;
        }

        public void UpdateRss(Rss rss)
        {
            using (var db = new ApplicationContext(fileName))
            {
                if (rss.Id != 0)
                {
                    db.Update(rss);
                    db.SaveChanges();
                }
            }
        }
    }
}
