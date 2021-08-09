using Core.DTOs;
using Core.Requests;
using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface IAuthenticationService
    {
        public Task<ServiceResponse> Login(LoginRequest model);
        public Task<ServiceResponse> ResetPassword(ResetPasswordRequest model);
        public Task<ServiceResponse> SendResetPasswordEmail(string userName);
        public Task<ServiceResponse> Register(RegisterRequest model);
        public Task<ServiceResponse> ConfirmEmail(EmailDto model);
    }
}
