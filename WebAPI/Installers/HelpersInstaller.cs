using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Helpers;

namespace WebAPI.Installers
{
    public class HelpersInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IJsonKeyValueGetter, JsonKeyValueGetter>();
        }
    }
}
