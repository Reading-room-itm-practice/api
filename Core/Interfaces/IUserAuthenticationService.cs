using System.Threading.Tasks;
using Core.DTOs;

namespace Core.Interfaces
{
    public interface IUserAuthenticationService
    {
        public Task<Response> Login(LoginModel model);
        public Task<Response> Register(RegisterModel model);
        public Task<Response> RegisterAdmin(RegisterModel model);
    }
}
