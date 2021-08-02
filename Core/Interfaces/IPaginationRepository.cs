using Core.Services;
using Core.Services.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPaginationRepository
    {
        public Task<ExtendedData<IEnumerable<T>>> FindAll<T>(PaginationFilter filter) where T : class;
    }
}
