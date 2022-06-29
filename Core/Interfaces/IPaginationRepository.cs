using Core.Services;
using Core.Services.Search;
using Storage.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPaginationRepository
    {
        public Task<ExtendedData<IEnumerable<T>>> FindAll<T>(PaginationFilter filter, bool isAdmin = false) where T : class, IApproveable;
    }
}
