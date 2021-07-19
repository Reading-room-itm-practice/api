using Core.DTOs;
using Core.Interfaces.Auth;
using Core.Interfaces.Email;
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
using System.Web;

namespace Core.Services.Auth
{
    public class AuthenticateService : IAuthenticateService
    {

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public AuthenticateService(UserManager<User> userManager, IConfiguration config, IEmailService emailService)
        {
            _userManager = userManager;
            _config = config;
            _emailService = emailService;
        }

        public async Task<ServiceResponse> Login(LoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if(!await _userManager.IsEmailConfirmedAsync(user))
                    return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Invalid username or password!" };

                var tokenResponse = await GenerateJWTToken(_userManager, _config, user.Email);

                return new SuccessResponse<string> { Message = "Successful login", Content = $"{tokenResponse}" };
            }

            return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Username or password is not correct!" };
        }

        public async Task<ServiceResponse> Register(RegisterRequest model)
        {
            try
            {
                if (await _userManager.FindByNameAsync(model.Username) != null || await _userManager.FindByEmailAsync(model.Email) != null)
                    return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Account already exists!" };

                User user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = CreateValidationErrorMessage(result) };

                await _userManager.AddToRoleAsync(user, UserRoles.User);

                user = await _userManager.FindByNameAsync(user.UserName);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var urlString = BuildUrl(token, user.UserName, _config["Paths:ConfirmEmail"]);

                await _emailService.SendEmailAsync(user.Email, "Confirm your email address", urlString);

                return new SuccessResponse { StatusCode = HttpStatusCode.Created, Message = "User created successfully! Confirm your email." };
            }
            catch (Exception e) {
                return new ErrorResponse { Message = $"Exception occured: {e}" };
            }
        }

        public async Task<ServiceResponse> ConfirmEmail(EmailDto model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var isConfirmed = user.EmailConfirmed;
                var result = await _userManager.ConfirmEmailAsync(user, model.Token);

                if (isConfirmed || !result.Succeeded)
                    throw new ArgumentException();

                return new SuccessResponse { Message = "Email confirmed succesfully" };
            }
            catch { return new ErrorResponse { StatusCode = HttpStatusCode.BadRequest, Message = "Link is invalid" }; };
        }

        public async Task<ServiceResponse> SendResetPasswordEmail(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var urlString = BuildUrl(token, user.UserName, _config["Paths:ResetPassword"]);

                await _emailService.SendEmailAsync(user.Email, "Reset your password", urlString);
            }
            catch { return new ErrorResponse { Message = "Something went wrong", StatusCode = HttpStatusCode.UnprocessableEntity}; };

            return new SuccessResponse { Message = "Email to reset your password's waiting for you in mailbox" };
        }

        public async Task<ServiceResponse> ResetPassword(ResetPasswordRequest model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.newPassword);

                if (!result.Succeeded)
                    return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = CreateValidationErrorMessage(result) };

                return new SuccessResponse { Message = "Password changed succesfully" };
            }
            catch { return new ErrorResponse { Message = "Something went wrong", StatusCode = HttpStatusCode.UnprocessableEntity }; };
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

        private string CreateValidationErrorMessage(IdentityResult result)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var error in result.Errors)
            {
                builder.Append(error.Description + " ");
            }

            return builder.ToString();
        }

        public static async Task<string> GenerateJWTToken(UserManager<User> _userManager, IConfiguration _config, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
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
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(4),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}