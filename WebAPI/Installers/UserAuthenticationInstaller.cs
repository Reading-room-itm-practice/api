using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Storage.DataAccessLayer;
using Storage.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Storage.Installers
{
    public class UserAuthenticationInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole<int>>(opttion =>
            {
                opttion.SignIn.RequireConfirmedEmail = false;
                opttion.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApiDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => {
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

            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });
        }
    }
}
