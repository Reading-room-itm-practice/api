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
        public LoginService(UserManager<User> userManager, IConfiguration config) {
            _userManager = userManager;
            _config = config;
        }

        public async Task<ServiceResponse> Login(LoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                    return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Invalid username or password!" };

                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    audience: _config["JWT:ValidAudience"],
                    issuer: _config["JWT:ValidIssuer"],
                    expires: DateTime.Now.AddHours(4),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                var tokenResponse = new JwtSecurityTokenHandler().WriteToken(token);

                return new SuccessResponse<string> { Message = "Successful login", Content = $"{tokenResponse}" };
            }

            return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Username or password is not correct!" };
        }
    }
}
