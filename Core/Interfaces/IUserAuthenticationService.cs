using System.Threading.Tasks;
using Core.DTOs;

namespace Core.Interfaces
{
    public interface IUserAuthenticationService
    {
        public Task<ResponseDto> Login(LoginDto model);
        public Task<ResponseDto> Register(RegisterDto model);
        public Task<ResponseDto> RegisterAdmin(RegisterDto model);
    }
}
