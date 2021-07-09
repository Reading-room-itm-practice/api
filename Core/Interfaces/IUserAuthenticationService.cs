using System.Threading.Tasks;
using Core.DTOs;
using Core.Requests;
using Core.ServiceResponses;

namespace Core.Interfaces
{
    public interface IUserAuthenticationService
    {
        public Task<SuccessResponse<string>> Login(LoginRequest model);
        public Task<SuccessResponse<string>> Register(RegisterRequest model);
        public Task<SuccessResponse<string>> RegisterAdmin(RegisterRequest model);
    }
}
