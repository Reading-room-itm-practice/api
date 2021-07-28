using Core;
using Core.Authorization.Requirements;
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
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeletePolicy", policy =>
                    policy.Requirements.Add(new SameCreatorRequirement()));

                options.AddPolicy("UpdatePolicy", policy =>
                  policy.Requirements.Add(new SameCreatorRequirement()));
            });
            services.AddCore();
            services.AddControllers();
        }
    }
}
