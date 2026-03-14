using FlowDesk.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlowDesk.Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

        await context.Database.MigrateAsync();

        foreach (var role in new[] { "Admin", "User" })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(role));
            }
        }

        await EnsureUserAsync(userManager, "admin@test.com", "Admin123!", "Admin", "Portfolio Admin");
        await EnsureUserAsync(userManager, "user@test.com", "User123!", "User", "Portfolio User");

        if (!await context.ContentItems.AnyAsync())
        {
            context.ContentItems.AddRange(
                new ContentItem { Title = "Quarterly Product Roadmap", Description = "Roadmap draft for Q4 release plan.", IsPublished = true },
                new ContentItem { Title = "Community Launch Checklist", Description = "Checklist used by the operations team.", IsPublished = true },
                new ContentItem { Title = "Internal Draft Post", Description = "Unpublished post visible only to admin users.", IsPublished = false });

            await context.SaveChangesAsync();
        }
    }

    private static async Task EnsureUserAsync(
        UserManager<User> userManager,
        string email,
        string password,
        string role,
        string fullName)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            user = new User
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                throw new InvalidOperationException($"Failed to seed user {email}: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
            }
        }

        if (!await userManager.IsInRoleAsync(user, role))
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
