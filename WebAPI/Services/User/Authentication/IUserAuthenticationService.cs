using System.Threading.Tasks;
using WebAPI.Models.Auth;

namespace WebAPI.Services
{
    public interface IUserAuthenticationService
    {
        public Task<Response> Login(LoginModel model);
        public Task<Response> Register(RegisterModel model);
        public Task<Response> RegisterAdmin(RegisterModel model);
    }
}
