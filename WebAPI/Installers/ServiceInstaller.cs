using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
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
using WebAPI.Services.Email;

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
            services.AddSingleton<IEmailService,EmailService>();
            services.AddScoped<ICrudService<Author>, CrudService<Author>>();
            services.AddScoped<ICrudService<Book>, CrudService<Book>>();
        }
    }
}
