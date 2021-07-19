using Core.DTOs;
using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Requests;
using Core.ServiceResponses;
using System.Threading.Tasks;

namespace Core.Services.Auth
{
    public class AuthenticationService : IAuthenticationService, ILoginService, IRegisterService, IPasswordResetService
    {

        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        private readonly IPasswordResetService _passwordResetService;

        public AuthenticationService(ILoginService loginService, IRegisterService registerService, IPasswordResetService passwordResetService)
        {
            _loginService = loginService;
            _registerService = registerService;
            _passwordResetService = passwordResetService;
        }

        public Task<ServiceResponse> ConfirmEmail(EmailDto model)
        {
            return _registerService.ConfirmEmail(model);
        }

        public Task<ServiceResponse> Login(LoginRequest model)
        {
            return _loginService.Login(model);
        }

        public Task<ServiceResponse> Register(RegisterRequest model)
        {
            return _registerService.Register(model);
        }

        public Task<ServiceResponse> ResetPassword(ResetPasswordRequest model)
        {
            return _passwordResetService.ResetPassword(model);
        }

        public Task<ServiceResponse> SendResetPasswordEmail(string userName)
        {
            return _passwordResetService.SendResetPasswordEmail(userName);
        }
    }
}