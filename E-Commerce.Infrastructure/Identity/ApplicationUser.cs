using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
    public Address Address { get; set; } = new();
}
