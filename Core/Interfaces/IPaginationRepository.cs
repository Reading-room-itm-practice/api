using Core.Common;
using Core.DTOs;
using Core.Services;
using Storage.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPaginationRepository
    {
        public Task<ExtendedData<IEnumerable<T>>> FindAll<T>(PaginationFilter filter) where T : class;
    }
}
