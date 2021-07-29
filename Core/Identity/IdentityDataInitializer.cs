using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Identity
{
    public static class IdentityDataInitializer
    {
        public static void SeedRolesAndAdmin
        (UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration)
        {
            SeedRoles(roleManager);
            SeedAdmin(userManager, configuration);
        }

        public static void SeedAdmin(UserManager<User> userManager, IConfiguration configuration)
        {
            var userExists = userManager.FindByNameAsync(configuration["Admin:Username"]).Result != null;
            if (!userExists)
            {
                User user = new()
                {
                    UserName = configuration["Admin:Username"],
                    Email = configuration["Admin:Email"],
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                IdentityResult result = userManager.CreateAsync(user, configuration["Admin:Password"]).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRoles.Admin).Wait();
                    userManager.AddToRoleAsync(user, UserRoles.User).Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole<Guid>> roleMenager)
        {
            var type = typeof(UserRoles);
            var fields = type.GetFields();
            List<string> fieldNames = new(type.GetFields().Select(x => x.Name));

            for (int i = 0; i < fields.Length; i++)
            {
                if (!roleMenager.RoleExistsAsync(fields[i].GetValue(type).ToString()).Result)
                    roleMenager.CreateAsync(new IdentityRole<Guid>(fields[i].GetValue(type).ToString())).Wait();
            }
        }
    }
}
