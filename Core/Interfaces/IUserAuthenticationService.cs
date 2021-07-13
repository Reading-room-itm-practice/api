using System.Threading.Tasks;
using Core.DTOs;
using Core.Requests;
using Core.ServiceResponses;

namespace Core.Interfaces
{
    public interface IUserAuthenticationService
    {
        public Task<ServiceResponse> Login(LoginRequest model);
        public Task<ServiceResponse> Register(RegisterRequest model);
        public Task<ServiceResponse> ConfirmEmail(ConfirmEmailModel model);
    }
}
