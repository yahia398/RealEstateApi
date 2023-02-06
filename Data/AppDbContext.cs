using Microsoft.EntityFrameworkCore;
using RealEstateApi.Data.Configs;
using RealEstateApi.Models;

namespace RealEstateApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new CategoryEntityTypeConfiguration().Configure(modelBuilder.Entity<Category>());
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new PropertyEntityTypeConfiguration().Configure(modelBuilder.Entity<Property>());
        }

    }
}
