using E_Commerce.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Data.Configurations;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(deliveryMethod => deliveryMethod.ShortName).HasMaxLength(50).IsRequired();
        builder.Property(deliveryMethod => deliveryMethod.DeliveryTime).HasMaxLength(50).IsRequired();
        builder.Property(deliveryMethod => deliveryMethod.Description).HasMaxLength(200).IsRequired();
        builder.Property(deliveryMethod => deliveryMethod.Price).HasColumnType("decimal(8,2)");
    }
}
