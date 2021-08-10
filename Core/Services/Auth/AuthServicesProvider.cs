using Core.Interfaces.Auth;
using Core.Interfaces.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Identity;

namespace Core.Services.Auth
{
    internal abstract class AuthServicesProvider
    {
        protected readonly UserManager<User> UserManager;
        protected readonly SignInManager<User> SignInManager;
        protected readonly IConfiguration Config;
        protected readonly IJwtGenerator JwtGenerator;
        protected readonly IEmailService EmailService;

        protected AuthServicesProvider
            (
            UserManager<User> userManager, 
            SignInManager<User> signInManager = null, 
            IConfiguration config = null, 
            IJwtGenerator jwtGenerator = null, 
            IEmailService emailService = null
            )
        {
            UserManager = userManager;
            SignInManager = signInManager;
            Config = config;
            JwtGenerator = jwtGenerator;
            EmailService = emailService;
        }
    }
}
