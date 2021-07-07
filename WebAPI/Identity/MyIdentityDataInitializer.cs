using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models.Auth;

namespace WebAPI.Identity
{
    public static class MyIdentityDataInitializer
    {
        public static void SeedData
        (UserManager<User> userMenager, RoleManager<IdentityRole<int>> roleMenager, IConfiguration configuration)
        {
            SeedRoles(roleMenager);
            SeedUsers(userMenager, configuration);
        }

        public static void SeedUsers(UserManager<User> userManager, IConfiguration configuration)
        {
            var userExists = userManager.FindByNameAsync("Admin").Result;
            if (userExists == null)
            {
                User user = new()
                {
                    UserName = "Admin",
                    Email = "example@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                IdentityResult result = userManager.CreateAsync(user, configuration["Admin:Secret"]).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRoles.Admin).Wait();
                    userManager.AddToRoleAsync(user, UserRoles.User).Wait();
                }
            }
        }
        public static void SeedRoles(RoleManager<IdentityRole<int>> roleMenager)
        {
            var type = typeof(UserRoles);
            var fields = type.GetFields();
            List<string> fieldNames = new List<string>(type.GetFields().Select(x => x.Name));

            for (int i = 0; i < fields.Length; i++)
            {
                if (!roleMenager.RoleExistsAsync(fields[i].GetValue(type).ToString()).Result)
                    roleMenager.CreateAsync(new IdentityRole<int>(fields[i].GetValue(type).ToString())).Wait();
            }
        }
    }
}
