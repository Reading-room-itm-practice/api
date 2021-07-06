using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Auth;

namespace WebAPI.Identity
{
    public static class MyIdentityDataInitializer
    {
        public static void SeedData
        (UserManager<User> userMenager, RoleManager<IdentityRole<int>> roleMenager)
        {
            SeedRoles(roleMenager);
            SeedUsers(userMenager);
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            var userExists = userManager.FindByNameAsync("Admin");
            if (userExists != null)
            {
                User user = new()
                {
                    UserName = "Admin",
                    Email = "example@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                IdentityResult result = userManager.CreateAsync(user, "S3cretP@@3").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
        public static void SeedRoles(RoleManager<IdentityRole<int>> roleMenager)
        {
            if (!roleMenager.RoleExistsAsync(UserRoles.Admin).Result)
                     roleMenager.CreateAsync(new IdentityRole<int>(UserRoles.Admin));
            if (!roleMenager.RoleExistsAsync(UserRoles.User).Result)
                roleMenager.CreateAsync(new IdentityRole<int>(UserRoles.User));
        }
    }
}
