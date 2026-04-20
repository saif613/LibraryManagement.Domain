using LibraryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Infrastructure.Seed.AdminSeed
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>(); // ✅

            await SeedRolesAsync(roleManager);
            await SeedAdminAsync(userManager, roleManager);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Admin")); // ✅
            }
        }

        private static async Task SeedAdminAsync(
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            var email = "saifradwan11@gmail.com";
            var password = "Daryl_dixon11!";

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                var admin = new User
                {
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true
                };

                admin.SetName("Saif");

                var result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
                else
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}