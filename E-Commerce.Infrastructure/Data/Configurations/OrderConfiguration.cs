using E_Commerce.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(order => order.SubTotal).HasColumnType("decimal(8,2)");
        builder.Property(order => order.Status).HasConversion<string>();

        builder.OwnsOne(order => order.ShippingAddress, address =>
        {
            address.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
            address.Property(a => a.LastName).HasMaxLength(50).IsRequired();
            address.Property(a => a.Street).HasMaxLength(50).IsRequired();
            address.Property(a => a.City).HasMaxLength(50).IsRequired();
            address.Property(a => a.Country).HasMaxLength(50).IsRequired();
        });

        builder.HasMany(order => order.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(order => order.DeliveryMethod)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
