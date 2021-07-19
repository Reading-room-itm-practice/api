using Core.Common;
using Core.Interfaces.Auth;
using Core.Requests;
using Core.ServiceResponses;
using Core.Services.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Storage.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Auth
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IJwtGenerator _jwtGenerator;
        public LoginService(UserManager<User> userManager, IConfiguration config, IJwtGenerator jwtGenerator) {
            _userManager = userManager;
            _config = config;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<ServiceResponse> Login(LoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                    return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Invalid username or password!" };

                var roles = await _userManager.GetRolesAsync(user);
                var tokenResponse = _jwtGenerator.GenerateJWTToken(_config, user, roles);

                return new SuccessResponse<string> { Message = "Successful login", Content = $"{tokenResponse}" };
            }

            return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Username or password is not correct!" };
        }
    }
}
