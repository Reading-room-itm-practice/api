using Core.DTOs;
using Core.Response;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReadStatusGetterService
    {
        public Task<ServiceResponse<ReadStatusDto>> GetReadStatus(int bookId);
    }
}
