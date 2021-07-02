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

            services.AddScoped<ICreator<Author>, Creator<Author>>();
            services.AddScoped<IGetter<Author>, Getter<Author>>();
            services.AddScoped<IUpdater<Author>, Updater<Author>>();
            services.AddScoped<IDeleter<Author>, Deleter<Author>>();
        }
    }
}
