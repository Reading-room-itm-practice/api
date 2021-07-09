using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebAPI.Identity.Auth;
using WebAPI.Models.Auth;
using WebAPI.Services.Email;

namespace WebAPI.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {

        private readonly UserManager<Identity.User> _userManager;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        
        public UserAuthenticationService(UserManager<Identity.User> userManager, IConfiguration config, IEmailService emailService)
        {
            _userManager = userManager;
            _config = config;
            _emailService = emailService;
        }

        public async Task<Response> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if(!await _userManager.IsEmailConfirmedAsync(user))
                    return new Response { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Invalid username or password!" };

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

                return new Response { StatusCode = HttpStatusCode.OK, Message = $"{tokenResponse}" };
            }

            return new Response { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Username or password is not correct!" };
        }
        public async Task<Response> Register(RegisterModel model)
        {
            Identity.User user = new();
            Response res = await RegisterUser(model, _userManager, user);

            if (res == null)
                return new Response { StatusCode = HttpStatusCode.Created, Message = "User created successfully!" };
                
            return res;
        }
        public async Task<Response> RegisterAdmin(RegisterModel model)
        {
            Identity.User user = new();
            Response res = await RegisterUser(model, _userManager, user);

            if (res == null)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);

                return new Response { StatusCode = HttpStatusCode.Created, Message = "User created." };
            }
            return res;
        }
        private async Task<Response> RegisterUser(RegisterModel model, UserManager<Identity.User> _userManager, Identity.User user)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new Response { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "User already exists!" };

            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return new Response { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "Email already used!" };

            user.Email = model.Email;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.UserName = model.Username;

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new Response { StatusCode = HttpStatusCode.UnprocessableEntity, Message = "User creation failed! Please check password details and try again." };
                
            await _userManager.AddToRoleAsync(user, UserRoles.User);
            var userFromDb = await _userManager.FindByNameAsync(user.UserName);
            
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);

            var uriBuilder = new UriBuilder(_config["ReturnPaths:ConfirmEmail"]);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["username"] = userFromDb.UserName;
            uriBuilder.Query = query.ToString();
            var urlString = uriBuilder.ToString();

            var senderEmail = _config["ReturnPaths:SenderEmail"];
            await _emailService.SendEmailAsync(senderEmail, userFromDb.Email, "Confirm your email address", urlString);


            return null;
        }
        public async Task<Response> ConfirmEmail(ConfirmEmailModel model)
        {
            if (model.UserName == null)
                return new Response { StatusCode = HttpStatusCode.BadRequest, Message = "Link is invalid" };
            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user == null)
                return new Response { StatusCode = HttpStatusCode.BadRequest, Message = "Link is invalid" };
            var result = await _userManager.ConfirmEmailAsync(user, model.Token);

            if (result.Succeeded)
                return new Response { StatusCode = HttpStatusCode.OK, Message = "Email confirmed succesfully" };
            return new Response { StatusCode = HttpStatusCode.BadRequest , Message = "Link is invalid" };
        }
    }
}
