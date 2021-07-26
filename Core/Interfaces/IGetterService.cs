using Core.Response;
using Core.Services;
using Storage.Iterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGetterService<T> where T : IDbModel
    {
        public Task<PagedResponse<IResponseDto>> GetAll<IResponseDto>(PaginationFilter filter, string route);
        public Task<IResponseDto> GetById<IResponseDto>(int id);
    }
}
