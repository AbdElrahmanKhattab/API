using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Identity;

public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
            .HasOne(user => user.Address)
            .WithOne()
            .HasForeignKey<Address>(address => address.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Address>(address =>
        {
            address.Property(a => a.FirstName).HasMaxLength(50);
            address.Property(a => a.LastName).HasMaxLength(50);
            address.Property(a => a.Street).HasMaxLength(50);
            address.Property(a => a.City).HasMaxLength(50);
            address.Property(a => a.Country).HasMaxLength(50);
        });
    }
}
