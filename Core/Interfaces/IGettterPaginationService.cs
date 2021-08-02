using Core.Common;
using Core.Response;
using Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGettterPaginationService
    {
        public Task<ServiceResponse<PagedResponse<IEnumerable<T1>>>> GetAll<T, T1>(PaginationFilter filter, string route) where T1 : class, IDto where T : class;
    }
}
