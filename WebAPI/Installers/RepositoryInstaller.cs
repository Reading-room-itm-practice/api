using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Storage.Models;
using Core.Repositories;

namespace WebAPI.Installers
{
    public class RepositoryInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBaseRepository<Author>, BaseRepository<Author>>();
            services.AddScoped<IBaseRepository<Book>, BaseRepository<Book>>();
            services.AddScoped<IBaseRepository<Category>, BaseRepository<Category>>();
        }
    }
}
