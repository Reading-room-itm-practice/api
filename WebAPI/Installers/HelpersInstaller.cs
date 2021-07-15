﻿using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Helpers;

namespace WebAPI.Installers
{
    public class HelpersInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IJsonKeyValueGetter, JsonKeyValueGetter>();
        }
    }
}
