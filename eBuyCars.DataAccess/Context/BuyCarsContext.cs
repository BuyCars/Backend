using BuyCars.Domain.Entities.Car;
using BuyCars.Domain.Entities.Favorite;
using BuyCars.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BuyCars.DataAccess.Context
{
    public class BuyCarsContext : DbContext
    {
        public DbSet<UserData> Users { get; set; }
        public DbSet<CarData> Cars { get; set; }
        public DbSet<FavoriteData> Favorites { get; set; }

        public BuyCarsContext(DbContextOptions<BuyCarsContext> options) : base(options) { }

        public BuyCarsContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbSession.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<UserData>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<FavoriteData>()
                .HasIndex(f => new { f.UserId, f.CarId })
                .IsUnique();

            modelBuilder.Entity<CarData>()
                .Property(c => c.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<CarData>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<UserData>()
                .Property(u => u.RegisteredOn)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<FavoriteData>()
                .Property(f => f.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
