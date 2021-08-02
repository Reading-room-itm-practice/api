using Core.Interfaces;
using Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Storage.Models;
using Storage.Models.Follows;
using Storage.Models.Likes;
using Storage.Models.Photos;

namespace Core.DependencyInjections
{
    static class RepositoriesInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<AuthorFollow>, FollowedRepository<AuthorFollow>>();
            services.AddScoped<IBaseRepository<CategoryFollow>, FollowedRepository<CategoryFollow>>();
            services.AddScoped<IBaseRepository<UserFollow>, FollowedRepository<UserFollow>>();
            services.AddScoped<IBaseRepository<Author>, BaseRepository<Author>>();
            services.AddScoped<IBaseRepository<Book>, BaseRepository<Book>>();
            services.AddScoped<IBaseRepository<Category>, BaseRepository<Category>>();
            services.AddScoped<IBaseRepository<Photo>, BaseRepository<Photo>>();
            services.AddScoped<IBaseRepository<Review>, BaseRepository<Review>>();
            services.AddScoped<IBaseRepository<ReviewLike>, LikeRepository<ReviewLike>>();
            services.AddScoped<IBaseRepository<ReviewCommentLike>, LikeRepository<ReviewCommentLike>>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();

            return services;
        }
    }
}
