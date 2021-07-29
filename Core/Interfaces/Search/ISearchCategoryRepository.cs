using Core.DTOs;
using Core.Enums;
using Core.Services;
using Storage.Models;

namespace Core.Interfaces.Search
{
    interface ISearchCategoryRepository
    {
        DataDto<Category> GetCategories(PaginationFilter filter, string searchString, SortType? sort);
    }
}
