using Core.Common;
using Core.Interfaces.Auth;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Storage.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Services.Auth
{
    class ExternalLoginService : IExternalLoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signIn;
        private readonly IConfiguration _config;
        private readonly IJwtGenerator _jwtGenerator;

        public ExternalLoginService(UserManager<User> userManager, SignInManager<User> signIn, IConfiguration config, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signIn = signIn;
            _config = config;
            _jwtGenerator = jwtGenerator;
        }

        public ChallengeResult Request(string provider)
        {
            string redirectUri = _config["External:RedirectUrl"];
            var properties = _signIn.ConfigureExternalAuthenticationProperties(provider, redirectUri);
            return new ChallengeResult(provider, properties) ;
        }

        public async Task<ServiceResponse> Login()
        {
            ExternalLoginInfo info = await _signIn.GetExternalLoginInfoAsync();
            if (info == null)
                return new ErrorResponse { 
                    Message = "Error loading external login information",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };

            var result = await _signIn.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
            {
                await UserTokenResponse(info.Principal.FindFirst(ClaimTypes.Email).Value);
            }

            User user = AdditionalAuthMetods.CreateExternalUser(info);

            IdentityResult identResult = await _userManager.CreateAsync(user);
            if (identResult.Succeeded)
            {
                identResult = await _userManager.AddLoginAsync(user, info);
                if (identResult.Succeeded)
                {
                    await UserTokenResponse(user.Email);
                }
            }
            return new ErrorResponse { Message = "User not created" };
        }

        private async Task<ServiceResponse> UserTokenResponse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);
            return new SuccessResponse<string>
            {
                Message = "Successful login",
                Content = $"{_jwtGenerator.GenerateJWTToken(_config, user, roles)}"
            };
        }
    }
}
