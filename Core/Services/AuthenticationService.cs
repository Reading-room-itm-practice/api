using Core.DTOs;
using Core.Interfaces;
using Core.Services.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Storage.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Services
{
    public class AuthenticationService : IUserAuthenticationService
    {

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        
        public AuthenticationService(UserManager<User> userManager, IConfiguration config, IEmailService emailService)
        {
            _userManager = userManager;
            _config = config;
            _emailService = emailService;
        }
        public async Task<ResponseDto> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if(!await _userManager.IsEmailConfirmedAsync(user))
                    return new ResponseDto { StatusCode = StatusCodes.Status422UnprocessableEntity, Message = "Invalid username or password!" };

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
                    expires: DateTime.Now.AddHours(4),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                var tokenResponse = new JwtSecurityTokenHandler().WriteToken(token);

                return new ResponseDto { StatusCode = StatusCodes.Status200OK, Message = $"{tokenResponse}" };
            }

            return new ResponseDto { StatusCode = StatusCodes.Status422UnprocessableEntity, Message = "Username or password is not correct!" };
        }
        public async Task<ResponseDto> Register(RegisterDto model)
        {
            if (await _userManager.FindByNameAsync(model.Username) != null || await _userManager.FindByEmailAsync(model.Email) != null)
                return new ResponseDto { StatusCode = StatusCodes.Status422UnprocessableEntity, Message = "Account already exists!" };

            User user = new()
            {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new ResponseDto { StatusCode = StatusCodes.Status422UnprocessableEntity, Message = result.Errors.ToString() };

            await _userManager.AddToRoleAsync(user, UserRoles.User);
            var userFromDb = await _userManager.FindByNameAsync(user.UserName);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);
            var urlString = BuildUrl(token, userFromDb.UserName, _config["ReturnPaths:ConfirmEmail"]);

            await _emailService.SendEmailAsync(_config["ReturnPaths:SenderEmail"], userFromDb.Email, "Confirm your email address", urlString);

            return new ResponseDto { StatusCode = StatusCodes.Status201Created, Message = "User created successfully! Confirm your email." };
        }
        public async Task<ResponseDto> ConfirmEmail(EmailDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var result = await _userManager.ConfirmEmailAsync(user, model.Token);

            if (model.UserName == null || user  == null || !result.Succeeded || user.EmailConfirmed)
                return new ResponseDto { StatusCode = StatusCodes.Status400BadRequest, Message = "Link is invalid" };

            return new ResponseDto { StatusCode = StatusCodes.Status200OK, Message = "Email confirmed succesfully" };
        }
        public async Task<ResponseDto> SendResetPasswordEmail(string email)
        {
            var userFromDb = await _userManager.FindByEmailAsync(email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(userFromDb);
            
            var urlString = BuildUrl(token, userFromDb.UserName, _config["ReturnPaths:ResetPassword"]);

            await _emailService.SendEmailAsync(_config["ReturnPaths:SenderEmail"], userFromDb.Email, "Reset your password", urlString);

            return new ResponseDto { StatusCode = StatusCodes.Status201Created, Message = "Email to reset your password's waiting for you in mailbox" };
        }
        public async Task<ResponseDto> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.newPassword);

            if (model.UserName == null || user == null || !result.Succeeded)
                return new ResponseDto { StatusCode = StatusCodes.Status400BadRequest, Message = "Link is invalid" };

            return new ResponseDto { StatusCode = StatusCodes.Status200OK, Message = "Password changed succesfully" };
        }
        private string BuildUrl(string token, string username, string path)
        {
            var uriBuilder = new UriBuilder(path);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["username"] = username;
            uriBuilder.Query = query.ToString();
            var urlString = uriBuilder.ToString();

            return urlString;
        }
    }
}