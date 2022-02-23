using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<ActivityImage> ActivityImages { get; set; }
        public DbSet<Enroll> Enrolls { get; set; }
        public string DbPath { get; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "ActivitySystem.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(
            //    @"Server=(localdb)\mssqllocaldb;Database=ActivitySystem;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Activity>()      // ActivityImage 與 Activity 的一對一關係
            .HasOne(a => a.ActivityImage)
            .WithOne(ai => ai.Activity)
            .HasForeignKey<ActivityImage>(ai => ai.ActivityId);

            builder.Entity<Enroll>()        // Activity 與 Enroll, Activity 與 ApplicationUser 的多對多關係
                .HasKey(e => new { e.ApplicationUserId, e.ActivityId});
        }
    }
}
