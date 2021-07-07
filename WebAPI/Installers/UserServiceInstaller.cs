using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Interfaces;
using Storage.Identity;

namespace WebAPI.Installers
{
    public class UserServiceInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
        }
    }
}
