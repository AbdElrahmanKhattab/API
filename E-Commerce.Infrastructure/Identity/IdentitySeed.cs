using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Infrastructure.Identity;

public static class IdentitySeed
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roles = ["Admin", "Customer"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        const string email = "customer@test.com";
        if (await userManager.FindByEmailAsync(email) is null)
        {
            var user = new ApplicationUser
            {
                DisplayName = "Default Customer",
                Email = email,
                UserName = email,
                Address = new Address
                {
                    FirstName = "Default",
                    LastName = "Customer",
                    Street = "Route Academy Street",
                    City = "Cairo",
                    Country = "Egypt"
                }
            };

            var result = await userManager.CreateAsync(user, "P@ssw0rd");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Customer");
            }
        }
    }
}
