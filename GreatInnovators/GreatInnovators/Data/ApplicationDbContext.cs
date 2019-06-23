using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GreatInnovators.Models;

namespace GreatInnovators.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Guide> Guides { get; set; }

        public DbSet<GuideCity> GuideCities { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<GuideLanguage> GuideLanguages { get; set; }

        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GuideCity>()
                .HasKey(x => new { x.GuideId, x.CityId });

            modelBuilder.Entity<GuideLanguage>()
                .HasKey(x => new { x.GuideId, x.LanguageId });
        }
    }
}
