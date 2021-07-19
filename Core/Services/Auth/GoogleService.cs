using Core.Interfaces.Auth;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Services.Auth
{
    class GoogleService : IGoogleService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signIn;
        private readonly IConfiguration _config;

        public GoogleService(UserManager<User> userManager, SignInManager<User> signIn, IConfiguration config)
        {
            _userManager = userManager;
            _signIn = signIn;
            _config = config;
        }

        public AuthenticationProperties GoogleRequest()
        {
            string redirectUri = _config["Google:RedirectUrl"];
            var properties = _signIn.ConfigureExternalAuthenticationProperties("Google", redirectUri);
            return properties;
        }

        public async Task<ServiceResponse> Login()
        {
            ExternalLoginInfo info = await _signIn.GetExternalLoginInfoAsync();
            if (info == null)
                return new ErrorResponse { Message = "Data not fetch" };

            var result = await _signIn.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
                return new SuccessResponse<string>
                {
                    Message = "Successful login",
                    Content = $"{await AuthenticateService.GenerateJWTToken(_userManager, _config, info.Principal.FindFirst(ClaimTypes.Email).Value)}" 
                };
            User user = new()
            {
                Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                UserName = info.Principal.FindFirst(ClaimTypes.Name).Value,
                EmailConfirmed = true
            };

            IdentityResult identResult = await _userManager.CreateAsync(user);
            if (identResult.Succeeded)
            {
                identResult = await _userManager.AddLoginAsync(user, info);
                if (identResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                    await _signIn.SignInAsync(user, false);
                    return new SuccessResponse<string>
                    {
                        Message = "Successful login",
                        Content = $"{await AuthenticateService.GenerateJWTToken(_userManager, _config, info.Principal.FindFirst(ClaimTypes.Email).Value)}"
                    };
                }
            }
            return new ErrorResponse { Message = "User not created" };
        }
    }
}
