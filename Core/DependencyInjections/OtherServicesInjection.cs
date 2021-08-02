﻿using Core.Authorization;
using Core.Common;
using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Interfaces.Email;
using Core.Services;
using Core.Services.Auth;
using Core.Services.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.DependencyInjections
{
    static class OtherServicesInjection
    {
        public static IServiceCollection AddOtherServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IExternalLoginService, ExternalLoginService>();
            services.AddScoped<IAdditionalAuthMetods, AdditionalAuthMetods>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IModifyAvalibilityChecker, ModifyAvailabilityChecker>();
            services.AddSingleton<IAuthorizationHandler, AuditableModelAuthorizationHandler>();

            return services;
        }
    }
}
