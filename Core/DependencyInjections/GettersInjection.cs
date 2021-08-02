using Core.Interfaces;
using Core.Services;
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
            services.AddScoped<IGetterByCreatorService<CategoryFollow>, GetterByCreatorService<CategoryFollow>>();
            services.AddScoped<IGetterByCreatorService<UserFollow>, GetterByCreatorService<UserFollow>>();
            services.AddScoped<IGetterByCreatorService<AuthorFollow>, GetterByCreatorService<AuthorFollow>>();
            services.AddScoped<IGetterService<Book>, GetterService<Book>>();
            services.AddScoped<IGetterService<Category>, GetterService<Category>>();
            services.AddScoped<IGetterService<Photo>, GetterService<Photo>>();
            services.AddScoped<IGetterService<Review>, GetterService<Review>>();

            return services;
        }
    }
}
