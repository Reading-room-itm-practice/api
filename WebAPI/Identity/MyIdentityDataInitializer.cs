using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
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
                IdentityResult result = userManager.CreateAsync(user, "S3cretP@@3").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
        public static void SeedRoles(RoleManager<IdentityRole<int>> roleMenager)
        {
            var t = typeof(UserRoles);
            var fields = t.GetFields();
            List<string> fieldNames = new List<string>(t.GetFields().Select(x => x.Name));

            for (int i = 0; i < fields.Length; i++)
            {
                if (!roleMenager.RoleExistsAsync(fields[i].GetValue(typeof(UserRoles)).ToString()).Result)
                    roleMenager.CreateAsync(new IdentityRole<int>(fields[i].GetValue(typeof(UserRoles)).ToString())).Wait();
            }
        }
    }
}
