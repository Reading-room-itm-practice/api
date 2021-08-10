using Core.Interfaces.Auth;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Identity;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services.Auth
{
    internal class LoginService : AuthServicesProvider, ILoginService
    {
        public LoginService(UserManager<User> userManager, IConfiguration config, IJwtGenerator jwtGenerator) 
            : base(userManager, config: config, jwtGenerator: jwtGenerator) { }

        public async Task<ServiceResponse> Login(LoginRequest model)
        {
            var user = await UserManager.FindByNameAsync(model.Username);
            if (await UserManager.CheckPasswordAsync(user, model.Password))
            {
                if (!await UserManager.IsEmailConfirmedAsync(user))
                    return ServiceResponse.Error("Invalid username or password!");
                var roles = await UserManager.GetRolesAsync(user);
                var tokenResponse = JwtGenerator.GenerateJWTToken(Config, user, roles);

                return ServiceResponse<string>.Success($"{tokenResponse}", "Successful login");
            }

            return ServiceResponse.Error("Username or password is not correct!");
        }
    }
}
