using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DailyMusicalNote
{
    public class Save
    {
        [Key]
        public int Id { get; set; }

        public DateTime dateTime { get; set; }
        public int score { get; set; }
        public int accuracy { get; set; }
        public string gameplayTime { get; set; }
        public int noteCounter { get; set; }
        public Difficulty difficulty { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Save> Save { get; set; }

        private string _dbPath = Path.Combine(FileSystem.AppDataDirectory, "save.db");

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Filename={_dbPath}");
    }

}
