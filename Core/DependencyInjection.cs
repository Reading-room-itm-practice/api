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

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBaseRepository<Author>, BaseRepository<Author>>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICreatorService<Author>, CreatorService<Author>>();
            services.AddScoped<IGetterService<Author>, GetterService<Author>>();
            services.AddScoped<IUpdaterService<Author>, UpdaterService<Author>>();
            services.AddScoped<IDeleterService<Author>, DeleterService<Author>>();

            return services;
        }
    }
}
