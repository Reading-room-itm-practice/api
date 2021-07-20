using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Storage.Identity;
using System;

namespace Storage.HosBuilderExtensions
{
    public static class DataSeeder
    {
        public static IHost SeedData(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var manager = services.GetRequiredService<UserManager<User>>();
                    var role = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
                    var conf = services.GetRequiredService<IConfiguration>();
                    IdentityDataInitializer.SeedRolesAndAdmin(manager, role, conf);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding.");
                }
            }
            return webHost;
        }
    }
}
