using Core.Authorization;
using Core.Authorization.Requirements;
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
using Microsoft.Extensions.Logging;
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

            services.AddScoped<IBaseRepository<AuthorFollow>, FollowRepository<AuthorFollow>>();
            services.AddScoped<IBaseRepository<CategoryFollow>, FollowRepository<CategoryFollow>>();
            services.AddScoped<IBaseRepository<UserFollow>, FollowRepository<UserFollow>>();
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

            services.AddScoped<ICreatorService<Author>, CreatorService<Author>>();
            services.AddScoped<IGetterService<Author>, GetterService<Author>>();
            services.AddScoped<IUpdaterService<Author>, UpdaterService<Author>>();
            services.AddScoped<IDeleterService<Author>, DeleterService<Author>>();

            services.AddScoped<ICreatorService<CategoryFollow>, CreatorService<CategoryFollow>>();
            services.AddScoped<IExtendedGetterService<CategoryFollow>, ExtendedGetterService<CategoryFollow>>();
            services.AddScoped<IDeleterService<CategoryFollow>, DeleterService<CategoryFollow>>();

            services.AddScoped<ICreatorService<UserFollow>, CreatorService<UserFollow>>();
            services.AddScoped<IExtendedGetterService<UserFollow>, ExtendedGetterService<UserFollow>>();
            services.AddScoped<IDeleterService<UserFollow>, DeleterService<UserFollow>>();

            services.AddScoped<ICreatorService<AuthorFollow>, CreatorService<AuthorFollow>>();
            services.AddScoped<IExtendedGetterService<AuthorFollow>, ExtendedGetterService<AuthorFollow>>();
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

            services.AddScoped<IModifyAvalibilityChecker, ModifyAvailabilityChecker>();
            
            services.AddSingleton<IAuthorizationHandler, AuditableModelAuthorizationHandler>();
            services.AddScoped<IUserCrudService<Author>, UserCrudService<Author>>();
            services.AddScoped<IUserCrudService<Book>, UserCrudService<Book>>();
            services.AddScoped<IUserCrudService<Category>, UserCrudService<Category>>();

            return services;
        }
    }
}
