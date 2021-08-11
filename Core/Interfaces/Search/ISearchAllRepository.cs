using Core.DTOs;
using Core.Enums;
using Core.Services;
using Core.Services.Search;

namespace Core.Interfaces.Search
{
    interface ISearchAllRepository
    {
        ExtendedData<AllData> SearchAll(PaginationFilter filter, string searchString, SortType? sort);
    }
}
