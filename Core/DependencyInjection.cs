﻿using Core.Common;
using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Interfaces.Email;
using Core.Repositories;
using Core.Services;
using Core.Services.Auth;
using Core.Services.Email;
using Microsoft.Extensions.DependencyInjection;
using Storage.Models;
using Storage.Models.Follows;
using Storage.Models.Photos;
using System;

namespace Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IBaseRepository<AuthorFollow>, BaseRepository<AuthorFollow>>();
            services.AddScoped<IBaseRepository<CategoryFollow>, BaseRepository<CategoryFollow>>();
            services.AddScoped<IBaseRepository<UserFollow>, BaseRepository<UserFollow>>();
            services.AddScoped<IBaseRepository<Author>, BaseRepository<Author>>();
            services.AddScoped<IBaseRepository<Book>, BaseRepository<Book>>();
            services.AddScoped<IBaseRepository<Category>, BaseRepository<Category>>();
            services.AddScoped<IBaseRepository<Photo>, BaseRepository<Photo>>();
            services.AddScoped<IBaseRepository<Review>, BaseRepository<Review>>();

            services.AddScoped<ICrudService<Author>, CrudService<Author>>();
            services.AddScoped<ICrudService<Book>, CrudService<Book>>();
            services.AddScoped<ICrudService<Category>, CrudService<Category>>();
            services.AddScoped<ICrudService<Photo>, CrudService<Photo>>();
            services.AddScoped<ICrudService<Review>, CrudService<Review>>();
            services.AddScoped<ICrudService<AuthorFollow>, CrudService<AuthorFollow>>();
            services.AddScoped<ICrudService<CategoryFollow>, CrudService<CategoryFollow>>();
            services.AddScoped<ICrudService<UserFollow>, CrudService<UserFollow>>();


            services.AddScoped<ICreatorService<Author>, CreatorService<Author>>();
            services.AddScoped<IGetterService<Author>, GetterService<Author>>();
            services.AddScoped<IUpdaterService<Author>, UpdaterService<Author>>();
            services.AddScoped<IDeleterService<Author>, DeleterService<Author>>();

            services.AddScoped<ICreatorService<CategoryFollow>, CreatorService<CategoryFollow>>();
            services.AddScoped<IGetterService<CategoryFollow>, GetterService<CategoryFollow>>();
            services.AddScoped<IUpdaterService<CategoryFollow>, UpdaterService<CategoryFollow>>();
            services.AddScoped<IDeleterService<CategoryFollow>, DeleterService<CategoryFollow>>();

            services.AddScoped<ICreatorService<UserFollow>, CreatorService<UserFollow>>();
            services.AddScoped<IGetterService<UserFollow>, GetterService<UserFollow>>();
            services.AddScoped<IUpdaterService<UserFollow>, UpdaterService<UserFollow>>();
            services.AddScoped<IDeleterService<UserFollow>, DeleterService<UserFollow>>();

            services.AddScoped<ICreatorService<AuthorFollow>, CreatorService<AuthorFollow>>();
            services.AddScoped<IGetterService<AuthorFollow>, GetterService<AuthorFollow>>();
            services.AddScoped<IUpdaterService<AuthorFollow>, UpdaterService<AuthorFollow>>();
            services.AddScoped<IDeleterService<AuthorFollow>, DeleterService<AuthorFollow>>();

            services.AddScoped<ICreatorService<Book>, CreatorService<Book>>();
            services.AddScoped<IGetterService<Book>, GetterService<Book>>();
            services.AddScoped<IUpdaterService<Book>, UpdaterService<Book>>();
            services.AddScoped<IDeleterService<Book>, DeleterService<Book>>();

            services.AddScoped<ICreatorService<Category>, CreatorService<Category>>();
            services.AddScoped<IGetterService<Category>, GetterService<Category>>();
            services.AddScoped<IUpdaterService<Category>, UpdaterService<Category>>();
            services.AddScoped<IDeleterService<Category>, DeleterService<Category>>();

            services.AddScoped<ICreatorService<Photo>, CreatorService<Photo>>();
            services.AddScoped<IGetterService<Photo>, GetterService<Photo>>();
            services.AddScoped<IUpdaterService<Photo>, UpdaterService<Photo>>();
            services.AddScoped<IDeleterService<Photo>, DeleterService<Photo>>();

            services.AddScoped<ICreatorService<Review>, CreatorService<Review>>();
            services.AddScoped<IGetterService<Review>, GetterService<Review>>();
            services.AddScoped<IUpdaterService<Review>, UpdaterService<Review>>();
            services.AddScoped<IDeleterService<Review>, DeleterService<Review>>();

            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

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
            services.AddScoped<ISearchRepository, SearchRepository>();

            return services;
        }
    }
}
