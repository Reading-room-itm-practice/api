using Core.DTOs;
using Core.Enums;
using Core.Services;

namespace Core.Interfaces.Search
{
    interface ISearchBookRepository
    {
        DataDto<BookDto> GetBooks(PaginationFilter filter, string searchString, SortType? sort, int? minYear = null, int? maxYear = null,
            int? categoryId = null, int? authorId = null);
    }
}
