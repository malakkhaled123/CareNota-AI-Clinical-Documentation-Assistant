// DataSeeder.cs
using CareNota.Data;
using CareNota.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CareNota.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext context,   // ✅ Added
        ILogger logger)
    {
        await RoleSeeder.SeedRolesAsync(roleManager);
        await SeedDefaultAdminAsync(userManager, context, logger);
    }

    private static async Task SeedDefaultAdminAsync(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context,   // ✅ Added
        ILogger logger)
    {
        const string AdminEmail = "admin@carenota.com";
        var adminPassword = Environment.GetEnvironmentVariable("CARENOTA_ADMIN_PASSWORD");
        if (string.IsNullOrWhiteSpace(adminPassword))
            throw new InvalidOperationException("CARENOTA_ADMIN_PASSWORD environment variable is required to seed the default admin.");

        if (await userManager.FindByEmailAsync(AdminEmail) is not null)
            return;

        var adminUser = new ApplicationUser
        {
            UserName = AdminEmail,
            Email = AdminEmail,
            FullName = "System Administrator",
            Gender = "Other",
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, RoleSeeder.Admin);

            // ✅ Now actually added to the DbContext and saved
            context.Admins.Add(new Admin
            {
                UserId = adminUser.Id,
                IsFirstLogin = true
            });
            await context.SaveChangesAsync();

            logger.LogInformation("✅ Default Admin seeded: {Email}", AdminEmail);
        }
        else
        {
            logger.LogError("❌ Failed to seed default admin: {Errors}",
                string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}