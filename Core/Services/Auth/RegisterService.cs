using Core.Common;
using Core.DTOs;
using Core.Interfaces.Auth;
using Core.Interfaces.Email;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Identity;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services.Auth
{
    class RegisterService : AuthServicesProvider, IRegisterService
    {
        private readonly IAdditionalAuthMetods _additionalAuthMetods;
        public RegisterService(UserManager<User> userManager, IConfiguration config, IEmailService emailService, IAdditionalAuthMetods additionalAuthMethods)
            :base(userManager, config: config, emailService: emailService)
        {
            _additionalAuthMetods = additionalAuthMethods;
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
                    return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = _additionalAuthMetods.CreateValidationErrorMessage(result) };

                await _userManager.AddToRoleAsync(user, UserRoles.User);

                user = await _userManager.FindByNameAsync(user.UserName);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var urlString = _additionalAuthMetods.BuildUrl(token, user.UserName, _config["Paths:ConfirmEmail"]);

                await _emailService.SendEmailAsync(user.Email, "Confirm your email address", urlString);

                return new SuccessResponse { StatusCode = HttpStatusCode.Created, Message = "User created successfully! Confirm your email." };
            }
            catch
            {
                return new ErrorResponse { Message = "An error accured while creating account.", StatusCode = HttpStatusCode.UnprocessableEntity};
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
                    throw new();

                return new SuccessResponse { Message = "Email confirmed succesfully" };
            }
            catch
            {
                return new ErrorResponse { StatusCode = HttpStatusCode.BadRequest, Message = "Link is invalid" };
            }
        }
    }
}
