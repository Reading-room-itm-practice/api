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
        private readonly IJwtGenerator _jwtGenerator;

        public GoogleService(UserManager<User> userManager, SignInManager<User> signIn, IConfiguration config, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signIn = signIn;
            _config = config;
            _jwtGenerator = jwtGenerator;
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
            {
                var Guser = await _userManager.FindByEmailAsync(info.Principal.FindFirst(ClaimTypes.Email).Value);
                var roles = await _userManager.GetRolesAsync(Guser);
                return new SuccessResponse<string>
                {

                    Message = "Successful login",
                    Content = $"{_jwtGenerator.GenerateJWTToken(_config, Guser, roles)}"
                };
            }
                
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
                    var roles = await _userManager.GetRolesAsync(user);
                    return new SuccessResponse<string>
                    {
                        Message = "Successful login",
                        Content = $"{_jwtGenerator.GenerateJWTToken(_config, user, roles)}"
                    };
                }
            }
            return new ErrorResponse { Message = "User not created" };
        }
    }
}
