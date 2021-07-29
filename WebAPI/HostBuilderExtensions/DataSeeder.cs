using System;
using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Storage.DataAccessLayer;
using Storage.Identity;

namespace WebAPI.HostBuilderExtensions
{
    public static class DataSeeder
    {
        public static IHost SeedData(this IHost webHost)
        {
            using var scope = webHost.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var manager = services.GetRequiredService<UserManager<User>>();
                var role = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                var conf = services.GetRequiredService<IConfiguration>();
                var context = services.GetRequiredService<ApiDbContext>();

                IdentityDataInitializer.SeedRolesAndAdmin(manager, role, conf);
                ApiDbContextSeeder.SeedUsers(manager);
                ApiDbContextSeeder.SeedAuthors(context);
                ApiDbContextSeeder.SeedCategories(context);
                ApiDbContextSeeder.SeedFollows(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding.");
            }

            return webHost;
        }
    }
}
