using Core.Common;
using Core.Interfaces.Auth;
using Core.Interfaces.Email;
using Core.Requests;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Identity;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services.Auth
{
    internal class PasswordResetService : AuthServicesProvider, IPasswordResetService
    {
        private readonly IAdditionalAuthMetods _additionalAuthMetods;

        public PasswordResetService(UserManager<User> userManager, IConfiguration config, IEmailService emailService, IAdditionalAuthMetods additionalAuthMethods) 
            : base(userManager, config: config, emailService: emailService)
        {
            _additionalAuthMetods = additionalAuthMethods;
        }

        public async Task<ServiceResponse> SendResetPasswordEmail(string email)
        {
            try
            {
                var user = await UserManager.FindByEmailAsync(email);
                var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                var urlString = _additionalAuthMetods.BuildUrl(token, user.UserName, Config["Paths:ResetPassword"]);

                await EmailService.SendEmailAsync(user.Email, "Reset your password", urlString);
            }
            catch
            {
                return ServiceResponse.Error("Sending the e-mail failed"); 
            }

            return ServiceResponse.Success("Email to reset your password's waiting for you in mailbox");
        }

        public async Task<ServiceResponse> ResetPassword(ResetPasswordRequest model)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(model.UserName);
                var result = await UserManager.ResetPasswordAsync(user, model.Token, model.newPassword);

                if (!result.Succeeded)
                    return ServiceResponse.Error(_additionalAuthMetods.CreateValidationErrorMessage(result));

                return ServiceResponse.Success("Password changed succesfully");
            }
            catch 
            {
                return ServiceResponse.Error("Sending the e-mail failed", HttpStatusCode.UnprocessableEntity);
            }
        }
    }
}
