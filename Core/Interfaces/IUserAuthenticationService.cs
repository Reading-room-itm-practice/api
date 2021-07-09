using System.Threading.Tasks;
using Core.DTOs;
using Core.ServiceResponses;

namespace Core.Interfaces
{
    public interface IUserAuthenticationService
    {
        public Task<SuccessResponse<string>> Login(LoginDto model);
        public Task<SuccessResponse<string>> Register(RegisterDto model);
        public Task<SuccessResponse<string>> RegisterAdmin(RegisterDto model);
    }
}
