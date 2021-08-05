using Core.Authorization;
using Core.Common;
using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Interfaces.Email;
using Core.Repositories;
using Core.Services;
using Core.Services.Auth;
using Core.Services.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Storage.Models;
using Storage.Models.Follows;
using Storage.Models.Likes;
using Storage.Models.Photos;
using System;

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
