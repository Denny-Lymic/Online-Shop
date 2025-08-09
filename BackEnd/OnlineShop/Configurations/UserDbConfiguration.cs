using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Entities;

namespace BackEnd.Configurations
{
    public class UserDbConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.Password)
                .IsRequired();
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(u => u.Address)
                .HasMaxLength(100);

            builder.HasMany(u => u.Orders)
                .WithOne(u => u.User);
        }
    }
}
