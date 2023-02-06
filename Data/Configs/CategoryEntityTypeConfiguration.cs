using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApi.Models;

namespace RealEstateApi.Data.Configs
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // DataTypes
            builder.Property(p => p.Name).HasColumnType("VARCHAR(50)");
            builder.Property(p => p.ImageUrl).HasColumnType("VARCHAR(MAX)");
        }
    }
}
