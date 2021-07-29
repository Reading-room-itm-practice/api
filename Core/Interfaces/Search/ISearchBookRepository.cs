using Core.DTOs;
using Core.Enums;
using Core.Services;
using Storage.Models;

namespace Core.Interfaces.Search
{
    interface ISearchBookRepository
    {
        DataDto<Book> GetBooks(PaginationFilter filter, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null);
    }
}
