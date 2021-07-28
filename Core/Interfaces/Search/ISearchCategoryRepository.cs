using Core.DTOs;
using Core.Enums;
using Core.Services;

namespace Core.Interfaces.Search
{
    interface ISearchCategoryRepository
    {
        DataDto<CategoryDto> GetCategories(PaginationFilter filter, string searchString, SortType? sort);
    }
}
