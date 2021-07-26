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
            var user = await _userManager.FindByNameAsync(model.Username);
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //if (!await _userManager.IsEmailConfirmedAsync(user))
                //    return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Invalid username or password!" };

                var roles = await _userManager.GetRolesAsync(user);
                var tokenResponse = _jwtGenerator.GenerateJWTToken(_config, user, roles);

                return new SuccessResponse<string> { Message = "Successful login", Content = $"{tokenResponse}" };
            }

            return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Username or password is not correct!" };
        }
    }
}
