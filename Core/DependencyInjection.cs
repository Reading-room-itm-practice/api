using Core.Interfaces;
using Core.Repositories;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Storage.Interfaces;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IBaseRepository<Author>, BaseRepository<Author>>();
            services.AddScoped<IBaseRepository<Book>, BaseRepository<Book>>();
            services.AddScoped<IBaseRepository<Category>, BaseRepository<Category>>();

            services.AddScoped<ICrudService<Author>, CrudService<Author>>();
            services.AddScoped<ICrudService<Book>, CrudService<Book>>();
            services.AddScoped<ICrudService<Category>, CrudService<Category>>();

            services.AddScoped<ICreatorService<Author>, CreatorService<Author>>();
            services.AddScoped<IGetterService<Author>, GetterService<Author>>();
            services.AddScoped<IUpdaterService<Author>, UpdaterService<Author>>();
            services.AddScoped<IDeleterService<Author>, DeleterService<Author>>();

            services.AddScoped<ICreatorService<Book>, CreatorService<Book>>();
            services.AddScoped<IGetterService<Book>, GetterService<Book>>();
            services.AddScoped<IUpdaterService<Book>, UpdaterService<Book>>();
            services.AddScoped<IDeleterService<Book>, DeleterService<Book>>();

            services.AddScoped<ICreatorService<Category>, CreatorService<Category>>();
            services.AddScoped<IGetterService<Category>, GetterService<Category>>();
            services.AddScoped<IUpdaterService<Category>, UpdaterService<Category>>();
            services.AddScoped<IDeleterService<Category>, DeleterService<Category>>();

            services.AddScoped<ISearchService, SearchService>();

            return services;
        }
    }
}
