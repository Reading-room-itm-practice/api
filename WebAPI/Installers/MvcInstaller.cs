using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Storage.Services;

namespace Storage.Installers
{
    public class MvcInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ILoggedUserProvider, LoggedUserProvider>();
            services.AddHttpContextAccessor();
            services.AddCore();
            services.AddControllers();
        }
    }
}
