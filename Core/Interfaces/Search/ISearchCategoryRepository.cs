using Core.DTOs;
using Core.Enums;
using Core.Services;
using WebAPI.DTOs;

namespace Core.Interfaces.Search
{
    interface ISearchCategoryRepository
    {
        DataDto<CategoryDto> GetCategories(PaginationFilter filter, string searchString, SortType? sort);
    }
}
