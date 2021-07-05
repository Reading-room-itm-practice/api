using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Services;



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
