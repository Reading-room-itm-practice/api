using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Storage.Models;
using Storage.Models.Photos;

namespace Core.DependencyInjections
{
    static class UpdatersInjection
    {
        public static IServiceCollection AddUpdaters(this IServiceCollection services)
        {
            services.AddScoped<IUpdaterService<Author>, UpdaterService<Author>>();
            services.AddScoped<IUpdaterService<Book>, UpdaterService<Book>>();
            services.AddScoped<IUpdaterService<Category>, UpdaterService<Category>>();
            services.AddScoped<IUpdaterService<Photo>, UpdaterService<Photo>>();
            services.AddScoped<IUpdaterService<Review>, UpdaterService<Review>>();

            return services;
        }
    }
}
