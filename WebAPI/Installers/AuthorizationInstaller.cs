using Core.Authorization;
using Core.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Installers
{
    public class AuthorizationInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeletePolicy", policy =>
                    policy.Requirements.Add(new SameCreatorRequirement()));

                options.AddPolicy("UpdatePolicy", policy =>
                  policy.Requirements.Add(new SameCreatorRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, AuditableModelAuthorizationHandler>();
        }
    }
}
