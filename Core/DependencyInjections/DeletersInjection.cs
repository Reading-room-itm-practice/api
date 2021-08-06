using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Storage.Models;
using Storage.Models.Follows;
using Storage.Models.Likes;
using Storage.Models.Photos;

namespace Core.DependencyInjections
{
    static class DeletersInjection
    {
        public static IServiceCollection AddDeleters(this IServiceCollection services)
        {
            services.AddScoped<IDeleterService<Author>, DeleterService<Author>>();
            services.AddScoped<IDeleterService<CategoryFollow>, DeleterService<CategoryFollow>>();
            services.AddScoped<IDeleterService<UserFollow>, DeleterService<UserFollow>>();
            services.AddScoped<IDeleterService<AuthorFollow>, DeleterService<AuthorFollow>>();
            services.AddScoped<IDeleterService<Book>, DeleterService<Book>>();
            services.AddScoped<IDeleterService<Category>, DeleterService<Category>>();
            services.AddScoped<IDeleterService<Photo>, DeleterService<Photo>>();
            services.AddScoped<IDeleterService<Review>, DeleterService<Review>>();
            services.AddScoped<IDeleterService<ReviewLike>, DeleterService<ReviewLike>>();
            services.AddScoped<IDeleterService<ReviewCommentLike>, DeleterService<ReviewCommentLike>>();
            services.AddScoped<IDeleterService<ReviewComment>, DeleterService<ReviewComment>>();

            return services;
        }
    }
}
