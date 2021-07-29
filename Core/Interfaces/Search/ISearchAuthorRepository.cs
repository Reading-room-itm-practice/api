using Core.DTOs;
using Core.Enums;
using Core.Services;
using Storage.Models;

namespace Core.Interfaces.Search
{
    interface ISearchAuthorRepository
    {
        DataDto<Author> GetAuthors(PaginationFilter filter, string searchString, SortType? sort);
    }
}
