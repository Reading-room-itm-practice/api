using Core.ServiceResponses;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGoogleService
    {
        public Task<ServiceResponse> Login();
    }
}
