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
            services.AddScoped<IExtendedBaseRepository<AuthorFollow>, ExtendedBaseRepository<AuthorFollow>>();
            services.AddScoped<IExtendedBaseRepository<CategoryFollow>, ExtendedBaseRepository<CategoryFollow>>();
            services.AddScoped<IExtendedBaseRepository<UserFollow>, ExtendedBaseRepository<UserFollow>>();
            services.AddScoped<IBaseRepository<AuthorFollow>, FollowsRepository<AuthorFollow>>();
            services.AddScoped<IBaseRepository<CategoryFollow>, FollowsRepository<CategoryFollow>>();
            services.AddScoped<IBaseRepository<UserFollow>, FollowsRepository<UserFollow>>();
            services.AddScoped<IBaseRepository<Author>, BaseRepository<Author>>();
            services.AddScoped<IBaseRepository<Book>, BaseRepository<Book>>();
            services.AddScoped<IBaseRepository<Category>, BaseRepository<Category>>();
            services.AddScoped<IBaseRepository<Photo>, BaseRepository<Photo>>();
            services.AddScoped<IBaseRepository<Review>, BaseRepository<Review>>();
            services.AddScoped<IBaseRepository<ReviewLike>, LikeRepository<ReviewLike>>();
            services.AddScoped<IBaseRepository<ReviewCommentLike>, LikeRepository<ReviewCommentLike>>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IBaseRepository<ReviewComment>, BaseRepository<ReviewComment>>();
            services.AddScoped<IReviewCommentRepository, ReviewCommentRepository>();

            return services;
        }
    }
}
