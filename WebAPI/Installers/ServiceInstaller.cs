using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Interfaces.User;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Installers
{
    public class ServiceInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddSingleton<ILoggedUserProvider, LoggedUserProvider>();
            services.AddScoped<ICreatorService<Author>, CreatorService<Author>>();
            services.AddScoped<IGetterService<Author>, GetterService<Author>>();
            services.AddScoped<IUpdaterService<Author>, UpdaterService<Author>>();
            services.AddScoped<IDeleterService<Author>, DeleterService<Author>>();
            services.AddScoped<ICreatorService<Book>, CreatorService<Book>>();
            services.AddScoped<IGetterService<Book>, GetterService<Book>>();
            services.AddScoped<IUpdaterService<Book>, UpdaterService<Book>>();
            services.AddScoped<IDeleterService<Book>, DeleterService<Book>>();
        }
    }
}
