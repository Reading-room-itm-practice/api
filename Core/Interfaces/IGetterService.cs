using Core.Response;
using Core.Services;
using Storage.Iterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGetterService<T> where T : IDbModel
    {
        public Task<ServiceResponse<PagedResponse<IEnumerable<IDto>>>> GetAll<IDto>(PaginationFilter filter, string route);
        public Task<ServiceResponse<IDto>> GetById<IDto>(int id);
    }
}
