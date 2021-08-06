using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Storage.Models;
using Storage.Models.Likes;
using Storage.Models.Photos;

namespace Core.DependencyInjections
{
    static class CrudServicesInjection
    {
        public static IServiceCollection AddCrudServices(this IServiceCollection services)
        {
            services.AddScoped<ICrudService<Author>, CrudService<Author>>();
            services.AddScoped<ICrudService<Book>, CrudService<Book>>();
            services.AddScoped<ICrudService<Category>, CrudService<Category>>();
            services.AddScoped<ICrudService<Photo>, CrudService<Photo>>();
            services.AddScoped<ICrudService<Review>, CrudService<Review>>();
            services.AddScoped<IUserCrudService<Author>, UserCrudService<Author>>();
            services.AddScoped<IUserCrudService<Book>, UserCrudService<Book>>();
            services.AddScoped<IUserCrudService<Category>, UserCrudService<Category>>();
            services.AddScoped<ICrudService<ReviewComment>, CrudService<ReviewComment>>();

            return services;
        }
    }
}
