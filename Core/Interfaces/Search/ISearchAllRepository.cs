using Core.DTOs;
using Core.Enums;
using Core.Services;
using Core.Services.Search;

namespace Core.Interfaces.Search
{
    interface ISearchAllRepository
    {
        DataDto<SearchAll> SearchAll(PaginationFilter filter, string route, string searchString, SortType? sort);
    }
}
