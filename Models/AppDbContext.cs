using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public string DbPath { get; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "ActivitySystem.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=ActivitySystem;Integrated Security=True");
        }
    }
}
