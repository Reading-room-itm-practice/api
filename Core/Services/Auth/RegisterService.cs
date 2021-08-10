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
    internal class RegisterService : AuthServicesProvider, IRegisterService
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
                if (await UserManager.FindByNameAsync(model.Username) != null || await UserManager.FindByEmailAsync(model.Email) != null)
                    return ServiceResponse.Error("Account already exists!");

                User user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return ServiceResponse.Error(_additionalAuthMetods.CreateValidationErrorMessage(result));

                await UserManager.AddToRoleAsync(user, UserRoles.User);

                user = await UserManager.FindByNameAsync(user.UserName);
                var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                var urlString = _additionalAuthMetods.BuildUrl(token, user.UserName, Config["Paths:ConfirmEmail"]);

                await EmailService.SendEmailAsync(user.Email, "Confirm your email address", urlString);

                return ServiceResponse.Success("User created successfully! Confirm your email.", HttpStatusCode.Created);
            }
            catch
            {
                return ServiceResponse.Error("An error accured while creating account.");
            }
        }

        public async Task<ServiceResponse> ConfirmEmail(EmailDto model)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(model.UserName);
                var isConfirmed = user.EmailConfirmed;
                var result = await UserManager.ConfirmEmailAsync(user, model.Token);

                if (isConfirmed || !result.Succeeded)
                    throw new();

                return ServiceResponse.Success("Email confirmed succesfully");
            }
            catch
            {
                return ServiceResponse.Error("Link is invalid", HttpStatusCode.BadRequest);
            }
        }
    }
}
