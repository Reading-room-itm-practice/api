using Core.ServiceResponses;
using Storage.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGetterService<T> where T : IDbModel
    {
        public Task<ServiceResponse<IEnumerable<IDto>>> GetAll<IDto>();
        public Task<ServiceResponse<IDto>> GetById<IDto>(int id);
    }
}
