using Core.Interfaces;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Identity;
using Storage.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Services.Auth
{
    class GoogleService : IGoogleService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signIn;

        public GoogleService(UserManager<User> userManager, SignInManager<User> signIn)
        {
            _userManager = userManager;
            _signIn = signIn;
        }
        public async Task<ServiceResponse> Login()
        {
            ExternalLoginInfo info = await _signIn.GetExternalLoginInfoAsync();
            if (info == null)
                return new ErrorResponse { };

            var result = await _signIn.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            if (result.Succeeded)
                return new SuccessResponse { };
            else
            {
                User user = new ()
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };

                IdentityResult identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signIn.SignInAsync(user, false);
                        return new SuccessResponse { };
                    }
                }
                return new ErrorResponse { };
            }
        }
    }
}
