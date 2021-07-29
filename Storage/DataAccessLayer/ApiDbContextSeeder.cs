using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Storage.Identity;
using Storage.Models;
using Storage.Models.Follows;

namespace Storage.DataAccessLayer
{
    public class ApiDbContextSeeder
    {
        private const int UsersCount = 100;
        private const int CategoriesCount = 50;
        private const int AuthorsCount = 1000;
        private const int FollowsCount = 250;
        private static readonly Random Random = new(); 

        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.Users.Count() <= 1)
            {
                for (var i = 0; i < UsersCount; i++)
                {
                    User user = new()
                    {
                        UserName = Faker.Name.First(),
                        Email = Faker.Internet.Email(),
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true
                    };
                    var result = userManager.CreateAsync(user, "exsampleP@5sword").Result;
                    if (result.Succeeded)
                    { 
                        userManager.AddToRoleAsync(user, UserRoles.User).Wait();
                    }
                }
            }
        }

        public static void SeedCategories(ApiDbContext context)
        {
            if (!context.Categories.Any())
            {
                var creatorId = context.Users.FirstOrDefaultAsync().Result.Id;

                for (var i = 0; i < CategoriesCount; i++)
                {
                    context.Add(new Category
                    {
                        CreatorId = creatorId,
                        Name = Faker.Lorem.Sentence(1)
                    });
                }
                context.SaveChanges();
            }
        }

        public static void SeedAuthors(ApiDbContext context)
        {
            if (!context.Authors.Any())
            {
                var creatorId = context.Users.FirstOrDefaultAsync().Result.Id;

                for (var i = 0; i < AuthorsCount; i++)
                {
                    context.Add(new Author()
                    {
                        CreatorId = creatorId,
                        Name = Faker.Name.FullName(),
                        Biography = Faker.Lorem.Sentence(100)
                    });
                }
                context.SaveChanges();
            }
        }

        public static void SeedFollows(ApiDbContext context)
        {
            if (!context.Follows.Any())
            {
                var users = context.Users.ToListAsync().Result;
                var authors = context.Authors.ToListAsync().Result;
                var categories = context.Categories.ToListAsync().Result;

                var maxUsersElement = context.Users.Count() - 1;
                var maxAuthorsElement = context.Authors.Count() - 1;
                var maxCategoriesElement = context.Categories.Count() - 1;

                for (var i = 0; i < FollowsCount; i++)
                {
                    context.Add(new AuthorFollow()
                    {
                        CreatorId = users.ElementAt(Random.Next(maxUsersElement)).Id,
                        AuthorId = authors.ElementAt(Random.Next(maxAuthorsElement)).Id
                    });
            
                    context.Add(new CategoryFollow()
                    {
                        CreatorId = users.ElementAt(Random.Next(maxUsersElement)).Id,
                        CategoryId = categories.ElementAt(Random.Next(maxCategoriesElement)).Id
                    });
              
                    context.Add(new UserFollow()
                    {
                        CreatorId = users.ElementAt(Random.Next(maxUsersElement)).Id,
                        FollowingId = users.ElementAt(Random.Next(maxUsersElement)).Id
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
