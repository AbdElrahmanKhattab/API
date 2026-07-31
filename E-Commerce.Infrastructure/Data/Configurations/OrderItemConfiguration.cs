using E_Commerce.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(item => item.Price).HasColumnType("decimal(8,2)");

        builder.OwnsOne(item => item.ItemOrdered, product =>
        {
            product.Property(p => p.ProductName).HasMaxLength(100).IsRequired();
            product.Property(p => p.PictureUrl).HasMaxLength(200).IsRequired();
        });
    }
}
