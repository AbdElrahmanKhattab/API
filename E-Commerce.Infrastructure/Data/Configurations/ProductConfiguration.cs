using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(product => product.Name).HasMaxLength(100).IsRequired();
        builder.Property(product => product.Description).HasMaxLength(500).IsRequired();
        builder.Property(product => product.PictureUrl).HasMaxLength(200).IsRequired();
        builder.Property(product => product.Price).HasColumnType("decimal(18,2)");

        builder.HasOne(product => product.Brand)
            .WithMany()
            .HasForeignKey(product => product.BrandId);

        builder.HasOne(product => product.Type)
            .WithMany()
            .HasForeignKey(product => product.TypeId);
    }
}
