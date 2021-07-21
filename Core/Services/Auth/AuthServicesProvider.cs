using Core.Interfaces.Auth;
using Core.Interfaces.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Identity;

namespace Core.Services.Auth
{
    abstract class AuthServicesProvider
    {
        protected readonly SignInManager<User> _signInManager;
        protected readonly UserManager<User> _userManager;
        protected readonly IConfiguration _config;
        protected readonly IJwtGenerator _jwtGenerator;
        protected readonly IEmailService _emailService;

        public AuthServicesProvider
            (
            UserManager<User> userManager, 
            SignInManager<User> signInManager = null, 
            IConfiguration config = null, 
            IJwtGenerator jwtGenerator = null, 
            IEmailService emailService = null
            )
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _emailService = emailService;
        }
    }
}
