using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackEnd.Entities;

namespace BackEnd.Configurations
{
    public class ProductDbConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Name)
                .IsUnique();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(p => p.Price)
                .IsRequired();
            builder.Property(p => p.Description)
                .IsRequired();
        }
    }
}


