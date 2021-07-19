using Core.ServiceResponses;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface IGoogleService
    {
        public AuthenticationProperties GoogleRequest();

        public Task<ServiceResponse> Login();
    }
}
