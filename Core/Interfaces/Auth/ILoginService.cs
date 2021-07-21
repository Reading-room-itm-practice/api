using Core.Requests;
using Core.ServiceResponses;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface ILoginService
    {
        public Task<ServiceResponse> Login(LoginRequest model);
    }
}
