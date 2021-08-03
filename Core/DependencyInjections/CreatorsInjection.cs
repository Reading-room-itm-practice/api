using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Storage.Models;
using Storage.Models.Follows;
using Storage.Models.Likes;
using Storage.Models.Photos;

namespace Core.DependencyInjections
{
    static class CreatorsInjection
    {
        public static IServiceCollection AddCreators(this IServiceCollection services)
        {
            services.AddScoped<ICreatorService<Author>, CreatorService<Author>>();
            services.AddScoped<ICreatorService<CategoryFollow>, CreatorService<CategoryFollow>>();
            services.AddScoped<ICreatorService<UserFollow>, CreatorService<UserFollow>>(); 
            services.AddScoped<ICreatorService<AuthorFollow>, CreatorService<AuthorFollow>>();
            services.AddScoped<ICreatorService<Book>, CreatorService<Book>>();
            services.AddScoped<ICreatorService<Category>, CreatorService<Category>>();
            services.AddScoped<ICreatorService<Photo>, CreatorService<Photo>>();
            services.AddScoped<ICreatorService<Review>, CreatorService<Review>>();
            services.AddScoped<ICreatorService<ReviewLike>, CreatorService<ReviewLike>>();
            services.AddScoped<ICreatorService<ReviewCommentLike>, CreatorService<ReviewCommentLike>>();

            return services;
        }
    }
}
