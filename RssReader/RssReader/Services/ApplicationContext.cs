using Microsoft.EntityFrameworkCore;
using RssReader.Models;

namespace RssReader.Services
{
    class ApplicationContext : DbContext
    {
        private string _databaseFileName;

        public DbSet<Rss> RssList { get; set; }
        public DbSet<RssMessage> Messages { get; set; }

        public ApplicationContext(string databaseFileName)
        {
            _databaseFileName = databaseFileName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databaseFileName}");
        }
    }
}
