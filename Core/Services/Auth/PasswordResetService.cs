using Core.Common;
using Core.Interfaces.Auth;
using Core.Requests;
using Core.ServiceResponses;
using Core.Services.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Storage.Identity;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services.Auth
{
    class PasswordResetService :  IPasswordResetService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        public PasswordResetService(UserManager<User> userManager, IConfiguration config, IEmailService emailService) {
            _userManager = userManager;
            _config = config;
            _emailService = emailService;
        }

        public async Task<ServiceResponse> SendResetPasswordEmail(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var urlString = AdditionalAuthMetods.BuildUrl(token, user.UserName, _config["Paths:ResetPassword"]);

                await _emailService.SendEmailAsync(_config["SMTP:Name"], user.Email, "Reset your password", urlString);
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
                    return new ErrorResponse { StatusCode = HttpStatusCode.UnprocessableEntity, Message = AdditionalAuthMetods.CreateValidationErrorMessage(result) };

                return new SuccessResponse { Message = "Password changed succesfully" };
            }
            catch { return new ErrorResponse { Message = "Sending the e-mail failed", StatusCode = HttpStatusCode.UnprocessableEntity }; };
        }
    }
}
