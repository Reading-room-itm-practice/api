using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Http;
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

namespace Core.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public UserAuthenticationService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<SuccessResponse<string>> Login(LoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
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

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(4),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                var tokenResponse = new JwtSecurityTokenHandler().WriteToken(token);

                return new SuccessResponse<string> { StatusCode = HttpStatusCode.OK, Message = $"{tokenResponse}" };
            }

            return new SuccessResponse<string> { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Username or password is not correct!" };
        }

        public async Task<SuccessResponse<string>> Register(RegisterRequest model)
        {
            User user = new();
            SuccessResponse<string> response = await RegisterUser(model, _userManager, user);

            if (response == null)
                return new SuccessResponse<string> { StatusCode = HttpStatusCode.Created, Message = "User created successfully!" };

            return response;
        }

        public async Task<ServiceResponse> RegisterAdmin(RegisterRequest model)
        {
            User user = new();
            ServiceResponse response = await RegisterUser(model, _userManager, user);

            if (response == null)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);

                return new SuccessResponse { StatusCode = HttpStatusCode.Created, Message = "User created." };
            }
            return response;
        }

        private async Task<ServiceResponse> RegisterUser(RegisterRequest model, UserManager<User> _userManager, User user)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new SuccessResponse<string> { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "User already exists!" };

            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return new SuccessResponse<string> { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Email already used!" };

            user.Email = model.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.UserName = model.Username;

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = result.Errors.ToString() };

            await _userManager.AddToRoleAsync(user, UserRoles.User);
            return Succ();
        }
    }
}
