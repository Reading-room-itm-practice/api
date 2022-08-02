using AutoMapper;
using Core.Interfaces;
using Core.Interfaces.Search;
using Core.Repositories.Search;
using Core.Services;
using Core.Services.Search;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISearchRepository, SearchRepository>();
            services.AddTransient<IUriService, UriService>();
            services.AddTransient<IMapper, Mapper>();
            services.AddTransient<ISearchService, SearchService>();
        }
    }
}
