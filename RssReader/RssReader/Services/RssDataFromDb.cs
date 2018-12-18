using System.Linq;
using RssReader.Models;
using RssReader.Services.Abstract;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using RssReader.Resources.Lang;

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

        public void CreateRss(Rss rss, Action<string> errorHandler = null)
        {
            try
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
            catch (Exception ex)
            {
#if DEBUG
                errorHandler?.Invoke(ex.Message);
#else
                errorHandler?.Invoke(Strings.DbError);
#endif
            }
        }

        public void DeleteRss(Rss rss, Action<string> errorHandler = null)
        {
            try
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
            catch (Exception ex)
            {
#if DEBUG
                errorHandler?.Invoke(ex.Message);
#else
                errorHandler?.Invoke(Strings.DbError);
#endif
            }
        }

        public IEnumerable<Rss> GetRssList(Action<string> errorHandler = null)
        {
            IEnumerable<Rss> list = null;
            try
            {
                using (var db = new ApplicationContext(fileName))
                {
                    list = db.RssList.Include(r => r.Messages).ToList();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                errorHandler?.Invoke(ex.Message);
#else
                errorHandler?.Invoke(Strings.DbError);
#endif
            }
            return list;
        }

        public void UpdateRss(Rss rss, Action<string> errorHandler = null)
        {
            try
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
            catch (Exception ex)
            {
#if DEBUG
                errorHandler?.Invoke(ex.Message);
#else
                errorHandler?.Invoke(Strings.DbError);
#endif
            }
        }
    }
}
