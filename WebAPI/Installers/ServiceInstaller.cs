using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces.User;
using WebAPI.Repositories;
using WebAPI.Services;

namespace WebAPI.Installers
{
    public class ServiceInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddSingleton<ILoggedUserProvider, LoggedUserProvider>();
        }
    }
}
