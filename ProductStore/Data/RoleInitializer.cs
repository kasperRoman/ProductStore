using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ProductStore.Data
{
    public static class RoleInitializer
    {
        public static async Task SeedRolesAndAdminAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IConfiguration config)
        {
            // ✅ 1. Створюємо роль Admin, якщо її немає
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // ✅ 2. Читаємо email і пароль з appsettings.json
            string adminEmail = config["AdminUser:Email"];
            string adminPassword = config["AdminUser:Password"];

            // ✅ 3. Шукаємо користувача
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            // ✅ 4. Якщо немає — створюємо
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                // ✅ 5. Якщо створився — додаємо роль Admin
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}