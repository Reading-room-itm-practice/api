using Core.Requests;
using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface ILoginService
    {
        public Task<ServiceResponse> Login(LoginRequest model);
    }
}
