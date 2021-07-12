using Core.DTOs;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserAuthenticationService
    {
        public Task<ResponseDto> Login(LoginDto model);
        public Task<ResponseDto> Register(RegisterDto model);
        public Task<ResponseDto> ConfirmEmail(EmailDto model);
        public Task<ResponseDto> ResetPassword(ResetPasswordDto model);
        public Task<ResponseDto> SendResetPasswordEmail(string userName);
    }
}
