using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyInjections
{
    public static class CoreDependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddCreators();
            services.AddGetters();
            services.AddUpdaters();
            services.AddDeleters();
            services.AddOtherServices();
            services.AddCrudServices();

            return services;
        }
    }
}
