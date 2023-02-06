using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApi.Models;

namespace RealEstateApi.Data.Configs
{
    public class PropertyEntityTypeConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            // DataTypes
            builder.Property(p => p.Name).HasColumnType("VARCHAR(50)");
            builder.Property(p => p.Detail).HasColumnType("VARCHAR(MAX)");
            builder.Property(p => p.Address).HasColumnType("VARCHAR(150)");
            builder.Property(p => p.ImageUrl).HasColumnType("VARCHAR(MAX)");
            builder.Property(p => p.Price).HasColumnType("DECIMAL(18,2)");
            // Default Values
            builder.Property(p => p.IsTrending).HasDefaultValue(false);
            // Relationships
            // Relationship between Properties and Categories tables
            builder.HasOne(p=>p.Category)
                .WithMany(p=>p.Properties)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            // Relationship between Properties and Users tables
            builder.HasOne(p => p.User)
                .WithMany(p => p.Properties)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
