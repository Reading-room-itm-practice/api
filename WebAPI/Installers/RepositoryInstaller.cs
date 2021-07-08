using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Installers
{
    public class RepositoryInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBaseRepository<Author>, BaseRepository<Author>>();
            services.AddScoped<IBaseRepository<Book>, BaseRepository<Book>>();
        }
    }
}
