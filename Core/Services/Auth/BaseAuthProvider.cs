using Core.Interfaces.Auth;
using Core.Interfaces.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Identity;

namespace Core.Services.Auth
{
    class BaseAuthProvider
    {
        protected readonly SignInManager<User> _signIn;
        protected readonly UserManager<User> _userManager;
        protected readonly IConfiguration _config;
        protected readonly IJwtGenerator _jwtGenerator;
        protected readonly IEmailService _emailService;

        public BaseAuthProvider(UserManager<User> userManager, IConfiguration config, IJwtGenerator jwtGenerator)
        {
            _config = config;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public BaseAuthProvider(UserManager<User> userManager, IConfiguration config, IEmailService emailService)
        {
            _config = config;
            _userManager = userManager;
            _emailService = emailService;
        }

        public BaseAuthProvider(UserManager<User> userManager, SignInManager<User> signIn, IConfiguration config, IJwtGenerator jwtGenerator)
        {
            _config = config;
            _signIn = signIn;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

    }
}
