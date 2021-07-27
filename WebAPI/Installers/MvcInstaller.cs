using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Interfaces;
using WebAPI.Services;

namespace WebAPI.Installers
{
    public class MvcInstaller : IInstaller
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
