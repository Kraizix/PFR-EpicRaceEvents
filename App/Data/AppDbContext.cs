using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Race> Races { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<RaceResult> RaceResults { get; set; }
        public DbSet<ResultItem> ResultItems { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Categories)
                .WithMany(c => c.Vehicles)
                .UsingEntity(join => join.ToTable("VehiclesCategories"));
            modelBuilder.Entity<Race>()
                .HasMany(r => r.AuhtorizedCategories)
                .WithMany(c => c.Races)
                .UsingEntity(join => join.ToTable("RacesCategories"));
        }
    }
}