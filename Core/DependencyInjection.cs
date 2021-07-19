using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Interfaces.Email;
using Core.Repositories;
using Core.Services;
using Core.Services.Auth;
using Core.Services.Email;
using Microsoft.Extensions.DependencyInjection;
using Storage.Models;
using System;

namespace Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IBaseRepository<Author>, BaseRepository<Author>>();
            services.AddScoped<IBaseRepository<Book>, BaseRepository<Book>>();
            services.AddScoped<IBaseRepository<Category>, BaseRepository<Category>>();
            services.AddScoped<IBaseRepository<Photo>, BaseRepository<Photo>>();

            services.AddScoped<ICrudService<Author>, CrudService<Author>>();
            services.AddScoped<ICrudService<Book>, CrudService<Book>>();
            services.AddScoped<ICrudService<Category>, CrudService<Category>>();
            services.AddScoped<ICrudService<Photo>, CrudService<Photo>>();

            services.AddScoped<ICreatorService<Author>, CreatorService<Author>>();
            services.AddScoped<IGetterService<Author>, GetterService<Author>>();
            services.AddScoped<IUpdaterService<Author>, UpdaterService<Author>>();
            services.AddScoped<IDeleterService<Author>, DeleterService<Author>>();

            services.AddScoped<ICreatorService<Book>, CreatorService<Book>>();
            services.AddScoped<IGetterService<Book>, GetterService<Book>>();
            services.AddScoped<IUpdaterService<Book>, UpdaterService<Book>>();
            services.AddScoped<IDeleterService<Book>, DeleterService<Book>>();

            services.AddScoped<ICreatorService<Category>, CreatorService<Category>>();
            services.AddScoped<IGetterService<Category>, GetterService<Category>>();
            services.AddScoped<IUpdaterService<Category>, UpdaterService<Category>>();
            services.AddScoped<IDeleterService<Category>, DeleterService<Category>>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICreatorService<Photo>, CreatorService<Photo>>();
            services.AddScoped<IGetterService<Photo>, GetterService<Photo>>();
            services.AddScoped<IUpdaterService<Photo>, UpdaterService<Photo>>();
            services.AddScoped<IDeleterService<Photo>, DeleterService<Photo>>();

            services.AddScoped<IPhotoService, PhotoService>();

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();

            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<ISearchRepository, SearchRepository>();

            return services;
        }
    }
}
