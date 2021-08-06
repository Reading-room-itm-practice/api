using Core.Common;
using Core.Enums;
using Core.Response;
using Core.Services;

namespace Core.Interfaces.Search
{
    public interface ISearchService
    {
        public ServiceResponse SearchEntity<InData, OutData>(PaginationFilter filter, string route, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null) where InData : class where OutData : class, IDto;
    }
}
