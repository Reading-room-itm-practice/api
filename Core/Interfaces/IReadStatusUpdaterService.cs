using Core.Requests;
using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReadStatusUpdaterService
    {
        public Task<ServiceResponse> UpdateReadStatus(ReadStatusRequest request);
    }
}
