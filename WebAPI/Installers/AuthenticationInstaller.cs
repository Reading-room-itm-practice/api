using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Storage.DataAccessLayer;
using Storage.Identity;
using System.Text;

namespace WebAPI.Installers
{
    public class AuthenticationInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole<int>>(opttion =>
            {
                opttion.SignIn.RequireConfirmedEmail = true;

                opttion.Password.RequireDigit = true;
                opttion.Password.RequireLowercase = true;
                opttion.Password.RequireUppercase = true;
                opttion.Password.RequiredLength = 8;
                opttion.Password.RequiredUniqueChars = 1;
            })
                .AddEntityFrameworkStores<ApiDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080", "https://localhost:8080")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddGoogle(opts =>
            {
                opts.ClientId = configuration["Google:Id"];
                opts.ClientSecret = configuration["Google:Secret"];
                opts.SignInScheme = IdentityConstants.ExternalScheme;
            })
            //.AddFacebook(options =>
            //{
            //    options.AppId = configuration["Facebook:AppId"];
            //    options.AppSecret = configuration["Facebook:Secret"];
            //})
            //.AddTwitter(twitterOptions =>
            //{
            //    twitterOptions.ConsumerKey = configuration["Twitter:APIKey"];
            //    twitterOptions.ConsumerSecret = configuration["Twitter:Secret"];
            //    twitterOptions.RetrieveUserDetails = true;
            //})
            .AddGitHub(options =>
            {
                options.ClientId = "580f3ba333ddf4bed2e2";
                options.ClientSecret = "4fc8273adcb78aad9276f13a56c84f805d012539";
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidAudience = configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });
        }
    }
}
