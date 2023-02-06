using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApi.Models;

namespace RealEstateApi.Data.Configs
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // DataTypes
            builder.Property(p=> p.Name).HasColumnType("VARCHAR(50)");
            builder.Property(p => p.Email).HasColumnType("VARCHAR(100)");
            builder.Property(p => p.Password).HasColumnType("VARCHAR(50)");
            builder.Property(p => p.Phone).HasColumnType("VARCHAR(50)");
        }
    }
}
