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
    class PasswordResetService : AuthServicesProvider, IPasswordResetService
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
                var user = await _userManager.FindByEmailAsync(email);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var urlString = _additionalAuthMetods.BuildUrl(token, user.UserName, _config["Paths:ResetPassword"]);

                await _emailService.SendEmailAsync(user.Email, "Reset your password", urlString);
            }
            catch { return new ErrorResponse { Message = "Sending the e-mail failed", StatusCode = HttpStatusCode.UnprocessableEntity }; };

            return new SuccessResponse { Message = "Email to reset your password's waiting for you in mailbox" };
        }

        public async Task<ServiceResponse> ResetPassword(ResetPasswordRequest model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.newPassword);

                if (!result.Succeeded)
                    return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = _additionalAuthMetods.CreateValidationErrorMessage(result) };

                return new SuccessResponse { Message = "Password changed succesfully" };
            }
            catch { return new ErrorResponse { Message = "Sending the e-mail failed", StatusCode = HttpStatusCode.UnprocessableEntity }; };
        }
    }
}
