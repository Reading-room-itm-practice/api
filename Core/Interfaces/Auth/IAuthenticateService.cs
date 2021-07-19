using System.Threading.Tasks;
using Core.DTOs;
using Core.Requests;
using Core.ServiceResponses;

namespace Core.Interfaces.Auth
{
    public interface IAuthenticateService
    {
        public Task<ServiceResponse> Login(LoginRequest model);
        public Task<ServiceResponse> Register(RegisterRequest model);
        public Task<ServiceResponse> ConfirmEmail(EmailDto model);
        public Task<ServiceResponse> ResetPassword(ResetPasswordRequest model);
        public Task<ServiceResponse> SendResetPasswordEmail(string userName);
    }
}
