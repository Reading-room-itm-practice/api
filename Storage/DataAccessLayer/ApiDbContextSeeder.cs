using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Storage.Identity;
using Storage.Models;
using Storage.Models.Follows;
using Storage.Models.Likes;

namespace Storage.DataAccessLayer
{
    public class ApiDbContextSeeder
    {
        private const int UsersCount = 100;
        private const int CategoriesCount = 50;
        private const int AuthorsCount = 1000;
        private const int FollowsCount = 250;
        private const int LikesCount = 2000;
        private const int BooksCount = 500;
        private const int ReviewsCount = 1000;
        private const int CommentsCount = 2000;

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
            if (context.Categories.Count() < UsersCount / 2 && context.Users.Any())
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
            if (context.Authors.Count() < AuthorsCount/2 && context.Users.Any())
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

        public static void SeedAuthorFollows(ApiDbContext context)
        {
            if (context.AuthorFollows.Count() < FollowsCount /2 && context.Authors.Any() && context.Users.Any())
            {
                var users = context.Users.ToListAsync().Result;
                var authors = context.Authors.ToListAsync().Result;

                var maxUsersElement = context.Users.Count() - 1;
                var maxAuthorsElement = context.Authors.Count() - 1;

                for (var i = 0; i < FollowsCount; i++)
                {
                    context.Add(new AuthorFollow()
                    {
                        CreatorId = users.ElementAt(Random.Next(maxUsersElement)).Id,
                        AuthorId = authors.ElementAt(Random.Next(maxAuthorsElement)).Id
                    });
                }
                context.SaveChanges();
            }
        }

        public static void SeedUsersFollows(ApiDbContext context)
        {
            if (context.UserFollows.Count() < FollowsCount / 2 && context.Users.Any())
            {
                var users = context.Users.ToListAsync().Result;

                var maxUsersElement = context.Users.Count() - 1;

                for (var i = 0; i < FollowsCount; i++)
                {
                    context.Add(new UserFollow()
                    {
                        CreatorId = users.ElementAt(Random.Next(maxUsersElement)).Id,
                        UserId = users.ElementAt(Random.Next(maxUsersElement)).Id
                    });
                }
                context.SaveChanges();
            }
        }

        public static void SeedCategoriesFollows(ApiDbContext context)
        {
            if (context.CategoryFollows.Count() < FollowsCount / 2 && context.Categories.Any() && context.Users.Any())
            {
                var users = context.Users.ToListAsync().Result;
                var categories = context.Categories.ToListAsync().Result;

                var maxUsersElement = context.Users.Count() - 1;
                var maxCategoriesElement = context.Categories.Count() - 1;

                for (var i = 0; i < FollowsCount; i++)
                {
                    context.Add(new CategoryFollow()
                    {
                        CreatorId = users.ElementAt(Random.Next(maxUsersElement)).Id,
                        CategoryId = categories.ElementAt(Random.Next(maxCategoriesElement)).Id
                    });
                }
                context.SaveChanges();
            }
        }

        public static void SeedBooks(ApiDbContext context)
        {
            if (context.Books.Count() < BooksCount / 2 && context.Categories.Any()
                && context.Authors.Any() && context.Users.Any())
            {
                var creatorId = context.Users.FirstOrDefaultAsync().Result.Id;
                var categories = context.Categories.ToListAsync().Result;
                var authors = context.Authors.ToListAsync().Result;

                var maxAuthorsElement = context.Authors.Count() - 1;
                var maxCategoriesElement = context.Categories.Count() - 1;

                for (var i = 0; i < BooksCount; i++)
                {
                    context.Add(new Book()
                    {
                        CreatorId = creatorId,
                        CategoryId = categories.ElementAt(Random.Next(maxCategoriesElement)).Id,
                        AuthorId = authors.ElementAt(Random.Next(maxAuthorsElement)).Id,
                        Title = Faker.Lorem.Words(1).FirstOrDefault(),
                        Description = Faker.Lorem.Sentence(100),
                        Approved = true
                    });
                }
                context.SaveChanges();
            }
        }

        public static void SeedReviews(ApiDbContext context)
        {
            if (context.Reviews.Count() < ReviewsCount / 2 && context.Books.Any() && context.Users.Any())
            {
                var users = context.Users.ToListAsync().Result;
                var books = context.Books.ToListAsync().Result;

                var maxUsersElement = context.Users.Count() - 1;
                var maxBooksElement = context.Books.Count() - 1;

                for (var i = 0; i < ReviewsCount; i++)
                {
                    context.Add(new Review()
                    {
                        CreatorId = users.ElementAt(Random.Next(maxUsersElement)).Id,
                        BookId = books.ElementAt(Random.Next(maxBooksElement)).Id,
                        Content = Faker.Lorem.Sentence(100)
                    });
                }
                context.SaveChanges();
            }
        }

        public static void SeedComments(ApiDbContext context)
        {
            if (context.ReviewComments.Count() < CommentsCount / 2 && context.Reviews.Any() && context.Users.Any())
            {
                var users = context.Users.ToListAsync().Result;
                var reviews = context.Reviews.ToListAsync().Result;

                var maxUsersElement = context.Users.Count() - 1;
                var maxReviewsElement = context.Reviews.Count() - 1;

                for (var i = 0; i < CommentsCount; i++)
                {
                    context.Add(new ReviewComment()
                    {
                        CreatorId = users.ElementAt(Random.Next(maxUsersElement)).Id,
                        ReviewId = reviews.ElementAt(Random.Next(maxReviewsElement)).Id,
                        Content = Faker.Lorem.Sentence(100)
                    });
                }
                context.SaveChanges();
            }
        }

        public static void SeedReviewLikes(ApiDbContext context)
        {
            if (context.ReviewLikes.Count() < LikesCount / 2 && context.Reviews.Any() && context.Users.Any())
            {
                var users = context.Users.ToListAsync().Result;
                var reviews = context.Reviews.ToListAsync().Result;

                var maxUsersElement = context.Users.Count() - 1;
                var maxReviewsElement = context.Reviews.Count() - 1;

                for (var i = 0; i < LikesCount; i++)
                {
                    context.Add(new ReviewLike()
                    {
                        CreatorId = users.ElementAt(Random.Next(maxUsersElement)).Id,
                        ReviewId = reviews.ElementAt(Random.Next(maxReviewsElement)).Id
                    });
                }
                context.SaveChanges();
            }
        }

        public static void SeedCommentsLikes(ApiDbContext context)
        {
            if (context.CommentLikes.Count() < LikesCount / 2 && context.ReviewComments.Any() && context.Users.Any())
            {
                var users = context.Users.ToListAsync().Result;
                var comments = context.ReviewComments.ToListAsync().Result;

                var maxUsersElement = context.Users.Count() - 1;
                var maxCommentsElement = context.ReviewComments.Count() - 1;

                for (var i = 0; i < LikesCount; i++)
                {
                    context.Add(new ReviewCommentLike()
                    {
                        CreatorId = users.ElementAt(Random.Next(maxUsersElement)).Id,
                        ReviewCommentId = comments.ElementAt(Random.Next(maxCommentsElement)).Id
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
