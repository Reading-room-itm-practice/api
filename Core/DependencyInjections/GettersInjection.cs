using Core.Interfaces;
using Core.Interfaces.Follows;
using Core.Services;
using Core.Services.Follows;
using Microsoft.Extensions.DependencyInjection;
using Storage.Models;
using Storage.Models.Follows;
using Storage.Models.Photos;

namespace Core.DependencyInjections
{
    static class GettersInjection
    {
        public static IServiceCollection AddGetters(this IServiceCollection services)
        {
            services.AddScoped<IGetterService<Author>, GetterService<Author>>();
            services.AddScoped<IFollowedGetter<CategoryFollow>, FollowedGetter<CategoryFollow>>();
            services.AddScoped<IFollowedGetter<UserFollow>, FollowedGetter<UserFollow>>();
            services.AddScoped<IFollowedGetter<AuthorFollow>, FollowedGetter<AuthorFollow>>();
            services.AddScoped<IFollowersGetter, FollowersGetter>();
            services.AddScoped<IGetterService<Book>, GetterService<Book>>();
            services.AddScoped<IGetterService<Category>, GetterService<Category>>();
            services.AddScoped<IGetterService<Photo>, GetterService<Photo>>();
            services.AddScoped<IGetterService<Review>, GetterService<Review>>();

            return services;
        }
    }
}
